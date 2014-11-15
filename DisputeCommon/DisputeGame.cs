using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeCommon;
using DisputeCommon.Arguments;
using DisputeCommon.Feedback;

namespace DisputeCommon
{
    public class DisputeGame:ISoMSelector,IGameObservable, IPlayable
    {
        Dictionary<ArgNames, Argument> arguments = new Dictionary<ArgNames, Argument>();
        List<IGameObserver> observers = new List<IGameObserver>();
        SoMAcquiredHandler SoMHandler;
        IFeedbackWriter feedbackWritter;
        Match m;
        bool waiting = false;
        
        public IFeedbackWriter FeedbackWritter
        {
            get { return feedbackWritter; }
            set { feedbackWritter = value;
            Argument.Feedback = this.feedbackWritter;
            }
        }
        

        public int Turn
        {
            get { return m.Turn; }
            set { m.Turn = value; }
        }
        public Match Match
        {
            get { return m; }
            set { m = value;
            m.PossibleArguments = (from arg in arguments
                                      select arg.Value).ToList();
            }
        }

        public CharacterData World
        {
            get { return Match.World; }
            set { Match.World = value; }
        }

        public DataPlayer Player2
        {
            get { return Match.Player2; }
            set { Match.Player2 = value; }
        }

        public DataPlayer Player1
        {
            get { return Match.Player1; }
            set { Match.Player1 = value; }
        }

        

        public DisputeGame()
        {
            Argument.Feedback = this.FeedbackWritter;
            arguments.Add(ArgNames.Analyze, new Analyze());
            arguments.Add(ArgNames.Bluff, new Bluff());
            arguments.Add(ArgNames.Charm, new Charm());
            arguments.Add(ArgNames.Coerce, new Coerce());
            arguments.Add(ArgNames.Convince, new Convince());
            arguments.Add(ArgNames.Empathy, new Empathy());
            arguments.Add(ArgNames.Focus, new Focus());
            arguments.Add(ArgNames.Manipulate, new Manipulate());
            arguments.Add(ArgNames.Scare, new Scare());
            arguments.Add(ArgNames.Taunt, new Taunt());
            arguments.Add(ArgNames.Trick, new Trick());
        }

        /// <summary>
        /// Game decision point. This method gets messages from connection and decides what arguments to call, as well
        /// as taking care of runtime handlers for SoM requests
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="player"></param>
        /// <param name="data"></param>
        public void getMessageFromConnection(Messages.GameMessages msg,DataPlayer player, List<object> data)
        {
            if (!checkTurn(player))//Not right turn?
            {
                (this as IGameObservable).notifyObservers(Messages.GameMessages.NotPlayerTurn, player, null);
                Match.updateTranscript("Not your turn");
                return;
            }
            CharacterData attacker=Turn==0?Player1.Character:Player2.Character,defender=Turn==0?Player2.Character:Player1.Character;
            ArgNames actionName=0;
            ArgumentFeedback argFeedback=null;

            if (waiting && msg != Messages.GameMessages.somChosen)
            {
                Match.updateTranscript("Waiting for SOM");
                return;
            }

            switch (msg)
            {
                ///This case handles the response from a request to get SoM from the user
                ///We need to translate the response (SoMName,value) into values the Handler needs (JoySorrowValue,AngerFearValue)
                case Messages.GameMessages.somChosen:
                    SOM name = (SOM)data[0];
                    switch (name)
                    {
                        case SOM.Joy:
                            argFeedback=SoMHandler(1, 0);
                            break;
                        case SOM.Sorrow:
                            argFeedback=SoMHandler(-1, 0);
                            break;
                        case SOM.Anger:
                            argFeedback=SoMHandler(0, 1);
                            break;
                        case SOM.Fear:
                            argFeedback=SoMHandler(0, -1);
                            break;
                    }
                    waiting = false;//Just in case
                    break;

                case Messages.GameMessages.Trick:
                    actionName = ArgNames.Trick;
                    argFeedback=arguments[actionName].doArgument(attacker, defender, World);
                    if ((arguments[actionName] as Trick).repeatTurn())
                        Turn--;//We turn it back then the regular turn counter turns it back to my turn
                    break;

                case Messages.GameMessages.Manipulate:
                    actionName=ArgNames.Manipulate;
                    goto case Messages.GameMessages.SoMDependentArgument;

                case Messages.GameMessages.Taunt:
                    actionName = ArgNames.Taunt;
                    goto case Messages.GameMessages.SoMDependentArgument;

                case Messages.GameMessages.Focus:
                    actionName = ArgNames.Focus;
                    goto case Messages.GameMessages.SoMDependentArgument;

                case Messages.GameMessages.Empathy:
                    actionName = ArgNames.Empathy;
                    argFeedback=(arguments[actionName] as Empathy).doArgument(attacker, defender, World);
                    if (argFeedback!=null && argFeedback.result==Result.None)
                        Match.updateTranscript("Empathy cannot be used\n");
                    break;

                case Messages.GameMessages.Bluff:
                    actionName=ArgNames.Bluff;
                    goto case Messages.GameMessages.GenericArgument;

                case Messages.GameMessages.Charm:
                    actionName=ArgNames.Charm;
                    goto case Messages.GameMessages.GenericArgument;

                case Messages.GameMessages.Coerce:
                    actionName = ArgNames.Coerce;
                    goto case Messages.GameMessages.GenericArgument;

                case Messages.GameMessages.Convince:
                    actionName = ArgNames.Convince;
                    goto case Messages.GameMessages.GenericArgument;

                case Messages.GameMessages.Scare:
                    actionName = ArgNames.Scare;
                    goto case Messages.GameMessages.GenericArgument;

                case Messages.GameMessages.GenericArgument:
                    argFeedback=arguments[actionName].doArgument(attacker, defender, World);
                    break;

                case Messages.GameMessages.SoMDependentArgument:
                    (arguments[actionName] as SoMDependentArgument).Selector = this;
                    
                    argFeedback=(arguments[actionName] as SoMDependentArgument).doArgument(attacker, defender, World);
                    //If true then it succeeded so it needs to get SoM
                    if(argFeedback==null)                    
                        waiting = true;
                    break;

                
            }//End Switch

            //Update transcript if there is feedback
            if (argFeedback != null)
            {
                argFeedback.playerName = player.PlayerName;
                Match.updateTranscript(argFeedback);
            }

            if (!waiting)
            {
                //Check Game over
                if (Match.Goal.isGoalReached(defender))
                {
                    (this as IGameObservable).notifyObservers(Messages.GameMessages.GameOver, Match.Player1, null);
                    (this as IGameObservable).notifyObservers(Messages.GameMessages.GameOver, Match.Player2, null);
                }
                else//Not over
                {
                    //Tell players to update and go to next turn
                    (this as IGameObservable).notifyObservers(Messages.GameMessages.ArgumentDone, player, null);
                    Turn = (Turn + 1) % 2;
                }
            }
        }
        bool checkTurn(DataPlayer player)
        {
            if (Turn == 0)
                return player.Equals(Player1);
            else
                return player.Equals(Player2);
        }

        void ISoMSelector.acquireSoM(List<string> allowed, Result result)
        {
            DataPlayer active=Turn==0?Player1:Player2;
            
        }

        void ISoMSelector.acquireSoM(SOM allowed, Result result)
        {
            DataPlayer active=Turn==0?Player1:Player2;
            (this as IGameObservable).notifyObservers(Messages.GameMessages.GetSOM, active, 
                                                    new List<object>() { new SoMRequest(){Allowed=allowed,Result=result}});
        }

        void ISoMSelector.setAcquiredHandler(SoMAcquiredHandler handler)
        {
            SoMHandler=handler;
        }

        void IGameObservable.registerObserver(IGameObserver newObserver)
        {
            observers.Add(newObserver);
        }

        void IGameObservable.removeObserver(IGameObserver newObserver)
        {
            observers.Remove(newObserver);
        }

        void IGameObservable.notifyObservers(Messages.GameMessages msg,DataPlayer player,List<object> data)
        {
            foreach (IGameObserver obs in observers)
                obs.update(player,msg, data);
        }
    }
}

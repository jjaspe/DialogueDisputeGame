using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;

using DialogueDisputeFormsGame.Forms;

using DialogueDisputeFormsGameForm_Controllers;
using DisputeCommon;
using DialogueDisputeGameClient.Network;
using DialogueCommon.Interfaces;
using DialogueDisputeGameClient;


namespace DialogueDisputeFormsGameForm_Controllers
{
    public class GraphicMatchController:IMatchController,IConnectionObserver,IDisputeObservable
    {
        //Singleton
        static GraphicMatchController instance = new GraphicMatchController();
        public static GraphicMatchController getInstance()
        {
            return instance??new GraphicMatchController();
        }

        List<IControllerObserver> myObservers;
        //List<IDisputeObservable> myObservedControllers;
        IMatchView myView;
        IClientConnectionManager connection;
        public IClientConnectionManager dataManager
        {
            get { return connection; }
            set { connection = value; }
        }
        FeedbackForm myFeedbackForm;
        bool isRunning = false;
        Match match;
        DataPlayer player;

        public DataPlayer Player
        {
            get { return player; }
            set { player = value; }
        }
        

        public bool IsPlayerOne
        {
            get { return Match.Player1.Equals(Player); }
        }

        public Match Match
        {
            get { return match; }
            set { match = value; }
        }
        


        public GraphicMatchController()
        {
            myObservers = new List<IControllerObserver>();
            
            myFeedbackForm = new FeedbackForm();
        }
        public GraphicMatchController(IMatchView view)
        {
            myObservers = new List<IControllerObserver>();
            this.myView = view;
            view.Controller = this;
            myFeedbackForm = new FeedbackForm();
        }


        private void initPlayer1Form()
        {
            if (!isRunning)
            {
                if(myView==null)
                    myView = new MatchForm(this);
                myView.start();                
            }
        }

        private void initFeedbackForm()
        {

            myFeedbackForm.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            myFeedbackForm.SetDesktopLocation(1000, 0);
            myFeedbackForm.Show();
        }

        

        /* IMatchFormController implementation */
        public void MessageSentFromView(MatchViewMessage msg, List<object> args,object sender)
        {
            switch (msg)
            {
                case MatchViewMessage.Action:
                    this.dataManager.parseRequest(Messages.GameMessages.doAction, args,this);
                    break;
                case  MatchViewMessage.Close:
                    this.dataManager.parseRequest(Messages.GameMessages.GameOver, null,this);                  
                    (this as IDisputeViewController).stop();
                    break;
                case MatchViewMessage.Update:
                    this.dataManager.parseRequest(Messages.GameMessages.UpdateMatch, null, this);
                    break;
                case MatchViewMessage.SoM:
                    this.dataManager.parseRequest(Messages.GameMessages.somChosen, args, this);
                    break;
                case MatchViewMessage.GameOver:
                    this.dataManager.parseRequest(Messages.GameMessages.GameOver, null, this);
                    (this as IDisputeViewController).stop();
                    break;
                default:
                    break;
            }
        }

       
        /* IDisputeForm implementation*/
        void IDisputeViewController.start()
        {   
            initPlayer1Form();
        }
        void IDisputeViewController.stop()
        {
            this.myView.stop();
            (this as IDisputeViewController).formClosed("");
            myFeedbackForm.Close();
        }
        void IDisputeViewController.formClosed(string code)
        {
            notifyObservers("Match Form Closed");
        }
  
        /* IDispute Observable Implementation*/
        public void registerObserver(object obs)
        {
            myObservers.Add((IControllerObserver)obs);
        }
        public void removeObserver(object obs)
        {
            myObservers.Remove((IControllerObserver)obs);
        }
        public void notifyObservers(object arg=null)
        {
            foreach (IControllerObserver obs in myObservers)
                obs.update(arg);
        }
        
        void IConnectionObserver.update(object code)
        {
            if (code != null)
            {
                List<NamedParameter> dataList = (List<NamedParameter>)code;

                List<NamedParameter> m = DataTypes.findAll(dataList, DataType.serverMessage);
                if (m != null && m.Count > 0)
                {
                    Messages.GameMessages msg = (Messages.GameMessages)m[0].data;
                    switch (msg)
                    {
                        case Messages.GameMessages.UpdateMatch:
                            updateData(dataList);
                            break;
                        case Messages.GameMessages.UpdateAll:
                            updateData(dataList);
                            break;
                        case Messages.GameMessages.GetSOM:
                            List<NamedParameter> somRequest=DataTypes.findAll(dataList,DataType.SoMRequest);
                            SOM selected=myView.selectSoM((somRequest[0].data as SoMRequest).Allowed, (somRequest[0].data as SoMRequest).Result);
                            MessageSentFromView(MatchViewMessage.SoM, new List<object>() { selected }, this);
                            break;
                        case Messages.GameMessages.GameOver:
                            //Determine who won
                            bool won = Match.Turn == 0 ? player.Equals(Match.Player1) : player.Equals(Match.Player2);
                            myView.gameOver(won);
                            break;
                    }//End Switch
                }//End MEssage if                
            }//End Code if
        }

        //Helper methods
        void updateData(List<NamedParameter> dataList)
        {
            List<NamedParameter> m = DataTypes.findAll(dataList, DataType.match);
            if (m != null && m.Count > 0)
            {
                this.Match = (Match)m[0].data;
                this.Match = this.Match ?? new Match();
            }
        }

        void IDisputeObservable.registerObserver(object newObserver)
        {
           this.myObservers.Add((IControllerObserver)newObserver);
        }

        void IDisputeObservable.removeObserver(object newObserver)
        {
            this.myObservers.Remove((IControllerObserver)newObserver);
        }

        void IDisputeObservable.notifyObservers(object code)
        {
            foreach (IControllerObserver obs in this.myObservers)
                obs.update(code);
        }

        void IFeedbackWriter.WriteLine(string line)
        {
            myView.WriteLine(line);
        }
    }
}

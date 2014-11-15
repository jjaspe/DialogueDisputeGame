using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeGameConnection;
using DisputeCommon;
using DialogueDisputeGameServer.Game_Interfaces;
using System.ServiceModel;
using System.Xml;
using DisputeCommon.Interfaces;

namespace DialogueDisputeGameServer
{
    public class WCFManager:IServerConnectionManager,IGameObserver
    {
        static WCFManager myInstance = new WCFManager();
        public static WCFManager getInstance()
        {
            myInstance =myInstance ?? new WCFManager();
            return myInstance;
        }

        List<DataPlayer> players = new List<DataPlayer>();
        Dictionary<string, List<WCFMessage>> MyMessages = new Dictionary<string, List<WCFMessage>>();
        private List<Match> matches = new List<Match>();
        public List<Match> Matches
        {
            get { return matches; }
        }
        IFeedbackWriter myFeedbackWriter;
        IServerView serverView;
        List<DisputeGame> games = new List<DisputeGame>();

        public IServerView View
        {
            get { return serverView; }
            set { serverView = value; }
        }
        ServiceHost host;

        private WCFManager()
        {
            
        }
        public IFeedbackWriter FeedbackWriter
        {
            get { return myFeedbackWriter; }
            set { myFeedbackWriter = value; }
        }
        public void sendFeedback(string functionName, string feedback)
        {
            FeedbackWriter.WriteLine("Server   Method:" + functionName + "   Feedback:" + feedback);
        }

        void RunServer()
        {
            DisputeGameConnectionService instance = DisputeGameConnectionService.getInstance();
            instance.Manager = this;
            host = new ServiceHost(instance);

            host.Open();
            FeedbackWriter.WriteLine("Server started");
            FeedbackWriter.WriteLine("\n");
            FeedbackWriter.WriteLine(" Configuration Name: " + host.Description.ConfigurationName);
            FeedbackWriter.WriteLine(" End point address: " + host.Description.Endpoints[0].Address);
            FeedbackWriter.WriteLine(" End point binding: " + host.Description.Endpoints[0].Binding.Name);
            FeedbackWriter.WriteLine(" End point contract: " + host.Description.Endpoints[0].Contract.ConfigurationName);
            FeedbackWriter.WriteLine(" End point name: " + host.Description.Endpoints[0].Name);
            FeedbackWriter.WriteLine(" End point lisent uri: " + host.Description.Endpoints[0].ListenUri);
            FeedbackWriter.WriteLine(" \nName:" + host.Description.Name);
            FeedbackWriter.WriteLine(" Namespace: " + host.Description.Namespace);
            FeedbackWriter.WriteLine(" Service Type: " + host.Description.ServiceType);
        }
        void StopServer()
        {
            FeedbackWriter.WriteLine("Closing Service");
            host.Close();
        }
        void IServerViewController.MessageSentFromView(Messages.LobbyViewMessage message, List<object> data)
        {
            switch (message)
            {
                case DisputeCommon.Messages.LobbyViewMessage.StartServer:
                    RunServer();
                    break;
                case DisputeCommon.Messages.LobbyViewMessage.StopServer:
                    StopServer();
                    break;
                case DisputeCommon.Messages.LobbyViewMessage.viewClosed:
                    StopServer();
                    break;
                default:
                    break;
            }
        }

        #region IConnection
        public void broadcast(Messages.GameMessages message)
        {
            WCFMessage newMessage=null;
            //Interpet message
            switch (message)
            {
                case Messages.GameMessages.PlayerConnected:
                    newMessage = new WCFMessage() { Type = Messages.DTOType.lobby, Data = getLobbyDTO() };
                    break;
                case Messages.GameMessages.matchCreated:
                    newMessage = new WCFMessage() { Type = Messages.DTOType.lobby, Data = getLobbyDTO() };
                    break;
                default:
                    break;
            }
            //Send to all
            if (newMessage != null && players!=null)
            {
                foreach (var player in players)
                {
                    newMessage.Player = player;
                    MyMessages[player.PlayerName].Add(newMessage);
                }
            }
        }

        public DataPlayer addNewPlayer(DataPlayer player)
        {
            if (players.Find(n=>n.Equals(player))!=null)
            {
                sendFeedback("addNewPlayer", "Player Exists");
                return null;
            }
            else
            {
                //Add player to list, make an entry in messages for messages to this player
                players.Add(player);
                MyMessages.Add(player.PlayerName, new List<WCFMessage>());
                addSignalMessage(Messages.GameMessages.PlayerConnected, player);
                sendFeedback("addNewPlayer", "Player " + player.PlayerName + " connected");
            }
            return player;

        }

        /// <summary>
        /// This method looks at the type of request the player sent,
        /// and puts the appropriate data and type in to msg.data before adding it to MyMessages
        /// </summary>
        /// <param name="player"></param>
        /// <param name="msg"></param>
        public void addNewMessage(DataPlayer player, Messages.GameMessages msg,String data=null)
        {
            player = players.Find(n => n.Equals(player));
            if (player == null)
            {
                sendFeedback("addNewMessage", "Player Not Found\n PlayerName:" + player.PlayerName + "\n" +
                    msg.ToString());
                return;
            }
            if(player.InGame)//Use in game handler
            {
                DisputeGame game = games.Find(n => n.Player1.Equals(player) || n.Player2.Equals(player));
                DataPlayer player1 = players.Find(n => n.Equals(game.Player1)), 
                        player2 = players.Find(n => n.Equals(game.Player2));
                switch (msg)
                {
                    case Messages.GameMessages.UpdateMatch:
                        updatePlayerInMatch(player);
                        break;
                    case Messages.GameMessages.somChosen:
                        int i= data.IndexOf("$");
                        String name = data.Substring(0, i);
                        double value = Double.Parse(data.Substring(i + 1));
                        game.getMessageFromConnection(Messages.GameMessages.somChosen,
                                                        player, 
                                                        new List<object> { name, value });
                        game.Match.Transcript += "SoM chosen:" + name + "\n";
                        break;

                    case Messages.GameMessages.GameOver:
                        player1.InGame = false;
                        player2.InGame = false;
                        Matches.Remove(player1.ActiveMatch);
                        player1.ActiveMatch = new Match();
                        player1.ActiveMatch = null;
                        player2.ActiveMatch = null;
                        game = null;
                        break;
                        
                    default://Covers all argument requests
                        game.getMessageFromConnection(msg, player, null);
                        break;
                    
                }
                
            }else//Use lobby handler
            {
                switch (msg)
                {
                    case Messages.GameMessages.SendActiveMatches:
                        updatePlayer(player);
                        break;
                    case Messages.GameMessages.CreateMatch:
                        createMatch(player);
                        break;
                    case Messages.GameMessages.LeaveMatch:
                        leaveMatch(player);
                        break;
                    case Messages.GameMessages.UpdateAll:
                        updatePlayer(player);
                        break;
                    case Messages.GameMessages.JoinMatch:
                        if (data == null)
                            addSignalMessage(Messages.GameMessages.InvalidMatch, player);
                        else
                            joinMatch(data, player);
                        break;
                    case Messages.GameMessages.PlayerReady:
                        lockInPlayer(player);
                        break;
                    case Messages.GameMessages.PlayerNotReady:
                        lockOutPlayer(player);
                        break;
                    default:
                        break;
                }
            }
        }        
        
        /// <summary>
        /// Adds WCFMessage to player's queue containing only the network message in the dto
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="player"></param>
        private void addSignalMessage(Messages.GameMessages msg, DataPlayer player)
        {
            MyMessages[player.PlayerName].Add(new WCFMessage() { Player = player, Data = new DisputeDTO() { Message = msg }, Type = Messages.DTOType.message });
        }        

        public void removePlayer(DataPlayer player)
        {
            if (players.Find(n=>n.Equals(player))!=null)
            {
                players.Remove(player);
                MyMessages.Remove(player.PlayerName);
            }
        }

        public List<WCFMessage> getMessages(DataPlayer player)
        {
            if(keyExists(player.PlayerName))
            {
                List<WCFMessage> msgs = MyMessages[player.PlayerName];
                MyMessages[player.PlayerName] = new List<WCFMessage>();
                return msgs;
            }
            return null;
        }
        public bool keyExists(string key)
        {
            var k = (from e in MyMessages
                        where e.Key.Equals(key)
                        select e.Key);
            if (k==null || k.Count()<1)
                return false;
            return !String.IsNullOrEmpty(k.First());
        }

        public List<DataPlayer> getPlayers()
        {
            return players;
        }

        /// <summary>
        /// Sets a character to a player
        /// </summary>
        /// <param name="character"></param>
        /// <param name="player"></param>
        public void addCharacter(CharacterData character, DataPlayer player)
        {            
            if (player != null)
            {
                DataPlayer _player = players.Find(n => n.Equals(player));
                if (_player == null)
                    return;
                if (character != null)
                {
                    character = addExtraProperties(character);
                    _player.Character = character;
                    //Remove lock in status from player 
                    lockOutPlayer(_player);
                    //Update match to reflect new character
                    if(_player.ActiveMatch!=null)
                         _player.ActiveMatch.setUpMatchStartingValue();
                    
                }
                else
                    addSignalMessage(Messages.GameMessages.InvalidCharacter, _player);
            }
        }
        /// <summary>
        /// Adds a goal to this player's active match if the player is player 1 and has an active match
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="player"></param>
        public void addGoal(Goal goal, DataPlayer player)
        {
            if (player != null && goal != null)
            {
                player = players.Find(n => n.Equals(player));
                if (player.ActiveMatch != null)
                    player.ActiveMatch.Goal = goal;
                else
                    addSignalMessage(Messages.GameMessages.PlayerNotInMatch, player);
            }
        }
        #endregion

        #region Request Handlers
        /// <summary>
        /// Adds SOM attributes to character if it didnt' have them
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        CharacterData addExtraProperties(CharacterData character)
        {
            if (character != null && character.MyAttributes != null)
            {
                if (!character.MyAttributes.ContainsKey("StateOfMindJoySorrow"))
                    character.MyAttributes.Add("StateOfMindJoySorrow",2);
                if (!character.MyAttributes.ContainsKey("StateOfMindAngerFear"))
                    character.MyAttributes.Add("StateOfMindAngerFear",2);
                if(!character.MyStats.ContainsKey("subterfugeBonus"))
                    character.MyStats.Add("subterfugeBonus",0);
                if (!character.MyStats.ContainsKey("nonSubterfugeBonus"))
                    character.MyStats.Add("nonSubterfugeBonus", 0);
                if (!character.MyStats.ContainsKey("subterfugeTragedy"))
                    character.MyStats.Add("subterfugeTragedy", 0);
                if (!character.MyStats.ContainsKey("nonSubterfugeTragedy"))
                    character.MyStats.Add("nonSubterfugeTragedy", 0);
                if (!character.MyStats.ContainsKey("analyzeBonus"))
                    character.MyStats.Add("analyzeBonus", 0);
            }
            return character;
        }
        /// <summary>
        /// Adds a new message to the player containing the match he is playing if there is one
        /// </summary>
        /// <param name="player"></param>
        void updatePlayerInMatch(DataPlayer player)
        {
            if (player.ActiveMatch != null)
            {
                WCFMessage msg = new WCFMessage() { Type = Messages.DTOType.match, Data = getMatchDTO(player.ActiveMatch) };
                MyMessages[player.PlayerName].Add(msg);
            }
        }
        /// <summary>
        /// Addds a new message to the player containint all loby data (matches,etc)
        /// </summary>
        /// <param name="player"></param>
        void updatePlayer(DataPlayer player)
        {
            WCFMessage dataMessage = new WCFMessage() { Type = Messages.DTOType.lobby, Data = getLobbyDTO() };
            MyMessages[player.PlayerName].Add(dataMessage);
        }
        private void createMatch(DataPlayer player)
        {
            if (player != null)
            {
                //Check to see if player is not in game
                if (player.ActiveMatch != null)
                {
                    addSignalMessage(Messages.GameMessages.PlayerInMatch, player);
                    return;
                }
                Match m = new Match() { Player1 = player, Goal = new Goal() { PropertyName = "Resistance", Value = 0, Type = GoalTypes.ReachValue } };
                Matches.Add(m);
                player.ActiveMatch = m;
                addSignalMessage(Messages.GameMessages.matchCreated, player);
                broadcast(Messages.GameMessages.matchCreated);
            }
            else
                addSignalMessage(Messages.GameMessages.PlayerNotFound, player);
        }
        public DisputeDTO getLobbyDTO()
        {
            DisputeDTO DTO = new DisputeDTO() { Matches = this.Matches,Players=this.players};
            return DTO;
        }
        public DisputeDTO getMatchDTO(Match m)
        {
            DisputeDTO DTO = new DisputeDTO() {Match=m,Message=Messages.GameMessages.UpdateMatch};
            return DTO;
        }
        private bool leaveMatch(DataPlayer player)
        {
            Match m = Matches.Find(n => n.Player1.Equals(player) ||
              (n.Player2 != null && n.Player2.Equals(player)));

            if (m == null)
            {
                sendFeedback("tryLeaveGame", player.PlayerName + " is not in a game");
                addSignalMessage(Messages.GameMessages.PlayerNotInMatch, player);
                return false;
            }

            if (m.Player1.Equals(player))//player is creator so end game
            {
                Matches.Remove(m);
                if(m.Player2!=null)//If match has a player 2, deactivate him too
                    players.Find(n=>n.Equals(m.Player2)).ActiveMatch=null;
                player.ActiveMatch = null;
                m = null;
            }
            else//Player is player2, just remove from game
            {
                player.ActiveMatch.Player2Ready = false;//Unlock
                player.ActiveMatch = null;
                m.Player2 = null;
            }

            addSignalMessage(Messages.GameMessages.playerLeftMatch, player);
            //sendData(Messages.NetworkMessage.gameLeft);
            return true;
        }
        private void joinMatch(string matchPlayer1Name, DataPlayer player)
        {
            //Check matchName exists
            if (matchPlayer1Name == null && matchPlayer1Name.Length <= 0)
            {
                sendFeedback("tryJoinGame", "Error getting game name");
                addSignalMessage(Messages.GameMessages.emptyDataReceived,player);
                return;
            }

            //Check that this player is not in a game already
            if (Matches.Find(n => n.Player1.Equals(player)) != null)
            {
                sendFeedback("tryJoinGame", player.PlayerName + " is already in a game");
                addSignalMessage(Messages.GameMessages.PlayerInMatch,player);
                return;
            }


            Match m = Matches.Find(n=>n.MatchName.Equals(matchPlayer1Name));
            if (m == null || m.Player1==null)
            {
                sendFeedback("tryJoinGame", "Game:" + matchPlayer1Name + " doesn't exist");
                addSignalMessage(Messages.GameMessages.InvalidMatch,player);
                return;
            }
            //Check that matchName has empty slot at player 2
            if (m.Player2!=null)
            {
                sendFeedback("tryJoinGame", "Game:" + matchPlayer1Name + " already has two players");
                addSignalMessage(Messages.GameMessages.MatchIsFull,player);
                return;
            }

            //All checks passed, add player to match at slot 2
            m.Player2 = player;
            m.Player2.ActiveMatch = m;
            addSignalMessage(Messages.GameMessages.PlayerJoinedGame, player);
        }
        /// <summary>
        /// Sets player as ready in his match. If both player are ready, starts the game
        /// </summary>
        /// <param name="player"></param>
        private void lockInPlayer(DataPlayer player)
        {
            //See if player is in match as creator
            Match m = Matches.Find(n => n.Player1.Equals(player));
            if (m != null)
            {
                m.Player1Ready = true;
                addSignalMessage(Messages.GameMessages.PlayerReady, player);
            }
            else//See if player is in game as Player 2
            {
                m = Matches.Find(n => n.Player2!=null && n.Player2.Equals(player));
                if (m != null)//player is Player2
                {
                    m.Player2Ready = true;
                    addSignalMessage(Messages.GameMessages.PlayerReady, player);
                }
                else//player is not in a game
                    addSignalMessage(Messages.GameMessages.PlayerNotInMatch, player);
            }

            //See if match has a goal
            if (m.Goal == null)
            {
                addSignalMessage(Messages.GameMessages.GameDoesntHaveGoal, player);
            }

            //See if both players are ready, if so send messages and start game
            if (m.Player1Ready && m.Player2Ready)
            {                
                startMatch(m);
            }
                
        }
        private void lockOutPlayer(DataPlayer player)
        {
            Match m = Matches.Find(n => n.Player1.Equals(player));
            if (m != null)
            {
                m.Player1Ready = false;
                return;
            }
            m = Matches.Find(n => n.Player2 != null && n.Player2.Equals(player));
            if (m != null)
            {
                m.Player2Ready = false;
                return;
            }

        }
        private void updateMatchData(Match m)
        {
            m.setUpMatchStartingValue();
        }

        /// <summary>
        /// Creates a game for Match m, and adds it to the list of games. Tells players of the match that they are in game,
        /// and send signal to clients
        /// </summary>
        /// <param name="m"></param>
        private void startMatch(Match m)
        {
            DisputeGame game = new DisputeGame() { Match= m};
            game.FeedbackWritter = this.FeedbackWriter;
            //Register as observer so game can send messages back
            (game as IGameObservable).registerObserver(this);

            games.Add(game);
            players.Find(n => n.Equals(m.Player1)).InGame = true;
            players.Find(n => n.Equals(m.Player2)).InGame = true;

            MyMessages[m.Player1.PlayerName].Add(new WCFMessage()
            {
                Type = Messages.DTOType.match,
                Data = new DisputeDTO() { Message = Messages.GameMessages.StartingMatch,Match=m }
            });
            MyMessages[m.Player2.PlayerName].Add(new WCFMessage()
            {
                Type = Messages.DTOType.match,
                Data = new DisputeDTO() { Message = Messages.GameMessages.StartingMatch,Match=m }
            });
            m.Transcript="Starting Match\n";
            updateMatchData(m);

            //Now update players in match, we may not need this because the starting match messages above
            //updatePlayerInMatch(m.Player1);
            //updatePlayerInMatch(m.Player2);
        }
        private void updateMatchPlayers(Match m)
        {
            DataPlayer p1 = m.Player1, p2 = m.Player2;
            MyMessages[p1.PlayerName].Add(new WCFMessage()
            {
                Type = Messages.DTOType.match,
                Data = new DisputeDTO() { Message = Messages.GameMessages.UpdateMatch,Match=m }
            });
            MyMessages[p2.PlayerName].Add(new WCFMessage()
            {
                Type = Messages.DTOType.match,
                Data = new DisputeDTO() { Message = Messages.GameMessages.UpdateMatch, Match = m }
            });
        }
        #endregion


        /// <summary>
        /// This method is used to get messages from the Model layer (GameDispute), and pass them on to the players.
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="player"></param>
        /// <param name="data"></param>
        void IGameObserver.update(DataPlayer player, Messages.GameMessages msg, List<object> data)
        {
            DisputeDTO dto=null;
            switch (msg)
            {
                case Messages.GameMessages.GetSOM:
                    dto = new DisputeDTO() { SomRequest = (SoMRequest)data[0], Message = Messages.GameMessages.GetSOM };
                    goto case Messages.GameMessages.none;

                case Messages.GameMessages.ArgumentDone:
                    break;

                case Messages.GameMessages.GameOver:
                    dto = new DisputeDTO() { Message = Messages.GameMessages.GameOver };
                    goto case Messages.GameMessages.none;

                case Messages.GameMessages.NotPlayerTurn:
                    dto = new DisputeDTO() { Message = Messages.GameMessages.NotPlayerTurn };
                    goto case Messages.GameMessages.none;

                case Messages.GameMessages.none:
                    this.MyMessages[player.PlayerName].Add(new WCFMessage()
                    {
                        Player = player,
                        Type = Messages.DTOType.match,
                        Data = dto
                    });
                    break;
                default:
                    break;
            }
        }
    }
}

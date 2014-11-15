using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeCommon;
using DialogueCommon.Interfaces;
using DisputeCommon.Interfaces;

namespace DialogueDisputeGameClient.Offline
{
    public class OfflineClientManager:IClientConnectionManager,IConnectionObservable
    {
        DataPlayer player;
        IFeedbackWriter feedbackWriter;
        public IFeedbackWriter FeedbackWriter
        {
            get { return feedbackWriter; }
            set { feedbackWriter = value; }
        }
        CharacterData myCharacter;
        public CharacterData MyCharacter
        {
            get { return myCharacter; }
            set { myCharacter = value; }
        }
        List<Match> matches;
        public List<Match> Matches
        {
            get { return matches; }
            set { matches = value; }
        }
        List<DataPlayer> players;
        public List<DataPlayer> Players
        {
            get { return players; }
            set { players = value; }
        }
        DisputeDTO myDTO;
        Match inMatch;
        Boolean connected = false;
        List<IConnectionObserver> myObservers=new List<IConnectionObserver>();
        /// <summary>
        /// This member is used to tell observers what messages the server sent. It's default should be UpdateMatch,
        /// and it should get reset to this after every use.
        /// </summary>
        Messages.GameMessages serverMessage = Messages.GameMessages.UpdateMatch;
        Messages.LobbyViewMessage viewMessage = Messages.LobbyViewMessage.UpdateAll;
        List<WCFMessage> serverMessages = new List<WCFMessage>();
        public DisputeDTO MyDTO
        {
            get { return myDTO; }
            set { myDTO = value; }
        }
        

        IServerConnectionManager serverManager;
        public IServerConnectionManager remoteProxy
        {
            get { return serverManager; }
            set { serverManager = value; }
        }


        /// <summary>
        /// Gets request from view controller and creates appropriate message for it
        /// </summary>
        /// <param name="header"></param>
        /// <param name="args"></param>
        /// <param name="sender"></param>
        public void parseRequest(Messages.GameMessages header, List<object> args, object sender)
        {
            if (!isConnected() && header != Messages.GameMessages.connect)
                return;
            List<NamedParameter> pars = null;
            switch (header)
            {
                #region LobbyRequests
                case Messages.GameMessages.connect:
                    pars = DataTypes.createList(args, DataType.name);
                    this.connect(null, null, pars[0].data.ToString());
                    //this.connect((String)pars[0].data, (String)pars[1].data, (String)pars[2].data);
                    break;
                case Messages.GameMessages.CreateMatch:
                    sendFeedback("parseRequest", "Begin Create Match");
                    createMatch();
                    break;
                case Messages.GameMessages.JoinMatch:
                    sendFeedback("parseRequest", "Begin Join Match");
                    pars = DataTypes.createList(args, DataType.name);
                    if (pars == null || pars.Count < 1)
                    {
                        sendFeedback("parseRequest", "Error in match name");
                        return;
                    }
                    String name = ((Match)pars[0].data).Player1.PlayerName;
                    joinGame(name);
                    break;
                case Messages.GameMessages.LeaveMatch:
                    sendFeedback("parseRequest", "Begin Leave Game");
                    leaveGame();
                    break;
                case Messages.GameMessages.UpdateMatches:
                    updateMatches();
                    break;
                case Messages.GameMessages.playerQuit:
                    this.connected = false;
                    sendFeedback("sendMessage", "Player quit");
                    doQuit();
                    break;
                case Messages.GameMessages.GameOver:
                    sendFeedback("sendMessage", "Game over");
                    remoteProxy.addNewMessage(player, Messages.GameMessages.GameOver);
                    break;
                case Messages.GameMessages.UpdateAll://Do nothing, notify will update anyway, but we don't
                    // want it to look like we forgot this case
                    break;
                case Messages.GameMessages.SendCharacterToServer:
                    pars = DataTypes.createList(args, DataType.character);
                    remoteProxy.addCharacter((CharacterData)pars[0].data, player);
                    break;
                case Messages.GameMessages.PlayerReady:
                    remoteProxy.addNewMessage(player, Messages.GameMessages.PlayerReady);
                    break;
                case Messages.GameMessages.SendGoalToServer:
                    pars = DataTypes.createList(args, DataType.Goal);
                    remoteProxy.addGoal((Goal)pars[0].data, player);
                    break;
                #endregion
                #region DATA REQUESTS
                case Messages.GameMessages.SendActiveMatches:
                    updateMatches();
                    break;
                case Messages.GameMessages.sendPlayers:
                    getPlayers();
                    break;
                #endregion
                #region ACTIONREQUESTS
                case Messages.GameMessages.doAction:
                    remoteProxy.addNewMessage(player, (Messages.GameMessages)args[0]);
                    break;
                case Messages.GameMessages.UpdateMatch:
                    remoteProxy.addNewMessage(player, Messages.GameMessages.UpdateMatch);
                    break;
                case Messages.GameMessages.somChosen:
                    remoteProxy.addNewMessage(player, Messages.GameMessages.somChosen,args[0].ToString());
                    break;
                #endregion
                default:
                    break;
            }
            if (connected)
            {
                (this as IConnectionObservable).notifyObservers();
            }
        }
        /// <summary>
        /// Decision Maker when game is playing.Gets messages from server and decides what to do with them
        /// </summary>
        /// <param name="disputeDTO"></param>
        private void parseMatchDTO(DisputeDTO disputeDTO)
        {
            Messages.GameMessages dtoMessage = disputeDTO.Message;
            serverMessage = dtoMessage;
            switch (dtoMessage)
            {
                case Messages.GameMessages.StartingMatch:
                    this.inMatch = disputeDTO.Match;
                    viewMessage = Messages.LobbyViewMessage.StartGame;
                    break;
                case Messages.GameMessages.ArgumentDone:
                    this.inMatch = disputeDTO.Match;
                    break;
                case Messages.GameMessages.GetSOM:
                    serverMessage = dtoMessage;//this was already done above but just emphasize
                    myDTO = disputeDTO;
                    break;
                case Messages.GameMessages.NotPlayerTurn:
                    sendFeedback("parseMatchDTO", dtoMessage);
                    break;
                case Messages.GameMessages.UpdateMatch:
                    this.inMatch = disputeDTO.Match;
                    break;
                case Messages.GameMessages.stillAlive:
                    this.inMatch = disputeDTO.Match;
                    break;
                case Messages.GameMessages.GameOver:
                    
                    serverMessage = dtoMessage;//this was already done above but just emphasize
                    break;
            }
        }
        /// <summary>
        /// Gets messages from server when in lobby. It basically just sets Matches data.
        /// </summary>
        /// <param name="dto"></param>
        void parseLobbyDTO(DisputeDTO dto)
        {
            this.matches = dto.Matches;
            this.Players = dto.Players;
            this.player = this.Players.Find(n => n.PlayerName.Equals(player.PlayerName));
        }

        /// <summary>
        /// Reads messages and if they contain data it gets set to appropriate members
        /// </summary>
        private void parseNewMessage(WCFMessage msg)
        {
            if (connected)
            {
                if (msg == null)
                    return;

                myDTO = msg.Data;
                switch (msg.Type)
                {
                    case Messages.DTOType.lobby:
                        parseLobbyDTO(msg.Data);
                        break;
                    case Messages.DTOType.message:
                        sendFeedback("parseNewMessage", msg.Data.Message);
                        break;
                    case Messages.DTOType.match:
                        parseMatchDTO(msg.Data);
                        break;
                    default:
                        break;
                }
            }
        }
        private void getNewMessages()
        {
            if (connected)
                serverMessages = remoteProxy.getMessages(player);
        }
        #region Request Handlers
        private void manualUpdate()
        {
        }
        private void getPlayers()
        {
            remoteProxy.addNewMessage(player, Messages.GameMessages.sendPlayers);
        }


        private void doQuit()
        {
            remoteProxy.removePlayer(this.player);
        }

        private void updateMatches()
        {
            remoteProxy.addNewMessage(player, Messages.GameMessages.SendActiveMatches);
        }

        private void leaveGame()
        {
            remoteProxy.addNewMessage(player, Messages.GameMessages.LeaveMatch);
        }

        private void joinGame(string name)
        {
            remoteProxy.addNewMessage(player, Messages.GameMessages.JoinMatch, name);
        }

        private void createMatch()
        {
            remoteProxy.addNewMessage(player, Messages.GameMessages.CreateMatch);
        }
        /// <summary>
        /// Order of parameter is address,port,playerName
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="playerName"></param>
        private void connect(string address, string port, string playerName)
        {
            DataPlayer player = remoteProxy.addNewPlayer(new DataPlayer() { PlayerName=playerName});
            if (player != null)
            {
                this.player = player;
                sendFeedback("Connect", "Player:" + player.PlayerName + " has connected");
                connected = true;
                players = remoteProxy.getPlayers();
            }
            else
                sendFeedback("Connect", "Player:" + playerName + " not connected. Name in use.");
        }


        #endregion


        public bool isConnected()
        {
            return connected;
        }

        public void sendFeedback(string functionName, string feedback)
        {
            FeedbackWriter.WriteLine("Method:" + functionName + "   Feedback:" + feedback);
        }
        public void sendFeedback(string functionName, Messages.GameMessages feedback)
        {
            FeedbackWriter.WriteLine("Method:" + functionName + "   Feedback:" + feedback.ToString());
        }

        /*Observable Stuff*/
        void IConnectionObservable.registerObserver(IConnectionObserver newObserver)
        {
            myObservers.Add(newObserver);
        }

        void IConnectionObservable.removeObserver(IConnectionObserver newObserver)
        {
            myObservers.Remove(newObserver);
        }

        /// <summary>
        /// Updates data from server and notifies observers of new data
        /// </summary>
        /// <param name="code"></param>
        void IConnectionObservable.notifyObservers(object code = null)
        {
            getNewMessages();
            foreach (WCFMessage msg in serverMessages)
            {
                parseNewMessage(msg);
                foreach (IConnectionObserver obs in myObservers)
                {
                    obs.update(createDataPackage());
                    serverMessage = Messages.GameMessages.UpdateMatch;
                    viewMessage = Messages.LobbyViewMessage.UpdateAll;
                }
            }
            serverMessages.Clear();
        }

        /// <summary>
        /// Creates an object containing all the data sent from server
        /// </summary>
        /// <returns>Data from server as a List of NamedParameters</returns>
        object createDataPackage()
        {
            List<NamedParameter> pars = new List<NamedParameter>();
            pars.Add(new NamedParameter() { type = DataType.viewMessage, data = viewMessage });
            pars.Add(new NamedParameter() { type = DataType.matches, data = this.Matches });
            pars.Add(new NamedParameter() { type = DataType.players, data = this.players });
            pars.Add(new NamedParameter() { type = DataType.playerName, data = this.player.PlayerName });
            pars.Add(new NamedParameter() { type = DataType.character, data = this.MyCharacter });
            pars.Add(new NamedParameter() { type = DataType.match, data = this.inMatch });
            pars.Add(new NamedParameter() { type = DataType.serverMessage, data = serverMessage });
            pars.Add(new NamedParameter() { type = DataType.SoMRequest, data = myDTO.SomRequest });
            return pars;
        }

    }
}

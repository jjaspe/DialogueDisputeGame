using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using DisputeCommon;
using System.IO;

namespace DialogueDisputeGameServer
{
    public class playerClient
    {
        TcpClient clientSocket;
        IFeedbackWriter feedbackWriter;

        public IFeedbackWriter FeedbackWriter
        {
            get { return feedbackWriter; }
            set { feedbackWriter = value; }
        }
        public TcpClient ClientSocket
        {
            get { return clientSocket; }
        }
        ServerConnectionManager serverManager;
        public String playerName;
        bool playerConnected = false;

        public playerClient(ServerConnectionManager manager, TcpClient socket, String name)
        {
            clientSocket = socket;
            playerName = name;
            serverManager = manager;
            
        }

        public void start()
        {
            Thread ctThread = new Thread(listen);
            ctThread.Name = serverManager.PlayerSocketList.Count.ToString();
            playerConnected = true;
            ctThread.Start();
        }

        
        
        
        public void getMessageFromServerManager(Messages.GameMessages message, List<NamedParameter> data)
        {
            switch (message)
            {
                case Messages.GameMessages.gamesUpdated:
                    //Tell client that game was created so they can react
                    sendData(message.ToString());
                    break;
                default:
                    break;
            }
        }

        #region NETWORK ACTIONS
        public String getData()
        {
            try
            {
                //Get Data
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] inStream = new byte[10025];
                serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                string data = System.Text.Encoding.ASCII.GetString(inStream);
                data = data.Substring(0, data.IndexOf("$"));
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in PlayerCLient.getData\n" + e.Message);
            }
            return null;
        }
        public void sendData(Messages.GameMessages msg)
        {
            sendData(msg.ToString());
        }
        public void sendData(String data)
        {
            //Send Data
            try
            {
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
            catch (Exception e)
            {
                sendFeedback("sendData", "Exception:" + e.InnerException);
            }
        }
        public void listen()
        {
            byte[] bytesFrom = new byte[10025];

            string dataFromClient = null;

            Byte[] sendBytes = null;

            while (!ServerConnectionManager.formClosed && playerConnected)
            {
                if (!clientSocket.Connected)
                {
                    sendFeedback("listen","disconnected");
                    break;
                }
                dataFromClient = getData();
                if (dataFromClient != null && dataFromClient.Length > 0)
                {
                    sendFeedback("listen",dataFromClient);
                    //sendData(Messages.NetworkMessage.dataReceived.ToString());
                    
                    parseMessage(dataFromClient);
                }
            }
        }
        void parseMessage(String header)
        {
            if (!Enum.IsDefined(typeof(Messages.GameMessages), header))
            {
                Console.WriteLine("Bad Header:" + header);
                return;
            }
            sendFeedback("parseMessage", header);
            Messages.GameMessages data = (Messages.GameMessages)Enum.Parse(typeof(Messages.GameMessages), header);
            switch (data)
            {
                case Messages.GameMessages.dataReceived:
                    break;
                case Messages.GameMessages.CreateMatch:
                    tryCreateMatch();
                    break;
                case Messages.GameMessages.JoinMatch:
                    String gameName = getData();
                    tryJoinGame(gameName);
                    break;
                case Messages.GameMessages.LeaveMatch:
                    tryLeaveGame();
                    break;
                case Messages.GameMessages.SendActiveMatches:
                    SendActiveMatches();
                    break;
                    
                case Messages.GameMessages.playerQuit:
                    tryLeaveGame();//Quit player from game if it's in one
                    ClientSocket.Close();
                    serverManager.disconnect(this.playerName);
                    playerConnected = false;
                    break;
                default:
                    break;
            }
        }

        private bool tryLeaveGame()
        {
            Match m = serverManager.Matches.Find(n => n.Player1.Equals(playerName) || 
               (n.Player2!=null && n.Player2.Equals(playerName)) );
            if (m == null)
            {
                sendFeedback("tryLeaveGame", playerName + " is not in a game");
                sendData(Messages.GameMessages.PlayerNotInMatch);
                return false;
            }

            if (m.Player1.Equals(playerName))//player is creator so end game
            {
                serverManager.Matches.Remove(m);
                m = null;
            }
            else//Player is player2, just remove from game
                m.Player2 = null;
            sendFeedback("tryLeaveGame", playerName + " has left the game");
            //sendData(Messages.NetworkMessage.gameLeft);
            serverManager.getMessageFromPlayerClient(Messages.GameMessages.gamesUpdated, null, this);
            return true;
        }

        

        bool tryCreateMatch()
        {
            //Dont create if player is in another match
            if (serverManager.Matches.Find(n => n.Player1.Equals(playerName) || (n.Player2 != null && n.Player2.Equals(playerName))) == null)
            {
                createGame();
                return true;
            }
            else
            {
                sendData(Messages.GameMessages.PlayerInMatch.ToString());
            }
            return false;
        }
        void createGame()
        {
            sendFeedback("createGame", "start create game");
           // serverManager.Matches.Add(new Match() { Player1 = playerName});
            //Tell this player this is his active game
            sendData(Messages.GameMessages.matchCreated.ToString());
            getData();
            //Add game to list,tell everyone about it
            this.serverManager.getMessageFromPlayerClient(Messages.GameMessages.gamesUpdated,null, this);
            sendFeedback("createGame", "end create game");
        }
        /// <summary>
        /// Checks to see if this player client can join match gameName;
        /// </summary>
        /// <returns>true if joined,false otherwise</returns>
        private bool tryJoinGame(String gameName)
        {            
            //Check gameName exists
            if (gameName == null && gameName.Length <= 0)
            {
                sendFeedback("tryJoinGame", "Error getting game name");
                sendData(Messages.GameMessages.emptyDataReceived);
                return false;
            }

            //Check that this player is not in a game already
            if (serverManager.Matches.Find(n => n.Player1.Equals(playerName)) != null)
            {
                sendFeedback("tryJoinGame", playerName + " is already in a game");
                sendData(Messages.GameMessages.PlayerInMatch);
                return false;
            }

            
            Match m = serverManager.getMatch(gameName);
            if(m == null || m.Player1 == null )
            {
                sendFeedback("tryJoinGame", "Game:" + gameName + " doesn't exist");
                sendData(Messages.GameMessages.InvalidMatch);
                return false;
            }
            //Check that gameName has empty slot at player 2
            if (m.Player2!=null)
            {
                sendFeedback("tryJoinGame", "Game:" + gameName + " already has two players");
                sendData(Messages.GameMessages.MatchIsFull);
                return false;
            }
            
            joinGame(m);
            return true;
        }
        /// <summary>
        /// Tells server to put this player client in Match match
        /// </summary>
        /// <param name="match">Match to put this player client in</param>
        void joinGame(Match match)
        {            
            List<object> data = new List<object> { match.Player1};
            serverManager.getMessageFromPlayerClient(Messages.GameMessages.JoinMatch, data,this);
        }
        void SendActiveMatches()
        {
            List<Match> matches = serverManager.Matches;
            
            sendFeedback("SendActiveMatches", "begin");
            //Tell client we will send active games
            sendData(Messages.GameMessages.SendActiveMatches.ToString());
            sendFeedback("SendActiveMatches", "sent SendActiveMatches signal");
            sendFeedback("SendActiveMatches", " Matches:" + matches.Count);
            String received=getData();
            
            if (matches != null)
            {
                foreach (Match match in matches)
                {
                    sendData(match.ToString());
                    received=getData();

                    sendFeedback("SendActiveMatches", " Sent Match");
                }
            }
            //Send stop signal
            sendData(Messages.GameMessages.stop);
        }
        #endregion

        public void sendFeedback(string functionName, string feedback)
        {
            FeedbackWriter.WriteLine("PlayerClient:" + playerName + "   Method:" + functionName + "   Feedback:" + feedback);
        }
    }
}

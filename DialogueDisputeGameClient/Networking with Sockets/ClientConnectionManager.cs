using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Net.Sockets;
using DisputeCommon;
using DialogueCommon.Interfaces;
using System.Threading;
using System.IO;
using System.Collections;

namespace DialogueDisputeGameMultiplayer.Network
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientConnectionManager:IClientConnectionManager,IDisputeObservable
    {
        //Singleton
        static ClientConnectionManager instance = new ClientConnectionManager();
        public static ClientConnectionManager getInstance()
        {
            return instance ?? new ClientConnectionManager();
        }

        List<IControllerObserver> myObservers;
        TcpClient clientSocket = new TcpClient();
        Thread thisThread = System.Threading.Thread.CurrentThread;
        IFeedbackWriter feedbackWriter;

        public IFeedbackWriter FeedbackWriter
        {
            get { return feedbackWriter; }
            set { feedbackWriter = value; }
        }

        //                      This are the data members that will be accessed by MainMenuController
        String playerName = "";
        public String PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }
        CharacterData myCharacter, myWorld;
        public CharacterData MyWorld
        {
            get { return myWorld; }
            set { myWorld = value; }
        }
        public CharacterData MyCharacter
        {
            get { return myCharacter; }
            set { myCharacter = value; }
        }
        List<DataPlayer> players = new List<DataPlayer>();

        public List<DataPlayer> Players
        {
            get { return players; }
            set { players = value; }
        }
        List<Match> matches = new List<Match>();
        public List<Match> Matches
        {
            get { return matches; }
            set { matches = value; }
        }
        bool connected = false, listening = true,connecting=false;
        Match activeMatch;
        //                              End accessed members///////////////////////////////////////////////


        bool isRunning = false;

        public ClientConnectionManager()
        {
            myObservers = new List<IControllerObserver>();
        }

        #region LOCAL_COMM_STUFF
        public void sendFeedback(string functionName, string feedback)
        {
            FeedbackWriter.WriteLine("Method:" + functionName + "   Feedback:" + feedback);
        }
        public void updateData(ref CharacterData character,ref CharacterData world,ref List<Match> games,ref Match activeMatch,ref String playerName)
        {
            character = this.MyCharacter;
            world = this.MyWorld;
            games = this.Matches;
            activeMatch = this.activeMatch;
            playerName = this.playerName;
        }
        public bool isConnected()
        {
            return connected;
        }
        #endregion

        #region NETWORK_STUFF     
        public IFeedbackWriter getFeedbackWriter()
        {
            return FeedbackWriter;
        }
        public void setFeedbackWriter(IFeedbackWriter writer)
        {
            FeedbackWriter=writer;
        }
        public void connect(String address, String port,String playerName)
        {
            if (!(port.Length == 0) && !(address.Length == 0))
            {
                try
                {
                    if (clientSocket.Client.Connected)
                    {
                        sendFeedback("connect","player already connected");
                        return;
                    }

                    clientSocket.Connect(address, Int32.Parse(port));
                    this.PlayerName = playerName;
                    connecting = true;
                }
                catch (Exception ex)
                {
                    sendFeedback("connect","Client Exception:Couldn't Connect:" + ex.Message);
                    clientSocket = new TcpClient();
                    return;
                }
                sendData(playerName + "$");
                String data=getData();
                if (data != null && data.Length > 0)
                {
                    parseMessage(data);
                    if (connected)
                    {
                        isRunning = true;
                        Thread clientThread = new Thread(getMessage);
                        clientThread.IsBackground = true;
                        clientThread.Start();
                    }else
                    {
                        clientSocket.Client.Disconnect(true);
                        clientSocket = new TcpClient();
                    }
                }
                else
                {
                    clientSocket.Client.Disconnect(true);
                    clientSocket = new TcpClient();
                }
                
                connecting = false;
            }
        }
        private void getMessage()
        {
            byte[] bytesFrom = new byte[10025];

            string dataFromClient = null;

            Byte[] sendBytes = null;

            while (isRunning)
            {                
                if(listening)
                    dataFromClient = getData();
                if (dataFromClient != null && dataFromClient.Length > 0)
                {
                    sendFeedback("getMessage", dataFromClient);
                    //sendData(Messages.NetworkMessage.dataReceived.ToString());
                    //sendFeedback("getMessage","Sent ack");
                    listening=false;
                    parseMessage(dataFromClient);
                }
                listening = true;
            }
        }
        public void sendHeader(String header)
        {
            if (!isConnected())
                return;
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(header + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }
        void sendData(Messages.GameMessages data)
        {
            sendData(data.ToString());
        }
        public void sendData(String data)
        {
            if (!isConnected() && !connecting)
                return;
            //Send Data
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }
        public String getData()
        {
            if (!isConnected() && !connecting)
                return "";

            try
            {
                //Get Data
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] inStream = new byte[10025];
                int bytesRead=serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                string data = System.Text.Encoding.ASCII.GetString(inStream,0,bytesRead);
                //sendFeedback("getData", data);
                data = data.Substring(0, data.IndexOf("$") < 0 ? 0 : data.IndexOf("$"));
                return data;
            }
            catch (IOException e)
            {
                MessageBox.Show("Client closed");
            }
            return null;

        }
        public void parseMessage(String header)
        {
            if (!Enum.IsDefined(typeof(Messages.GameMessages), header))
            {
                sendFeedback("parseMessage:","Bad Header:" + header);
                return;
            }
            sendFeedback("parseMessage",header);
            Messages.GameMessages data = (Messages.GameMessages)Enum.Parse(typeof(Messages.GameMessages),header);
            switch (data)
            {
                case Messages.GameMessages.connected:
                    connected = true;
                    sendFeedback("parseMessage", "Connected");
                    break;
                case Messages.GameMessages.playerExists:
                    //This send data needs to be here because the message came from the connect from server
                    //not the listen from playerClient, the connect is expecting an ack
                    sendData(Messages.GameMessages.dataReceived.ToString());
                    MessageBox.Show("Player Exists");
                    break;
                case Messages.GameMessages.SendActiveMatches://Get active games
                    getGames();
                    getActiveMatch();
                    break;
                case Messages.GameMessages.matchCreated://Game was successfully created, so it is my Active game now
                    sendData(Messages.GameMessages.dataReceived);
                    sendFeedback("parseMessage", "Game Created");
                    break;
                case Messages.GameMessages.gamesUpdated:
                    sendData(Messages.GameMessages.SendActiveMatches.ToString());
                    break;
                case Messages.GameMessages.playerLeftMatch:
                    sendFeedback("parseMessage", this.PlayerName + " has left the game");
                    sendData(Messages.GameMessages.SendActiveMatches.ToString());
                    break;
                case Messages.GameMessages.PlayerNotInMatch:
                    sendFeedback("parseMessage", this.PlayerName + " is not in a game");
                    break;
                case Messages.GameMessages.PlayerInMatch:
                    sendFeedback("parseMessage",this.PlayerName + " is already in a game");
                    break;
                case Messages.GameMessages.MatchIsFull:
                    sendFeedback("parseMessage", "Game already has two players");
                    break;
                case Messages.GameMessages.PlayerJoinedGame:
                    sendFeedback("parseMessage", playerName + " joined game");
                    sendData(Messages.GameMessages.SendActiveMatches.ToString());
                    break;
                case Messages.GameMessages.emptyDataReceived:
                    sendFeedback("parseMessage", " Bad data sent");
                    break;
                default:
                    break;

            }//End case
            notifyObservers();
        }
        public void parseRequest(Messages.GameMessages header, List<object> args, object sender)
        {
            if (!isConnected() && header != Messages.GameMessages.connect)
                return;
            List<NamedParameter> pars = null;
            switch (header)
            {
                case Messages.GameMessages.connect:
                    pars = DataTypes.createList(args, DataType.name);
                    this.connect((String)pars[0].data, (String)pars[1].data, (String)pars[2].data);
                    break;
                case Messages.GameMessages.CreateMatch:
                    sendFeedback("sendMessage", "Begin Create Game");
                    this.sendData(header.ToString());
                    break;
                case Messages.GameMessages.JoinMatch:
                    sendFeedback("sendMessage", "Begin Join Game");
                    pars = DataTypes.createList(args, DataType.name);
                    String name = (String)pars[0].data;
                    sendData(Messages.GameMessages.JoinMatch.ToString());
                    sendData(name);
                    break;
                case Messages.GameMessages.LeaveMatch:
                    sendFeedback("sendMessage", "Begin Join Game");
                    sendData(Messages.GameMessages.LeaveMatch.ToString());
                    break;
                case Messages.GameMessages.UpdateMatches:
                    sendData(Messages.GameMessages.SendActiveMatches);
                    break;
                case Messages.GameMessages.playerQuit:
                    this.isRunning = false;
                    sendFeedback("sendMessage", "Player quit");
                    this.sendData(header.ToString());
                    break;
                case Messages.GameMessages.SendActiveMatches:
                    this.sendData(header.ToString());
                    break;
                default:
                    break;
            }
        }
        
        private void getActiveMatch()
        {
            activeMatch = Matches.Find(n => n.Player1.Equals(playerName));
        }        
        public void getGames()
        {
            //Send ack for sendActiveGame message
            sendData(Messages.GameMessages.dataReceived);
            sendFeedback("getGames", "Begin Get Games, Count:" + matches.Count);
            matches.Clear();
            String data = getData();
            while (data != null && data.Length > 0 && !data.ToString().Equals(Messages.GameMessages.stop.ToString()) )
            {
                matches.Add(Match.parseGame(data));
                //Send ACK
                sendData(Messages.GameMessages.dataReceived);
                data = getData();
            }
            //Send ACK
            //sendData(Messages.NetworkMessage.dataReceived.ToString());
            sendFeedback("getGames","Got Games, Count:" + matches.Count);

        }
        
        #endregion

        /*Observable Stuff*/
        public void registerObserver(object obs)
        {
            myObservers.Add((IControllerObserver)obs);
        }

        public void removeObserver(object obs)
        {
            myObservers.Remove((IControllerObserver)obs);
        }

        public void notifyObservers(object arg = null)
        {
            foreach (IControllerObserver obs in myObservers)
                obs.update("Update");
        }

        
        bool IClientConnectionManager.isConnected()
        {
            throw new NotImplementedException();
        }

    }
}

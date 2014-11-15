using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Threading;
using DisputeCommon;
using System.Windows.Forms;
using DialogueDisputeGameServer.Game_Interfaces;
using System.IO;
using DisputeCommon.Interfaces;

namespace DialogueDisputeGameServer
{
    /// <summary>
    /// --Serves as entry point of program----
    /// This class will handle communication between clients and game manager. It will have methods to
    ///     - Accept new clients
    ///     - Listen to client requests
    ///     - Display list of clients
    ///     - Pass requests to game manager
    /// </summary>
    public class ServerConnectionManager:ISocketServerConnectionManager,IFormController,IServerViewController
    {
        
        static ServerConnectionManager  myInstance = new ServerConnectionManager();
        public static ServerConnectionManager getInstance()
        {
            myInstance = myInstance ?? new ServerConnectionManager();
            return myInstance;
        }


        List<KeyValuePair<String, playerClient>> playerSocketList = new List<KeyValuePair<string, playerClient>>();

        public List<KeyValuePair<String, playerClient>> PlayerSocketList
        {
            get { return playerSocketList; }
        }
        public static bool formClosed = false;
        Thread serverThread;
        public ServerForm myForm;
        private List<Match> matches = new List<Match>();
        public List<Match> Matches
        {
            get { return matches; }
        }
        IFeedbackWriter feedbackWriter;
        public IFeedbackWriter FeedbackWriter
        {
            get { return feedbackWriter; }
            set { feedbackWriter = value; }
        }
        bool gameSentFlag = false;

        public ServerConnectionManager()
        {
            myForm=new ServerForm(this);
            myForm.Show();


            Thread serverThread = new Thread(startServer);
            serverThread.IsBackground = true;
            serverThread.Start();
            
        }
        
        public void getName()
        {
        }

        #region NETWORK-FUNCTIONS
        public void acceptTcpClient(ref TcpClient clientRef, TcpListener serverSocket)
        {
            clientRef = serverSocket.AcceptTcpClient();
        }

        public IFeedbackWriter getFeedbackWriter()
        {
            return FeedbackWriter;
        }
        public void setFeedbackWriter(IFeedbackWriter writer)
        {
            feedbackWriter=writer;
        }
        public void startServer()
        {
            IPAddress serverAddress = IPAddress.Parse(DisputeCommonGlobals.defaultAddress);
            TcpListener serverSocket = new TcpListener(serverAddress, Int32.Parse(DisputeCommonGlobals.defaultPort));


            //Start Server
            serverSocket.Start();
            //Start listening for connection requests
            listenForConnections(serverSocket);

            serverSocket.Stop();
        }
        public void listenForConnections(TcpListener serverSocket)
        {
            TcpClient clientSocket = null;

            while (!formClosed)
            {
                byte[] bytesFrom = new byte[10025], bytesTo = new byte[10025];
                try
                {
                    //Call this method in a separate thread so that the loop doesn't get blocked by acceptTcpClient call
                    //Thread socketThread = new Thread(() => (new accepter(acceptTcpClient)).Invoke(ref clientSocket, serverSocket));
                    //socketThread.IsBackground = true;

                    acceptTcpClient(ref clientSocket, serverSocket);
                    //If server is open and we havent obtained a connection sleep this thread 
                    while (!formClosed && clientSocket == null)
                        Thread.Sleep(500);

                    if (formClosed)
                        serverThread.Abort();
                    else if (clientSocket != null)
                    {                        
                        bytesFrom = new byte[10025];
                        string dataFromPlayer = null;

                        dataFromPlayer = getData(clientSocket);

                        //Check if player with same name exists
                        if (playerSocketList.FindIndex(n => n.Key.Equals(dataFromPlayer)) != -1)
                        {
                            sendData(Messages.GameMessages.playerExists.ToString(), clientSocket);
                            String ack = getData(clientSocket);
                            sendFeedback("listenForConnections","Player Exists");

                            clientSocket.Client.Disconnect(true);
                            clientSocket = new TcpClient();
                        }
                        else
                        {
                            sendFeedback("listenForConnections", "New Player:"+dataFromPlayer);
                            sendData(Messages.GameMessages.connected.ToString(), clientSocket);

                            //Add to list using name as key
                            //Start listening to player
                            playerClient client = new playerClient(this, clientSocket, dataFromPlayer);
                            playerSocketList.Add(new KeyValuePair<string, playerClient>(dataFromPlayer,                                                                                             client));
                            Console.WriteLine(dataFromPlayer);
                            client.FeedbackWriter = this.FeedbackWriter;
                            client.start();

                            //Add name to player list, use thread safe way
                            updateServerForm();
                        }
                    }//End socket if
                }//End Try
                catch (Exception ex)
                {
                    sendFeedback("listenForConnections", "Exception:" + ex.InnerException) ;
                    Console.WriteLine(ex.ToString());
                }
            }//End while

            if (clientSocket != null)
                clientSocket.Close();
        }
        public void connect(string address, string port)
        {
            throw new NotImplementedException();
        }
        public void disconnect(string playername)
        {
            this.playerSocketList.Remove(this.playerSocketList.Find(n => n.Key.Equals(playername)));
            updateServerForm();
        }        
        public String getData(TcpClient clientSocket)
        {
            //Get Data
            NetworkStream serverStream = clientSocket.GetStream();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string data = System.Text.Encoding.ASCII.GetString(inStream);
            data=data.Substring(0,data.IndexOf("$"));
            return data;
        }
        public void sendData(String data,TcpClient clientSocket)
        {
            if (clientSocket.Connected == false)
                return;
            //Send Data
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        public void sendMessage(Messages.GameMessages header, List<object> args, object sender)
        {
            switch (header)
            {
                case Messages.GameMessages.matchCreated:
                    broadcast(Messages.GameMessages.gamesUpdated);
                    break;
                case Messages.GameMessages.SendActiveMatches:
                    broadcast(Messages.GameMessages.SendActiveMatches);    
                    break;
                case Messages.GameMessages.connect:
                    break;
                case Messages.GameMessages.GameOver:
                    //Tell everyclient the game is over
                    break;
                default:
                    break;
            };
        }
        public void broadcast(Messages.GameMessages msg)
        {
            foreach (KeyValuePair<String, playerClient> current in playerSocketList)
            {                
                current.Value.getMessageFromServerManager(msg, null);
            }
        }

        #endregion


        #region LOCAL_COMM_STUFF
        public Match getMatch(String creatorName)
        {
            return matches.Find(n => n.Player1.Equals(creatorName));
        }
        public void sendFeedback(string functionName, string feedback)
        {
            FeedbackWriter.WriteLine("Server   Method:" + functionName + "   Feedback:" + feedback);
        }
        public void setPlayerName(string name)
        {
            throw new NotImplementedException();
        }
        public void getMessageFromPlayerClient(Messages.GameMessages message,List<object> data, playerClient client)
        {
            switch (message)
            {
                case Messages.GameMessages.matchCreated:
                    broadcast(Messages.GameMessages.gamesUpdated);
                    break;
                case Messages.GameMessages.gamesUpdated:
                    broadcast(Messages.GameMessages.gamesUpdated);
                    break;
                case Messages.GameMessages.SendActiveMatches:
                    broadcast(Messages.GameMessages.SendActiveMatches);
                    break;
                case Messages.GameMessages.JoinMatch:
                    String gameName = (String)data[0];
                    Match m = this.getMatch(gameName);
                    if (m != null)
                        m.Player2.PlayerName = client.playerName;
                    broadcast(Messages.GameMessages.gamesUpdated);
                    break;
                default:
                    break;
            }
            updateServerForm();
        }
        #endregion

        public void updateServerForm()
        {
            myForm.Invoke((MethodInvoker)delegate
            {
                myForm.clearPlayerList();
                foreach (KeyValuePair<String, playerClient> client in playerSocketList)
                {
                    myForm.addPlayer(client.Key);
                }
                if (playerSocketList.Count == 0)
                    myForm.clearFeedback();
                myForm.clearMatchList();
                foreach (Match match in this.Matches)
                {
                   myForm.addMatch(match.Player1.PlayerName);
                }
            });
        }
        public void formUpdate(String code)
        {
            if (code.Equals("Close"))
            {
                formClosed = true;
                serverThread.Abort();
            }
        }

        void IFormController.formClosed(string code)
        {
            formClosed=true;
        }

        /*void IConnectionManager.setData(ref SerializableCharacter character,ref SerializableCharacter world,ref List<DisputeCommon.Match> games,ref Match activeMatch)
        {
            throw new NotImplementedException();
        }*/

        bool ISocketServerConnectionManager.isConnected()
        {
            throw new NotImplementedException();
        }

        void IServerViewController.MessageSentFromView(Messages.LobbyViewMessage message, List<object> data)
        {
            throw new NotImplementedException();
        }


        IServerView IServerViewController.View
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        IFeedbackWriter IServerViewController.FeedbackWriter
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

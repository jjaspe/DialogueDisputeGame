using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DisputeGameConnection;
using System.ServiceModel.Channels;
using DialogueDisputeGameServer;
using System.Windows.Forms;
using DisputeCommon;

namespace WCFServerConsoleTester
{
    class Program:IWCFConnectionManager
    {
        static void Main(string[] args)
        {
            new Program();
        }
        List<WCFPlayer> players = new List<WCFPlayer>();
        Dictionary<string, List<WCFMessage>> Messages = new Dictionary<string, List<WCFMessage>>();

        IFeedbackWriter myFeedbackWriter=new ConsoleFeedback();
        IServerView serverView;

        public IFeedbackWriter MyFeedbackWriter
        {
            get { return myFeedbackWriter; }
            set { myFeedbackWriter = value; }
        }
        public void sendFeedback(string functionName, string feedback)
        {
            MyFeedbackWriter.WriteLine("Server   Method:" + functionName + "   Feedback:" + feedback);
        }

        #region IConnection
        public void broadcast(WCFMessage message)
        {
            foreach (var player in players)
                addNewMessage(player, message);
        }

        public WCFPlayer addNewPlayer(WCFPlayer player)
        {
            if (!players.Contains(player))
            {
                sendFeedback("addNewPlayer", "Player Exists");
            }
            else
            {
                players.Add(player);
                sendFeedback("addNewPlayer", "Player " + player.PlayerName + " connected");
            }
            return player;

        }

        public void addNewMessage(WCFPlayer player, WCFMessage msg)
        {
            Messages[player.PlayerName].Add(msg);
        }

        public void removePlayer(WCFPlayer player)
        {
            if (players.Contains(player))
                players.Remove(player);

        }

        public List<WCFMessage> getMessages(WCFPlayer player)
        {
            List<WCFMessage> msgs=Messages[player.PlayerName];
            Messages[player.PlayerName]=new List<WCFMessage>();
            return msgs;
        }

        public List<WCFPlayer> getPlayers()
        {
            return players;
        }
        #endregion

        public Program()
        {
            RunServer();
        }

        public void RunServer()
        {
            DisputeGameConnectionService instance = DisputeGameConnectionService.getInstance();
            instance.Manager = this;

            ServiceHost host = new ServiceHost(DisputeGameConnectionService.getInstance());
            using (host)
            {
                host.Open();
                Console.WriteLine("Server started");
                Console.WriteLine("\n");
                Console.WriteLine(" Configuration Name: " + host.Description.ConfigurationName);
                Console.WriteLine(" End point address: " + host.Description.Endpoints[0].Address);
                Console.WriteLine(" End point binding: " + host.Description.Endpoints[0].Binding.Name);
                Console.WriteLine(" End point contract: " + host.Description.Endpoints[0].Contract.ConfigurationName);
                Console.WriteLine(" End point name: " + host.Description.Endpoints[0].Name);
                Console.WriteLine(" End point lisent uri: " + host.Description.Endpoints[0].ListenUri);
                Console.WriteLine(" \nName:" + host.Description.Name);
                Console.WriteLine(" Namespace: " + host.Description.Namespace);
                Console.WriteLine(" Service Type: " + host.Description.ServiceType);

                Console.ReadLine();
                host.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DisputeCommon;
using DialogueDisputeGameClient.WCF;
using DialogueDisputeFormsGame.Forms;
using System.Diagnostics;
using DialogueDisputeGameClient.Network;
using DialogueDisputeFormsGameForm_Controllers;
using DialogueDisputeGameClient.Offline;
using DialogueDisputeGameServer;
using DisputeCommon.Interfaces;

namespace DialogueDisputeFormsGame
{
    static class Program
    {
        [STAThread]
        static
        void Main(string[] args)
        {
            DialogResult d = DialogResult.No;//MessageBox.Show("Play Online?", "Online/Offline", MessageBoxButtons.YesNo);

            if (d.Equals(DialogResult.Yes))
                threeFormTesting(args);
            else
                offline();
        }

        static void offline()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            IServerConnectionManager serverManager = OfflineServerManager.getInstance();
            ServerForm serverForm = new ServerForm();
            serverManager.View = serverForm;
            serverManager.FeedbackWriter = serverForm;

            MainMenuForm mainForm1 = new MainMenuForm(), mainForm2 = new MainMenuForm();
            IClientConnectionManager clientManager1 = new OfflineClientManager() { remoteProxy = serverManager, FeedbackWriter = mainForm1 },
                clientManager2 = new OfflineClientManager() { remoteProxy = serverManager, FeedbackWriter = mainForm2 };


            ConnectToServerForm connectForm1 = new ConnectToServerForm(), connectForm2 = new ConnectToServerForm();
            MainMenuController mainController1 = new MainMenuController(clientManager1, mainForm1, connectForm1) { MyMatchFormController = new GraphicMatchController() },
            mainController2 = new MainMenuController(clientManager2, mainForm2, connectForm2) { MyMatchFormController = new GraphicMatchController() };

            DialogResult d = MessageBox.Show("Autorun?", "Autorun", MessageBoxButtons.YesNo);

            if (d == DialogResult.Yes)
            {
                mainController1.isAutorun = true;
                mainController2.isAutorun = true;
                mainController1.autorunName = "Player 1";
                mainController2.autorunName = "Player 2";
            }

            mainController1.start();
            mainController2.start();

            /*
            IClientConnectionManager manager = new WCFClientManager();
            MainMenuForm mainForm = new MainMenuForm();
            mainForm.Text = arg ?? "Form";
            ConnectToServerForm connectForm = new ConnectToServerForm();
            manager.FeedbackWriter = mainForm;
            MainMenuController controller = new MainMenuController(manager,
                mainForm, connectForm);
            controller.MyMatchFormController = new GraphicMatchController();
            controller.PlayerName = arg;
            controller.start();
            //controller.setCurrentForm("Dispute");
            */

            Application.Run((MainMenuForm)mainController1.MyView);

        }

        #region ONLINE
        public static void threeFormTesting(string[] args)
        {
            Process serverProcess = new Process();
            String serverFilename = "..\\..\\..\\DialogueDisputeGameServer\\bin\\debug\\DialogueDisputeGameServer.exe";
            serverProcess.StartInfo.FileName = serverFilename;
            serverProcess.EnableRaisingEvents = true;
            String number = (args == null || args.Length == 0 || args[0] == null || args[0].Equals("First") ? "First" :
                args[0].ToString());


            //Start server and second client only if this is the first client
            if (number.Equals("First"))
            {
                serverProcess.Start();
                openNewClient("Second");
                formTesting("First");
            }
            else if (number.Equals("Second"))
            {
                //openNewClient("Third");
                formTesting("Second");
            }
            else
                formTesting("Third");


        }
        public static void twoClientFormTest(string[] args)
        {
            Process serverProcess = new Process();
            String serverFilename = "..\\..\\..\\DialogueDisputeGameServer\\bin\\debug\\DialogueDisputeGameServer.exe";
            serverProcess.StartInfo.FileName = serverFilename;
            serverProcess.EnableRaisingEvents = true;
            bool first = (args == null || args.Length == 0 || args[0] == null || !args[0].Equals("Last"));


            //Start server and second client only if this is the first client
            if (first)
            {
                //serverProcess.Start();
            }

            if (first)
            {
                //openNewClient("Last");
                formTesting("First");
            }
            else
                formTesting("Second");


            //consoleTesting(true);
        }
        static void formTesting(string arg = null)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IClientConnectionManager manager = new WCFClientManager();
            MainMenuForm mainForm = new MainMenuForm();
            mainForm.Text = arg ?? "Form";
            ConnectToServerForm connectForm = new ConnectToServerForm();
            manager.FeedbackWriter = mainForm;
            MainMenuController controller = new MainMenuController(manager,
                mainForm, connectForm);
            controller.MyMatchFormController = new GraphicMatchController();
            controller.PlayerName = arg;
            controller.start();
            //controller.setCurrentForm("Dispute");

            Application.Run((MainMenuForm)controller.MyView);
        }

        public static void openNewClient(String name)
        {
            Process serverProcess = Process.Start("DialogueDisputeFormsGame.exe", name);
            serverProcess.EnableRaisingEvents = true;
        }

        static void twoClientConsoleTest(string[] args)
        {
            Process serverProcess = new Process();
            String serverFilename = "..\\..\\..\\DialogueDisputeGameServer\\bin\\debug\\DialogueDisputeGameServer.exe";
            serverProcess.StartInfo.FileName = serverFilename;
            serverProcess.EnableRaisingEvents = true;
            bool first = (args == null || args.Length == 0 || args[0] == null || !args[0].Equals("Last"));


            //Start server and second client only if this is the first client
            if (first)
            {
                serverProcess.Start();
            }

            if (first)
            {
                openNewClient("Second");
                consoleTesting(true, "Player1");
            }
            else
                consoleTesting(true, "Player2");
        }
        static void consoleTesting(bool loop, string name)
        {

            MainMenuConsoleView mView = new MainMenuConsoleView();
            ConnectToServerConsoleView cView = new ConnectToServerConsoleView();
            cView.playerName = name;
            IClientConnectionManager cManager = new ClientConnectionManager();
            cManager.FeedbackWriter = new DisputeCommon.FormFeedback();
            MainMenuController mController = new MainMenuController(cManager, mView, cView);
            ConnectToServerController cController = ConnectToServerController.getInstance(cManager);
            connectTest(cView);
            createGameTest(mView);
            //cView.sendMessage(Messages.ViewMessage.connect);
            //mView.sendMessage(MainMenuMessages.CreateGame);
            //cView2.sendMessage(Messages.ViewMessage.connect);
            //mView2.sendMessage(MainMenuMessages.CreateGame);

            while (loop) ;

        }
        static void connectTest(ConnectToServerConsoleView view)
        {
            view.sendMessage(Messages.LobbyViewMessage.connect);
        }
        static void createGameTest(MainMenuConsoleView view)
        {
            view.sendMessage(MainMenuMessages.CreateMatch);
        }

        class MainMenuConsoleView : IMainMenuView
        {
            private IMainMenuController myController;

            public IMainMenuController Controller
            {
                get { return myController; }
                set { myController = value; }
            }
            String playerName;
            void IMainMenuView.start()
            {
                throw new NotImplementedException();
            }

            void IMainMenuView.stop()
            {
                throw new NotImplementedException();
            }

            void IMainMenuView.updateCharacterData(string playerName, List<KeyValuePair<string, double>> properties)
            {
                throw new NotImplementedException();
            }

            void IMainMenuView.updateProperty(string propertyName, string playerName, string value)
            {
                throw new NotImplementedException();
            }

            void IMainMenuView.updateMatches(List<DisputeCommon.Match> matches)
            {
                Console.WriteLine(matches == null ? "Empty" : "Not Empty:" + matches.Count.ToString());
            }


            public void sendMessage(MainMenuMessages msg)
            {
                myController.MessageSentFromView(msg, null);
            }


            void IMainMenuView.updatePlayerName(string name)
            {
                playerName = name;
            }


            void IMainMenuView.updateSelectedMatch(Match activeMatch)
            {
                Console.WriteLine(activeMatch == null ? "No Active Match" : "Player1:" + activeMatch.Player1);
            }


            IMainMenuController IMainMenuView.Controller
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

            bool IMainMenuView.Connected
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

        class ConnectToServerConsoleView : IConnectToServerView
        {
            IConnectToServerController myController;
            int connectTry = 0;
            public String playerName;

            public void sendMessage(Messages.LobbyViewMessage msg)
            {
                switch (msg)
                {
                    case Messages.LobbyViewMessage.connect:
                        myController.MessageSentFromView(msg, new List<object>{DisputeCommonGlobals.defaultAddress,
                    DisputeCommonGlobals.defaultPort,playerName}, this);
                        break;
                    default:
                        break;
                }
            }


            void IConnectToServerView.start()
            {
                throw new NotImplementedException();
            }

            void IConnectToServerView.stop()
            {
                throw new NotImplementedException();
            }

            void IConnectToServerView.setController(DisputeCommon.IConnectToServerController controller)
            {
                myController = controller;
            }
        }
        #endregion
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DialogueDisputeGameMultiplayer.Offline;
using DisputeCommon.Interfaces;
using DialogueDisputeGameServer;
using DialogueDisputeGameClient.Forms;
using DisputeCommon;

namespace ConnectionTester
{
    [TestClass]
    public class UnitTest1
    {
        IServerConnectionManager serverManager;
        ServerForm serverForm;
        private MainMenuForm mainForm1;
        private MainMenuForm mainForm2;
        IClientConnectionManager clientManager1, clientManager2;

        [TestMethod]
        public void ServerNotNull()
        {
            Assert.IsNotNull(serverManager);
        }

        [TestMethod]
        public void ServerFormNotNull()
        {
            Assert.IsNotNull(serverForm);
        }

        [TestMethod]
        public void ServerHasView()
        {
            Assert.IsNotNull(serverManager.View);
        }

        [TestMethod]
        public void ServerHasFeedbackWriter()
        {
            Assert.IsNotNull(serverManager.FeedbackWriter);
        }

        [TestMethod]
        public void MainForm1NotNull()
        {
            Assert.IsNotNull(mainForm1);
        }

        [TestMethod]
        public void MainForm2NotNull()
        {
            Assert.IsNotNull(mainForm2);
        }


        void offline()
        {

            serverManager = OfflineServerManager.getInstance();
            serverForm = new ServerForm();
           
            serverManager.View = serverForm;
            serverManager.FeedbackWriter = serverForm;

            mainForm1 = new MainMenuForm();
            mainForm2 = new MainMenuForm();

            /*
            clientManager1 = new OfflineClientManager() { remoteProxy = serverManager, FeedbackWriter = mainForm1 };
            clientManager2 = new OfflineClientManager() { remoteProxy = serverManager, FeedbackWriter = mainForm2 };


            ConnectToServerForm connectForm1 = new ConnectToServerForm(), connectForm2 = new ConnectToServerForm();
            MainMenuController mainController1 = new MainMenuController(clientManager1, mainForm1, connectForm1) { MyMatchFormController = new GraphicMatchController() },
            mainController2 = new MainMenuController(clientManager2, mainForm2, connectForm2) { MyMatchFormController = new GraphicMatchController() };
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


        }

        
    }
}

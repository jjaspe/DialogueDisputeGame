using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DisputeCommon.Interfaces;
using DialogueDisputeGameMultiplayer.Offline;
using DialogueDisputeGameMultiplayer;
using DisputeCommon;
using DialogueDisputeFormsGameForms;
using DialogueDisputeFormsGameForm_Controllers;

namespace DialogueDisputeOpenGLView
{
    class Program
    {
        [STAThread]
        static public void Main(string[] args)
        {
            DialogResult d = MessageBox.Show("Play Online?", "Online/Offline", MessageBoxButtons.YesNo);

            if (d.Equals(DialogResult.Yes))
                MessageBox.Show("Not available");//threeFormTesting(args);
            else
                offline();
            //twoClientFormTest(args);
            //twoClientConsoleTest(args);

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
            MainMenuController mainController1 = new MainMenuController(clientManager1, mainForm1, connectForm1) { MyMatchFormController = new GraphicMatchController(new DisputeOpenGLView()) },
            mainController2 = new MainMenuController(clientManager2, mainForm2, connectForm2) { MyMatchFormController = new GraphicMatchController(new DisputeOpenGLView()) };
            mainController1.isAutorun = true;
            mainController2.isAutorun = true;
            mainController1.autorunName = "Player 1";
            mainController2.autorunName = "Player 2";
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
    }
}

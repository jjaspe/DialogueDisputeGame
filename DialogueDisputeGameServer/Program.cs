using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DisputeCommon;
using System.Security.Principal;
using DisputeGameConnection;
using System.ServiceModel;
using System.Xml;
using DisputeCommon.Interfaces;

namespace DialogueDisputeGameServer
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Program obj = new Program();

            if (!obj.IsCurrentlyRunningAsAdmin())
                MessageBox.Show("Did you run it as administrator?", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (args != null && args.Length > 0 && args[0] != null)
                MessageBox.Show(args[0]);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //controller.setCurrentForm("Dispute");
            //ServerConnectionManager c = new ServerConnectionManager();
            WCFManager c = WCFManager.getInstance();
            ServerForm form = new ServerForm(c);
            c.View = form;
            //FormFeedback f = new FormFeedback();
            c.FeedbackWriter = form;
            Application.Run((Form)c.View);

        }


        private bool IsCurrentlyRunningAsAdmin()
        {
            bool isAdmin = false;
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            if (currentIdentity != null)
            {
                WindowsPrincipal pricipal = new WindowsPrincipal(currentIdentity);
                isAdmin = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
                pricipal = null;
            }
            return isAdmin;
        }
    }
}

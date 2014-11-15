using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DialogueDisputeFormsGameForm_Controllers;
using DisputeCommon;



namespace DialogueDisputeFormsGame.Forms
{
    public partial class ConnectToServerForm : Form,IConnectToServerView
    {
        IConnectToServerController myController;
        bool asDialog = false;
        string playerName, port, address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        

        public string PlayerName
        {
          get { return playerName; }
          set { playerName = value; }
        }
        public bool AsDialog
        {
            get { return asDialog; }
            set { asDialog = value; }
        }

        public IConnectToServerController MyController
        {
            get { return myController; }
            set { myController = value; }
        }

        
        public ConnectToServerForm(IConnectToServerController controller=null)
        {
            InitializeComponent();
            myController = controller;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!asDialog)
                myController.MessageSentFromView(Messages.LobbyViewMessage.connect, new List<object> { txtAddress.Text, txtPort.Text, txtName.Text }, this);
            else
            {
                if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(txtPort.Text) ||
                    String.IsNullOrEmpty(txtAddress.Text))
                    MessageBox.Show("Fill all info", "Error");
                else
                {
                    PlayerName = txtName.Text;
                    Address = txtAddress.Text;
                    Port = txtPort.Text;
                    this.Close();
                }
            }
        }


        private void ConnectToServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!AsDialog)
            {
                myController.MessageSentFromView(Messages.LobbyViewMessage.viewClosed, null, this);
                //Give the controller a new form because this one will be disposed
                ConnectToServerForm form = new ConnectToServerForm(MyController);
                MyController.setView(form);
            }
        }

        void IConnectToServerView.start()
        {
            this.Enabled = true;
            this.Show();
        }

        void IConnectToServerView.stop()
        {
            this.Enabled = false;
            this.Hide();
        }

        void IConnectToServerView.setController(DisputeCommon.IConnectToServerController c)
        {
            MyController = c;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (asDialog)
            {
                PlayerName = "";
                Address="";
                Port="";
                this.Close();
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnConnect_Click(sender, e);
            }
        }

        private void ConnectToServerForm_Load(object sender, EventArgs e)
        {

        }

        
    }
}

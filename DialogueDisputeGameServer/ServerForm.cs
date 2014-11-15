using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using DialogueDisputeGameServer.Game_Interfaces;
using DisputeCommon;

namespace DialogueDisputeGameServer
{
    delegate void accepter(ref TcpClient clientSocket,TcpListener serverSocket);

    public partial class ServerForm : Form,IFeedbackWriter,IServerView
    {
        IServerViewController myController;
        public ServerForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
        }

        public ServerForm(IServerViewController c)
        {
            InitializeComponent();
            myController = c;
            this.WindowState = FormWindowState.Minimized;
        }

        public void addPlayer(String name)
        {
            lstPlayers.Items.Add(name);
            lstPlayers.Update();
        }
        public void removePlayer(String name)
        {
            lstPlayers.Items.Remove(name);
        }
        public void addMatch(String creatorName)
        {
            lstMatches.Items.Add(creatorName);
            lstPlayers.Update();
        }
        public void removeMatch(String creatorName)
        {
            lstMatches.Items.Remove(creatorName);
        }
        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            myController.MessageSentFromView(Messages.LobbyViewMessage.viewClosed,null);
        }


        internal void clearPlayerList()
        {
            lstPlayers.Items.Clear();
        }
        public void clearMatchList()
        {
            lstMatches.Items.Clear();
        }

        void IFeedbackWriter.WriteLine(string line)
        {
            if (this.IsDisposed)
                return;
                txtFeedback.Text = line + "\r\n" + txtFeedback.Text;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFeedback();
        }

        public void clearFeedback()
        {
            this.txtFeedback.Text = "";
        }

        void IServerView.updatePlayers(List<string> playerNames)
        {
            lstPlayers.DataSource = playerNames;
        }

        void IServerView.updateGames(List<string> gameNames)
        {
            lstMatches.DataSource = gameNames;
        }

        void IServerView.updateFeedback(string feedback)
        {
            txtFeedback.Text = feedback + "\r\n" + txtFeedback.Text;
        }

        void IServerView.clearFeedback()
        {
            txtFeedback.Clear();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            myController.MessageSentFromView(Messages.LobbyViewMessage.StartServer, null);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            myController.MessageSentFromView(Messages.LobbyViewMessage.StopServer, null);
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            myController.MessageSentFromView(Messages.LobbyViewMessage.StartServer, null);
        }
    }
}

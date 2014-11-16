using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CharacterSystemLibrary.Classes;

using DisputeCommon;

using DialogueDisputeFormsGame.Forms;
using DisputeCommon.Arguments;

namespace DialogueDisputeGameClient
{
    public partial class MatchForm : Form,IMatchView,IFeedbackWriter
    {
        private IMatchController myController;

        public CharacterData Character
        {
            get { return Player1?Match.Player1.Character:Match.Player2.Character; }
        }
        public string playerName
        {
            get {return Player1?this.Match.Player1.PlayerName:Match.Player2.PlayerName;}
        }

        Match Match
        {
            get
            {
                return myController.Match;
            }
        }
        bool Player1
        {
            get
            {
                return myController.IsPlayerOne;
            }
        }
        CharacterData WorldCharacter
        {
            get { return Match.World; }
        }

        public IMatchController Controller
        {
            get { return myController; }
            set { myController = value; }
        }
        public MatchForm()
        {
            this.Top = 0;
            InitializeComponent();
            initialize();
        }
        public MatchForm(IMatchController controller)
        {
            this.Top = 0;
            InitializeComponent();
            myController = controller;
            initialize();
        }

        /// <summary>
        /// Adds tooltip to buttons using the button Argument's defense and attack property
        /// </summary>
        void initialize()
        {
            string btnName = "";
            foreach (System.Windows.Forms.Control control in this.Controls)
            {

                if (control is customButton)
                {
                    btnName = control.Name.Substring(control.Name.IndexOf("txt") + 3, control.Name.Length - 3);

                    //Let's get arg  first
                    Argument arg = (from current in Match.PossibleArguments
                                    where current.ToString().Equals(btnName)
                                    select current).First();

                    //Now get roll properties and put them in the tag
                    control.Tag = arg.AttackerCheckProperty + " VS " + arg.DefenderCheckProperty;
                    ((customButton)control).setToolTipFromTag();
                }
            }
        }

        
        //Fills controls' values 
        public void updateControls()
        {
            //Group box
            this.grpPlayerInfo.Text = Character.Name;
            this.resistancePointsLabel1.Text = Character.getValue("Resistance").ToString();
            this.fortitudePointsLabel1.Text = Character.getValue("Fortitude").ToString();
            this.selfControlPointsLabel1.Text = Character.getValue("Self Control").ToString();
            this.persuasionPointsLabel1.Text = Character.getValue("Persuasion").ToString();
            this.intimidationPointsLabel1.Text = Character.getValue("Intimidation").ToString();
            this.subterfugePointsLabel1.Text = Character.getValue("Subterfuge").ToString();
            this.perceptionPointsLabel1.Text = Character.getValue("Perception").ToString();

            int toneValue = (int) WorldCharacter.getValue("Tone");
            this.toneComboBox.SelectedIndex = toneValue < 6 ? (int)(toneValue / 2) :
                (int)((toneValue - 1) / 2);

            //Update grids
            SoMGridP1.SoMAnger = (int)Character.getValue("StateOfMindAngerFear");
            SoMGridP1.SoMJoy = (int)Character.getValue("StateOfMindJoySorrow");


            txtMatchTranscript.Text = Match.Transcript.Replace("\n","\r\n");

            //Update Arguement feedback if the action was mine, or if it was the opponent but it was visible
            if (Match.LastArgument!=null && (!Match.LastArgument.hidden || Match.LastArgument.playerName.Equals(playerName)))
                txtArgumentFeedback.Text = Match.LastArgumentTranscript.Replace("\n","\r\n");
        }


        //Override show to update controls before showing
        public new void Show()
        {
            updateControls();
            base.Show();
        }

        private void charmAction_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action,new List<object>{ Messages.GameMessages.Charm},this);
        }

        private void convinceButton_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action,new List<object>{ Messages.GameMessages.Convince},this);
           
        }

        private void scareButton_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action,new List<object>{ Messages.GameMessages.Scare},this);
            
        }

        private void manipulateButton_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action,new List<object>{ Messages.GameMessages.Manipulate},this);
            
        
        }

        private void tauntButton_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action,new List<object>{Messages.GameMessages.Taunt},this);
            
        }

        private void coerceButton_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action, new List<object>{Messages.GameMessages.Coerce},this);
            
        }

        private void bluffButton_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action, new List<object>{Messages.GameMessages.Bluff},this);
           
        }

        private void form_Closed(object sender, EventArgs e)
        {
            myController.MessageSentFromView(MatchViewMessage.Close,new List<object>{null},this);
        }

        /* Control setters for controller */
        public void setLabel(string propertyName, string playerName, string value)
        {
            string labelName = "";
            switch (playerName)
            {
                case "Player":
                    labelName = propertyName + "PointsLabel1";
                    foreach (Control control in this.grpPlayerInfo.Controls)
                    {
                        if (control.Name == labelName)
                            ((Label)control).Text = value;
                    }
                    break;
                
                default:
                    break;
            }

        }


        private void empathyButton_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action, new List<object>{Messages.GameMessages.Empathy}, this);
        }

        private void btnFocus_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action, new List<object> { Messages.GameMessages.Focus }, this);

        }

        private void btnTrick_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action, new List<object> { Messages.GameMessages.Trick }, this);
        }

        private void messagesTimer_Tick(object sender, EventArgs e)
        {
            myController.MessageSentFromView(MatchViewMessage.Update, null, this);
            updateControls();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MatchViewMessage.Action, new List<object> { Messages.GameMessages.Analyze }, this);
        }

        private void btnEndGame_Click(object sender, EventArgs e)
        {
            myController.MessageSentFromView(MatchViewMessage.Close, null, this);
            this.Close();
        }

        void IMatchView.start()
        {
            this.Text = playerName;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.SetDesktopLocation(0, 200);
            this.messagesTimer.Enabled = true;
            this.Show();
        }

        void IMatchView.stop()
        {
            this.messagesTimer.Enabled = false;
        }

        void IMatchView.updateMatch(Match match)
        {
            updateControls();
        }
        SOM IMatchView.selectSoM(SOM allowed, Result result)
        {
            SoMSelectorForm somForm = new SoMSelectorForm(allowed,result);
            DialogResult d=somForm.ShowDialog();
            if (d.Equals(DialogResult.OK))
                return somForm.Selected;
            else
                return SOM.None;
        }
        void IMatchView.gameOver(bool won)
        {
            if (won)
                MessageBox.Show("You have won");
            else
                MessageBox.Show("You lost");

            myController.MessageSentFromView(MatchViewMessage.GameOver, null, this);
            this.Close();
        }
        void IFeedbackWriter.WriteLine(string line)
        {
            txtMatchTranscript.Text=line+"\r\n"+grpPlayerInfo.Text;
        }
    }
        
}

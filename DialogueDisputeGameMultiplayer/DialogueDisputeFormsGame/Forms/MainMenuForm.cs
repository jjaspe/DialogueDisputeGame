using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DialogueDisputeFormsGameForm_Controllers;

using System.IO;
using DisputeCommon;

using System.Threading;

namespace DialogueDisputeFormsGameForms
{
    public partial class MainMenuForm : Form,IMainMenuView,IFeedbackWriter
    {               

        private IMainMenuController myController;
        public IMainMenuController Controller
        {
            get { return myController; }
            set { myController = value; }
        }
        public CharacterData Character
        {
            get { 
                if (Players != null)
                {
                    DataPlayer p = Players.Find(n => n.PlayerName.Equals(PlayerName));
                    if (p != null)
                        return p.Character;
                    else
                        return null;
                }
                else
                    return null;
            }
        }
        public List<DataPlayer> Players { get { return Controller.Players; } }
        public List<Match> Matches { get { return Controller.Matches; } }
        public String PlayerName { get { return Controller.PlayerName; } }
        public DataPlayer Player { get { return Players.Find(n => n.PlayerName.Equals(PlayerName)); } }
        String feedback = "";
        bool connected = false;

        public bool Connected
        {
            get { return connected; }
            set
            {                
                if (value == true)
                {
                    connected = value;
                    timerMessages.Enabled = true;                    
                }
                else
                {
                    timerMessages.Enabled = false;
                    lstMatches.DataSource = null;
                }

            }
        }

        public MainMenuForm(IMainMenuController controller=null)
        {
            myController = controller;
            InitializeComponent();
            lstMatches.DisplayMember = "MatchName";
            lstMatches.ValueMember = "MatchName";
        }
        

        //Methods for controller to use
        public void setLabel(string propertyName, string playerName, string value)
        {
            string labelName = "";
            switch (playerName)
            {
                case "Player1":
                    labelName = propertyName + "PointsLabel1";
                    foreach (Control control in this.grpPlayer1.Controls)
                    {
                        if (control.Name == labelName)
                            ((Label)control).Text = value;
                    }
                    break;
                case "Player2":
                    labelName = propertyName + "PointsLabel2";
                    foreach (Control control in this.grpPlayer2.Controls)
                    {
                        if (control.Name == labelName)
                            ((Label)control).Text = value;
                    }
                    break;
                default:
                    break;
            }

        }
        public void setSelectedToneIndex(string playerName, int index)
        {
            switch (playerName)
            {
                case "Player1":
                    this.tonePlayer1Box.SelectedIndex = index%2==0?index/2:(index-1)/2;
                    break;
                case "Player2":
                    this.tonePlayer2Box.SelectedIndex = index%2==0?index/2:(index-1)/2;
                    break;
                case "Dispute":
                    this.toneComboBox.SelectedIndex = index%2==0?index/2:(index-1)/2;
                    break;
                //(int)myGame.Player1.getStat("Tone").Value / 2;
                default:
                    break;
            }
        }
        public void updateMatches(List<Match> matches)
        {
            if (this.IsDisposed)
                return;
            this.Invoke((MethodInvoker)delegate
            {
                //See if there is a selected item
                String gameName="";
                int i = this.lstMatches.SelectedIndex;
                if (i != -1)//Something is selected, so get its name
                {
                    gameName = this.lstMatches.SelectedItem.ToString();
                    //When the list is repopulated, select this item again.
                }
                lstMatches.Items.Clear();
                foreach (Match game in matches)                   
                    lstMatches.Items.Add(game.Player1);
                
                int index=lstMatches.Items.IndexOf(gameName);
                //Reselect if something was selected,check that it is still on the list
                if (!gameName.Equals("")&&index>-1)
                    this.lstMatches.SetSelected(index,true);
            });
            
        }
        public void updateSelectedMatch(Match selectedMatch)
        {
            if (selectedMatch == null)
                clearSelectedMatch();
            else
            {
                lblPlayer1.Text = selectedMatch.Player1.PlayerName ?? "";
                lblPlayer2.Text = selectedMatch.Player2 == null || selectedMatch.Player2.PlayerName == null ? "" : selectedMatch.Player2.PlayerName;
                if (selectedMatch.Player1Ready)
                    lblPlayer1.BackColor = Color.Green;
                else
                    lblPlayer1.BackColor = SystemColors.Control;
                if (selectedMatch.Player2Ready)
                    lblPlayer2.BackColor = Color.Green;
                else
                    lblPlayer2.BackColor = SystemColors.Control;
            }
        }
        public void updateMatchList()
        {
            Match selectedMatch = (Match)lstMatches.SelectedItem;
            if (selectedMatch != null)
            {
                selectedMatch = Matches.Find(n => n.Equals(selectedMatch));
            }
            lstMatches.DataSource = Matches;
            if(Matches.Contains(selectedMatch))
                lstMatches.SelectedItem=selectedMatch;
            ((CurrencyManager)lstMatches.BindingContext[lstMatches.DataSource]).Refresh();
        }
        public void updatePlayerName(string name)
        {
            this.txtPlayerName.Text = name;
            
        }
        void updateCharacterControls(string player,CharacterData character)
        {
            //Player 1 Controls
            
            if (character != null)
            {
                /*
                foreach (string skillName in Common.Skills)
                    (this as IMainMenuView).updateProperty(skillName, player,
                        character.getSkill(nameConverter(skillName)).ToString());

                foreach (string statName in Common.Stats)
                    (this as IMainMenuView).updateProperty(statName, player,
                        character.getStat(nameConverter(statName)).ToString());*/

                this.resistancePointsLabel1.Text = character.getValue("Resistance").ToString();
                this.fortitudePointsLabel1.Text = character.getValue("Fortitude").ToString();
                this.selfControlPointsLabel1.Text = character.getValue("Self Control").ToString();
                this.persuasionPointsLabel1.Text = character.getValue("Persuasion").ToString();
                this.intimidationPointsLabel1.Text = character.getValue("Intimidation").ToString();
                this.subterfugePointsLabel1.Text = character.getValue("Subterfuge").ToString();
                this.perceptionPointsLabel1.Text = character.getValue("Perception").ToString();

                    (this as IMainMenuView).updateProperty("Tone", player,
                        ((int)character.getStat("Tone")).ToString());
            }
            /*
                if (myWorldCharacter != null)
                {
                    myView.updateProperty("Tone", "Dispute",
                            (Math.Round(myWorldCharacter.getStat("Tone") / 2).ToString()));
                }*/
        }
        string nameConverter(string name)
        {
            String converted = name;
            char previous = name[0], current = name[0];
            for (int i = 0, j = 0; i < name.Length; i++, j++)
            {
                previous = current;
                current = name[i];
                if (Char.IsLower(previous) && !Char.IsLower(current))//New Word
                {
                    converted = converted.Insert(j, " ");
                    j++;
                    converted.PadRight(converted.Length + 1);
                }
                else
                    converted.Insert(j, name[i].ToString());
            }
            converted = Char.ToUpper(converted[0]).ToString() + converted.Substring(1);
            return converted;
        }
        private void updateControls()
        {
            updateMatchList();
            txtPlayerName.Text = PlayerName;

            if (lstMatches.SelectedItem != null)
            {
                toneComboBox.SelectedIndex=Math.Min(4,(int)((Match)lstMatches.SelectedItem).World.getStat("Tone")/2);
                updateSelectedMatch((Match)lstMatches.SelectedItem);                
            }

            //See if player is in a match
            Match m = Matches.Find(n => n.Player1.PlayerName.Equals(PlayerName) || (n.Player2!=null && n.Player2.PlayerName.Equals(PlayerName)));
            if (m == null)//Player is not in a game so show in group 1,clear group 2
            {
                updateCharacterControls("Player1", Character);
                grpPlayer1.Text = this.PlayerName;
                clearGroup2();
            }
            else//Player is in a game so put him in the right spot
            {
                if (m.Player1.PlayerName.Equals(PlayerName))//This player is one, 
                {
                    updateCharacterControls("Player1", Character);
                    grpPlayer1.Text = this.PlayerName;
                    if (m.Player2 != null)//check if there is a player2
                    {
                        DataPlayer player2 = Players.Find(n => n.PlayerName.Equals(m.Player2.PlayerName));
                        if (player2 != null)//put player2 in grooup 2
                        {
                            grpPlayer2.Text = this.PlayerName;
                            if (player2.Character != null)
                            {
                                grpPlayer2.Text = player2.PlayerName;
                                updateCharacterControls("Player2", player2.Character);
                            }
                        }
                    }
                    else//No player 2,clear group 2
                        clearGroup2();

                }
                else //Player is player 2
                {
                    updateCharacterControls("Player2", Character);
                    grpPlayer2.Text = this.PlayerName;
                    DataPlayer creator = Players.Find(n => n.PlayerName.Equals(m.Player1.PlayerName));//Get match's player1
                    if (creator != null)
                    {
                        grpPlayer2.Text = this.PlayerName;
                        if (creator.Character != null)
                        {
                            grpPlayer1.Text = creator.PlayerName;
                            updateCharacterControls("Player1", creator.Character);
                        }
                    }
                }
            }
            
        }
        void clearGroup2()
        {
            foreach (string stat in DisputeCommonGlobals.Stats)
                setLabel(stat, "Player2", "0");
            foreach (string stat in DisputeCommonGlobals.Skills)
                setLabel(stat, "Player2", "0");
            setSelectedToneIndex("Player2", -1);
        }
        void clearSelectedMatch()
        {
            lblPlayer1.Text = "";
            lblPlayer2.Text = "";
            lblPlayer1.BackColor = SystemColors.Control;
            lblPlayer2.BackColor = SystemColors.Control;
        }

        #region IMAINMENUVIEW
        void IMainMenuView.updatePlayerName(string name)
        {
            updatePlayerName(name);
        }
        void IMainMenuView.updateProperty(string propertyName, string playerName, string value)
        {
            if(propertyName.ToUpper().Equals("TONE"))
            {
                setSelectedToneIndex(playerName,Int32.Parse(value));
            }
            else
                setLabel(propertyName,playerName,value);
        }
        void IMainMenuView.updateSelectedMatch(Match m)
        {
            matchBindingSource.Add(m);
        }
        void IMainMenuView.updateMatches(List<Match> matches)
        {
            if (matches != null)
                updateMatches(matches);
        }
        
        void IMainMenuView.start()
        {
            this.Enabled = true;
            this.timerMessages.Enabled = true;
            this.Show();
        }
        void IMainMenuView.stop()
        {
            this.Enabled = false;
            this.timerMessages.Enabled = false;
            this.Hide();
        }
        void IMainMenuView.updateCharacterData(string playerName, List<KeyValuePair<String, double>> properties)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        void IFeedbackWriter.WriteLine(string line)
        {
            if (this.IsDisposed)
                return;
            this.Invoke((MethodInvoker)delegate
            {
                txtFeedback.Text = line + "\r\n" + txtFeedback.Text;
            });
        }

        

        //Control Events
        private void connectToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myController.MessageSentFromView(MainMenuMessages.ConnectToServer, null);
        }

        private void btnCreateMatch_Click(object sender, EventArgs e)
        {
            myController.MessageSentFromView(MainMenuMessages.CreateMatch, null);
        }

        private void MainMenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.MessageSentFromView(MainMenuMessages.FormClosed, null);
        }
        private void createCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myController.MessageSentFromView(MainMenuMessages.CreateCharacter,null);
        }
        private void loadPlayer1_Click(object sender, EventArgs e)
        {
            object doc = loadCharacterFromXml();
            myController.MessageSentFromView(MainMenuMessages.LoadPlayer1,new List<object>{doc});
        }
        private void loadPlayer2_Click(object sender, EventArgs e)
        {
            object doc = loadCharacterFromXml("Player2");
            myController.MessageSentFromView(MainMenuMessages.LoadPlayer2,new List<object>{doc});
        }
        private void playDisputeButton_Click(object sender, EventArgs e)
        {
            myController.MessageSentFromView(MainMenuMessages.PlayDispute, null);
        }
        public static XmlDocument loadCharacterFromXml(string defaultName="Player1")
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string path = "..\\DisputeCharacters";
            dialog.InitialDirectory = System.IO.Path.GetFullPath(path);
            dialog.Filter = "sdc Files (*.sdc)|*.sdc";
            DialogResult res = dialog.ShowDialog();
            String filename;
            if (res != DialogResult.OK)
            {
                MessageBox.Show("Couldn't open file");
                return null;
            }
            else
                filename = dialog.FileName;

            XmlDocument myDoc = new XmlDocument();
            try
            {
                myDoc.Load(filename);
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't load player in " + filename);
                return null;
            }
            /*
            string fileName = Microsoft.VisualBasic.Interaction.InputBox
                ("Enter character name", "Load Character", "C:/DisputeCharacters/"+defaultName+".xml", 600, 400);
            fileName = fileName.StartsWith("C:/DisputeCharacters/") ? fileName : fileName.Insert(0, "C:/DisputeCharacters/");   //Put in correct directory
            fileName = fileName.EndsWith(".xml") ? fileName : fileName + ".xml";    //Make sure filename has .xml extension
            */
            

            return myDoc;            
        }
        private void lstGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (lstMatches.SelectedItem != null)
            //{
            //    Match m = Matches.Find(n=>n==(Match)lstMatches.SelectedItem);
            //    this.updateSelectedMatch(m);
            //}
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            if (lstMatches.SelectedItem != null)
            {
                Match selMatch = (Match)lstMatches.SelectedItem;
                if (selMatch != null )
                    Controller.MessageSentFromView(MainMenuMessages.JoinMatch, new List<object> { selMatch });
            }
        }

        private void btnLeaveGame_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MainMenuMessages.LeaveGame, null);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MainMenuMessages.RefreshMatches, null);
            updateControls();
        }

        private void timerMessages_Tick(object sender, EventArgs e)
        {
            Controller.MessageSentFromView(MainMenuMessages.Update, null);
            updateControls();            
        }

        private void btnLockIn_Click(object sender, EventArgs e)
        {
            if (this.Character == null)
                MessageBox.Show("Load a character first");
            else if (Player.ActiveMatch == null)
                MessageBox.Show("Load a goal first");
            else
                myController.MessageSentFromView(MainMenuMessages.PlayerReady, null);
        }

        private void createGoalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myController.MessageSentFromView(MainMenuMessages.CreateGoal, null);
        }

        


        
    }

       
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using CharacterSystemLibrary.Classes;
using System.IO;
using System.Xml;


using DisputeCommon;

namespace DialogueDisputeGameClient
{
    public partial class CreateCharacterForm : Form
    {
        ICreateCharacterController myController;
        public bool asDialog=false;
        public CharacterData character;

        public ICreateCharacterController MyController
        {
            private get { return myController; }
            set { myController = value; }
        }

        public CreateCharacterForm(ICreateCharacterController controller)
        {
            myController = controller;
            resetForm();
        }
        public CreateCharacterForm()
        {
            resetForm();
        }

        private void resetForm()
        {
            InitializeComponent();
        }

        public void skillPointsChanged(int value)
        {
            int pointsLeft = Int32.Parse(skillPointsLabel.Text),
                newPoints=pointsLeft+value;
            if (newPoints == 0)//Stop Adding Points
            {
                
                intimidationPlusButton.Enabled = false;
                perceptionPlusButton.Enabled = false;
                persuasionPlusButton.Enabled = false;
                subterfugePlusButton.Enabled = false;
            }
            else
            {
                intimidationPlusButton.Enabled = true;
                perceptionPlusButton.Enabled = true;
                persuasionPlusButton.Enabled = true;
                subterfugePlusButton.Enabled = true;
            }

            skillPointsLabel.Text = newPoints.ToString();            
        }
        public void bonusPointsChanged(int value)
        {
            int pointsLeft = Int32.Parse(bonusPointsLabel.Text),
                newPoints = pointsLeft + value;
            if (newPoints == 0)//Stop Adding Points
            {
                
                bonusIntimidationPlusButton.Enabled = false;
                bonusPerceptionPlusButton.Enabled = false;
                bonusPersuasionPlusButton.Enabled = false;
                bonusSubterfugePlusButton.Enabled = false;
                fortitudePlusButton.Enabled = false;
                selfControlPlusButton.Enabled = false;
                resistancePlusButton.Enabled = false;
            }
            else
            {
                bonusIntimidationPlusButton.Enabled = true;
                bonusPerceptionPlusButton.Enabled = true;
                bonusPersuasionPlusButton.Enabled = true;
                bonusSubterfugePlusButton.Enabled = true;
                fortitudePlusButton.Enabled = true;
                selfControlPlusButton.Enabled = true;
                resistancePlusButton.Enabled = true;
            }
            bonusPointsLabel.Text = newPoints.ToString();   
        }        

        #region SKILL BUTTONS
        private void intimidationPlusButton_Click(object sender, EventArgs e)
        {
            intimidationPointsLabel.Text = (Int32.Parse(intimidationPointsLabel.Text) + 1).ToString();
            skillPointsChanged(-1);
            intimidationMinusButton.Enabled = true;
            if(Int32.Parse(intimidationPointsLabel.Text)>=3)
                bonusIntimidationPlusButton.Enabled=false;

        }

        private void intimidationMinusButton_Click(object sender, EventArgs e)
        {
            intimidationPointsLabel.Text = (Int32.Parse(intimidationPointsLabel.Text) - 1).ToString();
            skillPointsChanged(1);
            if (Int32.Parse(intimidationPointsLabel.Text) == 0)
                intimidationMinusButton.Enabled = false;
             if(Int32.Parse(intimidationPointsLabel.Text)<3 
                && Int32.Parse(bonusPointsLabel.Text)>0)
                bonusIntimidationPlusButton.Enabled=true;
        }

        private void perceptionPlusButton_Click(object sender, EventArgs e)
        {
            perceptionPointsLabel.Text = (Int32.Parse(perceptionPointsLabel.Text) + 1).ToString();
            skillPointsChanged(-1);
            perceptionMinusButton.Enabled = true;
            if(Int32.Parse(perceptionPointsLabel.Text)>=3)
                bonusPerceptionPlusButton.Enabled=false;
        }

        private void perceptionMinusButton_Click(object sender, EventArgs e)
        {
            perceptionPointsLabel.Text = (Int32.Parse(perceptionPointsLabel.Text) - 1).ToString();
            skillPointsChanged(1);
            if (Int32.Parse(perceptionPointsLabel.Text) == 0)
                perceptionMinusButton.Enabled = false;
            if(Int32.Parse(perceptionPointsLabel.Text)<3 
                && Int32.Parse(bonusPointsLabel.Text)>0)
                bonusPerceptionPlusButton.Enabled=true;
        }

        private void persuasionPlusButton_Click(object sender, EventArgs e)
        {
            persuasionPointsLabel.Text = (Int32.Parse(persuasionPointsLabel.Text) + 1).ToString();
            skillPointsChanged(-1);
            persuasionMinusButton.Enabled = true;
            if(Int32.Parse(persuasionPointsLabel.Text)>=3)
                bonusPersuasionPlusButton.Enabled=false;
        }

        private void persuasionMinusButton_Click(object sender, EventArgs e)
        {
            persuasionPointsLabel.Text = (Int32.Parse(persuasionPointsLabel.Text) - 1).ToString();
            skillPointsChanged(1);
            if (Int32.Parse(persuasionPointsLabel.Text) == 0)
                persuasionMinusButton.Enabled = false;
             if(Int32.Parse(persuasionPointsLabel.Text)<3 
                && Int32.Parse(bonusPointsLabel.Text)>0)
                bonusPersuasionPlusButton.Enabled=true;
        }

        private void subterfugePlusButton_Click(object sender, EventArgs e)
        {
            subterfugePointsLabel.Text = (Int32.Parse(subterfugePointsLabel.Text) + 1).ToString();
            skillPointsChanged(-1);
            subterfugeMinusButton.Enabled = true;
            if(Int32.Parse(subterfugePointsLabel.Text)>=3)
                bonusSubterfugePlusButton.Enabled=false;
        }

        private void subterfugeMinusButton_Click(object sender, EventArgs e)
        {
            subterfugePointsLabel.Text = (Int32.Parse(subterfugePointsLabel.Text) - 1).ToString();
            skillPointsChanged(1);
            if (Int32.Parse(subterfugePointsLabel.Text) == 0)
                subterfugeMinusButton.Enabled = false;
             if(Int32.Parse(subterfugePointsLabel.Text)<3 
                && Int32.Parse(bonusPointsLabel.Text)>0)
                bonusSubterfugePlusButton.Enabled=true;
        }
        #endregion               

        #region BONUS POINTS BUTTONS
        private void bonusIntimidationPlusButton_Click(object sender, EventArgs e)
        {
            intimidationPointsLabel.Text = (Int32.Parse(intimidationPointsLabel.Text) + 1).ToString();
            bonusPointsChanged(-1);
            bonusIntimidationMinusButton.Enabled = true;
            if (Int32.Parse(intimidationPointsLabel.Text) >= 3)
                bonusIntimidationPlusButton.Enabled = false;
        }

        private void bonusIntimidationMinusButton_Click(object sender, EventArgs e)
        {
            intimidationPointsLabel.Text = (Int32.Parse(intimidationPointsLabel.Text) - 1).ToString();
            bonusPointsChanged(1);
            if (Int32.Parse(intimidationPointsLabel.Text) == 0)
                bonusIntimidationMinusButton.Enabled = false;
            if (Int32.Parse(intimidationPointsLabel.Text) < 3
               && Int32.Parse(bonusPointsLabel.Text) > 0)
                bonusIntimidationPlusButton.Enabled = true;
        }

        private void bonusPerceptionPlusButton_Click(object sender, EventArgs e)
        {
            perceptionPointsLabel.Text = (Int32.Parse(perceptionPointsLabel.Text) + 1).ToString();
            bonusPointsChanged(-1);
            bonusPerceptionMinusButton.Enabled = true;
            if (Int32.Parse(perceptionPointsLabel.Text) >= 3)
                bonusPerceptionPlusButton.Enabled = false;
        }

        private void bonusPerceptionMinusButton_Click(object sender, EventArgs e)
        {
            perceptionPointsLabel.Text = (Int32.Parse(perceptionPointsLabel.Text) - 1).ToString();
            bonusPointsChanged(1);
            if (Int32.Parse(perceptionPointsLabel.Text) == 0)
                bonusPerceptionMinusButton.Enabled = false;
            if (Int32.Parse(perceptionPointsLabel.Text) < 3
               && Int32.Parse(bonusPointsLabel.Text) > 0)
                bonusPerceptionPlusButton.Enabled = true;
        }

        private void bonusPersuasionPlusButton_Click(object sender, EventArgs e)
        {
            persuasionPointsLabel.Text = (Int32.Parse(persuasionPointsLabel.Text) + 1).ToString();
            bonusPointsChanged(-1);
            bonusPersuasionMinusButton.Enabled = true;
            if (Int32.Parse(persuasionPointsLabel.Text) >= 3)
                bonusPersuasionPlusButton.Enabled = false;
        }

        private void bonusPersuasionMinusButton_Click(object sender, EventArgs e)
        {
            persuasionPointsLabel.Text = (Int32.Parse(persuasionPointsLabel.Text) - 1).ToString();
            bonusPointsChanged(1);
            if (Int32.Parse(persuasionPointsLabel.Text) == 0)
                bonusPersuasionMinusButton.Enabled = false;
            if (Int32.Parse(persuasionPointsLabel.Text) < 3
                && Int32.Parse(bonusPointsLabel.Text) > 0)
                bonusPersuasionPlusButton.Enabled = true;
        }

        private void bonusSubterfugePlusButton_Click(object sender, EventArgs e)
        {
            subterfugePointsLabel.Text = (Int32.Parse(subterfugePointsLabel.Text) + 1).ToString();
            bonusPointsChanged(-1);
            bonusSubterfugeMinusButton.Enabled = true;
            if (Int32.Parse(subterfugePointsLabel.Text) >= 3)
                bonusSubterfugePlusButton.Enabled = false;
        }

        private void bonusSubterfugeMinusButton_Click(object sender, EventArgs e)
        {
            subterfugePointsLabel.Text = (Int32.Parse(subterfugePointsLabel.Text) - 1).ToString();
            bonusPointsChanged(1);
            if (Int32.Parse(subterfugePointsLabel.Text) == 0)
                bonusSubterfugeMinusButton.Enabled = false;
            if (Int32.Parse(subterfugePointsLabel.Text) < 3
               && Int32.Parse(bonusPointsLabel.Text) > 0)
                bonusSubterfugePlusButton.Enabled = true;
        }

        private void fortitudePlusButton_Click(object sender, EventArgs e)
        {
            fortitudePointsLabel.Text = (Int32.Parse(fortitudePointsLabel.Text) + 1).ToString();
            bonusPointsChanged(-1);
            fortitudeMinusButton.Enabled = true;
        }

        private void fortitudeMinusButton_Click(object sender, EventArgs e)
        {
            fortitudePointsLabel.Text = (Int32.Parse(fortitudePointsLabel.Text) - 1).ToString();
            bonusPointsChanged(1);
            if (Int32.Parse(fortitudePointsLabel.Text) == 2)
                fortitudeMinusButton.Enabled = false;
        }

        private void selfControlPlusButton_Click(object sender, EventArgs e)
        {
            selfControlPointsLabel.Text = (Int32.Parse(selfControlPointsLabel.Text) + 1).ToString();
            bonusPointsChanged(-1);
            selfControlMinusButton.Enabled = true;
        }

        private void selfControlMinusButton_Click(object sender, EventArgs e)
        {
            selfControlPointsLabel.Text = (Int32.Parse(selfControlPointsLabel.Text) - 1).ToString();
            bonusPointsChanged(1);
            if (Int32.Parse(selfControlPointsLabel.Text) == 2)
                selfControlMinusButton.Enabled = false;
        }

        private void resistancePlusButton_Click(object sender, EventArgs e)
        {
            resistancePointsLabel.Text = (Int32.Parse(resistancePointsLabel.Text) + 4).ToString();
            bonusPointsChanged(-1);
            resistanceMinusButton.Enabled = true;
        }

        private void resistanceMinusButton_Click(object sender, EventArgs e)
        {
            resistancePointsLabel.Text = (Int32.Parse(resistancePointsLabel.Text) - 4).ToString();
            bonusPointsChanged(1);
            if (Int32.Parse(resistancePointsLabel.Text) == 12)
                resistanceMinusButton.Enabled = false;
        }
        #endregion

        private void saveCharacterButton_Click(object sender, EventArgs e)
        {           
            //Perform Checks
            if (nameTextBox.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("ENTER A NAME");
                return;
            }
            if (skillPointsLabel.Text != "0")
            {
                System.Windows.Forms.MessageBox.Show("YOU HAVE SKILL POINTS LEFT");
                return;
            }
            if (bonusPointsLabel.Text != "0")
            {
                System.Windows.Forms.MessageBox.Show("YOU HAVE BONUS POINTS LEFT");
                return;
            }

            int toneValue = this.toneComboBox.SelectedIndex;

            Dictionary<String, Double> skills = new Dictionary<string, double>()
            {
                {"Persuasion",Double.Parse(this.persuasionPointsLabel.Text)},
                {"Intimidation",Double.Parse(this.intimidationPointsLabel.Text)},
                {"Perception",Double.Parse(this.perceptionPointsLabel.Text)},
                {"Subterfuge",Double.Parse(this.subterfugePointsLabel.Text)},
            };

            Dictionary<String, Double> stats = new Dictionary<string, double>()
            {
                {"Self Control",Double.Parse(this.selfControlPointsLabel.Text)},
                {"Fortitude",Double.Parse(this.fortitudePointsLabel.Text)},
                {"Resistance",Double.Parse(this.resistancePointsLabel.Text)},
                {"State of Mind Anger",2.0},
                {"State of Mind Joy",2.0},
                {"Tone",toneValue}
            };

            if (asDialog)
            {
                character = new CharacterData() { MySkills = skills, MyStats = stats, Name = nameTextBox.Text };
                this.Close();
            }
            else
                myController.saveCharacter(nameTextBox.Text, stats, skills, null);

        }

        private void form_Closed(object sender, EventArgs e)
        {
            if (!asDialog)
                ((IDisputeViewController)myController).formClosed("Close");
        }

        public void SaveCharacterToFile(XmlNode characterNode,string characterName,XmlDocument creatorDoc)
        {
            string fileName = characterName;
            if (!Directory.Exists("../DisputeCharacters"))//make sure directory exists
                Directory.CreateDirectory("../DisputeCharacters");
            fileName = fileName.Insert(0, "../DisputeCharacters/");                             //Put in correct directory
            fileName = fileName.EndsWith(".sdc") ? fileName : fileName + ".sdc";    //Make sure filename has .xml extension

            XmlWriter myWriter = new XmlTextWriter(fileName, null);
            myWriter.WriteStartElement("Root");
            myWriter.Close();

            creatorDoc.Load(fileName);

            //Create nodes
            XmlNode root = creatorDoc.DocumentElement;

            //Join nodes
            root.AppendChild(characterNode);

            creatorDoc.Save(fileName);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

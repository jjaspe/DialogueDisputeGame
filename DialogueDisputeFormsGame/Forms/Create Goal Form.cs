using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DisputeCommon;

namespace DialogueDisputeGameClient.Forms
{
    public partial class Create_Goal_Form : Form
    {
        public string propertyName="Resistance";
        public double propertyValue=0;
        public GoalTypes type=GoalTypes.ReachValue;

        public Create_Goal_Form()
        {
            InitializeComponent();
        }

        private void radReachValue_CheckedChanged(object sender, EventArgs e)
        {
            if(radReachValue.Checked)
                type = GoalTypes.ReachValue;
        }

        private void radAboveValue_CheckedChanged(object sender, EventArgs e)
        {
            if (radAboveValue.Checked)
                type = GoalTypes.StayAbove;
        }

        private void radBelowValue_CheckedChanged(object sender, EventArgs e)
        {
            if (radBelowValue.Checked)
                type = GoalTypes.StayBelow;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            propertyName = txtPropertyName.Text;
            propertyValue = (double)nmrValue.Value;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

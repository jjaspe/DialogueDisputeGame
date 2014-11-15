using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DisputeCommon;

namespace DialogueDisputeFormsGame.Forms
{
    public partial class SoMSelectorForm : Form
    {
        SOM selected=SOM.Joy,allowed;
        Result result;

        public SOM Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public SoMSelectorForm(SOM allowed, Result result)
        {
            InitializeComponent();
            this.allowed = allowed;
            this.result = result;
            setAllowed(allowed);
            setLabel(result);
        }


        //Control Events
        private void button1_Click(object sender, EventArgs e)
        {           
            this.Close();
        }

        private void SoMJoyRadio_CheckedChanged(object sender, EventArgs e)
        {
            Selected = SOM.Joy;
        }

        private void SoMSorrowRadio_CheckedChanged(object sender, EventArgs e)
        {
            Selected = SOM.Sorrow;
        }

        private void SoMAngerRadio_CheckedChanged(object sender, EventArgs e)
        {
            Selected = SOM.Anger;
        }

        private void SoMFearRadio_CheckedChanged(object sender, EventArgs e)
        {
            Selected = SOM.Fear;
        }

        
        void setLabel(Result result)
        {
            resultLabel.Text = "Roll Result:"+result.ToString();
        }        

        void setAllowed(SOM allowed)
        {
            foreach (SOM value in Enum.GetValues(typeof(SOM)))
            {
                if(allowed.HasFlag(value))
                {
                    switch (value)
                    {
                        case SOM.Joy:
                            SoMJoyRadio.Enabled = true;
                            break;
                        case SOM.Anger:
                            SoMAngerRadio.Enabled = true;
                            break;
                        case SOM.Fear:
                            SoMFearRadio.Enabled = true;
                            break;
                        case SOM.Sorrow:
                            SoMSorrowRadio.Enabled = true;
                            break;
                    }
                }
            }
        }
    }
}

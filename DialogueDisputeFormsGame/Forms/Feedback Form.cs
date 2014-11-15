using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DialogueDisputeFormsGame.Forms
{
    public partial class FeedbackForm : Form
    {
        public FeedbackForm()
        {
            InitializeComponent();
        }
        public void updateFeedback(string feedback)
        {
            this.richTextBox1.Text = feedback;
        }
    }
}

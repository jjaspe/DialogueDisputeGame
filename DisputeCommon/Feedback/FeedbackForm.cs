using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DisputeCommon
{
    public partial class FormFeedback : Form,IFeedbackWriter
    {
        public FormFeedback()
        {
            InitializeComponent();
            this.Show();
        }

        void IFeedbackWriter.WriteLine(string line)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtFeedback.Text = line + txtFeedback.Text;
            });
        }
    }
}

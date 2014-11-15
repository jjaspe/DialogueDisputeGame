using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DisputeCommon;

namespace ListViewTester
{
    public partial class ListFomr : Form
    {
        List<Match> matches = new List<Match>();
        public ListFomr()
        {
            
            InitializeComponent();
            this.listBox1.DisplayMember = "MatchName";
            this.listBox1.DataSource = matches;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            matches.Clear();
            matches.Add(new Match() { Player1 = new DataPlayer() { PlayerName = "Juan" } });

            matches.Add(new Match() { Player1 = new DataPlayer() { PlayerName = "Carmen" } });
            this.listBox1.DataSource = matches;
            ((CurrencyManager)this.listBox1.BindingContext[this.listBox1.DataSource]).Refresh();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DisputeCommon
{
    public partial class SoMGrid : UserControl
    {
        private Panel[][] myPanels;
        private int soMAnger;
        public int SoMAnger
        {
            get { return soMAnger; }
            set
            {
                soMAnger = value;
                this.updateControl();
            }
        }
        private int soMJoy;
        public int SoMJoy
        {
            get { return soMJoy; }
            set { soMJoy = value; this.updateControl(); }
        }

        public SoMGrid()
        {
            InitializeComponent();
            fillPanels();
            recolorAll();

        }

        private void fillPanels()
        {
            string name;
            int x, y;
            myPanels=new Panel[5][];
            for (int i = 0; i < myPanels.Length; i++)
                myPanels[i] = new Panel[5];

            foreach (Control cl in this.Controls)
            {
                if (cl is Panel)
                {
                    Panel lb = (Panel)cl;
                    name = lb.Name;
                    name = name.TrimStart(new char[] { 'S', 'o', 'M' });
                    x = Int32.Parse(name[0].ToString());
                    y = Int32.Parse(name[1].ToString());
                    myPanels[x][y] = lb;
                }
            }
            
        }
        public void updateControl()
        {
            if (SoMAnger < 5 && SoMJoy < 5)
            {
                recolorAll();
                if(SoMAnger==2&& SoMJoy==2)//Tranquility
                    myPanels[SoMJoy][SoMAnger].BackColor
                        = System.Drawing.Color.Yellow ;
                else
                    myPanels[SoMJoy][SoMAnger].BackColor
                        = System.Drawing.Color.Red;
            }
        }       
        public void recolorAll()
        {
            foreach (Panel[] lbs in myPanels)
            {
                foreach (Panel lb in lbs)
                    lb.BackColor = System.Drawing.Color.White;
            }            
        }
       
    }
}

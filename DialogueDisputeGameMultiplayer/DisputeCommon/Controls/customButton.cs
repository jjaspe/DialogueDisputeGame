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
    public partial class customButton :Button
    {
        public customButton()
        {
            InitializeComponent();
        }
        public void setToolTipFromTag()
        {
            ToolTip toolTip = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;

            toolTip.SetToolTip(this, this.Tag.ToString());
        }
        public void setBackgroundColors(Color c1, Color c2)
        {
            this.BackgroundImage = createBackgroundImage(c1, c2);
        }
        public void setBackgroundColors(float[] c1, float[] c2)
        {
            setBackgroundColors(Color.FromArgb((int)(c1[0] * 255), (int)(c1[1] * 255),(int)( c1[2] * 255)), Color.FromArgb((int)(c2[0] * 255), (int)(c2[1] * 255),(int)( c2[2] * 255)));
        }
        Bitmap createBackgroundImage(Color c1, Color c2)
        {
            Bitmap b = new Bitmap(this.Width, this.Height);
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    if (i < (-j*2*this.Height/this.Width+3*this.Height/2))
                        b.SetPixel(j, i, c1);
                    else
                        b.SetPixel(j, i, c2);
                }
            }
            return b;
        }
    }
}

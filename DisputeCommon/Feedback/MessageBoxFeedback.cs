using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DisputeCommon
{
    public class MessageBoxFeedback:TextWriter
    {
        public override Encoding Encoding
        {
            get { throw new NotImplementedException(); }
        }

        public override void WriteLine(String txt)
        {
            MessageBox.Show(txt);
        }
    }
}

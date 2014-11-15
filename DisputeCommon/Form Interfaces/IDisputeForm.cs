using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public interface IDisputeViewController
    {
        void start();
        void stop();
        void formClosed(string code);
    }
}

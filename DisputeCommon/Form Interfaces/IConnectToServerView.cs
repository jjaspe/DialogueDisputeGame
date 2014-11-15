using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public interface IConnectToServerView
    {
        void start();
        void stop();
        void setController(IConnectToServerController controller);
    }
}

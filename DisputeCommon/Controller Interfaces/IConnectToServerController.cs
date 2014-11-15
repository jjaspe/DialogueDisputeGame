using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public interface IConnectToServerController
    {
        void MessageSentFromView(Messages.LobbyViewMessage code, List<object> args, object sender);
        void setView(IConnectToServerView view);
    }
}

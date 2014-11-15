using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeCommon;

namespace DisputeCommon
{
    public interface IServerViewController
    {
        void MessageSentFromView(Messages.LobbyViewMessage message, List<object> data);
        IServerView View
        {
            get;
            set;
        }
        IFeedbackWriter FeedbackWriter
        {
            get;
            set;
        }
    }
}

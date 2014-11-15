using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace DisputeCommon
{
    public interface IClientConnectionManager
    {        
        void parseRequest(Messages.GameMessages header, List<object> args, object sender);
        bool isConnected();
        IFeedbackWriter FeedbackWriter
        {
            get;
            set;
        }
        List<Match> Matches
        {
            get;
        }
        List<DataPlayer> Players
        {
            get;
        }
        
    }
}

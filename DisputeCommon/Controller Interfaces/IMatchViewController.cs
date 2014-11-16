using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public enum MatchViewMessage { Action, Close, SoM, Update, GameOver }
    public interface IMatchController: IFeedbackWriter,IDisputeViewController
    {
        void MessageSentFromView(MatchViewMessage code, List<object> args,object sender);
        IClientConnectionManager dataManager 
        {
            get;
            set;
        }
        Match Match
        {
            get;
            set;
        }
        bool IsPlayerOne
        {
            get;
        }
        DataPlayer Player
        {
            get;
            set;
        }

    }
}

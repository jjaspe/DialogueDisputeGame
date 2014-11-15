using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public interface IMatchView:IFeedbackWriter
    {
        void start();
        void stop();
        void updateMatch(Match match);
        SOM selectSoM(SOM allowed, Result result);
        IMatchController Controller
        {
            get;
            set;
        }

        void gameOver(bool won);
    }
}

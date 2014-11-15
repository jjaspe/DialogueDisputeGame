using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public interface IServerView:IFeedbackWriter
    {
        void updatePlayers(List<string> playerNames);
        void updateGames(List<string> gameNames);
        void updateFeedback(string feedback);
        void clearFeedback();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeCommon;

namespace DialogueDisputeGameServer.Game_Interfaces
{
    public interface IMatchController:IGameObserver
    {
        void MessageSentFromView(string code, object args,object sender);
        object GetPropertyValue(string code,object sender);
        string getFeedback();
        int getTurn();
    }
}

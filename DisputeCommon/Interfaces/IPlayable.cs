using System;
using System.Collections.Generic;

namespace DisputeCommon
{
    public interface IPlayable
    {
        IFeedbackWriter FeedbackWritter { get; set; }
        void getMessageFromConnection(Messages.GameMessages msg, DataPlayer player,List<object> data);
        Match Match { get; set; }
        DataPlayer Player1 { get; set; }
        DataPlayer Player2 { get; set; }
        int Turn { get; set; }
        CharacterData World { get; set; }
    }
}

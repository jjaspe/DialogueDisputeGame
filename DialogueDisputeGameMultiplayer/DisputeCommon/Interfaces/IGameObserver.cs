using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeCommon;

namespace DisputeCommon
{
    public interface IGameObserver
    {
        void update(DataPlayer player,Messages.GameMessages msg, List<object> data);
    }
}

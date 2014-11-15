using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeCommon;

namespace DisputeCommon
{
    public interface IGameObservable
    {
        void registerObserver(IGameObserver newObserver);
        void removeObserver(IGameObserver newObserver);
        void notifyObservers(Messages.GameMessages msg, DataPlayer player, List<object> data);
    }
}

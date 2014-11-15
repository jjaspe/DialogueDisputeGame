using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogueCommon.Interfaces
{
    public interface IConnectionObservable
    {
        void registerObserver(IConnectionObserver newObserver);
        void removeObserver(IConnectionObserver newObserver);
        void notifyObservers(object code = null);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogueCommon.Interfaces
{
    public interface IDisputeObservable
    {
        void registerObserver(object newObserver);
        void removeObserver(object newObserver);
        void notifyObservers(object code=null);
    }
}

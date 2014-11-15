using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogueCommon.Interfaces
{
    public interface IConnectionObserver
    {
        void update(object code);
    }
}

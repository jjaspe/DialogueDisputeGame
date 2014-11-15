using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogueCommon.Interfaces
{
    public interface IControllerObserver
    {
        void update(object code);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeCommon;
using DisputeCommon.Feedback;


namespace DisputeCommon
{
    public delegate ArgumentFeedback SoMAcquiredHandler(int JoySorrowValue,int AngerFearValue);
   
    public interface ISoMSelector
    {        
        void acquireSoM(List<string> allowed,Result result);
        void acquireSoM(SOM allowed, Result result);
        void setAcquiredHandler(SoMAcquiredHandler handler);
    }
}

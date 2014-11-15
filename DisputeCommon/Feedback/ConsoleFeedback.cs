using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public class ConsoleFeedback:IFeedbackWriter
    {
         void IFeedbackWriter.WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}

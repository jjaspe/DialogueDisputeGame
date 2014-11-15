using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public interface ICreateCharacterController
    {
         void saveCharacter(string name,Dictionary<String,Double> stats,Dictionary<String,Double> skills,
             Dictionary<String,Double> atts);
        
    }
}

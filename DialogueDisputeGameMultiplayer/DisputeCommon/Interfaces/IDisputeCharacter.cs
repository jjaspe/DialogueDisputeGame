using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public interface IDisputeCharacter
    {
        double getStat(string name);
        double getSkill(string name);
        string getName();
        double getAttribute(string name);
        String toString();
    }
}

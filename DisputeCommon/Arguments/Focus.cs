using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using DisputeCommon;
using DisputeCommon.Feedback;

namespace DisputeCommon.Arguments
{
    /// <summary>
    /// Focus: No roll. The Speaker remains silent and shifts their own state of mind in any direction they want by a number equal to their current Self-control value. This argument is immediately noticeable by the players, unlike all other arguments.
    /// </summary>
    public class Focus:SoMDependentArgument
    {
        public Focus()
            : base()
        {
            IsSubterfugeArgument = false;
            RollBehavior = new AlwaysSuccessRollBehavior();
            allowed = (SOM.Joy | SOM.Anger | SOM.Fear | SOM.Sorrow);
            myBehavior = new SoMArgumentBehavior();
        }

        public override void reset(CharacterData attacker, CharacterData defender, CharacterData world)
        {
            attackerSuccessValue.Numerator = attacker.MyStats["Self Control"];
        }
        public override string ToString()
        {
            return "Focus";
        }
    }
}

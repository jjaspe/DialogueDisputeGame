using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;

namespace DisputeCommon.Arguments
{
    /// <summary>
    /// Scare: Intimidation Vs Self-control. If successful, Target’s SoM shifts 1 in the direction of Fear. Great success increases the shift to 2. Hidden Roll.
    /// </summary>
    public class Scare:Argument
    {
        public Scare()
            : base()
        {
            IsSubterfugeArgument = false;

            this.attackerCheckProperty = "Intimidation";
            this.defenderCheckProperty = "Self Control";

            this.defenderAffectedPropertySuccess = "StateOfMindAngerFear";
            this.defenderAffectedPropertyGreatSuccess = "StateOfMindAngerFear";

            this.defenderSuccessValue = new Factor() { Numerator = -1 };
            this.defenderGreatSuccessValue = new Factor() { Numerator = -2 };

            RollBehavior = new NormalRollBehavior();
        }

        public override string ToString()
        {
            return "Scare";
        }

    }
}

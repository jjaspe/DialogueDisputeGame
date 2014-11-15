using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;

namespace DisputeCommon.Arguments
{
    /// <summary>
    /// Convince: Persuasion Vs Fortitude. If successful reduces the Target’s Resistance by 1d6. A great success gives a bonus of + 2 to the 1d6 roll, even if the total is over six. Failure drops Tone by 1.
    /// </summary>
    public class Convince:Argument
    {
        public Convince()
            : base()
        {
            IsSubterfugeArgument = false;

            attackerCheckProperty = "Persuasion";
            defenderCheckProperty = "Fortitude";
            this.defenderAffectedPropertySuccess = "Resistance";
            this.defenderAffectedPropertyGreatSuccess = "Resistance";

            defenderSuccessValue = new Factor() { NumberOfDice = -1, DiceType = 6 };
            defenderGreatSuccessValue = new Factor() { Numerator = -2, NumberOfDice = -1, DiceType = 6 };

            toneShiftFailureValue = new Factor() { Numerator = 1 };

            RollBehavior = new NormalRollBehavior();
        }
        public override string ToString()
        {
            return "Convince";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;

namespace DisputeCommon.Arguments
{
    /// <summary>
    /// Coerce: Intimidation Vs Fortitude. A success reduces Target’s Resistance by 1d6 while a great success gives a bonus of + 2 to the 1d6 roll, even if the total is six. Regardless of result decrease Tone by 1.
    /// </summary>
    public class Coerce:Argument
    {
        public Coerce()
            : base()
        {
            IsSubterfugeArgument = false;

            this.attackerCheckProperty = "Intimidation";
            this.defenderCheckProperty = "Fortitude";

            this.defenderAffectedPropertySuccess = "Resistance";
            this.defenderAffectedPropertyGreatSuccess = "Resistance";

            this.defenderSuccessValue = new Factor() { NumberOfDice = -1, DiceType = 6 };
            this.defenderGreatSuccessValue = new Factor() {Numerator=-2, NumberOfDice = -1, DiceType = 6 };

            this.toneShiftFailureValue = new Factor() { Numerator = 1 };
            this.toneShiftSuccessValue = new Factor() { Numerator = 1 };
            this.toneShiftGreatSuccessValue = new Factor() { Numerator = 1 };

            RollBehavior = new NormalRollBehavior();
        }

        public override string ToString()
        {
            return "Coerce";
        }
    }
}

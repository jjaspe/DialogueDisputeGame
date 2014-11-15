using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;


namespace DisputeCommon.Arguments
{
    /// <summary>
    /// Bluff: Subterfuge Vs Perception. A success allows you to recover 1d6 of Resistance, 1d6 + 2 with a great success. Failure gives a – 1 penalty to all of Speaker’s next Subterfuge Arguments. This penalty can accumulate. Hidden Roll.
    /// </summary>
    public class Bluff:Argument
    {
        public Bluff()
            : base()
        {
            IsSubterfugeArgument = true;

            this.attackerCheckProperty = "Subterfuge";
            this.defenderCheckProperty = "Perception";

            this.attackerAffectedPropertySuccess = "Resistance";
            this.attackerAffectedPropertyGreatSuccess = "Resistance";
            this.attackerAffectedPropertyFailure = "subterfugeBonus";

            this.attackerSuccessValue = new Factor() { DiceType = 6, NumberOfDice = 1 };
            this.attackerGreatSuccessValue = new Factor() {Numerator=2, DiceType = 6, NumberOfDice = 1 };
            this.attackerFailureValue = new Factor() { Numerator = -1 };

            RollBehavior = new NormalRollBehavior();

        }
        public override string ToString()
        {
            return "Bluff";
        }
    }
}

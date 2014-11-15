using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using DisputeCommon;

namespace DisputeCommon.Arguments
{
    /// <summary>
    /// Trick: Subterfuge Vs Perception. A success allows you to do another non-subterfuge based argument on the same turn with a + 2 bonus to the roll, with a great success increasing it to + 3. Failure drops tone by 1 and gives a -1 penalty to all Speaker’s further subterfuge Arguments. This penalty can accumulate. Hidden Roll.
    /// </summary>
    public class Trick:Argument
    {
        public Trick()
            : base()
        {
            IsSubterfugeArgument = true;

            this.attackerCheckProperty = "Subterfuge";
            this.defenderCheckProperty = "Perception";

            this.attackerAffectedPropertySuccess = "nonSubterfugeBonus";
            this.attackerAffectedPropertyGreatSuccess = "nonSubterfugeBonus";
            this.attackerAffectedPropertyFailure = "subterfugePenalty";

            this.attackerFailureValue = new Factor() { Numerator = 1 };
            this.attackerSuccessValue = new Factor() { Numerator = 2 };
            this.attackerGreatSuccessValue = new Factor() { Numerator = 3 };

            this.toneShiftFailureValue = new Factor() { Numerator = 1 };
            RollBehavior = new NormalRollBehavior();
        }

        public bool repeatTurn()
        {
            return result == Result.Success || result== Result.GreatSuccess;
        }

        public override string ToString()
        {
            return "Trick";
        }
    }
}

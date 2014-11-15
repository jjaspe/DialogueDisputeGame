using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using DisputeCommon;

namespace DisputeCommon.Arguments
{
    /// <summary>
    /// Charm: Persuasion Vs Fortitude. If successful, then Tone increases by 2. Great success increases Tone by 3 instead.
    /// </summary>
    public class Charm:Argument
    {
        public Charm()
            : base()
        {
            IsSubterfugeArgument = false;

            attackerCheckProperty = "Persuasion";
            defenderCheckProperty = "Fortitude";

            toneShiftSuccessValue = new Factor() { Numerator = -2 };
            toneShiftGreatSuccessValue = new Factor() { Numerator = -3 };

            RollBehavior = new NormalRollBehavior();
        }

        public override string ToString()
        {
            return "Charm";
        }
    }
}

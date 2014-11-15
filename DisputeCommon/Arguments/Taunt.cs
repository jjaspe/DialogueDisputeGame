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
    /// Taunt: Intimidation Vs Self-control. If successful then Target’s SoM shifts 1 in the direction of Anger or Sorrow (Speaker’s choice). A great success increases the shift to 2. Regardless of result, Tone decreases by 2. Hidden Roll.
    /// </summary>
    public class Taunt:SoMDependentArgument
    {
        public Taunt()
            : base()
        {
            IsSubterfugeArgument = false;
            allowed = (SOM.Anger | SOM.Sorrow);
            this.attackerCheckProperty = "Intimidation";
            this.defenderCheckProperty = "Self Control";
            reset(null,null,null);
            myBehavior = new SoMArgumentBehavior();
        }

        public override void reset(CharacterData attacker, CharacterData defender, CharacterData world)
        {
            this.defenderSuccessValue = new Factor() { Numerator = 1 };
            this.defenderGreatSuccessValue = new Factor() { Numerator = 2 };

            toneShiftFailureValue = new Factor() { Numerator = 2 };
            toneShiftGreatSuccessValue = new Factor() { Numerator = 2 };
            toneShiftSuccessValue = new Factor() { Numerator = 2 };

            RollBehavior = new NormalRollBehavior();
        }

        public override string ToString()
        {
            return "Taunt";
        }
    }
}

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
    /// Manipulate: Persuasion Vs Self-control. If successful, then Target’s SoM shifts 1 in any direction chosen by the Speaker. Great success increases the shift to 2. Hidden Roll.
    /// </summary>
    public class Manipulate:SoMDependentArgument
    {
        public Manipulate()
            : base()
        {
            IsSubterfugeArgument = false;
            allowed = (SOM.Joy | SOM.Anger | SOM.Fear | SOM.Sorrow); 
            this.attackerCheckProperty = "Persuasion";
            this.defenderCheckProperty = "Self Control";
            myBehavior=new SoMArgumentBehavior();
            RollBehavior = new NormalRollBehavior();
            reset(null,null,null);            
        }

        public override void reset(CharacterData attacker, CharacterData defender, CharacterData world)
        {
            this.DefenderAffectedPropertySuccess = "";
            this.DefenderAffectedPropertyGreatSuccess = "";

            this.defenderSuccessValue = new Factor() { Numerator = 1, Denominator = 1, NumberOfDice = 0 };
            this.defenderGreatSuccessValue = new Factor() { Numerator = 2, Denominator = 1, NumberOfDice = 0 };
        }

        public override string ToString()
        {
            return "Manipulate";
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using DisputeCommon;

namespace DisputeCommon.Arguments
{
    /// <summary>
    /// Analyze: Perception Vs Subterfuge. Success allows the Speaker to discover one of the following things about the target: their current SoM, their four skill levels or their Self-control, Fortitude and Resistance values. If you achieve a great Success you gain a +1 bonus on all arguments for the rest of the battle, but you can only get this bonus once. Hidden Roll.
    /// </summary>
    public class Analyze:Argument
    {
        public Analyze()
            : base()
        {
            IsSubterfugeArgument = false;

            this.attackerCheckProperty = "Perception";
            this.defenderCheckProperty = "Subterfuge";

            this.defenderAffectedPropertyGreatSuccess = "analyzeBonus";
            this.defenderGreatSuccessValue = new Factor() { Numerator = 1 };
            
        }

        public bool findOutStuff()
        {
            return result == Result.Success || result == Result.GreatSuccess;
        }

        public override string ToString()
        {
            return "Analyze";
        }
    }
}

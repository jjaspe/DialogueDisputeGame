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
    /// Empathy: Persuasion Vs -. Empathy only works if Speaker and Target have same emotions in their SoM (intensity is irrelevant). Success reduces target’s resistance by 1d6, and a great success gives you a + 2 bonus to the roll even if the total surpasses six. 
    /// </summary>
    public class Empathy:Argument
    {
        public bool passedCondition = false;
        public Empathy()
            : base()
        {
            IsSubterfugeArgument = false;

            this.attackerCheckProperty = "Persuasion";
            

            this.defenderAffectedPropertySuccess = "Resistance";
            this.defenderAffectedPropertyGreatSuccess = "Resistance";

            defenderSuccessValue = new Factor() { NumberOfDice = -1, DiceType = 6 };
            defenderGreatSuccessValue = new Factor() { Numerator = -2, NumberOfDice = -1, DiceType = 6 };

            RollBehavior = new EmpathyRollBehavior();
        }


        public override string ToString()
        {
            return "Empathy";
        }
    }
}

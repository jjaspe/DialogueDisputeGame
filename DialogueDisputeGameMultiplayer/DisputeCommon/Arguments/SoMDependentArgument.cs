using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeCommon.Feedback;
using CharacterSystemLibrary.Classes;

namespace DisputeCommon.Arguments
{
    public abstract class SoMDependentArgument:Argument
    {
        protected SoMArgumentBehavior myBehavior;
        public SOM allowed;
        public ISoMSelector Selector
        {
            get { return selector; }
            set
            {
                selector = value;
                selector.setAcquiredHandler(SoMSelected);
            }
        }
        ISoMSelector selector;

        public new ArgumentFeedback doArgument(CharacterData attacker, CharacterData defender, CharacterData World)
        {
            return myBehavior.doArgument(this, attacker, defender, World);
        }
        public abstract void reset(CharacterData attacker,CharacterData defender,CharacterData world);
        public ArgumentFeedback SoMSelected(int jsValue, int afValue)
        {
            return myBehavior.SoMSelected(this, jsValue, afValue);
        }
        public abstract override string ToString();
    }

    public class SoMArgumentBehavior
    {

        public ArgumentFeedback SoMSelected(SoMDependentArgument arg,int jsValue, int afValue)
        {

            if (jsValue != 0)//Chose js SoM
            {
                arg.DefenderAffectedPropertySuccess = "StateOfMindJoySorrow";
                arg.DefenderAffectedPropertyGreatSuccess = "StateOfMindJoySorrow";
                arg.AttackerAffectedPropertySuccess = "StateOfMindJoySorrow";
                arg.AttackerAffectedPropertyGreatSuccess = "StateOfMindJoySorrow";
                if (jsValue < 0)//Go back so multiply value by -1
                {
                    arg.DefenderSuccessValue.Numerator *= -1;
                    arg.DefenderGreatSuccessValue.Numerator *= -1;
                    arg.AttackerSuccessValue.Numerator *= -1;
                    arg.AttackerGreatSuccessValue.Numerator *= -1;
                }
            }
            else
            {
                arg.DefenderAffectedPropertySuccess = "StateOfMindAngerFear";
                arg.DefenderAffectedPropertyGreatSuccess = "StateOfMindAngerFear";
                arg.AttackerAffectedPropertySuccess = "StateOfMindAngerFear";
                arg.AttackerAffectedPropertyGreatSuccess = "StateOfMindAngerFear";
                if (afValue < 0)//Go back so multiply value by -1
                {
                    arg.DefenderSuccessValue.Numerator *= -1;
                    arg.DefenderGreatSuccessValue.Numerator *= -1;
                    arg.AttackerSuccessValue.Numerator *= -1;
                    arg.AttackerGreatSuccessValue.Numerator *= -1;
                }
            }

            arg.doEffects();
            return arg.ArgFeedback;

        }


        public ArgumentFeedback doArgument(SoMDependentArgument arg, CharacterData attacker, CharacterData defender, CharacterData world)
        {
            if (!Argument.checkCharactersValid(attacker, defender))
                return null;            

            //Reset feedback atts
            arg.ArgFeedback = new ArgumentFeedback();
            arg.ArgFeedback.argumentName = arg.ToString();
            arg.reset(attacker,defender,world);//Reset this so old values of numerator and property names are deleted
            
            //Set Characters
            arg.Attacker = attacker;
            arg.Defender = defender;
            arg.World = world;

            arg.result = arg.RollBehavior.doRoll(arg, attacker, defender, world, arg.ArgFeedback);

            if(arg.result==Result.Failure)
            {
                arg.ArgFeedback.result = arg.result;
                arg.doEffects();
                return arg.ArgFeedback;
            }
            else
                arg.Selector.acquireSoM(arg.allowed, arg.result);

            return null;
        }

    }

    public interface IRollBehavior
    {
        Result doRoll(Argument arg, CharacterData attacker, CharacterData defender,CharacterData world,
            ArgumentFeedback argFeedback);
    }

    public class NormalRollBehavior : IRollBehavior
    {

        public Result doRoll(Argument arg,CharacterData attacker,CharacterData defender,CharacterData world,
            ArgumentFeedback argFeedback)
        {
            Factor rollFactor = new Factor() { NumberOfDice = 2, DiceType = 6 };
            Result result;
            double subBonus = attacker.getProperty( "subterfugeBonus"), nonSubBonus = attacker.getProperty( "nonSubterfugeBonus"), analyzeBonus = attacker.getProperty( "analyzeBonus"),subPenalty=attacker.getProperty("subterfugePenalty");
            double defValue = defender.getProperty(arg.DefenderCheckProperty), attValue = attacker.getProperty(arg.AttackerCheckProperty);


            //Check bonuses
            if (arg.IsSubterfugeArgument)
                rollFactor.Numerator = subBonus-subPenalty;
            else
                rollFactor.Numerator = nonSubBonus;
            //keep at 1
            analyzeBonus = analyzeBonus >= 1 ? 1 : 0;
            rollFactor.Numerator += analyzeBonus;

            //Add Roll
            attValue += Argument.doRoll(rollFactor);

            argFeedback.rollResult = attValue;

            if (Double.IsNaN(defValue))
                defValue = 0;

            //Perform check
            if (attValue - defValue > 8)//Great success
            {
                result = Result.GreatSuccess;
            }
            else if (attValue - defValue > 6)//Success 
            {
                result = Result.Success;
            }
            else//Failure
            {
                result = Result.Failure;
            }
            argFeedback.result = result;
            return result;
        }
    }

    public class AlwaysSuccessRollBehavior : IRollBehavior
    {
        public Result doRoll(Argument arg, CharacterData attacker, CharacterData defender, CharacterData world, ArgumentFeedback argFeedback)
        {
            argFeedback.result = Result.Success;
            return Result.Success;
        }
    }

    public class EmpathyRollBehavior : IRollBehavior
    {
        public Result doRoll(Argument arg, CharacterData attacker, CharacterData defender, CharacterData world, ArgumentFeedback argFeedback)
        {
            double attackerJS = attacker.MyAttributes["StateOfMindJoySorrow"], attackerAF = attacker.MyAttributes["StateOfMindAngerFear"], defenderJS = defender.MyAttributes["StateOfMindJoySorrow"], defenderAF = defender.MyAttributes["StateOfMindAngerFear"];
            argFeedback.result = Result.None;
            if ((attackerJS == 2 && attackerAF == 2) || (defenderJS == 2 && defenderAF == 2))//Tranquility, so no action
                return Result.None;
            if (Math.Abs(attackerJS - defenderJS) > 1 || Math.Abs(attackerAF - defenderAF) > 1)//not the same SoM
                return Result.None;
            if ((attackerJS == 2 && defenderJS != 2) || (defenderJS == 2 && attackerJS != 2))
                return Result.None;
            if ((attackerAF == 2 && defenderAF != 2) || (attackerAF != 2 && defenderAF == 2))
                return Result.None;

            return (new NormalRollBehavior()).doRoll(arg,attacker,defender,world,argFeedback);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharacterSystemLibrary.Classes;
using DisputeCommon;
using DisputeCommon.Feedback;
using System.Runtime.Serialization;

namespace DisputeCommon.Arguments
{

    public class PropertyNotFoundException : Exception
    {
        public string propertyName;
    }

    [DataContract]
    public abstract class Argument
    {
        static public int lowerPropertyBound=0;
        static public int upperToneBound = 10;
        static public int upperSOMBound = 4;
        static public IFeedbackWriter Feedback;

        public Result result;
        protected CharacterData attacker, defender, world;
        private bool isSubterfugeArgument;
        //Attributesused for feedback
        private ArgumentFeedback argFeedback;
        private IRollBehavior rollBehavior;

        public IRollBehavior RollBehavior
        {
            get { return rollBehavior; }
            set { rollBehavior = value; }
        }

        public CharacterData World
        {
            get { return world; }
            set { world = value; }
        }

        public CharacterData Defender
        {
            get { return defender; }
            set { defender = value; }
        }

        public CharacterData Attacker
        {
            get { return attacker; }
            set { attacker = value; }
        }

        public ArgumentFeedback ArgFeedback
        {
            get { return argFeedback; }
            set { argFeedback = value; }
        }

        public bool IsSubterfugeArgument
        {
            get { return isSubterfugeArgument; }
            set { isSubterfugeArgument = value; }
        }

        protected string attackerCheckProperty;
        protected string defenderCheckProperty;
        protected string defenderAffectedPropertyFailure, defenderAffectedPropertySuccess, defenderAffectedPropertyGreatSuccess;
        protected string attackerAffectedPropertyFailure, attackerAffectedPropertySuccess, attackerAffectedPropertyGreatSuccess;

        protected Factor defenderFailureValue, defenderSuccessValue, defenderGreatSuccessValue;
        protected Factor attackerFailureValue, attackerSuccessValue, attackerGreatSuccessValue;
        protected Factor toneShiftFailureValue, toneShiftSuccessValue, toneShiftGreatSuccessValue;

        #region PROPERTIES

        public Factor DefenderGreatSuccessValue
        {
            get { return defenderGreatSuccessValue; }
            set { defenderGreatSuccessValue = value; }
        }

        public Factor DefenderSuccessValue
        {
            get { return defenderSuccessValue; }
            set { defenderSuccessValue = value; }
        }

        public Factor DefenderFailureValue
        {
            get { return defenderFailureValue; }
            set { defenderFailureValue = value; }
        }

        public Factor AttackerGreatSuccessValue
        {
            get { return attackerGreatSuccessValue; }
            set { attackerGreatSuccessValue = value; }
        }

        public Factor AttackerSuccessValue
        {
            get { return attackerSuccessValue; }
            set { attackerSuccessValue = value; }
        }

        public Factor AttackerFailureValue
        {
            get { return attackerFailureValue; }
            set { attackerFailureValue = value; }
        }

        public Factor ToneShiftGreatSuccessValue
        {
            get { return toneShiftGreatSuccessValue; }
            set { toneShiftGreatSuccessValue = value; }
        }

        public Factor ToneShiftSuccessValue
        {
            get { return toneShiftSuccessValue; }
            set { toneShiftSuccessValue = value; }
        }

        public Factor ToneShiftFailureValue
        {
            get { return toneShiftFailureValue; }
            set { toneShiftFailureValue = value; }
        }

        [DataMember]
        public string AttackerCheckProperty
        {
            get { return attackerCheckProperty; }
            set { attackerCheckProperty = value; }
        }

        [DataMember]
        public string DefenderCheckProperty
        {
            get { return defenderCheckProperty; }
            set { defenderCheckProperty = value; }
        }

        public string DefenderAffectedPropertyGreatSuccess
        {
            get { return defenderAffectedPropertyGreatSuccess; }
            set { defenderAffectedPropertyGreatSuccess = value; }
        }

        public string DefenderAffectedPropertySuccess
        {
            get { return defenderAffectedPropertySuccess; }
            set { defenderAffectedPropertySuccess = value; }
        }

        public string DefenderAffectedPropertyFailure
        {
            get { return defenderAffectedPropertyFailure; }
            set { defenderAffectedPropertyFailure = value; }
        }

        public string AttackerAffectedPropertyGreatSuccess
        {
            get { return attackerAffectedPropertyGreatSuccess; }
            set { attackerAffectedPropertyGreatSuccess = value; }
        }

        public string AttackerAffectedPropertySuccess
        {
            get { return attackerAffectedPropertySuccess; }
            set { attackerAffectedPropertySuccess = value; }
        }

        public string AttackerAffectedPropertyFailure
        {
            get { return attackerAffectedPropertyFailure; }
            set { attackerAffectedPropertyFailure = value; }
        }
        #endregion

        public Argument()
        {
            defenderFailureValue = new Factor();
            defenderGreatSuccessValue = new Factor();
            defenderSuccessValue = new Factor();
            attackerFailureValue = new Factor();
            attackerSuccessValue = new Factor();
            attackerGreatSuccessValue = new Factor();
            toneShiftFailureValue = new Factor();
            toneShiftGreatSuccessValue = new Factor();
            toneShiftSuccessValue = new Factor();
        }

        public void doEffects()
        {
            switch (result)
            {
                case Result.Success:
                    changeProperty(Defender, defenderAffectedPropertySuccess??"", defenderSuccessValue);
                    changeProperty(Attacker, attackerAffectedPropertySuccess??"", attackerSuccessValue);
                    changeProperty(World, "Tone", toneShiftSuccessValue);
                    break;
                case Result.GreatSuccess:
                    changeProperty(Defender, defenderAffectedPropertyGreatSuccess??"", defenderGreatSuccessValue);
                    changeProperty(Attacker, attackerAffectedPropertyGreatSuccess??"", attackerGreatSuccessValue);
                    changeProperty(World, "Tone", toneShiftGreatSuccessValue);                    
                    break;
                case Result.Failure:
                    changeProperty(Attacker, attackerAffectedPropertyFailure??"", attackerFailureValue);
                    changeProperty(Defender, defenderAffectedPropertyFailure??"", defenderFailureValue);
                    changeProperty(World, "Tone", toneShiftFailureValue);
                    break;
                default:
                    break;
            }
            //Make sure tone and som stay within bounds
            toneWithinBounds(World);
            SoMWithinBounds(Attacker, Defender);
        }

        double changeProperty(CharacterData playerAffected, string propertyName, Factor factor)
        {
            if (String.IsNullOrEmpty(propertyName))
                return -1;
            double roll = doRoll(factor);

            if (playerAffected.MyStats.ContainsKey(propertyName))
                playerAffected.MyStats[propertyName] = changeWithinBounds(playerAffected.MyStats[propertyName],roll);
            else if (playerAffected.MyAttributes.ContainsKey(propertyName))
                playerAffected.MyAttributes[propertyName] = changeWithinBounds(playerAffected.MyAttributes[propertyName], roll);
            else if (playerAffected.MySkills.ContainsKey(propertyName))
                playerAffected.MySkills[propertyName] = changeWithinBounds(playerAffected.MySkills[propertyName],roll);
            else
            {
                sendFeedback("changeProperty", propertyName + " is not a stat,att or skill of " + playerAffected.Name);
                roll = -1;
            }

            if (roll != -1)
            {
                ArgFeedback.affectedProperty.Add(propertyName);
                ArgFeedback.affectedValues.Add(roll);
            }

            return roll;
        }
        /// <summary>
        /// Overwrite this if you are SoM action
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <param name="world"></param>
        public ArgumentFeedback doArgument(CharacterData attacker, CharacterData defender, CharacterData world)
        {
            if (!checkCharactersValid(attacker, defender))
                return null;

            //Reset feedback atts
            ArgFeedback = new ArgumentFeedback();
            ArgFeedback.argumentName = this.ToString();

            //Set Characters
            this.Attacker = attacker;
            this.Defender = defender;
            this.World = world;

            result= RollBehavior.doRoll(this, attacker, defender, world, ArgFeedback);
            doEffects();

            return ArgFeedback;
        }


        public abstract string ToString();



        //Static Helpers
        /// <summary>
        /// If property is not found, return 0
        /// </summary>
        /// <param name="character"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        static double getProperty(CharacterData character, string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
                return 0;
            if (character.MyStats.ContainsKey(propertyName))
                return character.MyStats[propertyName];
            else if (character.MyAttributes.ContainsKey(propertyName))
                return character.MyAttributes[propertyName];
            else if (character.MySkills.ContainsKey(propertyName))
                return character.MySkills[propertyName];
            else
                return 0;
        }
        /// <summary>
        /// Makes sure properties dont get changed out of bounds
        /// </summary>
        /// <param name="old"></param>
        /// <param name="change"></param>
        /// <returns></returns>
        static double changeWithinBounds(double old, double change)
        {
            return (old + change) >= 0 ? old + change : 0;
        }
        /// <summary>
        /// Makes sure World.Tone doesn't go out of bounds
        /// </summary>
        /// <param name="world"></param>
        static void toneWithinBounds(CharacterData world)
        {
            world.MyStats["Tone"] = world.MyStats["Tone"] < upperToneBound ? world.MyStats["Tone"] : upperToneBound;
        }
        static void SoMWithinBounds(CharacterData p1, CharacterData p2 = null)
        {
            p1.MyAttributes["StateOfMindAngerFear"] = p1.MyAttributes["StateOfMindAngerFear"] < upperSOMBound ? p1.MyAttributes["StateOfMindAngerFear"] : upperSOMBound;
            p1.MyAttributes["StateOfMindJoySorrow"] = p1.MyAttributes["StateOfMindJoySorrow"] < upperSOMBound ? p1.MyAttributes["StateOfMindJoySorrow"] : upperSOMBound;

            if (p2 != null)
            {
                p2.MyAttributes["StateOfMindAngerFear"] = p2.MyAttributes["StateOfMindAngerFear"] < upperSOMBound ? p2.MyAttributes["StateOfMindAngerFear"] : upperSOMBound;
                p2.MyAttributes["StateOfMindJoySorrow"] = p2.MyAttributes["StateOfMindJoySorrow"] < upperSOMBound ? p2.MyAttributes["StateOfMindJoySorrow"] : upperSOMBound;
            }

        }
        public static bool checkCharactersValid(CharacterData c1, CharacterData c2)
        {
            if (c1 == null || c2 == null || c1.MyAttributes == null || c2.MyAttributes == null || c2.MySkills == null || c1.MySkills == null || c1.MyStats == null || c2.MyStats == null)
            {
                sendFeedback("checkCharactersValid", "Characters not valid");
                return false;
            }
            else
                return true;
        }
        public static void sendFeedback(string functionName, string feedback)
        {
            Feedback.WriteLine("Server   Method:" + functionName + "   Feedback:" + feedback);
        }        
        public static int doRoll(int dice, int type)
        {
            int sum=0;
            Random r=new Random();
            for (int i = 0; i < Math.Abs(dice); i++)
                sum += r.Next(type) + 1;

            return dice>=0?sum:-sum;
        }
        /// <summary>
        /// Returns the sum of the roll and the constant part using values from factor. 
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static double doRoll(Factor factor)
        {
            double constant = factor.Numerator / factor.Denominator;
            double roll = doRoll(factor.NumberOfDice, factor.DiceType);
            return constant + roll;
        }

        
    }
}

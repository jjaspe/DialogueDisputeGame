using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{    
    public class Goal
    {
        GoalTypes type;
        string propertyName;

        
        double value;

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public GoalTypes Type
        {
            get { return type; }
            set { type = value; }
        }

        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        public bool isGoalReached(CharacterData character)
        {
            double current = character.getProperty(this.propertyName);
            if(!Double.IsNaN(current))
            {
                switch(Type)
                {
                    case GoalTypes.ReachValue:
                        return current==value;
                    case GoalTypes.StayAbove:
                        return current>value;
                    case GoalTypes.StayBelow:
                        return current<value;
                    default:
                        return false;
                }
            }
            else
                return false;
        }
    }
}

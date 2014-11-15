using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public enum ArgNames { Analyze, Bluff, Charm, Coerce, Convince, Empathy, Focus, Manipulate, Scare, Taunt, Trick };
    public enum GoalTypes { ReachValue, StayAbove, StayBelow };
    /// <summary>
    /// State of Mind values
    /// </summary>
    [Flags]
    public enum SOM 
    { Joy=1, Sorrow=2, Anger=4, Fear=8,None=16 };

    /// <summary>
    /// Result of action values
    /// </summary>
    public enum Result { Failure, Success, GreatSuccess, None };
    /// <summary>
    /// Types of data that can be passed from client to server and viceversa
    /// </summary>
    public enum DataType { xmlDocument, player, match, matches, value, stat, skill, attribute, name, playerName, world, character, players, viewMessage, serverMessage, SoMRequest, Goal };


    public class DisputeCommonGlobals
    {
        public static string defaultAddress = "127.0.0.1", defaultPort = "8441";
        public static List<String> Stats = new List<string>() { "Resistance", "Fortitude", "Self Control" };
        public static List<String> Skills = 
        new List<String>(){ "Persuasion", "Intimidation", "Perception", "Subterfuge" };
    }

    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}

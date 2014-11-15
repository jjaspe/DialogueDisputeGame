using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using DisputeCommon.Feedback;
using DisputeCommon.Arguments;

namespace DisputeCommon
{
    [DataContract]
    public class Match
    {
        DataPlayer player1, player2;
        bool player1Ready, player2Ready;
        CharacterData worldData;
        int turn = 0;//0 for player1, 1 for player2
        String transcript = "", lastArgumentTranscript = "";
        ArgumentFeedback lastArgument;
        List<Argument> possibleArguments = new List<Argument>();
        Goal goal;

        public Match()
        {
            List<PropertyData> stats = new List<PropertyData>(){new PropertyData(){Name="Tone",Value=0},
                                                              new PropertyData(){Name="Tone Shift",Value=0}};
            List<PropertyData> skills = new List<PropertyData>() { new PropertyData() { Name = "Test", Value = 0 } };

            worldData = new CharacterData(null,PropertyData.toDictionary(stats),PropertyData.toDictionary(skills),"World");
            
        }

        public string MatchName
        {
            get { return Player1.PlayerName; }
        }


        [DataMember(Order = 0)]
        public Goal Goal
        {
            get { return goal; }
            set { goal = value; }
        }

        [DataMember(Order=0)]
        public List<Argument> PossibleArguments
        {
            get { return possibleArguments; }
            set { possibleArguments = value; }
        }

        [DataMember(Order = 0)]
        public ArgumentFeedback LastArgument
        {
            get { return lastArgument; }
            set { lastArgument = value; }
        }

        [DataMember(Order=0)]
        public String LastArgumentTranscript
        {
            get { return lastArgumentTranscript; }
            set { lastArgumentTranscript = value; }
        }

        [DataMember(Order=0)]
        public String Transcript
        {
            get { return transcript; }
            set { transcript = value; }
        }

        [DataMember(Order=0)]
        public CharacterData World
        {
            get { return worldData; }
            set { worldData = value; }
        }

        [DataMember(Order=1)]
        public bool Player2Ready
        {
            get { return player2Ready; }
            set { player2Ready = value;
            setUpMatchStartingValue();
            }
        }
        [DataMember(Order = 1)]
        public bool Player1Ready
        {
            get { return player1Ready; }
            set { player1Ready = value;
                    setUpMatchStartingValue();
            }
        }

        [DataMember(Order = 2)]
        public DataPlayer Player2
        {
            get { return player2; }
            set { player2 = value;
            setUpMatchStartingValue();
            }
        }
        [DataMember(Order = 2)]
        public DataPlayer Player1
        {
            get { return player1; }
            set { player1 = value; }
        }

        [DataMember(Order = 3)]
        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        public void setUpMatchStartingValue()
        {
            if(player1==null || player2==null || player1.Character==null || player2.Character==null)
                return;
            double tone1 = player1.Character.getStat("Tone"), tone2 = player2.Character.getStat("Tone");
            worldData.MyStats["Tone"] = (int)(tone1 + tone2) / 2;
        }
        public void playerUpdated(DataPlayer player)
        {
            if (player2.Equals(player) || player1.Equals(player))
                setUpMatchStartingValue();
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType() && (obj as Match).MatchName.Equals(this.MatchName))
                return true;
            else
                return false;
        }
        public override String ToString()
        {
            return player1 + "%" + player2;
        }
        void updateTranscripts(string argName,double rollResult, 
            List<string> affectedProperties, List<double> attackResults,Result result,string player)
        {
            string propertiesString = "";
            foreach (string s in affectedProperties)
                propertiesString += "(" + s + "),";
            string valuesString = "";
            foreach (double d in attackResults)
                valuesString += "(" + d + ")," ;

            string newLines = player + " uses " + "\n" +
                         argName + "\n" +
                         "Roll:" + rollResult + "\n" +
                         "Properties Affected:" + propertiesString +"\n"+
                         "Changed by: "+valuesString+"\n";
            
            transcript = newLines + "\n"+ transcript;

            LastArgumentTranscript = argName + "\n" +
                         "Roll:" + rollResult + "\t" + result.ToString() + "\n"+
                         "Properties Affected:" + propertiesString + "\n" +
                         "Changed by: " + valuesString + "\n";
        }

        public void updateTranscript(ArgumentFeedback f)
        {
            LastArgument = f;
            updateTranscripts(f.argumentName, f.rollResult, f.affectedProperty, f.affectedValues,f.result,f.playerName);
        }
        public static Match parseGame(String gameString)
        {
            int n=gameString.IndexOf("%");
            Match newGame = new Match();
            newGame.Player1 = new DataPlayer();
            newGame.Player2 = new DataPlayer();

            if (n >= 0)
            {
                newGame.player1.PlayerName = gameString.Substring(0, n);
                gameString = gameString.Substring(n+1);
                n = gameString.IndexOf("%");
                if (n >= 0)
                {
                    newGame.player2.PlayerName = gameString.Substring(0, n);
                    gameString = gameString.Substring(n+1);
                }
            }

            return newGame;
            
        }


        public void updateTranscript(string p)
        {
            transcript=p+"\n"+transcript;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DisputeCommon
{
    [DataContract(IsReference=true)]
    public class DataPlayer
    {
        string address, port, playerName;
        CharacterData character;
        Match activeMatch;
        bool inGame = false;

        [DataMember]
        public bool InGame
        {
            get { return inGame; }
            set { inGame = value; }
        }

        [DataMember]
        public Match ActiveMatch
        {
            get { return activeMatch; }
            set { activeMatch = value; }
        }

        [DataMember]
        public CharacterData Character
        {
            get { return character; }
            set { character = value; }
        }

        [DataMember]
        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }
        [DataMember]
        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string toString() { return playerName; }
        public override bool  Equals(object obj)
        {
 	         if(obj.GetType()==this.GetType() &&((DataPlayer)obj).PlayerName.Equals(this.PlayerName))
                 return true;
            return false;

        }
    }


    [DataContract]
    public class DisputeDTO
    {
        String playerName;
        List<Match> matches;
        List<DataPlayer> players;
        CharacterData world;
        Messages.GameMessages message;
        SoMRequest somRequest;

        [DataMember]
        public SoMRequest SomRequest
        {
            get { return somRequest; }
            set { somRequest = value; }
        }
        DataPlayer player1, player2;
        Match match;

        [DataMember]
        public Match Match
        {
            get { return match; }
            set { match = value; }
        }

        [DataMember]
        public DataPlayer Player2
        {
            get { return player2; }
            set { player2 = value; }
        }

        [DataMember]
        public DataPlayer Player1
        {
            get { return player1; }
            set { player1 = value; }
        }


        [DataMember]
        public String PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        [DataMember]
        public List<Match> Matches
        {
            get { return matches; }
            set { matches = value; }
        }

        [DataMember]
        public List<DataPlayer> Players
        {
            get { return players; }
            set { players = value; }
        }

        [DataMember]
        public CharacterData World
        {
            get { return world; }
            set { world = value; }
        }

        [DataMember]
        public Messages.GameMessages Message
        {
            get { return message; }
            set { message = value; }
        }

    }

    [DataContract]
    public class WCFMessage
    {
        DataPlayer player;
        Messages.DTOType type;
        DisputeDTO data;

        [DataMember]
        public Messages.DTOType Type
        {
            get { return type; }
            set
            {
                type = value;
            }
        }

        [DataMember]
        public DataPlayer Player
        {
            get { return player; }
            set { player = value; }
        }

        [DataMember]
        public DisputeDTO Data
        {
            get { return data; }
            set { data = value; }
        }
    }

    [DataContract]
    public class WCFMatch
    {
        string player1, player2, creator;

        [DataMember]
        public string Creator
        {
            get { return creator; }
            set { creator = value; }
        }

        [DataMember]
        public string Player2
        {
            get { return player2; }
            set { player2 = value; }
        }

        [DataMember]
        public string Player1
        {
            get { return player1; }
            set { player1 = value; }
        }
    }

    [DataContract]
    public class SoMRequest
    {
        SOM allowed;

        [DataMember]
        public SOM Allowed
        {
            get { return allowed; }
            set { allowed = value; }
        }
        Result result;

        [DataMember]
        public Result Result
        {
            get { return result; }
            set { result = value; }
        }
    }
}

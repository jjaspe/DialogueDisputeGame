using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace DisputeCommon
{
    public class DataTypes
    {
        /// <summary>
        /// Finds number'th object of required type
        /// </summary>
        /// <param name="list">List of named parameters to search</param>
        /// <param name="type"> Type of parameter to search for (must be in Model list)</param>
        /// <param name="number"> Out of the possible many parameters meeting the type requirement, number
        ///  is the position of the wanted one (0 for first,1 for second,etc)</param>
        /// <returns> object found</returns>
        static public object findOne(List<NamedParameter> list,DataType type, int number = 0)
        {
            object value = null;
            List<NamedParameter> found=list.FindAll(n => n.type.Equals(type.ToString()));
            if (found != null && found.Count > 0)
                value = found.ElementAt(number);

            return value;
        }
        /// <summary>
        /// Returns all parameters of required type
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static public List<NamedParameter> findAll(List<NamedParameter> list, DataType type)
        {
            return list.FindAll(n => n.type.Equals(type));
        }

        static public List<NamedParameter> createList(IList list,DataType type)
        {
            List<NamedParameter> newList = new List<NamedParameter>();
            foreach (object o in list)
            {
                newList.Add(new NamedParameter() { data = o, type = type });
            }
            return newList;
        }
        
    }
    public class Messages
    {
        public enum GameMessages
        {
            stillAlive,
            connect,
            connected,
            playerExists,
            dataReceived,
            SendActiveMatches,
            CreateMatch,
            matchCreated,
            gamesUpdated,
            JoinMatch,
            PlayerJoinedGame,
            PlayerInMatch,
            lockIn,
            lockOut,
            characterInfo,
            startGame,
            yourTurn,
            opponentTurn,
            playerQuit,
            doAction,
            GameOver,
            startAction,
            endAction,
            loadCharacter,
            saveCharacter,
            PlayerReady,
            emptyDataReceived,
            MatchIsFull,
            InvalidMatch,
            LeaveMatch,
            PlayerNotInMatch,
            playerLeftMatch,
            UpdateMatches,
            stop,
            activeMatchesSent,
            playersSent,
            sendPlayers,
            PlayerConnected,
            UpdateAll,
            none,
            InvalidCharacter,
            SendCharacterToServer,
            PlayerNotFound,
            Manipulate,
            GetSOM,
            ArgumentDone,
            somChosen,
            StartingMatch,
            NotPlayerTurn,
            UpdateMatch,
            Charm,
            Convince,
            Scare,
            Taunt,
            Coerce,
            Bluff,
            Empathy,
            PlayerNotReady,
            GenericArgument,
            SoMDependentArgument,
            Focus,
            Trick,
            Analyze,
            SendGoalToServer,
            GameDoesntHaveGoal
        };
        public enum DTOType
        {
            message,
            lobby,
            match
        }

        public enum LobbyViewMessage
        {
            viewClosed,
            connect,
            StartServer,
            StopServer,
            StartGame,
            UpdateAll
        };

        public enum MatchViewMessage
        {
            analyze,
            bluff,
            charm,
            coerce,
            convince,
            empathy,
            focus,
            manipulate,
            scare,
            taunt,
            trick,

        }
    }
}

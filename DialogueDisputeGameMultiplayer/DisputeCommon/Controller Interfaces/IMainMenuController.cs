using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    
    public interface IMainMenuController
    {
        List<Match> Matches
        {
            get;
            set;
        }
        bool Connected
        {
            get;
            set;
        }
        void MessageSentFromView(MainMenuMessages code,List<object> args);

        string PlayerName { get;}

        CharacterData Character { get;}
        List<DataPlayer> Players { get; set; }

    }
    public enum MainMenuMessages
    {
        CreateCharacter,
        ConnectToServer,
        FormClosed,
        CreateMatch,
        JoinMatch,
        LoadPlayer1,
        LoadPlayer2,
        PlayDispute,
        LockIn,
        CharacterLoaded,
        Update,
        SelectedMatchChanged,
        LeaveGame,
        RefreshMatches,
        PlayerReady,
        CreateGoal
    };
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon
{
    public interface IMainMenuView
    {
        void start();
        void stop();
        void updateCharacterData(string playerName, List<KeyValuePair<String, double>> properties);
        void updateProperty(string propertyName, string playerName, string value);
        void updateMatches(List<Match> matches);
        void updateSelectedMatch(Match activeMatch);
        void updatePlayerName(String name);
        IMainMenuController Controller
        {
            get;
            set;
        }
        bool Connected
        {
            get;
            set;
        }
    }
}

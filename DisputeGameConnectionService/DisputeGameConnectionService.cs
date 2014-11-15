using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DisputeCommon;
using System.Xml;
using DisputeCommon.Interfaces;

namespace DisputeGameConnection
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class DisputeGameConnectionService : IDisputeGameConnectionService
    {
        static DisputeGameConnectionService myInstance;
        public static DisputeGameConnectionService getInstance()
        {
            myInstance = myInstance ?? new DisputeGameConnectionService();
            return myInstance;
        }
        IServerConnectionManager manager;

        public IServerConnectionManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        private DisputeGameConnectionService()
        {
            
        }
        private DisputeGameConnectionService(IServerConnectionManager m)
        {
            manager = m;
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        #region IService
        public void sendMessage(Messages.GameMessages message)
        {
            manager.broadcast(message);
        }

        public void addNewMessage(DataPlayer player, Messages.GameMessages message)
        {
            manager.addNewMessage(player, message);
        }

        public void addDataMessage(DataPlayer player, Messages.GameMessages msg, String data)
        {
            manager.addNewMessage(player, msg, data);
        }
        public DataPlayer connect(string playerName)
        {
            return manager.addNewPlayer(new DataPlayer() { PlayerName = playerName });

        }

        public List<WCFMessage> getMessages(DataPlayer player)
        {
            return manager.getMessages(player);
        }

        public List<DataPlayer> getPlayers()
        {
            return manager.getPlayers();
        }

        public void removePlayer(DataPlayer player)
        {
            manager.removePlayer(player);
        }
        public void addCharacter(CharacterData character, DataPlayer player)
        {
            manager.addCharacter(character, player);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DisputeCommon;
using System.Xml;

namespace DisputeGameConnection
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDisputeGameConnectionService
    {
        [OperationContract]
        DataPlayer connect(String playerName);

        [OperationContract]
        void sendMessage(Messages.GameMessages message);

        [OperationContract]
        void addNewMessage(DataPlayer player, Messages.GameMessages msg);

        [OperationContract]
        void addDataMessage(DataPlayer player, Messages.GameMessages msg, String data);

        [OperationContract]
        List<WCFMessage> getMessages(DataPlayer player);

        [OperationContract]
        void removePlayer(DataPlayer player);

        [OperationContract]
        List<DataPlayer> getPlayers();

        [OperationContract]
        void addCharacter(CharacterData character,DataPlayer player);

        // TODO: Add your service operations here
    }

}

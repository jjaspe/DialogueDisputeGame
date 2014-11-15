using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisputeCommon;
using System.Xml;

namespace DisputeCommon.Interfaces
{
    public interface IServerConnectionManager:IServerViewController
    {
         void broadcast(Messages.GameMessages message);
         DataPlayer addNewPlayer(DataPlayer player);
         void addNewMessage(DataPlayer player, Messages.GameMessages msg,String data=null);
         void removePlayer(DataPlayer player);
         List<WCFMessage> getMessages(DataPlayer player);
         List<DataPlayer> getPlayers();
         void addCharacter(CharacterData character, DataPlayer player);

         void addGoal(Goal goal, DataPlayer player);
    }
}

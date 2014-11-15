using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DisputeGameConnection;
using System.ServiceModel.Channels;

namespace WCFClientConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IDisputeGameConnectionService> remoteFactory = new ChannelFactory<IDisputeGameConnectionService>("ClientConfig");
            IDisputeGameConnectionService remoteProxy = remoteFactory.CreateChannel();
            WCFPlayer clientUser = remoteProxy.connect("Johnny");

            if (clientUser != null)
            {
                //Enable timers
                //Create a message and send it to service
                WCFMessage msg = new WCFMessage() { Player = clientUser, Data = "Hello", Type = "Name" };
                remoteProxy.sendMessage(msg);
                //Get messages from server
                List<WCFMessage> messages = remoteProxy.getMessages(clientUser);
                Console.WriteLine(messages[0].Data.ToString());
                Console.ReadLine();
            }
        }
    }
}

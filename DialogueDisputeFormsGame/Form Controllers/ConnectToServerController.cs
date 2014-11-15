using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using DialogueDisputeFormsGameForms;
using System.Windows.Forms;
using DialogueCommon.Interfaces;
using DisputeCommon;



namespace DialogueDisputeFormsGameForm_Controllers
{
    /// <summary>
    /// Singleton class in charge of communication between form controllers and server
    /// </summary>
    public class ConnectToServerController : IDisputeViewController,IConnectToServerController,
        IDisputeObservable
    {
        //Singleton
        static ConnectToServerController instance = new ConnectToServerController();
        public static ConnectToServerController getInstance(IClientConnectionManager cm=null)
        {
            if(instance==null)
                instance=new ConnectToServerController(cm);
            else
                instance.cm = cm;

            return instance;
        }

        IClientConnectionManager cm;
        List<IControllerObserver> myObservers;
        IConnectToServerView myConnectToServerView;

        public IConnectToServerView MyConnectToServerView
        {
          get { return myConnectToServerView; }
          set { myConnectToServerView = value; }
        }

        private ConnectToServerController()
        {
            myObservers = new List<IControllerObserver>();
        }
        public ConnectToServerController(IClientConnectionManager m)
        {
            myObservers = new List<IControllerObserver>();
            cm = m;
        }

        public void setView(IConnectToServerView view)
        {
            MyConnectToServerView=view;
        }

        public void MessageSentFromView(Messages.LobbyViewMessage code, List<object> args, object sender)
        {
            switch (code)
            {
                case Messages.LobbyViewMessage.connect://Address and port are in array
                    cm.parseRequest(Messages.GameMessages.connect, args, this);
                    break;
                case Messages.LobbyViewMessage.viewClosed:
                    notifyObservers("Connect To Server Form Closed");
                    break;
                default:
                    break;
            }
        }


        //DisputeFormController Stuff
        public void start()
        {
            if (myConnectToServerView != null)
                myConnectToServerView.start();
        }

        public void stop()
        {
            if (this.myConnectToServerView != null)
                this.myConnectToServerView.stop();
        }

        public void formClosed(string code)
        {
            throw new NotImplementedException();
             
        }

        //Observable implementation
        /*Observable Stuff*/
        public void registerObserver(object obs)
        {
            myObservers.Add((IControllerObserver)obs);
        }

        public void removeObserver(object obs)
        {
            myObservers.Remove((IControllerObserver)obs);
        }

        public void notifyObservers(object arg = null)
        {
            foreach (IControllerObserver obs in myObservers)
                obs.update((string)arg);
        }
    }
}

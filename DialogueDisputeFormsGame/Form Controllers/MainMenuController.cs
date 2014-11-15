using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using DialogueDisputeFormsGame.Forms;

using DialogueCommon.Interfaces;
using DisputeCommon;
using DialogueDisputeGameClient.Network;


using DialogueDisputeGameClient;
using DialogueDisputeGameClient.WCF;
using DialogueDisputeGameClient.Forms;
using DialogueDisputeGameClient;

/* ***************************************************Commented out***********************************************
 * - playDispute
 * */






namespace DialogueDisputeFormsGameForm_Controllers
{

    public class MainMenuController :IMainMenuController, IControllerObserver,IConnectionObserver
    {
        //Views
        IMainMenuView myView;
        Type mainMenuType;

        public IMainMenuView MyView
        {
            set { myView = value; }
            get { return myView; }
        }
        ICreateCharacterController myCreateCharacterFormController;
        IMatchController myMatchFormController;

        public IMatchController MyMatchFormController
        {
            get { return myMatchFormController; }
            set { myMatchFormController = value; }
        }
        IConnectToServerController myConnectToServerController;

        //ISoMSelectorController mySoMSelectorController;
        IClientConnectionManager connectionManager;

        CharacterData myCharacter;
        Match activeMatch, selectedMatch;
        List<DataPlayer> players = new List<DataPlayer>();
        private String playerName;
        bool connected;
        /// <summary>
        /// IF true, updates will be caught here, otherwise updates will be passed to matchController
        /// </summary>
        bool lobbyUpdate = true;
        Goal goal;
        List<Match> matches = new List<Match>();

        //Autorun stuff
        public bool isAutorun = false;
        public string autorunName;//Player name for autorun


        
        public CharacterData Character
        {
            get { return myCharacter; }
            set { myCharacter = value; }
        }        
        public Match ActiveMatch()
        {
            return activeMatch;
        }
        public List<Match> Matches
        {
            get { return matches; }
            set { matches = value; }
        }
        public List<DataPlayer> Players
        {
            get { return players; }
            set { players = value; }
        }
        public String PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }       
        public bool Connected
        {
            get { return connected; }
            set { 
                connected = value;
                MyView.Connected = connected;
                    }
        }
        public DataPlayer Player
        {
            get { return players.Find(n => n.PlayerName.Equals(playerName)); }
        }
        




        /*          CONSTRUCTOR         */
        public MainMenuController(IClientConnectionManager _manager,IMainMenuView mainMenuView,
            IConnectToServerView connectToServerView)
        {
            initGame();
            myView = new MainMenuForm(this);

            //Create main view components and link them
            this.MyView = mainMenuView;
            this.MyView.Controller=this;

            //Create connect to server components and link them
            myConnectToServerController = new ConnectToServerController(_manager);
            myConnectToServerController.setView(connectToServerView);
            connectToServerView.setController(myConnectToServerController);
            mainMenuType = typeof(IConnectToServerView);

            ((IDisputeObservable)myConnectToServerController).registerObserver(this);
            //mySoMSelectorController=new GraphicSoMSelectorController();
            //myMatchController = new GraphicMatchController(myGame);
            myCreateCharacterFormController = new GraphicCreateCharacterController();
            connectionManager = _manager;
            //Register as observer of other controllers
            ((IDisputeObservable)myCreateCharacterFormController).registerObserver(this);
            ((IConnectionObservable)connectionManager).registerObserver(this);
            
        }

        private void initGame()
        {
            //Tell connection to start game
        }

        //MainMenuController implementation 
        /// <summary>
        /// Parses messages from MainMenuView, calls appropriate function as response
        /// </summary>
        /// <param name="code">message</param>
        /// <param name="args">view bag from view, in case parameters needed to be passed to response function</param>
        public void MessageSentFromView(MainMenuMessages code, List<object> args)
        {
            NamedParameter par;
            switch (code)
            {
                case MainMenuMessages.ConnectToServer:
                    connectToServer();
                    //updatePlayerName();
                    break;
                case MainMenuMessages.CreateCharacter:
                    createCharacter();
                    break;
                case MainMenuMessages.CharacterLoaded:
                    sendCharacterToServer();
                    break;
                case MainMenuMessages.CreateMatch:
                    createMatch(args);
                    break;
                case MainMenuMessages.CreateGoal:
                    createGoal();
                    break;
                case MainMenuMessages.JoinMatch:
                    joinMatch(args);
                    break;
                case MainMenuMessages.LeaveGame:
                    connectionManager.parseRequest(Messages.GameMessages.LeaveMatch, args, this);
                    break;
                case MainMenuMessages.RefreshMatches:
                    connectionManager.parseRequest(Messages.GameMessages.UpdateMatches, args, this);
                    break;
                case MainMenuMessages.LoadPlayer1:
                    par=DataTypes.createList(args,DataType.xmlDocument)[0];
                    if (args != null)
                        loadCharacterFromXml((XmlDocument)par.data, 0);
                    break;
                case MainMenuMessages.LoadPlayer2:
                    par = DataTypes.createList(args, DataType.xmlDocument)[0];
                    if (args != null)
                        loadCharacterFromXml((XmlDocument)par.data, 1);
                    break;
                case MainMenuMessages.SelectedMatchChanged:
                    par = DataTypes.createList(args, DataType.name)[0];
                    setSelectedMatch(par.data.ToString());
                    updateSelectedMatch();
                    break;
                case MainMenuMessages.PlayerReady:
                    setPlayerReady();
                    break;
                case MainMenuMessages.PlayDispute:
                    playDispute();
                    break;
                case MainMenuMessages.FormClosed:
                    this.formClosed("Closed");
                    break;
                case MainMenuMessages.Update:
                    connectionManager.parseRequest(Messages.GameMessages.SendActiveMatches,args, this);
                    break;
                default:
                    break;
            }
            
        }

       

        //View Response Methods
        /// <summary>
        /// Sends join match request to connection manager
        /// </summary>
        /// <param name="args">contains match to join (as Match object) as first element</param>
        void joinMatch(List<object> args)
        {
 	        connectionManager.parseRequest(Messages.GameMessages.JoinMatch, args, this);
        }
        /// <summary>
        /// Sends connect to server request to connection
        /// </summary>
        void connectToServer()
        {
            ConnectToServerForm form = new ConnectToServerForm();
            form.AsDialog = true;
            form.ShowDialog((Form)this.myView);
            if (!String.IsNullOrEmpty(form.PlayerName))
            {
                connectionManager.parseRequest(Messages.GameMessages.connect, new List<object> { form.PlayerName }, this);
            }
            
        }
        /// <summary>
        /// Sends create match signal to connection manager
        /// </summary>
        /// <param name="args"></param>
        void createMatch(List<object> args)
        {
            connectionManager.parseRequest(Messages.GameMessages.CreateMatch, args, this);
        }
        /// <summary>
        /// Send player ready signal to connection
        /// </summary>
        void setPlayerReady()
        {
            connectionManager.parseRequest(Messages.GameMessages.PlayerReady, null, this);
        }
        /// <summary>
        /// Creates character data from nodes,saves it into a myCharacter, and calls sendCharacterToServer
        /// </summary>
        /// <param name="myDoc">Character data in xml</param>
        /// <param name="playerIndex">Player 1 (playerIndex=0) or 2 (playerIndex=1)</param>
        void loadCharacterFromXml(XmlDocument myDoc, int playerIndex)
        {
            Dictionary<String, Double> stats = new Dictionary<string, double>(),
                skills = new Dictionary<string, double>(), atts = new Dictionary<string, double>();
            String name = "";

            //Read xml data
            try
            {
                XmlNode charNode = myDoc.GetElementsByTagName("Character").Item(0), nameNode;
                XmlNodeList statNodes = ((XmlElement)((XmlElement)charNode).GetElementsByTagName("Stats").Item(0)).GetElementsByTagName("Stat");
                XmlNodeList skillNodes = ((XmlElement)((XmlElement)charNode).GetElementsByTagName("Skills").Item(0)).GetElementsByTagName("Skill");
                XmlNodeList attributeNodes = ((XmlElement)((XmlElement)charNode).GetElementsByTagName("Attributes").Item(0)).GetElementsByTagName("Attribute");
                nameNode = ((XmlElement)charNode).GetElementsByTagName("Name").Item(0);

                //Save xmlData to lists

                foreach (XmlNode node in statNodes)
                {
                    stats.Add(node.FirstChild.InnerText, Double.Parse(node.LastChild.InnerText));
                }
                foreach (XmlNode node in skillNodes)
                {
                    skills.Add(node.FirstChild.InnerText, Double.Parse(node.LastChild.InnerText));
                }
                foreach (XmlNode node in attributeNodes)
                {
                    atts.Add(node.FirstChild.InnerText, Double.Parse(node.LastChild.InnerText));
                }
                if (nameNode != null)
                    name = nameNode.InnerXml;

                myCharacter = new CharacterData(atts, stats, skills, name);
                
                //Tell server that we loaded a character
                sendCharacterToServer();
               
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in :" + e.Source);
                return;
            }

        }
        /// <summary>
        /// Sends load character request to server, sending the character data as parameter
        /// </summary>
        private void sendCharacterToServer()
        {
            connectionManager.parseRequest(Messages.GameMessages.SendCharacterToServer,
                                            new List<object>(){myCharacter},
                                            this);
        }
        /// <summary>
        /// Call create character form as dialog, then saves character to a file
        /// </summary>
        void createCharacter()
        {
            CreateCharacterForm form = new CreateCharacterForm();
            form.asDialog = true;
            form.ShowDialog((Form)this.myView);

            if (form.character != null)
            {
                object[] obj = CharacterData.createXmlSavingObjects(form.character);
                form.SaveCharacterToFile((XmlNode)obj[0], (String)obj[1], (XmlDocument)obj[2]);
            }

            //myCreateCharacterFormController = new GraphicCreateCharacterController();
            //Register as observer of child controllers
            //((IDisputeObservable)myCreateCharacterFormController).registerObserver(this);
            //this.stop();
            //((IDisputeFormController)myCreateCharacterFormController).start();
        }
        /// <summary>
        /// Calls create goal form, then sends created goal to connection manager
        /// </summary>
        private void createGoal()
        {
            Create_Goal_Form form = new Create_Goal_Form();
            DialogResult res=form.ShowDialog((Form)this.myView);

            if (res==DialogResult.OK)
            {
                goal = new Goal() { Type = form.type, PropertyName = form.propertyName, Value = form.propertyValue };
                connectionManager.parseRequest(Messages.GameMessages.SendGoalToServer, new List<object> { goal }, this);
            }

        }
        /// <summary>
        /// Initialize MatchController data,register this as it's observer, stop this view, start
        /// Match view
        /// </summary>
        void playDispute()
        {            
            MyMatchFormController.dataManager = connectionManager;
            myMatchFormController.Player = this.Player;
            myMatchFormController.Match = Player.ActiveMatch;
            connectionManager.FeedbackWriter=MyMatchFormController;
            lobbyUpdate = false;
            
            
            //Register match controller here
            (MyMatchFormController as IDisputeObservable).registerObserver(this);

            //Switch
            this.stop();
            (MyMatchFormController).start();
        }
        /// <summary>
        /// Sets match with given name as selectedMatch
        /// </summary>
        /// <param name="creatorName">Name of the match</param>
        void setSelectedMatch(String creatorName)
        {
            Match match = matches.Find(n => n.Player1.Equals(creatorName));
            selectedMatch = match == null ? selectedMatch : match;
        }
        /// <summary>
        /// Sends signal asking for data update
        /// </summary>
        public void sendUpdateDataRequest()
        {
            if (connectionManager.isConnected())
            {
                connectionManager.parseRequest(Messages.GameMessages.UpdateAll, null, this);
            }
        }


        //Form Methods
        void updatePlayerName()
        {
            myView.updatePlayerName(playerName);
        }
        void updateSelectedMatch()
        {
            myView.updateSelectedMatch(selectedMatch);
        }
        void updateMatchList()
        {            

            //myMainMenuForm.updateMatches(matches);
            ////See if selectedMatch is still in the list
            //if (!matches.Contains(selectedMatch))
            //{
            //    selectedMatch = null;
            //    myMainMenuForm.updateSelectedMatch(selectedMatch);
            //}
        }
        
        

        //Controller Observer implementation
        /// <summary>
        /// Used for managing communication between forms
        /// </summary>
        /// <param name="arg"></param>
        void IControllerObserver.update(object arg)
    {
        string formName="", actionName;
        int lastSpaceIndex;
        string code = (string)arg;

        if (code.Contains("Form"))
        {
            lastSpaceIndex = code.LastIndexOf("Form");
            actionName = code.Substring(lastSpaceIndex, code.Length - lastSpaceIndex);
            formName = code.Substring(0, lastSpaceIndex - 1);
        }
        else
        {
            actionName = code;
        }

        switch (actionName)
        {
            case "Form Closed":
                {
                    switch (formName)
                    {
                        case "Create Character":
                            ((IDisputeViewController)myCreateCharacterFormController).stop();
                            break;
                        case "Connect_To_Server":
                            updatePlayerName();
                            break;
                        case "Match":
                            this.start();
                            lobbyUpdate = true;
                            break;
                        default:
                            break;
                    }
                    this.start();
                }
                break;
            case "Game Ended":
                ((IDisputeViewController)MyMatchFormController).stop();                    
                initGame();//start new game
                this.start();
                break;
            case "Update":
                sendUpdateDataRequest();
                updateMatchList();
                updatePlayerName();
                break;
            default:
                break;
        }//End Switch
        //updateData();
        //updatePlayerControlsForm();
    }

        /// <summary>
    /// Used for managing communication with connection. This method will only be called when
    /// client is connected, so it serves as a setter for Connected
    /// </summary>
    /// <param name="code"></param>
        void IConnectionObserver.update(object code)
        {
            if(!Connected)
                Connected = true;

            if (lobbyUpdate)//Check if we do updates here or at the matchController
            {
                if (code != null)
                {
                    List<NamedParameter> dataList = (List<NamedParameter>)code;

                    List<NamedParameter> lobbyMessage = DataTypes.findAll(dataList, DataType.viewMessage);
                    List<NamedParameter> serverMessage = DataTypes.findAll(dataList, DataType.serverMessage);
                    if (lobbyMessage != null && lobbyMessage.Count > 0)
                    {
                        Messages.LobbyViewMessage msg = (Messages.LobbyViewMessage)lobbyMessage[0].data;
                        switch (msg)
                        {
                            case Messages.LobbyViewMessage.UpdateAll:
                                updateData(dataList);
                                break;
                            case Messages.LobbyViewMessage.StartGame:
                                playDispute();
                                break;                                
                        }//End Switch
                    }//End Message if
                }//End Code if
            }//End lobby/match if
            else
                (MyMatchFormController as IConnectionObserver).update(code);

        }//End update

        void updateData(List<NamedParameter> dataList)
        {
            List<NamedParameter> m = DataTypes.findAll(dataList, DataType.matches);
            if (m != null && m.Count > 0)
            {
                this.Matches = (List<Match>)m[0].data;
                this.Matches = this.Matches ?? new List<Match>();
            }
            m = DataTypes.findAll(dataList, DataType.playerName);
            if (m != null && m.Count > 0)
            {
                this.playerName = (String)m[0].data;
            }
            m = DataTypes.findAll(dataList, DataType.players);
            if (m != null && m.Count > 0)
            {
                this.Players = (List<DataPlayer>)m[0].data;
                this.players = this.players ?? new List<DataPlayer>();
            }
            m = DataTypes.findAll(dataList, DataType.character);
            if (m != null && m.Count > 0)
            {
                this.Character = (CharacterData)m[0].data;
            }
        }

        

        /* DisputeForm implementation */
        /// <summary>
        /// Entry point of controller, initializes form 
        /// </summary>
        public void start()
        {
            if(myView==null)
                myView = new MainMenuForm(this);
            myView.start();
            lobbyUpdate = true;
            if(isAutorun)
            autorun(this.autorunName.Equals("Player 1"));
        }

        void stop()
        {
            if (this.myView != null)
            {
                this.myView.stop();
            }
        }

        void formClosed(string code)
        {
            this.connectionManager.parseRequest(Messages.GameMessages.playerQuit, null, this);
            return;
        }


        void autorun(bool creator=true)
        {
            playerName = autorunName;
            if (creator)
            {
                //Connect
                connectionManager.parseRequest(Messages.GameMessages.connect, new List<object> { autorunName }, this);
                //Load character
                loadCharacterFromXml(MainMenuForm.loadCharacterFromXml(), 1);
                //Create match
                createMatch(null);
                //Lock in
                setPlayerReady();
            }
            else
            {
                //Connect
                connectionManager.parseRequest(Messages.GameMessages.connect, new List<object> { autorunName }, this);
                //Load character
                loadCharacterFromXml(MainMenuForm.loadCharacterFromXml(), 2);
                //Update to get exising matches
                sendUpdateDataRequest();
                //Join first match in list
                joinMatch(new List<object> { matches[0] });
                //Lock in
                setPlayerReady();
            }
                
        }
        
    }

}

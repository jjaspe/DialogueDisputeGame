using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DisputeCommon;
using Canvas_Window_Template;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;
using System.Threading;
using DisputeCommon.Arguments;


namespace DialogueDisputeOpenGLView
{
    public enum propertyNames { Subterfuge, Perception, Intimidation, Persuasion, Resistance, Fortitude, SelfControl }
    
    
    
    /// <summary>
    /// Dispute view that uses OpenGL primitives to represent match data.
    /// </summary>
    public partial class DisputeOpenGLView : ReadyOpenGlTemplate,IMatchView,ICanvasWindow
    {
        #region DRAWING_CONSTANTS
        public static double pauseTimeMs = 2;
        public static IPoint center,opCenter;
        public static int mapHeight = 46,mapWidth = 46,squareSize=10;
        public static int cubesPerBar = 6,columnsPerStat=3;
        static int leftEdge=-mapWidth*squareSize/2, rightEdge=mapWidth*squareSize/2;
        static int 
            panelOffset=8,
            verticalGap=7,
            horizontalGap=5,
            perceptionSquare = 5,
            persuasionSquare = perceptionSquare+verticalGap,
            subterfugeSquare=perceptionSquare+2*verticalGap,
            intimidationSquare=perceptionSquare+3*verticalGap,
            skillSquare=-1,
            fortitudeSquare = 4,
            selfControlSquare = fortitudeSquare + horizontalGap,
            resistanceSquare = selfControlSquare + horizontalGap,
            statBottomSquares = 2,
            centerX = -5,
            centerY = -10,
            centerSpace=3;
        static int 
            bottomEdge=-mapHeight*squareSize/2, 
            topEdge=mapHeight*squareSize/2;
        static int 
            resistanceLeftEdge=leftEdge+resistanceSquare*squareSize,
            resistanceBottomEdge=bottomEdge+statBottomSquares*squareSize,
            fortitudeLeftEdge = leftEdge + fortitudeSquare * squareSize,
            fortitudeBottomEdge=resistanceBottomEdge,
            selfControlLeftEdge=leftEdge+selfControlSquare*squareSize,
            selfControlBottomEdge=resistanceBottomEdge,
            skillLeftEdge=leftEdge+skillSquare*squareSize,
            perceptionBottomEdge=bottomEdge+perceptionSquare*squareSize,
            persuasionBottomEdge=bottomEdge +persuasionSquare*squareSize,
            subterfugeBottomEdge=bottomEdge+subterfugeSquare*squareSize,
            intimidationBottomEdge = bottomEdge + intimidationSquare*squareSize;
        #endregion

        #region PROPERTY_COLORS
        static Color
            fortitudeColor = Color.Brown,
            selfControlColor = Color.Blue,
            resistanceColor = Color.Red,
            persuasionColor = Color.ForestGreen,
            perceptionColor = Color.SlateGray,
            intimidationColor = Color.DarkMagenta,
            subterfugeColor = Color.DarkOrange;

        #endregion

        IWorld world = new DisputeOpenGLWorld();
        Common myDrawingObject = new Common();
        dynamicStatGraphic resistanceGraphic, fortitudeGraphic, selfControlGraphic, intimidationGraphic, persuasionGraphic,perceptionGraphic, subterfugeGraphic;
        dynamicStatGraphic resistanceGraphic2, fortitudeGraphic2, selfControlGraphic2, intimidationGraphic2, persuasionGraphic2,perceptionGraphic2, subterfugeGraphic2;
        List<customButton> argButtons = new List<customButton>();

        resultStatGraphic resultGraphics;

        IMatchController myController;
        Match myMatch;

        public CharacterData Character
        {
            get { return Player1 ? MyMatch.Player1.Character : MyMatch.Player2.Character; }
        }
        public CharacterData Opponent
        {
            get { return Player1 ? MyMatch.Player2.Character : MyMatch.Player1.Character; }
        }
        public string playerName
        {
            get { return Player1 ? this.MyMatch.Player1.PlayerName : MyMatch.Player2.PlayerName; }
        }
        Match MyMatch
        {
            get
            {
                return myController.Match;
            }
        }
        bool Player1
        {
            get
            {
                return myController.IsPlayerOne;
            }
        }
        CharacterData WorldCharacter
        {
            get { return MyMatch.World; }
        }
        public IMatchController Controller
        {
            get { return myController; }
            set { myController = value; }
        }

        
        
        
        public simpleOpenGlView MyView
        {
            get { return myView; }
        }

        /// <summary>
        /// Builds the window and initializes it's private objects, like the view, drawer object, drawables,etc
        /// </summary>
        public DisputeOpenGLView()
        {
            InitializeComponent();
        }
        void addButtonsToList()
        {
            foreach (Control c in grpArguments.Controls)
            {
                if (c is customButton)
                    argButtons.Add((customButton)c);
            }
        }

        /// <summary>
        /// Sets the background colors for all buttons, using the colors for the appropriate properties
        /// of the argument carried out by every button
        /// </summary>
        void setButtonBackgrounds()
        {
            Argument arg=null;

            foreach (customButton b in argButtons)
            {
                //Get argument that matches this button's name
                arg=(from p in MyMatch.PossibleArguments
                     where p.ToString().Contains(b.Name.Substring(3))
                     select p).First();

                //Get defender and attacker properties for found argument
                string def = arg.DefenderCheckProperty, att = arg.AttackerCheckProperty;
                //SOme properties might not have def or att, so only set colors for those
                //that have it (i.e. for those with not null att or def)
                if (def != null && att != null)
                {
                    dynamicStatGraphic defGraphic, attGraphic;
                    defGraphic = getStatGraphic(def);
                    attGraphic = getStatGraphic(att);
                    b.setBackgroundColors(attGraphic.Color, defGraphic.Color);
                }
            }
            this.btnConvince.setBackgroundColors(Color.Gray, Color.Blue);
        }

        /// <summary>
        /// Given a string name for a stat, returns the appropriate statGraphic
        /// </summary>
        /// <param name="name">stat name</param>
        /// <returns>statGraphic with that name</returns>
        dynamicStatGraphic getStatGraphic(string name)
        {
            name = name.Replace(" ", "");
            propertyNames p = (propertyNames)Enum.Parse(typeof(propertyNames),name);
            switch (p)
            {
                case propertyNames.Fortitude:
                    return fortitudeGraphic;
                case propertyNames.Resistance:
                    return resistanceGraphic;
                case propertyNames.SelfControl:
                    return selfControlGraphic;
                case propertyNames.Perception:
                    return perceptionGraphic;
                case propertyNames.Persuasion:
                    return persuasionGraphic;
                case propertyNames.Subterfuge:
                    return subterfugeGraphic;
                case propertyNames.Intimidation:
                    return intimidationGraphic;
                default:
                    throw new Exception("Bad Property Name in getStatGraphic");
            }
        }

        #region DRAWABLE_CREATION
        void createBackground()
        {
            wallObj background = new wallObj(new pointObj(leftEdge, bottomEdge, 0), mapHeight, mapWidth, squareSize, Common.planeOrientation.Z, Common.colorGreen, Common.colorBlack);
            Tile[,] tiles = Common.createTileArray(mapWidth, mapHeight, background.createTiles(), Common.colorGreen, squareSize);
            foreach (Tile t in tiles)
                world.add(t);
        }
        /// <summary>
        /// Create graphics for roll animation
        /// </summary>
        void createResultGraphics()
        {
            int start = centerX + centerSpace;
            resultGraphics = new resultStatGraphic(6, squareSize, Common.colorToArray(Color.Red), new pointObj(start * squareSize, centerY * squareSize, 0));
            resultGraphics.BonusColor = Common.colorToArray(Color.Yellow);
            resultGraphics.BonusValue = 2;
            resultGraphics.RollColor = Common.colorToArray(Color.Green);
            resultGraphics.RollValue = 1;
            resultGraphics.resetStacks();
            resultGraphics.Visible = false;

            world.add(resultGraphics);
        }
        /// <summary>
        /// Create graphics for player properties
        /// </summary>
        void createPropertyGraphics()
        {            
            fortitudeGraphic = new dynamicStatGraphic(getValue(propertyNames.Fortitude), fortitudeLeftEdge, fortitudeBottomEdge, squareSize, Common.colorToArray(fortitudeColor), center) { BonusColor = Common.colorOrange, RollColor = Common.colorGreen,BonusValue=1,RollValue=1 };
            fortitudeGraphic.resetStacks();
            fortitudeGraphic.Property = propertyNames.Fortitude;

            resistanceGraphic = new dynamicStatGraphic(getValue(propertyNames.Resistance), resistanceLeftEdge, resistanceBottomEdge, squareSize,
                    Common.colorToArray(resistanceColor), center);
            resistanceGraphic.resetStacks();
            resistanceGraphic.Property = propertyNames.Resistance;

            selfControlGraphic = new dynamicStatGraphic(getValue(propertyNames.SelfControl), selfControlLeftEdge, selfControlBottomEdge, squareSize,
                    Common.colorToArray(selfControlColor), center);
            selfControlGraphic.resetStacks();
            selfControlGraphic.Property = propertyNames.SelfControl;

            persuasionGraphic = new dynamicStatGraphic(getValue(propertyNames.Persuasion), skillLeftEdge, persuasionBottomEdge, squareSize, Common.colorToArray(persuasionColor), center) { BonusColor = Common.colorToArray(Color.DarkBlue), RollColor = Common.colorBrown, BonusValue = 1, RollValue = 1 };
            persuasionGraphic.resetStacks();
            persuasionGraphic.Property = propertyNames.Persuasion;

            perceptionGraphic = new dynamicStatGraphic(getValue(propertyNames.Perception), skillLeftEdge, perceptionBottomEdge, squareSize, Common.colorToArray(perceptionColor), center) { BonusColor = Common.colorToArray(Color.DarkBlue), RollColor = Common.colorBrown, BonusValue = 1, RollValue = 1 };
            perceptionGraphic.resetStacks();
            perceptionGraphic.Property = propertyNames.Perception;

            intimidationGraphic = new dynamicStatGraphic(getValue(propertyNames.Intimidation), skillLeftEdge, intimidationBottomEdge, squareSize, Common.colorToArray(intimidationColor), center) { BonusColor = Common.colorToArray(Color.DarkBlue), RollColor = Common.colorBrown, BonusValue = 1, RollValue = 1 };
            intimidationGraphic.resetStacks();
            intimidationGraphic.Property = propertyNames.Intimidation;

            subterfugeGraphic = new dynamicStatGraphic(getValue(propertyNames.Subterfuge), skillLeftEdge, subterfugeBottomEdge, squareSize, Common.colorToArray(subterfugeColor), center) { BonusColor = Common.colorToArray(Color.DarkBlue), RollColor = Common.colorBrown, BonusValue = 1, RollValue = 1 };
            subterfugeGraphic.resetStacks();
            subterfugeGraphic.Property = propertyNames.Subterfuge;

            world.add(persuasionGraphic);
            world.add(subterfugeGraphic);
            world.add(intimidationGraphic);
            world.add(perceptionGraphic);
            world.add(selfControlGraphic);
            world.add(resistanceGraphic);
            world.add(fortitudeGraphic);
            
        }
        /// <summary>
        /// Creates graphics for opponenet properties
        /// </summary>
        void createOpponentGraphics()
        {
            fortitudeGraphic2 = new dynamicStatGraphic(10, -panelOffset * squareSize - fortitudeLeftEdge, fortitudeBottomEdge, squareSize, Common.colorToArray(fortitudeColor), opCenter) { BonusColor = Common.colorOrange, RollColor = Common.colorGreen, BonusValue = 1, RollValue = 1 };
            fortitudeGraphic2.resetStacks();

            resistanceGraphic2 = new dynamicStatGraphic(10, -panelOffset * squareSize - resistanceLeftEdge, resistanceBottomEdge, squareSize, Common.colorToArray(resistanceColor), opCenter) { BonusColor = Common.colorOrange, RollColor = Common.colorGreen, BonusValue = 1, RollValue = 1 };
            resistanceGraphic2.resetStacks();

            selfControlGraphic2 = new dynamicStatGraphic(10, -panelOffset * squareSize - selfControlLeftEdge, selfControlBottomEdge, squareSize, Common.colorToArray(selfControlColor), opCenter) { BonusColor = Common.colorOrange, RollColor = Common.colorGreen, BonusValue = 1, RollValue = 1 };
            selfControlGraphic2.resetStacks();

            persuasionGraphic2 = new dynamicStatGraphic(10, -panelOffset * squareSize - skillLeftEdge, persuasionBottomEdge, squareSize, Common.colorToArray(persuasionColor), opCenter) { BonusColor = Common.colorToArray(Color.DarkBlue), RollColor = Common.colorBrown, BonusValue = 1, RollValue = 1 };
            persuasionGraphic2.resetStacks();

            perceptionGraphic2 = new dynamicStatGraphic(10, -panelOffset * squareSize - skillLeftEdge, perceptionBottomEdge, squareSize, Common.colorToArray(perceptionColor), opCenter) { BonusColor = Common.colorToArray(Color.DarkBlue), RollColor = Common.colorBrown, BonusValue = 1, RollValue = 1 };
            perceptionGraphic2.resetStacks();

            intimidationGraphic2 = new dynamicStatGraphic(10, -panelOffset * squareSize - skillLeftEdge, intimidationBottomEdge, squareSize, Common.colorToArray(intimidationColor), opCenter) { BonusColor = Common.colorToArray(Color.DarkBlue), RollColor = Common.colorBrown, BonusValue = 1, RollValue = 1 };
            intimidationGraphic2.resetStacks();

            subterfugeGraphic2 = new dynamicStatGraphic(10, -panelOffset * squareSize - skillLeftEdge, subterfugeBottomEdge, squareSize, Common.colorToArray(subterfugeColor), opCenter) { BonusColor = Common.colorToArray(Color.DarkBlue), RollColor = Common.colorBrown, BonusValue = 1, RollValue = 1 };
            subterfugeGraphic2.resetStacks();

            world.add(persuasionGraphic2);
            world.add(subterfugeGraphic2);
            world.add(intimidationGraphic2);
            world.add(perceptionGraphic2);
            world.add(selfControlGraphic2);
            world.add(resistanceGraphic2);
            world.add(fortitudeGraphic2);
        }
        double getValue(propertyNames pName)
        {
            string name = null;

            if(pName.Equals(null))
                return Double.NaN;

            if (pName.Equals(propertyNames.SelfControl))
                name = "Self Control";
            else
                name = pName.ToString();

            return Character.getProperty(name);
        }
        double getOppValue(propertyNames pName)
        {
            string name = null;

            if (pName.Equals(null))
                return Double.NaN;

            if (pName.Equals(propertyNames.SelfControl))
                name = "Self Control";
            else
                name = pName.ToString();

            return Opponent.getProperty(name);
        }
        #endregion


        #region DRAWING
        /// <summary>
        /// Initializes graphics (all of them, player 1s,player 2's and result)
        /// </summary>
        void initWorld()
        {
            //createBackground();
            createPropertyGraphics();
            createOpponentGraphics();
            createResultGraphics();
        }
        /// <summary>
        /// Draws the scene if the view is active
        /// </summary>
        new void drawingLoop()
        {
            if (!this.IsDisposed)
            {           
                drawScene();
            }
        }
        /// <summary>
        /// Draws the roll animations
        /// </summary>
        /// <param name="atacker">graphic for attacker</param>
        /// <param name="defender">graphic for defneder</param>
        void drawRoll(dynamicStatGraphic atacker,dynamicStatGraphic defender=null)
        {
            //Show result graphic
            resultGraphics.Visible = true;

            //Draw translating of stat
            while (!atacker.translateNext())
                drawScene();

           
            drawScene();
            Thread.Sleep(20);

            //Take blocks from both sides
            while (!resultGraphics.switchNext())
            {
                //Hide the next block in stat graphic, stop hiding if we run out of blocks
                if (atacker.hideNext())
                {                    
                    break;
                }
                drawScene();
                Thread.Sleep(100);
            }
            Thread.Sleep(1000);
            

            //Draw reset stat and hide result
            resultGraphics.Visible = false;
            resultGraphics.resetHide();
            atacker.resetHide();
            atacker.resetTranslation();
            drawScene();
        }
        /// <summary>
        /// Sets up the scene, draws it, then flushes and refreses view
        /// </summary>
        void drawScene()
        {            
            MyView.setupScene();            
            myDrawingObject.drawWorld(world);
            MyView.flushScene();
            this.Refresh();
        }
        /// <summary>
        /// Create floor objects
        /// </summary>
        

        #endregion




        void IMatchView.start()
        {
             MyView.InitializeContexts();
            
            myNavigator.MyView = MyView;
            myNavigator.MyWindowOwner = this;
            myNavigator.Orientation = Common.planeOrientation.Z;
            center = new pointObj(centerX* squareSize, centerY * squareSize, 0);
            opCenter = new pointObj((centerX + 3) * squareSize, centerY * squareSize, 0);
            initWorld();
            this.WindowState = FormWindowState.Maximized;
            MyView.Height = this.Height;
            this.MyView.Dock = DockStyle.Fill;
            
            grpArguments.Dock = DockStyle.Right;
            this.MyView.setCameraView(simpleOpenGlView.VIEWS.FrontUp);

            addButtonsToList();
            setButtonBackgrounds();
            this.Show();
            //this.myView.setCameraView(simpleOpenGlView.VIEWS.Top);
            drawingLoop();
        
        }

        void IMatchView.stop()
        {
            myController.MessageSentFromView(MatchViewMessage.Close, null, this);
        }

        void IMatchView.updateMatch(Match match)
        {
            
        }

        SOM IMatchView.selectSoM(SOM allowed, Result result)
        {
            throw new NotImplementedException();
        }

        IMatchController IMatchView.Controller
        {
            get
            {
                return myController;
            }
            set
            {
                myController = value;
            }
        }

        void IMatchView.gameOver(bool won)
        {
            throw new NotImplementedException();
        }

        void IFeedbackWriter.WriteLine(string line)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            drawingLoop();
        }

        simpleOpenGlView ICanvasWindow.getView()
        {
            throw new NotImplementedException();
        }

        IWorld ICanvasWindow.getMap()
        {
            throw new NotImplementedException();
        }

        void ICanvasWindow.refresh()
        {
            drawingLoop();
        }

        private void btnManipulate_Click(object sender, EventArgs e)
        {
            resistanceGraphic.Value = 1;
            (this as ICanvasWindow).refresh();
        }

        private void btnConvince_Click(object sender, EventArgs e)
        {
            drawRoll(persuasionGraphic);
        }
    }

    

    
}

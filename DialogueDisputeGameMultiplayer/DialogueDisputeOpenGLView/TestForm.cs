using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Drawables;

namespace DialogueDisputeOpenGLView
{
    public partial class TestForm : Form,ICanvasWindow
    {
        Common myDrawer = new Common();
        IWorld world = new DisputeOpenGLWorld();
        Common.planeOrientation orientation = Common.planeOrientation.Z;
        int view = 0;
        int tileSize = 20;

        public TestForm()
        {
            InitializeComponent();
            myView.InitializeContexts();
            this.WindowState = FormWindowState.Maximized;
            myNavigator.MyView = this.myView;
            myNavigator.Orientation = orientation;
            myNavigator.MyWindowOwner = this;
            myView.Dock = DockStyle.Fill;
            draw();
        }

        void draw()
        {
            myView.setCameraView((Canvas_Window_Template.simpleOpenGlView.VIEWS)view);
            LowBlock tile = new LowBlock(new pointObj(0, 0, 0), 2*tileSize, Common.colorRed,Common.colorGreen);
            cubeObj cube = new cubeObj(new pointObj(100, 100, 0), tileSize, Common.colorRed, Common.colorBlack);
            Tile[,] wall = Tile.createTileArray(new pointObj(-300, -300, 0), 30, 30, orientation, Common.colorGreen, tileSize);
            parallepiped p = new parallepiped(new pointObj(-300, -300, 0), 30, 30, 100, Common.colorRed, null);
            bool skip = false;
            Random r = new Random();
            world.getEntities().Clear();
            foreach (Tile t in wall)
            {
                t.MyOutlineColor = Common.colorRed;
                world.add(t);
            }
            world.add(p);
            world.add(cube);
            world.add(tile);
            
            myView.setupScene();
            myDrawer.drawWorld(world);
            myView.flushScene();
            this.Refresh();
        }
        private void TestForm_Load(object sender, EventArgs e)
        {
            draw();
        }

        private void TestForm_Resize(object sender, EventArgs e)
        {
            draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            view++;
            view = view % 4;
            draw();
        }

        Canvas_Window_Template.simpleOpenGlView ICanvasWindow.getView()
        {
            throw new NotImplementedException();
        }


        IWorld ICanvasWindow.getMap()
        {
            throw new NotImplementedException();
        }

        void ICanvasWindow.refresh()
        {
            draw();
        }
    }
}

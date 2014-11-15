using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Interfaces;

namespace DialogueDisputeOpenGLView
{
    public class DisputeOpenGLWorld:IWorld
    {
        List<IDrawable> drawables = new List<IDrawable>();
        List<IDrawable> IWorld.getEntities()
        {
            return drawables;
        }


        void IWorld.add(IDrawable d)
        {
            drawables.Add(d);
        }

        IDrawable IWorld.remove(IDrawable d)
        {
            return drawables.Remove(d)?d:null;
        }
    }
}

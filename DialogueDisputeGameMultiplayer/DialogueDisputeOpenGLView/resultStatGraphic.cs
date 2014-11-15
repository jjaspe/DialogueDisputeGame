using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Basic_Drawing_Functions;
using Canvas_Window_Template.Interfaces;

namespace DialogueDisputeOpenGLView
{
    public class resultStatGraphic:statGraphic
    {
        int index=0;

        public resultStatGraphic(int value, int leftEdge, int bottomEdge, int squareSize, float[] color) :
        base(value, leftEdge, bottomEdge, squareSize, color)
        {
        }

        public resultStatGraphic(int value, int squareSize, float[] color, IPoint origin) :
        base(value, squareSize, color, origin)
        {
        }

        /// <summary>
        /// Recreates all 3 stacks, and sets bonus and roll to visible=true
        /// </summary>
        public new void resetStacks()
        {
            base.resetStacks();
            
            resetHide();
        }

        /// <summary>
        /// Switches the next block from visible to not visible or viceversa
        /// </summary>
        /// <returns> true if it switched the last element, false otherwise</returns>
        public bool switchNext()
        {
            if (index < value)
                valueBlocks[index].Visible = !valueBlocks[index].Visible;
            else if (index < BonusValue + value)
                bonusBlocks[(int)(index - Value)].Visible = !bonusBlocks[(int)(index - value)].Visible;
            else
                rollBlocks[(int)(index - Value - BonusValue)].Visible = !rollBlocks[(int)(index - Value - BonusValue)].Visible;
            index++;

            if (index == Value+BonusValue+RollValue)
            {
                resetHide();
                return true;
            }
            else
                return false;
        }

        public void resetHide()
        {
            foreach (parallepiped p in this.valueBlocks)
                p.Visible = true;
            foreach (parallepiped p in this.bonusBlocks)
                p.Visible = true;
            foreach (parallepiped p in this.rollBlocks)
                p.Visible = true;

            index = 0;
        }

    }
}

using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Basic_Drawing_Functions;
using System.Collections.Generic;
using DialogueDisputeOpenGLView;

public class dynamicStatGraphic : statGraphic
{
    int translationIndex = 0,hideIndex=0;
    IPoint nextOrigin;
    List<parallepiped> nextBlocks = new List<parallepiped>();

    public dynamicStatGraphic(double value, int leftEdge, int bottomEdge, int squareSize, float[] color, IPoint nextOrigin) :
        base(value, leftEdge, bottomEdge, squareSize, color)
    {
        this.nextOrigin = nextOrigin;
        translationIndex = 0;
        
    }
    public dynamicStatGraphic(double value, int squareSize, float[] color, IPoint origin, IPoint nextOrigin) :
        base(value, squareSize, color, origin)
    {
        this.nextOrigin = nextOrigin;
        translationIndex = 0;
    }

    /// <summary>
    /// Moves blocks from original location to new location
    /// </summary>
    /// <returns>true if it was the last value, false otherwise</returns>
    public bool translateNext()
    {
        if (translationIndex == value+BonusValue+RollValue)
        {
            translationIndex = 0;
            return true;
        }
        else
        {
            //See which list we are removing blocks from at this time
            List<parallepiped> correctList = null;
            if (translationIndex < value)
                correctList = valueBlocks;
            else if (translationIndex < BonusValue + value)
                correctList = bonusBlocks;
            else
                correctList = rollBlocks;

            moveNextBlock(correctList, nextBlocks);
            translationIndex++;     
            return false;
        }
    }
    /// <summary>
    /// Moves last block from source to dest
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dest"></param>
    void moveNextBlock(List<parallepiped> source, List<parallepiped> dest)
    {
        if (source.Count < 1)
            return;
        parallepiped p = source[0];
        if (p != null)
        {
            double[] position = (p as IDrawable).getPosition();
            //Get old shifts from origin and add them to new origin 
            //I.E. if this p was 3 units up, 2 left from old origin, make it 3 up, 2 left from new origin
            position[0] = nextOrigin.X + (position[0] - Origin.X);
            position[1] = nextOrigin.Y + (position[1] - Origin.Y);
            position[2] = nextOrigin.Z + (position[2] - Origin.Z);
            //Remove from original list
            source.Remove(p);
            //Now make p visible in case it was a bonus or roll block
            p.Visible = true;
            //Add to new list with different origin
            (p as IDrawable).setPosition(new pointObj(position[0], position[1], position[2]));
            dest.Add(p);
            
        }
    }
    /// <summary>
    /// Returns last block from source to dest, and depending and sets its visibility using 
    /// visible
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dest"></param>
    /// <param name="visible"></param>
    void returnNextBlock(List<parallepiped> source, List<parallepiped> dest,bool visible)
    {
        //Get block from nextBlocks 
        parallepiped p = source[0];

        if (p != null)
        {
            double[] position = (p as IDrawable).getPosition();
            //Get old shifts from origin and add them to new origin 
            //I.E. if this p was 3 units up, 2 left from old origin, make it 3 up, 2 left from new origin
            position[0] = Origin.X + (position[0] - nextOrigin.X);
            position[1] = Origin.Y + (position[1] - nextOrigin.Y);
            position[2] = Origin.Z + (position[2] - nextOrigin.Z);
            //Remove from original list
            source.Remove(p);
            //Add to original list with different origin
            (p as IDrawable).setPosition(new pointObj(position[0], position[1], position[2]));
            p.Visible = visible;
            dest.Add(p);
        }
    }
    /// <summary>
    /// Puts all blocks back in original position
    /// </summary>
    public void resetTranslation()
    {
        int index=0;
        List<parallepiped> correctList = null;
        bool visible = true;
        while (nextBlocks.Count > 0)
        {
            //See which list p belongs to
            if (index < value)
            {
                visible = true;
                correctList = valueBlocks;
            }
            else if (index < value + BonusValue)
            {
                visible = false;
                correctList = bonusBlocks;
            }
            else
            {
                visible = false;
                correctList = rollBlocks;
            }
            returnNextBlock(nextBlocks, correctList,visible);
            index++;
        }
    }

    /// <summary>
    /// Hides the last block from the last stack
    /// </summary>
    /// <returns></returns>
    public bool hideNext()
    {
        nextBlocks[hideIndex].Visible = !nextBlocks[hideIndex].Visible;
        hideIndex++;

        if (hideIndex == Value+RollValue+BonusValue)
        {
            resetHide();
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Unhides all valueBlocks
    /// </summary>
    public void resetHide()
    {
        foreach (parallepiped p in valueBlocks)
            p.Visible = true;
        hideIndex = 0;
    }

    public new void resetStacks()
    {
        base.resetStacks();
        hideIndex = 0;
    }
    public override void draw()
    {
        if (Visible)
        {
            base.draw();
            foreach (parallepiped p in nextBlocks)
                (p as IDrawable).draw();
        }
    }

}
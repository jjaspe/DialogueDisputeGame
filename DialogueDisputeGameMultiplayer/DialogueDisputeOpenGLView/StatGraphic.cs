using Canvas_Window_Template.Interfaces;
using Canvas_Window_Template.Basic_Drawing_Functions;
using System.Collections.Generic;
using DialogueDisputeOpenGLView;
using System;

/// <summary>
/// Represents a stat by drawing a stack of blocks, each with width and length of size=squareSize, and height=sizePerValue
/// </summary>
public class statGraphic : IDrawable
{
    public static int max = 40;
    protected List<parallepiped> valueBlocks = new List<parallepiped>();
    protected List<parallepiped> bonusBlocks = new List<parallepiped>();
    protected List<parallepiped> rollBlocks = new List<parallepiped>();
    protected IPoint[] origins = new IPoint[max];
    private propertyNames property;

    public propertyNames Property
    {
        get { return property; }
        set { property = value; }
    }
    IPoint origin;
    float[] bonusColor, rollColor;
    bool visible = true;
    string name;
    protected double value = 0, bonusValue, rollValue, heightPerColumn, squareSize;
    double sizePerValue, xw, yw, zw;
    Common.planeOrientation or = Common.planeOrientation.Z;
    float[] color;

    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }


    public double RollValue
    {
        get { return rollValue; }
        set { rollValue = value; }
    }
    public double BonusValue
    {
        get { return bonusValue; }
        set { bonusValue = value; }
    }
    public float[] BonusColor
    {
        get { return bonusColor; }
        set { bonusColor = value; }
    }
    public float[] RollColor
    {
        get { return rollColor; }
        set { rollColor = value; }
    }

    public IPoint Origin
    {
        get { return origin; }
        set { origin = value; createStacks(); }
    }
    

    public float[] Color
    {
        get { return color; }
        set { color = value; }
    }

    public double Value
    {
        get { return this.value; }
        set { this.value = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    /// Creates as stat graphic of height max at leftEdge,bottomEdge, with direction depending on 
    /// instance variable or
    /// </summary>
    /// <param name="value"></param>
    /// <param name="leftEdge"></param>
    /// <param name="bottomEdge"></param>
    /// <param name="cubesPerColumn"></param>
    /// <param name="squareSize"></param>
    public statGraphic(double value, int leftEdge, int bottomEdge, int squareSize, float[] color)
    {
        this.Color = color;
        this.squareSize = squareSize;
        this.value = value;
        BonusColor = color;
        RollColor = color;
        BonusValue = 0;
        RollValue = 0;

        switch (or)
        {
            case Common.planeOrientation.X:
                this.origin = new pointObj(0, leftEdge + squareSize, bottomEdge + squareSize);
                break;
            case Common.planeOrientation.Y:
                this.origin = new pointObj(leftEdge + squareSize, 0, bottomEdge + squareSize);
                break;
            case Common.planeOrientation.Z:
                this.origin = new pointObj(leftEdge + squareSize, bottomEdge + squareSize, 0);
                break;
        }

        createStacks();
    }
    /// <summary>
    /// Creates a statGraphic at origin, of size squareSize
    /// </summary>
    /// <param name="value"></param>
    /// <param name="squareSize"></param>
    /// <param name="color"></param>
    /// <param name="origin"></param>
    public statGraphic(double value, int squareSize, float[] color, IPoint origin)
    {
        this.Color = color;
        this.squareSize = squareSize;
        this.value = value;
        BonusColor = color;
        RollColor = color;
        BonusValue = 0;
        RollValue = 0;
        if (origin == null)
            throw new NullReferenceException();
        this.origin = origin;
        createStacks();
    }

    
    void createStacks()
    {
        generateOrigins();
        setBlockDimensions();
        resetStacks();        
    }
    public void resetStacks()
    {
        //Clear all stacks first
        valueBlocks.Clear();
        bonusBlocks.Clear();
        rollBlocks.Clear();

        for (int i = 0; i < value; i++)
        {
            valueBlocks.Add(new parallepiped(origins[i], xw, yw, zw, Color));
        }
        for (int i = (int)value; i < value + bonusValue; i++)
        {
            bonusBlocks.Add(new parallepiped(origins[i], xw, yw, zw, BonusColor) { Visible = false });
        }
        for (int i = (int)(value + bonusValue); i < value + bonusValue + rollValue; i++)
        {
            rollBlocks.Add(new parallepiped(origins[i], xw, yw, zw, RollColor) { Visible = false });
        }
    }
    
    /// <summary>
    /// Sets block dimensions based on orientation, squareSize and sizePerValue
    /// </summary>
    void setBlockDimensions()
    {
        switch (or)
        {
            case Common.planeOrientation.X:
                xw = sizePerValue;
                break;
            case Common.planeOrientation.Y:
                yw = sizePerValue;
                break;
            case Common.planeOrientation.Z:
                zw = sizePerValue;
                break;
        }
    }
    /// <summary>
    /// Creates all the origins for possible blocks of this stat
    /// </summary>
    void generateOrigins()
    {
        int index = 0;//origin index

        //Get the number of columns needed, the last one might not get completely filled
        int neededColumns = DisputeOpenGLView.columnsPerStat;

        //This is how many 1 point values go in one bar
        int valuesPerBar = max % DisputeOpenGLView.columnsPerStat == 0 ?
            max / neededColumns :
            max / neededColumns + 1;

        //How much in pixels one value point is worth
        sizePerValue = DisputeOpenGLView.cubesPerBar * squareSize / valuesPerBar;

        double x = Origin.X, y = Origin.Y, z = Origin.Z;//Helper variables for easier typing
        xw = squareSize;
        yw = squareSize;
        zw = squareSize;

        
        //Now let's create all the parallepipes and push them 
        // onto the stack, using these measures(width=length=squareSize,height=sizePerValue)      
        for (int j = 0; j < neededColumns; j++)
        {
            for (int i = 0; i < valuesPerBar && j * valuesPerBar + i < max; i++)
            {
                switch (or)
                {
                    case Common.planeOrientation.X:
                        x = Origin.X + i * sizePerValue;
                        y = Origin.Y + j * squareSize;
                        z = Origin.Z;
                        xw = sizePerValue;
                        break;
                    case Common.planeOrientation.Y:
                        x = Origin.X + j * squareSize;
                        y = Origin.Y + i * sizePerValue;
                        z = Origin.Z;
                        yw = sizePerValue;
                        break;
                    case Common.planeOrientation.Z:
                        x = Origin.X + j * squareSize;
                        y = Origin.Y;
                        z = Origin.Z + i * sizePerValue;
                        zw = sizePerValue;
                        break;
                }
                origins[index++] = new pointObj(x, y, z);
                //valueBlocks.Add(new parallepiped(new pointObj(x, y, z), xw, yw, zw, Color));
            }
        }
    }
    public virtual void draw()
    {
        if (Visible)
        {
            foreach (parallepiped p in valueBlocks)
                (p as IDrawable).draw();
            foreach (parallepiped p in bonusBlocks)
                (p as IDrawable).draw();
            foreach (parallepiped p in rollBlocks)
                (p as IDrawable).draw();            
        }

    }

    int IDrawable.getId()
    {
        return 0;
    }

    double[] IDrawable.getPosition()
    {
        return origin.toArray();
    }

    void IDrawable.setPosition(IPoint newPosition)
    {
        origin = newPosition;
    }
}
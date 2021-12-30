namespace NovaEngine.Common.Windows.Native;

/// <summary>Contains a rectangle defined by its upper-left and lower-right corners.</summary>
public struct Rectangle
{
    /*********
    ** Fields
    *********/
    /// <summary>The x-coordinate of the upper-left corner of the rectangle.</summary>
    public int Left;

    /// <summary>The y-coordinate of the upper-left corner of the rectangle.</summary>
    public int Top;

    /// <summary>The x-coordinate of the lower-right corner of the rectangle.</summary>
    public int Right;

    /// <summary>The y-coordinate of the lower-right corner of the rectangle.</summary>
    public int Bottom;


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="topLeft">The coordinates of the upper-left corner of the rectangle.</param>
    /// <param name="bottomRight">The coordinates of the lower-right corner of the rectangle.</param>
    public Rectangle(Vector2I topLeft, Vector2I bottomRight)
    {
        Left = topLeft.X;
        Top = topLeft.Y;
        Right = bottomRight.X;
        Bottom = bottomRight.Y;
    }
}

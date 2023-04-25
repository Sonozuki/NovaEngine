namespace NovaEngine.Maths;

/// <summary>Represents a rectangle.</summary>
public struct Rectangle : IEquatable<Rectangle>
{
    /*********
    ** Fields
    *********/
    /// <summary>The X position of the rectangle.</summary>
    public float X;

    /// <summary>The Y position of the rectangle.</summary>
    public float Y;

    /// <summary>The width of the rectangle.</summary>
    public float Width;

    /// <summary>The height of the rectangle.</summary>
    public float Height;


    /*********
    ** Properties
    *********/
    /// <summary>The right position of the rectangle.</summary>
    public float Right => X + Width;

    /// <summary>The bottom position of the rectangle.</summary>
    public float Bottom => Y + Height;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="x">The X position of the rectangle.</param>
    /// <param name="y">The Y position of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    public Rectangle(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Checks two rectangles for equality.</summary>
    /// <param name="other">The rectangle to check equality with.</param>
    /// <returns><see langword="true"/> if the rectangles are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Rectangle other) => this == other;

    /// <summary>Checks the rectangle and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the rectangle and object are equal; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is Rectangle state && this == state;

    /// <summary>Retrieves the hash code of the rectangle.</summary>
    /// <returns>The hash code of the rectangle.</returns>
    public override int GetHashCode() => (X, Y, Width, Height).GetHashCode();


    /*********
    ** Operators
    *********/
    /// <summary>Checks two rectangles for equality.</summary>
    /// <param name="left">The first rectangle.</param>
    /// <param name="right">The second rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Rectangle left, Rectangle right) =>
        left.X == right.X &&
        left.Y == right.Y &&
        left.Width == right.Width &&
        left.Height == right.Height;

    /// <summary>Checks two rectangles for inequality.</summary>
    /// <param name="left">The first rectangle.</param>
    /// <param name="right">The second rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Rectangle left, Rectangle right) => !(left == right);
}

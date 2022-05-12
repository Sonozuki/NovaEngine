namespace NovaEngine.Content.Models.Font;

// TODO: move this from font to the main engine
/// <summary>Represents a rectangle.</summary>
internal struct Rectangle
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
    ** Public Methods
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
}
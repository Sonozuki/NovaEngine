namespace NovaEngine.Content.Models.Font;

// TODO: maybe move this from font to the main engine (?)
/// <summary>Represents a colour with four <see langword="float"/> channels (R, G, B, A).</summary>
internal struct Colour128
{
    /*********
    ** Fields
    *********/
    /// <summary>The red channel.</summary>
    public float R;

    /// <summary>The green channel.</summary>
    public float G;

    /// <summary>The blue channel.</summary>
    public float B;

    /// <summary>The alpha channel.</summary>
    public float A;


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="r">The red channel.</param>
    /// <param name="g">The green channel.</param>
    /// <param name="b">The blue channel.</param>
    /// <param name="a">The alpha channel.</param>
    public Colour128(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}
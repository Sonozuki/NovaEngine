namespace NovaEngine.ContentPipeline.Font;

/// <summary>Represents a component in a composite glyph.</summary>
internal struct CompositeGlyphComponent
{
    /*********
    ** Fields
    *********/
    /// <summary>The index of the component glyph.</summary>
    public ushort GlyphIndex;

    /// <summary>The matrix for the component glyph.</summary>
    public Matrix3x2<float> Matrix = new(1, 0, 0, 1, 0, 0);


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="glyphIndex">The index of the component glyph.</param>
    public CompositeGlyphComponent(ushort glyphIndex)
    {
        GlyphIndex = glyphIndex;
    }
}

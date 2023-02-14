namespace NovaEngine.Content.Models.Font;

/// <summary>Represents a component for a compound glyph.</summary>
internal struct CompoundGlyphComponent
{
    /*********
    ** Fields
    *********/
    /// <summary>The index of the component glyph.</summary>
    public ushort GlyphIndex;

    /// <summary>The matrix for the component glyph.</summary>
    public Matrix3x2<float> Matrix;
}

namespace NovaEngine.ContentPipeline.Font.GlyphParsers;

/// <summary>Represents a glyph outline parser.</summary>
internal abstract class GlyphParserBase
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Parses a glyph from the font.</summary>
    /// <param name="glyphIndex">The index of the glyph to parse.</param>
    /// <returns>The parsed glyph.</returns>
    public abstract Glyph Parse(ushort glyphIndex);
}

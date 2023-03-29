namespace NovaEngine.ContentPipeline.Font;

/// <summary>The types of glyph outlines in an OTF file.</summary>
internal enum OutlineType
{
    /// <summary>The font uses TrueType outlines.</summary>
    TrueType,

    /// <summary>The font uses Compact Font Format 1 outlines.</summary>
    CFF,

    /// <summary>The font uses Compact Font Format 2 outlines.</summary>
    CFF2
}

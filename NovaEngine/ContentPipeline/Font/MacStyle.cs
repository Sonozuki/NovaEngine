namespace NovaEngine.ContentPipeline.Font;

/// <summary>The mac styles of a font.</summary>
[Flags]
internal enum MacStyle : ushort
{
    /// <summary>The glyphs are emboldened.</summary>
    Bold = 1 << 0,

    /// <summary>The font contains italic or oblique glyphs.</summary>
    Italic = 1 << 1,

    /// <summary>The glyphs are underlined.</summary>
    Underline = 1 << 2,

    /// <summary>The glyphs are outlines (hollow).</summary>
    Outline = 1 << 3,

    /// <summary>The glyphs are shadows.</summary>
    Shadow = 1 << 4,

    /// <summary>The glyphs are condensed (narrow).</summary>
    Condensed = 1 << 5,

    /// <summary>The glyphs are extended.</summary>
    Extended = 1 << 6

    // 7 - 15 reserved
}

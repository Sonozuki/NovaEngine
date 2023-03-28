namespace NovaEngine.ContentPipeline.Font;

/// <summary>A relative change from the normal aspect ratio (width to height ratio) as specified by a font designer for the glyphs in a font.</summary>
internal enum FontWidth : ushort
{
    /// <summary>The glyphs are ultra-condensed (50% of normal).</summary>
    UltraCondensed = 1,

    /// <summary>The glyphs are extra-condensed (62.5% of normal).</summary>
    ExtraCondensed,

    /// <summary>The glyphs are condensed (75% of normal).</summary>
    Condensed,

    /// <summary>The glyphs are semi-condensed (87.5% of normal).</summary>
    SemiCondensed,

    /// <summary>The glyphs are normal.</summary>
    Normal,

    /// <summary>The glyphs are semi-expanded (112.5% of normal).</summary>
    SemiExpanded,

    /// <summary>The glyphs are expanded (125% of normal).</summary>
    Expanded,

    /// <summary>The glyphs are extra-expanded (150% of normal).</summary>
    ExtraExpanded,

    /// <summary>The glyphs are ultra-expanded (200% of normal).</summary>
    UltraExpanded
}

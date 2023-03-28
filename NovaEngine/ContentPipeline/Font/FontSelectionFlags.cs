using NovaEngine.ContentPipeline.Font.Tables;

namespace NovaEngine.ContentPipeline.Font;

/// <summary>Information concerning the nature of font patterns.</summary>
internal enum FontSelectionFlags : ushort
{
    /// <summary>The font contains italic or oblique glyphs.</summary>
    Italic = 1 << 0,

    /// <summary>Glyphs are underscored.</summary>
    Underscore = 1 << 1,

    /// <summary>Glyphs have their foreground and background reversed.</summary>
    Negative = 1 << 2,

    /// <summary>Glyphs are outlined (hollow).</summary>
    Outlined = 1 << 3,

    /// <summary>Glyphs are overstruck.</summary>
    Strikeout = 1 << 4,

    /// <summary>Glyphs are emboldened.</summary>
    Bold = 1 << 5,

    /// <summary>Glyphs are in the standard weight/style for the font.</summary>
    Regular = 1 << 6,

    /// <summary>If set, it is strongly recommended that applications use <see cref="OS2Table.TypoAscender"/> - <see cref="OS2Table.TypoDescender"/> + <see cref="OS2Table.TypoLineGap"/> as the default line spacing for the font.</summary>
    UseTypoMetrics = 1 << 7,

    /// <summary>The font has 'name' table strings consistent with a weight/width/slope family without requiring use of <see cref="NameId.WWSFamilyName"/> and <see cref="NameId.WWSSubfamilyName"/>.</summary>
    WWS = 1 << 8,

    /// <summary>Font contains oblique glyphs.</summary>
    Oblique = 1 << 9

    // 10 - 15 reserved
}

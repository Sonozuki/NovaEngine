namespace NovaEngine.ContentPipeline.Font;

/// <summary>The flags for the head table.</summary>
[Flags]
internal enum HeadTableFlags : ushort
{
    /// <summary>The baseline for the font is at y=0.</summary>
    BaselineY0 = 1 << 0,

    /// <summary>The left sidebearing point is at x=0.</summary>
    LeftSidebearingX0 = 1 << 1,

    /// <summary>Whether the instructions may depend on point size.</summary>
    DependOnPointSize = 1 << 2,

    /// <summary>Whether PPEM values should be forced to integer values for the internal scalar maths.</summary>
    ForcePPEMToInt = 1 << 3,

    /// <summary>Whether the instructions may alter advance width (the advance width may not scale linearly).</summary>
    AlterAdvanceWith = 1 << 4,

    // 5 - 10 are unused in OpenType

    /// <summary>Whether font data is "lossless" as a result of having been subject to optimising transformation and/or compression where the original font functionality and features are retained but the binary compatibility between input and output font files is not guaranteed. As a result of the applied transform, the DSIG table may also be invalidated.</summary>
    Lossless = 1 << 11,

    /// <summary>Whether the font is converted (produce compatible metrics).</summary>
    Converted = 1 << 12,

    /// <summary>Whether the font has been optimised for ClearType.</summary>
    OptimizedForClearType = 1 << 13,

    /// <summary>Whether the font is a last resort</summary>
    /// <remarks>
    /// If set, indicates that the glyphs encoded in the 'cmap' subtables are simply generic symbolic representations of code point ranges and don’t truly represent support for those code points.<br/>
    /// If unset, indicates that the glyphs encoded in the 'cmap' subtables represent proper support for those code points.
    /// </remarks>
    LastResort = 1 << 14,

    // 15 reserved
}

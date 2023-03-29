namespace NovaEngine.ContentPipeline.Font;

/// <summary>The flags used when reading a composite glyph.</summary>
[Flags]
internal enum CompositeGlyphFlags : ushort
{
    /// <summary>If set, the arguments are 16-bit; otherwise, they're 8-bit.</summary>
    Arg1And2AreWords = 1 << 0,

    /// <summary>If set, the arguments are signed xy values; otherwise, they're unsigned point numbers.</summary>
    ArgsAreXYValues = 1 << 1,

    /// <summary>If set and <see cref="ArgsAreXYValues"/> is also set, the xy values are rounded to the nearest grid line. Ignored if <see cref="ArgsAreXYValues"/> is not set.</summary>
    RoundXYToGrid = 1 << 2,

    /// <summary>There is a simple scale for the component; otherwise, scale = 1.</summary>
    WeHaveAScale = 1 << 3,

    // 4 reserved

    /// <summary>Indicates that there is at least one more glyph after this one.</summary>
    MoreComponents = 1 << 5,

    /// <summary>The X direction will use a different scale from the Y direction.</summary>
    WeHaveAnXAndYScale = 1 << 6,

    /// <summary>There is a 2x2 transformation that will be used to scale the component.</summary>
    WeHaveATwoByTwo = 1 << 7,

    /// <summary>Following the last component are instructions for the composite character.</summary>
    WeHaveInstructions = 1 << 8,

    /// <summary>If set, this forces the advance width and left side bearing (and right bearing) for the composite to be equal to those from the component glyph.</summary>
    UseMyMetrics = 1 << 9,

    /// <summary>If set, the components of the composite glyph overlap. Use of this flag is not required in OpenType — that is, it is valid to have components overlap without having this flag set.</summary>
    OverlapComposite = 1 << 10,

    /// <summary>The composite is designed to have the component offset scaled. Ignored if <see cref="ArgsAreXYValues"/> is not set.</summary>
    ScaledComponentOffset = 1 << 11,

    /// <summary>The composite is designed not to have the component offset scaled. Ignored if <see cref="ArgsAreXYValues"/> is not set.</summary>
    UnscaledComponentOffset = 1 << 12

    // 13 - 15 reserved
}

namespace NovaEngine.ContentPipeline.Models.Font;

/// <summary>The flags used when reading a compound glyph.</summary>
[Flags]
internal enum CompoundGlyphFlags : ushort
{
    /// <summary>If set, the arguments are 16-bit; otherwise, they're 8-bit.</summary>
    Arg1And2AreWords = 1 << 0,

    /// <summary>If set, the arguments are signed xy values; otherwise, they're unsigned point numbers.</summary>
    ArgsAreXYValues = 1 << 1,

    /// <summary>If set, there is a simple scale for the component; otherwise, scale = 1.</summary>
    WeHaveAScale = 1 << 3,

    /// <summary>If set, indicates at least one more glyph after this one.</summary>
    MoreComponents = 1 << 5,

    /// <summary>If set, indicates the X direction will use a different scale from the Y direction.</summary>
    WeHaveAnXAndYScale = 1 << 6,

    /// <summary>If set, there is a 2x2 transformation that will be used to scale the component.</summary>
    WeHaveATwoByTwo = 1 << 7,

    /// <summary>If set, following the last component are instructions for the composite character.</summary>
    WeHaveInstructions = 1 << 8,
}

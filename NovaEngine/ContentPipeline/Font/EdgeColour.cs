namespace NovaEngine.ContentPipeline.Font;

/// <summary>The colours an edge segment can be.</summary>
[Flags]
internal enum EdgeColour : byte
{
    /// <summary>Black (invalid).</summary>
    Black = 0,

    /// <summary>Red channel only.</summary>
    Red = 1 << 1,

    /// <summary>Green channel only.</summary>
    Green = 1 << 2,

    /// <summary>Blue channel only.</summary>
    Blue = 1 << 3,

    /// <summary>Yellow (red and green channels only).</summary>
    Yellow = Red | Green,

    /// <summary>Magenta (red and blue channels only).</summary>
    Magenta = Red | Blue,

    /// <summary>Cyan (green and blue channels only).</summary>
    Cyan = Green | Blue,

    /// <summary>White (all channels).</summary>
    White = Red | Green | Blue
}

namespace NovaEngine.Content.Models.Font;

/// <summary>The flags used when reading a simple glyph.</summary>
[Flags]
internal enum SimpleGlyphFlags : byte
{
    /// <summary>If set, the point is on the curve.</summary>
    OnCurve = 1 << 0,

    /// <summary>If set, the corresponding x-coordinate is 1 byte long.</summary>
    XIsByte = 1 << 1,

    /// <summary>If set, the corresponding y-coordinate is 1 byte long.</summary>
    YIsByte = 1 << 2,

    /// <summary>If set, the next byte specifies the number of additional times this flag byte is to be repeated in the logical flags array - that is, the number of additional logical flag entries inserted after this entry.</summary>
    Repeat = 1 << 3,

    /// <summary>If set, the current x-coordinate is a signed 16-bit delta vector.</summary>
    XDelta = 1 << 4,

    /// <summary>If set, the current y-coordiante is a signed 16-bit delta vector.</summary>
    YDelta = 1 << 5
}

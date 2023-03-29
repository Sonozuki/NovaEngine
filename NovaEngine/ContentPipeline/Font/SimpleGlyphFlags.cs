namespace NovaEngine.ContentPipeline.Font;

/// <summary>The flags used when reading a simple glyph.</summary>
[Flags]
internal enum SimpleGlyphFlags : byte
{
    /// <summary>If set, the point is on the curve.</summary>
    OnCurve = 1 << 0,

    /// <summary>
    /// If set, the corresponding x-coordinate is 1 byte long, and the sign is determined by <see cref="XIsSameOrPositiveXShortVector"/>.<br/>
    /// If not set, its interpretation depends on <see cref="XIsSameOrPositiveXShortVector"/>: If that other flag is set, the x-coordinate is the same as the previous x-coordinate, and no element is added to the xCoordinates array.<br/>
    /// If both flags are not set, the corresponding element in the xCoordinates array is two bytes and interpreted as a signed integer.
    /// </summary>
    XShortVector = 1 << 1,

    /// <summary>
    /// If set, the corresponding y-coordinate is 1 byte long, and the sign is determined by <see cref="YIsSameOrPositiveYShortVector"/>.<br/>
    /// If not set, its interpretation depends on <see cref="YIsSameOrPositiveYShortVector"/>: If that other flag is set, the y-coordinate is the same as the previous y-coordinate, and no element is added to the yCoordinates array.<br/>
    /// If both flags are not set, the corresponding element in the yCoordinates array is two bytes and interpreted as a signed integer.
    /// </summary>
    YShortVector = 1 << 2,

    /// <summary>If set, the next byte (read as unsigned) specifies the number of additional times this flag byte is to be repeated in the logical flags array — that is, the number of additional logical flag entries inserted after this entry.</summary>
    Repeat = 1 << 3,

    /// <summary>
    /// This has two meanings, depending on how the <see cref="XShortVector"/> flag is set.<br/>
    /// If <see cref="XShortVector"/> is set, this bit describes the sign of the value, with 1 equalling positive and 0 negative.<br/>
    /// If <see cref="XShortVector"/> is not set and this bit is set, then the current x-coordinate is the same as the previous x-coordinate.<br/>
    /// If <see cref="XShortVector"/> is not set and this bit is also not set, the current x-coordinate is a signed 16-bit delta vector.<br/>
    /// </summary>
    XIsSameOrPositiveXShortVector = 1 << 4,

    /// <summary>
    /// This has two meanings, depending on how the <see cref="YShortVector"/> flag is set.<br/>
    /// If <see cref="YShortVector"/> is set, this bit describes the sign of the value, with 1 equalling positive and 0 negative.<br/>
    /// If <see cref="YShortVector"/> is not set and this bit is set, then the current y-coordinate is the same as the previous y-coordinate.<br/>
    /// If <see cref="YShortVector"/> is not set and this bit is also not set, the current y-coordinate is a signed 16-bit delta vector.
    /// </summary>
    YIsSameOrPositiveYShortVector = 1 << 5,

    /// <summary>If set, contours in the glyph description may overlap. Use of this flag is not required in OpenType — that is, it is valid to have contours overlap without having this flag set.</summary>
    OverlapSimple = 1 << 6
}

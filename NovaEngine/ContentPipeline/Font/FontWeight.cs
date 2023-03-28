namespace NovaEngine.ContentPipeline.Font;

/// <summary>The visual weight (degree of blackness or thickness of strokes) of the characters in the font.</summary>
/// <remarks>Named values are defined for increments of 100 but any value between 1 to 1000 is valid.</remarks>
internal enum FontWeight : ushort
{
    /// <summary>The glyphs have a thin weight.</summary>
    Thin = 100,

    /// <summary>The glyphs have an extra-light weight.</summary>
    ExtraLight = 200,

    /// <summary>The glyphs have a light weight.</summary>
    Light = 300,

    /// <summary>The glyphs have a normal weight.</summary>
    Normal = 400,

    /// <summary>The glyphs have a medium weight.</summary>
    Medium = 500,

    /// <summary>The glyphs have a semi-bold weight.</summary>
    SemiBold = 600,

    /// <summary>The glyphs have a bold weight.</summary>
    Bold = 700,

    /// <summary>The glyphs have an extra-bold weight.</summary>
    ExtraBold = 800,

    /// <summary>The glyphs have a heavy weight.</summary>
    Heavy = 900
}

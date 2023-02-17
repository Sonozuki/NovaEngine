namespace NovaEngine.Components;

/// <summary>How the border of text glyphs should be rendered.</summary>
public enum TextBorderType
{
    /// <summary>The text has no border.</summary>
    None,

    /// <summary>The text has a solid colour border.</summary>
    Colour,

    /// <summary>The text has a textured border.</summary>
    Texture,

    /// <summary>The text has a bloom border.</summary>
    Bloom
}
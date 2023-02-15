namespace NovaEngine.Content.Models.Font;

/// <summary>Metadata of a glyph.</summary>
public class GlyphData
{
    /*********
    ** Properties
    *********/
    /// <summary>The character the glyph represents.</summary>
    public char Character { get; }

    /// <summary>The size of the glyph, in pixels.</summary>
    public Vector2I Size { get; }

    /// <summary>The position of the glyph on the atlas.</summary>
    public Rectangle AtlasPosition { get; }

    /// <summary>The horizontal metrics of the glyph.</summary>
    public HorizontalMetrics HorizontalMetrics { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="character">The character the glyph represents.</param>
    /// <param name="size">The size of the glyph, in pixels.</param>
    /// <param name="atlasPosition">The position of the glyph on the atlas.</param>
    /// <param name="horizontalMetrics">The horizontal metrics of the glyph.</param>
    public GlyphData(char character, Vector2I size, Rectangle atlasPosition, HorizontalMetrics horizontalMetrics)
    {
        Character = character;
        Size = size;
        AtlasPosition = atlasPosition;
        HorizontalMetrics = horizontalMetrics;
    }
}

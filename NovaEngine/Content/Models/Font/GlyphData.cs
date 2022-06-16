namespace NovaEngine.Content.Models.Font;

/// <summary>Metadata of a glyph.</summary>
public class GlyphData
{
    /*********
    ** Accessors
    *********/
    /// <summary>The character the glyph represents.</summary>
    public char Character { get; }

    /// <summary>The position of the glyph on the atlas.</summary>
    public Rectangle AtlasPosition { get; }

    /// <summary>The horizontal metrics of the glyph.</summary>
    public HorizontalMetrics HorizontalMetrics { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="character">The character the glyph represents.</param>
    /// <param name="atlasPosition">The position of the glyph on the atlas.</param>
    /// <param name="horizontalMetrics">The horizontal metrics of the glyph.</param>
    public GlyphData(char character, Rectangle atlasPosition, HorizontalMetrics horizontalMetrics)
    {
        Character = character;
        AtlasPosition = atlasPosition;
        HorizontalMetrics = horizontalMetrics;
    }


    /*********
    ** Protected Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    protected GlyphData() { } // required for serialiser

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

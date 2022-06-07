namespace NovaEngine.Content.Models.Font;

/// <summary>Metadata of a glyph.</summary>
public class GlyphData
{
    /*********
    ** Accessors
    *********/
    /// <summary>The position of the glyph on the font atlas.</summary>
    public GlyphPosition AtlasPosition { get; set; }

    /// <summary>The horizontal metrics of the glyph.</summary>
    public HorizontalMetrics HorizontalMetrics { get; set; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="atlasPosition">The position of the glyph on the font atlas.</param>
    /// <param name="horizontalMetrics">The horizontal metrics of the glyph.</param>
    public GlyphData(GlyphPosition atlasPosition, HorizontalMetrics horizontalMetrics)
    {
        AtlasPosition = atlasPosition;
        HorizontalMetrics = horizontalMetrics;
    }
}

using NovaEngine.Content.Models.Font;

namespace NovaEngine.Core;

/// <summary>Represents a font.</summary>
public class Font : IDisposable
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the font.</summary>
    public string Name { get; }

    /// <summary>The max height of the glyphs after being scaled, in pixels.</summary>
    public float MaxGlyphHeight { get; }

    /// <summary>The pixel range that was used when generating the atlas.</summary>
    public float PixelRange { get; }

    // TODO: support multiple atlases
    /// <summary>The atlas of the font.</summary>
    public Texture2D Atlas { get; }

    /// <summary>The glyphs in the font.</summary>
    public List<GlyphData> Glyphs { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the font.</param>
    /// <param name="maxGlyphHeight">The max height of the glyphs after being scaled, in pixels.</param>
    /// <param name="pixelRange">The pixel range that was used when generating the atlas.</param>
    /// <param name="atlas">The atlas of the font.</param>
    /// <param name="glyphs">The positions of each glyph on the atlas.</param>
    public Font(string name, float maxGlyphHeight, float pixelRange, Texture2D atlas, IEnumerable<GlyphData> glyphs)
    {
        Name = name;
        MaxGlyphHeight = maxGlyphHeight;
        PixelRange = pixelRange;
        Atlas = atlas;
        Glyphs = glyphs.ToList();
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public void Dispose() => Atlas.Dispose();
}

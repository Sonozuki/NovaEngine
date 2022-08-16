using NovaEngine.Content.Models.Font;

namespace NovaEngine.Core;

/// <summary>Represents a font.</summary>
public class Font : IDisposable
{
    /*********
    ** Accessors
    *********/
    /// <summary>The name of the font.</summary>
    public string Name { get; }

    // TODO: support multiple atlases
    /// <summary>The atlas of the font.</summary>
    public Texture2D Atlas { get; }

    /// <summary>The glyphs in the font.</summary>
    public List<GlyphData> Glyphs{ get; } 


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the font.</param>
    /// <param name="atlas">The atlas of the font.</param>
    /// <param name="glyphs">The positions of each glyph on the atlas.</param>
    public Font(string name, Texture2D atlas, IEnumerable<GlyphData> glyphs)
    {
        Name = name;
        Atlas = atlas;
        Glyphs = glyphs.ToList();
    }

    /// <inheritdoc/>
    public void Dispose() => Atlas.Dispose();
}

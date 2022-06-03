namespace NovaEngine.Core;

/// <summary>Represents a font.</summary>
public class Font
{
    /*********
    ** Accessors
    *********/
    /// <summary>The name of the font.</summary>
    public string Name { get; }

    // TODO: support multiple atlases
    /// <summary>The atlas of the font.</summary>
    public Texture2D Atlas { get; }

    /// <summary>The positions of each glyph on the atlas.</summary>
    public List<GlyphPosition> GlyphPositions { get; } 


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the font.</param>
    /// <param name="atlas">The atlas of the font.</param>
    /// <param name="glyphPositions">The positions of each glyph on the atlas.</param>
    public Font(string name, Texture2D atlas, IEnumerable<GlyphPosition> glyphPositions)
    {
        Name = name;
        Atlas = atlas;
        GlyphPositions = glyphPositions.ToList();
    }
}

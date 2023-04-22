using NovaEngine.ContentPipeline.Font;

namespace NovaEngine.Core;

/// <summary>Represents a font.</summary>
public class Font : IDisposable
{
    /*********
    ** Fields
    *********/
    /// <summary>Whether the font has been disposed.</summary>
    private bool IsDisposed;


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
    public IReadOnlyList<GlyphData> Glyphs { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Destructs the instance.</summary>
    ~Font() => Dispose(false);

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
        Glyphs = glyphs.ToList().AsReadOnly();
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the font.</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the font.</summary>
    /// <param name="disposing">Whether the font is being disposed deterministically.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        if (disposing)
            Atlas?.Dispose();

        IsDisposed = true;
    }
}

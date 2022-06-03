namespace NovaEngine.Core;

/// <summary>Represents a glyph position on a font atlas.</summary>
public class GlyphPosition
{
    /*********
    ** Accessors
    *********/
    /// <summary>The character the glyph represents.</summary>
    public char Character { get; }

    /// <summary>The position of the glyph on the atlas.</summary>
    public Rectangle Position { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="character">The character the glyph represents.</param>
    /// <param name="position">The position of the glyph on the atlas.</param>
    public GlyphPosition(char character, Rectangle position)
    {
        Character = character;
        Position = position;
    }
}

namespace NovaEngine.Content.Models.Font;

/// <summary>Represents a font glyph.</summary>
internal class Glyph
{
    /*********
    ** Accessors
    *********/
    /// <summary>The indices in <see cref="Points"/> the represent the end of each contour.</summary>
    public List<ushort> ContourEnds { get; private set; } = new();

    /// <summary>The number of contours in the glyph.</summary>
    public short NumberOfContours { get; set; }

    /// <summary>The points in the glyph.</summary>
    public List<Point> Points { get; private set; } = new();

    /// <summary>The contours in the glyph.</summary>
    public List<Contour> Contours { get; private set; } = new();


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="numberOfContours">The number of contours in the glyph.</param>
    public Glyph(short numberOfContours)
    {
        NumberOfContours = numberOfContours;
    }

    /// <summary>Flushes the contour ends, points, and contours of the glyph.</summary>
    public void Flush()
    {
        ContourEnds = new();
        Points = new();
        Contours = new();
    }
}

namespace NovaEngine.Content.Models.Font;

/// <summary>Represents a font glyph.</summary>
/// <remarks>This if used internally while parsing the font file for packing.</remarks>
internal class Glyph
{
    /*********
    ** Fields
    *********/
    /// <summary>The bounds of the glyph in the font file.</summary>
    public Rectangle UnscaledBounds = new();

    /// <summary>The bounds of the glyph in the texture atlas, in pixels.</summary>
    public Rectangle ScaledBounds = new();


    /*********
    ** Accessors
    *********/
    /// <summary>The character the glyph represents.</summary>
    public char Character { get; set; }

    /// <summary>The indices in <see cref="Points"/> the represent the end of each contour.</summary>
    public List<ushort> ContourEnds { get; private set; } = new();

    /// <summary>The number of contours in the glyph.</summary>
    public short NumberOfContours { get; set; }

    /// <summary>The points in the glyph.</summary>
    public List<Point> Points { get; private set; } = new();

    /// <summary>The contours in the glyph.</summary>
    public List<Contour> Contours { get; private set; } = new();

    /// <summary>The horizontal metrics of the glyph.</summary>
    public HorizontalMetrics HorizontalMetrics { get; set; }


    /*********
    ** Public Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="numberOfContours">The number of contours in the glyph.</param>
    public Glyph(short numberOfContours)
    {
        NumberOfContours = numberOfContours;
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Flushes the contour ends, points, and contours of the glyph.</summary>
    public void Flush()
    {
        ContourEnds = new();
        Points = new();
        Contours = new();
    }
}

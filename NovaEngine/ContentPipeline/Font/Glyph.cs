namespace NovaEngine.ContentPipeline.Font;

/// <summary>Represents a font glyph.</summary>
/// <remarks>The bounding rectangle for a glyph is defined as the rectangle with a lower left corner of (<see cref="XMin"/>, <see cref="YMin"/>) and an upper right corner of (<see cref="XMax"/>, <see cref="YMax"/>).</remarks>
internal sealed class Glyph
{
    /*********
    ** Properties
    *********/
    /// <summary>The character the glyph represents.</summary>
    public char Character { get; internal set; }

    /// <summary>The number of contours the glyph has.</summary>
    public short NumberOfContours { get; }

    /// <summary>The minimum x for coordinate data.</summary>
    public short XMin { get; }

    /// <summary>The minimum y for coordinate data.</summary>
    public short YMin { get; }

    /// <summary>The maximum x for coordinate data.</summary>
    public short XMax { get; }

    /// <summary>The maximum y for coordinate data.</summary>
    public short YMax { get; }

    /// <summary>Whether the glyph is a composite glyph.</summary>
    public bool IsComposite { get; }

    /// <summary>The indices into <see cref="Points"/> of the contour end points.</summary>
    public ImmutableArray<ushort> ContourEnds { get; internal set; }

    /// <summary>The points that comprise the glyph.</summary>
    public ImmutableArray<Point> Points { get; internal set; }

    /// <summary>The contours in the glyph.</summary>
    public ImmutableArray<Contour> Contours { get; internal set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the glyph.<br/>
    /// This assumes the reader has been positioned for the glyph to be read.
    /// </param>
    public Glyph(BinaryReader binaryReader)
    {
        NumberOfContours = binaryReader.ReadInt16BigEndian();
        XMin = binaryReader.ReadInt16BigEndian();
        YMin = binaryReader.ReadInt16BigEndian();
        XMax = binaryReader.ReadInt16BigEndian();
        YMax = binaryReader.ReadInt16BigEndian();

        IsComposite = NumberOfContours < 0;
    }
}

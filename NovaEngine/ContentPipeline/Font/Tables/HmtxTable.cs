namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the horizontal metrics table.</summary>
internal sealed class HmtxTable
{
    /*********
    ** Properties
    *********/
    /// <summary>The advance width and left side bearing values for each glyph, indexed by glyph id.</summary>
    public ImmutableArray<HorizontalMetrics> HorizontalMetrics { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the table.<br/>
    /// This assumes the reader has been positioned for the table to be read.
    /// </param>
    /// <param name="numberOfGlyphs">The number of glyphs as specified in the 'maxp' table.</param>
    /// <param name="numberOfHorizontalMetrics">The number of horizontal metrics as specified in the 'hhea' table.</param>
    public HmtxTable(BinaryReader binaryReader, ushort numberOfGlyphs, ushort numberOfHorizontalMetrics)
    {
        var horizontalMetrics = new List<HorizontalMetrics>();

        for (var i = 0; i < numberOfHorizontalMetrics; i++)
            horizontalMetrics.Add(new(binaryReader.ReadUInt16BigEndian(), binaryReader.ReadInt16BigEndian()));

        var leftSideBearings = new List<short>();
        for (var i = 0; i < numberOfHorizontalMetrics - numberOfGlyphs; i++)
            leftSideBearings.Add(binaryReader.ReadInt16BigEndian());

        var lastAdvanceWidth = horizontalMetrics.Last().AdvanceWidth;
        foreach (var leftSideBearing in leftSideBearings)
            horizontalMetrics.Add(new(lastAdvanceWidth, leftSideBearing));

        HorizontalMetrics = horizontalMetrics.ToImmutableArray();
    }
}

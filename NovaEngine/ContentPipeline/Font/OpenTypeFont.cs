using NovaEngine.ContentPipeline.Font.GlyphParsers;
using NovaEngine.ContentPipeline.Font.Tables;

namespace NovaEngine.ContentPipeline.Font;

/// <summary>Represents an open type font.</summary>
internal sealed class OpenTypeFont : IDisposable
{
    /*********
    ** Constants
    *********/
    /// <summary>The max height of the glyphs after being scaled, in pixels.</summary>
    public const float MaxGlyphHeight = 64;


    /*********
    ** Fields
    *********/
    /// <summary>Whether the font has been disposed.</summary>
    private bool IsDisposed;

    /// <summary>The binary reader to use to parse the font.</summary>
    private readonly BinaryReader BinaryReader;


    /*********
    ** Properties
    *********/
    /// <summary>The type of glyph outlines the font uses.</summary>
    public OutlineType OutlineType { get; private set; }

    /// <summary>The table directory of the font.</summary>
    public TableDirectory TableDirectory { get; private set; }

    /// <summary>The 'cmap' table of the font.</summary>
    public CmapTable CmapTable { get; private set; }

    /// <summary>The 'head' table of the font.</summary>
    public HeadTable HeadTable { get; private set; }

    /// <summary>The 'hhea' table of the font.</summary>
    public HheaTable HheaTable { get; private set; }

    /// <summary>The 'maxp' table of the font.</summary>
    public MaxpTable MaxpTable { get; private set; }

    /// <summary>The 'hmtx' table of the font.</summary>
    public HmtxTable HmtxTable { get; private set; }

    /// <summary>The 'name' table of the font.</summary>
    public NameTable NameTable { get; private set; }

    /// <summary>The 'OS/2' table of the font.</summary>
    public OS2Table OS2Table { get; private set; }

    /// <summary>The 'post' table of the font.</summary>
    public PostTable PostTable { get; private set; }

    /// <summary>The 'loca' table of the font.</summary>
    /// <remarks>This is only used if <see cref="OutlineType"/> is <see cref="OutlineType.TrueType"/>.</remarks>
    public LocaTable? LocaTable { get; private set; }

    /// <summary>The glyphs in the font.</summary>
    public ImmutableArray<Glyph> Glyphs { get; }

    /// <summary>The name of the font.</summary>
    public string Name => NameTable.NameRecords.First(nameRecord => nameRecord.NameId == NameId.FullName).Value;


    /*********
    ** Constructors
    *********/
    /// <summary>Destructs the instance.</summary>
    ~OpenTypeFont() => Dispose(false);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="fontFile">The file stream to read the font from.</param>
    public OpenTypeFont(FileStream fontFile)
    {
        BinaryReader = new BinaryReader(fontFile, Encoding.UTF8, leaveOpen: true);

        ReadTableDirectory();

        EnsureRequiredTablesExist();

        ReadCmapTable();
        ReadHeadTable();
        ReadHheaTable();
        ReadMaxpTable();
        ReadHmtxTable();
        ReadNameTable();
        ReadOS2Table();
        ReadPostTable();

        DetermineOutlineType();
        var glyphParser = CreateGlyphParser();
        
        // TODO: currently only reads ASCII glyphs
        {
            var glyphs = new List<Glyph>();
            var characterMap = CmapTable!.Subtables.First();

            var glyphCharacterCodes = new List<char> { '\0' };
            for (var i = 0x21; i < 0x7F; i++)
                glyphCharacterCodes.Add((char)i);

            foreach (var glyphCharacterCode in glyphCharacterCodes)
            {
                var glyphIndex = characterMap.Map(BinaryReader, glyphCharacterCode);

                var glyph = glyphParser.Parse(glyphIndex);
                glyph.Character = glyphCharacterCode;
                glyph.UnscaledHorizontalMetrics = HmtxTable!.HorizontalMetrics[glyphIndex];
                glyphs.Add(glyph);
            }

            Glyphs = glyphs.ToImmutableArray();
        }

        CalculateGlyphEdgeColours();
        CalculateGlyphScales();
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


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
    ** Private Methods
    *********/
    /// <summary>Reads the table directory.</summary>
    private void ReadTableDirectory()
    {
        BinaryReader.BaseStream.Position = 0;
        TableDirectory = new(BinaryReader);

        foreach (var tableRecord in TableDirectory.TableRecords)
            if (tableRecord.Tag != "head") // the head table has special validation rules because of the checksumAdjustment field, validation for the head table will be done after the it's parsed
                tableRecord.Validate(BinaryReader);
    }

    /// <summary>Ensures the tables that are required, as per the spec, are present in the table directory.</summary>
    /// <exception cref="FontException">Thrown if any of the required tables are missing.</exception>
    private void EnsureRequiredTablesExist()
    {
        foreach (var requiredTableTag in new[] { "cmap", "head", "hhea", "hmtx", "maxp", "name", "OS/2", "post" })
            if (!TableDirectory.TableRecords.Any(tableRecord => tableRecord.Tag == requiredTableTag))
                throw new FontException($"Font doesn't contain required table '{requiredTableTag}'.");
    }

    /// <summary>Reads the 'cmap' table from the font.</summary>
    private void ReadCmapTable()
    {
        var cmapOffset = GetTableOffset("cmap");
        BinaryReader.BaseStream.Position = cmapOffset;
        CmapTable = new(BinaryReader, cmapOffset);
    }

    /// <summary>Reads the 'head' table from the font.</summary>
    private void ReadHeadTable()
    {
        BinaryReader.BaseStream.Position = GetTableOffset("head");
        HeadTable = new(BinaryReader);

        // TODO: validate
    }

    /// <summary>Reads the 'hhea' table from the font.</summary>
    private void ReadHheaTable()
    {
        BinaryReader.BaseStream.Position = GetTableOffset("hhea");
        HheaTable = new(BinaryReader);
    }

    /// <summary>Reads the 'maxp' table from the font.</summary>
    private void ReadMaxpTable()
    {
        BinaryReader.BaseStream.Position = GetTableOffset("maxp");
        MaxpTable = new(BinaryReader);
    }

    /// <summary>Reads the 'hmtx' table from the font.</summary>
    private void ReadHmtxTable()
    {
        BinaryReader.BaseStream.Position = GetTableOffset("hmtx");
        HmtxTable = new(BinaryReader, MaxpTable.NumberOfGlyphs, HheaTable.NumberOfHorizontalMetrics);
    }

    /// <summary>Reads the 'name' table from the font.</summary>
    private void ReadNameTable()
    {
        BinaryReader.BaseStream.Position = GetTableOffset("name");
        NameTable = new(BinaryReader);
    }

    /// <summary>Reads the 'OS/2' table from the font.</summary>
    private void ReadOS2Table()
    {
        BinaryReader.BaseStream.Position = GetTableOffset("OS/2");
        OS2Table = new(BinaryReader);
    }

    /// <summary>Reads the 'post' table from the font.</summary>
    private void ReadPostTable()
    {
        BinaryReader.BaseStream.Position = GetTableOffset("post");
        PostTable = new(BinaryReader);
    }

    /// <summary>Reads the 'loca' table from the font.</summary>
    private void ReadLocaTable()
    {
        BinaryReader.BaseStream.Position = GetTableOffset("loca");
        LocaTable = new(BinaryReader, HeadTable.IndexToLocFormat, MaxpTable.NumberOfGlyphs);
    }

    /// <summary>Determines the type of glyph outlines used in the font.</summary>
    /// <exception cref="FontException">Thrown if the type of outline couldn't be determined.</exception>
    private void DetermineOutlineType()
    {
        if (DoesTableExist("glyf"))
            OutlineType = OutlineType.TrueType;
        else if (DoesTableExist("CFF "))
            OutlineType = OutlineType.CFF;
        else if (DoesTableExist("CFF2"))
            OutlineType = OutlineType.CFF2;
        else
            throw new FontException("Failed to determine type of outline used in the font.");
    }

    /// <summary>Creates a glyph parser for the font.</summary>
    /// <returns>The created glyph parser.</returns>
    /// <exception cref="FontException">Thrown if a glyph parser couldn't be created.</exception>
    private GlyphParserBase CreateGlyphParser()
    {
        var glyphParser = (GlyphParserBase?)null;

        switch (OutlineType)
        {
            case OutlineType.TrueType:
                ReadLocaTable();
                glyphParser = new TTFGlyphParser(BinaryReader, LocaTable!, GetTableOffset("glyf"));
                break;

            case OutlineType.CFF:
                throw new NotImplementedException();

            case OutlineType.CFF2:
                throw new NotImplementedException();
        }

        return glyphParser!;
    }

    /// <summary>Calculates the colours of each edge segment in each glyph.</summary>
    private void CalculateGlyphEdgeColours()
    {
        foreach (var glyph in Glyphs)
            MTSDF.ColourEdges(glyph);
    }

    /// <summary>Calculates the <see cref="Glyph.UnscaledBounds"/>, <see cref="Glyph.ScaledBounds"/>, and <see cref="Glyph.ScaledHorizontalMetrics"/> or each glyph.</summary>
    private void CalculateGlyphScales()
    {
        var maxGlyphHeight = -1;
        foreach (var glyph in Glyphs)
        {
            var points = glyph.Contours
                .SelectMany(contour => contour.Edges)
                .SelectMany(edge => edge.Points)
                .ToList();

            var xMin = points.Min(point => point.X);
            var yMin = points.Min(point => point.Y);
            var xMax = points.Max(point => point.X);
            var yMax = points.Max(point => point.Y);
            glyph.UnscaledBounds = new(xMin, yMin, xMax - xMin, yMax - yMin);

            // record height so glyphs can be correctly scaled in the atlas
            if (glyph.UnscaledBounds.Height > maxGlyphHeight)
                maxGlyphHeight = (int)glyph.UnscaledBounds.Height;
        }

        var scale = maxGlyphHeight / MaxGlyphHeight;
        foreach (var glyph in Glyphs)
        {
            glyph.ScaledBounds = new(0, 0,
                (uint)MathF.Ceiling(glyph.UnscaledBounds.Width / scale),
                (uint)MathF.Ceiling(glyph.UnscaledBounds.Height / scale)
            );

            glyph.ScaledHorizontalMetrics = new(
                (ushort)MathF.Round(glyph.UnscaledHorizontalMetrics.AdvanceWidth / scale),
                (short)MathF.Round(glyph.UnscaledHorizontalMetrics.LeftSideBearing / scale)
            );
        }
    }

    /// <summary>Determines whether a table with a specified tag exists.</summary>
    /// <param name="tableTag">The tag of the table to check exists.</param>
    /// <returns><see langword="true"/>, if a table with the tag of <paramref name="tableTag"/> exists; otherwise, <see langword="false"/>.</returns>
    private bool DoesTableExist(string tableTag) => TableDirectory.TableRecords.Any(tableRecord => tableRecord.Tag == tableTag);

    /// <summary>Retrieves the offset of the table with a specified tag.</summary>
    /// <param name="tableTag">The tag of the table to retrieve the offset of.</param>
    /// <returns>The offset of the table with the tag of <paramref name="tableTag"/>.</returns>
    private uint GetTableOffset(string tableTag)
    {
        var tableRecord = TableDirectory.TableRecords.First(tableRecord => tableRecord.Tag == tableTag);
        return tableRecord.Offset;
    }

    /// <summary>Cleans up unmanaged resources in the font.</summary>
    /// <param name="disposing">Whether the font is being disposed deterministically.</param>
    private void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        if (disposing)
            BinaryReader.Dispose();

        IsDisposed = true;
    }
}

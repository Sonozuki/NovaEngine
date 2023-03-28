using NovaEngine.ContentPipeline.Font.Tables;

namespace NovaEngine.ContentPipeline.Font;

/// <summary>Represents an open type font.</summary>
internal sealed class OpenTypeFont : IDisposable
{
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

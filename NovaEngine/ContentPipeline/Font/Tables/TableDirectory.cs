using NovaEngine.ContentPipeline.Font.Models;

namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the directory of the top-level tables in the font.</summary>
internal sealed class TableDirectory
{
    /*********
    ** Properties
    *********/
    /// <summary>The version of the font, either 0x00010000 or 0x4F54544F.</summary>
    public uint Version { get; }

    /// <summary>The number of tables.</summary>
    public ushort NumberOfTables { get; }

    /// <summary>The table records array, one for each top-level table in the font.</summary>
    public ImmutableArray<TableRecord> TableRecords { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the directory.<br/>
    /// This assumes the reader has been positioned for the directory to be read.
    /// </param>
    /// <exception cref="FontException"></exception>
    public TableDirectory(BinaryReader binaryReader)
    {
        Version = binaryReader.ReadUInt32BigEndian();
        NumberOfTables = binaryReader.ReadUInt16BigEndian();

        binaryReader.BaseStream.Position += 6; // searchRange, entrySelector, rangeShift

        var tableRecords = new List<TableRecord>();
        for (var i = 0; i < NumberOfTables; i++)
            tableRecords.Add(new(binaryReader.ReadOTFTag(), binaryReader.ReadUInt32BigEndian(), binaryReader.ReadUInt32BigEndian(), binaryReader.ReadUInt32BigEndian()));

        TableRecords = tableRecords.ToImmutableArray();

        if (Version != 0x00010000 && Version != 0x4F54544F)
            throw new FontException("Font version is invalid");
    }
}

using NovaEngine.ContentPipeline.Font.Cmap;
using NovaEngine.ContentPipeline.Font.Records;

namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the character to glyph index mapping table.</summary>
internal sealed class CmapTable
{
    /*********
    ** Properties
    *********/
    /// <summary>The version of the table.</summary>
    public ushort Version { get; }

    /// <summary>The number of encoding tables.</summary>
    public ushort NumberOfTables { get; }

    /// <summary>The encoding tables.</summary>
    public ImmutableArray<EncodingRecord> EncodingRecords { get; }

    /// <summary>The cmap subtables.</summary>
    public ImmutableArray<CmapFormatBase> Subtables { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the table.<br/>
    /// This assumes the reader has been positioned for the table to be read.
    /// </param>
    /// <param name="cmapOffset">The offset into the binary reader of the 'cmap' table.</param>
    public CmapTable(BinaryReader binaryReader, uint cmapOffset)
    {
        var version = binaryReader.ReadUInt16BigEndian();
        var numberOfTables = binaryReader.ReadUInt16BigEndian();

        var encodingRecords = new List<EncodingRecord>();
        for (var i = 0; i < numberOfTables; i++)
            encodingRecords.Add(new((PlatformId)binaryReader.ReadUInt16BigEndian(), binaryReader.ReadUInt16BigEndian(), binaryReader.ReadUInt32BigEndian()));
        EncodingRecords = encodingRecords.ToImmutableArray();
        Subtables = encodingRecords.Select(encodingRecord => encodingRecord.ReadSubtable(binaryReader, cmapOffset)).ToImmutableArray();

        Version = version;
        NumberOfTables = (ushort)EncodingRecords.Length;
    }
}

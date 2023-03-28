using NovaEngine.ContentPipeline.Font.Cmap;

namespace NovaEngine.ContentPipeline.Font.Models;

/// <summary>Represents a character map subtable record.</summary>
internal sealed class EncodingRecord
{
    /*********
    ** Properties
    *********/
    /// <summary>The id of the platform of the encoding record.</summary>
    public PlatformId PlatformId { get; }

    /// <summary>The id of the encoding of the encoding record.</summary>
    public ushort EncodingId { get; }

    /// <summary>The offset from the beginning of the 'cmap' table of the subtable.</summary>
    public uint Offset { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="platformId">The id of the platform of the encoding record.</param>
    /// <param name="encodingId">The id of the encoding of the encoding record.</param>
    /// <param name="offset">The offset from the beginning of the cmap table of the subtable.</param>
    public EncodingRecord(PlatformId platformId, ushort encodingId, uint offset)
    {
        PlatformId = platformId;
        EncodingId = encodingId;
        Offset = offset;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Reads the subtable the record represents.</summary>
    /// <param name="binaryReader">The binary reader to use to read the subtable from.</param>
    /// <param name="cmapTableOffset">The offset of the 'cmap' table.</param>
    /// <returns>The subtable the record represents.</returns>
    /// <exception cref="FontException">Thrown if the subtable format isn't recognised (hasn't been implemented or is invalid).</exception>
    public CmapFormatBase ReadSubtable(BinaryReader binaryReader, uint cmapTableOffset)
    {
        binaryReader.BaseStream.Position = cmapTableOffset + Offset;

        var format = binaryReader.ReadUInt16BigEndian();
        return format switch
        {
            0 => new CmapFormat0(binaryReader),
            4 => new CmapFormat4(binaryReader),
            _ => throw new FontException($"Cmap format '{format}' is unknown.")
        };
    }
}

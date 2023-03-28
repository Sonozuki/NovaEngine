namespace NovaEngine.ContentPipeline.Font.Cmap;

/// <summary>Represents a format 4 character map subtable.</summary>
internal sealed class CmapFormat4 : CmapFormatBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The segments in the cmap.</summary>
    private readonly List<CmapFormat4Segment> Segments = new();


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public override ushort Format => 4;


    /*********
    ** Constructors
    *********/
    /// <inheritdoc/>
    public CmapFormat4(BinaryReader binaryReader)
        : base(binaryReader)
    {
        var segmentCount = (ushort)(binaryReader.ReadUInt16BigEndian() / 2);

        binaryReader.BaseStream.Position += 6; // searchRange, entrySelector, rangeShift

        for (var i = 0; i < segmentCount; i++)
            Segments.Add(new() { EndCode = binaryReader.ReadUInt16BigEndian() });

        binaryReader.ReadUInt16(); // reservedPad

        for (var i = 0; i < segmentCount; i++)
            Segments[i].StartCode = binaryReader.ReadUInt16BigEndian();
        for (var i = 0; i < segmentCount; i++)
            Segments[i].IdDelta = binaryReader.ReadInt16BigEndian();
        for (var i = 0; i < segmentCount; i++)
        {
            var idRangeOffset = binaryReader.ReadUInt16BigEndian();
            if (idRangeOffset != 0)
                Segments[i].IdRangeOffset = (ushort)(binaryReader.BaseStream.Position - 2 + idRangeOffset);
            else
                Segments[i].IdRangeOffset = 0;
        }
    }
}

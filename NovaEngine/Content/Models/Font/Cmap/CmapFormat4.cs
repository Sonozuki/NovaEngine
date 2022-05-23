﻿namespace NovaEngine.Content.Models.Font.Cmap;

/// <summary>Represents a format 4 cmap subtable.</summary>
internal class CmapFormat4 : ICmapFormat
{
    /*********
    ** Fields
    *********/
    /// <summary>The map cache.</summary>
    private readonly Dictionary<int, ushort> Cache = new();

    /// <summary>The segments in the cmap.</summary>
    private readonly List<Segment> Segments = new();


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">The binary reader whose current position is at the start of the format 4 cmap subtable content.</param>
    public CmapFormat4(BinaryReader binaryReader)
    {
        var segmentCount = binaryReader.ReadUInt16BigEndian() / 2;
        binaryReader.BaseStream.Position += 6;

        for (int i = 0; i < segmentCount; i++)
            Segments.Add(new() { EndCode = binaryReader.ReadUInt16BigEndian() });
        binaryReader.BaseStream.Position += 2;
        for (int i = 0; i < segmentCount; i++)
            Segments[i].StartCode = binaryReader.ReadUInt16BigEndian();
        for (int i = 0; i < segmentCount; i++)
            Segments[i].IdDelta = binaryReader.ReadUInt16BigEndian();
        for (int i = 0; i < segmentCount; i++)
        {
            var ro = binaryReader.ReadUInt16BigEndian();
            if (ro != 0)
                Segments[i].IdRangeOffset = (ushort)(binaryReader.BaseStream.Position - 2 + ro);
            else
                Segments[i].IdRangeOffset = 0;
        }
    }

    /// <inheritdoc/>
    public uint Map(BinaryReader binaryReader, int characterCode)
    {
        if (!Cache.TryGetValue(characterCode, out var glyphIndex))
            foreach (var segment in Segments)
            {
                if (segment.StartCode > characterCode || segment.EndCode < characterCode)
                    continue;

                if (segment.IdRangeOffset > 0)
                {
                    var glyphIndexPosition = segment.IdRangeOffset + 2 * (characterCode - segment.StartCode);
                    var oldPosition = binaryReader.BaseStream.Position;

                    binaryReader.BaseStream.Position = glyphIndexPosition;
                    glyphIndex = binaryReader.ReadUInt16();
                    binaryReader.BaseStream.Position = oldPosition;
                }
                else
                    glyphIndex = (ushort)((segment.IdDelta + characterCode) & 0xFFFF);

                Cache[characterCode] = glyphIndex;
                break;
            }

        return glyphIndex;
    }
}
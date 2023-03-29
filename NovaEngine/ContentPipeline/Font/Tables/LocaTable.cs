namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the index to location table.</summary>
internal sealed class LocaTable
{
    /*********
    ** Properties
    *********/
    /// <summary>The offsets to the locations of the glyphs relative to the start of the 'glyf' table.</summary>
    public ImmutableArray<uint> GlyphOffsets { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the table.<br/>
    /// This assumes the reader has been positioned for the table to be read.
    /// </param>
    /// <param name="indexToLocaFormat">The index to loca format as specified in the 'head' table.</param>
    /// <param name="numberOfGlyphs">The number of glyphs as specified in the 'maxp' table.</param>
    public LocaTable(BinaryReader binaryReader, short indexToLocaFormat, ushort numberOfGlyphs)
    {
        var glyphOffsets = new List<uint>();
        
        for (var i = 0; i < numberOfGlyphs; i++)
            if (indexToLocaFormat == 0)
                glyphOffsets.Add(binaryReader.ReadUInt16BigEndian() * 2u);
            else
                glyphOffsets.Add(binaryReader.ReadUInt32BigEndian());

        GlyphOffsets = glyphOffsets.ToImmutableArray();
    }
}

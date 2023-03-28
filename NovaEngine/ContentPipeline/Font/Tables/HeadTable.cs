namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the font header table.</summary>
internal sealed class HeadTable
{
    /*********
    ** Properties
    *********/
    /// <summary>The major version of the table.</summary>
    public ushort MajorVersion { get; }

    /// <summary>The minor version of the table.</summary>
    public ushort MinorVersion { get; }

    /// <summary>The font revision, as per the manufacturer.</summary>
    public float FontRevision { get; }

    /// <summary>The checksum adjustment of the table.</summary>
    public uint ChecksumAdjustment { get; }

    /// <summary>The flags of the font.</summary>
    public HeadTableFlags Flags { get; }

    /// <summary>The number of units per em, must be between 16 and 16384 (inclusive).</summary>
    /// <remarks>This determines the granularity of the font's coordinate grid at which coordinates can be specified for visual elements such as outline control points or mark-attachment anchor positions.</remarks>
    public ushort UnitsPerEm { get; }

    /// <summary>When the font was created.</summary>
    public DateTime Created { get; }

    /// <summary>When the font was last modified.</summary>
    public DateTime Modified { get; }

    /// <summary>The minimum x coordinate across all glyph bounding boxes.</summary>
    public short XMin { get; }

    /// <summary>The minimum y coordinate across all glyph bounding boxes.</summary>
    public short YMin { get; }

    /// <summary>The maximum x coordinate across all glyph bounding boxes.</summary>
    public short XMax { get; }

    /// <summary>The maximum y coordinate across all glyph bounding boxes.</summary>
    public short YMax { get; }

    /// <summary>The mac style of the font.</summary>
    public MacStyle MacStyle { get; }

    /// <summary>The smallest readable size of the font, in pixels.</summary>
    public ushort LowestRecPPEM { get; }

    /// <summary>The font direction hint, this is deprecated (should be 2).</summary>
    /// <remarks>
    /// <b>0</b>: Fully mixed directional glyphs.<br/>
    /// <b>1</b>: Only strongly left to right.<br/>
    /// <b>2</b>: Like 1 but also contains neutrals.<br/>
    /// <b>-1</b>: Only strongly right to left.<br/>
    /// <b>-2</b>: Like -1 but also contains neutrals.<br/><br/>
    /// A neutral character has no inherent directionality; it is not a character with zero (0) width. Spaces and punctuation are examples of neutral characters. Non-neutral characters are those with inherent directionality. For example, Roman letters (left-to-right) and Arabic letters (right-to-left) have directionality. In a “normal” Roman font where spaces and punctuation are present, the font direction hints should be set to two (2).
    /// </remarks>
    public short FontDirectionHint { get; }

    /// <summary>0 for <see langword="ushort"/> offsets, 1 for <see langword="uint"/>.</summary>
    public short IndexToLocFormat { get; }

    /// <summary>The glyph data format (0 for current format).</summary>
    public short GlyphDataFormat { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the table.<br/>
    /// This assumes the reader has been positioned for the table to be read.
    /// </param>
    /// <exception cref="FontException">Thrown if any of the specified values were invalid.</exception>
    public HeadTable(BinaryReader binaryReader)
    {
        MajorVersion = binaryReader.ReadUInt16BigEndian();
        MinorVersion = binaryReader.ReadUInt16BigEndian();
        FontRevision = binaryReader.Read16Dot16();
        ChecksumAdjustment = binaryReader.ReadUInt32BigEndian();
        var magicNumber = binaryReader.ReadUInt32BigEndian();
        Flags = (HeadTableFlags)binaryReader.ReadUInt16BigEndian();
        UnitsPerEm = binaryReader.ReadUInt16BigEndian();
        Created = binaryReader.ReadOTFDateTime();
        Modified = binaryReader.ReadOTFDateTime();
        XMin = binaryReader.ReadInt16BigEndian();
        YMin = binaryReader.ReadInt16BigEndian();
        XMax = binaryReader.ReadInt16BigEndian();
        YMax = binaryReader.ReadInt16BigEndian();
        MacStyle = (MacStyle)binaryReader.ReadUInt16BigEndian();
        LowestRecPPEM = binaryReader.ReadUInt16BigEndian();
        FontDirectionHint = binaryReader.ReadInt16BigEndian();
        IndexToLocFormat = binaryReader.ReadInt16BigEndian();
        GlyphDataFormat = binaryReader.ReadInt16BigEndian();

        if (magicNumber != 0x5F0F3CF5)
            throw new FontException("Font magic number is invalid.");

        if (IndexToLocFormat != 0 && IndexToLocFormat != 1)
            throw new FontException("Font indexToLocFormat to invalid.");
    }
}

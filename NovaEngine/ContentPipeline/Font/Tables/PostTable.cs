namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the PostScript info table.</summary>
internal sealed class PostTable
{
    /*********
    ** Properties
    *********/
    /// <summary>The version of the table. Either 1, 2, 2.5, or 3.</summary>
    public float Version { get; }

    /// <summary>The italic angle in anti-clockwise degrees from the vertical. Zero for upright text, negative for text that leans to the right (forward).</summary>
    public float ItalicAngle { get; }

    /// <summary>The suggested distance of the top of the underline from the baseline (negative values indicate below the baseline).</summary>
    public short UnderlinePosition { get; }

    /// <summary>The suggested values from the underline thickness.</summary>
    public short UnderlineThickness { get; }

    /// <summary><see langword="true"/>, if the font is not proportionally spaced (i.e. monospaced); otherwise, <see langword="false"/>.</summary>
    public bool IsFixedPitch { get; }

    /// <summary>The minimum memory usage when an OpenType font is downloaded.</summary>
    public uint MinMemType42 { get; }

    /// <summary>The maximum memory usage when an OpenType font is downloaded.</summary>
    public uint MaxMemType42 { get; }

    /// <summary>The minimum memory usage when an OpenType font is downloaded as a Type 1 font.</summary>
    public uint MinMemType1 { get; }

    /// <summary>The maximum memory usage when an OpenType font is downloaded as a Type 1 font.</summary>
    public uint MaxMemType1 { get; }

    /// <summary>The number of glyphs.</summary>
    /// <remarks>
    /// This is only set when <see cref="Version"/> is 2 or 2.5.<br/><br/>
    /// This should be the same as <see cref="MaxpTable.NumberOfGlyphs"/>.
    /// </remarks>
    public ushort NumberOfGlyphs { get; }

    /// <summary>An array of indices into the string data.</summary>
    /// <remarks>
    /// This is only set when <see cref="Version"/> is 2.<br/><br/>
    /// This maps glyph ids to a glyph name index. If the glyph name index is between 0 and 257 (inclusive), treat that index as a glyph index in the Macintosh standard glyph set and use the Macintosh glyph name. If the glyph name is between 258 and 65535, then subtract 258 and use that to index into the list of Pascal strings at the end of the table.
    /// </remarks>
    public ImmutableArray<ushort> GlyphNameIndexes { get; }

    /// <summary>The difference between graphic index and standard order of glyph.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 2.5.</remarks>
    public ImmutableArray<byte> Offsets { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the table.<br/>
    /// This assumes the reader has been positioned for the table to be read.
    /// </param>
    public PostTable(BinaryReader binaryReader)
    {
        Version = binaryReader.Read16Dot16();
        ItalicAngle = binaryReader.Read16Dot16();
        UnderlinePosition = binaryReader.ReadInt16BigEndian();
        UnderlineThickness = binaryReader.ReadInt16BigEndian();
        IsFixedPitch = binaryReader.ReadUInt32BigEndian() != 0;
        MinMemType42 = binaryReader.ReadUInt32BigEndian();
        MaxMemType42 = binaryReader.ReadUInt32BigEndian();
        MinMemType1 = binaryReader.ReadUInt32BigEndian();
        MaxMemType1 = binaryReader.ReadUInt32BigEndian();

        if (Version == 2)
        {
            NumberOfGlyphs = binaryReader.ReadUInt16BigEndian();

            var glyphNameIndexes = new List<ushort>();
            for (var i = 0; i < NumberOfGlyphs; i++)
                glyphNameIndexes.Add(binaryReader.ReadUInt16BigEndian());
            GlyphNameIndexes = glyphNameIndexes.ToImmutableArray();
        }

        if (Version == 2.5f)
        {
            NumberOfGlyphs = binaryReader.ReadUInt16BigEndian();
            Offsets = binaryReader.ReadBytes(NumberOfGlyphs).ToImmutableArray();
        }

        if (Version != 1 && Version != 2 && Version != 2.5f && Version != 3)
            throw new FontException("'post' version is invalid.");
    }
}

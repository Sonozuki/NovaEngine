namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the horizontal header table.</summary>
internal sealed class HheaTable
{
    /*********
    ** Properties
    *********/
    /// <summary>The major version of the table.</summary>
    public ushort MajorVersion { get; }

    /// <summary>The minor version of the table.</summary>
    public ushort MinorVersion { get; }

    /// <summary>The typographic ascender.</summary>
    /// <remarks>This is only used on Apple platforms. See <see cref="OS2Table.TypoAscender"/> for the Windows equivalent.</remarks>
    public short Ascender { get; }

    /// <summary>The typographic descender.</summary>
    /// <remarks>This is only used on Apple platforms. See <see cref="OS2Table.TypoDescender"/> for the Windows equivalent.</remarks>
    public short Descender { get; }

    /// <summary>The typographic line gap.</summary>
    /// <remarks>This is only used on Apple platforms. See <see cref="OS2Table.TypoLineGap"/> for the Windows equivalent.</remarks>
    public short LineGap { get; }

    /// <summary>The maximum advance width value in the 'hmtx' table.</summary>
    public ushort AdvanceWidthMax { get; }

    /// <summary>The minimum left side bearing value in the 'hmtx' table for glyphs with contours.</summary>
    public short MinLeftSideBearing { get; }

    /// <summary>The minimum right side bearing value; calculated as <c>min(aw - (lsb + xMax - xMin))</c> for glyphs with contours.</summary>
    public short MinRightSideBearing { get; }

    /// <summary><c>max(lsb + (xMax - xMin))</c></summary>
    public short XMaxExtent { get; }

    /// <summary>Used to calculate the slope of the cursor (rise/run); 1 for vertical.</summary>
    public short CaretSlopeRise { get; }

    /// <summary>Used to calculate the slope of the cursor (rise/run); 0 for vertical.</summary>
    public short CaretSlopeRun { get; }

    /// <summary>The amount by which a slanted highlight on a glyph need sto be shifted to produce the best appearance.</summary>
    /// <remarks>Should be 0 for non-slanted fonts.</remarks>
    public short CaretOffset { get; }

    /// <summary>The number of horizontal metrics in the 'hmtx' table.</summary>
    public ushort NumberOfHorizontalMetrics { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the table.<br/>
    /// This assumes the reader has been positioned for the table to be read.
    /// </param>
    public HheaTable(BinaryReader binaryReader)
    {
        MajorVersion = binaryReader.ReadUInt16BigEndian();
        MinorVersion = binaryReader.ReadUInt16BigEndian();
        Ascender = binaryReader.ReadInt16BigEndian();
        Descender = binaryReader.ReadInt16BigEndian();
        LineGap = binaryReader.ReadInt16BigEndian();
        AdvanceWidthMax = binaryReader.ReadUInt16BigEndian();
        MinLeftSideBearing = binaryReader.ReadInt16BigEndian();
        MinRightSideBearing = binaryReader.ReadInt16BigEndian();
        XMaxExtent = binaryReader.ReadInt16BigEndian();
        CaretSlopeRise = binaryReader.ReadInt16BigEndian();
        CaretSlopeRun = binaryReader.ReadInt16BigEndian();
        CaretOffset = binaryReader.ReadInt16BigEndian();

        binaryReader.BaseStream.Position += 10; // reserved * 4, metricDataFormat

        NumberOfHorizontalMetrics = binaryReader.ReadUInt16BigEndian();
    }
}

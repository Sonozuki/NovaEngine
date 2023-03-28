namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the maximum profile table.</summary>
internal sealed class MaxpTable
{
    /*********
    ** Properties
    *********/
    /// <summary>The version of the table. This is either 0.5 or 1.</summary>
    public float Version { get; }

    /// <summary>The number of glyphs in the font.</summary>
    public ushort NumberOfGlyphs { get; }

    /// <summary>The maximum number of points in a non-composite glyph.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfPoints { get; }

    /// <summary>The maximum number of contours in a non-composite glyph.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfContours { get; }

    /// <summary>The maximum number of points in a composite glyph.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfCompositePoints { get; }

    /// <summary>The maximum number of contours in a composite glyph.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfCompositeContours { get; }

    /// <summary>1, if instructions do not use the twilight zone (Z0); or 2, if instructions do use Z0.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfZones { get; }

    /// <summary>Maximum number of points used in the twilight zone.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfTwilightPoints { get; }

    /// <summary>The number of Storage Area locations.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfStorageAreaLocations { get; }

    /// <summary>The number of FDEFs, equal to the highest function number + 1.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfFunctionDefs { get; }

    /// <summary>The number of IDEFs.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfInstructionDefs { get; }

    /// <summary>The maximum stack depth across Font Program ('fpgm'), CVT Program ('prep'), and all glyph instructions (in 'glyf').</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfStackElements { get; }

    /// <summary>The maximum byte count for glyph instructions.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxSizeOfInstructions { get; }

    /// <summary>The maximum number of components referenced at "top level" for any composite glyph.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfComponentElements { get; }

    /// <summary>The maximum levels of recursion; 1 for simple components.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort MaxNumberOfComponentDepths { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the table.<br/>
    /// This assumes the reader has been positioned for the table to be read.
    /// </param>
    public MaxpTable(BinaryReader binaryReader)
    {
        Version = binaryReader.Read16Dot16();
        NumberOfGlyphs = binaryReader.ReadUInt16BigEndian();

        if (Version == 1)
        {
            MaxNumberOfPoints = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfContours = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfCompositePoints = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfCompositeContours = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfZones = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfTwilightPoints = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfStorageAreaLocations = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfFunctionDefs = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfInstructionDefs = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfStackElements = binaryReader.ReadUInt16BigEndian();
            MaxSizeOfInstructions = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfComponentElements = binaryReader.ReadUInt16BigEndian();
            MaxNumberOfComponentDepths = binaryReader.ReadUInt16BigEndian();
        }

        if (Version != .5f && Version != 1)
            throw new FontException("'maxp' version is invalid.");
    }
}

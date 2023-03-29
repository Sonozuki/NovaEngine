namespace NovaEngine.ContentPipeline.Font.Cmap;

/// <summary>Represents a character map subtable.</summary>
internal abstract class CmapFormatBase
{
    /*********
    ** Properties
    *********/
    /// <summary>The format of the subtable.</summary>
    public abstract ushort Format { get; }

    /// <summary>The length of the subtable, in <see langword="byte"/>s.</summary>
    public ushort Length { get; }

    /// <summary>Only used on <see cref="PlatformId.Macintosh"/>, the value is the Macintosh language Id of the subtable plus one, or zero if the subtable is not language-specific.</summary>
    public ushort Language { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">The binary reader to use to read the subtable.</param>
    public CmapFormatBase(BinaryReader binaryReader)
    {
        Length = binaryReader.ReadUInt16BigEndian();
        Language = binaryReader.ReadUInt16BigEndian();
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves the glyph index for a character.</summary>
    /// <param name="binaryReader">The binary reader to use to retrieve the glyph index.</param>
    /// <param name="characterCode">The character code to get the glyph index of.</param>
    /// <returns>The glyph index.</returns>
    public abstract ushort Map(BinaryReader binaryReader, int characterCode);
}

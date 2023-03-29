namespace NovaEngine.ContentPipeline.Font.Cmap;

/// <summary>Represents a format 0 character map subtable.</summary>
internal sealed class CmapFormat0 : CmapFormatBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The glyph index to character code map.</summary>
    private readonly byte[] GlyphIndices;


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public override ushort Format => 0;


    /*********
    ** Constructors
    *********/
    /// <inheritdoc/>
    public CmapFormat0(BinaryReader binaryReader)
        : base(binaryReader)
    {
        GlyphIndices = binaryReader.ReadBytes(256);
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override ushort Map(BinaryReader binaryReader, int characterCode)
    {
        if (characterCode >= 0 && characterCode <= 255)
            return GlyphIndices[characterCode];

        return 0;
    }
}

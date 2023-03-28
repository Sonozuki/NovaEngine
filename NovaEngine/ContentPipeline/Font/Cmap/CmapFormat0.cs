namespace NovaEngine.ContentPipeline.Font.Cmap;

/// <summary>Represents a format 0 character map subtable.</summary>
internal sealed class CmapFormat0 : CmapFormatBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The glyph index to character code map.</summary>
    private readonly byte[] GlyphIds;


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
        GlyphIds = binaryReader.ReadBytes(256);
    }
}

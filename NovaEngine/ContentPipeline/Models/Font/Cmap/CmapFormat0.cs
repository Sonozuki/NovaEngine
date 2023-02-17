namespace NovaEngine.ContentPipeline.Models.Font.Cmap;

/// <summary>Represents a format 0 cmap subtable.</summary>
internal sealed class CmapFormat0 : ICmapFormat
{
    /*********
    ** Fields
    *********/
    /// <summary>The character codes mapped to glyph indices.</summary>
    private readonly byte[] GlyphIndices;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">The binary reader whose current position is at the start of the format 0 cmap subtable content.</param>
    public CmapFormat0(BinaryReader binaryReader)
    {
        GlyphIndices = binaryReader.ReadBytes(256);
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public ushort Map(BinaryReader binaryReader, int characterCode)
    {
        if (characterCode >= 0 && characterCode <= 255)
            return GlyphIndices[characterCode];

        return 0;
    }
}

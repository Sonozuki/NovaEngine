namespace NovaEngine.Content.Readers;

/// <summary>Defines how a font should be read from a nova file.</summary>
[ContentReader("font", typeof(Font))]
public class FontReader : IContentReader
{
    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public object? Read(Stream stream, Type outputType)
    {
        using var binaryReader = new BinaryReader(stream);

        // name
        var name = binaryReader.ReadString();

        // atlas
        var atlasEdgeLength = binaryReader.ReadUInt32();
        var pixelBuffer = binaryReader.ReadBytes((int)(atlasEdgeLength * atlasEdgeLength * 4));
        var pixels = MemoryMarshal.Cast<byte, Colour>(pixelBuffer);
        
        var atlas = new Texture2D(atlasEdgeLength, atlasEdgeLength);
        // TODO: populate atlas
         
        // glyph positions
        var count = binaryReader.ReadInt32();
        var glyphPositions = new List<GlyphPosition>();
        for (int i = 0; i < count; i++)
            glyphPositions.Add(new(binaryReader.ReadChar(), new(binaryReader.ReadUInt16(), binaryReader.ReadUInt16(), binaryReader.ReadUInt16(), binaryReader.ReadUInt16())));

        return new Font(name, atlas, glyphPositions);
    }
}

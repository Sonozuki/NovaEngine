using NovaEngine.ContentPipeline.Models.Font;

namespace NovaEngine.ContentPipeline.Readers;

/// <summary>Defines how a font should be read from a nova file.</summary>
public class FontReader : IContentReader
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public string Type => "font";

    /// <inheritdoc/>
    public Type[] OutputTypes => new[] { typeof(Font) };


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public unsafe object? Read(FileStream novaFileStream, Type outputType)
    {
        using var binaryReader = new BinaryReader(novaFileStream);

        var name = binaryReader.ReadString();
        var maxGlyphHeight = binaryReader.ReadSingle();
        var pixelRange = binaryReader.ReadInt32();

        var atlasEdgeLength = binaryReader.ReadUInt32();
        var pixelBuffer = binaryReader.ReadBytes((int)(atlasEdgeLength * atlasEdgeLength * sizeof(Colour32)));
        var pixels = MemoryMarshal.Cast<byte, Colour32>(pixelBuffer);
        
        var atlas = new Texture2D(atlasEdgeLength, atlasEdgeLength, pixelType: TexturePixelType.Float);
        atlas.SetPixels(pixels.ToArray());
        
        var count = binaryReader.ReadInt32();
        var glyphs = new List<GlyphData>();
        for (var i = 0; i < count; i++)
        {
            var character = binaryReader.ReadChar();
            var size = new Vector2I(binaryReader.ReadUInt16(), binaryReader.ReadUInt16());
            var atlasPosition = new Rectangle(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            var horizontalMetrics = new HorizontalMetrics(binaryReader.ReadUInt16(), binaryReader.ReadUInt16());

            glyphs.Add(new(character, size, atlasPosition, horizontalMetrics));
        }

        return new Font(name, maxGlyphHeight, pixelRange, atlas, glyphs);
    }
}

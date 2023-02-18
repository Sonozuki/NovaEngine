using NovaEngine.ContentPipeline.Models.Font;

namespace NovaEngine.ContentPipeline.Packers;

/// <summary>Defines how a font should be rewritten for a nova file.</summary>
public class FontPacker : IContentPacker
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public string Type => "font";

    /// <inheritdoc/>
    public string[] Extensions => new[] { "ttf" };


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public unsafe Stream Write(FileStream fileStream)
    {
        using var ttf = new TrueTypeFont(fileStream);
        var atlas = AtlasPacker.CreateAtlas(ttf);

        var memoryStream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8, true);

        binaryWriter.Write(ttf.Name);
        binaryWriter.Write(TrueTypeFont.MaxGlyphHeight);
        binaryWriter.Write(AtlasPacker.PixelRange);

        // TODO: add support for multiple texture atlases
        var atlasEdgeLength = (uint)atlas.GetLength(0);
        binaryWriter.Write(atlasEdgeLength);

        var pixelsBuffer = new byte[atlasEdgeLength * atlasEdgeLength * sizeof(Colour32)];
        fixed (Colour32* atlasPointer = atlas)
        fixed (byte* pixelsBufferPointer = pixelsBuffer)
            Buffer.MemoryCopy(atlasPointer, pixelsBufferPointer, pixelsBuffer.Length, pixelsBuffer.Length);
        binaryWriter.Write(pixelsBuffer);

        binaryWriter.Write(ttf.Glyphs.Count);
        foreach (var glyph in ttf.Glyphs)
        {
            binaryWriter.Write(glyph.Character);

            binaryWriter.Write((ushort)(glyph.ScaledBounds.Width + 2 * AtlasPacker.PixelRange));
            binaryWriter.Write((ushort)(glyph.ScaledBounds.Height + 2 * AtlasPacker.PixelRange));

            binaryWriter.Write((glyph.ScaledBounds.X - AtlasPacker.PixelRange) / atlasEdgeLength);
            binaryWriter.Write((glyph.ScaledBounds.Y - AtlasPacker.PixelRange) / atlasEdgeLength);
            binaryWriter.Write((glyph.ScaledBounds.Width + 2 * AtlasPacker.PixelRange) / atlasEdgeLength);
            binaryWriter.Write((glyph.ScaledBounds.Height + 2 * AtlasPacker.PixelRange) / atlasEdgeLength);

            binaryWriter.Write(glyph.HorizontalMetrics.AdvanceWidth);
            binaryWriter.Write(glyph.HorizontalMetrics.LeftSideBearing);
        }

        // TODO: kerning

        return memoryStream;
    }
}

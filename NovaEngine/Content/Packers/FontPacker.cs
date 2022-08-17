using NovaEngine.Content.Models.Font;

namespace NovaEngine.Content.Packers;

/// <summary>Defines how a font should be rewritten for a nova file.</summary>
[ContentPacker("font", ".tff")]
public class FontPacker : IContentPacker
{
    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public unsafe Stream Write(string file)
    {
        using var ttf = new TrueTypeFont(file);
        var atlas = AtlasPacker.CreateAtlas(ttf);

        var memoryStream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8, true);

        // name
        binaryWriter.Write(ttf.Name);

        // TODO: add support for multiple texture atlases
        // texture atlas edge length
        var atlasEdgeLength = (uint)atlas.GetLength(0);
        binaryWriter.Write(atlasEdgeLength);

        // atlas pixel data
        var pixelsBuffer = new byte[atlasEdgeLength * atlasEdgeLength * sizeof(Colour32)];
        fixed (Colour32* atlasPointer = atlas)
        fixed (byte* pixelsBufferPointer = pixelsBuffer)
            Buffer.MemoryCopy(atlasPointer, pixelsBufferPointer, pixelsBuffer.Length, pixelsBuffer.Length);
        binaryWriter.Write(pixelsBuffer);

        // glyphs
        binaryWriter.Write(ttf.Glyphs.Count);
        foreach (var glyph in ttf.Glyphs)
        {
            binaryWriter.Write(glyph.Character);

            // glyph size
            binaryWriter.Write((ushort)glyph.ScaledBounds.Width);
            binaryWriter.Write((ushort)glyph.ScaledBounds.Height);

            // atlas rectangle
            binaryWriter.Write((float)(glyph.ScaledBounds.X / atlasEdgeLength));
            binaryWriter.Write((float)(glyph.ScaledBounds.Y / atlasEdgeLength));
            binaryWriter.Write((float)(glyph.ScaledBounds.Width / atlasEdgeLength));
            binaryWriter.Write((float)(glyph.ScaledBounds.Height / atlasEdgeLength));

            // horizontal metrics
            binaryWriter.Write(glyph.HorizontalMetrics.AdvanceWidth);
            binaryWriter.Write(glyph.HorizontalMetrics.LeftSideBearing);
        }

        // TODO: kerning

        return memoryStream;
    }
}
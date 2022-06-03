using NovaEngine.Content.Models.Font;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

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
        binaryWriter.Write((uint)atlas.GetLength(0));

        // atlas pixel data
        var pixelsBuffer = new byte[atlas.GetLength(0) * atlas.GetLength(1) * 4];
        fixed (Colour* atlasPointer = atlas)
        fixed (byte* pixelsBufferPointer = pixelsBuffer)
            Buffer.MemoryCopy(atlasPointer, pixelsBufferPointer, pixelsBuffer.Length, pixelsBuffer.Length);
        binaryWriter.Write(pixelsBuffer);

        // glyph atlas position data
        binaryWriter.Write(ttf.Glyphs.Count);
        foreach (var glyph in ttf.Glyphs)
        {
            binaryWriter.Write(glyph.Character);
            binaryWriter.Write((ushort)glyph.ScaledBounds.X);
            binaryWriter.Write((ushort)glyph.ScaledBounds.Y);
            binaryWriter.Write((ushort)glyph.ScaledBounds.Width);
            binaryWriter.Write((ushort)glyph.ScaledBounds.Height);
        }

        // TODO: kerning

        return memoryStream;
    }
}
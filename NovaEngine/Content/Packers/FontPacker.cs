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
    public Stream Write(string file)
    {
        var ttf = new TrueTypeFont(file);

        var glyph = ttf.Glyphs[1];
        var pixels = MTSDF.GenerateMTSDF(glyph);
        GenerateOutput(pixels);

        return new MemoryStream();
    }

    private static void GenerateOutput(Colour128[,] pixels)
    {
        byte ConvertToByte(float value) => (byte)Math.Clamp(value * 255f, 0, 255);

        List<byte> buffer = new();
        for (int y = pixels.GetLength(1) - 1; y >= 0; y--)
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                buffer.Add(ConvertToByte(pixels[x, y].R));
                buffer.Add(ConvertToByte(pixels[x, y].G));
                buffer.Add(ConvertToByte(pixels[x, y].B));
                buffer.Add(ConvertToByte(pixels[x, y].A));
            }

        using var image = Image.LoadPixelData<Rgba32>(buffer.ToArray(), pixels.GetLength(0), pixels.GetLength(1));
        image.SaveAsPng(Path.Combine(Environment.CurrentDirectory, "test.png"));
    }
}
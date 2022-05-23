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
        var atlas = AtlasPacker.CreateAtlas(ttf);

        GenerateOutput(atlas);

        return new MemoryStream();
    }

    private static void GenerateOutput(Colour[,] pixels)
    {
        List<byte> buffer = new();
        for (int y = 0; y < pixels.GetLength(1); y++)
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                buffer.Add(pixels[x, y].R);
                buffer.Add(pixels[x, y].G);
                buffer.Add(pixels[x, y].B);
                buffer.Add(pixels[x, y].A);
            }

        using var image = Image.LoadPixelData<Rgba32>(buffer.ToArray(), pixels.GetLength(0), pixels.GetLength(1));
        image.SaveAsPng(Path.Combine(Environment.CurrentDirectory, "test.png"));
    }
}
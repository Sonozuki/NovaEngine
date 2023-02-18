using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace NovaEngine.ContentPipeline.Packers;

/// <summary>Defines how a texture should be rewritten for a nova file.</summary>
public class TexturePacker : IContentPacker
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public string Type => "texture2d";

    /// <inheritdoc/>
    public string[] Extensions => new[] { "bmp", "gif", "jpg", "png" };


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public Stream Write(FileStream fileStream)
    {
        using var image = Image.Load<Rgba32>(fileStream);

        var stream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true);

        binaryWriter.Write(image.Width);
        binaryWriter.Write(image.Height);

        for (var row = 0; row < image.Height; row++)
            binaryWriter.Write(MemoryMarshal.Cast<Rgba32, byte>(image.GetPixelRowSpan(row)));

        return stream;
    }
}

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace NovaEngine.ContentPipeline.Unpackers;

/// <summary>Defines how a texture should be rewritten from a nova file.</summary>
public class TextureUnpacker : IContentUnpacker
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public string Type => "texture2d";

    /// <inheritdoc/>
    public string Extension => "png";


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public void Write(Stream stream, string destinationFile)
    {
        using var binaryReader = new BinaryReader(stream);
        var width = binaryReader.ReadInt32();
        var height = binaryReader.ReadInt32();
        var pixels = MemoryMarshal.Cast<byte, Rgba32>(binaryReader.ReadBytes(width * height * 4));

        using var image = Image.LoadPixelData(pixels.ToArray(), width, height);
        image.SaveAsPng(destinationFile);
    }
}

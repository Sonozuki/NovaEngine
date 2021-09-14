using NovaEngine.Content.Attributes;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Runtime.InteropServices;

namespace NovaEngine.Content.Unpackers
{
    /// <summary>Defines how a texture should be rewritten from a nova file.</summary>
    [ContentUnpacker("texture2d", ".png")]
    public class TextureUnpacker : IContentUnpacker
    {
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
}

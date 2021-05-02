using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing;
using System.IO;

namespace NovaEngine.Content.Unpackers
{
    /// <summary>Defines how a texture should be rewritten from a nova file.</summary>
    public class TextureUnpacker : IContentUnpacker
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public string Type => "texture2d";

        /// <inheritdoc/>
        public string Extension => ".png";


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public void Write(Stream stream, string destinationFile)
        {
            using (var binaryReader = new BinaryReader(stream))
            {
                var width = binaryReader.ReadInt32();
                var height = binaryReader.ReadInt32();

                var pixels = binaryReader.ReadBytes(width * height);
                using (var image = Image.Load(pixels))
                    image.SaveAsPng(destinationFile);
            }
        }
    }
}

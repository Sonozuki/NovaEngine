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

                using (var bitmap = new Bitmap(width, height))
                {
                    for (int y = 0; y < bitmap.Height; y++)
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            var r = binaryReader.ReadByte();
                            var g = binaryReader.ReadByte();
                            var b = binaryReader.ReadByte();
                            var a = binaryReader.ReadByte();

                            var pixel = Color.FromArgb(a, r, g, b);
                            bitmap.SetPixel(x, y, pixel);
                        }

                    bitmap.Save(destinationFile);
                }
            }
        }
    }
}

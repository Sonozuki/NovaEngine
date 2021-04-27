using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace NovaEngine.Content.Packers
{
    /// <summary>Defines how a texture should be rewritten for a nova file.</summary>
    public class TexturePacker : IContentPacker
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public string Type => "texture2d";

        /// <inheritdoc/>
        public List<string> Extensions => new() { ".bmp", ".gif", ".jpg", ".png", ".tiff" };


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public Stream Write(string file)
        {
            using (var bitmap = new Bitmap(file))
            {
                // populate memory stream with raw pixel data
                var stream = new MemoryStream();
                using (var binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    binaryWriter.Write(bitmap.Width);
                    binaryWriter.Write(bitmap.Height);

                    for (int y = 0; y < bitmap.Height; y++)
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            var pixel = bitmap.GetPixel(x, y);
                            binaryWriter.Write(pixel.R);
                            binaryWriter.Write(pixel.B);
                            binaryWriter.Write(pixel.G);
                            binaryWriter.Write(pixel.A);
                        }
                }

                return stream;
            }
        }
    }
}

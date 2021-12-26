using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

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
        public List<string> Extensions => new() { ".bmp", ".gif", ".jpg", ".png" };


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public Stream Write(string file)
        {
            using (var image = Image.Load<Rgba32>(file))
            {
                // populate memory stream with raw pixel data
                var stream = new MemoryStream();
                using (var binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    binaryWriter.Write(image.Width);
                    binaryWriter.Write(image.Height);

                    for (int row = 0; row < image.Height; row++)
                        binaryWriter.Write(MemoryMarshal.Cast<Rgba32, byte>(image.GetPixelRowSpan(row)));
                }

                return stream;
            }
        }
    }
}

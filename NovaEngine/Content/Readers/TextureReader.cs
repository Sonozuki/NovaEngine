using NovaEngine.Content.Readers.Attributes;
using NovaEngine.Graphics;
using System;
using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how a texture should be read from a nova file.</summary>
    [ContentReader("texture2d", typeof(Texture2D))]
    public class TextureReader : IContentReader
    {
        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public unsafe object Read(Stream stream, Type outputType, string? additionalInformation = null)
        {
            using (var binaryReader = new BinaryReader(stream))
            {
                var width = (uint)binaryReader.ReadInt32();
                var height = (uint)binaryReader.ReadInt32();

                // read remaining pixels data
                var pixels = binaryReader.ReadBytes((int)(width * height * 4));

                // convert pixels data to multidimensional array
                var pixelsArray = new Colour[height, width];
                fixed (byte* pixelsPointer = pixels)
                fixed (Colour* pixelsArrayPointer = pixelsArray)
                    Buffer.MemoryCopy(pixelsPointer, pixelsArrayPointer, pixels.Length, pixels.Length);

                // create the texture
                var texture = new Texture2D(width, height);
                texture.SetPixels(pixelsArray);
                return texture;
            }
        }
    }
}

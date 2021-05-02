using NovaEngine.Graphics;
using System;
using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how a texture should be read from a nova file.</summary>
    public class TextureReader : ContentReaderBase<Texture2D>
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public override string Type => "texture2d";


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public unsafe override Texture2D Read(Stream stream)
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

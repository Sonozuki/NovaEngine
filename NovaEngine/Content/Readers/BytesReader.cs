using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how bytes should be read from a nova file.</summary>
    public class BytesReader : ContentReaderBase<byte[]>
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public override string Type => "bytes";


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public override byte[] Read(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}

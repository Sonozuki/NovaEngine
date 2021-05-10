using NovaEngine.Content.Readers.Attributes;
using System;
using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how bytes should be read from a nova file.</summary>
    [ContentReader("bytes", typeof(byte[]))]
    public class BytesReader : IContentReader
    {
        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public object Read(Stream stream, Type outputType, string? additionalInformation = null)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}

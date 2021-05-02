using System.IO;

namespace NovaEngine.Content.Unpackers
{
    /// <summary>Defines how a binary file should be rewritten from a nova file.</summary>
    public class BytesUnpacker : IContentUnpacker
    {
        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public string Type => "bytes";

        /// <inheritdoc/>
        public string Extension => ".bin";


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public void Write(Stream stream, string destinationFile)
        {
            using (var fileStream = File.Create(destinationFile))
                stream.CopyTo(fileStream);
        }
    }
}

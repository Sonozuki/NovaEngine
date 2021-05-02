using System.Collections.Generic;
using System.IO;

namespace NovaEngine.Content.Packers
{
    /// <summary>Defines how a binary file should be rewritten for a nova file.</summary>
    public class BytesPacker : IContentPacker
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public string Type => "bytes";

        /// <inheritdoc/>
        public List<string> Extensions => new() { ".spv" };


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public Stream Write(string file) => File.OpenRead(file);
    }
}

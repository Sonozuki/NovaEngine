using NovaEngine.Content.Attributes;
using System.IO;

namespace NovaEngine.Content.Packers
{
    /// <summary>Defines how a binary file should be rewritten for a nova file.</summary>
    [ContentPacker("bytes", ".spv")]
    public class BytesPacker : IContentPacker
    {
        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public Stream Write(string file) => File.OpenRead(file);
    }
}

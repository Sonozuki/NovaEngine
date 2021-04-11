using System.Collections.Generic;
using System.IO;

namespace NovaEngine.Content.Packers
{
    /// <summary>Defines how a file should be rewritten for a nova file.</summary>
    public interface IContentPacker
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The type of content the packer is for.</summary>
        /// <remarks>For example 'texture', this is used so an asset file can't be used with the wrong reader.</remarks>
        public string Type { get; }

        /// <summary>The extensions the content packer can handle.</summary>
        public List<string> Extensions { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Writes a file to a <see cref="Stream"/> (the strem that will be written to the nova file).</summary>
        /// <param name="file">The file to write to the stream.</param>
        /// <returns>The file as a <see cref="Stream"/>.</returns>
        /// <remarks>This is used to order file contents to a format this is faster to read from for a <see cref="IContentReader"/>.</remarks>
        public Stream Write(string file);
    }
}

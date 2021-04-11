using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how content should be read from a nova file.</summary>
    /// <typeparam name="T">The type the reader can handle.</typeparam>
    public abstract class ContentReaderBase<T> : IContentReader
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public abstract string Type { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Reads content from a <see cref="Stream"/>.</summary>
        /// <param name="stream">The stream to read the content from.</param>
        /// <returns>The content read from the stream.</returns>
        /// <exception cref="ContentLoaderException">Thrown if the content failed to be read.</exception>
        public abstract T Read(Stream stream);
    }
}

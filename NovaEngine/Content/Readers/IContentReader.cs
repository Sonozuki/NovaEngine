using System;
using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how content should be read from a nova file.</summary>
    public interface IContentReader
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Reads content from a <see cref="Stream"/>.</summary>
        /// <param name="stream">The stream to read the content from.</param>
        /// <param name="outputType">The type to read to.</param>
        /// <returns>The content read from the stream.</returns>
        /// <exception cref="ContentException">Thrown if the content failed to be read.</exception>
        public abstract object? Read(Stream stream, Type outputType);
    }
}

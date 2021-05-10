using NovaEngine.Content.Readers.Attributes;
using NovaEngine.Extensions;
using NovaEngine.SceneManagement;
using System;
using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how a scene should be read from a nova file.</summary>
    [ContentReader("scene", typeof(Scene))]
    public class SceneReader : IContentReader
    {
        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public object Read(Stream stream, Type outputType, string? additionalInformation = null)
        {
            using (var binaryReader = new BinaryReader(stream))
                return binaryReader.ReadScene();
        }
    }
}

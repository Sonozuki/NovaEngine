using NovaEngine.Extensions;
using NovaEngine.SceneManagement;
using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how a scene should be read from a nova file.</summary>
    public class SceneReader : ContentReaderBase<Scene>
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public override string Type => "scene";


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public override Scene Read(Stream stream)
        {
            using (var binaryReader = new BinaryReader(stream))
                return binaryReader.ReadScene();
        }
    }
}

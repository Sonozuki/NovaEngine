using NovaEngine.Content.Models;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using System.IO;

namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="BinaryReader"/> class.</summary>
    internal static class BinaryReaderExtensions
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Reads a <see cref="ModelContent"/> from the current stream.</summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
        /// <returns>The read <see cref="ModelContent"/>.</returns>
        public static ModelContent ReadModelContent(this BinaryReader reader)
        {
            var modelContent = new ModelContent();

            // read meshes
            var length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
                modelContent.Meshes.Add(reader.ReadMeshContent());

            return modelContent;
        }

        /// <summary>Reads a <see cref="MeshContent"/> from the current stream.</summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
        /// <returns>The read <see cref="MeshContent"/>.</returns>
        public static MeshContent ReadMeshContent(this BinaryReader reader)
        {
            var meshContent = new MeshContent(reader.ReadString());

            // read vertices
            var length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
                meshContent.Vertices.Add(reader.ReadVertex());

            // read indices
            length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
                meshContent.Indices.Add(reader.ReadUInt32());

            return meshContent;
        }

        /// <summary>Reads a <see cref="Vertex"/> from the current stream.</summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
        /// <returns>The read <see cref="Vertex"/>.</returns>
        public static Vertex ReadVertex(this BinaryReader reader) => new(reader.ReadVector3(), reader.ReadVector2(), reader.ReadVector3());

        /// <summary>Reads a <see cref="Vector3"/> from the current stream.</summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
        /// <returns>The read <see cref="Vector3"/>.</returns>
        public static Vector3 ReadVector3(this BinaryReader reader) => new(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

        /// <summary>Reads a <see cref="Vector2"/> from the current stream.</summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
        /// <returns>The read <see cref="Vector2"/>.</returns>
        public static Vector2 ReadVector2(this BinaryReader reader) => new(reader.ReadSingle(), reader.ReadSingle());
    }
}

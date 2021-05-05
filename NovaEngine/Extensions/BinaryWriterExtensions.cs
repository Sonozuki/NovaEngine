using NovaEngine.Content.Models;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using System.IO;

namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="BinaryWriter"/> class.</summary>
    internal static class BinaryWriterExtensions
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Writes a <see cref="ModelContent"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="modelContent">The <see cref="ModelContent"/> to write.</param>
        public static void Write(this BinaryWriter writer, ModelContent modelContent)
        {
            // write meshes
            writer.Write(modelContent.Meshes.Count);
            foreach (var mesh in modelContent.Meshes)
                writer.Write(mesh);
        }

        /// <summary>Writes a <see cref="MeshContent"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="meshContent">The <see cref="MeshContent"/> to write.</param>
        public static void Write(this BinaryWriter writer, MeshContent meshContent)
        {
            // write name
            writer.Write(meshContent.Name);

            // write vertex data
            writer.Write(meshContent.Vertices.Count);
            foreach (var vertex in meshContent.Vertices)
                writer.Write(vertex);

            // write index data
            writer.Write(meshContent.Indices.Count);
            foreach (var index in meshContent.Indices)
                writer.Write(index);
        }

        /// <summary>Writes a <see cref="Vertex"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="vertex">The <see cref="Vertex"/> to write.</param>
        public static void Write(this BinaryWriter writer, Vertex vertex)
        {
            writer.Write(vertex.Position);
            writer.Write(vertex.TextureCoordinates);
            writer.Write(vertex.Normal);
        }

        /// <summary>Writes a <see cref="Vector3"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="vector">The <see cref="Vector3"/> to write.</param>
        public static void Write(this BinaryWriter writer, Vector3 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        /// <summary>Writes a <see cref="Vector2"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="vector">The <see cref="Vector2"/> to write.</param>
        public static void Write(this BinaryWriter writer, Vector2 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }
    }
}

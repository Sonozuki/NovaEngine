using NovaEngine.Content.Models;
using NovaEngine.Core;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using NovaEngine.SceneManagement;
using System.IO;

namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="BinaryWriter"/> class.</summary>
    public static class BinaryWriterExtensions
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Writes a <see cref="Scene"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="scene">The <see cref="GameObject"/> to write.</param>
        public static void Write(this BinaryWriter writer, Scene scene)
        {
            // name
            writer.Write(scene.Name);

            // game objects
            writer.Write(scene.RootGameObjects.Count);
            foreach (var gameObject in scene.RootGameObjects)
                writer.Write(gameObject);
        }

        /// <summary>Writes a <see cref="GameObject"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="gameObject">The <see cref="GameObject"/> to write.</param>
        public static void Write(this BinaryWriter writer, GameObject gameObject)
        {
            // name
            writer.Write(gameObject.Name);

            // isEnabled
            writer.Write(gameObject.IsEnabled);

            // transform
            writer.Write(gameObject.Transform.LocalPosition);
            writer.Write(gameObject.Transform.LocalRotation);
            writer.Write(gameObject.Transform.LocalScale);

            // components
            writer.Write(gameObject.Components.Count);
            foreach (var component in gameObject.Components)
                writer.Write(component.GetType().FullName!);

            // children
            writer.Write(gameObject.Children.Count);
            foreach (var child in gameObject.Children)
                writer.Write(child);
        }

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
            // name
            writer.Write(meshContent.Name);

            // vertex data
            writer.Write(meshContent.Vertices.Count);
            foreach (var vertex in meshContent.Vertices)
                writer.Write(vertex);

            // index data
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

        /// <summary>Writes a <see cref="Quaternion"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="quaternion">The <see cref="Quaternion"/> to write.</param>
        public static void Write(this BinaryWriter writer, Quaternion quaternion)
        {
            writer.Write(quaternion.X);
            writer.Write(quaternion.Y);
            writer.Write(quaternion.Z);
            writer.Write(quaternion.W);
        }
    }
}

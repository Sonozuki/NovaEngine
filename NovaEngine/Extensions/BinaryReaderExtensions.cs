using NovaEngine.Content;
using NovaEngine.Content.Models;
using NovaEngine.Core;
using NovaEngine.Core.Components;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using NovaEngine.SceneManagement;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="BinaryReader"/> class.</summary>
    internal static class BinaryReaderExtensions
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Reads a <see cref="Scene"/> from the current stream.</summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
        /// <returns>The read <see cref="Scene"/>.</returns>
        public static Scene ReadScene(this BinaryReader reader)
        {
            var scene = new Scene(reader.ReadString(), true);

            var length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
                scene.RootGameObjects.Add(reader.ReadGameObject());

            return scene;
        }

        /// <summary>Reads a <see cref="Scene"/> from the current stream.</summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
        /// <returns>The read <see cref="Scene"/>.</returns>
        public static GameObject ReadGameObject(this BinaryReader reader)
        {
            var gameObject = new GameObject(reader.ReadString(), isEnabled: reader.ReadBoolean());

            // transform
            gameObject.Transform.LocalPosition = reader.ReadVector3();
            gameObject.Transform.LocalRotation = reader.ReadQuaternion();
            gameObject.Transform.LocalScale = reader.ReadVector3();

            // components
            var length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
            {
                var assembly = Assembly.GetExecutingAssembly();

                var typeName = reader.ReadString().ToLower();
                var type = assembly.GetTypes().FirstOrDefault(type => type.FullName?.ToLower() == typeName);
                if (type == null)
                    throw new InvalidOperationException($"Couldn't find type: {type}.");

                // create the component
                object? component;
                if (reader.ReadBoolean()) // check if the component should be instantiated or loaded through the content pipeline
                    component = FormatterServices.GetUninitializedObject(type);
                else
                    component = ContentLoader.Load(reader.ReadString(), type, reader.ReadString());
                if (component is not ComponentBase componentBase)
                    throw new InvalidDataException($"{typeName} isn't a component.");

                // TODO: default property values of component

                gameObject.AddComponent(componentBase);
            }

            // children
            length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
                gameObject.Children.Add(reader.ReadGameObject()); // TODO: parent doesn't get set, should be fixed when GameObject.Children becomes a custom collection

            return gameObject;
        }

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
            var meshContent = new MeshContent(new Guid(reader.ReadString()), reader.ReadString());

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

        /// <summary>Reads a <see cref="Quaternion"/> from the current stream.</summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
        /// <returns>The read <see cref="Quaternion"/>.</returns>
        public static Quaternion ReadQuaternion(this BinaryReader reader) => new(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
    }
}

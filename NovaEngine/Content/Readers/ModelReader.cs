using NovaEngine.Content.Models;
using NovaEngine.Core;
using NovaEngine.Core.Components;
using NovaEngine.Extensions;
using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how a model should be read from a nova file.</summary>
    public class ModelReader : ContentReaderBase<GameObject>
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public override string Type => "3dmodel";


        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public override GameObject Read(Stream stream)
        {
            var parentGameObject = new GameObject("Model");

            // retrieve model content from stream
            ModelContent modelContent;
            using (var reader = new BinaryReader(stream))
                modelContent = reader.ReadModelContent();

            // convert each mesh to a game object
            foreach (var meshContent in modelContent.Meshes)
            {
                var meshGameObject = new GameObject(meshContent.Name);
                meshGameObject.AddComponent(new MeshRenderer(new(meshContent.Name, meshContent.Vertices.ToArray(), meshContent.Indices.ToArray())));
                parentGameObject.Children.Add(meshGameObject);
            }

            return parentGameObject;
        }
    }
}

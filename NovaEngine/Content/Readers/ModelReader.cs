using NovaEngine.Content.Attributes;
using NovaEngine.Content.Models;
using NovaEngine.Core;
using NovaEngine.Components;
using NovaEngine.Serialisation;
using System;
using System.IO;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how a model should be read from a nova file.</summary>
    [ContentReader("3dmodel", typeof(GameObject), typeof(ModelContent), typeof(Mesh[]))]
    public class ModelReader : IContentReader
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public object? Read(Stream stream, Type outputType)
        {
            // retrieve model content from stream
            var modelContent = Serialiser.Deserialise<ModelContent>(stream);
            if (modelContent == null)
                return null;

            // return data if possible
            if (outputType == typeof(ModelContent))
                return modelContent;
            if (outputType == typeof(Mesh[]))
                return modelContent.Meshes.ToArray();

            // return data as game object
            var parentGameObject = new GameObject("Model");

            // convert each mesh to a game object
            foreach (var meshContent in modelContent.Meshes)
            {
                var meshGameObject = new GameObject(meshContent.Name);
                meshGameObject.Components.Add(new MeshRenderer(new(meshContent.Name, meshContent.Vertices.ToArray(), meshContent.Indices.ToArray())));
                parentGameObject.Children.Add(meshGameObject);
            }

            return parentGameObject;
        }
    }
}

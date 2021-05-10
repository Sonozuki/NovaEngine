using NovaEngine.Content.Models;
using NovaEngine.Content.Readers.Attributes;
using NovaEngine.Core;
using NovaEngine.Core.Components;
using NovaEngine.Extensions;
using System;
using System.IO;
using System.Linq;

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how a model should be read from a nova file.</summary>
    [ContentReader("3dmodel", typeof(GameObject), typeof(ModelContent), typeof(Mesh[]), typeof(MeshRenderer))]
    public class ModelReader : IContentReader
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public object Read(Stream stream, Type outputType, string? additionalInformation = null)
        {
            // retrieve model content from stream
            ModelContent modelContent;
            using (var reader = new BinaryReader(stream))
                modelContent = reader.ReadModelContent();

            // return data if possible
            if (outputType == typeof(ModelContent))
                return modelContent;
            if (outputType == typeof(Mesh[]))
                return modelContent.Meshes.ToArray();
            if (outputType == typeof(MeshRenderer))
            {
                if (string.IsNullOrEmpty(additionalInformation))
                    throw new InvalidOperationException("Cannot load a model as a mesh without additional information.");

                var guid = new Guid(additionalInformation);
                var mesh = modelContent.Meshes.FirstOrDefault(mesh => mesh.Guid == guid);
                if (mesh == null)
                    throw new InvalidDataException($"Model doesn't contain a mesh with the guid: {guid}");

                return new MeshRenderer(new(mesh.Name, mesh.Vertices.ToArray(), mesh.Indices.ToArray(), mesh.Guid));
            }

            // return data as game object
            var parentGameObject = new GameObject("Model");

            // convert each mesh to a game object
            foreach (var meshContent in modelContent.Meshes)
            {
                var meshGameObject = new GameObject(meshContent.Name);
                meshGameObject.AddComponent(new MeshRenderer(new(meshContent.Name, meshContent.Vertices.ToArray(), meshContent.Indices.ToArray(), meshContent.Guid)));
                parentGameObject.Children.Add(meshGameObject);
            }

            return parentGameObject;
        }
    }
}

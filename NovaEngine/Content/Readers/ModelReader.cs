using NovaEngine.Content.Models.Model;

namespace NovaEngine.Content.Readers;

/// <summary>Defines how a model should be read from a nova file.</summary>
[ContentReader("3dmodel", typeof(GameObject), typeof(ModelContent), typeof(Mesh[]))]
public class ModelReader : IContentReader
{
    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public object? Read(Stream stream, Type outputType)
    {
        var modelContent = Serialiser.Deserialise<ModelContent>(stream);
        if (modelContent == null)
            return null;

        if (outputType == typeof(ModelContent))
            return modelContent;
        if (outputType == typeof(Mesh[]))
            return modelContent.Meshes.ToArray();

        var parentGameObject = new GameObject("Model");

        foreach (var meshContent in modelContent.Meshes)
        {
            var meshGameObject = new GameObject(meshContent.Name);
            meshGameObject.Components.Add(new MeshRenderer(new(meshContent.Name, meshContent.Vertices.ToArray(), meshContent.Indices.ToArray())));
            parentGameObject.Children.Add(meshGameObject);
        }

        return parentGameObject;
    }
}

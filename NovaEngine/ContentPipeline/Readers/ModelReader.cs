using NovaEngine.ContentPipeline.Models.Model;

namespace NovaEngine.ContentPipeline.Readers;

/// <summary>Defines how a model should be read from a nova file.</summary>
public class ModelReader : IContentReader
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public string Type => "3dmodel";

    /// <inheritdoc/>
    public Type[] OutputTypes => new[] { typeof(GameObject), typeof(ModelContent), typeof(Mesh[]) };


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public object? Read(FileStream novaFileStream, Type outputType)
    {
        var modelContent = Serialiser.Deserialise<ModelContent>(novaFileStream);
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

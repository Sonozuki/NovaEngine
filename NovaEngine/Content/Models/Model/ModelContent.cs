namespace NovaEngine.Content.Models.Model;

/// <summary>Contains the meshes that make up a (3D) model.</summary>
public class ModelContent
{
    /*********
    ** Accessors
    *********/
    /// <summary>The meshes of the model.</summary>
    public List<MeshContent> Meshes { get; } = new();
}

namespace NovaEngine.Content.Models.Model;

/// <summary>Contains the meshes that make up a (3D) model.</summary>
public class ModelContent
{
    /*********
    ** Properties
    *********/
#pragma warning disable CA1002 // TODO: temp: just here to compile until content pipeline is cleaned up
    
    /// <summary>The meshes of the model.</summary>
    public List<MeshContent> Meshes { get; } = new();

#pragma warning restore CA1002 // TODO: temp: just here to compile until content pipeline is cleaned up
}

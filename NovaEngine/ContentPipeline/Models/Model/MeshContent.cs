namespace NovaEngine.ContentPipeline.Models.Model;

/// <summary>Contains the positions, texture coordinates, and normals vertex data as well as the faces that make up a mesh.</summary>
public class MeshContent
{
    /*********
    ** Properties
    *********/
#pragma warning disable CA1002 // TODO: temp: just here to compile until content pipeline is cleaned up
    
    /// <summary>The name of the mesh.</summary>
    public string Name { get; set; }
    
    /// <summary>The vertices of the mesh.</summary>
    public List<Vertex> Vertices { get; } = new();

    /// <summary>The indices of the mesh.</summary>
    public List<uint> Indices { get; } = new();

#pragma warning restore CA1002 // TODO: temp: just here to compile until content pipeline is cleaned up

    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public MeshContent()
        : this("") { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the mesh.</param>
    public MeshContent(string name)
    {
        Name = name;
    }
}

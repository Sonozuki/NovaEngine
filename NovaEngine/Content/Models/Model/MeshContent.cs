namespace NovaEngine.Content.Models.Model;

/// <summary>Contains the positions, texture coordinates, and normals vertex data as well as the faces that make up a mesh.</summary>
public class MeshContent
{
    /*********
    ** Accessors
    *********/
    /// <summary>The name of the mesh.</summary>
    public string Name { get; set; }
    
    /// <summary>The vertices of the mesh.</summary>
    public List<Vertex> Vertices { get; } = new();

    /// <summary>The indices of the mesh.</summary>
    public List<uint> Indices { get; } = new();


    /*********
    ** Public Methods
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

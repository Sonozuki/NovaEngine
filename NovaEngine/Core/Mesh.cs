namespace NovaEngine.Core;

/// <summary>Represents a mesh.</summary>
public class Mesh
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the mesh.</summary>
    public string Name { get; }

    /// <summary>The data for the vertex buffer.</summary>
    public Vertex[] VertexData { get; private set; }

    /// <summary>The data for the index buffer.</summary>
    public uint[] IndexData { get; private set; }

    /// <summary>The type of the mesh.</summary>
    public MeshType Type { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the mesh.</param>
    /// <param name="vertexData">The data for the vertex buffer.</param>
    /// <param name="indexData">The data for the index buffer.</param>
    /// <param name="type">The type of the mesh.</param>
    public Mesh(string name, Vertex[] vertexData, uint[] indexData, MeshType type = MeshType.TriangleList)
    {
        Name = name;
        VertexData = vertexData;
        IndexData = indexData;
        Type = type;
    }
}

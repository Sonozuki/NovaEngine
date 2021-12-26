namespace NovaEngine.Graphics;

/// <summary>Represents a mesh vertex.</summary>
public struct Vertex
{
    /*********
    ** Fields
    *********/
    /// <summary>The position of the vertex.</summary>
    public Vector3 Position;

    /// <summary>The texture coordinates of the vertex.</summary>
    public Vector2 TextureCoordinates;

    /// <summary>The normal of the vertex.</summary>
    public Vector3 Normal;


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="position">The position of the vertex.</param>
    /// <param name="textureCoordinates">The texture coordinates of the vertex.</param>
    /// <param name="normal">The normal of the vertex.</param>
    public Vertex(Vector3 position = default, Vector2 textureCoordinates = default, Vector3 normal = default)
    {
        Position = position;
        TextureCoordinates = textureCoordinates;
        Normal = normal;
    }
}

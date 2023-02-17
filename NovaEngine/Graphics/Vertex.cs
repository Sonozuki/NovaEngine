namespace NovaEngine.Graphics;

/// <summary>Represents a mesh vertex.</summary>
public struct Vertex : IEquatable<Vertex>
{
    /*********
    ** Fields
    *********/
    /// <summary>The position of the vertex.</summary>
    public Vector3<float> Position;

    /// <summary>The texture coordinates of the vertex.</summary>
    public Vector2<float> TextureCoordinates;

    /// <summary>The normal of the vertex.</summary>
    public Vector3<float> Normal;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="position">The position of the vertex.</param>
    /// <param name="textureCoordinates">The texture coordinates of the vertex.</param>
    /// <param name="normal">The normal of the vertex.</param>
    public Vertex(Vector3<float> position = default, Vector2<float> textureCoordinates = default, Vector3<float> normal = default)
    {
        Position = position;
        TextureCoordinates = textureCoordinates;
        Normal = normal;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Checks two vertices for equality.</summary>
    /// <param name="other">The vertex to check equality with.</param>
    /// <returns><see langword="true"/> if the vertices are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Vertex other) => this == other;

    /// <summary>Checks the vertex and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the vertex and object are equal; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is Vertex state && this == state;

    /// <summary>Retrieves the hash code of the vertex.</summary>
    /// <returns>The hash code of the vertex.</returns>
    public override int GetHashCode() => (Position, TextureCoordinates, Normal).GetHashCode();


    /*********
    ** Operators
    *********/
    /// <summary>Checks two vertices for equality.</summary>
    /// <param name="left">The first vertex.</param>
    /// <param name="right">The second vertex.</param>
    /// <returns><see langword="true"/> if the vertices are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vertex left, Vertex right) =>
        left.Position == right.Position &&
        left.TextureCoordinates == right.TextureCoordinates &&
        left.Normal == right.Normal;

    /// <summary>Checks two vertices for inequality.</summary>
    /// <param name="left">The first vertex.</param>
    /// <param name="right">The second vertex.</param>
    /// <returns><see langword="true"/> if the vertices are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vertex left, Vertex right) => !(left == right);
}

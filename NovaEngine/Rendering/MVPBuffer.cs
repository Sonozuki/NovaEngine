namespace NovaEngine.Rendering;

/// <summary>The model view projection uniform buffer object.</summary>
public struct MVPBuffer : IEquatable<MVPBuffer>
{
    /*********
    ** Fields
    *********/
    /// <summary>The model matrix.</summary>
    public Matrix4x4<float> Model;

    /// <summary>The view matrix.</summary>
    public Matrix4x4<float> View;

    /// <summary>The projection matrix.</summary>
    public Matrix4x4<float> Projection;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="model">The model matrix.</param>
    /// <param name="view">The view matrix.</param>
    /// <param name="projection">The projection matrix.</param>
    public MVPBuffer(Matrix4x4<float> model, Matrix4x4<float> view, Matrix4x4<float> projection)
    {
        Model = model;
        View = view;
        Projection = projection;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Checks two buffers for equality.</summary>
    /// <param name="other">The buffer to check equality with.</param>
    /// <returns><see langword="true"/> if the buffers are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(MVPBuffer other) => this == other;

    /// <summary>Checks the buffer and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the buffer and object are equal; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is MVPBuffer buffer && this == buffer;

    /// <summary>Retrieves the hash code of the buffer.</summary>
    /// <returns>The hash code of the buffer.</returns>
    public override int GetHashCode() => (Model, View, Projection).GetHashCode();


    /*********
    ** Operators
    *********/
    /// <summary>Checks two buffers for equality.</summary>
    /// <param name="left">The first buffer.</param>
    /// <param name="right">The second buffer.</param>
    /// <returns><see langword="true"/> if the buffers are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(MVPBuffer left, MVPBuffer right) =>
        left.Model == right.Model &&
        left.View == right.View &&
        left.Projection == right.Projection;

    /// <summary>Checks two buffers for inequality.</summary>
    /// <param name="left">The first buffer.</param>
    /// <param name="right">The second buffer.</param>
    /// <returns><see langword="true"/> if the buffers are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(MVPBuffer left, MVPBuffer right) => !(left == right);
}

namespace NovaEngine.Rendering;

/// <summary>The model view projection uniform buffer object.</summary>
public struct MVPBuffer
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
}

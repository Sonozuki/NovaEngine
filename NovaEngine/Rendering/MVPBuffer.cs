namespace NovaEngine.Rendering;

/// <summary>The model view projection uniform buffer object.</summary>
public struct MVPBuffer
{
    /*********
    ** Fields
    *********/
    /// <summary>The model matrix.</summary>
    public Matrix4x4 Model;

    /// <summary>The view matrix.</summary>
    public Matrix4x4 View;

    /// <summary>The projection matrix.</summary>
    public Matrix4x4 Projection;


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="model">The model matrix.</param>
    /// <param name="view">The view matrix.</param>
    /// <param name="projection">The projection matrix.</param>
    public MVPBuffer(Matrix4x4 model, Matrix4x4 view, Matrix4x4 projection)
    {
        Model = model;
        View = view;
        Projection = projection;
    }
}

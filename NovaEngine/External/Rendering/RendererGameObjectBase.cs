namespace NovaEngine.External.Rendering;

/// <summary>Represents a renderer game object.</summary>
public abstract class RendererGameObjectBase : IDisposable
{
    /*********
    ** Properties
    *********/
    /// <summary>The underlying game object.</summary>
    public GameObject BaseGameObject { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="baseGameObject">The underlying game object.</param>
    public RendererGameObjectBase(GameObject baseGameObject)
    {
        BaseGameObject = baseGameObject;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Invoked whenever the mesh of a game object gets updated.</summary>
    /// <param name="mesh">The updated mesh.</param>
    public abstract void UpdateMesh(Mesh mesh);

    /// <summary>Invoked every tick to just before the object is rendered.</summary>
    /// <param name="camera">The camera to update the game object UBO with.</param>
    /// <remarks>Used for updating UBOs etc.</remarks>
    public abstract void PrepareForCamera(Camera camera);

    /// <inheritdoc/>
    public abstract void Dispose();
}

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
    /// <summary>Destructs the instance.</summary>
    ~RendererGameObjectBase() => Dispose(false);

    /// <summary>Constructs an instance.</summary>
    /// <param name="baseGameObject">The underlying game object.</param>
    protected RendererGameObjectBase(GameObject baseGameObject)
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

    /// <summary>Cleans up unmanaged resources in the game object.</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the game object.</summary>
    /// <param name="disposing">Whether the game object is being disposed deterministically.</param>
    protected abstract void Dispose(bool disposing);
}

namespace NovaEngine.Rendering.Fake;

/// <summary>Represents a game object that is only used when nova is being used without a program instance.</summary>
internal sealed class FakeGameObject : RendererGameObjectBase
{
    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public FakeGameObject(GameObject baseGameObject)
        : base(baseGameObject) { }

    /// <inheritdoc/>
    public override void UpdateMesh(Mesh mesh) { }

    /// <inheritdoc/>
    public override void PrepareForCamera(Camera camera) { }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public FakeGameObject() : base(new("Fake")) { }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the game object.</summary>
    /// <param name="disposing">Whether the game object is being disposed deterministically.</param>
    protected override void Dispose(bool disposing) { }
}

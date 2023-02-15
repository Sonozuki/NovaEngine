namespace NovaEngine.Rendering.Fake;

/// <summary>Represents a game object that is only used when nova is being used without a program instance.</summary>
internal class FakeGameObject : RendererGameObjectBase
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

    /// <inheritdoc/>
    public override void Dispose() { }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public FakeGameObject() : base(new("Fake")) { }
}

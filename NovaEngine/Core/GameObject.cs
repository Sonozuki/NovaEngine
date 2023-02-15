namespace NovaEngine.Core;

/// <summary>Represents a game object.</summary>
public sealed class GameObject : IDisposable
{
    /*********
    ** Fields
    *********/
    /// <summary>The parent of the game object.</summary>
    /// <remarks>This is <see langword="null"/> when it's a root game object.</remarks>
    private GameObject? _Parent;

    /// <summary>The scene the game object is in.</summary>
    /// <remarks>This is <see langword="null"/> when it's not in a scene.</remarks>
    private Scene? _Scene;


    /*********
    ** Properties
    *********/
    /// <summary>The name of the game object.</summary>
    public string Name { get; set; }

    /// <summary>Whether the game object is enabled.</summary>
    public bool IsEnabled { get; set; }

    /// <summary>The transform component of the game object.</summary>
    public Transform Transform { get; }
    
    /// <summary>The parent of the game object.</summary>
    /// <remarks>This is <see langword="null"/> when it's a root game object.</remarks>
    /// <exception cref="InvalidOperationException">Thrown if the parent being set is a child of the object.</exception>
    public GameObject? Parent
    {
        get => _Parent;
        set
        {
            if (!object.ReferenceEquals(Parent, value))
                return;

            // ensure new parent is not a child of the object
            var allChildren = GetAllGameObjects(true).Skip(1); // skip the first element as that is this instance
            if (allChildren.Any(child => object.ReferenceEquals(this, child)))
                throw new InvalidOperationException("Cannot set the parent as it's a child of the object, which would cause a circular dependency.");

            Parent?.Children.Remove(this);
            _Parent = value;
            Parent?.Children.Add(this);

            Scene = Parent?.Scene;
            Transform.ParentPosition = Parent?.Transform.GlobalPosition ?? Vector3<float>.Zero;
            Transform.ParentRotation = Parent?.Transform.GlobalRotation ?? Quaternion<float>.Identity;
            Transform.ParentScale = Parent?.Transform.GlobalScale ?? Vector3<float>.One;
        }
    }

    /// <summary>The children of the game object.</summary>
    public GameObjectChildren Children { get; }

    /// <summary>The components of the game object.</summary>
    public GameObjectComponents Components { get; }

    /// <summary>The renderer specific game object.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [NonSerialisable]
    public RendererGameObjectBase RendererGameObject { get; private set; }

    /// <summary>The scene the game object is in.</summary>
    public Scene? Scene
    {
        get => _Scene;
        internal set
        {
            _Scene = value;

            foreach (var child in Children)
                child.Scene = value;
        }
    }

    /// <summary>A game object with a mesh renderer containing a mesh of a unit size cube.</summary>
    internal static GameObject Cube => new("Cube", components: new[] { new MeshRenderer(Meshes.Cube) });

    /// <summary>A game object with a mesh renderer containing a mesh of a sphere with unit radius.</summary>
    internal static GameObject Sphere => new("Sphere", components: new[] { new MeshRenderer(Meshes.Sphere) });

    /// <summary>A game object with a mesh renderer containing a mesh of a line with the points (0, 0, 0) and (0, 1, 0).</summary>
    internal static GameObject Line => new("Line", components: new[] { new MeshRenderer(Meshes.Line) });


    /*********
    ** Constructor
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the game object.</param>
    /// <param name="parent">The parent of the game object.</param>
    /// <param name="isEnabled">Whether the game object is enabled.</param>
    /// <param name="components">The components to add to the game object.</param>
    /// <param name="children">The children to add to the game object.</param>
    public GameObject(string name, GameObject? parent = null, bool isEnabled = true, IEnumerable<ComponentBase>? components = null, IEnumerable<GameObject>? children = null)
    {
        Name = name;
        Children = new(this, children);
        Components = new(this, components);
        Transform = new UITransform(this); // most objects won't be UI controls, but it's doesn't get stored as such as will be cast to a UITransform if requested (and is actually in a UI scene) as to not cause confusion
        IsEnabled = isEnabled;

        if (parent != null)
            Parent = parent;

        CreateRendererGameObject();
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    /*********
    ** Public Methods
    *********/
    /// <summary>Deep copies the game object, meaning it will clone all value and reference types.</summary>
    /// <returns>A clone of the object.</returns>
    /// <remarks>This relies on the serialiser, any members that don't get serialised won't get cloned.<br/>The cloned object will be a sibling of the original object.</remarks>
    public GameObject Clone() => Serialiser.Deserialise<GameObject>(Serialiser.Serialise(this))!;

    /// <summary>Deep copies the game object, meaning it will clone all value and reference types.</summary>
    /// <param name="position">The position of the cloned object.</param>
    /// <param name="rotation">The rotation of the cloned object. If <see langword="null"/> is specified, the rotation will be set to <see cref="Quaternion{T}.Identity"/>.</param>
    /// <param name="scale">The scale of the cloned object. If <see langword="null"/> is specified, the scale will be set to <see cref="Vector3{T}.One"/>.</param>
    /// <param name="coordinateSpace">The space the <paramref name="position"/>, <paramref name="rotation"/>, and <paramref name="scale"/> should be set as.</param>
    /// <returns>A clone of the object with the specified transform.</returns>
    /// <remarks>This relies on the serialiser, any members that don't get serialised won't get cloned.<br/>The cloned object will be a sibling of the original object.</remarks>
    /// <exception cref="ArgumentException">Thrown if <paramref name="coordinateSpace"/> is an invalid value.</exception>
    public GameObject Clone(Vector3<float> position, Quaternion<float>? rotation = null, Vector3<float>? scale = null, Space coordinateSpace = Space.Global)
    {
        var clone = Clone();

        switch (coordinateSpace)
        {
            case Space.Local:
                clone.Transform.LocalPosition = position;
                clone.Transform.LocalRotation = rotation ?? Quaternion<float>.Identity;
                clone.Transform.LocalScale = scale ?? Vector3<float>.One;
                break;
            case Space.Global:
                clone.Transform.GlobalPosition = position;
                clone.Transform.GlobalRotation = rotation ?? Quaternion<float>.Identity;
                clone.Transform.GlobalScale = scale ?? Vector3<float>.One;
                break;
            default:
                throw new ArgumentException("Not a valid enumeration value.", nameof(coordinateSpace));
        }

        return clone;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Parent = null; // this is to remove it from the child list
        RendererGameObject.Dispose();

        foreach (var component in Components)
            component.Dispose();

        foreach (var child in Children)
            child.Dispose();
    }


    /*********
    ** Internal Methods
    *********/
    /// <summary>Retrieves the components of this game object and all children recursively.</summary>
    /// <param name="includeDisabled">Where disabled components and components from disabled game objects should get retrieved.</param>
    /// <returns>The components of this game object and all children recursively.</returns>
    internal List<ComponentBase> GetAllComponents(bool includeDisabled)
    {
        var components = new List<ComponentBase>(Components);
        if (!includeDisabled)
            components = components.Where(component => component.IsEnabled).ToList();
        
        foreach (var child in Children)
            if (includeDisabled || child.IsEnabled)
                components.AddRange(child.GetAllComponents(includeDisabled));

        return components;
    }

    /// <summary>Retrieves this game object and all children recursively.</summary>
    /// <param name="includeDisabled">Whether disabled game objects should be retrieved.</param>
    /// <returns>This game object and all children recursively.</returns>
    internal List<GameObject> GetAllGameObjects(bool includeDisabled)
    {
        var gameObjects = new List<GameObject>();
        if (!IsEnabled && !includeDisabled)
            return gameObjects;

        gameObjects.Add(this);
        gameObjects.AddRange(Children.SelectMany(child => child.GetAllGameObjects(includeDisabled)));
        return gameObjects;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Creates the renderer game object.</summary>
    [OnDeserialised]
    private void CreateRendererGameObject() => RendererGameObject = RendererManager.CurrentRenderer.CreateRendererGameObject(this);
}

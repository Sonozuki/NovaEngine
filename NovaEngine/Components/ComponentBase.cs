namespace NovaEngine.Components;

/// <summary>Represents the base of a <see cref="Core.GameObject"/> component.</summary>
/// <remarks>A component refers to some scriptable behaviour.</remarks>
public abstract class ComponentBase : IDisposable
{
    /*********
    ** Properties
    *********/
    /// <summary>Whether the component is enabled.</summary>
    public bool IsEnabled { get; set; }

    /// <summary>Whether this component is affected by <see cref="ApplicationLoop.IsComponentUpdatingPaused"/>.</summary>
    public bool IsPausable { get; set; } = true;

    /// <summary>The game object the component is currently on.</summary>
    public GameObject GameObject { get; internal set; }

    /// <summary>The transform of the game object the component is currently attached to.</summary>
    public Transform Transform => GameObject.Transform;

    /// <summary>The children of the game object that component is currently attached to.</summary>
    public GameObjectChildren Children => GameObject.Children;

    /// <summary>The components of the game object the component is currently attached to.</summary>
    public GameObjectComponents Components => GameObject.Components;


    /*********
    ** Constructors
    *********/
    /// <summary>Destructs the instance.</summary>
    ~ComponentBase() => Dispose(false);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="isEnabled">Whether the component is enabled.</param>
    protected ComponentBase(bool isEnabled = true)
    {
        IsEnabled = isEnabled;
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    /*********
    ** Public Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the component.</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    /*********
    ** Protected Internal Methods
    *********/
    /// <summary>Invoked when the game starts (before the first tick).</summary>
    protected internal virtual void OnStart() { }

    /// <summary>Invoked when the component should get updated (once per tick).</summary>
    protected internal virtual void OnUpdate() { }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the component.</summary>
    /// <param name="disposing">Whether the component is being disposed deterministically.</param>
    protected virtual void Dispose(bool disposing) { }
}

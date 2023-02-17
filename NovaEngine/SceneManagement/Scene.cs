namespace NovaEngine.SceneManagement;

/// <summary>Represents a scene.</summary>
public class Scene : IDisposable
{
    /*********
    ** Fields
    *********/
    /// <summary>Whether the scene has been disposed.</summary>
    private bool IsDisposed;


    /*********
    ** Properties
    *********/
    /// <summary>The name of the scene.</summary>
    public string Name { get; set; }

    /// <summary>Whether the scene is active.</summary>
    public bool IsActive { get; set; }

    /// <summary>The root game objects of the scene.</summary>
    public SceneRootObjects RootGameObjects { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Destructs the instance.</summary>
    ~Scene() => Dispose(false);

    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the scene.</param>
    /// <param name="isActive">Whether the scene is active.</param>
    public Scene(string name, bool isActive)
    {
        Name = name;
        IsActive = isActive;

        RootGameObjects = new(this);
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the scene.</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    /*********
    ** Internal Methods
    *********/
    /// <summary>Starts the scene.</summary>
    internal virtual void Start()
    {
        var components = RootGameObjects.SelectMany(gameObject => gameObject.GetAllComponents(true));
        Parallel.ForEach(components, component =>
        {
            try
            {
                component.OnStart();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Component crashed on start. Technical details:\n{ex}");
            }
        });
    }

    /// <summary>Updates the scene.</summary>
    internal virtual void Update()
    {
        var components = RootGameObjects.SelectMany(gameObject => gameObject.GetAllComponents(false));
        Parallel.ForEach(components, component =>
        {
            try
            {
                if (ApplicationLoop.IsComponentUpdatingPaused && component.IsPausable)
                    return;

                component.OnUpdate();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Component crashed on update. Technical details:\n{ex}");
            }
        });
    }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the scene.</summary>
    /// <param name="disposing">Whether the scene is being disposed deterministically.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        if (disposing)
            foreach (var gameObject in RootGameObjects)
                gameObject.Dispose();

        IsDisposed = true;
    }
}

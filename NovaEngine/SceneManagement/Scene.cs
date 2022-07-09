namespace NovaEngine.SceneManagement;

/// <summary>Represents a scene.</summary>
public class Scene : IDisposable
{
    /*********
    ** Accessors
    *********/
    /// <summary>The name of the scene.</summary>
    public string Name { get; set; }

    /// <summary>Whether the scene is active.</summary>
    public bool IsActive { get; set; }

    /// <summary>The root game objects of the scene.</summary>
    public SceneRootObjects RootGameObjects { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the scene.</param>
    /// <param name="isActive">Whether the scene is active.</param>
    public Scene(string name, bool isActive)
    {
        Name = name;
        IsActive = isActive;

        RootGameObjects = new(this);
    }

    /// <inheritdoc/>
    public virtual void Dispose()
    {
        foreach (var gameObject in RootGameObjects)
            gameObject.Dispose();
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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    /// <summary>Constructs an instance.</summary>
    protected Scene() { } // required for serialiser

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

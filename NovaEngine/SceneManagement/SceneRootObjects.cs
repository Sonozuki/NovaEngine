namespace NovaEngine.SceneManagement;

/// <summary>Represents a collection of root <see cref="GameObject"/>s for use in <see cref="Scene.RootGameObjects"/>.</summary>
/// <remarks>This automatically sets/unsets the <see cref="GameObject.Scene"/> where necessary.</remarks>
public class SceneRootObjects : IList<GameObject>
{
    /*********
    ** Fields
    *********/
    /// <summary>The scene whose root objects this collection contains.</summary>
    /// <remarks>This is used for automatically setting/unsetting the scene of root objects (which will then set/unset all recursive children).</remarks>
    private readonly Scene Scene;

    /// <summary>The underlying collection of root game objects.</summary>
    private readonly List<GameObject> RootGameObjects = new();


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public int Count => RootGameObjects.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <see langword="value"/> is <see langword="null"/>.</exception>
    public GameObject this[int index]
    {
        get => RootGameObjects[index];
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            var oldRootObject = RootGameObjects[index];
            if (oldRootObject != null)
                oldRootObject.Scene = null;

            value.Scene = Scene;
            RootGameObjects[index] = value;
        }
    }

    
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="scene">The scene whose root objects this collection contains.</param>
    internal SceneRootObjects(Scene scene)
    {
        Scene = scene;
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
    public void Add(GameObject item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (RootGameObjects.Contains(item))
            throw new InvalidOperationException("The item is already a root object.");

        item.Scene = Scene;
        RootGameObjects.Add(item);
    }

    /// <inheritdoc/>
    public void Clear()
    {
        foreach (var gameObject in RootGameObjects)
            gameObject.Scene = null;

        RootGameObjects.Clear();
    }

    /// <inheritdoc/>
    public bool Contains(GameObject item) => RootGameObjects.Contains(item);

    /// <inheritdoc/>
    public void CopyTo(GameObject[] array, int arrayIndex) => RootGameObjects.CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    public IEnumerator<GameObject> GetEnumerator() => RootGameObjects.GetEnumerator();

    /// <inheritdoc/>
    public int IndexOf(GameObject item) => RootGameObjects.IndexOf(item);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
    public void Insert(int index, GameObject item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var oldRootObject = RootGameObjects[index];
        if (oldRootObject != null)
            oldRootObject.Scene = null;

        item.Scene = Scene;
        RootGameObjects[index] = item;
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
    public bool Remove(GameObject item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var removed = RootGameObjects.Remove(item);
        if (removed)
            item.Scene = null;

        return removed;
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        RootGameObjects[index].Scene = null;
        RootGameObjects.RemoveAt(index);
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => RootGameObjects.GetEnumerator();
}

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
    [Serialisable]
    private readonly Scene Scene;

    /// <summary>The underlying collection of root game objects.</summary>
    private readonly List<GameObject> RootGameObjects = new();


    /*********
    ** Accessors
    *********/
    /// <inheritdoc/>
    public int Count => RootGameObjects.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    public GameObject this[int index]
    {
        get => RootGameObjects[index];
        set
        {
            // unset the scene of the root object being replaced
            var oldRootObject = RootGameObjects[index];
            if (oldRootObject != null)
                oldRootObject.Scene = null;

            // set the scene of the new root object
            value.Scene = Scene;
            RootGameObjects[index] = value;
        }
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public void Add(GameObject item)
    {
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
    public void Insert(int index, GameObject item)
    {
        // unset the scene of the root object being replaced
        var oldRootObject = RootGameObjects[index];
        if (oldRootObject != null)
            oldRootObject.Scene = null;

        // set the scene of the new root object
        item.Scene = Scene;
        RootGameObjects[index] = item;
    }

    /// <inheritdoc/>
    public bool Remove(GameObject item)
    {
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


    /*********
    ** Internal Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="scene">The scene whose root objects this collection contains.</param>
    internal SceneRootObjects(Scene scene)
    {
        Scene = scene;
    }


    /*********
    ** Protected Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    protected SceneRootObjects() { } // required for serialiser

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

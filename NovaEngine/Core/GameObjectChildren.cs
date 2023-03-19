namespace NovaEngine.Core;

/// <summary>Represents a collection of child <see cref="GameObject"/>s for use in <see cref="GameObject.Children"/>.</summary>
/// <remarks>This automatically sets/unsets the parent of children where necessary.</remarks>
public class GameObjectChildren : IList<GameObject>
{
    /*********
    ** Fields
    *********/
    /// <summary>The game object whose children this collection contains.</summary>
    /// <remarks>This is used for automatically setting/unsetting the parent of children.</remarks>
    private readonly GameObject Parent;

    /// <summary>The underlying collection of children.</summary>
    private readonly List<GameObject> Children = new();


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public int Count => Children.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref langword="value"/> is <see langword="null"/>.</exception>
    public GameObject this[int index]
    {
        get => Children[index];
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            var oldChild = Children[index];
            if (oldChild != null)
                oldChild.Parent = null;

            value.Parent = Parent;
            Children[index] = value;
        }
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="parent">The game object whose children this collection is.</param>
    /// <param name="children">The initial children to add.</param>
    internal GameObjectChildren(GameObject parent, IEnumerable<GameObject>? children)
    {
        Parent = parent;

        if (children != null)
            foreach (var child in children)
                Add(child);
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the item is already a child.</exception>
    public void Add(GameObject item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (Children.Contains(item))
            throw new InvalidOperationException("The item is already a child.");
        
        if (!object.ReferenceEquals(item.Parent, Parent))
            item.Parent = Parent;

        Children.Add(item);
    }

    /// <inheritdoc/>
    public void Clear()
    {
        while (Children.Any())
            Children[0].Parent = null;
    }

    /// <inheritdoc/>
    public bool Contains(GameObject item) => Children.Contains(item);
    
    /// <inheritdoc/>
    public void CopyTo(GameObject[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);
    
    /// <inheritdoc/>
    public IEnumerator<GameObject> GetEnumerator() => Children.GetEnumerator();
    
    /// <inheritdoc/>
    public int IndexOf(GameObject item) => Children.IndexOf(item);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
    public void Insert(int index, GameObject item)
    {
        ArgumentNullException.ThrowIfNull(item);

        item.Parent = Parent;
        Children.Insert(index, item);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
    public bool Remove(GameObject item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var wasRemoved = Children.Remove(item);
        if (wasRemoved)
            item.Parent = null;

        return wasRemoved;
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        var removedChild = Children[index];
        Children.RemoveAt(index);
        removedChild.Parent = null;
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => Children.GetEnumerator();
}

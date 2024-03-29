﻿namespace NovaEngine.Core;

/// <summary>Represents a collection of child <see cref="Core.GameObject"/>s for use in <see cref="GameObject.Children"/>.</summary>
/// <remarks>This automatically sets/unsets the parent of components where necessary.</remarks>
public class GameObjectComponents : IList<ComponentBase>
{
    /*********
    ** Fields
    *********/
    /// <summary>The game object whose components this collection contains.</summary>
    /// <remarks>This is used for automatically setting/unsetting the parent of component.</remarks>
    private readonly GameObject GameObject;

    /// <summary>The underlying collection of components.</summary>
    private readonly List<ComponentBase> Components = new();


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public int Count => Components.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref langword="value"/> is <see langword="null"/>.</exception>
    public ComponentBase this[int index]
    {
        get => Components[index];
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            value.GameObject = GameObject;
            Components[index] = value;
        }
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="parent">The game object whose components this collection is.</param>
    /// <param name="components">The initial components to add.</param>
    internal GameObjectComponents(GameObject parent, IEnumerable<ComponentBase>? components)
    {
        GameObject = parent;

        if (components != null)
            foreach (var component in components)
                Add(component);
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
    public void Add(ComponentBase item)
    {
        ArgumentNullException.ThrowIfNull(item);

        // TODO: ensure component isn't a transform
        // TODO: remove component from current gameobject, if it's attached to one, so it doesn't get updated twice

        item.GameObject = GameObject;
        Components.Add(item);

        if (item is MeshRenderer meshRenderer)
            meshRenderer.UpdateMesh();
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
    public bool Remove(ComponentBase item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var wasRemoved = Components.Remove(item);
        if (wasRemoved)
            item.GameObject = null!; // setting this is null is fine as no code will try to access it expecting a non null value

        return wasRemoved;
    }

    /// <summary>Removes a type of component from the collection.</summary>
    /// <typeparam name="T">The type of component to remove.</typeparam>
    /// <param name="removeAll">Whether all components of the type <typeparamref name="T"/> should be removed (as opposed to just the first component with that type).</param>
    public void Remove<T>(bool removeAll)
        where T : ComponentBase
    {
        for (var i = 0; i < Components.Count; i++)
        {
            var component = Components[i];
            if (component.GetType() != typeof(T))
                continue;

            component.GameObject = null!; // setting this is null is fine as the no code will try to access it expecting a non null value
            Components.RemoveAt(i--);

            if (!removeAll)
                break;
        }
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        Components[index].GameObject = null!; // setting this is null is fine as the no code will try to access it expecting a non null value
        Components.RemoveAt(index);
    }

    /// <summary>Gets the first component with a specified type.</summary>
    /// <typeparam name="T">The type of the component to get.</typeparam>
    /// <param name="includeDisabled">Whether disabled components should be included.</param>
    /// <returns>The first component with the specified type; otherwise, <see langword="null"/> if no component was found.</returns>
    public T? Get<T>(bool includeDisabled = true) where T : ComponentBase => GetRange<T>(includeDisabled).FirstOrDefault();

    /// <summary>Gets the components with a specified type.</summary>
    /// <typeparam name="T">The type of the components to get.</typeparam>
    /// <param name="includeDisabled">Whether disabled components should be included.</param>
    /// <returns>The components with the specified type.</returns>
    public IReadOnlyList<T> GetRange<T>(bool includeDisabled = true)
        where T : ComponentBase
    {
        if (typeof(T).IsAssignableTo(typeof(Transform)))
            return new List<T>() { (T)(object)GameObject.Transform }; // objects can only ever have one transform

        var components = Components
            .Where(component => component is T)
            .Where(component => includeDisabled || component.IsEnabled)
            .Cast<T>()
            .ToList();

        return components.AsReadOnly();
    }

    /// <summary>Gets the first component with a specified type from the children of the game object.</summary>
    /// <typeparam name="T">The type of the component to get.</typeparam>
    /// <param name="includeDisabled">Whether disabled components should be included.</param>
    /// <returns>The first component with the specified type in the children; otherwise, <see langword="null"/> if no component was found.</returns>
    public T? GetFromChildren<T>(bool includeDisabled = true) where T : ComponentBase => GetRangeFromChildren<T>(includeDisabled).FirstOrDefault();

    /// <summary>Gets the components with a specified type from the children of the game object.</summary>
    /// <typeparam name="T">The type of the components to get.</typeparam>
    /// <param name="includeDisabled">Whether disabled components should be included.</param>
    /// <returns>The components with the specified type from the children.</returns>
    public IReadOnlyList<T> GetRangeFromChildren<T>(bool includeDisabled = true)
        where T : ComponentBase
    {
        var components = new List<T>();
        foreach (var child in GameObject.Children)
            components.AddRange(child.Components.GetRange<T>(includeDisabled));

        return components.AsReadOnly();
    }

    /// <summary>Gets the first component with a specified type from the parent of the game object.</summary>
    /// <typeparam name="T">The type of the component to get.</typeparam>
    /// <param name="includeDisabled">Whether disabled components should be included.</param>
    /// <returns>The first component with the specified type in the parent; otherwise, <see langword="null"/> if no component was found.</returns>
    public T? GetFromParent<T>(bool includeDisabled = true) where T : ComponentBase => GetRangeFromParent<T>(includeDisabled).FirstOrDefault();

    /// <summary>Gets the components with a specified type from the parent of the game object.</summary>
    /// <typeparam name="T">The type of the components to get.</typeparam>
    /// <param name="includeDisabled">Whether disabled components should be included.</param>
    /// <returns>The components with the specified type from the parent.</returns>
    public IReadOnlyList<T> GetRangeFromParent<T>(bool includeDisabled = true) where T : ComponentBase => GameObject.Components.GetRange<T>(includeDisabled);

    /// <summary>Gets the components with a specified type from this collection and all recursive children of the game object (all nodes to leaf nodes).</summary>
    /// <typeparam name="T">The type of the components to get.</typeparam>
    /// <param name="includeDisabled">Whether disabled components should be included.</param>
    /// <returns>The components with the specified type from this game object and all recursive children (all nodes to leaf nodes).</returns>
    public IReadOnlyList<T> GetAll<T>(bool includeDisabled = true)
        where T : ComponentBase
    {
        var components = new List<T>();

        components.AddRange(GetRange<T>(includeDisabled));
        foreach (var child in GameObject.Children)
            components.AddRange(child.Components.GetAll<T>(includeDisabled));

        return components.AsReadOnly();
    }

    /// <inheritdoc/>
    public void Clear()
    {
        Components.ForEach(component => component.GameObject = null!); // setting this is null is fine as the no code will try to access it expecting a non null value
        Components.Clear();
    }

    /// <inheritdoc/>
    public bool Contains(ComponentBase item) => Components.Contains(item);

    /// <inheritdoc/>
    public void CopyTo(ComponentBase[] array, int arrayIndex) => Components.CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    public IEnumerator<ComponentBase> GetEnumerator() => Components.GetEnumerator();

    /// <inheritdoc/>
    public int IndexOf(ComponentBase item) => Components.IndexOf(item);

    /// <inheritdoc/>
    public void Insert(int index, ComponentBase item) => Components.Insert(index, item);

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => Components.GetEnumerator();
}

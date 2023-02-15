namespace NovaEngine.DataStructures;

/// <summary>Represents a pool of objects that can be reused, instead of deleting and instantiating repeatedly.</summary>
/// <typeparam name="T">The type of the objects to pool.</typeparam>
public class ObjectPool<T> : IDisposable
    where T : class
{
    /*********
    ** Fields
    *********/
    /// <summary>The method that is invoked when a new object needs to be created for the pool.</summary>
    private readonly Func<T> InstantiateObject;

    /// <summary>The method that is invoked when an object has been returned to the pool used to reset its state.</summary>
    private readonly Action<T>? ResetObject;

    /// <summary>The objects that are currently unused.</summary>
    private readonly Queue<T> AvailableObjects = new();


    /*********
    ** Properties
    *********/
    /// <summary>The number of available objects that are currently in the pool.</summary>
    /// <remarks>If this is zero, the next <see cref="GetObject"/> call will instantiate a new object assuming <see cref="ReturnObject(T)"/> hasn't since been called (the count will remain at zero as the new instance is instantly returned).</remarks>
    public int AvailableCount => AvailableObjects.Count;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="instantiateObject">The method that is invoked when a new object needs to be created for the pool.</param>
    /// <param name="resetObject">The method that is invoked when an object has been returned to the pool used to reset its state.</param>
    public ObjectPool(Func<T> instantiateObject, Action<T>? resetObject = null)
    {
        InstantiateObject = instantiateObject;
        ResetObject = resetObject;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves an object from the pool.</summary>
    /// <returns>An object from the pool.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an object had to be instantiated and the provided <see cref="InstantiateObject"/> method returned <see langword="null"/>.</exception>
    public T GetObject()
    {
        if (AvailableObjects.Count == 0)
            return InstantiateObject.Invoke() ?? throw new InvalidOperationException($"Failed to instantiate a new object for the pool. The provided {nameof(InstantiateObject)} method returned null.");
        else
            return AvailableObjects.Dequeue();
    }

    /// <summary>Returns an object into the pool.</summary>
    /// <param name="object">The object to add to return to the pool.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="object"/> is <see langword="null"/>.</exception>
    public void ReturnObject(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));

        ResetObject?.Invoke(@object);
        AvailableObjects.Enqueue(@object);
    }

    /// <summary>Disposes all the available objects in the pool.</summary>
    public void Dispose()
    {
        foreach (var element in AvailableObjects)
            if (element is IDisposable disposable)
                disposable.Dispose();
    }
}

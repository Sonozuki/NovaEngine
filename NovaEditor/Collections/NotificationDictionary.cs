namespace NovaEditor.Collections;

/// <summary>Represents a collection of keys and values with an event for receiving notifications of changes.</summary>
/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
public class NotificationDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    where TKey : notnull
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when the contents of the dictionary are changed.</summary>
    public event EventHandler<NotificationDictionaryChangedEventArgs> NotificationDictionaryChanged;


    /*********
    ** Fields
    *********/
    /// <summary>The underlying dictionary.</summary>
    private readonly Dictionary<TKey, TValue> Dictionary = new();


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public ICollection<TKey> Keys => Dictionary.Keys;

    /// <inheritdoc/>
    public ICollection<TValue> Values => Dictionary.Values;

    /// <inheritdoc/>
    public int Count => Dictionary.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;


    /*********
    ** Indexers
    *********/
    /// <inheritdoc/>
    public TValue this[TKey key]
    {
        get => Dictionary[key];
        set
        {
            var changeType = NotificationDictionaryChangeTypes.Add;
            if (Dictionary.TryGetValue(key, out var oldValue))
                changeType = NotificationDictionaryChangeTypes.Change;

            Dictionary[key] = value;
            NotificationDictionaryChanged?.Invoke(this, new(changeType, key, value, oldValue));
        }
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public NotificationDictionary() { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="dictionary">The initial values that should be stored in the dictionary.</param>
    /// <remarks>No events will be raised when adding these values.</remarks>
    internal NotificationDictionary(Dictionary<TKey, TValue> dictionary)
    {
        Dictionary = dictionary ?? new();
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves this dictionary as a regular dictionary.</summary>
    /// <returns>This dictionary as a regular dictionary.</returns>
    /// <remarks>This won't return the underlying reference, changes to the returned dictionary won't affect this dictionary.</remarks>
    public Dictionary<TKey, TValue> AsDictionary() => new(Dictionary);

    /// <inheritdoc/>
    public void Add(TKey key, TValue value)
    {
        Dictionary.Add(key, value);
        NotificationDictionaryChanged?.Invoke(this, new(NotificationDictionaryChangeTypes.Add, key, null, value));
    }

    /// <inheritdoc/>
    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

    /// <inheritdoc/>
    public void Clear()
    {
        foreach (var key in Dictionary.Keys)
            Remove(key);
    }

    /// <inheritdoc/>
    public bool Contains(KeyValuePair<TKey, TValue> item) => Dictionary.Contains(item);

    /// <inheritdoc/>
    public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);

    /// <inheritdoc/>
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    public bool Remove(TKey key)
    {
        var result = Dictionary.Remove(key, out var oldValue);
        NotificationDictionaryChanged?.Invoke(this, new(NotificationDictionaryChangeTypes.Remove, key, oldValue, null));

        return result;
    }

    /// <inheritdoc/>
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        var result = ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Remove(item);
        NotificationDictionaryChanged?.Invoke(this, new(NotificationDictionaryChangeTypes.Remove, item.Key, item.Value, null));

        return result;
    }

    /// <inheritdoc/>
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => Dictionary.TryGetValue(key, out value);

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Dictionary.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => Dictionary.GetEnumerator();
}

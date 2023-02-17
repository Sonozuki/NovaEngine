namespace NovaEngine.Serialisation;

/// <summary>Specifies that a value should be loaded from a file when constructed manually or through the serialiser.</summary>
/// <typeparam name="T">The type of the underlying value.</typeparam>
public sealed class SerialiserLoadable<T>
{
    /*********
    ** Fields
    *********/
    /// <summary>The underlying value.</summary>
    [NonSerialisable]
    public T Value;


    /*********
    ** Properties
    *********/
    /// <summary>The path to load the initial value from when deserialising.</summary>
    public string Path { get; }


    /*********
    ** Constructors
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="path">The path to load the initial value from when deserialising.</param>
    public SerialiserLoadable(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path must not be null or whitespace.", nameof(path));

        Path = path;

        LoadValueFromFile();
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    /*********
    ** Private Methods
    *********/
    /// <summary>Sets <see cref="Value"/> to the value loaded through the content pipeline using the <see cref="Path"/>.</summary>
    /// <exception cref="ArgumentException">Thrown if </exception>
    /// <exception cref="ContentException">Thrown if the value failed to be loaded from the file.</exception>
    [OnDeserialised(SerialiserCallbackPriority.High - 5000)]
    private void LoadValueFromFile()
    {
        if (string.IsNullOrWhiteSpace(Path))
            throw new ArgumentException($"Path is invalid in a {GetType().Name} instance.");

        Value = Content.Load<T>(Path);
    }
}

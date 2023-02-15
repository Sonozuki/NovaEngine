namespace NovaEngine.Debugging;

/// <summary>Represents a value to be used with the <see cref="Debugger"/>.</summary>
internal abstract class DebugValueBase
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the debug value.</summary>
    public string Name { get; }

    /// <summary>The documentation of the debug value.</summary>
    public string Documentation { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the debug value.</param>
    /// <param name="documentation">The documentation of the debug value.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is <see langword="null"/> or white space.</exception>
    public DebugValueBase(string name, string documentation)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Cannot be null or white space.", nameof(name));

        Name = name;
        Documentation = documentation;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Invokes the debug callback with a user entered value.</summary>
    /// <param name="value">The value to parse, then pass to the callback.</param>
    public abstract void InvokeCallback(string value);
}

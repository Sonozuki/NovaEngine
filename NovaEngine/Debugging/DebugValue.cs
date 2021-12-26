namespace NovaEngine.Debugging;

/// <summary>Represents a value to be used with the <see cref="Debugger"/>.</summary>
/// <typeparam name="T">The type of the value.</typeparam>
internal class DebugValue<T> : DebugValueBase
{
    /*********
    ** Accessors
    *********/
    /// <summary>The callback of the debug value.</summary>
    public Action<T?> Callback { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the debug value.</param>
    /// <param name="documentation">The documentation of the debug value.</param>
    /// <param name="callback">The callback of the debug value.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is <see langword="null"/> or white space.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is <see langword="null"/>.</exception>
    public DebugValue(string name, string documentation, Action<T?> callback)
        : base(name, documentation)
    {
        Callback = callback ?? throw new ArgumentNullException(nameof(callback));
    }

    /// <inheritdoc/>
    public override void InvokeCallback(string value)
    {
        T? parsedValue;

        try
        {
            parsedValue = (T?)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
        }
        catch
        {
            Logger.LogError($"Cannot convert: '{value}' to type: {typeof(T)}.");
            return;
        }

        Callback.Invoke(parsedValue);
    }
}

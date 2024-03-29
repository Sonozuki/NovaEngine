﻿namespace NovaEngine.Debugging;

/// <summary>Represents a value to be used with the <see cref="Debugger"/>.</summary>
/// <typeparam name="T">The type of the value.</typeparam>
internal sealed class DebugValue<T> : DebugValueBase
{
    /*********
    ** Properties
    *********/
    /// <summary>The callback of the debug value.</summary>
    public Action<T?> Callback { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the debug value.</param>
    /// <param name="documentation">The documentation of the debug value.</param>
    /// <param name="callback">The callback of the debug value.</param>
    public DebugValue(string name, string documentation, Action<T?> callback)
        : base(name, documentation)
    {
        Callback = callback;
    }


    /*********
    ** Public Methods
    *********/
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
            Logger.LogError($"Cannot convert '{value}' to type: {typeof(T)}.");
            return;
        }

        Callback.Invoke(parsedValue);
    }
}

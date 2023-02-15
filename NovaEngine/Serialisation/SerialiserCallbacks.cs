namespace NovaEngine.Serialisation;

/// <summary>Stores all the methods that are called by the serialiser for a certain type.</summary>
/// <remarks>The type is not stored locally, that is the callers responsibility.</remarks>
internal class SerialiserCallbacks
{
    /*********
    ** Properties
    *********/
    /// <summary>The methods that will automatically be called by the serialiser just before the object gets serialised.</summary>
    public MethodInfo[] OnSerialisingMethods { get; }

    /// <summary>The methods that will automatically be called by the serialiser after the object gets serialised.</summary>
    public MethodInfo[] OnSerialisedMethods { get; }

    /// <summary>The methods that will automatically be called by the serialiser just before the object gets deserialised.</summary>
    public MethodInfo[] OnDeserialisingMethods { get; }

    /// <summary>The methods that will automatically be called by the serialiser after the object gets deserialised.</summary>
    public MethodInfo[] OnDeserialisedMethods { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="onSerialisingMethods">The methods that will automatically be called by the serialiser just before the object gets serialised.</param>
    /// <param name="onSerialisedMethods">The methods that will automatically be called by the serialiser after the object gets serialised.</param>
    /// <param name="onDeserialisingMethods">The methods that will automatically be called by the serialiser just before the object gets deserialised.</param>
    /// <param name="onDeserialisedMethods">The methods that will automatically be called by the serialiser after the object gets deserialised.</param>
    public SerialiserCallbacks(MethodInfo[] onSerialisingMethods, MethodInfo[] onSerialisedMethods, MethodInfo[] onDeserialisingMethods, MethodInfo[] onDeserialisedMethods)
    {
        OnSerialisingMethods = onSerialisingMethods;
        OnSerialisedMethods = onSerialisedMethods;
        OnDeserialisingMethods = onDeserialisingMethods;
        OnDeserialisedMethods = onDeserialisedMethods;
    }
}
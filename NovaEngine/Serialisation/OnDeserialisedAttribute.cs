namespace NovaEngine.Serialisation;

/// <summary>Indicates that the method will automatically be called by the serialiser after the object gets deserialised.</summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class OnDeserialisedAttribute : Attribute
{
    /*********
    ** Accessors
    *********/
    /// <summary>The priority of the callback.</summary>
    public SerialiserCallbackPriority Priority { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="prioirty">The priority of the callback.</param>
    public OnDeserialisedAttribute(SerialiserCallbackPriority prioirty = SerialiserCallbackPriority.Normal)
    {
        Priority = prioirty;
    }
}

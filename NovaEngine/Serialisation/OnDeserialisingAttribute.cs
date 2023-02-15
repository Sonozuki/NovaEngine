namespace NovaEngine.Serialisation;

/// <summary>Indicates that the method will automatically be called by the serialiser just before the object gets deserialised.</summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class OnDeserialisingAttribute : Attribute
{
    /*********
    ** Properties
    *********/
    /// <summary>The priority of the callback.</summary>
    public SerialiserCallbackPriority Priority { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="prioirty">The priority of the callback.</param>
    public OnDeserialisingAttribute(SerialiserCallbackPriority prioirty = SerialiserCallbackPriority.Normal)
    {
        Priority = prioirty;
    }
}

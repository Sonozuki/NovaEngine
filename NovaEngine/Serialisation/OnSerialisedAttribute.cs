namespace NovaEngine.Serialisation;

/// <summary>Indicates that the method will automatically be called by the serialiser after the object gets serialised.</summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class OnSerialisedAttribute : Attribute
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
    public OnSerialisedAttribute(SerialiserCallbackPriority prioirty = SerialiserCallbackPriority.Normal)
    {
        Priority = prioirty;
    }
}

namespace NovaEngine.Serialisation;

/// <summary>Indicates that the method will automatically be called by the serialiser after the object gets deserialised.</summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class OnDeserialisedAttribute : Attribute
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
    /// <param name="priority">The priority of the callback.</param>
    public OnDeserialisedAttribute(SerialiserCallbackPriority priority = SerialiserCallbackPriority.Normal)
    {
        Priority = priority;
    }
}

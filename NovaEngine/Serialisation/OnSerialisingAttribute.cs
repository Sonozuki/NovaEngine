namespace NovaEngine.Serialisation;

/// <summary>Indicates that the method will automatically be called by the serialiser just before the object gets serialised.</summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class OnSerialisingAttribute : Attribute
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
    public OnSerialisingAttribute(SerialiserCallbackPriority priority = SerialiserCallbackPriority.Normal)
    {
        Priority = priority;
    }
}

using System;

namespace NovaEngine.Serialisation
{
    /// <summary>Indicates that the method will automatically be called by the serialiser once the object has been fully created (all members populated).</summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SerialiserCalledAttribute : Attribute { }
}

using System;

namespace NovaEngine.Serialisation
{
    /// <summary>Indicates that a non public member should be serialised.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SerialisableAttribute : Attribute { }
}

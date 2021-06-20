using System;

namespace NovaEngine.Serialisation
{
    /// <summary>Indicates that a public member should not be serialised.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NonSerialisableAttribute : Attribute { }
}

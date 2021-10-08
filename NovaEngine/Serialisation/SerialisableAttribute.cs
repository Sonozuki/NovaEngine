using System;

namespace NovaEngine.Serialisation
{
    /// <summary>Indicates that a member, that wouldn't be serialised by default, should be serialised.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SerialisableAttribute : Attribute { }
}

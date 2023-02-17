namespace NovaEngine.Serialisation;

/// <summary>Indicates that a member, that would be serialised by default, should not be serialised.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class NonSerialisableAttribute : Attribute { }

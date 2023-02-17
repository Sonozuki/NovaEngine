namespace NovaEngine.Serialisation;

/// <summary>Indicates that a member, that wouldn't be serialised by default, should be serialised.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class SerialisableAttribute : Attribute { }

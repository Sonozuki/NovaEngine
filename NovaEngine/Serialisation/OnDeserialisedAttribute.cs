namespace NovaEngine.Serialisation;

/// <summary>Indicates that the method will automatically be called by the serialiser after the object gets deserialised.</summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class OnDeserialisedAttribute : Attribute { }

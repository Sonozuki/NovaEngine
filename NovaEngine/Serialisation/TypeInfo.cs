namespace NovaEngine.Serialisation;

/// <summary>Represents the info about a type.</summary>
internal class TypeInfo
{
    /*********
    ** Accessors
    *********/
    /// <summary>The underlying type this represents.</summary>
    public Type Type { get; }

    /// <summary>Whether the type can be inlined.</summary>
    public bool IsInlinable { get; }

    /// <summary>Whether the type is <see langword="unmanaged"/>.</summary>
    public bool IsUnmanaged { get; }

    /// <summary>The fields that should be serialised for the type.</summary>
    public List<FieldInfo> SerialisableFields { get; } = new();

    /// <summary>The properties that should be serialised for the type.</summary>
    public List<PropertyInfo> SerialisableProperties { get; } = new();

    /// <summary>The methods that will get invoked when the object is reconstructed.</summary>
    public SerialiserCallbacks SerialiserCallbacks { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="type">The object whose object info should be retrieved.</param>
    public TypeInfo(Type type)
    {
        Type = type;
        IsInlinable = type.IsInlinable();
        IsUnmanaged = type.IsUnmanaged();

        SerialisableFields.AddRange(type.GetSerialisableFields());

        var properties = type.GetSerialisableProperties();
        foreach (var property in properties)
            if (property.HasBackingField()) // serialise the backing field directly (if it has one), instead of through the property
                SerialisableFields.Add(property.GetBackingField()!);
            else
                SerialisableProperties.Add(property);

        SerialiserCallbacks = type.GetSerialiserCallbacks();
    }
}

namespace NovaEngine.Extensions;

/// <summary>Extension methods for <see cref="Type"/>.</summary>
public static class TypeExtensions
{
    /*********
    ** Fields
    *********/
    /// <summary>The cached field infos for each type.</summary>
    private static readonly Dictionary<Type, FieldInfo[]> CachedTypeFieldInfos = new();

    /// <summary>The cached property infos for each type.</summary>
    private static readonly Dictionary<Type, PropertyInfo[]> CachedTypePropetyInfos = new();

    /// <summary>The cached serialiser callbacks for each type.</summary>
    private static readonly Dictionary<Type, SerialiserCallbacks> CachedTypeSerialiserCallbacks = new();

    /// <summary>The cached unmanaged values for each type.</summary>
    private static readonly Dictionary<Type, bool> CachedTypeUnmanaged = new();

    /// <summary>The cached inlinable values for each type.</summary>
    private static readonly Dictionary<Type, bool> CachedTypeInlinable = new();


    /*********
    ** Public Methods
    *********/
#pragma warning disable CA1062 // Validate arguments of public methods

    /// <summary>Retrieves the serialisable fields of a type.</summary>
    /// <param name="type">The type to retrieve the serialisable fields for.</param>
    /// <returns>The <see cref="FieldInfo"/>s for the serialisable fields of the type.</returns>
    public static FieldInfo[] GetSerialisableFields(this Type type)
    {
        if (CachedTypeFieldInfos.TryGetValue(type, out var fieldInfos))
            return fieldInfos;

        fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Where(fieldInfo => !fieldInfo.Name.EndsWith(">k__BackingField", true, G11n.Culture) // backing fields are serialised seperately, based on whether properties are serialisable, so exclude them here
                             && !fieldInfo.HasCustomAttribute<NonSerialisableAttribute>()
                             && !(fieldInfo.IsStatic && fieldInfo.IsInitOnly)) // the serialiser isn't able to set the value of static initonly fields
            .ToArray();

        CachedTypeFieldInfos[type] = fieldInfos;
        return fieldInfos;
    }

    /// <summary>Retrieves the serialisable properties of a type.</summary>
    /// <param name="type">The type to retrieve the serialisable properties for.</param>
    /// <returns>The <see cref="PropertyInfo"/>s for the serialisable properties of the type.</returns>
    public static PropertyInfo[] GetSerialisableProperties(this Type type)
    {
        if (CachedTypePropetyInfos.TryGetValue(type, out var propertyInfos))
            return propertyInfos;

        propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Where(propertyInfo => propertyInfo.CanRead && propertyInfo.CanWriteForSerialisation() // property must be readable and writable
                           && (propertyInfo.HasBackingField() || propertyInfo.HasCustomAttribute<SerialisableAttribute>()) // must be an auto property, or property with a serialisable attribute
                           && ((propertyInfo.GetMethod!.IsPublic && !propertyInfo.HasCustomAttribute<NonSerialisableAttribute>()) // getter must either be public without a non serialisable attribute
                               || (!propertyInfo.GetMethod!.IsPublic && propertyInfo.HasCustomAttribute<SerialisableAttribute>())) // or be non public with a serialisable attribute
                           && (propertyInfo.CanWrite || !propertyInfo.IsStatic())) // must be writeable (have a specified set method), or not static (as static methods without a set method are initonly)
            .ToArray();

        CachedTypePropetyInfos[type] = propertyInfos;
        return propertyInfos;
    }

    /// <summary>Gets whether the type is <see langword="unmanaged"/>.</summary>
    /// <param name="type">The type to check.</param>
    /// <returns><see langword="true"/>, if the type is <see langword="unmanaged"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsUnmanaged(this Type type)
    {
        if (CachedTypeUnmanaged.TryGetValue(type, out var isUnmanaged))
            return isUnmanaged;

        try { typeof(U<>).MakeGenericType(type); isUnmanaged = true; }
        catch (ArgumentException) { isUnmanaged = false; }

        CachedTypeUnmanaged[type] = isUnmanaged;
        return isUnmanaged;
    }

    /// <summary>Gets whether the serialiser can inline the type.</summary>
    /// <param name="type">The type to check.</param>
    /// <returns><see langword="true"/>, if the type can be inlined; otherwise, <see langword="false"/>.</returns>
    public static bool IsInlinable(this Type type)
    {
        if (CachedTypeInlinable.TryGetValue(type, out var isInlinable))
            return isInlinable;

        isInlinable = type.IsPrimitive
            || ReferenceEquals(type, typeof(string))
            || ReferenceEquals(type, typeof(decimal))
            || (type.IsUnmanaged() && !type.IsGenericType);

        CachedTypeInlinable[type] = isInlinable;
        return isInlinable;
    }

#pragma warning restore CA1062 // Validate arguments of public methods


    /*********
    ** Internal Methods
    *********/
    /// <summary>Retrieves the serialiser callbacks methods of a type.</summary>
    /// <param name="type">The type to retrieve the callback methods for.</param>
    /// <returns>The serialiser callback methods of the type.</returns>
    internal static SerialiserCallbacks GetSerialiserCallbacks(this Type type)
    {
        if (CachedTypeSerialiserCallbacks.TryGetValue(type, out var callbacks))
            return callbacks;

        var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

        var onSerialisingMethods = methodInfos.Where(methodInfo => methodInfo.HasCustomAttribute<OnSerialisingAttribute>()).OrderBy(methodInfo => methodInfo.GetCustomAttribute<OnSerialisingAttribute>()!.Priority).ToArray();
        var onSerialisedMethods = methodInfos.Where(methodInfo => methodInfo.HasCustomAttribute<OnSerialisedAttribute>()).OrderBy(methodInfo => methodInfo.GetCustomAttribute<OnSerialisedAttribute>()!.Priority).ToArray();
        var onDeserialisingMethods = methodInfos.Where(methodInfo => methodInfo.HasCustomAttribute<OnDeserialisingAttribute>()).OrderBy(methodInfo => methodInfo.GetCustomAttribute<OnDeserialisingAttribute>()!.Priority).ToArray();
        var onDeserialisedMethods = methodInfos.Where(methodInfo => methodInfo.HasCustomAttribute<OnDeserialisedAttribute>()).ToArray(); // OnDeserialised doesn't get ordered here as they get all grouped together and ordered then, so priorities work across types
        callbacks = new(onSerialisingMethods, onSerialisedMethods, onDeserialisingMethods, onDeserialisedMethods);

        CachedTypeSerialiserCallbacks[type] = callbacks;
        return callbacks;
    }
}

#pragma warning disable CA1812 // Internal class that is apparently never instantiated.

/// <summary>A class used for checking if a type is <see langword="unmanaged"/>.</summary>
/// <typeparam name="T">The type to check if it's unmanaged.</typeparam>
file sealed class U<T> where T : unmanaged { }

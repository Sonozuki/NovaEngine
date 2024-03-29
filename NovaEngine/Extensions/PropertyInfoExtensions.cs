﻿namespace NovaEngine.Extensions;

/// <summary>Extension methods for <see cref="PropertyInfo"/>.</summary>
public static class PropertyInfoExtensions
{
    /*********
    ** Fields
    *********/
    /// <summary>The cached backing field info for each property info.</summary>
    private static readonly Dictionary<PropertyInfo, FieldInfo?> CachedPropertyBackingFields = new();


    /*********
    ** Public Methods
    *********/
#pragma warning disable CA1062 // Validate arguments of public methods

    /// <summary>Gets whether the property is static.</summary>
    /// <param name="propertyInfo">The property to check whether it's static.</param>
    /// <returns><see langword="true"/>, if the property is static; otherwise, <see langword="false"/>.</returns>
    public static bool IsStatic(this PropertyInfo propertyInfo) => propertyInfo.GetMethod!.IsStatic;

    /// <summary>Gets whether the property can be written to.</summary>
    /// <param name="propertyInfo">The property to check for the ability to write to.</param>
    /// <returns><see langword="true"/>, if the property can be written to; otherwise, <see langword="false"/>.</returns>
    /// <remarks>This differs from <see cref="PropertyInfo.CanWrite"/> as this also checks for a backing field.</remarks>
    public static bool CanWriteForSerialisation(this PropertyInfo propertyInfo) => propertyInfo.CanWrite || propertyInfo.HasBackingField();

    /// <summary>Gets whether the property has a backing field.</summary>
    /// <param name="propertyInfo">The property info to check for a backing field.</param>
    /// <returns><see langword="true"/>, if the property has a backing field; otherwise, <see langword="false"/>.</returns>
    public static bool HasBackingField(this PropertyInfo propertyInfo) => propertyInfo.GetBackingField() != null;

    /// <summary>Retrieves the backing field of the property.</summary>
    /// <param name="propertyInfo">The property whose backing field should be retrieved.</param>
    /// <returns>The backing field of the property, if the property has one; otherwise, <see langword="null"/>.</returns>
    public static FieldInfo? GetBackingField(this PropertyInfo propertyInfo)
    {
        if (CachedPropertyBackingFields.TryGetValue(propertyInfo, out var fieldInfo))
            return fieldInfo;

        fieldInfo = propertyInfo.DeclaringType?.GetField(propertyInfo.GetBackingFieldName(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);

        CachedPropertyBackingFields[propertyInfo] = fieldInfo;
        return fieldInfo;
    }

    /// <summary>Retrieves the name of the backing field of the property.</summary>
    /// <param name="propertyInfo">The property whose backing field name should be retrieved.</param>
    /// <returns>The name of the backing field.</returns>
    /// <remarks>The name of the backing field will be returned regardless of if the property actually has a backing field.</remarks>
    public static string GetBackingFieldName(this PropertyInfo propertyInfo)
    {
        // key value pair is a special case as it doesn't use auto generated properteries
        if (propertyInfo.DeclaringType?.Name == "KeyValuePair`2")
            return propertyInfo.Name.ToLower(G11n.Culture);
        else
            return $"<{propertyInfo.Name}>k__BackingField";
    }

#pragma warning restore CA1062 // Validate arguments of public methods
}

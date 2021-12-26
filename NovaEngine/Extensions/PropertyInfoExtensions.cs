namespace NovaEngine.Extensions;

/// <summary>Extension methods for the <see cref="PropertyInfo"/> class.</summary>
public static class PropertyInfoExtensions
{
    /*********
    ** Public Methods
    *********/
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
    public static FieldInfo? GetBackingField(this PropertyInfo propertyInfo) =>
        propertyInfo.DeclaringType?.GetField(propertyInfo.GetBackingFieldName(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);

    /// <summary>Retrieves the name of the backing field of the property.</summary>
    /// <param name="propertyInfo">The property whose backing field name should be retrieved.</param>
    /// <returns>The name of the backing field.</returns>
    /// <remarks>The name of the backing field will be returned regardless of if the property actually has a backing field.</remarks>
    public static string GetBackingFieldName(this PropertyInfo propertyInfo)
    {
        // key value pair is a special case as it doesn't use auto generated properteries
        if (propertyInfo.DeclaringType?.Name == "KeyValuePair`2")
            return propertyInfo.Name.ToLower();
        else
            return $"<{propertyInfo.Name}>k__BackingField";
    }
}

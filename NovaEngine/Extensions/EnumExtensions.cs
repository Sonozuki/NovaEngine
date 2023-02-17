namespace NovaEngine.Extensions;

/// <summary>Extension methods for <see cref="Enum"/>.</summary>
public static class EnumExtensions
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Gets the first attribute of a specified type on an enum.</summary>
    /// <typeparam name="T">The attribute type to get.</typeparam>
    /// <param name="value">The enumeration instance to get the attribute from.</param>
    /// <returns>The first occurance of an attribute of type <typeparamref name="T"/>, if one exists; otherwise, <see langword="null"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
    public static T? GetAttribute<T>(this Enum value)
        where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(value);

        var memberInfo = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        return (T?)memberInfo?.GetCustomAttributes(typeof(T), false).FirstOrDefault();
    }
}

namespace NovaEngine.Extensions;

/// <summary>Extension methods for the <see cref="MemberInfo"/> class.</summary>
public static class MemberInfoExtensions
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Gets whether the member has an attribute of a specified type.</summary>
    /// <typeparam name="T">The type of of attribute to check for.</typeparam>
    /// <param name="memberInfo">The member whose attributes to check.</param>
    /// <returns><see langword="true"/>, if the member has an attribute of type <typeparamref name="T"/>; otherwise, <see langword="false"/>.</returns>
    public static bool HasCustomAttribute<T>(this MemberInfo memberInfo) where T : Attribute => memberInfo.GetCustomAttribute<T>() != null;

    /// <summary>Gets the full name of the member.</summary>
    /// <param name="memberInfo">The member whose full name to retrieve.</param>
    /// <returns>The full name of the member (includes namespace and type).</returns>
    public static string GetFullName(this MemberInfo memberInfo) => $"{memberInfo.DeclaringType?.FullName}.{memberInfo.Name}";
}

﻿namespace NovaEngine.Extensions;

/// <summary>Extension methods for <see cref="MemberInfo"/>.</summary>
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
}

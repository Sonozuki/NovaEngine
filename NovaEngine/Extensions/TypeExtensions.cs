using System;
using System.Reflection;

namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="Type"/> class.</summary>
    public static class TypeExtensions
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Gets whether the type is <see langword="unmanaged"/>.</summary>
        /// <param name="type">The type to check.</param>
        /// <returns><see langword="true"/>, if the type is <see cref="unmanaged"/>; otherwise, <see langword="false"/>.</returns>
        public static bool IsUnmanaged(this Type type)
        {
            try { typeof(U<>).MakeGenericType(type); return true; }
            catch { return false; }
        }

        /// <summary>Gets whether the serialiser can inline the type.</summary>
        /// <param name="type">The type to check.</param>
        /// <returns><see langword="true"/>, if the type can be inlined; otherwise, <see langword="false"/>.</returns>
        public static bool IsInlinable(this Type type) => type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || (type.IsUnmanaged() && !type.IsGenericType);

        /// <summary>Searches for the specified field, using the specified binding constraints.</summary>
        /// <param name="type">The type to search for the field.</param>
        /// <param name="name">The name of the field to get.</param>
        /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return null.</param>
        /// <returns>An object representing the field that matches the specified requirements, if found; otherwise, <see langword="null"/>.</returns>
        /// <remarks>This will check the base type recursively until the field is found, if it's found at all.</remarks>
        public static FieldInfo? GetFieldRecursive(this Type type, string name, BindingFlags bindingAttr)
        {
            do
            {
                var field = type.GetField(name, bindingAttr);
                if (field != null)
                    return field;
            }
            while ((type = type.BaseType!) != null);

            return null;
        }

        /// <summary>Searches for the specified property, using the specified binding constraints.</summary>
        /// <param name="type">The type to search for the property.</param>
        /// <param name="name">The name of the property to get.</param>
        /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return null.</param>
        /// <returns>An object representing the property that matches the specified requirements, if found; otherwise, <see langword="null"/>.</returns>
        /// <remarks>This will check the base type recursively until the property is found, if it's found at all.</remarks>
        public static PropertyInfo? GetPropertyRecursive(this Type type, string name, BindingFlags bindingAttr)
        {
            do
            {
                var property = type.GetProperty(name, bindingAttr);
                if (property != null)
                    return property;
            }
            while ((type = type.BaseType!) != null);

            return null;
        }


        /*********
        ** Classes
        *********/
        /// <summary>A class used for checking if a type is <see langword="unmanaged"/>.</summary>
        /// <typeparam name="T">The type to check if it's unmanaged.</typeparam>
        private class U<T> where T : unmanaged { }
    }
}

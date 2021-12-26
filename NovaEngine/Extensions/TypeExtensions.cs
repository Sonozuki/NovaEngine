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
        /// <returns><see langword="true"/>, if the type is <see langword="unmanaged"/>; otherwise, <see langword="false"/>.</returns>
        public static bool IsUnmanaged(this Type type)
        {
            try { typeof(U<>).MakeGenericType(type); return true; }
            catch { return false; }
        }

        /// <summary>Gets whether the serialiser can inline the type.</summary>
        /// <param name="type">The type to check.</param>
        /// <returns><see langword="true"/>, if the type can be inlined; otherwise, <see langword="false"/>.</returns>
        public static bool IsInlinable(this Type type) => type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || (type.IsUnmanaged() && !type.IsGenericType);


        /*********
        ** Classes
        *********/
        /// <summary>A class used for checking if a type is <see langword="unmanaged"/>.</summary>
        /// <typeparam name="T">The type to check if it's unmanaged.</typeparam>
        private class U<T> where T : unmanaged { }
    }
}

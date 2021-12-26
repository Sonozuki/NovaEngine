namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="Enum"/> class.</summary>
    public static class EnumExtensions
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Gets the first attribute of a specified type on an enum.</summary>
        /// <typeparam name="T">The attribute type to get.</typeparam>
        /// <param name="enum">The enumeration instance to get the attribute from.</param>
        /// <returns>The first occurance of an attribute of type <typeparamref name="T"/>, if one exists; otherwise, <see langword="null"/>.</returns>
        public static T? GetAttribute<T>(this Enum @enum)
            where T : Attribute
        {
            var memberInfo = @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault();
            return (T?)memberInfo?.GetCustomAttributes(typeof(T), false).FirstOrDefault();
        }
    }
}

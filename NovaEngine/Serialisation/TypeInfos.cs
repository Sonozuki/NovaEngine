namespace NovaEngine.Serialisation
{
    /// <summary>Represents a collection of <see cref="TypeInfo"/>s.</summary>
    internal class TypeInfos : IEnumerable<TypeInfo>
    {
        /*********
        ** Fields
        *********/
        /// <summary>The cached type infos.</summary>
        private readonly Dictionary<Type, TypeInfo> CachedTypeInfos = new();


        /*********
        ** Public Methods
        *********/
        /// <summary>Retrieves a type info for a specified type.</summary>
        /// <param name="type">The type whose type info should be retrieved.</param>
        /// <returns>The type info representing <paramref name="type"/>.</returns>
        /// <remarks>If a type info hasn't been calculated yet, one will get calculated and cached.</remarks>
        public TypeInfo Get(Type type)
        {
            // check if type info has been cached
            if (CachedTypeInfos.TryGetValue(type, out var typeInfo))
                return typeInfo;

            // calculate and cache type info
            typeInfo = new TypeInfo(type);
            CachedTypeInfos[type] = typeInfo;
            return typeInfo;
        }

        /// <inheritdoc/>
        public IEnumerator<TypeInfo> GetEnumerator() => CachedTypeInfos.Values.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => CachedTypeInfos.Values.GetEnumerator();
    }
}

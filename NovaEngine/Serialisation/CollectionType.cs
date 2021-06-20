namespace NovaEngine.Serialisation
{
    /// <summary>The type of a serialised collection.</summary>
    internal enum CollectionType
    {
        /// <summary>The collection is an array.</summary>
        Array,

        /// <summary>The collection is a non generic list.</summary>
        List,

        /// <summary>The collection is a generic list.</summary>
        GenericList,

        /// <summary>The collection is a non generic dictionary.</summary>
        Dictionary,

        /// <summary>The collection is a generic dictionary.</summary>
        GenericDictionary
    }
}

namespace NovaEngine.Content.Readers
{
    /// <summary>Defines how content should be read from a nova file.</summary>
    public interface IContentReader
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The tyoe of content the reader is for.</summary>
        /// <remarks>For example 'texture', this is used so an asset file can't be used with the wrong reader.</remarks>
        public string Type { get; }
    }
}

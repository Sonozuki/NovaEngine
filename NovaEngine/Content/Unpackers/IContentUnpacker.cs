namespace NovaEngine.Content.Unpackers
{
    /// <summary>Defines how a nova file should be rewritten for a regular file.</summary>
    public interface IContentUnpacker
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Writes the a packed stream to an unpacked file.</summary>
        /// <param name="stream">The packed stream to convert.</param>
        /// <param name="destinationFile">The file that should be created/overwrote with the unpacked content.</param>
        public void Write(Stream stream, string destinationFile);
    }
}

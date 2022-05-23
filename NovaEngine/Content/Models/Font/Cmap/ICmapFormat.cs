namespace NovaEngine.Content.Models.Font.Cmap;

/// <summary>The base of a cmap subtable format.</summary>
internal interface ICmapFormat
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves the glyph index for a specified characte code.</summary>
    /// <param name="binaryReader">The binary reader; the position isn't reset automatically, so should be handled manually.</param>
    /// <param name="characterCode">The character code.</param>
    /// <returns>The glyph index.</returns>
    public uint Map(BinaryReader binaryReader, int characterCode);
}

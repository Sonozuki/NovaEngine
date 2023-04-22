namespace NovaEngine.ContentPipeline.Font.Records;

/// <summary>Represents a language-tag record.</summary>
internal sealed class LanguageTagRecord
{
    /*********
    ** Properties
    *********/
    /// <summary>The language-tag string length, in <see langword="byte"/>s.</summary>
    public ushort Length { get; }

    /// <summary>The offset from the start of the storage area to the language-tag string, in <see langword="byte"/>s.</summary>
    public ushort LanguageTagOffset { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="length">The language-tag string length, in <see langword="byte"/>s.</param>
    /// <param name="languageTagOffset">The offset from the start of the storage area to the language-tag string, in <see langword="byte"/>s.</param>
    public LanguageTagRecord(ushort length, ushort languageTagOffset)
    {
        Length = length;
        LanguageTagOffset = languageTagOffset;
    }
}

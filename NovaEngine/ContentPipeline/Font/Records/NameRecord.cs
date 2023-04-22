namespace NovaEngine.ContentPipeline.Font.Records;

/// <summary>Represents a name record.</summary>
internal sealed class NameRecord
{
    /*********
    ** Properties
    *********/
    /// <summary>The id of the platform of the record.</summary>
    public PlatformId PlatformId { get; }

    /// <summary>The id of the encoding of the record.</summary>
    public ushort EncodingId { get; }

    /// <summary>The id of the language of the record.</summary>
    public ushort LanguageId { get; }

    /// <summary>The name id of the record.</summary>
    public NameId NameId { get; }

    /// <summary>The length of the string, in <see langword="byte"/>s.</summary>
    public ushort Length { get; }

    /// <summary>The offset from the start of the storage area of the string, in <see langword="byte"/>s.</summary>
    public ushort StringOffset { get; }

    /// <summary>The value of the record.</summary>
    public string Value { get; private set; }


    /*********
    ** Constructors
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="platformId">The id of the platform of the record.</param>
    /// <param name="encodingId">The id of the encoding of the record.</param>
    /// <param name="languageId">The id of the language of the record.</param>
    /// <param name="nameId">The name id of the record.</param>
    /// <param name="length">The length of the string, in <see langword="byte"/>s.</param>
    /// <param name="stringOffset">The offset from the start of the storage area of the string, in <see langword="byte"/>s.</param>
    public NameRecord(PlatformId platformId, ushort encodingId, ushort languageId, NameId nameId, ushort length, ushort stringOffset)
    {
        PlatformId = platformId;
        EncodingId = encodingId;
        LanguageId = languageId;
        NameId = nameId;
        Length = length;
        StringOffset = stringOffset;
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    /*********
    ** Public Methods
    *********/
    /// <summary>Reads the string the record represents</summary>
    /// <param name="binaryReader">The binary reader to use to read the string from.</param>
    /// <param name="storageAreaOffset">The offset of the storage area.</param>
    public void ReadString(BinaryReader binaryReader, uint storageAreaOffset)
    {
        binaryReader.BaseStream.Position = storageAreaOffset + StringOffset;

        if (PlatformId == PlatformId.Macintosh)
            // Apple (English) uses UTF-8. I'm not sure if other languages also use UTF-8 I'll eventually double check that
            Value = Encoding.UTF8.GetString(binaryReader.ReadBytes(Length));
        else if (PlatformId == PlatformId.Unicode || PlatformId == PlatformId.Windows)
        {
            // Unicode and Windows are always UTF-16BE
            var chars = new List<char>();
            for (var i = 0; i < Length; i += 2)
                chars.Add((char)binaryReader.ReadUInt16BigEndian());

            Value = new(chars.ToArray());
        }
    }
}

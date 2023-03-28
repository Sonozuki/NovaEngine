using NovaEngine.ContentPipeline.Font.Models;

namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the naming table.</summary>
internal sealed class NameTable
{
    /*********
    ** Properties
    *********/
    /// <summary>The version of the table. This is either 0 or 1.</summary>
    public ushort Version { get; }

    /// <summary>The number of name records.</summary>
    public ushort NumberOfNameRecords { get; }

    /// <summary>The offset from the beginning of the 'name' table of the string storage.</summary>
    public ushort StorageOffset { get; }

    /// <summary>The name records.</summary>
    public ImmutableArray<NameRecord> NameRecords { get; }

    /// <summary>The number of language-tag records.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ushort NumberOfLanguageTagRecords { get; }

    /// <summary>The language-tag records.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 1.</remarks>
    public ImmutableArray<LanguageTagRecord> LanguageTagRecords { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the table.<br/>
    /// This assumes the reader has been positioned for the table to be read.
    /// </param>
    public NameTable(BinaryReader binaryReader)
    {
        var tableOffset = binaryReader.BaseStream.Position;

        Version = binaryReader.ReadUInt16BigEndian();
        NumberOfNameRecords = binaryReader.ReadUInt16BigEndian();
        StorageOffset = binaryReader.ReadUInt16BigEndian();

        var nameRecords = new List<NameRecord>();
        for (var i = 0; i < NumberOfNameRecords; i++)
            nameRecords.Add(new((PlatformId)binaryReader.ReadUInt16BigEndian(), binaryReader.ReadUInt16BigEndian(), binaryReader.ReadUInt16BigEndian(), (NameId)binaryReader.ReadUInt16BigEndian(), binaryReader.ReadUInt16BigEndian(), binaryReader.ReadUInt16BigEndian()));
        NameRecords = nameRecords.ToImmutableArray();

        if (Version == 1)
        {
            NumberOfLanguageTagRecords = binaryReader.ReadUInt16BigEndian();

            var languageTagRecord = new List<LanguageTagRecord>();
            for (var i = 0; i < NumberOfLanguageTagRecords; i++)
                languageTagRecord.Add(new(binaryReader.ReadUInt16BigEndian(), binaryReader.ReadUInt16BigEndian()));
            LanguageTagRecords = languageTagRecord.ToImmutableArray();
        }

        if (Version != 0 && Version != 1)
            throw new FontException("'name' version is invalid.");

        foreach (var nameRecord in NameRecords)
            nameRecord.ReadString(binaryReader, (uint)(tableOffset + StorageOffset));
    }
}

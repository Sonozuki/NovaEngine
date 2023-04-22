namespace NovaEngine.ContentPipeline.Font.Records;

/// <summary>Represents a top-level table record.</summary>
internal sealed class TableRecord
{
    /*********
    ** Properties
    *********/
    /// <summary>The identifier of the table.</summary>
    public Tag Tag { get; }

    /// <summary>The checksum of the table.</summary>
    public uint Checksum { get; }

    /// <summary>The offset from the beginning of the font file of the table.</summary>
    public uint Offset { get; }

    /// <summary>The length of the table.</summary>
    public uint Length { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="tag">The identifier of the table.</param>
    /// <param name="checksum">The checksum of the table.</param>
    /// <param name="offset">The offset from the beginning of the font file of the table.</param>
    /// <param name="length">The length of the table.</param>
    public TableRecord(Tag tag, uint checksum, uint offset, uint length)
    {
        Tag = tag;
        Checksum = checksum;
        Offset = offset;
        Length = length;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Validates the table using the checksum.</summary>
    /// <param name="binaryReader">The reader to use that contains the table.</param>
    /// <remarks>This will not reset the position of the binary reader.</remarks>
    /// <exception cref="FontException">Thrown if the table is invalid.</exception>
    public void Validate(BinaryReader binaryReader)
    {
        binaryReader.BaseStream.Position = Offset;

        var sum = 0u;
        var numberOfUints = (int)MathF.Ceiling(Length / 4f);
        for (var i = 0; i < numberOfUints; i++)
            sum += binaryReader.ReadUInt32BigEndian();

        if (sum != Checksum)
            throw new FontException($"Table '{Tag}' has invalid checksum.");
    }
}

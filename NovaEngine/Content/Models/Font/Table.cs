namespace NovaEngine.Content.Models.Font;

/// <summary>Represents a table in a TTF file.</summary>
internal sealed class Table
{
    /*********
    ** Properties
    *********/
    /// <summary>The checksum of the table.</summary>
    public uint Checksum { get; }

    /// <summary>The offset of the table.</summary>
    public uint Offset { get; }

    /// <summary>The length of the table.</summary>
    public uint Length { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="checksum">The checksum of the table.</param>
    /// <param name="offset">The offset of the table.</param>
    /// <param name="length">The length of the table.</param>
    public Table(uint checksum, uint offset, uint length)
    {
        Checksum = checksum;
        Offset = offset;
        Length = length;
    }
}

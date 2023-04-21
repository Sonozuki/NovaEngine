using NovaEngine.ContentPipeline.Font;
using System.Buffers.Binary;

namespace NovaEngine.Extensions;

/// <summary>Extension methods for <see cref="BinaryReader"/>.</summary>
internal static class BinaryReaderExtensions
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Reads a 4-byte tag from the current stream and advances the current position of the stream by four bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 4-byte tag read from the current stream.</returns>
    public static Tag ReadOTFTag(this BinaryReader binaryReader) => new(binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte());

    /// <summary>Reads an 8-byte date time as big endian from the current stream and advances the current position of the stream by eight bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>The 8-byte date time read from the current stream.</returns>
    /// <remarks>This will read an 8-byte number representing the number of seconds that have elapsed since 12:00 midnight, January 1, 1904, UTC.</remarks>
    public static DateTime ReadOTFDateTime(this BinaryReader binaryReader) => new DateTime(1904, 1, 1).AddSeconds(binaryReader.ReadInt64BigEndian());

    /// <summary>Reads a 2-byte signed integer as big endian from the current stream and advances the current position of the stream by two bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 2-byte signed integer read from the current stream.</returns>
    public static short ReadInt16BigEndian(this BinaryReader binaryReader) => BinaryPrimitives.ReverseEndianness(binaryReader.ReadInt16());

    /// <summary>Reads a 2-byte unsigned integer as big endian from the current stream and advances the current position of the stream by two bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 2-byte unsigned integer read from the current stream.</returns>
    public static ushort ReadUInt16BigEndian(this BinaryReader binaryReader) => BinaryPrimitives.ReverseEndianness(binaryReader.ReadUInt16());

    /// <summary>Reads a 4-byte signed integer as big endian from the current stream and advances the current position of the stream by four bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 4-byte signed integer read from the current stream.</returns>
    public static int ReadInt32BigEndian(this BinaryReader binaryReader) => BinaryPrimitives.ReverseEndianness(binaryReader.ReadInt32());
    
    /// <summary>Reads a 4-byte unsigned integer as big endian from the current stream and advances the current position of the stream by four bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 4-byte unsigned integer read from the current stream.</returns>
    public static uint ReadUInt32BigEndian(this BinaryReader binaryReader) => BinaryPrimitives.ReverseEndianness(binaryReader.ReadUInt32());

    /// <summary>Reads an 8-byte signed integer as big endian from the current stream and advances the current position of the stream by eight bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>An 8-byte signed integer read from the current stream.</returns>
    public static long ReadInt64BigEndian(this BinaryReader binaryReader) => BinaryPrimitives.ReverseEndianness(binaryReader.ReadInt64());

    /// <summary>Reads a 2-byte fixed-point number with the low 14 bits representing the fraction, and advances the current position of the stream by two bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 2-byte fixed-point number with the low 14 bits representing fraction.</returns>
    public static float Read2Dot14(this BinaryReader binaryReader) => binaryReader.ReadInt16BigEndian() / (float)(1 << 14);

    /// <summary>Reads a 4-byte fixed-point number with the low 16 bits representing the fraction, and advances the current position of the stream by four bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 4-byte fixed-point number with the low 16 bits representing fraction.</returns>
    public static float Read16Dot16(this BinaryReader binaryReader) => binaryReader.ReadInt32BigEndian() / (float)(1 << 16);

    /// <summary>Reads a collection of inlinable objects from the current stream and advances the position.</summary>
    /// <param name="binaryReader">The binary reader to read from.</param>
    /// <returns>The read inlinable objects.</returns>
    public static List<object?> ReadInlinableObjects(this BinaryReader binaryReader)
    {
        var list = new List<object?>();

        var count = binaryReader.ReadUInt16();
        for (var i = 0; i < count; i++)
            list.Add(SerialiserUtilities.ReadInlinedValueFromStream(binaryReader));

        return list;
    }

    /// <summary>Reads a collection of non inlinable object ids from the current stream and advances the position.</summary>
    /// <param name="binaryReader">The binary reader to read from.</param>
    /// <returns>The read non inlinable object ids.</returns>
    public static List<uint> ReadNonInlinableObjects(this BinaryReader binaryReader)
    {
        var list = new List<uint>();

        var count = binaryReader.ReadUInt16();
        for (var i = 0; i < count; i++)
            list.Add(binaryReader.ReadUInt32());

        return list;
    }

    /// <summary>Writes a collection of inlinable members to the current stream and advances the position.</summary>
    /// <param name="binaryReader">The binary reader to read from.</param>
    /// <returns>The read inlinable members.</returns>
    public static Dictionary<string, object?> ReadInlinableMembers(this BinaryReader binaryReader)
    {
        var dictionary = new Dictionary<string, object?>();
        
        var count = binaryReader.ReadUInt16();
        for (var i = 0; i < count; i++)
            dictionary[binaryReader.ReadString()] = SerialiserUtilities.ReadInlinedValueFromStream(binaryReader);

        return dictionary;
    }

    /// <summary>Writes a collection of non inlinable members to the current stream and advances the position.</summary>
    /// <param name="binaryReader">The binary reader to read from.</param>
    /// <returns>The read non inlinable members.</returns>
    public static Dictionary<string, uint> ReadNonInlinableMembers(this BinaryReader binaryReader)
    {
        var dictionary = new Dictionary<string, uint>();

        var count = binaryReader.ReadUInt16();
        for (var i = 0; i < count; i++)
            dictionary[binaryReader.ReadString()] = binaryReader.ReadUInt32();

        return dictionary;
    }

    /// <summary>Reads a UTF-8 encoded null-terminated string.</summary>
    /// <param name="binaryReader">The binary reader to read from.</param>
    /// <returns>The read string.</returns>
    public static string ReadUTF8NullTerminatedString(this BinaryReader binaryReader)
    {
        var contentTypeBytes = new List<byte>();

        byte nextCharacter;
        while ((nextCharacter = binaryReader.ReadByte()) != 0)
            contentTypeBytes.Add(nextCharacter);

        return Encoding.UTF8.GetString(contentTypeBytes.ToArray());
    }
}

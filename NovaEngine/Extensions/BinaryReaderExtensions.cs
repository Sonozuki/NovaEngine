using System.Buffers.Binary;

namespace NovaEngine.Extensions;

/// <summary>Extension methods for <see cref="BinaryReader"/>.</summary>
internal static class BinaryReaderExtensions
{
    /*********
    ** Internal Methods
    *********/
    /// <summary>Reads a 2-byte signed integer as big endian from the current stream and advances the current position of the stream by two bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 2-byte signed integer read from the current stream.</returns>
    internal static short ReadInt16BigEndian(this BinaryReader binaryReader) => BinaryPrimitives.ReverseEndianness(binaryReader.ReadInt16());

    /// <summary>Reads a 2-byte signed integer as big endian from the current stream and advances the current position of the stream by two bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 2-byte signed integer read from the current stream.</returns>
    internal static ushort ReadUInt16BigEndian(this BinaryReader binaryReader) => BinaryPrimitives.ReverseEndianness(binaryReader.ReadUInt16());

    /// <summary>Reads a 4-byte signed integer as big endian from the current stream and advances the current position of the stream by four bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 4-byte signed integer read from the current stream.</returns>
    internal static uint ReadUInt32BigEndian(this BinaryReader binaryReader) => BinaryPrimitives.ReverseEndianness(binaryReader.ReadUInt32());

    /// <summary>Reads a 2-byte signed integer as big endian from the current stream, dividing it by <c>1&lt;&lt;14</c>, and advances the current position of the stream by two bytes.</summary>
    /// <param name="binaryReader">The binary reader to read the bytes from.</param>
    /// <returns>A 2-byte signed integer read from the current stream, divided by <c>1&lt;&lt;14</c>.</returns>
    internal static short Read2Dot14(this BinaryReader binaryReader) => (short)(binaryReader.ReadInt16BigEndian() / (1 << 14));

    /// <summary>Writes a collection of inlinable object buffers to the current stream and advances the position.</summary>
    /// <param name="binaryReader">The binary reader to read from.</param>
    /// <returns>The read inlinable object buffers.</returns>
    internal static List<object?> ReadInlinableObjects(this BinaryReader binaryReader)
    {
        var list = new List<object?>();

        var count = binaryReader.ReadUInt16();
        for (int i = 0; i < count; i++)
            list.Add(SerialiserUtilities.ReadInlinedValueFromStream(binaryReader));

        return list;
    }

    /// <summary>Writes a collection of non inlinable object ids to the current stream and advances the position.</summary>
    /// <param name="binaryReader">The binary reader to read from.</param>
    /// <returns>The read non inlinable object ids.</returns>
    internal static List<uint> ReadNonInlinableObjects(this BinaryReader binaryReader)
    {
        var list = new List<uint>();

        var count = binaryReader.ReadUInt16();
        for (int i = 0; i < count; i++)
            list.Add(binaryReader.ReadUInt32());

        return list;
    }

    /// <summary>Writes a collection of inlinable members to the current stream and advances the position.</summary>
    /// <param name="binaryReader">The binary reader to read from.</param>
    /// <returns>The read inlinable members.</returns>
    internal static Dictionary<string, object?> ReadInlinableMembers(this BinaryReader binaryReader)
    {
        var dictionary = new Dictionary<string, object?>();
        
        var count = binaryReader.ReadUInt16();
        for (int i = 0; i < count; i++)
            dictionary[binaryReader.ReadString()] = SerialiserUtilities.ReadInlinedValueFromStream(binaryReader);

        return dictionary;
    }

    /// <summary>Writes a collection of non inlinable members to the current stream and advances the position.</summary>
    /// <param name="binaryReader">The binary reader to read from.</param>
    /// <returns>The read non inlinable members.</returns>
    internal static Dictionary<string, uint> ReadNonInlinableMembers(this BinaryReader binaryReader)
    {
        var dictionary = new Dictionary<string, uint>();

        var count = binaryReader.ReadUInt16();
        for (int i = 0; i < count; i++)
            dictionary[binaryReader.ReadString()] = binaryReader.ReadUInt32();

        return dictionary;
    }
}

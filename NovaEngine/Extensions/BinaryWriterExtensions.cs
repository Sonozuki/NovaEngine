﻿namespace NovaEngine.Extensions;

/// <summary>Extension methods for <see cref="BinaryWriter"/>.</summary>
internal static class BinaryWriterExtensions
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Writes a collection of inlinable object buffers to the current stream and advances the position.</summary>
    /// <param name="binaryWriter">The binary writer to write to.</param>
    /// <param name="inlinableObjects">The inlinable object buffers to write.</param>
    public static void Write(this BinaryWriter binaryWriter, IEnumerable<byte[]> inlinableObjects)
    {
        var array = inlinableObjects.ToArray();

        binaryWriter.Write((ushort)array.Length);
        binaryWriter.Write(array.SelectMany(buffer => buffer).ToArray());
    }

    /// <summary>Writes a collection of non inlinable object unique ids to the current stream and advances the stream position.</summary>
    /// <param name="binaryWriter">The binary writer to write to.</param>
    /// <param name="nonInlinableObjects">The non inlinable object ids to write.</param>
    public static void Write(this BinaryWriter binaryWriter, IEnumerable<uint> nonInlinableObjects)
    {
        var array = nonInlinableObjects.ToArray();

        binaryWriter.Write((ushort)array.Length);
        foreach (var element in array)
            binaryWriter.Write(element);
    }

    /// <summary>Writes a collection of inlinable members to the current stream and advances the stream position.</summary>
    /// <param name="binaryWriter">The binary writer to write to.</param>
    /// <param name="inlinableMembers">The inlinable members to write.</param>
    public static void Write(this BinaryWriter binaryWriter, IDictionary<string, byte[]> inlinableMembers)
    {
        binaryWriter.Write((ushort)inlinableMembers.Count);
        foreach (var kvp in inlinableMembers)
        {
            binaryWriter.Write(kvp.Key);
            binaryWriter.Write(kvp.Value);
        }
    }

    /// <summary>Writes a collection of non inlinable members to the current stream and advances the stream position.</summary>
    /// <param name="binaryWriter">The binary writer to write to.</param>
    /// <param name="nonInlinableMembers">The non inlinable members to write.</param>
    public static void Write(this BinaryWriter binaryWriter, IDictionary<string, uint> nonInlinableMembers)
    {
        binaryWriter.Write((ushort)nonInlinableMembers.Count);
        foreach (var kvp in nonInlinableMembers)
        {
            binaryWriter.Write(kvp.Key);
            binaryWriter.Write(kvp.Value);
        }
    }

    /// <summary>Writes a UTF-8 encoded null-terminated string to the current stream and advances the stream position.</summary>
    /// <param name="binaryWriter">The binary writer to write to.</param>
    /// <param name="value">The string to write.</param>
    public static void WriteUTF8NullTerminatedString(this BinaryWriter binaryWriter, string value)
    {
        binaryWriter.Write(Encoding.UTF8.GetBytes(value));
        binaryWriter.Write((byte)0);
    }
}

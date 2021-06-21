using NovaEngine.Serialisation;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="BinaryReader"/> class.</summary>
    internal static class BinaryReaderExtensions
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Reads an object from the current stream and advances the stream position.</summary>
        /// <param name="binaryReader">The <see cref="BinaryReader"/> to read from.</param>
        /// <returns>The read <see langword="object"/>.</returns>
        public static object? ReadObject(this BinaryReader binaryReader)
        {
            if (binaryReader.ReadBoolean())
                return null;

            switch ((InlinedValueType)binaryReader.ReadByte())
            {
                case InlinedValueType.Bool:    return binaryReader.ReadBoolean();
                case InlinedValueType.SByte:   return binaryReader.ReadSByte();
                case InlinedValueType.Byte:    return binaryReader.ReadByte();
                case InlinedValueType.Char:    return binaryReader.ReadChar();
                case InlinedValueType.Short:   return binaryReader.ReadInt16();
                case InlinedValueType.UShort:  return binaryReader.ReadUInt16();
                case InlinedValueType.Int:     return binaryReader.ReadInt32();
                case InlinedValueType.UInt:    return binaryReader.ReadUInt32();
                case InlinedValueType.Long:    return binaryReader.ReadInt64();
                case InlinedValueType.ULong:   return binaryReader.ReadUInt64();
                case InlinedValueType.Float:   return binaryReader.ReadSingle();
                case InlinedValueType.Double:  return binaryReader.ReadDouble();
                case InlinedValueType.Decimal: return binaryReader.ReadDecimal();
                case InlinedValueType.String:  return binaryReader.ReadString();
                case InlinedValueType.Enum:    return Enum.Parse(Type.GetType(binaryReader.ReadString())!, binaryReader.ReadString());
                case InlinedValueType.Unmanaged:
                    {
                        var typeName = binaryReader.ReadString();
                        var size = binaryReader.ReadInt32();
                        var buffer = binaryReader.ReadBytes(size);
                        var pointer = IntPtr.Zero;

                        try
                        {
                            pointer = Marshal.AllocHGlobal(size);
                            Marshal.Copy(buffer, 0, pointer, size);

                            var type = Type.GetType(typeName);
                            if (type == null)
                                throw new SerialisationException($"Failed to create type: {typeName}");

                            var @object = Marshal.PtrToStructure(pointer, type);
                            if (@object == null)
                                throw new SerialisationException($"Failed to create instance of type: {typeName}");

                            return @object;
                        }
                        finally
                        {
                            if (pointer != IntPtr.Zero)
                                Marshal.FreeHGlobal(pointer);
                        }
                    }
                case var valueType: throw new SerialisationException($"Invalid inlined value type found ({(int)valueType})");
            };
        }
    }
}

using NovaEngine.Serialisation;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="BinaryWriter"/> class.</summary>
    public static class BinaryWriterExtensions
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Writes an <see langword="object"/> to the current stream and advances the stream position.</summary>
        /// <param name="binaryWriter">The <see cref="BinaryWriter"/> to writer to.</param>
        /// <param name="object">The <see langword="object"/> to write to the stream.</param>
        /// <remarks><paramref name="object"/> must be of a type that's normally serialisable, just not casted as that type.</remarks>
        /// <exception cref="ArgumentException">Thrown if <paramref name="object"/> isn't a serialisable type.</exception>
        public static void Write(this BinaryWriter binaryWriter, object? @object)
        {
            binaryWriter.Write(@object == null);
            if (@object == null)
                return;

            switch (@object)
            {
                case bool @bool:       binaryWriter.Write((byte)InlinedValueType.Bool);    binaryWriter.Write(@bool);    break;
                case sbyte @sbyte:     binaryWriter.Write((byte)InlinedValueType.SByte);   binaryWriter.Write(@sbyte);   break;
                case byte @byte:       binaryWriter.Write((byte)InlinedValueType.Byte);    binaryWriter.Write(@byte);    break;
                case char @char:       binaryWriter.Write((byte)InlinedValueType.Char);    binaryWriter.Write(@char);    break;
                case short @short:     binaryWriter.Write((byte)InlinedValueType.Short);   binaryWriter.Write(@short);   break;
                case ushort @ushort:   binaryWriter.Write((byte)InlinedValueType.UShort);  binaryWriter.Write(@ushort);  break;
                case int @int:         binaryWriter.Write((byte)InlinedValueType.Int);     binaryWriter.Write(@int);     break;
                case uint @uint:       binaryWriter.Write((byte)InlinedValueType.UInt);    binaryWriter.Write(@uint);    break;
                case long @long:       binaryWriter.Write((byte)InlinedValueType.Long);    binaryWriter.Write(@long);    break;
                case ulong @ulong:     binaryWriter.Write((byte)InlinedValueType.ULong);   binaryWriter.Write(@ulong);   break;
                case float @float:     binaryWriter.Write((byte)InlinedValueType.Float);   binaryWriter.Write(@float);   break;
                case double @double:   binaryWriter.Write((byte)InlinedValueType.Double);  binaryWriter.Write(@double);  break;
                case decimal @decimal: binaryWriter.Write((byte)InlinedValueType.Decimal); binaryWriter.Write(@decimal); break;
                case string @string:   binaryWriter.Write((byte)InlinedValueType.String);  binaryWriter.Write(@string);  break;
                case var value when value.GetType().IsEnum:
                    {
                        binaryWriter.Write((byte)InlinedValueType.Enum);
                        binaryWriter.Write(value.GetType().FullName!);
                        binaryWriter.Write(value.ToString()!);
                        break;
                    }
                case var value when value.GetType().IsUnmanaged():
                    {
                        var size = Marshal.SizeOf(value);
                        var buffer = new byte[size];
                        var pointer = IntPtr.Zero;

                        // convert struct to byte buffer
                        try
                        {
                            pointer = Marshal.AllocHGlobal(size);
                            Marshal.StructureToPtr(value, pointer, true);
                            Marshal.Copy(pointer, buffer, 0, size);
                        }
                        finally
                        {
                            if (pointer != IntPtr.Zero)
                                Marshal.FreeHGlobal(pointer);
                        }

                        // write to stream
                        binaryWriter.Write((byte)InlinedValueType.Unmanaged);
                        binaryWriter.Write(value.GetType().FullName!);
                        binaryWriter.Write(size);
                        binaryWriter.Write(buffer);
                        break;
                    }
                default: throw new ArgumentException("Isn't a type that can normally be serialised.", nameof(@object));
            }
        }
    }
}

using NovaEngine.Content.Models;
using NovaEngine.Core;
using NovaEngine.Core.Components;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using NovaEngine.SceneManagement;
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

        /// <summary>Writes a <see cref="Scene"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="scene">The <see cref="GameObject"/> to write.</param>
        public static void Write(this BinaryWriter writer, Scene scene)
        {
            // name
            writer.Write(scene.Name);

            // game objects
            writer.Write(scene.RootGameObjects.Count);
            foreach (var gameObject in scene.RootGameObjects)
                writer.Write(gameObject);
        }

        /// <summary>Writes a <see cref="GameObject"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="gameObject">The <see cref="GameObject"/> to write.</param>
        public static void Write(this BinaryWriter writer, GameObject gameObject)
        {
            // name
            writer.Write(gameObject.Name);

            // isEnabled
            writer.Write(gameObject.IsEnabled);

            // transform
            writer.Write(gameObject.Transform.LocalPosition);
            writer.Write(gameObject.Transform.LocalRotation);
            writer.Write(gameObject.Transform.LocalScale);

            // components
            writer.Write(gameObject.Components.Count);
            foreach (var component in gameObject.Components)
            {
                writer.Write(component.GetType().FullName!);

                // TODO: temp, don't hardcode just the mesh renderer to be loaded through the content pipeline
                if (component is MeshRenderer meshRenderer)
                {
                    writer.Write(false);
                    writer.Write("Models/Cubes"); // TODO: store the file in the mesh perhaps?
                    writer.Write(meshRenderer.Mesh.Guid.ToString());
                }
                else
                    writer.Write(true);
            }

            // children
            writer.Write(gameObject.Children.Count);
            foreach (var child in gameObject.Children)
                writer.Write(child);
        }

        /// <summary>Writes a <see cref="ModelContent"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="modelContent">The <see cref="ModelContent"/> to write.</param>
        public static void Write(this BinaryWriter writer, ModelContent modelContent)
        {
            // write meshes
            writer.Write(modelContent.Meshes.Count);
            foreach (var mesh in modelContent.Meshes)
                writer.Write(mesh);
        }

        /// <summary>Writes a <see cref="MeshContent"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="meshContent">The <see cref="MeshContent"/> to write.</param>
        public static void Write(this BinaryWriter writer, MeshContent meshContent)
        {
            // guid + name
            writer.Write(meshContent.Guid.ToString());
            writer.Write(meshContent.Name);

            // vertex data
            writer.Write(meshContent.Vertices.Count);
            foreach (var vertex in meshContent.Vertices)
                writer.Write(vertex);

            // index data
            writer.Write(meshContent.Indices.Count);
            foreach (var index in meshContent.Indices)
                writer.Write(index);
        }

        /// <summary>Writes a <see cref="Vertex"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="vertex">The <see cref="Vertex"/> to write.</param>
        public static void Write(this BinaryWriter writer, Vertex vertex)
        {
            writer.Write(vertex.Position);
            writer.Write(vertex.TextureCoordinates);
            writer.Write(vertex.Normal);
        }

        /// <summary>Writes a <see cref="Vector3"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="vector">The <see cref="Vector3"/> to write.</param>
        public static void Write(this BinaryWriter writer, Vector3 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        /// <summary>Writes a <see cref="Vector2"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="vector">The <see cref="Vector2"/> to write.</param>
        public static void Write(this BinaryWriter writer, Vector2 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

        /// <summary>Writes a <see cref="Quaternion"/> to the current stream.</summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="quaternion">The <see cref="Quaternion"/> to write.</param>
        public static void Write(this BinaryWriter writer, Quaternion quaternion)
        {
            writer.Write(quaternion.X);
            writer.Write(quaternion.Y);
            writer.Write(quaternion.Z);
            writer.Write(quaternion.W);
        }
    }
}

using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a vector with four 32-bit integer values.</summary>
    public struct Vector4I : IEquatable<Vector4I>
    {
        /*********
        ** Fields
        *********/
        /// <summary>The X component of the vector.</summary>
        public int X;

        /// <summary>The Y component of the vector.</summary>
        public int Y;

        /// <summary>The Z component of the vector.</summary>
        public int Z;

        /// <summary>The W component of the vector.</summary>
        public int W;


        /*********
        ** Accessors
        *********/
        /// <summary>The length of the vector.</summary>
        public readonly float Length => MathF.Sqrt(LengthSquared);

        /// <summary>The squared length of the vector.</summary>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public readonly int LengthSquared => X * X + Y * Y + Z * Z + W * W;

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, and <see cref="Z"/> components.</summary>
        public Vector4I XYWZ
        {
            readonly get => new(X, Y, W, Z);
            set
            {
                X = value.X;
                Y = value.Y;
                W = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, and <see cref="W"/> components.</summary>
        public Vector4I XZYW
        {
            readonly get => new(X, Z, Y, W);
            set
            {
                X = value.X;
                Z = value.Y;
                Y = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, and <see cref="Y"/> components.</summary>
        public Vector4I XZWY
        {
            readonly get => new(X, Z, W, Y);
            set
            {
                X = value.X;
                Z = value.Y;
                W = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
        public Vector4I XWYZ
        {
            readonly get => new(X, W, Y, Z);
            set
            {
                X = value.X;
                W = value.Y;
                Y = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
        public Vector4I XWZY
        {
            readonly get => new(X, W, Z, Y);
            set
            {
                X = value.X;
                W = value.Y;
                Z = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, and <see cref="W"/> components.</summary>
        public Vector4I YXZW
        {
            readonly get => new(Y, X, Z, W);
            set
            {
                Y = value.X;
                X = value.Y;
                Z = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, and <see cref="Z"/> components.</summary>
        public Vector4I YXWZ
        {
            readonly get => new(Y, X, W, Z);
            set
            {
                Y = value.X;
                X = value.Y;
                W = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
        public Vector4I YZXW
        {
            readonly get => new(Y, Z, X, W);
            set
            {
                Y = value.X;
                Z = value.Y;
                X = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, and <see cref="X"/> components.</summary>
        public Vector4I YZWX
        {
            readonly get => new(Y, Z, W, X);
            set
            {
                Y = value.X;
                Z = value.Y;
                W = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, and <see cref="Z"/> components.</summary>
        public Vector4I YWXZ
        {
            readonly get => new(Y, W, X, Z);
            set
            {
                Y = value.X;
                W = value.Y;
                X = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector4I YWZX
        {
            readonly get => new(Y, W, Z, X);
            set
            {
                Y = value.X;
                W = value.Y;
                Z = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, and <see cref="W"/> components.</summary>
        public Vector4I ZXYW
        {
            readonly get => new(Z, X, Y, W);
            set
            {
                Z = value.X;
                X = value.Y;
                Y = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, and <see cref="Y"/> components.</summary>
        public Vector4I ZXWY
        {
            readonly get => new(Z, X, W, Y);
            set
            {
                Z = value.X;
                X = value.Y;
                W = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
        public Vector4I ZYXW
        {
            readonly get => new(Z, Y, X, W);
            set
            {
                Z = value.X;
                Y = value.Y;
                X = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, and <see cref="X"/> components.</summary>
        public Vector4I ZYWX
        {
            readonly get => new(Z, Y, W, X);
            set
            {
                Z = value.X;
                Y = value.Y;
                W = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, and <see cref="Y"/> components.</summary>
        public Vector4I ZWXY
        {
            readonly get => new(Z, W, X, Y);
            set
            {
                Z = value.X;
                W = value.Y;
                X = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, and <see cref="X"/> components.</summary>
        public Vector4I ZWYX
        {
            readonly get => new(Z, W, Y, X);
            set
            {
                Z = value.X;
                W = value.Y;
                Y = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
        public Vector4I WXYZ
        {
            readonly get => new(W, X, Y, Z);
            set
            {
                W = value.X;
                X = value.Y;
                Y = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
        public Vector4I WXZY
        {
            readonly get => new(W, X, Z, Y);
            set
            {
                W = value.X;
                X = value.Y;
                Z = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, and <see cref="Z"/> components.</summary>
        public Vector4I WYXZ
        {
            readonly get => new(W, Y, X, Z);
            set
            {
                W = value.X;
                Y = value.Y;
                X = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector4I WYZX
        {
            readonly get => new(W, Y, Z, X);
            set
            {
                W = value.X;
                Y = value.Y;
                Z = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, and <see cref="Y"/> components.</summary>
        public Vector4I WZXY
        {
            readonly get => new(W, Z, X, Y);
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, and <see cref="X"/> components.</summary>
        public Vector4I WZYX
        {
            readonly get => new(W, Z, Y, X);
            set
            {
                W = value.X;
                Z = value.Y;
                Y = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
        public Vector3I XYZ
        {
            readonly get => new(X, Y, Z);
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Y"/>, and <see cref="W"/> components.</summary>
        public Vector3I XYW
        {
            readonly get => new(X, Y, W);
            set
            {
                X = value.X;
                Y = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
        public Vector3I XZY
        {
            readonly get => new(X, Z, Y);
            set
            {
                X = value.X;
                Z = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, and <see cref="W"/> components.</summary>
        public Vector3I XZW
        {
            readonly get => new(X, Z, W);
            set
            {
                X = value.X;
                Z = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="W"/>, and <see cref="Y"/> components.</summary>
        public Vector3I XWY
        {
            readonly get => new(X, W, Y);
            set
            {
                X = value.X;
                W = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="W"/>, and <see cref="Z"/> components.</summary>
        public Vector3I XWZ
        {
            readonly get => new(X, W, Z);
            set
            {
                X = value.X;
                W = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="X"/>, and <see cref="Z"/> components.</summary>
        public Vector3I YXZ
        {
            readonly get => new(Y, X, Z);
            set
            {
                Y = value.X;
                X = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
        public Vector3I YXW
        {
            readonly get => new(Y, X, W);
            set
            {
                Y = value.X;
                X = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector3I YZX
        {
            readonly get => new(Y, Z, X);
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, and <see cref="W"/> components.</summary>
        public Vector3I YZW
        {
            readonly get => new(Y, Z, W);
            set
            {
                Y = value.X;
                Z = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="W"/>, and <see cref="X"/> components.</summary>
        public Vector3I YWX
        {
            readonly get => new(Y, W, X);
            set
            {
                Y = value.X;
                W = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="W"/>, and <see cref="Z"/> components.</summary>
        public Vector3I YWZ
        {
            readonly get => new(Y, W, Z);
            set
            {
                Y = value.X;
                W = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="X"/>, and <see cref="Y"/> components.</summary>
        public Vector3I ZXY
        {
            readonly get => new(Z, X, Y);
            set
            {
                Z = value.X;
                X = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
        public Vector3I ZXW
        {
            readonly get => new(Z, X, W);
            set
            {
                Z = value.X;
                X = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="Y"/>, and <see cref="X"/> components.</summary>
        public Vector3I ZYX
        {
            readonly get => new(Z, Y, X);
            set
            {
                Z = value.X;
                Y = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="Y"/>, and <see cref="W"/> components.</summary>
        public Vector3I ZYW
        {
            readonly get => new(Z, Y, W);
            set
            {
                Z = value.X;
                Y = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="X"/>, and <see cref="Y"/> components.</summary>
        public Vector3I WXY
        {
            readonly get => new(W, X, Y);
            set
            {
                W = value.X;
                X = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="X"/>, and <see cref="Z"/> components.</summary>
        public Vector3I WXZ
        {
            readonly get => new(W, X, Z);
            set
            {
                W = value.X;
                X = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Y"/>, and <see cref="X"/> components.</summary>
        public Vector3I WYX
        {
            readonly get => new(W, Y, X);
            set
            {
                W = value.X;
                Y = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
        public Vector3I WYZ
        {
            readonly get => new(W, Y, Z);
            set
            {
                W = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector3I WZX
        {
            readonly get => new(W, Z, X);
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
        public Vector3I WZY
        {
            readonly get => new(W, Z, Y);
            set
            {
                W = value.X;
                Z = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/> and <see cref="Y"/> components.</summary>
        public Vector2I XY
        {
            readonly get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="X"/> and <see cref="Z"/> components.</summary>
        public Vector2I XZ
        {
            readonly get => new(X, Z);
            set
            {
                X = value.X;
                Z = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="X"/> and <see cref="W"/> components.</summary>
        public Vector2I XW
        {
            readonly get => new(X, W);
            set
            {
                X = value.X;
                W = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/> and <see cref="X"/> components.</summary>
        public Vector2I YX
        {
            readonly get => new(Y, X);
            set
            {
                Y = value.X;
                X = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/> and <see cref="Z"/> components.</summary>
        public Vector2I YZ
        {
            readonly get => new(Y, Z);
            set
            {
                Y = value.X;
                Z = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/> and <see cref="W"/> components.</summary>
        public Vector2I YW
        {
            readonly get => new(Y, W);
            set
            {
                Y = value.X;
                W = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/> and <see cref="X"/> components.</summary>
        public Vector2I ZX
        {
            readonly get => new(Z, X);
            set
            {
                Z = value.X;
                X = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/> and <see cref="Y"/> components.</summary>
        public Vector2I ZY
        {
            readonly get => new(Z, Y);
            set
            {
                Z = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/> and <see cref="W"/> components.</summary>
        public Vector2I ZW
        {
            readonly get => new(Z, W);
            set
            {
                Z = value.X;
                W = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="W"/> and <see cref="X"/> components.</summary>
        public Vector2I WX
        {
            readonly get => new(W, X);
            set
            {
                W = value.X;
                X = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="W"/> and <see cref="Y"/> components.</summary>
        public Vector2I WY
        {
            readonly get => new(W, Y);
            set
            {
                W = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="W"/> and <see cref="Z"/> components.</summary>
        public Vector2I WZ
        {
            readonly get => new(W, Z);
            set
            {
                W = value.X;
                Z = value.Y;
            }
        }

        /// <summary>Gets a vector with (X, Y, Z, W) = (0, 0, 0, 0).</summary>
        public static Vector4I Zero => new(0);

        /// <summary>Gets a vector with (X, Y, Z, W) = (1, 1, 1, 1).</summary>
        public static Vector4I One => new(1);

        /// <summary>Gets a vector with (X, Y, Z, W) = (1, 0, 0, 0).</summary>
        public static Vector4I UnitX => new(1, 0, 0, 0);

        /// <summary>Gets a vector with (X, Y, Z, W) = (0, 1, 0, 0).</summary>
        public static Vector4I UnitY => new(0, 1, 0, 0);

        /// <summary>Gets a vector with (X, Y, Z, W) = (0, 0, 1, 0).</summary>
        public static Vector4I UnitZ => new(0, 0, 1, 0);

        /// <summary>Gets a vector with (X, Y, Z, W) = (0, 0, 0, 1).</summary>
        public static Vector4I UnitW => new(0, 0, 0, 1);

        /// <summary>Gets or sets the value at a specified position.</summary>
        /// <param name="index">The position index.</param>
        /// <returns>The value at the specified position.</returns>
        public int this[int index]
        {
            readonly get
            {
                if (index == 0)
                    return X;
                else if (index == 1)
                    return Y;
                else if (index == 2)
                    return Z;
                else if (index == 3)
                    return W;
                else
                    throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 3 (inclusive)");
            }
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                else if (index == 2)
                    Z = value;
                else if (index == 3)
                    W = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 3 (inclusive)");
            }
        }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="value">The X, Y, Z, and W components of the vector.</param>
        public Vector4I(int value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        /// <param name="w">The W component of the vector.</param>
        public Vector4I(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="xy">The X and Y components of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        /// <param name="w">The W component of the vector.</param>
        public Vector4I(in Vector2I xy, int z = 0, int w = 0)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="xy">The X and Y components of the vector.</param>
        /// <param name="zw">The Z and W components of the vector.</param>
        public Vector4I(in Vector2I xy, in Vector2I zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="xyz">The X, Y, and Z components of the vector.</param>
        /// <param name="w">The W component of the vector.</param>
        public Vector4I(in Vector3I xyz, int w = 0)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }

        /// <summary>Gets the vector as a <see cref="Vector4"/>.</summary>
        /// <returns>The vector as a <see cref="Vector4"/>.</returns>
        public readonly Vector4 ToVector4() => new(X, Y, Z, W);

        /// <summary>Gets the vector as a <see cref="Vector4D"/>.</summary>
        /// <returns>The vector as a <see cref="Vector4D"/>.</returns>
        public readonly Vector4D ToVector4D() => new(X, Y, Z, W);

        /// <inheritdoc/>
        public readonly bool Equals(Vector4I other) => this == other;

        /// <inheritdoc/>
        public readonly override bool Equals(object? obj) => obj is Vector4I vector && this == vector;

        /// <inheritdoc/>
        public readonly override int GetHashCode() => (X, Y, Z, W).GetHashCode();

        /// <inheritdoc/>
        public readonly override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}, W: {W}>";

        /// <summary>Calculates the distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static float Distance(in Vector4 vector1, in Vector4 vector2) => MathF.Sqrt(Vector4.DistanceSquared(vector1, vector2));

        /// <summary>Calculates the sqaured distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The squared distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public static int DistanceSquared(in Vector4I vector1, in Vector4I vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y) + (vector2.Z - vector1.Z) * (vector2.Z - vector1.Z) + (vector2.W - vector1.W) * (vector2.W - vector1.W);

        /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector4I ComponentMin(in Vector4I vector1, in Vector4I vector2) => new(Math.Min(vector1.X, vector2.X), Math.Min(vector1.Y, vector2.Y), Math.Min(vector1.Z, vector2.Z), Math.Min(vector1.W, vector2.W));

        /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector4I ComponentMax(in Vector4I vector1, in Vector4I vector2) => new(Math.Max(vector1.X, vector2.X), Math.Max(vector1.Y, vector2.Y), Math.Max(vector1.Z, vector2.Z), Math.Max(vector1.W, vector2.W));

        /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector4I Clamp(in Vector4I value, in Vector4I min, in Vector4I max) => new(Math.Clamp(value.X, min.X, max.X), Math.Clamp(value.Y, min.Y, max.Y), Math.Clamp(value.Z, min.Z, max.Z), Math.Clamp(value.W, min.W, max.W));


        /*********
        ** Operators
        *********/
        /// <summary>Adds a vector and a value.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector4I operator +(Vector4I left, int right) => new(left.X + right, left.Y + right, left.Z + right, left.W + right);

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector4I operator +(Vector4I left, Vector4I right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        /// <summary>Subtracts a value from a vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector4I operator -(Vector4I left, int right) => new(left.X - right, left.Y - right, left.Z - right, left.W - right);

        /// <summary>Subtracts a vector from another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector4I operator -(Vector4I left, Vector4I right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        /// <summary>Flips the sign of each component of a vector.</summary>
        /// <param name="vector">The vector to flip the component signs of.</param>
        /// <returns><paramref name="vector"/> with the sign of its components flipped.</returns>
        public static Vector4I operator -(Vector4I vector) => vector * -1;

        /// <summary>Multiplies a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector4I operator *(Vector4I left, int right) => new(left.X * right, left.Y * right, left.Z * right, left.W * right);

        /// <summary>Multiplies two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector4I operator *(Vector4I left, Vector4I right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

        /// <summary>Checks two vectors for equality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector4I vector1, Vector4I vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z && vector1.W == vector2.W;

        /// <summary>Checks two vectors for inequality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector4I vector1, Vector4I vector2) => !(vector1 == vector2);

        /// <summary>Converts a vector to a tuple.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator (int X, int Y, int Z, int W)(Vector4I vector) => (vector.X, vector.Y, vector.Z, vector.W);

        /// <summary>Converts a tuple to a vector.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator Vector4I((int X, int Y, int Z, int W) tuple) => new(tuple.X, tuple.Y, tuple.Z, tuple.W);

        /// <summary>Converts a <see cref="Vector4I"/> to a <see cref="Vector4"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator Vector4(Vector4I vector) => vector.ToVector4();

        /// <summary>Converts a <see cref="Vector4I"/> to a <see cref="Vector4"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator Vector4D(Vector4I vector) => vector.ToVector4D();
    }
}

using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a vector with four double-precision floating-point values.</summary>
    public struct Vector4D : IEquatable<Vector4D>
    {
        /*********
        ** Fields
        *********/
        /// <summary>The X component of the vector.</summary>
        public double X;

        /// <summary>The Y component of the vector.</summary>
        public double Y;

        /// <summary>The Z component of the vector.</summary>
        public double Z;

        /// <summary>The W component of the vector.</summary>
        public double W;


        /*********
        ** Accessors
        *********/
        /// <summary>The length of the vector.</summary>
        public double Length => Math.Sqrt(LengthSquared);

        /// <summary>The squared length of the vector.</summary>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public double LengthSquared => X * X + Y * Y + Z * Z + W * W;

        /// <summary>The vector with unit length.</summary>
        public Vector4D Normalised
        {
            get
            {
                var vector = this;
                vector.Normalise();
                return vector;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, and <see cref="Z"/> components.</summary>
        public Vector4D XYWZ
        {
            get => new(X, Y, W, Z);
            set
            {
                X = value.X;
                Y = value.Y;
                W = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, and <see cref="W"/> components.</summary>
        public Vector4D XZYW
        {
            get => new(X, Z, Y, W);
            set
            {
                X = value.X;
                Z = value.Y;
                Y = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, and <see cref="Y"/> components.</summary>
        public Vector4D XZWY
        {
            get => new(X, Z, W, Y);
            set
            {
                X = value.X;
                Z = value.Y;
                W = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
        public Vector4D XWYZ
        {
            get => new(X, W, Y, Z);
            set
            {
                X = value.X;
                W = value.Y;
                Y = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
        public Vector4D XWZY
        {
            get => new(X, W, Z, Y);
            set
            {
                X = value.X;
                W = value.Y;
                Z = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, and <see cref="W"/> components.</summary>
        public Vector4D YXZW
        {
            get => new(Y, X, Z, W);
            set
            {
                Y = value.X;
                X = value.Y;
                Z = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, and <see cref="Z"/> components.</summary>
        public Vector4D YXWZ
        {
            get => new(Y, X, W, Z);
            set
            {
                Y = value.X;
                X = value.Y;
                W = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
        public Vector4D YZXW
        {
            get => new(Y, Z, X, W);
            set
            {
                Y = value.X;
                Z = value.Y;
                X = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, and <see cref="X"/> components.</summary>
        public Vector4D YZWX
        {
            get => new(Y, Z, W, X);
            set
            {
                Y = value.X;
                Z = value.Y;
                W = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, and <see cref="Z"/> components.</summary>
        public Vector4D YWXZ
        {
            get => new(Y, W, X, Z);
            set
            {
                Y = value.X;
                W = value.Y;
                X = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector4D YWZX
        {
            get => new(Y, W, Z, X);
            set
            {
                Y = value.X;
                W = value.Y;
                Z = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, and <see cref="W"/> components.</summary>
        public Vector4D ZXYW
        {
            get => new(Z, X, Y, W);
            set
            {
                Z = value.X;
                X = value.Y;
                Y = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, and <see cref="Y"/> components.</summary>
        public Vector4D ZXWY
        {
            get => new(Z, X, W, Y);
            set
            {
                Z = value.X;
                X = value.Y;
                W = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
        public Vector4D ZYXW
        {
            get => new(Z, Y, X, W);
            set
            {
                Z = value.X;
                Y = value.Y;
                X = value.Z;
                W = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, and <see cref="X"/> components.</summary>
        public Vector4D ZYWX
        {
            get => new(Z, Y, W, X);
            set
            {
                Z = value.X;
                Y = value.Y;
                W = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, and <see cref="Y"/> components.</summary>
        public Vector4D ZWXY
        {
            get => new(Z, W, X, Y);
            set
            {
                Z = value.X;
                W = value.Y;
                X = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, and <see cref="X"/> components.</summary>
        public Vector4D ZWYX
        {
            get => new(Z, W, Y, X);
            set
            {
                Z = value.X;
                W = value.Y;
                Y = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
        public Vector4D WXYZ
        {
            get => new(W, X, Y, Z);
            set
            {
                W = value.X;
                X = value.Y;
                Y = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
        public Vector4D WXZY
        {
            get => new(W, X, Z, Y);
            set
            {
                W = value.X;
                X = value.Y;
                Z = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, and <see cref="Z"/> components.</summary>
        public Vector4D WYXZ
        {
            get => new(W, Y, X, Z);
            set
            {
                W = value.X;
                Y = value.Y;
                X = value.Z;
                Z = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector4D WYZX
        {
            get => new(W, Y, Z, X);
            set
            {
                W = value.X;
                Y = value.Y;
                Z = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, and <see cref="Y"/> components.</summary>
        public Vector4D WZXY
        {
            get => new(W, Z, X, Y);
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
                Y = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, and <see cref="X"/> components.</summary>
        public Vector4D WZYX
        {
            get => new(W, Z, Y, X);
            set
            {
                W = value.X;
                Z = value.Y;
                Y = value.Z;
                X = value.W;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
        public Vector3D XYZ
        {
            get => new(X, Y, Z);
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Y"/>, and <see cref="W"/> components.</summary>
        public Vector3D XYW
        {
            get => new(X, Y, W);
            set
            {
                X = value.X;
                Y = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
        public Vector3D XZY
        {
            get => new(X, Z, Y);
            set
            {
                X = value.X;
                Z = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, and <see cref="W"/> components.</summary>
        public Vector3D XZW
        {
            get => new(X, Z, W);
            set
            {
                X = value.X;
                Z = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="W"/>, and <see cref="Y"/> components.</summary>
        public Vector3D XWY
        {
            get => new(X, W, Y);
            set
            {
                X = value.X;
                W = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="W"/>, and <see cref="Z"/> components.</summary>
        public Vector3D XWZ
        {
            get => new(X, W, Z);
            set
            {
                X = value.X;
                W = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="X"/>, and <see cref="Z"/> components.</summary>
        public Vector3D YXZ
        {
            get => new(Y, X, Z);
            set
            {
                Y = value.X;
                X = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
        public Vector3D YXW
        {
            get => new(Y, X, W);
            set
            {
                Y = value.X;
                X = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector3D YZX
        {
            get => new(Y, Z, X);
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, and <see cref="W"/> components.</summary>
        public Vector3D YZW
        {
            get => new(Y, Z, W);
            set
            {
                Y = value.X;
                Z = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="W"/>, and <see cref="X"/> components.</summary>
        public Vector3D YWX
        {
            get => new(Y, W, X);
            set
            {
                Y = value.X;
                W = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="W"/>, and <see cref="Z"/> components.</summary>
        public Vector3D YWZ
        {
            get => new(Y, W, Z);
            set
            {
                Y = value.X;
                W = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="X"/>, and <see cref="Y"/> components.</summary>
        public Vector3D ZXY
        {
            get => new(Z, X, Y);
            set
            {
                Z = value.X;
                X = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
        public Vector3D ZXW
        {
            get => new(Z, X, W);
            set
            {
                Z = value.X;
                X = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="Y"/>, and <see cref="X"/> components.</summary>
        public Vector3D ZYX
        {
            get => new(Z, Y, X);
            set
            {
                Z = value.X;
                Y = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/>, <see cref="Y"/>, and <see cref="W"/> components.</summary>
        public Vector3D ZYW
        {
            get => new(Z, Y, W);
            set
            {
                Z = value.X;
                Y = value.Y;
                W = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="X"/>, and <see cref="Y"/> components.</summary>
        public Vector3D WXY
        {
            get => new(W, X, Y);
            set
            {
                W = value.X;
                X = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="X"/>, and <see cref="Z"/> components.</summary>
        public Vector3D WXZ
        {
            get => new(W, X, Z);
            set
            {
                W = value.X;
                X = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Y"/>, and <see cref="X"/> components.</summary>
        public Vector3D WYX
        {
            get => new(W, Y, X);
            set
            {
                W = value.X;
                Y = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
        public Vector3D WYZ
        {
            get => new(W, Y, Z);
            set
            {
                W = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector3D WZX
        {
            get => new(W, Z, X);
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="W"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
        public Vector3D WZY
        {
            get => new(W, Z, Y);
            set
            {
                W = value.X;
                Z = value.Y;
                Y = value.Z;
            }
        }

        /// <summary>Swizzle the <see cref="X"/> and <see cref="Y"/> components.</summary>
        public Vector2D XY
        {
            get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="X"/> and <see cref="Z"/> components.</summary>
        public Vector2D XZ
        {
            get => new(X, Z);
            set
            {
                X = value.X;
                Z = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="X"/> and <see cref="W"/> components.</summary>
        public Vector2D XW
        {
            get => new(X, W);
            set
            {
                X = value.X;
                W = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/> and <see cref="X"/> components.</summary>
        public Vector2D YX
        {
            get => new(Y, X);
            set
            {
                Y = value.X;
                X = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/> and <see cref="Z"/> components.</summary>
        public Vector2D YZ
        {
            get => new(Y, Z);
            set
            {
                Y = value.X;
                Z = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/> and <see cref="W"/> components.</summary>
        public Vector2D YW
        {
            get => new(Y, W);
            set
            {
                Y = value.X;
                W = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/> and <see cref="X"/> components.</summary>
        public Vector2D ZX
        {
            get => new(Z, X);
            set
            {
                Z = value.X;
                X = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/> and <see cref="Y"/> components.</summary>
        public Vector2D ZY
        {
            get => new(Z, Y);
            set
            {
                Z = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/> and <see cref="W"/> components.</summary>
        public Vector2D ZW
        {
            get => new(Z, W);
            set
            {
                Z = value.X;
                W = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="W"/> and <see cref="X"/> components.</summary>
        public Vector2D WX
        {
            get => new(W, X);
            set
            {
                W = value.X;
                X = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="W"/> and <see cref="Y"/> components.</summary>
        public Vector2D WY
        {
            get => new(W, Y);
            set
            {
                W = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="W"/> and <see cref="Z"/> components.</summary>
        public Vector2D WZ
        {
            get => new(W, Z);
            set
            {
                W = value.X;
                Z = value.Y;
            }
        }

        /// <summary>Gets a vector with (X, Y, Z, W) = (0, 0, 0, 0).</summary>
        public static Vector4D Zero => new(0);

        /// <summary>Gets a vector with (X, Y, Z, W) = (1, 1, 1, 1).</summary>
        public static Vector4D One => new(1);

        /// <summary>Gets a vector with (X, Y, Z, W) = (1, 0, 0, 0).</summary>
        public static Vector4D UnitX => new(1, 0, 0, 0);

        /// <summary>Gets a vector with (X, Y, Z, W) = (0, 1, 0, 0).</summary>
        public static Vector4D UnitY => new(0, 1, 0, 0);

        /// <summary>Gets a vector with (X, Y, Z, W) = (0, 0, 1, 0).</summary>
        public static Vector4D UnitZ => new(0, 0, 1, 0);

        /// <summary>Gets a vector with (X, Y, Z, W) = (0, 0, 0, 1).</summary>
        public static Vector4D UnitW => new(0, 0, 0, 1);

        /// <summary>Gets or sets the value at a specified position.</summary>
        /// <param name="index">The position index.</param>
        /// <returns>The value at the specified position.</returns>
        public double this[int index]
        {
            get
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
        public Vector4D(double value)
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
        public Vector4D(double x, double y, double z, double w)
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
        public Vector4D(Vector2D xy, double z = 0, double w = 0)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="xy">The X and Y components of the vector.</param>
        /// <param name="zw">The Z and W components of the vector.</param>
        public Vector4D(Vector2D xy, Vector2D zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="xyz">The X, Y, and Z components of the vector.</param>
        /// <param name="w">The W component of the vector.</param>
        public Vector4D(Vector3D xyz, double w = 0)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }

        /// <summary>Converts the vector to unit length.</summary>
        public void Normalise()
        {
            var scale = 1 / Length;
            X *= scale;
            Y *= scale;
            Z *= scale;
            W *= scale;
        }

        /// <summary>Gets the vector as a <see cref="Vector4"/>.</summary>
        /// <returns>The vector as a <see cref="Vector4"/>.</returns>
        public Vector4 ToVector4() => new((float)X, (float)Y, (float)Z, (float)W);

        /// <summary>Converts the vector to a <see cref="Vector4I"/> by rounding the components down.</summary>
        /// <returns>The rounded down vector as a <see cref="Vector4I"/>.</returns>
        public Vector4I ToFlooredVector4I() => new((int)Math.Floor(X), (int)Math.Floor(Y), (int)Math.Floor(Z), (int)Math.Floor(W));

        /// <summary>Converts the vector to a <see cref="Vector4I"/> by rounding the components.</summary>
        /// <returns>The rounded vector as a <see cref="Vector4I"/>.</returns>
        public Vector4I ToRoundedVector4I() => new((int)Math.Round(X), (int)Math.Round(Y), (int)Math.Round(Z), (int)Math.Round(W));

        /// <summary>Converts the vector to a <see cref="Vector4I"/> by rounding the components up.</summary>
        /// <returns>The rounded up vector as a <see cref="Vector4I"/>.</returns>
        public Vector4I ToCeilingedVector4I() => new((int)Math.Ceiling(X), (int)Math.Ceiling(Y), (int)Math.Ceiling(Z), (int)Math.Ceiling(W));

        /// <inheritdoc/>
        public bool Equals(Vector4D other) => this == other;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Vector4D vector && this == vector;

        /// <inheritdoc/>
        public override int GetHashCode() => (X, Y, Z, W).GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}, W: {W}>";

        /// <summary>Calculates the distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static double Distance(Vector4D vector1, Vector4D vector2) => Math.Sqrt(Vector4D.DistanceSquared(vector1, vector2));

        /// <summary>Calculates the sqaured distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The squared distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public static double DistanceSquared(Vector4D vector1, Vector4D vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y) + (vector2.Z - vector1.Z) * (vector2.Z - vector1.Z) + (vector2.W - vector1.W) * (vector2.W - vector1.W);

        /// <summary>Calculates the dot product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The dot product of <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static double Dot(Vector4D vector1, Vector4D vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;

        /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector4D ComponentMin(Vector4D vector1, Vector4D vector2) => new(Math.Min(vector1.X, vector2.X), Math.Min(vector1.Y, vector2.Y), Math.Min(vector1.Z, vector2.Z), Math.Min(vector1.W, vector2.W));

        /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector4D ComponentMax(Vector4D vector1, Vector4D vector2) => new(Math.Max(vector1.X, vector2.X), Math.Max(vector1.Y, vector2.Y), Math.Max(vector1.Z, vector2.Z), Math.Max(vector1.W, vector2.W));

        /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector4D Clamp(Vector4D value, Vector4D min, Vector4D max) => new(MathsHelper.Clamp(value.X, min.X, max.X), MathsHelper.Clamp(value.Y, min.Y, max.Y), MathsHelper.Clamp(value.Z, min.Z, max.Z), MathsHelper.Clamp(value.W, min.W, max.W));

        /// <summary>Linearly interpolates between two values.</summary>
        /// <param name="value1">The source value.</param>
        /// <param name="value2">The destination value.</param>
        /// <param name="amount">The amount to interpolate between <paramref name="value1"/> and <paramref name="value2"/>.</param>
        /// <returns>The interpolated value.</returns>
        public static Vector4D Lerp(Vector4D value1, Vector4D value2, double amount) => new(MathsHelper.Lerp(value1.X, value2.X, amount), MathsHelper.Lerp(value1.Y, value2.Y, amount), MathsHelper.Lerp(value1.Z, value2.Z, amount), MathsHelper.Lerp(value1.W, value2.W, amount));

        /// <summary>Transforms a vector by a matrix.</summary>
        /// <param name="position">The source position.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4D Transform(Vector2D position, Matrix4x4D matrix)
        {
            return new(
                x: position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41,
                y: position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42,
                z: position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43,
                w: position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44
            );
        }

        /// <summary>Transforms a vector by a rotation.</summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The rotated vector.</returns>
        public static Vector4D Transform(Vector2D vector, QuaternionD rotation)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var xz2 = rotation.X * z2;
            var yy2 = rotation.Y * y2;
            var yz2 = rotation.Y * z2;
            var zz2 = rotation.Z * z2;
            var wx2 = rotation.W * x2;
            var wy2 = rotation.W * y2;
            var wz2 = rotation.W * z2;

            return new(
                x: vector.X * (1 - yy2 - zz2) + vector.Y * (xy2 - wz2),
                y: vector.X * (xy2 + wz2) + vector.Y * (1 - xx2 - zz2),
                z: vector.X * (xz2 - wy2) + vector.Y * (yz2 + wx2),
                w: 1
            );
        }

        /// <summary>Transforms a vector by a matrix.</summary>
        /// <param name="position">The source position.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4D Transform(Vector3D position, Matrix4x4D matrix)
        {
            return new(
                x: position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41,
                y: position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42,
                z: position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43,
                w: position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44
            );
        }

        /// <summary>Transforms a vector by a rotation.</summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The rotated vector.</returns>
        public static Vector4D Transform(Vector3D vector, QuaternionD rotation)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var xz2 = rotation.X * z2;
            var yy2 = rotation.Y * y2;
            var yz2 = rotation.Y * z2;
            var zz2 = rotation.Z * z2;
            var wx2 = rotation.W * x2;
            var wy2 = rotation.W * y2;
            var wz2 = rotation.W * z2;

            return new(
                x: vector.X * (1 - yy2 - zz2) + vector.Y * (xy2 - wz2) + vector.Z * (xz2 + wy2),
                y: vector.X * (xy2 + wz2) + vector.Y * (1 - xx2 - zz2) + vector.Z * (yz2 - wx2),
                z: vector.X * (xz2 - wy2) + vector.Y * (yz2 + wx2) + vector.Z * (1 - xx2 - yy2),
                w: 1
            );
        }

        /// <summary>Transforms a vector by a matrix.</summary>
        /// <param name="position">The source position.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4D Transform(Vector4D position, Matrix4x4D matrix)
        {
            return new(
                x: position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + position.W * matrix.M41,
                y: position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + position.W * matrix.M42,
                z: position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + position.W * matrix.M43,
                w: position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + position.W * matrix.M44
            );
        }

        /// <summary>Transforms a vector by a rotation.</summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The rotated vector.</returns>
        public static Vector4D Transform(Vector4D vector, QuaternionD rotation)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var xz2 = rotation.X * z2;
            var yy2 = rotation.Y * y2;
            var yz2 = rotation.Y * z2;
            var zz2 = rotation.Z * z2;
            var wx2 = rotation.W * x2;
            var wy2 = rotation.W * y2;
            var wz2 = rotation.W * z2;

            return new(
                x: vector.X * (1 - yy2 - zz2) + vector.Y * (xy2 - wz2) + vector.Z * (xz2 + wy2),
                y: vector.X * (xy2 + wz2) + vector.Y * (1 - xx2 - zz2) + vector.Z * (yz2 - wx2),
                z: vector.X * (xz2 - wy2) + vector.Y * (yz2 + wx2) + vector.Z * (1 - xx2 - yy2),
                w: vector.W
            );
        }


        /*********
        ** Operators
        *********/
        /// <summary>Adds a vector and a value.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector4D operator +(Vector4D left, double right) => new(left.X + right, left.Y + right, left.Z + right, left.W + right);

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector4D operator +(Vector4D left, Vector4D right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        /// <summary>Subtracts a value from a vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector4D operator -(Vector4D left, double right) => new(left.X - right, left.Y - right, left.Z - right, left.W - right);

        /// <summary>Subtracts a vector from another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector4D operator -(Vector4D left, Vector4D right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        /// <summary>Flips the sign of each component of a vector.</summary>
        /// <param name="vector">The vector to flip the component signs of.</param>
        /// <returns><paramref name="vector"/> with the sign of its components flipped.</returns>
        public static Vector4D operator -(Vector4D vector) => vector * -1;

        /// <summary>Multiplies a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector4D operator *(Vector4D left, double right) => new(left.X * right, left.Y * right, left.Z * right, left.W * right);

        /// <summary>Multiplies two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector4D operator *(Vector4D left, Vector4D right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

        /// <summary>Divides a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the division.</returns>
        public static Vector4D operator /(Vector4D left, double right) => new(left.X / right, left.Y / right, left.Z / right, left.W / right);

        /// <summary>Divides a vector by another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the division.</returns>
        public static Vector4D operator /(Vector4D left, Vector4D right) => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

        /// <summary>Checks two vectors for equality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector4D vector1, Vector4D vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z && vector1.W == vector2.W;

        /// <summary>Checks two vectors for inequality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector4D vector1, Vector4D vector2) => !(vector1 == vector2);

        /// <summary>Converts a vector to a tuple.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator (double X, double Y, double Z, double W)(Vector4D vector) => (vector.X, vector.Y, vector.Z, vector.W);

        /// <summary>Converts a tuple to a vector.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator Vector4D((double X, double Y, double Z, double W) tuple) => new(tuple.X, tuple.Y, tuple.Z, tuple.W);

        /// <summary>Converts a <see cref="Vector4D"/> to a <see cref="Vector4"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static explicit operator Vector4(Vector4D vector) => vector.ToVector4();

        /// <summary>Converts a <see cref="Vector4D"/> to a <see cref="Vector4I"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        /// <remarks>This floors the vector components, to be consistant with explicit <see langword="float"/> to <see langword="int"/> conversion.</remarks>
        public static explicit operator Vector4I(Vector4D vector) => vector.ToFlooredVector4I();
    }
}

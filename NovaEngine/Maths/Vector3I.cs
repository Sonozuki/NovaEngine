using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a vector with three 32-bit integer values.</summary>
    public struct Vector3I : IEquatable<Vector3I>
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


        /*********
        ** Accessors
        *********/
        /// <summary>The length of the vector.</summary>
        public readonly float Length => MathF.Sqrt(LengthSquared);

        /// <summary>The squared length of the vector.</summary>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public readonly int LengthSquared => X * X + Y * Y + Z * Z;

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

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector3I YZX
        {
            readonly get => new(Y, Z, X);
            set
            {
                Y = value.X;
                Z = value.Y;
                X = value.Z;
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

        /// <summary>Gets a vector with (X, Y, Z) = (0, 0, 0).</summary>
        public static Vector3I Zero => new(0);

        /// <summary>Gets a vector with (X, Y, Z) = (1, 1, 1).</summary>
        public static Vector3I One => new(1);

        /// <summary>Gets a vector with (X, Y, Z) = (1, 0, 0).</summary>
        public static Vector3I UnitX => new(1, 0, 0);

        /// <summary>Gets a vector with (X, Y, Z) = (0, 1, 0).</summary>
        public static Vector3I UnitY => new(0, 1, 0);

        /// <summary>Gets a vector with (X, Y, Z) = (0, 0, 1).</summary>
        public static Vector3I UnitZ => new(0, 0, 1);

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
                else
                    throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 2 (inclusive)");
            }
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                else if (index == 2)
                    Z = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 2 (inclusive)");
            }
        }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="value">The X, Y, and Z components of the vector.</param>
        public Vector3I(int value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        public Vector3I(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="xy">The X and Y components of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        public Vector3I(in Vector2I xy, int z = 0)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        /// <summary>Gets the vector as a <see cref="Vector3"/>.</summary>
        /// <returns>The vector as a <see cref="Vector3"/>.</returns>
        public readonly Vector3 ToVector3() => new(X, Y, Z);

        /// <summary>Gets the vector as a <see cref="Vector3D"/>.</summary>
        /// <returns>The vector as a <see cref="Vector3D"/>.</returns>
        public readonly Vector3D ToVector3D() => new(X, Y, Z);

        /// <inheritdoc/>
        public readonly bool Equals(Vector3I other) => this == other;

        /// <inheritdoc/>
        public readonly override bool Equals(object? obj) => obj is Vector3I vector && this == vector;

        /// <inheritdoc/>
        public readonly override int GetHashCode() => (X, Y, Z).GetHashCode();

        /// <inheritdoc/>
        public readonly override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}>";

        /// <summary>Calculates the distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static float Distance(in Vector3I vector1, in Vector3I vector2) => MathF.Sqrt(Vector3I.DistanceSquared(vector1, vector2));

        /// <summary>Calculates the sqaured distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The squared distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public static float DistanceSquared(in Vector3I vector1, in Vector3I vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y) + (vector2.Z - vector1.Z) * (vector2.Z - vector1.Z);

        /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector3I ComponentMin(in Vector3I vector1, in Vector3I vector2) => new(Math.Min(vector1.X, vector2.X), Math.Min(vector1.Y, vector2.Y), Math.Min(vector1.Z, vector2.Z));

        /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector3I ComponentMax(in Vector3I vector1, in Vector3I vector2) => new(Math.Max(vector1.X, vector2.X), Math.Max(vector1.Y, vector2.Y), Math.Max(vector1.Z, vector2.Z));

        /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector3I Clamp(in Vector3I value, in Vector3I min, in Vector3I max) => new(Math.Clamp(value.X, min.X, max.X), Math.Clamp(value.Y, min.Y, max.Y), Math.Clamp(value.Z, min.Z, max.Z));


        /*********
        ** Operators
        *********/
        /// <summary>Adds a vector and a value.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector3I operator +(Vector3I left, int right) => new(left.X + right, left.Y + right, left.Z + right);

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector3I operator +(Vector3I left, Vector3I right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        /// <summary>Subtracts a value from a vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector3I operator -(Vector3I left, int right) => new(left.X - right, left.Y - right, left.Z - right);

        /// <summary>Subtracts a vector from another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector3I operator -(Vector3I left, Vector3I right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        /// <summary>Flips the sign of each component of a vector.</summary>
        /// <param name="vector">The vector to flip the component signs of.</param>
        /// <returns><paramref name="vector"/> with the sign of its components flipped.</returns>
        public static Vector3I operator -(Vector3I vector) => vector * -1;

        /// <summary>Multiplies a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector3I operator *(Vector3I left, int right) => new(left.X * right, left.Y * right, left.Z * right);

        /// <summary>Multiplies two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector3I operator *(Vector3I left, Vector3I right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        /// <summary>Checks two vectors for equality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector3I vector1, Vector3I vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;

        /// <summary>Checks two vectors for inequality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector3I vector1, Vector3I vector2) => !(vector1 == vector2);

        /// <summary>Converts a vector to a tuple.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator (int X, int Y, int Z)(Vector3I vector) => (vector.X, vector.Y, vector.Z);

        /// <summary>Converts a tuple to a vector.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator Vector3I((int X, int Y, int Z) tuple) => new(tuple.X, tuple.Y, tuple.Z);

        /// <summary>Converts a <see cref="Vector3I"/> to a <see cref="Vector3"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator Vector3(Vector3I vector) => vector.ToVector3();

        /// <summary>Converts a <see cref="Vector3I"/> to a <see cref="Vector3"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator Vector3D(Vector3I vector) => vector.ToVector3D();
    }
}
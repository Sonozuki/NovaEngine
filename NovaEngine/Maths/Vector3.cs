using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a vector with three single-precision floating-point values.</summary>
    public struct Vector3 : IEquatable<Vector3>
    {
        /*********
        ** Fields
        *********/
        /// <summary>The X component of the vector.</summary>
        public float X;

        /// <summary>The Y component of the vector.</summary>
        public float Y;

        /// <summary>The Z component of the vector.</summary>
        public float Z;


        /*********
        ** Accessors
        *********/
        /// <summary>The length of the vector.</summary>
        public readonly float Length => MathF.Sqrt(LengthSquared);

        /// <summary>The sqaured length of the vector.</summary>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public readonly float LengthSquared => X * X + Y * Y + Z * Z;

        /// <summary>The vector with unit length.</summary>
        public readonly Vector3 Normalised
        {
            get
            {
                var vector = this;
                vector.Normalise();
                return vector;
            }
        }

        /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
        public Vector3 XZY
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
        public Vector3 YXZ
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
        public Vector3 YZX
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
        public Vector3 ZXY
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
        public Vector3 ZYX
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
        public Vector2 XY
        {
            readonly get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/> and <see cref="X"/> components.</summary>
        public Vector2 YX
        {
            readonly get => new(Y, X);
            set
            {
                Y = value.X;
                X = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="X"/> and <see cref="Z"/> components.</summary>
        public Vector2 XZ
        {
            readonly get => new(X, Z);
            set
            {
                X = value.X;
                Z = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/> and <see cref="X"/> components.</summary>
        public Vector2 ZX
        {
            readonly get => new(Z, X);
            set
            {
                Z = value.X;
                X = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/> and <see cref="Z"/> components.</summary>
        public Vector2 YZ
        {
            readonly get => new(Y, Z);
            set
            {
                Y = value.X;
                Z = value.Y;
            }
        }

        /// <summary>Swizzle the <see cref="Z"/> and <see cref="Y"/> components.</summary>
        public Vector2 ZY
        {
            readonly get => new(Z, Y);
            set
            {
                Z = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Gets a vector with (X, Y, Z) = (0, 0, 0).</summary>
        public static Vector3 Zero => new(0);

        /// <summary>Gets a vector with (X, Y, Z) = (1, 1, 1).</summary>
        public static Vector3 One => new(1);

        /// <summary>Gets a vector with (X, Y, Z) = (1, 0, 0).</summary>
        public static Vector3 UnitX => new(1, 0, 0);

        /// <summary>Gets a vector with (X, Y, Z) = (0, 1, 0).</summary>
        public static Vector3 UnitY => new(0, 1, 0);

        /// <summary>Gets a vector with (X, Y, Z) = (0, 0, 1).</summary>
        public static Vector3 UnitZ => new(0, 0, 1);

        /// <summary>Gets or sets the value at a specified position.</summary>
        /// <param name="index">The position index.</param>
        /// <returns>The value at the specified position.</returns>
        public float this[int index]
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
        public Vector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="xy">The X and Y components of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        public Vector3(in Vector2 xy, float z = 0)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        /// <summary>Converts the vector to unit length.</summary>
        public void Normalise()
        {
            var scale = 1 / Length;
            X *= scale;
            Y *= scale;
            Z *= scale;
        }

        /// <summary>Gets the vector as a <see cref="Vector3D"/>.</summary>
        /// <returns>The vector as a <see cref="Vector3D"/>.</returns>
        public readonly Vector3D ToVector3D() => new(X, Y, Z);

        /// <summary>Gets the vector as a <see cref="Vector3I"/> by rounding the components down.</summary>
        /// <returns>The rounded down vector as a <see cref="Vector3I"/>.</returns>
        public readonly Vector3I ToFlooredVector3I() => new((int)MathF.Floor(X), (int)MathF.Floor(Y), (int)MathF.Floor(Z));

        /// <summary>Gets the vector as a <see cref="Vector3I"/> by rounding the components.</summary>
        /// <returns>The rounded vector as a <see cref="Vector3I"/>.</returns>
        public readonly Vector3I ToRoundedVector3I() => new((int)MathF.Round(X), (int)MathF.Round(Y), (int)MathF.Round(Z));

        /// <summary>Gets the vector as a <see cref="Vector3I"/> by rounding the components up.</summary>
        /// <returns>The rounded up vector as a <see cref="Vector3I"/>.</returns>
        public readonly Vector3I ToCeilingedVector3I() => new((int)MathF.Ceiling(X), (int)MathF.Ceiling(Y), (int)MathF.Ceiling(Z));

        /// <inheritdoc/>
        public readonly bool Equals(Vector3 other) => this == other;

        /// <inheritdoc/>
        public readonly override bool Equals(object? obj) => obj is Vector3 vector && this == vector;

        /// <inheritdoc/>
        public readonly override int GetHashCode() => (X, Y, Z).GetHashCode();

        /// <inheritdoc/>
        public readonly override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}>";

        /// <summary>Calculates the distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static float Distance(in Vector3 vector1, in Vector3 vector2) => MathF.Sqrt(Vector3.DistanceSquared(vector1, vector2));

        /// <summary>Calculates the sqaured distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The squared distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public static float DistanceSquared(in Vector3 vector1, in Vector3 vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y) + (vector2.Z - vector1.Z) * (vector2.Z - vector1.Z);

        /// <summary>Calculates the dot product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The dot product of <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static float Dot(in Vector3 vector1, in Vector3 vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;

        /// <summary>Calculates the cross product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product of <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static Vector3 Cross(in Vector3 vector1, in Vector3 vector2) => new(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);

        /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector3 ComponentMin(in Vector3 vector1, in Vector3 vector2) => new(Math.Min(vector1.X, vector2.X), Math.Min(vector1.Y, vector2.Y), Math.Min(vector1.Z, vector2.Z));

        /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector3 ComponentMax(in Vector3 vector1, in Vector3 vector2) => new(Math.Max(vector1.X, vector2.X), Math.Max(vector1.Y, vector2.Y), Math.Max(vector1.Z, vector2.Z));

        /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector3 Clamp(in Vector3 value, in Vector3 min, in Vector3 max) => new(MathsHelper.Clamp(value.X, min.X, max.X), MathsHelper.Clamp(value.Y, min.Y, max.Y), MathsHelper.Clamp(value.Z, min.Z, max.Z));

        /// <summary>Linearly interpolates between two values.</summary>
        /// <param name="value1">The source value.</param>
        /// <param name="value2">The destination value.</param>
        /// <param name="amount">The amount to interpolate between <paramref name="value1"/> and <paramref name="value2"/>.</param>
        /// <returns>The interpolated value.</returns>
        public static Vector3 Lerp(in Vector3 value1, in Vector3 value2, float amount) => new(MathsHelper.Lerp(value1.X, value2.X, amount), MathsHelper.Lerp(value1.Y, value2.Y, amount), MathsHelper.Lerp(value1.Z, value2.Z, amount));

        /// <summary>Transforms a vector by a matrix.</summary>
        /// <param name="position">The source position.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector3 Transform(in Vector3 position, in Matrix4x4 matrix)
        {
            return new(
                x: position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41,
                y: position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42,
                z: position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43
            );
        }

        /// <summary>Transforms a vector by a rotation.</summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The rotated vector.</returns>
        public static Vector3 Transform(in Vector3 vector, in Quaternion rotation)
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
                vector.X * (1 - yy2 - zz2) + vector.Y * (xy2 - wz2) + vector.Z * (xz2 + wy2),
                vector.X * (xy2 + wz2) + vector.Y * (1 - xx2 - zz2) + vector.Z * (yz2 - wx2),
                vector.X * (xz2 - wy2) + vector.Y * (yz2 + wx2) + vector.Z * (1 - xx2 - yy2)
            );
        }

        /// <summary>Transforms a vector normal by a matrix.</summary>
        /// <param name="normal">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector3 TransformNormal(in Vector3 normal, in Matrix4x4 matrix)
        {
            return new(
                x: normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31,
                y: normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32,
                z: normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33
            );
        }


        /*********
        ** Operators
        *********/
        /// <summary>Adds a vector and a value.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector3 operator +(Vector3 left, float right) => new(left.X + right, left.Y + right, left.Z + right);

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector3 operator +(Vector3 left, Vector3 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        /// <summary>Subtracts a value from a vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector3 operator -(Vector3 left, float right) => new(left.X - right, left.Y - right, left.Z - right);

        /// <summary>Subtracts a vector from another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector3 operator -(Vector3 left, Vector3 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        /// <summary>Flips the sign of each component of a vector.</summary>
        /// <param name="vector">The vector to flip the component signs of.</param>
        /// <returns><paramref name="vector"/> with the sign of its components flipped.</returns>
        public static Vector3 operator -(Vector3 vector) => vector * -1;

        /// <summary>Multiplies a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector3 operator *(Vector3 left, float right) => new(left.X * right, left.Y * right, left.Z * right);

        /// <summary>Multiplies two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector3 operator *(Vector3 left, Vector3 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        /// <summary>Multiplies a vector by a quaternion.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector3 operator *(Vector3 left, Quaternion right)
        {
            var vector = right.XYZ;
            return left + ((Vector3.Cross(vector, left) * right.W) + Vector3.Cross(vector, Vector3.Cross(vector, left))) * 2;
        }

        /// <summary>Divides a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the division.</returns>
        public static Vector3 operator /(Vector3 left, float right) => new(left.X / right, left.Y / right, left.Z / right);

        /// <summary>Divides a vector by another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the division.</returns>
        public static Vector3 operator /(Vector3 left, Vector3 right) => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        /// <summary>Checks two vectors for equality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector3 vector1, Vector3 vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;

        /// <summary>Checks two vectors for inequality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector3 vector1, Vector3 vector2) => !(vector1 == vector2);

        /// <summary>Converts a vector to a tuple.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator (float X, float Y, float Z)(Vector3 vector) => (vector.X, vector.Y, vector.Z);

        /// <summary>Converts a tuple to a vector.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator Vector3((float X, float Y, float Z) tuple) => new(tuple.X, tuple.Y, tuple.Z);

        /// <summary>Converts a <see cref="Vector3"/> to a <see cref="Vector3D"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator Vector3D(Vector3 vector) => vector.ToVector3D();

        /// <summary>Converts a <see cref="Vector3"/> to a <see cref="Vector3I"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        /// <remarks>This floors the vector components, to be consistant with explicit <see langword="float"/> to <see langword="int"/> conversion.</remarks>
        public static explicit operator Vector3I(Vector3 vector) => vector.ToFlooredVector3I();
    }
}

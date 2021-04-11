﻿using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a vector with three double-precision floating-point values.</summary>
    public struct Vector3D : IEquatable<Vector3D>
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


        /*********
        ** Accessors
        *********/
        /// <summary>The length of the vector.</summary>
        public double Length => Math.Sqrt(LengthSquared);

        /// <summary>The sqaured length of the vector.</summary>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public double LengthSquared => X * X + Y * Y + Z * Z;

        /// <summary>The vector with unit length.</summary>
        public Vector3D Normalised
        {
            get
            {
                var vector = this;
                vector.Normalise();
                return vector;
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

        /// <summary>Swizzle the <see cref="Y"/>, <see cref="Z"/>, and <see cref="X"/> components.</summary>
        public Vector3D YZX
        {
            get => new(Y, Z, X);
            set
            {
                Y = value.X;
                Z = value.Y;
                X = value.Z;
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

        /// <summary>Gets a vector with (X, Y, Z) = (0, 0, 0).</summary>
        public static Vector3D Zero => new(0);

        /// <summary>Gets a vector with (X, Y, Z) = (1, 1, 1).</summary>
        public static Vector3D One => new(1);

        /// <summary>Gets a vector with (X, Y, Z) = (1, 0, 0).</summary>
        public static Vector3D UnitX => new(1, 0, 0);

        /// <summary>Gets a vector with (X, Y, Z) = (0, 1, 0).</summary>
        public static Vector3D UnitY => new(0, 1, 0);

        /// <summary>Gets a vector with (X, Y, Z) = (0, 0, 1).</summary>
        public static Vector3D UnitZ => new(0, 0, 1);

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
        public Vector3D(double value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="xy">The X and Y components of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        public Vector3D(Vector2D xy, double z = 0)
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

        /// <summary>Gets the vector as a <see cref="Vector2"/>.</summary>
        /// <returns>The vector as a <see cref="Vector2"/>.</returns>
        public Vector3 ToVector3() => new((float)X, (float)Y, (float)Z);

        /// <summary>Gets the vector as a <see cref="Vector3I"/> by rounding the components down.</summary>
        /// <returns>The rounded down vector as a <see cref="Vector3I"/>.</returns>
        public Vector3I ToFlooredVector3I() => new((int)Math.Floor(X), (int)Math.Floor(Y), (int)Math.Floor(Z));

        /// <summary>Gets the vector as a <see cref="Vector3I"/> by rounding the components.</summary>
        /// <returns>The rounded vector as a <see cref="Vector3I"/>.</returns>
        public Vector3I ToRoundedVector3I() => new((int)Math.Round(X), (int)Math.Round(Y), (int)Math.Round(Z));

        /// <summary>Gets the vector as a <see cref="Vector3I"/> by rounding the components up.</summary>
        /// <returns>The rounded up vector as a <see cref="Vector32I"/>.</returns>
        public Vector3I ToCeilingedVector3I() => new((int)Math.Ceiling(X), (int)Math.Ceiling(Y), (int)Math.Ceiling(Z));

        /// <inheritdoc/>
        public bool Equals(Vector3D other) => this == other;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Vector3D vector && this == vector;

        /// <inheritdoc/>
        public override int GetHashCode() => (X, Y, Z).GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}>";

        /// <summary>Calculates the distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static double Distance(Vector3D vector1, Vector3D vector2) => Math.Sqrt(Vector3D.DistanceSquared(vector1, vector2));

        /// <summary>Calculates the sqaured distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The squared distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public static double DistanceSquared(Vector3D vector1, Vector3D vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y) + (vector2.Z - vector1.Z) * (vector2.Z - vector1.Z);

        /// <summary>Calculates the dot product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The dot product of <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static double Dot(Vector3D vector1, Vector3D vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;

        /// <summary>Calculates the cross product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product of <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static Vector3D Cross(Vector3D vector1, Vector3D vector2) => new(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);

        /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector3D ComponentMin(Vector3D vector1, Vector3D vector2) => new(Math.Min(vector1.X, vector2.X), Math.Min(vector1.Y, vector2.Y), Math.Min(vector1.Z, vector2.Z));

        /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector3D ComponentMax(Vector3D vector1, Vector3D vector2) => new(Math.Max(vector1.X, vector2.X), Math.Max(vector1.Y, vector2.Y), Math.Max(vector1.Z, vector2.Z));

        /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector3D Clamp(Vector3D value, Vector3D min, Vector3D max) => new(MathsHelper.Clamp(value.X, min.X, max.X), MathsHelper.Clamp(value.Y, min.Y, max.Y), MathsHelper.Clamp(value.Z, min.Z, max.Z));

        /// <summary>Linearly interpolates between two values.</summary>
        /// <param name="value1">The source value.</param>
        /// <param name="value2">The destination value.</param>
        /// <param name="amount">The amount to interpolate between <paramref name="value1"/> and <paramref name="value2"/>.</param>
        /// <returns>The interpolated value.</returns>
        public static Vector3D Lerp(Vector3D value1, Vector3D value2, double amount) => new(MathsHelper.Lerp(value1.X, value2.X, amount), MathsHelper.Lerp(value1.Y, value2.Y, amount), MathsHelper.Lerp(value1.Z, value2.Z, amount));

        /// <summary>Transforms a vector by a matrix.</summary>
        /// <param name="position">The source position.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector3D Transform(Vector3D position, Matrix4x4D matrix)
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
        public static Vector3D Transform(Vector3D vector, QuaternionD rotation)
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
        public static Vector3D TransformNormal(Vector3D normal, Matrix4x4D matrix)
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
        public static Vector3D operator +(Vector3D left, double right) => new(left.X + right, left.Y + right, left.Z + right);

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector3D operator +(Vector3D left, Vector3D right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        /// <summary>Subtracts a value from a vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector3D operator -(Vector3D left, double right) => new(left.X - right, left.Y - right, left.Z - right);

        /// <summary>Subtracts a vector from another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector3D operator -(Vector3D left, Vector3D right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        /// <summary>Flips the sign of each component of a vector.</summary>
        /// <param name="vector">The vector to flip the component signs of.</param>
        /// <returns><paramref name="vector"/> with the sign of its components flipped.</returns>
        public static Vector3D operator -(Vector3D vector) => vector * -1;

        /// <summary>Multiplies a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector3D operator *(Vector3D left, double right) => new(left.X * right, left.Y * right, left.Z * right);

        /// <summary>Multiplies two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector3D operator *(Vector3D left, Vector3D right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        /// <summary>Divides a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the division.</returns>
        public static Vector3D operator /(Vector3D left, double right) => new(left.X / right, left.Y / right, left.Z / right);

        /// <summary>Divides a vector by another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the division.</returns>
        public static Vector3D operator /(Vector3D left, Vector3D right) => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        /// <summary>Checks two vectors for equality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector3D vector1, Vector3D vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;

        /// <summary>Checks two vectors for inequality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector3D vector1, Vector3D vector2) => !(vector1 == vector2);

        /// <summary>Converts a vector to a tuple.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator (double X, double Y, double Z)(Vector3D vector) => (vector.X, vector.Y, vector.Z);

        /// <summary>Converts a tuple to a vector.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator Vector3D((double X, double Y, double Z) tuple) => new(tuple.X, tuple.Y, tuple.Z);

        /// <summary>Converts a <see cref="Vector3D"/> to a <see cref="Vector3"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static explicit operator Vector3(Vector3D vector) => vector.ToVector3();

        /// <summary>Converts a <see cref="Vector3D"/> to a <see cref="Vector3I"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        /// <remarks>This floors the vector components, to be consistant with explicit <see langword="float"/> to <see langword="int"/> conversion.</remarks>
        public static explicit operator Vector3I(Vector3D vector) => vector.ToFlooredVector3I();
    }
}

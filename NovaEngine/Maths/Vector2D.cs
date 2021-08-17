﻿using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a vector with two double-precision floating-point values.</summary>
    public struct Vector2D : IEquatable<Vector2D>
    {
        /*********
        ** Fields
        *********/
        /// <summary>The X component of the vector.</summary>
        public double X;

        /// <summary>The Y component of the vector.</summary>
        public double Y;


        /*********
        ** Accessors
        *********/
        /// <summary>The length of the vector.</summary>
        public readonly double Length => Math.Sqrt(LengthSquared);

        /// <summary>The squared length of the vector.</summary>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public readonly double LengthSquared => X * X + Y * Y;

        /// <summary>The perpendicular vector to the left of this vector.</summary>
        public readonly Vector2D PerpendicularLeft => new(-Y, X);

        /// <summary>The perpendicular vector to the right of this vector.</summary>
        public readonly Vector2D PerpendicularRight => new(Y, -X);

        /// <summary>The vector with unit length.</summary>
        public readonly Vector2D Normalised
        {
            get
            {
                var vector = this;
                vector.Normalise();
                return vector;
            }
        }

        /// <summary>Swizzle the <see cref="Y"/> and <see cref="X"/> components.</summary>
        public Vector2D YX
        {
            readonly get => new(Y, X);
            set
            {
                Y = value.X;
                X = value.Y;
            }
        }

        /// <summary>Gets a vector with (X, Y) = (0, 0).</summary>
        public static Vector2D Zero => new(0);

        /// <summary>Gets a vector with (X, Y) = (1, 1).</summary>
        public static Vector2D One => new(1);

        /// <summary>Gets a vector with (X, Y) = (1, 0).</summary>
        public static Vector2D UnitX => new(1, 0);

        /// <summary>Gets a vector with (X, Y) = (0, 1).</summary>
        public static Vector2D UnitY => new(0, 1);

        /// <summary>Gets or sets the value at a specified position.</summary>
        /// <param name="index">The position index.</param>
        /// <returns>The value at the specified position.</returns>
        public double this[int index]
        {
            readonly get
            {
                if (index == 0)
                    return X;
                else if (index == 1)
                    return Y;
                else
                    throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 1 (inclusive)");
            }
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 1 (inclusive)");
            }
        }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="value">The X and Y components of the vector.</param>
        public Vector2D(double value)
        {
            X = value;
            Y = value;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Converts the vector to unit length.</summary>
        public void Normalise()
        {
            var scale = 1 / Length;
            X *= scale;
            Y *= scale;
        }

        /// <summary>Gets the vector as a <see cref="Vector2"/>.</summary>
        /// <returns>The vector as a <see cref="Vector2"/>.</returns>
        public readonly Vector2 ToVector2() => new((float)X, (float)Y);

        /// <summary>Gets the vector as a <see cref="Vector2I"/> by rounding the components down.</summary>
        /// <returns>The rounded down vector as a <see cref="Vector2I"/>.</returns>
        public readonly Vector2I ToFlooredVector2I() => new((int)Math.Floor(X), (int)Math.Floor(Y));

        /// <summary>Gets the vector as a <see cref="Vector2I"/> by rounding the components.</summary>
        /// <returns>The rounded vector as a <see cref="Vector2I"/>.</returns>
        public readonly Vector2I ToRoundedVector2I() => new((int)Math.Round(X), (int)Math.Round(Y));

        /// <summary>Gets the vector as a <see cref="Vector2I"/> by rounding the components up.</summary>
        /// <returns>The rounded up vector as a <see cref="Vector2I"/>.</returns>
        public readonly Vector2I ToCeilingedVector2I() => new((int)Math.Ceiling(X), (int)Math.Ceiling(Y));

        /// <inheritdoc/>
        public readonly bool Equals(Vector2D other) => this == other;

        /// <inheritdoc/>
        public readonly override bool Equals(object? obj) => obj is Vector2D vector && this == vector;

        /// <inheritdoc/>
        public readonly override int GetHashCode() => (X, Y).GetHashCode();

        /// <inheritdoc/>
        public readonly override string ToString() => $"<X: {X}, Y: {Y}>";

        /// <summary>Calculates the distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static double Distance(in Vector2D vector1, in Vector2D vector2) => Math.Sqrt(Vector2D.DistanceSquared(vector1, vector2));

        /// <summary>Calculates the sqaured distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The squared distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public static double DistanceSquared(in Vector2D vector1, in Vector2D vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y);

        /// <summary>Calculates the dot product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The dot product of <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static double Dot(in Vector2D vector1, in Vector2D vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y;

        /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector2D ComponentMin(in Vector2D vector1, in Vector2D vector2) => new(Math.Min(vector1.X, vector2.X), Math.Min(vector1.Y, vector2.Y));

        /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector2D ComponentMax(in Vector2D vector1, in Vector2D vector2) => new(Math.Max(vector1.X, vector2.X), Math.Max(vector1.Y, vector2.Y));

        /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector2D Clamp(in Vector2D value, in Vector2D min, in Vector2D max) => new(MathsHelper.Clamp(value.X, min.X, max.X), MathsHelper.Clamp(value.Y, min.Y, max.Y));

        /// <summary>Linearly interpolates between two values.</summary>
        /// <param name="value1">The source value.</param>
        /// <param name="value2">The destination value.</param>
        /// <param name="amount">The amount to interpolate between <paramref name="value1"/> and <paramref name="value2"/>.</param>
        /// <returns>The interpolated value.</returns>
        public static Vector2D Lerp(in Vector2D value1, in Vector2D value2, double amount) => new(MathsHelper.Lerp(value1.X, value2.X, amount), MathsHelper.Lerp(value1.Y, value2.Y, amount));

        /// <summary>Transforms a vector by a rotation.</summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The rotated vector.</returns>
        public static Vector2D Transform(in Vector2D vector, in QuaternionD rotation)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var wz2 = rotation.W * z2;
            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var yy2 = rotation.Y * y2;
            var zz2 = rotation.Z * z2;

            return new(
                x: vector.X * (1 - yy2 - zz2) + vector.Y * (xy2 - wz2),
                y: vector.X * (xy2 + wz2) + vector.Y * (1 - xx2 - zz2)
            );
        }


        /*********
        ** Operators
        *********/
        /// <summary>Adds a vector and a value.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector2D operator +(Vector2D left, double right) => new(left.X + right, left.Y + right);

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector2D operator +(Vector2D left, Vector2D right) => new(left.X + right.X, left.Y + right.Y);

        /// <summary>Subtracts a value from a vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector2D operator -(Vector2D left, double right) => new(left.X - right, left.Y - right);

        /// <summary>Subtracts a vector from another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector2D operator -(Vector2D left, Vector2D right) => new(left.X - right.X, left.Y - right.Y);

        /// <summary>Flips the sign of each component of a vector.</summary>
        /// <param name="vector">The vector to flip the component signs of.</param>
        /// <returns><paramref name="vector"/> with the sign of its components flipped.</returns>
        public static Vector2D operator -(Vector2D vector) => vector * -1;

        /// <summary>Multiplies a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector2D operator *(Vector2D left, double right) => new(left.X * right, left.Y * right);

        /// <summary>Multiplies two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector2D operator *(Vector2D left, Vector2D right) => new(left.X * right.X, left.Y * right.Y);

        /// <summary>Divides a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the division.</returns>
        public static Vector2D operator /(Vector2D left, double right) => new(left.X / right, left.Y / right);

        /// <summary>Divides a vector by another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the division.</returns>
        public static Vector2D operator /(Vector2D left, Vector2D right) => new(left.X / right.X, left.Y / right.Y);

        /// <summary>Checks two vectors for equality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector2D vector1, Vector2D vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y;

        /// <summary>Checks two vectors for inequality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector2D vector1, Vector2D vector2) => !(vector1 == vector2);

        /// <summary>Converts a vector to a tuple.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator (double X, double Y)(Vector2D vector) => (vector.X, vector.Y);

        /// <summary>Converts a tuple to a vector.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator Vector2D((double X, double Y) tuple) => new(tuple.X, tuple.Y);

        /// <summary>Converts a <see cref="Vector2D"/> to a <see cref="Vector2"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static explicit operator Vector2(Vector2D vector) => vector.ToVector2();

        /// <summary>Converts a <see cref="Vector2D"/> to a <see cref="Vector2I"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        /// <remarks>This floors the vector components, to be consistant with explicit <see langword="float"/> to <see langword="int"/> conversion.</remarks>
        public static explicit operator Vector2I(Vector2D vector) => vector.ToFlooredVector2I();
    }
}

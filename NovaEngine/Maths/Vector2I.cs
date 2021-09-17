using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a vector with two 32-bit integer values.</summary>
    public struct Vector2I : IEquatable<Vector2I>
    {
        /*********
        ** Fields
        *********/
        /// <summary>The X component of the vector.</summary>
        public int X;

        /// <summary>The Y component of the vector.</summary>
        public int Y;


        /*********
        ** Accessors
        *********/
        /// <summary>The length of the vector.</summary>
        public readonly float Length => MathF.Sqrt(LengthSquared);

        /// <summary>The squared length of the vector.</summary>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public readonly int LengthSquared => X * X + Y * Y;

        /// <summary>The perpendicular vector to the left of this vector.</summary>
        public readonly Vector2I PerpendicularLeft => new(-Y, X);

        /// <summary>The perpendicular vector to the right of this vector.</summary>
        public readonly Vector2I PerpendicularRight => new(Y, -X);

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

        /// <summary>Gets a vector with (X, Y) = (0, 0).</summary>
        public static Vector2I Zero => new(0);

        /// <summary>Gets a vector with (X, Y) = (1, 1).</summary>
        public static Vector2I One => new(1);

        /// <summary>Gets a vector with (X, Y) = (1, 0).</summary>
        public static Vector2I UnitX => new(1, 0);

        /// <summary>Gets a vector with (X, Y) = (0, 1).</summary>
        public static Vector2I UnitY => new(0, 1);

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
        public Vector2I(int value)
        {
            X = value;
            Y = value;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        public Vector2I(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Gets the vector as a <see cref="Vector2"/>.</summary>
        /// <returns>The vector as a <see cref="Vector2"/>.</returns>
        public readonly Vector2 ToVector2() => new(X, Y);

        /// <summary>Gets the vector as a <see cref="Vector2D"/>.</summary>
        /// <returns>The vector as a <see cref="Vector2D"/>.</returns>
        public readonly Vector2D ToVector2D() => new(X, Y);

        /// <inheritdoc/>
        public readonly bool Equals(Vector2I other) => this == other;

        /// <inheritdoc/>
        public readonly override bool Equals(object? obj) => obj is Vector2I vector && this == vector;

        /// <inheritdoc/>
        public readonly override int GetHashCode() => (X, Y).GetHashCode();

        /// <inheritdoc/>
        public readonly override string ToString() => $"<X: {X}, Y: {Y}>";

        /// <summary>Calculates the distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        public static float Distance(in Vector2I vector1, in Vector2I vector2) => MathF.Sqrt(Vector2I.DistanceSquared(vector1, vector2));

        /// <summary>Calculates the squared distance between two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The squared distance between <paramref name="vector1"/> and <paramref name="vector2"/>.</returns>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public static int DistanceSquared(in Vector2I vector1, in Vector2I vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y);

        /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector2I ComponentMin(in Vector2I vector1, in Vector2I vector2) => new(Math.Min(vector1.X, vector2.X), Math.Min(vector1.Y, vector2.Y));

        /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector2I ComponentMax(in Vector2I vector1, in Vector2I vector2) => new(Math.Max(vector1.X, vector2.X), Math.Max(vector1.Y, vector2.Y));

        /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector2I Clamp(in Vector2I value, in Vector2I min, in Vector2I max) => new(Math.Clamp(value.X, min.X, max.X), Math.Clamp(value.Y, min.Y, max.Y));


        /*********
        ** Operators
        *********/
        /// <summary>Adds a vector and a value.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector2I operator +(Vector2I left, int right) => new(left.X + right, left.Y + right);

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Vector2I operator +(Vector2I left, Vector2I right) => new(left.X + right.X, left.Y + right.Y);

        /// <summary>Subtracts a value from a vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector2I operator -(Vector2I left, int right) => new(left.X - right, left.Y - right);

        /// <summary>Subtracts a vector from another vector.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Vector2I operator -(Vector2I left, Vector2I right) => new(left.X - right.X, left.Y - right.Y);

        /// <summary>Flips the sign of each component of a vector.</summary>
        /// <param name="vector">The vector to flip the component signs of.</param>
        /// <returns><paramref name="vector"/> with the sign of its components flipped.</returns>
        public static Vector2I operator -(Vector2I vector) => vector * -1;

        /// <summary>Multiplies a vector by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector2I operator *(Vector2I left, int right) => new(left.X * right, left.Y * right);

        /// <summary>Multiplies two vectors together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Vector2I operator *(Vector2I left, Vector2I right) => new(left.X * right.X, left.Y * right.Y);

        /// <summary>Checks two vectors for equality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector2I vector1, Vector2I vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y;

        /// <summary>Checks two vectors for inequality.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector2I vector1, Vector2I vector2) => !(vector1 == vector2);

        /// <summary>Converts a vector to a tuple.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator (int X, int Y)(Vector2I vector) => (vector.X, vector.Y);

        /// <summary>Converts a tuple to a vector.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator Vector2I((int X, int Y) tuple) => new(tuple.X, tuple.Y);

        /// <summary>Converts a <see cref="Vector2I"/> to a <see cref="Vector2"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator Vector2(Vector2I vector) => vector.ToVector2();

        /// <summary>Converts a <see cref="Vector2I"/> to a <see cref="Vector3"/>.</summary>
        /// <param name="vector">The vector to convert.</param>
        public static implicit operator Vector2D(Vector2I vector) => vector.ToVector2D();
    }
}

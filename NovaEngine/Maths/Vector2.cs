using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a vector with two floating-point values.</summary>
public struct Vector2<T> : IEquatable<Vector2<T>>, IComparable<Vector2<T>>
    where T : IFloatingPoint<T>, IRootFunctions<T>, ITrigonometricFunctions<T>, IConvertible
{
    /*********
    ** Fields
    *********/
    /// <summary>The X component of the vector.</summary>
    public T X;

    /// <summary>The Y component of the vector.</summary>
    public T Y;


    /*********
    ** Accessors
    *********/
    /// <summary>The length of the vector.</summary>
    public readonly T Length => T.Sqrt(LengthSquared);

    /// <summary>The squared length of the vector.</summary>
    /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
    public readonly T LengthSquared => X * X + Y * Y;

    /// <summary>The perpendicular vector to the left of this vector.</summary>
    public readonly Vector2<T> PerpendicularLeft => new(-Y, X);

    /// <summary>The perpendicular vector to the right of this vector.</summary>
    public readonly Vector2<T> PerpendicularRight => new(Y, -X);

    /// <summary>The vector with unit length.</summary>
    public readonly Vector2<T> Normalised
    {
        get
        {
            var vector = this;
            vector.Normalise();
            return vector;
        }
    }

    /// <summary>Swizzle the <see cref="Y"/> and <see cref="X"/> components.</summary>
    public Vector2<T> YX
    {
        readonly get => new(Y, X);
        set
        {
            Y = value.X;
            X = value.Y;
        }
    }

    /// <summary>A vector with (X, Y) = (0, 0).</summary>
    public static Vector2<T> Zero => new(T.Zero);

    /// <summary>A vector with (X, Y) = (1, 1).</summary>
    public static Vector2<T> One => new(T.One);

    /// <summary>A vector with (X, Y) = (1, 0).</summary>
    public static Vector2<T> UnitX => new(T.One, T.Zero);

    /// <summary>A vector with (X, Y) = (0, 1).</summary>
    public static Vector2<T> UnitY => new(T.Zero, T.One);

    /// <summary>Gets or sets the value at a specified position.</summary>
    /// <param name="index">The position index.</param>
    /// <returns>The value at the specified position.</returns>
    public T this[int index]
    {
        readonly get
        {
            if (index == 0)
                return X;
            else if (index == 1)
                return Y;
            else
                throw new ArgumentOutOfRangeException(nameof(index), $"Must be between 0 => 1 (inclusive)");
        }
        set
        {
            if (index == 0)
                X = value;
            else if (index == 1)
                Y = value;
            else
                throw new ArgumentOutOfRangeException(nameof(index), $"Must be between 0 => 1 (inclusive)");
        }
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="value">The X and Y components of the vector.</param>
    public Vector2(T value)
    {
        X = value;
        Y = value;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    public Vector2(T x, T y)
    {
        X = x;
        Y = y;
    }

    /// <summary>Scales the vector to unit length.</summary>
    public void Normalise()
    {
        if (LengthSquared == T.Zero)
            return;

        var scale = T.One / Length;
        X *= scale;
        Y *= scale;
    }

    /// <summary>Checks two vectors for equality.</summary>
    /// <param name="other">The vector to check equality with.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector2<T> other) => this == other;

    /// <summary>Compares two vectors to determine whether the current instance preceeds, follows, or appears at the same position in the sort order as the other vector.</summary>
    /// <param name="other">The vector to compare against.</param>
    /// <returns>
    /// <b>Less than zero</b>, if this instance preceeds <paramref name="other"/> in the sort order.<br/>
    /// <b>Zero</b>, if this instance appears in the same position as <paramref name="other"/> in the sort order.<br/>
    /// <b>Greater than zero</b>, if this instance follows <paramref name="other"/> in the sort order.
    /// </returns>
    public readonly int CompareTo(Vector2<T> other)
    {
        var difference = LengthSquared - other.LengthSquared;
        return (difference > T.Zero ? T.Ceiling(difference) : T.Floor(difference)).ToInt32(null);
    }

    /// <summary>Checks the vector and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the vector and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Vector2<T> vector && this == vector;

    /// <summary>Retrieves the hash code of the vector.</summary>
    /// <returns>The hash code of the vector.</returns>
    public readonly override int GetHashCode() => (X, Y).GetHashCode();

    /// <summary>Calculates a string representation of the vector.</summary>
    /// <returns>A string representation of the vector.</returns>
    public readonly override string ToString() => $"<X: {X}, Y: {Y}>";

    /// <summary>Calculates the angle between two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The angle between the vectors, in degrees.</returns>
    public static T Angle(in Vector2<T> vector1, in Vector2<T> vector2) => MathsHelper<T>.RadiansToDegrees(T.Acos(Dot(vector1, vector2) / (vector1.Length * vector2.Length)));

    /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
    /// <param name="value">The vector to clamp.</param>
    /// <param name="min">The minimum vector.</param>
    /// <param name="max">The maximum vector.</param>
    /// <returns>The clamped vector.</returns>
    public static Vector2<T> Clamp(in Vector2<T> value, in Vector2<T> min, in Vector2<T> max) => new(T.Clamp(value.X, min.X, max.X), T.Clamp(value.Y, min.Y, max.Y));

    /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise maximum.</returns>
    public static Vector2<T> ComponentMax(in Vector2<T> vector1, in Vector2<T> vector2) => new(T.Max(vector1.X, vector2.X), T.Max(vector1.Y, vector2.Y));

    /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise minimum.</returns>
    public static Vector2<T> ComponentMin(in Vector2<T> vector1, in Vector2<T> vector2) => new(T.Min(vector1.X, vector2.X), T.Min(vector1.Y, vector2.Y));

    /// <summary>Calculates the distance between two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The distance between the vectors.</returns>
    public static T Distance(in Vector2<T> vector1, in Vector2<T> vector2) => T.Sqrt(DistanceSquared(vector1, vector2));

    /// <summary>Calculates the squared distance between two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The squared distance between the vectors.</returns>
    /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
    public static T DistanceSquared(in Vector2<T> vector1, in Vector2<T> vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y);

    /// <summary>Calculates the dot product of two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The dot product of the vectors.</returns>
    public static T Dot(in Vector2<T> vector1, in Vector2<T> vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y;

    /// <summary>Linearly interpolates between two vectors.</summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The destination vector.</param>
    /// <param name="amount">The amount to interpolate between the vectors.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector2<T> Lerp(in Vector2<T> value1, in Vector2<T> value2, T amount) => new(MathsHelper<T>.Lerp(value1.X, value2.X, amount), MathsHelper<T>.Lerp(value1.Y, value2.Y, amount));

    /// <summary>Linearly interpolates between two vectors.</summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The destination vector.</param>
    /// <param name="amount">The amount to interpolate between the vectors.</param>
    /// <returns>The interpolated vector.</returns>
    /// <remarks>This clamps <paramref name="amount"/> before performing the linear interpolation.</remarks>
    public static Vector2<T> LerpClamped(in Vector2<T> value1, in Vector2<T> value2, T amount) => new (MathsHelper<T>.LerpClamped(value1.X, value2.X, amount), MathsHelper<T>.LerpClamped(value1.Y, value2.Y, amount));

    /// <summary>Reflects a vector off a normal.</summary>
    /// <param name="direction">The vector to reflect.</param>
    /// <param name="normal">The surface normal.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector2<T> Reflect(in Vector2<T> direction, Vector2<T> normal)
    {
        normal.Normalise();
        return direction - T.CreateChecked(2) * Dot(direction, normal) * normal;
    }


    /*********
    ** Operators
    *********/
    /// <summary>Adds a vector and a value.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector2<T> operator +(Vector2<T> left, T right) => new(left.X + right, left.Y + right);

    /// <summary>Adds two vectors together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector2<T> operator +(Vector2<T> left, Vector2<T> right) => new(left.X + right.X, left.Y + right.Y);

    /// <summary>Subtracts a value from a vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector2<T> operator -(Vector2<T> left, T right) => new(left.X - right, left.Y - right);

    /// <summary>Subtracts a vector from another vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector2<T> operator -(Vector2<T> left, Vector2<T> right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>Negates each component of a vector.</summary>
    /// <param name="vector">The vector to negate the components of.</param>
    /// <returns><paramref name="vector"/> with its components negated.</returns>
    public static Vector2<T> operator -(Vector2<T> vector) => new(-vector.X, -vector.Y);

    /// <summary>Multiplies a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2<T> operator *(T left, Vector2<T> right) => right * left;

    /// <summary>Multiplies a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2<T> operator *(Vector2<T> left, T right) => new(left.X * right, left.Y * right);

    /// <summary>Multiplies two vectors together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2<T> operator *(Vector2<T> left, Vector2<T> right) => new(left.X * right.X, left.Y * right.Y);

    /// <summary>Divides a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the division.</returns>
    public static Vector2<T> operator /(Vector2<T> left, T right) => new(left.X / right, left.Y / right);

    /// <summary>Divides a vector by another vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the division.</returns>
    public static Vector2<T> operator /(Vector2<T> left, Vector2<T> right) => new(left.X / right.X, left.Y / right.Y);

    /// <summary>Checks two vectors for equality.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vector2<T> vector1, Vector2<T> vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y;

    /// <summary>Checks two vectors for inequality.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vector2<T> vector1, Vector2<T> vector2) => !(vector1 == vector2);

    /// <summary>Converts a vector to a tuple.</summary>
    /// <param name="vector">The vector to convert.</param>
    public static implicit operator (T X, T Y)(Vector2<T> vector) => (vector.X, vector.Y);

    /// <summary>Converts a tuple to a vector.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Vector2<T>((T X, T Y) tuple) => new(tuple.X, tuple.Y);
}

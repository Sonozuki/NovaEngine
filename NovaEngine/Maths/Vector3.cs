using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a vector with three floating-point values.</summary>
public struct Vector3<T> : IEquatable<Vector3<T>>, IComparable<Vector3<T>>
    where T : IFloatingPoint<T>, IRootFunctions<T>, ITrigonometricFunctions<T>, IConvertible
{
    /*********
    ** Fields
    *********/
    /// <summary>The X component of the vector.</summary>
    public T X;

    /// <summary>The Y component of the vector.</summary>
    public T Y;

    /// <summary>The Z component of the vector.</summary>
    public T Z;


    /*********
    ** Properties
    *********/
    /// <summary>The length of the vector.</summary>
    public readonly T Length => T.Sqrt(LengthSquared);

    /// <summary>The squared length of the vector.</summary>
    /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
    public readonly T LengthSquared => X * X + Y * Y + Z * Z;

    /// <summary>The vector with unit length.</summary>
    public readonly Vector3<T> Normalised
    {
        get
        {
            var vector = this;
            vector.Normalise();
            return vector;
        }
    }

    /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, and <see cref="Y"/> components.</summary>
    public Vector3<T> XZY
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
    public Vector3<T> YXZ
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
    public Vector3<T> YZX
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
    public Vector3<T> ZXY
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
    public Vector3<T> ZYX
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
    public Vector2<T> XY
    {
        readonly get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
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

    /// <summary>Swizzle the <see cref="X"/> and <see cref="Z"/> components.</summary>
    public Vector2<T> XZ
    {
        readonly get => new(X, Z);
        set
        {
            X = value.X;
            Z = value.Y;
        }
    }

    /// <summary>Swizzle the <see cref="Z"/> and <see cref="X"/> components.</summary>
    public Vector2<T> ZX
    {
        readonly get => new(Z, X);
        set
        {
            Z = value.X;
            X = value.Y;
        }
    }

    /// <summary>Swizzle the <see cref="Y"/> and <see cref="Z"/> components.</summary>
    public Vector2<T> YZ
    {
        readonly get => new(Y, Z);
        set
        {
            Y = value.X;
            Z = value.Y;
        }
    }

    /// <summary>Swizzle the <see cref="Z"/> and <see cref="Y"/> components.</summary>
    public Vector2<T> ZY
    {
        readonly get => new(Z, Y);
        set
        {
            Z = value.X;
            Y = value.Y;
        }
    }

    /// <summary>A vector with (X, Y, Z) = (0, 0, 0).</summary>
    public static Vector3<T> Zero => new(T.Zero);

    /// <summary>A vector with (X, Y, Z) = (1, 1, 1).</summary>
    public static Vector3<T> One => new(T.One);

    /// <summary>A vector with (X, Y, Z) = (1, 0, 0).</summary>
    public static Vector3<T> UnitX => new(T.One, T.Zero, T.Zero);

    /// <summary>A vector with (X, Y, Z) = (0, 1, 0).</summary>
    public static Vector3<T> UnitY => new(T.Zero, T.One, T.Zero);

    /// <summary>A vector with (X, Y, Z) = (0, 0, 1).</summary>
    public static Vector3<T> UnitZ => new(T.Zero, T.Zero, T.One);

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
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="value">The X, Y, and Z components of the vector.</param>
    public Vector3(T value)
    {
        X = value;
        Y = value;
        Z = value;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    /// <param name="z">The Z component of the vector.</param>
    public Vector3(T x, T y, T z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="xy">The X and Y components of the vector.</param>
    /// <param name="z">The Z component of the vector.</param>
    public Vector3(in Vector2<T> xy, T z)
    {
        X = xy.X;
        Y = xy.Y;
        Z = z;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Scales the vector to unit length.</summary>
    public void Normalise()
    {
        if (LengthSquared == T.Zero)
            return;

        var scale = T.One / Length;
        X *= scale;
        Y *= scale;
        Z *= scale;
    }

    /// <summary>Checks two vectors for equality.</summary>
    /// <param name="other">The vector to check equality with.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector3<T> other) => this == other;

    /// <summary>Compares two vectors to determine whether the current instance preceeds, follows, or appears at the same position in the sort order as another vector.</summary>
    /// <param name="other">The vector to compare against.</param>
    /// <returns>
    /// <b>Less than zero</b>, if this instance preceeds <paramref name="other"/> in the sort order.<br/>
    /// <b>Zero</b>, if this instance appears in the same position as <paramref name="other"/> in the sort order.<br/>
    /// <b>Greater than zero</b>, if this instance follows <paramref name="other"/> in the sort order.
    /// </returns>
    public int CompareTo(Vector3<T> other)
    {
        var difference = LengthSquared - other.LengthSquared;
        return (difference > T.Zero ? T.Ceiling(difference) : T.Floor(difference)).ToInt32(null);
    }

    /// <summary>Checks the vector and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the vector and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Vector3<T> vector && this == vector;

    /// <summary>Retrieves the hash code of the vector.</summary>
    /// <returns>The hash code of the vector.</returns>
    public readonly override int GetHashCode() => (X, Y, Z).GetHashCode();

    /// <summary>Calculates a string representation of the vector.</summary>
    /// <returns>A string representation of the vector.</returns>
    public readonly override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}>";

    /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
    /// <param name="value">The vector to clamp.</param>
    /// <param name="min">The minimum vector.</param>
    /// <param name="max">The maximum vector.</param>
    /// <returns>The clamped vector.</returns>
    public static Vector3<T> Clamp(in Vector3<T> value, in Vector3<T> min, in Vector3<T> max) => new(T.Clamp(value.X, min.X, max.X), T.Clamp(value.Y, min.Y, max.Y), T.Clamp(value.Z, min.Z, max.Z));

    /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise maximum.</returns>
    public static Vector3<T> ComponentMax(in Vector3<T> vector1, in Vector3<T> vector2) => new(T.Max(vector1.X, vector2.X), T.Max(vector1.Y, vector2.Y), T.Max(vector1.Z, vector2.Z));

    /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise minimum.</returns>
    public static Vector3<T> ComponentMin(in Vector3<T> vector1, in Vector3<T> vector2) => new(T.Min(vector1.X, vector2.X), T.Min(vector1.Y, vector2.Y), T.Min(vector1.Z, vector2.Z));

    /// <summary>Calculates the cross product of two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The cross product the vectors.</returns>
    public static Vector3<T> Cross(in Vector3<T> vector1, in Vector3<T> vector2) => new(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);

    /// <summary>Calculates the distance between two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The distance between the vectors.</returns>
    public static T Distance(in Vector3<T> vector1, in Vector3<T> vector2) => T.Sqrt(DistanceSquared(vector1, vector2));

    /// <summary>Calculates the sqaured distance between two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The squared distance between the vectors.</returns>
    /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
    public static T DistanceSquared(in Vector3<T> vector1, in Vector3<T> vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y) + (vector2.Z - vector1.Z) * (vector2.Z - vector1.Z);

    /// <summary>Calculates the dot product of two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The dot product of the vectors.</returns>
    public static T Dot(in Vector3<T> vector1, in Vector3<T> vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;

    /// <summary>Linearly interpolates between two vectors.</summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The destination vector.</param>
    /// <param name="amount">The amount to interpolate between the vectors.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector3<T> Lerp(in Vector3<T> value1, in Vector3<T> value2, T amount) => new(MathsHelper<T>.Lerp(value1.X, value2.X, amount), MathsHelper<T>.Lerp(value1.Y, value2.Y, amount), MathsHelper<T>.Lerp(value1.Z, value2.Z, amount));

    /// <summary>Linearly interpolates between two vectors.</summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The destination vector.</param>
    /// <param name="amount">The amount to interpolate between the vectors.</param>
    /// <returns>The interpolated vector.</returns>
    /// <remarks>This clamps <paramref name="amount"/> before performing the linear interpolation.</remarks>
    public static Vector3<T> LerpClamped(in Vector3<T> value1, in Vector3<T> value2, T amount) => new(MathsHelper<T>.LerpClamped(value1.X, value2.X, amount), MathsHelper<T>.LerpClamped(value1.Y, value2.Y, amount), MathsHelper<T>.LerpClamped(value1.Z, value2.Z, amount));

    /// <summary>Reflects a vector off a normal.</summary>
    /// <param name="direction">The vector to reflect.</param>
    /// <param name="normal">The surface normal.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector3<T> Reflect(in Vector3<T> direction, Vector3<T> normal)
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
    public static Vector3<T> operator +(Vector3<T> left, T right) => new(left.X + right, left.Y + right, left.Z + right);

    /// <summary>Adds two vectors together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector3<T> operator +(Vector3<T> left, Vector3<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    /// <summary>Subtracts a value from a vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector3<T> operator -(Vector3<T> left, T right) => new(left.X - right, left.Y - right, left.Z - right);

    /// <summary>Subtracts a vector from another vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector3<T> operator -(Vector3<T> left, Vector3<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    /// <summary>Negates each component of a vector.</summary>
    /// <param name="vector">The vector to negate the components of.</param>
    /// <returns><paramref name="vector"/> with its components negated.</returns>
    public static Vector3<T> operator -(Vector3<T> vector) => new(-vector.X, -vector.Y, -vector.Z);

    /// <summary>Multiplies a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3<T> operator *(T left, Vector3<T> right) => right * left;

    /// <summary>Multiplies a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3<T> operator *(Vector3<T> left, T right) => new(left.X * right, left.Y * right, left.Z * right);

    /// <summary>Multiplies two vectors together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3<T> operator *(Vector3<T> left, Vector3<T> right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    /// <summary>Multiplies a vector by a quaternion.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3<T> operator *(Vector3<T> left, Quaternion<T> right)
    {
        var vector = right.XYZ;

        var vectorLeft = Vector3<T>.Cross(vector, left);
        var vectorVectorLeft = Vector3<T>.Cross(vector, vectorLeft);

        return left + ((vectorLeft * right.W) + vectorVectorLeft) * T.CreateChecked(2);
    }

    /// <summary>Divides a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the division.</returns>
    public static Vector3<T> operator /(Vector3<T> left, T right) => new(left.X / right, left.Y / right, left.Z / right);

    /// <summary>Divides a vector by another vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the division.</returns>
    public static Vector3<T> operator /(Vector3<T> left, Vector3<T> right) => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

    /// <summary>Compares two vectors.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true"/>, if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator <(Vector3<T> left, Vector3<T> right) => left.CompareTo(right) < 0;

    /// <summary>Compares two vectors.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true"/>, if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(Vector3<T> left, Vector3<T> right) => left.CompareTo(right) <= 0;

    /// <summary>Compares two vectors.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true"/>, if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator >(Vector3<T> left, Vector3<T> right) => left.CompareTo(right) > 0;

    /// <summary>Compares two vectors.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true"/>, if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(Vector3<T> left, Vector3<T> right) => left.CompareTo(right) >= 0;

    /// <summary>Checks two vectors for equality.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vector3<T> vector1, Vector3<T> vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;

    /// <summary>Checks two vectors for inequality.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vector3<T> vector1, Vector3<T> vector2) => !(vector1 == vector2);

    /// <summary>Converts a vector to a tuple.</summary>
    /// <param name="vector">The vector to convert.</param>
    public static implicit operator (T X, T Y, T Z)(Vector3<T> vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>Converts a tuple to a vector.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Vector3<T>((T X, T Y, T Z) tuple) => new(tuple.X, tuple.Y, tuple.Z);
}

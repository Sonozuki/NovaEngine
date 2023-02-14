using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a vector with two 32-bit unsigned integer values.</summary>
public struct Vector2U
{
    /*********
    ** Fields
    *********/
    /// <summary>The X component of the vector.</summary>
    public uint X;

    /// <summary>The Y component of the vector.</summary>
    public uint Y;


    /*********
    ** Accessors
    *********/
    /// <summary>Swizzle the <see cref="Y"/> and <see cref="X"/> components.</summary>
    public Vector2U YX
    {
        readonly get => new(Y, X);
        set
        {
            Y = value.X;
            X = value.Y;
        }
    }

    /// <summary>Gets or sets the value at a specified position.</summary>
    /// <param name="index">The position index.</param>
    /// <returns>The value at the specified position.</returns>
    public uint this[int index]
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
    public Vector2U(uint value)
    {
        X = value;
        Y = value;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    public Vector2U(uint x, uint y)
    {
        X = x;
        Y = y;
    }

    /// <summary>Gets the vector as a <see cref="Vector2{T}"/>.</summary>
    /// <typeparam name="T">The type of vector to convert to.</typeparam>
    /// <returns>The converted vector.</returns>
    /// <remarks>Components will get truncated if they fall outside of the range of <typeparamref name="T"/>.</remarks>
    public readonly Vector2<T> ToVector2<T>()
        where T : IFloatingPoint<T>, IRootFunctions<T>, ITrigonometricFunctions<T>, IConvertible
    {
        return new(T.CreateTruncating(X), T.CreateTruncating(Y));
    }

    /// <summary>Checks two vectors for equality.</summary>
    /// <param name="other">The vector to check equality with.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector2U other) => this == other;

    /// <summary>Checks the vector and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the vector and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Vector2U vector && this == vector;

    /// <summary>Retrieves the hash code of the vector.</summary>
    /// <returns>The hash code of the vector.</returns>
    public readonly override int GetHashCode() => (X, Y).GetHashCode();

    /// <summary>Calculates a string representation of the vector.</summary>
    /// <returns>A string representation of the vector.</returns>
    public readonly override string ToString() => $"<X: {X}, Y: {Y}>";

    /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
    /// <param name="value">The vector to clamp.</param>
    /// <param name="min">The minimum vector.</param>
    /// <param name="max">The maximum vector.</param>
    /// <returns>The clamped vector.</returns>
    public static Vector2U Clamp(in Vector2U value, in Vector2U min, in Vector2U max) => new(Math.Clamp(value.X, min.X, max.X), Math.Clamp(value.Y, min.Y, max.Y));

    /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise maximum.</returns>
    public static Vector2U ComponentMax(in Vector2U vector1, in Vector2U vector2) => new(Math.Max(vector1.X, vector2.X), Math.Max(vector1.Y, vector2.Y));

    /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise minimum.</returns>
    public static Vector2U ComponentMin(in Vector2U vector1, in Vector2U vector2) => new(Math.Min(vector1.X, vector2.X), Math.Min(vector1.Y, vector2.Y));


    /*********
    ** Operators
    *********/
    /// <summary>Adds a vector and a value.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector2U operator +(Vector2U left, uint right) => new(left.X + right, left.Y + right);

    /// <summary>Adds two vectors together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector2U operator +(Vector2U left, Vector2U right) => new(left.X + right.X, left.Y + right.Y);

    /// <summary>Subtracts a value from a vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector2U operator -(Vector2U left, uint right) => new(left.X - right, left.Y - right);

    /// <summary>Subtracts a vector from another vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector2U operator -(Vector2U left, Vector2U right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>Multiplies a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2U operator *(uint left, Vector2U right) => right * left;

    /// <summary>Multiplies a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2U operator *(Vector2U left, uint right) => new(left.X * right, left.Y * right);

    /// <summary>Multiplies two vectors together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2U operator *(Vector2U left, Vector2U right) => new(left.X * right.X, left.Y * right.Y);

    /// <summary>Checks two vectors for equality.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vector2U vector1, Vector2U vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y;

    /// <summary>Checks two vectors for inequality.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vector2U vector1, Vector2U vector2) => !(vector1 == vector2);

    /// <summary>Converts a vector to a tuple.</summary>
    /// <param name="vector">The vector to convert.</param>
    public static implicit operator (uint X, uint Y)(Vector2U vector) => (vector.X, vector.Y);

    /// <summary>Converts a tuple to a vector.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Vector2U((uint X, uint Y) tuple) => new(tuple.X, tuple.Y);
}

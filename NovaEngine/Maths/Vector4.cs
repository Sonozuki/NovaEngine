using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a vector with four floating-point values.</summary>
public struct Vector4<T> : IEquatable<Vector4<T>>
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

    /// <summary>The W component of the vector.</summary>
    public T W;


    /*********
    ** Accessors
    *********/
    /// <summary>The length of the vector.</summary>
    public readonly T Length => T.Sqrt(LengthSquared);

    /// <summary>The squared length of the vector.</summary>
    /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
    public readonly T LengthSquared => X * X + Y * Y + Z * Z + W * W;

    /// <summary>The vector with unit length.</summary>
    public readonly Vector4<T> Normalised
    {
        get
        {
            var vector = this;
            vector.Normalise();
            return vector;
        }
    }

    /// <summary>Swizzle the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, and <see cref="Z"/> components.</summary>
    public Vector4<T> XYWZ
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
    public Vector4<T> XZYW
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
    public Vector4<T> XZWY
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
    public Vector4<T> XWYZ
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
    public Vector4<T> XWZY
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
    public Vector4<T> YXZW
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
    public Vector4<T> YXWZ
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
    public Vector4<T> YZXW
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
    public Vector4<T> YZWX
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
    public Vector4<T> YWXZ
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
    public Vector4<T> YWZX
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
    public Vector4<T> ZXYW
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
    public Vector4<T> ZXWY
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
    public Vector4<T> ZYXW
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
    public Vector4<T> ZYWX
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
    public Vector4<T> ZWXY
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
    public Vector4<T> ZWYX
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
    public Vector4<T> WXYZ
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
    public Vector4<T> WXZY
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
    public Vector4<T> WYXZ
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
    public Vector4<T> WYZX
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
    public Vector4<T> WZXY
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
    public Vector4<T> WZYX
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
    public Vector3<T> XYZ
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
    public Vector3<T> XYW
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

    /// <summary>Swizzle the <see cref="X"/>, <see cref="Z"/>, and <see cref="W"/> components.</summary>
    public Vector3<T> XZW
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
    public Vector3<T> XWY
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
    public Vector3<T> XWZ
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

    /// <summary>Swizzle the <see cref="Y"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
    public Vector3<T> YXW
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
    public Vector3<T> YZX
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
    public Vector3<T> YZW
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
    public Vector3<T> YWX
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
    public Vector3<T> YWZ
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

    /// <summary>Swizzle the <see cref="Z"/>, <see cref="X"/>, and <see cref="W"/> components.</summary>
    public Vector3<T> ZXW
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

    /// <summary>Swizzle the <see cref="Z"/>, <see cref="Y"/>, and <see cref="W"/> components.</summary>
    public Vector3<T> ZYW
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
    public Vector3<T> WXY
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
    public Vector3<T> WXZ
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
    public Vector3<T> WYX
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
    public Vector3<T> WYZ
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
    public Vector3<T> WZX
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
    public Vector3<T> WZY
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
    public Vector2<T> XY
    {
        readonly get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
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

    /// <summary>Swizzle the <see cref="X"/> and <see cref="W"/> components.</summary>
    public Vector2<T> XW
    {
        readonly get => new(X, W);
        set
        {
            X = value.X;
            W = value.Y;
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

    /// <summary>Swizzle the <see cref="Y"/> and <see cref="W"/> components.</summary>
    public Vector2<T> YW
    {
        readonly get => new(Y, W);
        set
        {
            Y = value.X;
            W = value.Y;
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

    /// <summary>Swizzle the <see cref="Z"/> and <see cref="W"/> components.</summary>
    public Vector2<T> ZW
    {
        readonly get => new(Z, W);
        set
        {
            Z = value.X;
            W = value.Y;
        }
    }

    /// <summary>Swizzle the <see cref="W"/> and <see cref="X"/> components.</summary>
    public Vector2<T> WX
    {
        readonly get => new(W, X);
        set
        {
            W = value.X;
            X = value.Y;
        }
    }

    /// <summary>Swizzle the <see cref="W"/> and <see cref="Y"/> components.</summary>
    public Vector2<T> WY
    {
        readonly get => new(W, Y);
        set
        {
            W = value.X;
            Y = value.Y;
        }
    }

    /// <summary>Swizzle the <see cref="W"/> and <see cref="Z"/> components.</summary>
    public Vector2<T> WZ
    {
        readonly get => new(W, Z);
        set
        {
            W = value.X;
            Z = value.Y;
        }
    }

    /// <summary>A vector with (X, Y, Z, W) = (0, 0, 0, 0).</summary>
    public static Vector4<T> Zero => new(T.Zero);

    /// <summary>A vector with (X, Y, Z, W) = (1, 1, 1, 1).</summary>
    public static Vector4<T> One => new(T.One);

    /// <summary>A vector with (X, Y, Z, W) = (1, 0, 0, 0).</summary>
    public static Vector4<T> UnitX => new(T.One, T.Zero, T.Zero, T.Zero);

    /// <summary>A vector with (X, Y, Z, W) = (0, 1, 0, 0).</summary>
    public static Vector4<T> UnitY => new(T.Zero, T.One, T.Zero, T.Zero);

    /// <summary>A vector with (X, Y, Z, W) = (0, 0, 1, 0).</summary>
    public static Vector4<T> UnitZ => new(T.Zero, T.Zero, T.One, T.Zero);

    /// <summary>A vector with (X, Y, Z, W) = (0, 0, 0, 1).</summary>
    public static Vector4<T> UnitW => new(T.Zero, T.Zero, T.Zero, T.One);

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
    public Vector4(T value)
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
    public Vector4(T x, T y, T z, T w)
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
    public Vector4(in Vector2<T> xy, T z, T w)
    {
        X = xy.X;
        Y = xy.Y;
        Z = z;
        W = w;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="xy">The X and Y components of the vector.</param>
    /// <param name="zw">The Z and W components of the vector.</param>
    public Vector4(in Vector2<T> xy, in Vector2<T> zw)
    {
        X = xy.X;
        Y = xy.Y;
        Z = zw.X;
        W = zw.Y;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="xyz">The X, Y, and Z components of the vector.</param>
    /// <param name="w">The W component of the vector.</param>
    public Vector4(in Vector3<T> xyz, T w)
    {
        X = xyz.X;
        Y = xyz.Y;
        Z = xyz.Z;
        W = w;
    }

    /// <summary>Scales the vector to unit length.</summary>
    public void Normalise()
    {
        if (LengthSquared == T.Zero)
            return;

        var scale = T.One / Length;
        X *= scale;
        Y *= scale;
        Z *= scale;
        W *= scale;
    }

    /// <summary>Checks two vectors for equality.</summary>
    /// <param name="other">The vector to check equality with.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector4<T> other) => this == other;

    /// <summary>Checks the vector and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the vector and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Vector4<T> vector && this == vector;

    /// <summary>Retrieves the hash code of the vector.</summary>
    /// <returns>The hash code of the vector.</returns>
    public readonly override int GetHashCode() => (X, Y, Z, W).GetHashCode();

    /// <summary>Calculates a string representation of the vector.</summary>
    /// <returns>A string representation of the vector.</returns>
    public readonly override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}, W: {W}>";

    /// <summary>Clamps a vector to the specified minimum and maximum vectors.</summary>
    /// <param name="value">The vector to clamp.</param>
    /// <param name="min">The minimum vector.</param>
    /// <param name="max">The maximum vector.</param>
    /// <returns>The clamped vector.</returns>
    public static Vector4<T> Clamp(in Vector4<T> value, in Vector4<T> min, in Vector4<T> max) => new(T.Clamp(value.X, min.X, max.X), T.Clamp(value.Y, min.Y, max.Y), T.Clamp(value.Z, min.Z, max.Z), T.Clamp(value.W, min.W, max.W));

    /// <summary>Creates a vector using the largest of the corresponding components from two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise maximum.</returns>
    public static Vector4<T> ComponentMax(in Vector4<T> vector1, in Vector4<T> vector2) => new(T.Max(vector1.X, vector2.X), T.Max(vector1.Y, vector2.Y), T.Max(vector1.Z, vector2.Z), T.Max(vector1.W, vector2.W));

    /// <summary>Creates a vector using the smallest of the corresponding components from two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise minimum.</returns>
    public static Vector4<T> ComponentMin(in Vector4<T> vector1, in Vector4<T> vector2) => new(T.Min(vector1.X, vector2.X), T.Min(vector1.Y, vector2.Y), T.Min(vector1.Z, vector2.Z), T.Min(vector1.W, vector2.W));

    /// <summary>Calculates the distance between two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The distance between the vectors.</returns>
    public static T Distance(in Vector4<T> vector1, in Vector4<T> vector2) => T.Sqrt(DistanceSquared(vector1, vector2));

    /// <summary>Calculates the sqaured distance between two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The squared distance between the vectors.</returns>
    /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
    public static T DistanceSquared(in Vector4<T> vector1, in Vector4<T> vector2) => (vector2.X - vector1.X) * (vector2.X - vector1.X) + (vector2.Y - vector1.Y) * (vector2.Y - vector1.Y) + (vector2.Z - vector1.Z) * (vector2.Z - vector1.Z) + (vector2.W - vector1.W) * (vector2.W - vector1.W);

    /// <summary>Calculates the dot product of two vectors.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The dot product of the vectors.</returns>
    public static T Dot(in Vector4<T> vector1, in Vector4<T> vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;

    /// <summary>Linearly interpolates between two vectors.</summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The destination vector.</param>
    /// <param name="amount">The amount to interpolate between the vectors.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector4<T> Lerp(in Vector4<T> value1, in Vector4<T> value2, T amount) => new(MathsHelper<T>.Lerp(value1.X, value2.X, amount), MathsHelper<T>.Lerp(value1.Y, value2.Y, amount), MathsHelper<T>.Lerp(value1.Z, value2.Z, amount), MathsHelper<T>.Lerp(value1.W, value2.W, amount));

    /// <summary>Linearly interpolates between two vectors.</summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The destination vector.</param>
    /// <param name="amount">The amount to interpolate between the vectors.</param>
    /// <returns>The interpolated vector.</returns>
    /// <remarks>This clamps <paramref name="amount"/> before performing the linear interpolation.</remarks>
    public static Vector4<T> LerpClamped(in Vector4<T> value1, in Vector4<T> value2, T amount) => new(MathsHelper<T>.LerpClamped(value1.X, value2.X, amount), MathsHelper<T>.LerpClamped(value1.Y, value2.Y, amount), MathsHelper<T>.LerpClamped(value1.Z, value2.Z, amount), MathsHelper<T>.LerpClamped(value1.W, value2.W, amount));


    /*********
    ** Operators
    *********/
    /// <summary>Adds a vector and a value.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector4<T> operator +(Vector4<T> left, T right) => new(left.X + right, left.Y + right, left.Z + right, left.W + right);

    /// <summary>Adds two vectors together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector4<T> operator +(Vector4<T> left, Vector4<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    /// <summary>Subtracts a value from a vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector4<T> operator -(Vector4<T> left, T right) => new(left.X - right, left.Y - right, left.Z - right, left.W - right);

    /// <summary>Subtracts a vector from another vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector4<T> operator -(Vector4<T> left, Vector4<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    /// <summary>Negates each component of a vector.</summary>
    /// <param name="vector">The vector to negate the components of.</param>
    /// <returns><paramref name="vector"/> with its components negated.</returns>
    public static Vector4<T> operator -(Vector4<T> vector) => new(-vector.X, -vector.Y, -vector.Z, -vector.W);

    /// <summary>Multiplies a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector4<T> operator *(T left, Vector4<T> right) => right * left;

    /// <summary>Multiplies a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector4<T> operator *(Vector4<T> left, T right) => new(left.X * right, left.Y * right, left.Z * right, left.W * right);

    /// <summary>Multiplies two vectors together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector4<T> operator *(Vector4<T> left, Vector4<T> right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    /// <summary>Divides a vector by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the division.</returns>
    public static Vector4<T> operator /(Vector4<T> left, T right) => new(left.X / right, left.Y / right, left.Z / right, left.W / right);

    /// <summary>Divides a vector by another vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the division.</returns>
    public static Vector4<T> operator /(Vector4<T> left, Vector4<T> right) => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

    /// <summary>Checks two vectors for equality.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vector4<T> vector1, Vector4<T> vector2) => vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z && vector1.W == vector2.W;

    /// <summary>Checks two vectors for inequality.</summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vector4<T> vector1, Vector4<T> vector2) => !(vector1 == vector2);

    /// <summary>Converts a vector to a tuple.</summary>
    /// <param name="vector">The vector to convert.</param>
    public static implicit operator (T X, T Y, T Z, T W)(Vector4<T> vector) => (vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>Converts a tuple to a vector.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Vector4<T>((T X, T Y, T Z, T W) tuple) => new(tuple.X, tuple.Y, tuple.Z, tuple.W);
}

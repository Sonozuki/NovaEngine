using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a quaternion using single-precision floating-point numbers.</summary>
public struct Quaternion<T> : IEquatable<Quaternion<T>>
    where T : IFloatingPoint<T>, IRootFunctions<T>, ITrigonometricFunctions<T>, IConvertible
{
    /*********
    ** Fields
    *********/
    /// <summary>The X component of the vector part of the quaternion.</summary>
    public T X;

    /// <summary>The Y component of the vector part of the quaternion.</summary>
    public T Y;

    /// <summary>The Z component of the vector part of the quaternion.</summary>
    public T Z;

    /// <summary>The W component of the quaternion.</summary>
    public T W;


    /*********
    ** Accessors
    *********/
    /// <summary>Whether the quaternion is an identity quaternion.</summary>
    public readonly bool IsIdentity => this == Identity;

    /// <summary>The length of the quaternion.</summary>
    public readonly T Length => T.Sqrt(LengthSquared);

    /// <summary>The squared length of the quaternion.</summary>
    /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
    public readonly T LengthSquared => X * X + Y * Y + Z * Z + W * W;

    /// <summary>The quaternion with unit length.</summary>
    public readonly Quaternion<T> Normalised
    {
        get
        {
            var quaternion = this;
            quaternion.Normalise();
            return quaternion;
        }
    }

    /// <summary>The inverse of the quaternion.</summary>
    public readonly Quaternion<T> Inverse
    {
        get
        {
            var quaternion = this;
            quaternion.Invert();
            return quaternion;
        }
    }

    /// <summary>The conjugate of the quaternion.</summary>
    public readonly Quaternion<T> Conjugate => new(-X, -Y, -Z, W);

    /// <summary>The <see cref="X"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
    public readonly Vector3<T> XYZ => new(X, Y, Z);

    /// <summary>A quaternion with (X, Y, Z, W) = (0, 0, 0, 1), which represents no rotation.</summary>
    public static Quaternion<T> Identity => new(T.Zero, T.Zero, T.Zero, T.One);

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
    /// <param name="x">The X component of the vector part of the quaternion.</param>
    /// <param name="y">The Y component of the vector part of the quaternion.</param>
    /// <param name="z">The Z component of the vector part of the quaternion.</param>
    /// <param name="w">The W component of the quaternion.</param>
    public Quaternion(T x, T y, T z, T w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="vector">The vector part of the quaternion.</param>
    /// <param name="w">The W component of the quaternion.</param>
    public Quaternion(in Vector3<T> vector, T w)
    {
        X = vector.X;
        Y = vector.Y;
        Z = vector.Z;
        W = w;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="vector">The quaternion components.</param>
    public Quaternion(in Vector4<T> vector)
    {
        X = vector.X;
        Y = vector.Y;
        Z = vector.Z;
        W = vector.W;
    }

    /// <summary>Scales the quaternion to unit length.</summary>
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

    /// <summary>Inverts the quaternion.</summary>
    public void Invert()
    {
        if (LengthSquared == T.Zero)
            return;

        var invertedLengthSquared = T.One / LengthSquared;
        X *= -invertedLengthSquared;
        Y *= -invertedLengthSquared;
        Z *= -invertedLengthSquared;
        W *= invertedLengthSquared;
    }

    /// <summary>Gets the axis and angle that the quaternion represents.</summary>
    /// <param name="axis">The axis.</param>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    public void GetAxisAngle(out Vector3<T> axis, out T angle)
    {
        var quaternion = this;
        if (T.Abs(quaternion.W) > T.One)
            quaternion.Normalise();

        angle = MathsHelper<T>.RadiansToDegrees(T.CreateChecked(2) * T.Acos(quaternion.W));
        var denominator = T.Sqrt(T.One - quaternion.W * quaternion.W);
        if (denominator == T.Zero)
            axis = Vector3<T>.UnitX;
        else
            axis = quaternion.XYZ / denominator;
    }

    /// <summary>Checks two quaternions for equality.</summary>
    /// <param name="other">The quaternion to check equality with.</param>
    /// <returns><see langword="true"/> if the quaternions are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Quaternion<T> other) => this == other;

    /// <summary>Checks the quaternion and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the quaternion and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Quaternion<T> quaternion && this == quaternion;

    /// <summary>Retrieves the hash code of the quaternion.</summary>
    /// <returns>The hash code of the quaternion.</returns>
    public readonly override int GetHashCode() => (X, Y, Z, W).GetHashCode();

    /// <summary>Calculates a string representation of the quaternion.</summary>
    /// <returns>A string representation of the quaternion.</returns>
    public readonly override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}, W: {W}>";

    /// <summary>Calculates the dot product of two quaternions.</summary>
    /// <param name="quaternion1">The first quaternion.</param>
    /// <param name="quaternion2">The second quaternion.</param>
    /// <returns>The dot product of the quaternions.</returns>
    public static T Dot(in Quaternion<T> quaternion1, in Quaternion<T> quaternion2) => quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;

    /// <summary>Interpolates between two quaternions, using spherical linear interpolation.</summary>
    /// <param name="quaternion1">The source quaternion.</param>
    /// <param name="quaternion2">The destination quaternion.</param>
    /// <param name="amount">The amount to interpolate between the quaternions.</param>
    /// <returns>The interpolated quaternion.</returns>
    public static Quaternion<T> Slerp(in Quaternion<T> quaternion1, in Quaternion<T> quaternion2, T amount)
    {
        // if either quaternion is zero, return the other
        if (quaternion1.LengthSquared == T.Zero)
        {
            if (quaternion2.LengthSquared == T.Zero)
                return Quaternion<T>.Identity;

            return quaternion2;
        }
        else if (quaternion2.LengthSquared == T.Zero)
            return quaternion1;

        // slerp quaternion
        var cosOmega = Quaternion<T>.Dot(quaternion1, quaternion2);
        var flip = false;
        if (cosOmega < T.Zero)
        {
            flip = true;
            cosOmega = -cosOmega;
        }

        T amountA;
        T amountB;
        if (cosOmega > T.CreateChecked(.999f))
        {
            // too close, do regular linear interpolation
            amountA = T.One - amount;
            amountB = flip ? -amount : amount;
        }
        else
        {
            // do proper slerp
            var omega = T.Acos(cosOmega);
            var inverseSinOmega = T.One / T.Sin(omega);

            amountA = T.Sin((T.One - amount) * omega) * inverseSinOmega;
            amountB = flip
                ? -T.Sin(amount * omega) * inverseSinOmega
                : T.Sin(amount * omega) * inverseSinOmega;
        }

        return new(
            x: amountA * quaternion1.X + amountB * quaternion2.X,
            y: amountA * quaternion1.Y + amountB * quaternion2.Y,
            z: amountA * quaternion1.Z + amountB * quaternion2.Z,
            w: amountA * quaternion1.W + amountB * quaternion2.W
        );
    }

    /// <summary>Creates a quaternion from an axis and an angle.</summary>
    /// <param name="axis">The axis to rotate around.</param>
    /// <param name="angle">The clockwise angle, in degrees, to rotate around the axis.</param>
    /// <returns>The created quaternion.</returns>
    public static Quaternion<T> CreateFromAxisAngle(Vector3<T> axis, T angle)
    {
        if (axis.LengthSquared == T.Zero)
            return Quaternion<T>.Identity;
        axis.Normalise();

        var halfAngle = MathsHelper<T>.DegreesToRadians(angle) / T.CreateChecked(2);
        var sinHalfAngle = T.Sin(halfAngle);
        var cosHalfAngle = T.Cos(halfAngle);

        return new Quaternion<T>(axis * sinHalfAngle, cosHalfAngle).Normalised;
    }

    /// <summary>Creates a quaternion from euler angles.</summary>
    /// <param name="eulerAngles">The clockwise euler angles, in degrees.</param>
    /// <returns>The created quaternion.</returns>
    public static Quaternion<T> CreateFromEulerAngles(in Vector3<T> eulerAngles) => Quaternion<T>.CreateFromEulerAngles(eulerAngles.X, eulerAngles.Y, eulerAngles.Z);

    /// <summary>Creates a quaternion from euler angles.</summary>
    /// <param name="x">The clockwise angle, in degrees, around the X axis.</param>
    /// <param name="y">The clockwise angle, in degrees, around the Y axis.</param>
    /// <param name="z">The clockwise angle, in degrees, around the Z axis.</param>
    /// <returns>The created quaterion.</returns>
    public static Quaternion<T> CreateFromEulerAngles(T x, T y, T z)
    {
        var halfX = MathsHelper<T>.DegreesToRadians(x) / T.CreateChecked(2);
        var halfY = MathsHelper<T>.DegreesToRadians(y) / T.CreateChecked(2);
        var halfZ = MathsHelper<T>.DegreesToRadians(z) / T.CreateChecked(2);

        var sinHalfX = T.Sin(halfX);
        var sinHalfY = T.Sin(halfY);
        var sinHalfZ = T.Sin(halfZ);
        var cosHalfX = T.Cos(halfX);
        var cosHalfY = T.Cos(halfY);
        var cosHalfZ = T.Cos(halfZ);

        return new(
            x: sinHalfX * cosHalfY * cosHalfZ + cosHalfX * sinHalfY * sinHalfZ,
            y: cosHalfX * sinHalfY * cosHalfZ - sinHalfX * cosHalfY * sinHalfZ,
            z: cosHalfX * cosHalfY * sinHalfZ - sinHalfX * sinHalfY * cosHalfZ,
            w: cosHalfX * cosHalfY * cosHalfZ + sinHalfX * sinHalfY * sinHalfZ
        );
    }


    /*********
    ** Operators
    *********/
    /// <summary>Adds two quaternions together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Quaternion<T> operator +(Quaternion<T> left, Quaternion<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    /// <summary>Subtracts a quaternions from another quaternion.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Quaternion<T> operator -(Quaternion<T> left, Quaternion<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    /// <summary>Multiplies two quaternions together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Quaternion<T> operator *(Quaternion<T> left, Quaternion<T> right)
    {
        var cross = Vector3<T>.Cross(left.XYZ, right.XYZ);
        var dot = Vector3<T>.Dot(left.XYZ, right.XYZ);

        return new(
            x: left.X * right.W + right.X * left.W + cross.X,
            y: left.Y * right.W + right.Y * left.W + cross.Y,
            z: left.Z * right.W + right.Z * left.W + cross.Z,
            w: left.W * right.W - dot
        );
    }

    /// <summary>Checks two quaternions for equality.</summary>
    /// <param name="quaternion1">The first quaternion.</param>
    /// <param name="quaternion2">The second quaternion.</param>
    /// <returns><see langword="true"/> if the quaternions are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Quaternion<T> quaternion1, Quaternion<T> quaternion2) => quaternion1.X == quaternion2.X && quaternion1.Y == quaternion2.Y && quaternion1.Z == quaternion2.Z && quaternion1.W == quaternion2.W;

    /// <summary>Checks two quaternions for inequality.</summary>
    /// <param name="quaternion1">The first quaternion.</param>
    /// <param name="quaternion2">The second quaternion.</param>
    /// <returns><see langword="true"/> if the quaternions are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Quaternion<T> quaternion1, Quaternion<T> quaternion2) => !(quaternion1 == quaternion2);

    /// <summary>Converts a quaternion to a tuple.</summary>
    /// <param name="quaternion">The quaternion to convert.</param>
    public static implicit operator (T X, T Y, T Z, T W)(Quaternion<T> quaternion) => (quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

    /// <summary>Converts a tuple to a quaternion.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Quaternion<T>((T X, T Y, T Z, T W) tuple) => new(tuple.X, tuple.Y, tuple.Z, tuple.W);
}

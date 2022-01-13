namespace NovaEngine.Maths;

/// <summary>Represents a quaternion using single-precision floating-point numbers.</summary>
public struct Quaternion : IEquatable<Quaternion>
{
    /*********
    ** Fields
    *********/
    /// <summary>The X component of the vector part of the quaternion.</summary>
    public float X;

    /// <summary>The Y component of the vector part of the quaternion.</summary>
    public float Y;

    /// <summary>The Z component of the vector part of the quaternion.</summary>
    public float Z;

    /// <summary>The W component of the quaternion.</summary>
    public float W;


    /*********
    ** Accessors
    *********/
    /// <summary>Whether the quaternion is an identity quaternion.</summary>
    public readonly bool IsIdentity => this == Identity;

    /// <summary>The length of the quaternion.</summary>
    public readonly float Length => MathF.Sqrt(LengthSquared);

    /// <summary>The sqaured length of the quaternion.</summary>
    /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
    public readonly float LengthSquared => X * X + Y * Y + Z * Z + W * W;

    /// <summary>The quaternion with unit length.</summary>
    public readonly Quaternion Normalised
    {
        get
        {
            var quaternion = this;
            quaternion.Normalise();
            return quaternion;
        }
    }

    /// <summary>The inverse of the quaternion.</summary>
    public readonly Quaternion Inverse
    {
        get
        {
            var quaternion = this;
            quaternion.Invert();
            return quaternion;
        }
    }

    /// <summary>The conjugate of the quaternion.</summary>
    public readonly Quaternion Conjugate => new(-X, -Y, -Z, W);

    /// <summary>The <see cref="X"/>, <see cref="Y"/>, and <see cref="Z"/> components.</summary>
    public readonly Vector3 XYZ => new(X, Y, Z);

    /// <summary>Gets a quaternion with (X, Y, Z, W) = (0, 0, 0, 1), which represents no rotation.</summary>
    public static Quaternion Identity => new(0, 0, 0, 1);

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
    public Quaternion(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="vector">The vector part of the quaternion.</param>
    /// <param name="w">The W component of the quaternion.</param>
    public Quaternion(in Vector3 vector, float w = 1)
    {
        X = vector.X;
        Y = vector.Y;
        Z = vector.Z;
        W = w;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="vector">The quaternion components.</param>
    public Quaternion(in Vector4 vector)
    {
        X = vector.X;
        Y = vector.Y;
        Z = vector.Z;
        W = vector.W;
    }

    /// <summary>Converts the quaternion to unit length.</summary> // TODO: change 'Converts' to 'Scales' (same for vector classes)
    public void Normalise()
    {
        if (Length == 0)
            return;

        var scale = 1 / Length;
        X *= scale;
        Y *= scale;
        Z *= scale;
        W *= scale;
    }

    /// <summary>Inverts the quaternion.</summary>
    public void Invert()
    {
        var invertedLengthSquared = 1 / LengthSquared;
        X *= -invertedLengthSquared;
        Y *= -invertedLengthSquared;
        Z *= -invertedLengthSquared;
        W *= invertedLengthSquared;
    }

    /// <summary>Gets the axis and angle that the quaternion represents.</summary>
    /// <param name="axis">The axis.</param>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    public void GetAxisAngle(out Vector3 axis, out float angle)
    {
        var quaternion = this;
        if (Math.Abs(quaternion.W) > 1)
            quaternion.Normalise();

        angle = MathsHelper.RadiansToDegrees(2 * MathF.Acos(quaternion.W));
        var denominator = MathF.Sqrt(1 - quaternion.W * quaternion.W);
        if (denominator == 0)
            axis = Vector3.UnitX;
        else
            axis = quaternion.XYZ / denominator;
    }

    /// <summary>Gets the quaternion as a <see cref="QuaternionD"/>.</summary>
    /// <returns>The quaternion as a <see cref="QuaternionD"/>.</returns>
    public readonly QuaternionD ToQuaternionD() => new(X, Y, Z, W);

    /// <inheritdoc/>
    public readonly bool Equals(Quaternion other) => this == other;

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj) => obj is Quaternion quaternion && this == quaternion;

    /// <inheritdoc/>
    public readonly override int GetHashCode() => (X, Y, Z, W).GetHashCode();

    /// <inheritdoc/>
    public readonly override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}, W: {W}>";

    /// <summary>Calculates the dot product of two quaternions.</summary>
    /// <param name="quaternion1">The first quaternion.</param>
    /// <param name="quaternion2">The second quaternion.</param>
    /// <returns>The dot product of <paramref name="quaternion1"/> and <paramref name="quaternion2"/>.</returns>
    public static float Dot(in Quaternion quaternion1, in Quaternion quaternion2) => quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;

    /// <summary>Interpolates between two values, using spherical linear interpolation.</summary>
    /// <param name="quaternion1">The source value.</param>
    /// <param name="quaternion2">The destination value.</param>
    /// <param name="amount">The amount to interpolate between <paramref name="quaternion1"/> and <paramref name="quaternion2"/>.</param>
    /// <returns>The interpolated value.</returns>
    public static Quaternion Slerp(in Quaternion quaternion1, in Quaternion quaternion2, float amount)
    {
        // if either quaternion is zero, return the other
        if (quaternion1.LengthSquared == 0)
        {
            if (quaternion2.LengthSquared == 0)
                return Quaternion.Identity;

            return quaternion2;
        }
        else if (quaternion2.LengthSquared == 0)
            return quaternion1;

        // slerp quaternion
        var cosOmega = Quaternion.Dot(quaternion1, quaternion2);
        var flip = false;
        if (cosOmega < 0)
        {
            flip = true;
            cosOmega = -cosOmega;
        }

        float amountA;
        float amountB;
        if (cosOmega > .999f)
        {
            // too close, do regular linear interpolation
            amountA = 1 - amount;
            amountB = (flip) ? -amount : amount;
        }
        else
        {
            // do proper slerp
            var omega = MathF.Acos(cosOmega);
            var inverseSinOmega = 1 / MathF.Sin(omega);

            amountA = MathF.Sin((1 - amount) * omega) * inverseSinOmega;
            amountB = (flip)
                ? -MathF.Sin(amount * omega) * inverseSinOmega
                : MathF.Sin(amount * omega) * inverseSinOmega;
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
    public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
    {
        // validate axis
        if (axis.LengthSquared == 0)
            return Quaternion.Identity;
        axis.Normalise();

        // create quaternion
        var halfAngle = MathsHelper.DegreesToRadians(angle) / 2;
        var sinHalfAngle = MathF.Sin(halfAngle);
        var cosHalfAngle = MathF.Cos(halfAngle);

        return new Quaternion(axis * sinHalfAngle, cosHalfAngle).Normalised;
    }

    /// <summary>Creates a quaternion from euler angles.</summary>
    /// <param name="eulerAngles">The clockwise euler angles, in degrees.</param>
    /// <returns>The created quaternion.</returns>
    public static Quaternion CreateFromEulerAngles(in Vector3 eulerAngles) => Quaternion.CreateFromEulerAngles(eulerAngles.X, eulerAngles.Y, eulerAngles.Z);

    /// <summary>Creates a quaternion from euler angles.</summary>
    /// <param name="x">The clockwise angle, in degrees, around the X axis.</param>
    /// <param name="y">The clockwise angle, in degrees, around the Y axis.</param>
    /// <param name="z">The clockwise angle, in degrees, around the Z axis.</param>
    /// <returns>The created quaterion.</returns>
    public static Quaternion CreateFromEulerAngles(float x, float y, float z)
    {
        var halfX = MathsHelper.DegreesToRadians(x) / 2;
        var halfY = MathsHelper.DegreesToRadians(y) / 2;
        var halfZ = MathsHelper.DegreesToRadians(z) / 2;

        var sinHalfX = MathF.Sin(halfX);
        var sinHalfY = MathF.Sin(halfY);
        var sinHalfZ = MathF.Sin(halfZ);
        var cosHalfX = MathF.Cos(halfX);
        var cosHalfY = MathF.Cos(halfY);
        var cosHalfZ = MathF.Cos(halfZ);

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
    /// <summary>Multiplies two quaternions together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Quaternion operator *(Quaternion left, Quaternion right)
    {
        var cross = Vector3.Cross(left.XYZ, right.XYZ);
        var dot = Vector3.Dot(left.XYZ, right.XYZ);

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
    public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2) => quaternion1.X == quaternion2.X && quaternion1.Y == quaternion2.Y && quaternion1.Z == quaternion2.Z && quaternion1.W == quaternion2.W;

    /// <summary>Checks two quaternions for inequality.</summary>
    /// <param name="quaternion1">The first quaternion.</param>
    /// <param name="quaternion2">The second quaternion.</param>
    /// <returns><see langword="true"/> if the quaternions are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2) => !(quaternion1 == quaternion2);

    /// <summary>Converts a quaternion to a tuple.</summary>
    /// <param name="quaternion">The quaternion to convert.</param>
    public static implicit operator (float X, float Y, float Z, float W)(Quaternion quaternion) => (quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

    /// <summary>Converts a tuple to a quaternion.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Quaternion((float X, float Y, float Z, float W) tuple) => new(tuple.X, tuple.Y, tuple.Z, tuple.W);

    /// <summary>Converts a <see cref="Quaternion"/> to a <see cref="QuaternionD"/>.</summary>
    /// <param name="quaternion">The quaternion to convert.</param>
    public static implicit operator QuaternionD(Quaternion quaternion) => quaternion.ToQuaternionD();
}

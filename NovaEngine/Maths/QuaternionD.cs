using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a quaternion using double-precision floating-point numbers.</summary>
    public struct QuaternionD
    {
        /*********
        ** Fields
        *********/
        /// <summary>The X component of the vector part of the quaternion.</summary>
        public double X;

        /// <summary>The Y component of the vector part of the quaternion.</summary>
        public double Y;

        /// <summary>The Z component of the vector part of the quaternion.</summary>
        public double Z;

        /// <summary>The W component of the quaternion.</summary>
        public double W;


        /*********
        ** Accessors
        *********/
        /// <summary>Whether the quaternion is an identity quaternion.</summary>
        public bool IsIdentity => this == Identity;

        /// <summary>The length of the quaternion.</summary>
        public double Length => Math.Sqrt(LengthSquared);

        /// <summary>The sqaured length of the quaternion.</summary>
        /// <remarks>This is preferred for comparison as it avoids the square root operation.</remarks>
        public double LengthSquared => X * X + Y * Y + Z * Z + W * W;

        /// <summary>The quaternion with unit length.</summary>
        public QuaternionD Normalised
        {
            get
            {
                var quaternion = this;
                quaternion.Normalise();
                return quaternion;
            }
        }

        /// <summary>The inverse of the quaternion.</summary>
        public QuaternionD Inverted
        {
            get
            {
                var quaternion = this;
                quaternion.Invert();
                return quaternion;
            }
        }

        /// <summary>The conjugate of the quaternion.</summary>
        public QuaternionD Conjugate => new(-X, -Y, -Z, W);

        /// <summary>Gets a quaternion with (X, Y, Z, W) = (0, 0, 0, 1), which represents no rotation.</summary>
        public static QuaternionD Identity => new(0, 0, 0, 1);

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
        public QuaternionD(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="vector">The vector part of the quaternion.</param>
        /// <param name="w">The W component of the quaternion.</param>
        public QuaternionD(Vector3D vector, double w = 1)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = w;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="vector">The quaternion components.</param>
        public QuaternionD(Vector4D vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = vector.W;
        }

        /// <summary>Converts the quaternion to unit length.</summary> // TODO: change 'Converts' to 'Scales' (same for vector classes)
        public void Normalise()
        {
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
            X = -X * invertedLengthSquared;
            Y = -Y * invertedLengthSquared;
            Z = -Z * invertedLengthSquared;
            W = -W * invertedLengthSquared;
        }

        /// <summary>Gets the axis and angle that the quaternion represents.</summary>
        /// <param name="axis">The axis.</param>
        /// <param name="angle">The angle, in radians.</param>
        public void GetAxisAngle(out Vector3D axis, out double angle)
        {
            var quaternion = this;
            if (Math.Abs(quaternion.W) > 1)
                quaternion.Normalise();

            angle = 2 * Math.Acos(quaternion.W);
            var denominator = Math.Sqrt(1 - quaternion.W * quaternion.W);
            if (denominator == 0)
                axis = Vector3D.UnitX;
            else
                axis = new Vector3D(quaternion.X, quaternion.Y, quaternion.Z) / denominator;
        }

        /// <summary>Gets the quaternion as a <see cref="Quaternion"/>.</summary>
        /// <returns>The quaternion as a <see cref="Quaternion"/>.</returns>
        public Quaternion ToQuaternion() => new((float)X, (float)Y, (float)Z, (float)W);

        /// <inheritdoc/>
        public bool Equals(QuaternionD other) => this == other;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is QuaternionD quaternion && this == quaternion;

        /// <inheritdoc/>
        public override int GetHashCode() => (X, Y, Z, W).GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => $"<X: {X}, Y: {Y}, Z: {Z}, W: {W}>";

        /// <summary>Calculates the dot product of two quaternions.</summary>
        /// <param name="quaternion1">The first quaternion.</param>
        /// <param name="quaternion2">The second quaternion.</param>
        /// <returns>The dot product of <paramref name="quaternion1"/> and <paramref name="quaternion2"/>.</returns>
        public static double Dot(QuaternionD quaternion1, QuaternionD quaternion2) => quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;

        /// <summary>Interpolates between two values, using spherical linear interpolation.</summary>
        /// <param name="quaternion1">The source value.</param>
        /// <param name="quaternion2">The destination value.</param>
        /// <param name="amount">The amount to interpolate between <paramref name="value1"/> and <paramref name="value2"/>.</param>
        /// <returns>The interpolated value.</returns>
        public static QuaternionD Slerp(QuaternionD quaternion1, QuaternionD quaternion2, double amount)
        {
            // if either quaternion is zero, return the other
            if (quaternion1.LengthSquared == 0)
            {
                if (quaternion2.LengthSquared == 0)
                    return QuaternionD.Identity;

                return quaternion2;
            }
            else if (quaternion2.LengthSquared == 0)
                return quaternion1;

            // slerp quaternion
            var cosOmega = QuaternionD.Dot(quaternion1, quaternion2);
            var flip = false;
            if (cosOmega < 0)
            {
                flip = true;
                cosOmega = -cosOmega;
            }

            double amountA;
            double amountB;
            if (cosOmega > .999f)
            {
                // too close, do regular linear interpolation
                amountA = 1 - amount;
                amountB = (flip) ? -amount : amount;
            }
            else
            {
                // do proper slerp
                var omega = Math.Acos(cosOmega);
                var inverseSinOmega = 1 / Math.Sin(omega);

                amountA = Math.Sin((1 - amount) * omega) * inverseSinOmega;
                amountB = (flip)
                    ? -Math.Sin(amount * omega) * inverseSinOmega
                    : Math.Sin(amount * omega) * inverseSinOmega;
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
        /// <param name="angle">The angle, in radians, to rotate around the axis.</param>
        /// <returns>The created quaternion.</returns>
        public static QuaternionD CreateFromAxisAngle(Vector3D axis, double angle)
        {
            // validate axis
            if (axis.LengthSquared == 0)
                return QuaternionD.Identity;
            axis.Normalise();

            // create quaternion
            var halfAngle = angle / 2;
            var sinHalfAngle = Math.Sin(halfAngle);
            var cosHalfAngle = Math.Cos(halfAngle);

            return new(
                x: axis.X * sinHalfAngle,
                y: axis.Y * sinHalfAngle,
                z: axis.Z * sinHalfAngle,
                w: cosHalfAngle
            );
        }

        /// <summary>Creates a quaternion from a pitch, yaw, and roll.</summary>
        /// <param name="pitch">The angle, in radians, around the X axis.</param>
        /// <param name="yaw">The angle, in radians, around the Y axis.</param>
        /// <param name="roll">The angle, in radians, around the Z axis.</param>
        /// <returns>The created quaternion.</returns>
        public static QuaternionD CreateFromPitchYawRoll(double pitch, double yaw, double roll)
        {
            var halfPitch = pitch / 2;
            var halfYaw = yaw / 2;
            var halfRoll = roll / 2;

            var sinHalfPitch = Math.Sin(halfPitch);
            var sinHalfYaw = Math.Sin(halfYaw);
            var sinHalfRoll = Math.Sin(halfRoll);
            var cosHalfPitch = Math.Cos(halfPitch);
            var cosHalfYaw = Math.Cos(halfYaw);
            var cosHalfRoll = Math.Cos(halfRoll);

            return new(
                x: cosHalfYaw * sinHalfPitch * cosHalfRoll + sinHalfYaw * cosHalfPitch * sinHalfRoll,
                y: sinHalfYaw * cosHalfPitch * cosHalfRoll - cosHalfYaw * sinHalfPitch * sinHalfRoll,
                z: cosHalfYaw * cosHalfPitch * sinHalfRoll - sinHalfYaw * sinHalfPitch * cosHalfRoll,
                w: cosHalfYaw * cosHalfPitch * cosHalfRoll + sinHalfYaw * sinHalfPitch * sinHalfRoll
            );
        }

        /// <summary>Creates a quaternion from euler angles.</summary>
        /// <param name="eulerAngles">The euler angles, in radians.</param>
        /// <returns>The created quaternion.</returns>
        public static QuaternionD CreateFromEulerAngles(Vector3D eulerAngles) => QuaternionD.CreateFromPitchYawRoll(eulerAngles.X, eulerAngles.Y, eulerAngles.Z);


        /*********
        ** Operators
        *********/
        /// <summary>Adds two quaternions together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static QuaternionD operator +(QuaternionD left, QuaternionD right)
        {
            return new(
                x: left.X + right.X,
                y: left.Y + right.Y,
                z: left.Z + right.Z,
                w: left.W + right.W
            );
        }

        /// <summary>Subtracts a quaternion from another quaternion.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static QuaternionD operator -(QuaternionD left, QuaternionD right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        /// <summary>Flips the sign of each component of a quaternion.</summary>
        /// <param name="quaternion">The quaternion to flip the component signs of.</param>
        /// <returns><paramref name="quaternion"/> with the sign of its components flipped.</returns>
        public static QuaternionD operator -(QuaternionD quaternion) => quaternion * -1;

        /// <summary>Multiplies a quaternion by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static QuaternionD operator *(QuaternionD left, double right) => new(left.X * right, left.Y * right, left.Z * right, left.W * right);

        /// <summary>Multiplies a quaternion by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static QuaternionD operator *(double left, QuaternionD right) => right * left;

        /// <summary>Multiplies two quaternions together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static QuaternionD operator *(QuaternionD left, QuaternionD right)
        {
            var cross = Vector3D.Cross(new(left.X, left.Y, left.Z), new(right.X, right.Y, right.Z));
            var dot = QuaternionD.Dot(left, right);

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
        public static bool operator ==(QuaternionD quaternion1, QuaternionD quaternion2) => quaternion1.X == quaternion2.X && quaternion1.Y == quaternion2.Y && quaternion1.Z == quaternion2.Z && quaternion1.W == quaternion2.W;

        /// <summary>Checks two quaternions for inequality.</summary>
        /// <param name="quaternion1">The first quaternion.</param>
        /// <param name="quaternion2">The second quaternion.</param>
        /// <returns><see langword="true"/> if the quaternions are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(QuaternionD quaternion1, QuaternionD quaternion2) => !(quaternion1 == quaternion2);

        /// <summary>Converts a quaternion to a tuple.</summary>
        /// <param name="quaternion">The quaternion to convert.</param>
        public static implicit operator (double X, double Y, double Z, double W)(QuaternionD quaternion) => (quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>Converts a tuple to a quaternion.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator QuaternionD((double X, double Y, double Z, double W) tuple) => new(tuple.X, tuple.Y, tuple.Z, tuple.W);

        /// <summary>Converts a <see cref="QuaternionD"/> to a <see cref="Quaternion"/>.</summary>
        /// <param name="quaternion">The quaternion to convert.</param>
        public static explicit operator Quaternion(QuaternionD quaternion) => quaternion.ToQuaternion();
    }
}

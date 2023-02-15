using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a 3x4 matrix using floating-point numbers.</summary>
public struct Matrix3x4<T> : IEquatable<Matrix3x4<T>>
    where T : IFloatingPoint<T>, IRootFunctions<T>, ITrigonometricFunctions<T>, IConvertible
{
    /*********
    ** Fields
    *********/
    /// <summary>The first element of the first row.</summary>
    public T M11;

    /// <summary>The second element of the first row.</summary>
    public T M12;

    /// <summary>The third element of the first row.</summary>
    public T M13;

    /// <summary>The fourth element of the first row.</summary>
    public T M14;

    /// <summary>The first element of the second row.</summary>
    public T M21;

    /// <summary>The second element of the second row.</summary>
    public T M22;

    /// <summary>The third element of the second row.</summary>
    public T M23;

    /// <summary>The fourth element of the second row.</summary>
    public T M24;

    /// <summary>The first element of the third row.</summary>
    public T M31;

    /// <summary>The second element of the third row.</summary>
    public T M32;

    /// <summary>The third element of the third row.</summary>
    public T M33;

    /// <summary>The fourth element of the third row.</summary>
    public T M34;


    /*********
    ** Properties
    *********/
    /// <summary>The trace of the matrix (the sum of the values along the diagonal).</summary>
    public readonly T Trace => M11 + M22 + M33;

    /// <summary>The diagonal of the matrix.</summary>
    public Vector3<T> Diagonal
    {
        readonly get => new(M11, M22, M33);
        set
        {
            M11 = value.X;
            M22 = value.Y;
            M33 = value.Z;
        }
    }

    /// <summary>The transposed matrix.</summary>
    public readonly Matrix4x3<T> Transposed => new(M11, M21, M31, M12, M22, M32, M13, M23, M33, M14, M24, M34);

    /// <summary>The matrix without the rotation.</summary>
    public readonly Matrix3x4<T> RotationRemoved
    {
        get
        {
            var matrix = this;
            matrix.RemoveRotation();
            return matrix;
        }
    }

    /// <summary>The matrix without the scale.</summary>
    public readonly Matrix3x4<T> ScaleRemoved
    {
        get
        {
            var matrix = this;
            matrix.RemoveScale();
            return matrix;
        }
    }

    /// <summary>The rotation of the matrix.</summary>
    public readonly Quaternion<T> Rotation
    {
        get
        {
            var copy = this;
            copy.RemoveScale();

            var quaternion = new Quaternion<T>();
            if (Trace > T.Zero)
            {
                var sq = T.CreateChecked(.5f) / T.Sqrt(Trace + T.One);
                quaternion.W = T.CreateChecked(.25f) / sq;
                quaternion.X = (M31 - M23) * sq;
                quaternion.Y = (M13 - M31) * sq;
                quaternion.Z = (M21 - M12) * sq;
            }
            else if (M11 > M22 && M11 > M33)
            {
                var sq = T.CreateChecked(2) * T.Sqrt(T.One + M11 - M22 - M33);
                quaternion.X = T.CreateChecked(.25f) * sq;
                quaternion.Y = (M12 + M21) / sq;
                quaternion.Z = (M13 + M31) / sq;
                quaternion.W = (M32 - M23) / sq;
            }
            else if (M22 > M33)
            {
                var sq = T.CreateChecked(2) * T.Sqrt(T.One + M22 - M11 - M33);
                quaternion.X = (M12 + M21) / sq;
                quaternion.Y = T.CreateChecked(.25f) * sq;
                quaternion.Z = (M23 + M32) / sq;
                quaternion.W = (M13 - M31) / sq;
            }
            else
            {
                var sq = T.CreateChecked(2) * T.Sqrt(T.One + M33 - M11 - M22);
                quaternion.X = (M13 + M31) / sq;
                quaternion.Y = (M23 + M32) / sq;
                quaternion.Z = T.CreateChecked(.25f) * sq;
                quaternion.W = (M21 - M12) / sq;
            }

            return quaternion.Normalised;
        }
    }

    /// <summary>The scale of the matrix.</summary>
    public readonly Vector3<T> Scale => new(Row1.XYZ.Length, Row2.XYZ.Length, Row3.XYZ.Length);

    /// <summary>The first row of the matrix.</summary>
    public Vector4<T> Row1
    {
        readonly get => new(M11, M12, M13, M14);
        set
        {
            M11 = value.X;
            M12 = value.Y;
            M13 = value.Z;
            M14 = value.W;
        }
    }

    /// <summary>The second row of the matrix.</summary>
    public Vector4<T> Row2
    {
        readonly get => new(M21, M22, M23, M24);
        set
        {
            M21 = value.X;
            M22 = value.Y;
            M23 = value.Z;
            M24 = value.W;
        }
    }

    /// <summary>The third row of the matrix.</summary>
    public Vector4<T> Row3
    {
        readonly get => new(M31, M32, M33, M34);
        set
        {
            M31 = value.X;
            M32 = value.Y;
            M33 = value.Z;
            M34 = value.W;
        }
    }

    /// <summary>The first column of the matrix.</summary>
    public Vector3<T> Column1
    {
        readonly get => new(M11, M21, M31);
        set
        {
            M11 = value.X;
            M21 = value.Y;
            M31 = value.Z;
        }
    }

    /// <summary>The second column of the matrix.</summary>
    public Vector3<T> Column2
    {
        readonly get => new(M12, M22, M32);
        set
        {
            M12 = value.X;
            M22 = value.Y;
            M32 = value.Z;
        }
    }

    /// <summary>The third column of the matrix.</summary>
    public Vector3<T> Column3
    {
        readonly get => new(M13, M23, M33);
        set
        {
            M13 = value.X;
            M23 = value.Y;
            M33 = value.Z;
        }
    }

    /// <summary>The fourth column of the matrix.</summary>
    public Vector3<T> Column4
    {
        readonly get => new(M14, M24, M34);
        set
        {
            M14 = value.X;
            M24 = value.Y;
            M34 = value.Z;
        }
    }

    /// <summary>A zero matrix.</summary>
    public static Matrix3x4<T> Zero => new(T.Zero);

    /// <summary>Gets or sets the value at a specified position.</summary>
    /// <param name="index">The position index.</param>
    /// <returns>The value at the specified position.</returns>
    public T this[int index]
    {
        readonly get
        {
            if (index < 0 || index > 11)
                throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 => 11 (inclusive)");

            return index switch
            {
                0 => M11,
                1 => M12,
                2 => M13,
                3 => M14,
                4 => M21,
                5 => M22,
                6 => M23,
                7 => M24,
                8 => M31,
                9 => M32,
                10 => M33,
                _ => M34,
            };
        }
        set
        {
            if (index < 0 || index > 11)
                throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 => 11 (inclusive)");

            switch (index)
            {
                case 0: M11 = value; break;
                case 1: M12 = value; break;
                case 2: M13 = value; break;
                case 3: M14 = value; break;
                case 4: M21 = value; break;
                case 5: M22 = value; break;
                case 6: M23 = value; break;
                case 7: M24 = value; break;
                case 8: M31 = value; break;
                case 9: M32 = value; break;
                case 10: M33 = value; break;
                default: M34 = value; break;
            }
        }
    }

    /// <summary>Gets or sets the value at a specified row and column.</summary>
    /// <param name="rowIndex">The row index.</param>
    /// <param name="columnIndex">The column index.</param>
    /// <returns>The value at the specified position.</returns>
    public T this[int rowIndex, int columnIndex]
    {
        readonly get
        {
            if (rowIndex < 0 || rowIndex > 2)
                throw new IndexOutOfRangeException($"{nameof(rowIndex)} must be between 0 => 2 (inclusive)");
            if (columnIndex < 0 || columnIndex > 3)
                throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 3 (inclusive)");

            return (rowIndex, columnIndex) switch
            {
                (0, 0) => M11,
                (0, 1) => M12,
                (0, 2) => M13,
                (0, 3) => M14,
                (1, 0) => M21,
                (1, 1) => M22,
                (1, 2) => M23,
                (1, 3) => M24,
                (2, 0) => M31,
                (2, 1) => M32,
                (2, 2) => M33,
                _ => M34,
            };
        }
        set
        {
            if (rowIndex < 0 || rowIndex > 2)
                throw new IndexOutOfRangeException($"{nameof(rowIndex)} must be between 0 => 2 (inclusive)");
            if (columnIndex < 0 || columnIndex > 3)
                throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 3 (inclusive)");

            switch ((rowIndex, columnIndex))
            {
                case (0, 0): M11 = value; break;
                case (0, 1): M12 = value; break;
                case (0, 2): M13 = value; break;
                case (0, 3): M14 = value; break;
                case (1, 0): M21 = value; break;
                case (1, 1): M22 = value; break;
                case (1, 2): M23 = value; break;
                case (1, 3): M24 = value; break;
                case (2, 0): M31 = value; break;
                case (2, 1): M32 = value; break;
                case (2, 2): M33 = value; break;
                default: M34 = value; break;
            }
        }
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="value">The value of all the matrix components.</param>
    public Matrix3x4(T value)
    {
        M11 = value;
        M12 = value;
        M13 = value;
        M14 = value;
        M21 = value;
        M22 = value;
        M23 = value;
        M24 = value;
        M31 = value;
        M32 = value;
        M33 = value;
        M34 = value;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="m11">The first element of the first row.</param>
    /// <param name="m12">The second element of the first row.</param>
    /// <param name="m13">The third element of the first row.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m21">The first element of the second row.</param>
    /// <param name="m22">The second element of the second row.</param>
    /// <param name="m23">The third element of the second row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    public Matrix3x4(T m11, T m12, T m13, T m14, T m21, T m22, T m23, T m24, T m31, T m32, T m33, T m34)
    {
        M11 = m11;
        M12 = m12;
        M13 = m13;
        M14 = m14;
        M21 = m21;
        M22 = m22;
        M23 = m23;
        M24 = m24;
        M31 = m31;
        M32 = m32;
        M33 = m33;
        M34 = m34;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="row1">The first row of the matrix.</param>
    /// <param name="row2">The second row of the matrix.</param>
    /// <param name="row3">The third row of the matrix.</param>
    public Matrix3x4(in Vector4<T> row1, in Vector4<T> row2, in Vector4<T> row3)
    {
        M11 = row1.X;
        M12 = row1.Y;
        M13 = row1.Z;
        M14 = row1.W;
        M21 = row2.X;
        M22 = row2.Y;
        M23 = row2.Z;
        M24 = row2.W;
        M31 = row3.X;
        M32 = row3.Y;
        M33 = row3.Z;
        M34 = row3.W;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 2x2 matrix.</param>
    /// <param name="m13">The third element of the first row.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m23">The third element of the second row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    public Matrix3x4(in Matrix2x2<T> matrix, T m13, T m14, T m23, T m24, T m31, T m32, T m33, T m34)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = m13;
        M14 = m14;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = m23;
        M24 = m24;
        M31 = m31;
        M32 = m32;
        M33 = m33;
        M34 = m34;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 2x3 matrix.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    public Matrix3x4(in Matrix2x3<T> matrix, T m14, T m24, T m31, T m32, T m33, T m34)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = matrix.M13;
        M14 = m14;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = matrix.M23;
        M24 = m24;
        M31 = m31;
        M32 = m32;
        M33 = m33;
        M34 = m34;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 2x4 matrix.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    public Matrix3x4(in Matrix2x4<T> matrix, T m31, T m32, T m33, T m34)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = matrix.M13;
        M14 = matrix.M14;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = matrix.M23;
        M24 = matrix.M24;
        M31 = m31;
        M32 = m32;
        M33 = m33;
        M34 = m34;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 3x2 matrix.</param>
    /// <param name="m13">The third element of the first row.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m23">The third element of the second row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    public Matrix3x4(in Matrix3x2<T> matrix, T m13, T m14, T m23, T m24, T m33, T m34)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = m13;
        M14 = m14;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = m23;
        M24 = m24;
        M31 = matrix.M31;
        M32 = matrix.M32;
        M33 = m33;
        M34 = m34;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 3x3 matrix.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    public Matrix3x4(in Matrix3x3<T> matrix, T m14, T m24, T m34)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = matrix.M13;
        M14 = m14;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = matrix.M23;
        M24 = m24;
        M31 = matrix.M31;
        M32 = matrix.M32;
        M33 = matrix.M33;
        M34 = m34;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Removes the rotation from the matrix.</summary>
    public void RemoveRotation()
    {
        Row1 = new(Row1.Length, T.Zero, T.Zero, M14);
        Row2 = new(T.Zero, Row2.Length, T.Zero, M24);
        Row3 = new(T.Zero, T.Zero, Row3.Length, M34);
    }

    /// <summary>Removes the scale from the matrix.</summary>
    public void RemoveScale()
    {
        Row1 = new(Row1.XYZ.Normalised, M14);
        Row2 = new(Row2.XYZ.Normalised, M24);
        Row3 = new(Row3.XYZ.Normalised, M34);
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="other">The matrix to check equality with.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Matrix3x4<T> other) => this == other;

    /// <summary>Checks the matrix and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the matrix and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Matrix3x4<T> matrix && this == matrix;

    /// <summary>Retrieves the hash code of the matrix.</summary>
    /// <returns>The hash code of the matrix.</returns>
    public readonly override int GetHashCode() => (M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34).GetHashCode();

    /// <summary>Calculates a string representation of the matrix.</summary>
    /// <returns>A string representation of the matrix.</returns>
    public readonly override string ToString() => $"<M11: {M11}, M12: {M12}, M13: {M13}, M14: {M14}, M21: {M21}, M22: {M22}, M23: {M23}, M24: {M24}, M31: {M31}, M32: {M32}, M33: {M33}, M34: {M34}>";

    /// <summary>Creates a rotation matrix from an axis and an angle.</summary>
    /// <param name="axis">The axis to rotate around.</param>
    /// <param name="angle">The angle, in degrees, to rotate around the axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix3x4<T> CreateFromAxisAngle(Vector3<T> axis, T angle) => new(Matrix3x3<T>.CreateFromAxisAngle(axis, angle), T.Zero, T.Zero, T.Zero);

    /// <summary>Creates a rotation matrix from a quaternion.</summary>
    /// <param name="quaternion">The quaternion to create a rotation matrix from.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix3x4<T> CreateFromQuaternion(Quaternion<T> quaternion) => new(Matrix3x3<T>.CreateFromQuaternion(quaternion), T.Zero, T.Zero, T.Zero);

    /// <summary>Creates a rotation matrix for a rotation about the X axis.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix3x4<T> CreateRotationX(T angle)
    {
        angle = MathsHelper<T>.DegreesToRadians(angle);
        var sinAngle = T.Sin(angle);
        var cosAngle = T.Cos(angle);

        return new()
        {
            M11 = T.One,
            M22 = cosAngle,
            M23 = -sinAngle,
            M32 = sinAngle,
            M33 = cosAngle
        };
    }

    /// <summary>Creates a rotation matrix for a rotation about the Y axis.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix3x4<T> CreateRotationY(T angle)
    {
        angle = MathsHelper<T>.DegreesToRadians(angle);
        var sinAngle = T.Sin(angle);
        var cosAngle = T.Cos(angle);

        return new()
        {
            M11 = cosAngle,
            M13 = sinAngle,
            M22 = T.One,
            M31 = -sinAngle,
            M33 = cosAngle
        };
    }

    /// <summary>Creates a rotation matrix for a rotation about the Z axis.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix3x4<T> CreateRotationZ(T angle)
    {
        angle = MathsHelper<T>.DegreesToRadians(angle);
        var sinAngle = T.Sin(angle);
        var cosAngle = T.Cos(angle);

        return new()
        {
            M11 = cosAngle,
            M12 = -sinAngle,
            M21 = sinAngle,
            M22 = cosAngle,
            M33 = T.One
        };
    }

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The uniform scale factor.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix3x4<T> CreateScale(T scale) => new(scale, T.Zero, T.Zero, T.Zero, T.Zero, scale, T.Zero, T.Zero, T.Zero, T.Zero, scale, T.Zero);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="x">The scale factor of the X axis.</param>
    /// <param name="y">The scale factor of the Y axis.</param>
    /// <param name="z">The scale factor of the Z axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix3x4<T> CreateScale(T x, T y, T z) => new(x, T.Zero, T.Zero, T.Zero, T.Zero, y, T.Zero, T.Zero, T.Zero, T.Zero, z, T.Zero);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The scale factor of the X, Y, and Z axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix3x4<T> CreateScale(Vector3<T> scale) => new(scale.X, T.Zero, T.Zero, T.Zero, T.Zero, scale.Y, T.Zero, T.Zero, T.Zero, T.Zero, scale.Z, T.Zero);


    /*********
    ** Operators
    *********/
    /// <summary>Adds two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Matrix3x4<T> operator +(Matrix3x4<T> left, Matrix3x4<T> right)
    {
        return new(
            m11: left.M11 + right.M11,
            m12: left.M12 + right.M12,
            m13: left.M13 + right.M13,
            m14: left.M14 + right.M14,
            m21: left.M21 + right.M21,
            m22: left.M22 + right.M22,
            m23: left.M23 + right.M23,
            m24: left.M24 + right.M24,
            m31: left.M31 + right.M31,
            m32: left.M32 + right.M32,
            m33: left.M33 + right.M33,
            m34: left.M34 + right.M34
        );
    }

    /// <summary>Subtracts a matrix from another matrix.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Matrix3x4<T> operator -(Matrix3x4<T> left, Matrix3x4<T> right)
    {
        return new(
            m11: left.M11 - right.M11,
            m12: left.M12 - right.M12,
            m13: left.M13 - right.M13,
            m14: left.M14 - right.M14,
            m21: left.M21 - right.M21,
            m22: left.M22 - right.M22,
            m23: left.M23 - right.M23,
            m24: left.M24 - right.M24,
            m31: left.M31 - right.M31,
            m32: left.M32 - right.M32,
            m33: left.M33 - right.M33,
            m34: left.M34 - right.M34
        );
    }

    /// <summary>Flips the sign of each component of a matrix.</summary>
    /// <param name="matrix">The matrix to flip the component signs of.</param>
    /// <returns><paramref name="matrix"/> with the sign of its components flipped.</returns>
    public static Matrix3x4<T> operator -(Matrix3x4<T> matrix) => matrix * T.CreateChecked(-1);

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix3x4<T> operator *(T left, Matrix3x4<T> right)
    {
        return new(
            m11: left * right.M11,
            m12: left * right.M12,
            m13: left * right.M13,
            m14: left * right.M14,
            m21: left * right.M21,
            m22: left * right.M22,
            m23: left * right.M23,
            m24: left * right.M24,
            m31: left * right.M31,
            m32: left * right.M32,
            m33: left * right.M33,
            m34: left * right.M34
        );
    }

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix3x4<T> operator *(Matrix3x4<T> left, T right) => right * left;

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix3x2<T> operator *(Matrix3x4<T> left, Matrix4x2<T> right)
    {
        return new(
            m11: Vector4<T>.Dot(left.Row1, right.Column1),
            m12: Vector4<T>.Dot(left.Row1, right.Column2),
            m21: Vector4<T>.Dot(left.Row2, right.Column1),
            m22: Vector4<T>.Dot(left.Row2, right.Column2),
            m31: Vector4<T>.Dot(left.Row3, right.Column1),
            m32: Vector4<T>.Dot(left.Row3, right.Column2)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix3x3<T> operator *(Matrix3x4<T> left, Matrix4x3<T> right)
    {
        return new(
            m11: Vector4<T>.Dot(left.Row1, right.Column1),
            m12: Vector4<T>.Dot(left.Row1, right.Column2),
            m13: Vector4<T>.Dot(left.Row1, right.Column3),
            m21: Vector4<T>.Dot(left.Row2, right.Column1),
            m22: Vector4<T>.Dot(left.Row2, right.Column2),
            m23: Vector4<T>.Dot(left.Row2, right.Column3),
            m31: Vector4<T>.Dot(left.Row3, right.Column1),
            m32: Vector4<T>.Dot(left.Row3, right.Column2),
            m33: Vector4<T>.Dot(left.Row3, right.Column3)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix3x4<T> operator *(Matrix3x4<T> left, Matrix4x4<T> right)
    {
        return new(
            m11: Vector4<T>.Dot(left.Row1, right.Column1),
            m12: Vector4<T>.Dot(left.Row1, right.Column2),
            m13: Vector4<T>.Dot(left.Row1, right.Column3),
            m14: Vector4<T>.Dot(left.Row1, right.Column4),
            m21: Vector4<T>.Dot(left.Row2, right.Column1),
            m22: Vector4<T>.Dot(left.Row2, right.Column2),
            m23: Vector4<T>.Dot(left.Row2, right.Column3),
            m24: Vector4<T>.Dot(left.Row2, right.Column4),
            m31: Vector4<T>.Dot(left.Row3, right.Column1),
            m32: Vector4<T>.Dot(left.Row3, right.Column2),
            m33: Vector4<T>.Dot(left.Row3, right.Column3),
            m34: Vector4<T>.Dot(left.Row3, right.Column4)
        );
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Matrix3x4<T> matrix1, Matrix3x4<T> matrix2)
    {
        return matrix1.M11 == matrix2.M11
            && matrix1.M12 == matrix2.M12
            && matrix1.M13 == matrix2.M13
            && matrix1.M14 == matrix2.M14
            && matrix1.M21 == matrix2.M21
            && matrix1.M22 == matrix2.M22
            && matrix1.M23 == matrix2.M23
            && matrix1.M24 == matrix2.M24
            && matrix1.M31 == matrix2.M31
            && matrix1.M32 == matrix2.M32
            && matrix1.M33 == matrix2.M33
            && matrix1.M34 == matrix2.M34;
    }

    /// <summary>Checks two matrices for inequality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Matrix3x4<T> matrix1, Matrix3x4<T> matrix2) => !(matrix1 == matrix2);

    /// <summary>Converts a matrix to a tuple.</summary>
    /// <param name="matrix">The matrix to convert.</param>
    public static implicit operator (T M11, T M12, T M13, T M14, T M21, T M22, T M23, T M24, T M31, T M32, T M33, T M34)(Matrix3x4<T> matrix) => (matrix.M11, matrix.M12, matrix.M13, matrix.M14, matrix.M21, matrix.M22, matrix.M23, matrix.M24, matrix.M31, matrix.M32, matrix.M33, matrix.M34);

    /// <summary>Converts a tuple to a matrix.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Matrix3x4<T>((T M11, T M12, T M13, T M14, T M21, T M22, T M23, T M24, T M31, T M32, T M33, T M34) tuple) => new(tuple.M11, tuple.M12, tuple.M13, tuple.M14, tuple.M21, tuple.M22, tuple.M23, tuple.M24, tuple.M31, tuple.M32, tuple.M33, tuple.M34);
}

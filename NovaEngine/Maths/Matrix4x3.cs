using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a 4x3 matrix using floating-point numbers.</summary>
public struct Matrix4x3<T> : IEquatable<Matrix4x3<T>>
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

    /// <summary>The first element of the second row.</summary>
    public T M21;

    /// <summary>The second element of the second row.</summary>
    public T M22;

    /// <summary>The third element of the second row.</summary>
    public T M23;

    /// <summary>The first element of the third row.</summary>
    public T M31;

    /// <summary>The second element of the third row.</summary>
    public T M32;

    /// <summary>The third element of the third row.</summary>
    public T M33;

    /// <summary>The first element of the fourth row.</summary>
    public T M41;

    /// <summary>The second element of the fourth row.</summary>
    public T M42;

    /// <summary>The third element of the fourth row.</summary>
    public T M43;


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
    public readonly Matrix3x4<T> Transposed => new(M11, M21, M31, M41, M12, M22, M32, M42, M13, M23, M33, M43);

    /// <summary>The matrix without the translation.</summary>
    public readonly Matrix4x3<T> TranslationRemoved
    {
        get
        {
            var matrix = this;
            matrix.RemoveTranslation();
            return matrix;
        }
    }

    /// <summary>The matrix without the rotation.</summary>
    public readonly Matrix4x3<T> RotationRemoved
    {
        get
        {
            var matrix = this;
            matrix.RemoveRotation();
            return matrix;
        }
    }

    /// <summary>The matrix without the scale.</summary>
    public readonly Matrix4x3<T> ScaleRemoved
    {
        get
        {
            var matrix = this;
            matrix.RemoveScale();
            return matrix;
        }
    }

    /// <summary>The translation of the matrix.</summary>
    public readonly Vector3<T> Translation => new(M41, M42, M43);

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
    public readonly Vector3<T> Scale => new(Row1.Length, Row2.Length, Row3.Length);

    /// <summary>The first row of the matrix.</summary>
    public Vector3<T> Row1
    {
        readonly get => new(M11, M12, M13);
        set
        {
            M11 = value.X;
            M12 = value.Y;
            M13 = value.Z;
        }
    }

    /// <summary>The second row of the matrix.</summary>
    public Vector3<T> Row2
    {
        readonly get => new(M21, M22, M23);
        set
        {
            M21 = value.X;
            M22 = value.Y;
            M23 = value.Z;
        }
    }

    /// <summary>The third row of the matrix.</summary>
    public Vector3<T> Row3
    {
        readonly get => new(M31, M32, M33);
        set
        {
            M31 = value.X;
            M32 = value.Y;
            M33 = value.Z;
        }
    }

    /// <summary>The fourth row of the matrix.</summary>
    public Vector3<T> Row4
    {
        readonly get => new(M41, M42, M43);
        set
        {
            M41 = value.X;
            M42 = value.Y;
            M43 = value.Z;
        }
    }

    /// <summary>The first column of the matrix.</summary>
    public Vector4<T> Column1
    {
        readonly get => new(M11, M21, M31, M41);
        set
        {
            M11 = value.X;
            M21 = value.Y;
            M31 = value.Z;
            M41 = value.W;
        }
    }

    /// <summary>The second column of the matrix.</summary>
    public Vector4<T> Column2
    {
        readonly get => new(M12, M22, M32, M42);
        set
        {
            M12 = value.X;
            M22 = value.Y;
            M32 = value.Z;
            M42 = value.W;
        }
    }

    /// <summary>The third column of the matrix.</summary>
    public Vector4<T> Column3
    {
        readonly get => new(M13, M23, M33, M43);
        set
        {
            M13 = value.X;
            M23 = value.Y;
            M33 = value.Z;
            M43 = value.W;
        }
    }

    /// <summary>A zero matrix.</summary>
    public static Matrix4x3<T> Zero => new(T.Zero);

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
                3 => M21,
                4 => M22,
                5 => M23,
                6 => M31,
                7 => M32,
                8 => M33,
                9 => M41,
                10 => M42,
                _ => M43,
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
                case 3: M21 = value; break;
                case 4: M22 = value; break;
                case 5: M23 = value; break;
                case 6: M31 = value; break;
                case 7: M32 = value; break;
                case 8: M33 = value; break;
                case 9: M41 = value; break;
                case 10: M42 = value; break;
                default: M43 = value; break;
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
            if (rowIndex < 0 || rowIndex > 3)
                throw new IndexOutOfRangeException($"{nameof(rowIndex)} must be between 0 => 3 (inclusive)");
            if (columnIndex < 0 || columnIndex > 2)
                throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 2 (inclusive)");

            return (rowIndex, columnIndex) switch
            {
                (0, 0) => M11,
                (0, 1) => M12,
                (0, 2) => M13,
                (1, 0) => M21,
                (1, 1) => M22,
                (1, 2) => M23,
                (2, 0) => M31,
                (2, 1) => M32,
                (2, 2) => M33,
                (3, 0) => M41,
                (3, 1) => M42,
                _ => M43,
            };
        }
        set
        {
            if (rowIndex < 0 || rowIndex > 3)
                throw new IndexOutOfRangeException($"{nameof(rowIndex)} must be between 0 => 3 (inclusive)");
            if (columnIndex < 0 || columnIndex > 2)
                throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 2 (inclusive)");

            switch ((rowIndex, columnIndex))
            {
                case (0, 0): M11 = value; break;
                case (0, 1): M12 = value; break;
                case (0, 2): M13 = value; break;
                case (1, 0): M21 = value; break;
                case (1, 1): M22 = value; break;
                case (1, 2): M23 = value; break;
                case (2, 0): M31 = value; break;
                case (2, 1): M32 = value; break;
                case (2, 2): M33 = value; break;
                case (3, 0): M41 = value; break;
                case (3, 1): M42 = value; break;
                default: M43 = value; break;
            }
        }
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="value">The value of all the matrix components.</param>
    public Matrix4x3(T value)
    {
        M11 = value;
        M12 = value;
        M13 = value;
        M21 = value;
        M22 = value;
        M23 = value;
        M31 = value;
        M32 = value;
        M33 = value;
        M41 = value;
        M42 = value;
        M43 = value;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="m11">The first element of the first row.</param>
    /// <param name="m12">The second element of the first row.</param>
    /// <param name="m13">The third element of the first row.</param>
    /// <param name="m21">The first element of the second row.</param>
    /// <param name="m22">The second element of the second row.</param>
    /// <param name="m23">The third element of the second row.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    public Matrix4x3(T m11, T m12, T m13, T m21, T m22, T m23, T m31, T m32, T m33, T m41, T m42, T m43)
    {
        M11 = m11;
        M12 = m12;
        M13 = m13;
        M21 = m21;
        M22 = m22;
        M23 = m23;
        M31 = m31;
        M32 = m32;
        M33 = m33;
        M41 = m41;
        M42 = m42;
        M43 = m43;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="row1">The first row of the matrix.</param>
    /// <param name="row2">The second row of the matrix.</param>
    /// <param name="row3">The third row of the matrix.</param>
    /// <param name="row4">The fourth row of the matrix.</param>
    public Matrix4x3(in Vector3<T> row1, in Vector3<T> row2, in Vector3<T> row3, in Vector3<T> row4)
    {
        M11 = row1.X;
        M12 = row1.Y;
        M13 = row1.Z;
        M21 = row2.X;
        M22 = row2.Y;
        M23 = row2.Z;
        M31 = row3.X;
        M32 = row3.Y;
        M33 = row3.Z;
        M41 = row4.X;
        M42 = row4.Y;
        M43 = row4.Z;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 2x2 matrix.</param>
    /// <param name="m13">The third element of the first row.</param>
    /// <param name="m23">The third element of the second row.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    public Matrix4x3(in Matrix2x2<T> matrix, T m13, T m23, T m31, T m32, T m33, T m41, T m42, T m43)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = m13;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = m23;
        M31 = m31;
        M32 = m32;
        M33 = m33;
        M41 = m41;
        M42 = m42;
        M43 = m43;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 2x3 matrix.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    public Matrix4x3(in Matrix2x3<T> matrix, T m31, T m32, T m33, T m41, T m42, T m43)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = matrix.M13;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = matrix.M23;
        M31 = m31;
        M32 = m32;
        M33 = m33;
        M41 = m41;
        M42 = m42;
        M43 = m43;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 3x2 matrix.</param>
    /// <param name="m13">The third element of the first row.</param>
    /// <param name="m23">The third element of the second row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    public Matrix4x3(in Matrix3x2<T> matrix, T m13, T m23, T m33, T m41, T m42, T m43)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = m13;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = m23;
        M31 = matrix.M31;
        M32 = matrix.M32;
        M33 = m33;
        M41 = m41;
        M42 = m42;
        M43 = m43;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 3x3 matrix.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    public Matrix4x3(in Matrix3x3<T> matrix, T m41, T m42, T m43)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = matrix.M13;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = matrix.M23;
        M31 = matrix.M31;
        M32 = matrix.M32;
        M33 = matrix.M33;
        M41 = m41;
        M42 = m42;
        M43 = m43;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 4x2 matrix.</param>
    /// <param name="m13">The third element of the first row.</param>
    /// <param name="m23">The third element of the second row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    public Matrix4x3(in Matrix4x2<T> matrix, T m13, T m23, T m33, T m43)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = m13;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = m23;
        M31 = matrix.M31;
        M32 = matrix.M32;
        M33 = m33;
        M41 = matrix.M41;
        M42 = matrix.M42;
        M43 = m43;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Removes the translation from the matrix.</summary>
    public void RemoveTranslation() => Row4 = Vector3<T>.Zero;

    /// <summary>Removes the rotation from the matrix.</summary>
    public void RemoveRotation()
    {
        Row1 = new(Row1.Length, T.Zero, T.Zero);
        Row2 = new(T.Zero, Row2.Length, T.Zero);
        Row3 = new(T.Zero, T.Zero, Row3.Length);
    }

    /// <summary>Removes the scale from the matrix.</summary>
    public void RemoveScale()
    {
        Row1.Normalise();
        Row2.Normalise();
        Row3.Normalise();
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="other">The matrix to check equality with.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Matrix4x3<T> other) => this == other;

    /// <summary>Checks the matrix and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the matrix and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Matrix4x3<T> matrix && this == matrix;

    /// <summary>Retrieves the hash code of the matrix.</summary>
    /// <returns>The hash code of the matrix.</returns>
    public readonly override int GetHashCode() => (M11, M12, M13, M21, M22, M23, M31, M32, M33, M41, M42, M43).GetHashCode();

    /// <summary>Calculates a string representation of the matrix.</summary>
    /// <returns>A string representation of the matrix.</returns>
    public readonly override string ToString() => $"<M11: {M11}, M12: {M12}, M13: {M13}, M21: {M21}, M22: {M22}, M23: {M23}, M31: {M31}, M32: {M32}, M33: {M33}, M41: {M41}, M42: {M42}, M43: {M43}>";

    /// <summary>Creates a rotation matrix from an axis and an angle.</summary>
    /// <param name="axis">The axis to rotate around.</param>
    /// <param name="angle">The angle, in degrees, to rotate around the axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x3<T> CreateFromAxisAngle(Vector3<T> axis, T angle) => new(Matrix3x3<T>.CreateFromAxisAngle(axis, angle), T.Zero, T.Zero, T.Zero);

    /// <summary>Creates a rotation matrix from a quaternion.</summary>
    /// <param name="quaternion">The quaternion to create a rotation matrix from.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x3<T> CreateFromQuaternion(Quaternion<T> quaternion) => new(Matrix3x3<T>.CreateFromQuaternion(quaternion), T.Zero, T.Zero, T.Zero);

    /// <summary>Creates a rotation matrix for a rotation about the X axis.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x3<T> CreateRotationX(T angle)
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
    public static Matrix4x3<T> CreateRotationY(T angle)
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
    public static Matrix4x3<T> CreateRotationZ(T angle)
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
    public static Matrix4x3<T> CreateScale(T scale) => new(scale, T.Zero, T.Zero, T.Zero, scale, T.Zero, T.Zero, T.Zero, scale, T.Zero, T.Zero, T.Zero);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="x">The scale factor of the X axis.</param>
    /// <param name="y">The scale factor of the Y axis.</param>
    /// <param name="z">The scale factor of the Z axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x3<T> CreateScale(T x, T y, T z) => new(x, T.Zero, T.Zero, T.Zero, y, T.Zero, T.Zero, T.Zero, z, T.Zero, T.Zero, T.Zero);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The scale factor of the X, Y, and Z axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x3<T> CreateScale(Vector3<T> scale) => new(scale.X, T.Zero, T.Zero, T.Zero, scale.Y, T.Zero, T.Zero, T.Zero, scale.Z, T.Zero, T.Zero, T.Zero);


    /*********
    ** Operators
    *********/
    /// <summary>Adds two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Matrix4x3<T> operator +(Matrix4x3<T> left, Matrix4x3<T> right)
    {
        return new(
            m11: left.M11 + right.M11,
            m12: left.M12 + right.M12,
            m13: left.M13 + right.M13,
            m21: left.M21 + right.M21,
            m22: left.M22 + right.M22,
            m23: left.M23 + right.M23,
            m31: left.M31 + right.M31,
            m32: left.M32 + right.M32,
            m33: left.M33 + right.M33,
            m41: left.M41 + right.M41,
            m42: left.M42 + right.M42,
            m43: left.M43 + right.M43
        );
    }

    /// <summary>Subtracts a matrix from another matrix.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Matrix4x3<T> operator -(Matrix4x3<T> left, Matrix4x3<T> right)
    {
        return new(
            m11: left.M11 - right.M11,
            m12: left.M12 - right.M12,
            m13: left.M13 - right.M13,
            m21: left.M21 - right.M21,
            m22: left.M22 - right.M22,
            m23: left.M23 - right.M23,
            m31: left.M31 - right.M31,
            m32: left.M32 - right.M32,
            m33: left.M33 - right.M33,
            m41: left.M41 - right.M41,
            m42: left.M42 - right.M42,
            m43: left.M43 - right.M43
        );
    }

    /// <summary>Flips the sign of each component of a matrix.</summary>
    /// <param name="matrix">The matrix to flip the component signs of.</param>
    /// <returns><paramref name="matrix"/> with the sign of its components flipped.</returns>
    public static Matrix4x3<T> operator -(Matrix4x3<T> matrix) => matrix * T.CreateChecked(-1);

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x3<T> operator *(T left, Matrix4x3<T> right)
    {
        return new(
            m11: left * right.M11,
            m12: left * right.M12,
            m13: left * right.M13,
            m21: left * right.M21,
            m22: left * right.M22,
            m23: left * right.M23,
            m31: left * right.M31,
            m32: left * right.M32,
            m33: left * right.M33,
            m41: left * right.M41,
            m42: left * right.M42,
            m43: left * right.M43
        );
    }

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x3<T> operator *(Matrix4x3<T> left, T right) => right * left;

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x2<T> operator *(Matrix4x3<T> left, Matrix3x2<T> right)
    {
        return new(
            m11: Vector3<T>.Dot(left.Row1, right.Column1),
            m12: Vector3<T>.Dot(left.Row1, right.Column2),
            m21: Vector3<T>.Dot(left.Row2, right.Column1),
            m22: Vector3<T>.Dot(left.Row2, right.Column2),
            m31: Vector3<T>.Dot(left.Row3, right.Column1),
            m32: Vector3<T>.Dot(left.Row3, right.Column2),
            m41: Vector3<T>.Dot(left.Row4, right.Column1),
            m42: Vector3<T>.Dot(left.Row4, right.Column2)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x3<T> operator *(Matrix4x3<T> left, Matrix3x3<T> right)
    {
        return new(
            m11: Vector3<T>.Dot(left.Row1, right.Column1),
            m12: Vector3<T>.Dot(left.Row1, right.Column2),
            m13: Vector3<T>.Dot(left.Row1, right.Column3),
            m21: Vector3<T>.Dot(left.Row2, right.Column1),
            m22: Vector3<T>.Dot(left.Row2, right.Column2),
            m23: Vector3<T>.Dot(left.Row2, right.Column3),
            m31: Vector3<T>.Dot(left.Row3, right.Column1),
            m32: Vector3<T>.Dot(left.Row3, right.Column2),
            m33: Vector3<T>.Dot(left.Row3, right.Column3),
            m41: Vector3<T>.Dot(left.Row4, right.Column1),
            m42: Vector3<T>.Dot(left.Row4, right.Column2),
            m43: Vector3<T>.Dot(left.Row4, right.Column3)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x4<T> operator *(Matrix4x3<T> left, Matrix3x4<T> right)
    {
        return new(
            m11: Vector3<T>.Dot(left.Row1, right.Column1),
            m12: Vector3<T>.Dot(left.Row1, right.Column2),
            m13: Vector3<T>.Dot(left.Row1, right.Column3),
            m14: Vector3<T>.Dot(left.Row1, right.Column4),
            m21: Vector3<T>.Dot(left.Row2, right.Column1),
            m22: Vector3<T>.Dot(left.Row2, right.Column2),
            m23: Vector3<T>.Dot(left.Row2, right.Column3),
            m24: Vector3<T>.Dot(left.Row2, right.Column4),
            m31: Vector3<T>.Dot(left.Row3, right.Column1),
            m32: Vector3<T>.Dot(left.Row3, right.Column2),
            m33: Vector3<T>.Dot(left.Row3, right.Column3),
            m34: Vector3<T>.Dot(left.Row3, right.Column4),
            m41: Vector3<T>.Dot(left.Row4, right.Column1),
            m42: Vector3<T>.Dot(left.Row4, right.Column2),
            m43: Vector3<T>.Dot(left.Row4, right.Column3),
            m44: Vector3<T>.Dot(left.Row4, right.Column4)
        );
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Matrix4x3<T> matrix1, Matrix4x3<T> matrix2)
    {
        return matrix1.M11 == matrix2.M11
            && matrix1.M12 == matrix2.M12
            && matrix1.M13 == matrix2.M13
            && matrix1.M21 == matrix2.M21
            && matrix1.M22 == matrix2.M22
            && matrix1.M23 == matrix2.M23
            && matrix1.M31 == matrix2.M31
            && matrix1.M32 == matrix2.M32
            && matrix1.M33 == matrix2.M33
            && matrix1.M41 == matrix2.M41
            && matrix1.M42 == matrix2.M42
            && matrix1.M43 == matrix2.M43;
    }

    /// <summary>Checks two matrices for inequality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Matrix4x3<T> matrix1, Matrix4x3<T> matrix2) => !(matrix1 == matrix2);

    /// <summary>Converts a matrix to a tuple.</summary>
    /// <param name="matrix">The matrix to convert.</param>
    public static implicit operator (T M11, T M12, T M13, T M21, T M22, T M23, T M31, T M32, T M33, T M41, T M42, T M43)(Matrix4x3<T> matrix) => (matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23, matrix.M31, matrix.M32, matrix.M33, matrix.M41, matrix.M42, matrix.M43);

    /// <summary>Converts a tuple to a matrix.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Matrix4x3<T>((T M11, T M12, T M13, T M21, T M22, T M23, T M31, T M32, T M33, T M41, T M42, T M43) tuple) => new(tuple.M11, tuple.M12, tuple.M13, tuple.M21, tuple.M22, tuple.M23, tuple.M31, tuple.M32, tuple.M33, tuple.M41, tuple.M42, tuple.M43);
}

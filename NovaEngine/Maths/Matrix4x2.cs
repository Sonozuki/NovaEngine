using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a 4x2 matrix using floating-point numbers.</summary>
public struct Matrix4x2<T> : IEquatable<Matrix4x2<T>>
    where T : IFloatingPoint<T>, IRootFunctions<T>, ITrigonometricFunctions<T>, IConvertible
{
    /*********
    ** Fields
    *********/
    /// <summary>The first element of the first row.</summary>
    public T M11;

    /// <summary>The second element of the first row.</summary>
    public T M12;

    /// <summary>The first element of the second row.</summary>
    public T M21;

    /// <summary>The second element of the second row.</summary>
    public T M22;

    /// <summary>The first element of the third row.</summary>
    public T M31;

    /// <summary>The second element of the third row.</summary>
    public T M32;

    /// <summary>The first element of the fourth row.</summary>
    public T M41;

    /// <summary>The second element of the fourth row.</summary>
    public T M42;


    /*********
    ** Accessors
    *********/
    /// <summary>The trace of the matrix (the sum of the values along the diagonal).</summary>
    public readonly T Trace => M11 + M22;

    /// <summary>The diagonal of the matrix.</summary>
    public Vector2<T> Diagonal
    {
        readonly get => new(M11, M22);
        set
        {
            M11 = value.X;
            M22 = value.Y;
        }
    }

    /// <summary>The transposed matrix.</summary>
    public readonly Matrix2x4<T> Transposed => new(M11, M21, M31, M41, M12, M22, M32, M42);

    /// <summary>The first row of the matrix.</summary>
    public Vector2<T> Row1
    {
        readonly get => new(M11, M12);
        set
        {
            M11 = value.X;
            M12 = value.Y;
        }
    }

    /// <summary>The second row of the matrix.</summary>
    public Vector2<T> Row2
    {
        readonly get => new(M21, M22);
        set
        {
            M21 = value.X;
            M22 = value.Y;
        }
    }

    /// <summary>The third row of the matrix.</summary>
    public Vector2<T> Row3
    {
        readonly get => new(M31, M32);
        set
        {
            M31 = value.X;
            M32 = value.Y;
        }
    }

    /// <summary>The fourth row of the matrix.</summary>
    public Vector2<T> Row4
    {
        get => new(M41, M42);
        set
        {
            M41 = value.X;
            M42 = value.Y;
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

    /// <summary>A zero matrix.</summary>
    public static Matrix2x4<T> Zero => new(T.Zero);

    /// <summary>Gets or sets the value at a specified position.</summary>
    /// <param name="index">The position index.</param>
    /// <returns>The value at the specified position.</returns>
    public T this[int index]
    {
        readonly get
        {
            if (index < 0 || index > 7)
                throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 => 7 (inclusive)");

            return index switch
            {
                0 => M11,
                1 => M12,
                2 => M21,
                3 => M22,
                4 => M31,
                5 => M32,
                6 => M41,
                _ => M42,
            };
        }
        set
        {
            if (index < 0 || index > 7)
                throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 => 7 (inclusive)");

            switch (index)
            {
                case 0: M11 = value; break;
                case 1: M12 = value; break;
                case 2: M21 = value; break;
                case 3: M22 = value; break;
                case 4: M31 = value; break;
                case 5: M32 = value; break;
                case 6: M41 = value; break;
                default: M42 = value; break;
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
            if (columnIndex < 0 || columnIndex > 1)
                throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 1 (inclusive)");

            return (rowIndex, columnIndex) switch
            {
                (0, 0) => M11,
                (0, 1) => M12,
                (1, 0) => M21,
                (1, 1) => M22,
                (2, 0) => M31,
                (2, 1) => M32,
                (3, 0) => M41,
                _ => M42,
            };
        }
        set
        {
            if (rowIndex < 0 || rowIndex > 3)
                throw new IndexOutOfRangeException($"{nameof(rowIndex)} must be between 0 => 3 (inclusive)");
            if (columnIndex < 0 || columnIndex > 1)
                throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 1 (inclusive)");

            switch ((rowIndex, columnIndex))
            {
                case (0, 0): M11 = value; break;
                case (0, 1): M12 = value; break;
                case (1, 0): M21 = value; break;
                case (1, 1): M22 = value; break;
                case (2, 0): M31 = value; break;
                case (2, 1): M32 = value; break;
                case (3, 0): M41 = value; break;
                default: M42 = value; break;
            }
        }
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="value">The value of all the matrix components.</param>
    public Matrix4x2(T value)
    {
        M11 = value;
        M12 = value;
        M21 = value;
        M22 = value;
        M31 = value;
        M32 = value;
        M41 = value;
        M42 = value;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="m11">The first element of the first row.</param>
    /// <param name="m12">The second element of the first row.</param>
    /// <param name="m21">The first element of the second row.</param>
    /// <param name="m22">The second element of the second row.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    public Matrix4x2(T m11, T m12, T m21, T m22, T m31, T m32, T m41, T m42)
    {
        M11 = m11;
        M12 = m12;
        M21 = m21;
        M22 = m22;
        M31 = m31;
        M32 = m32;
        M41 = m41;
        M42 = m42;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="row1">The first row of the matrix.</param>
    /// <param name="row2">The second row of the matrix.</param>
    /// <param name="row3">The third row of the matrix.</param>
    /// <param name="row4">The fourth row of the matrix.</param>
    public Matrix4x2(in Vector2<T> row1, in Vector2<T> row2, in Vector2<T> row3, in Vector2<T> row4)
    {
        M11 = row1.X;
        M12 = row1.Y;
        M21 = row2.X;
        M22 = row2.Y;
        M31 = row3.X;
        M32 = row3.Y;
        M41 = row4.X;
        M42 = row4.Y;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 2x2 matrix.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    public Matrix4x2(in Matrix2x2<T> matrix, T m31, T m32, T m41, T m42)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M31 = m31;
        M32 = m32;
        M41 = m41;
        M42 = m42;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 3x2 matrix.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    public Matrix4x2(in Matrix3x2<T> matrix, T m41, T m42)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M31 = matrix.M31;
        M32 = matrix.M32;
        M41 = m41;
        M42 = m42;
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="other">The matrix to check equality with.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Matrix4x2<T> other) => this == other;

    /// <summary>Checks the matrix and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the matrix and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Matrix4x2<T> matrix && this == matrix;

    /// <summary>Retrieves the hash code of the matrix.</summary>
    /// <returns>The hash code of the matrix.</returns>
    public readonly override int GetHashCode() => (M11, M12, M21, M22, M31, M32, M41, M42).GetHashCode();

    /// <summary>Calculates a string representation of the matrix.</summary>
    /// <returns>A string representation of the matrix.</returns>
    public readonly override string ToString() => $"<M11: {M11}, M12: {M12}, M21: {M21}, M22: {M22}, M31: {M31}, M32: {M32}, M41: {M41}, M42: {M42}>";

    /// <summary>Creates a rotation matrix.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x2<T> CreateRotation(T angle) => new(Matrix2x2<T>.CreateRotation(angle), T.Zero, T.Zero, T.Zero, T.Zero);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The uniform scale factor.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x2<T> CreateScale(T scale) => new(Matrix2x2<T>.CreateScale(scale), T.Zero, T.Zero, T.Zero, T.Zero);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="xScale">The scale factor of the X axis.</param>
    /// <param name="yScale">The scale factor of the Y axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x2<T> CreateScale(T xScale, T yScale) => new(Matrix2x2<T>.CreateScale(xScale, yScale), T.Zero, T.Zero, T.Zero, T.Zero);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The scale factor of the X and Y axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x2<T> CreateScale(Vector2<T> scale) => new(Matrix2x2<T>.CreateScale(scale), T.Zero, T.Zero, T.Zero, T.Zero);


    /*********
    ** Operators
    *********/
    /// <summary>Adds two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Matrix4x2<T> operator +(Matrix4x2<T> left, Matrix4x2<T> right)
    {
        return new(
            m11: left.M11 + right.M11,
            m12: left.M12 + right.M12,
            m21: left.M21 + right.M21,
            m22: left.M22 + right.M22,
            m31: left.M31 + right.M31,
            m32: left.M32 + right.M32,
            m41: left.M41 + right.M41,
            m42: left.M42 + right.M42
        );
    }

    /// <summary>Subtracts a matrix from another matrix.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Matrix4x2<T> operator -(Matrix4x2<T> left, Matrix4x2<T> right)
    {
        return new(
            m11: left.M11 - right.M11,
            m12: left.M12 - right.M12,
            m21: left.M21 - right.M21,
            m22: left.M22 - right.M22,
            m31: left.M31 - right.M31,
            m32: left.M32 - right.M32,
            m41: left.M41 - right.M41,
            m42: left.M42 - right.M42
        );
    }

    /// <summary>Flips the sign of each component of a matrix.</summary>
    /// <param name="matrix">The matrix to flip the component signs of.</param>
    /// <returns><paramref name="matrix"/> with the sign of its components flipped.</returns>
    public static Matrix4x2<T> operator -(Matrix4x2<T> matrix) => matrix * T.CreateChecked(-1);

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x2<T> operator *(T left, Matrix4x2<T> right)
    {
        return new(
            m11: left * right.M11,
            m12: left * right.M12,
            m21: left * right.M21,
            m22: left * right.M22,
            m31: left * right.M31,
            m32: left * right.M32,
            m41: left * right.M41,
            m42: left * right.M42
        );
    }

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x2<T> operator *(Matrix4x2<T> left, T right) => right * left;
    
    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x2<T> operator *(Matrix4x2<T> left, Matrix2x2<T> right)
    {
        return new(
            m11: Vector2<T>.Dot(left.Row1, right.Column1),
            m12: Vector2<T>.Dot(left.Row1, right.Column2),
            m21: Vector2<T>.Dot(left.Row2, right.Column1),
            m22: Vector2<T>.Dot(left.Row2, right.Column2),
            m31: Vector2<T>.Dot(left.Row3, right.Column1),
            m32: Vector2<T>.Dot(left.Row3, right.Column2),
            m41: Vector2<T>.Dot(left.Row4, right.Column1),
            m42: Vector2<T>.Dot(left.Row4, right.Column2)
        );
    }
    
    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x3<T> operator *(Matrix4x2<T> left, Matrix2x3<T> right)
    {
        return new(
            m11: Vector2<T>.Dot(left.Row1, right.Column1),
            m12: Vector2<T>.Dot(left.Row1, right.Column2),
            m13: Vector2<T>.Dot(left.Row1, right.Column3),
            m21: Vector2<T>.Dot(left.Row2, right.Column1),
            m22: Vector2<T>.Dot(left.Row2, right.Column2),
            m23: Vector2<T>.Dot(left.Row2, right.Column3),
            m31: Vector2<T>.Dot(left.Row3, right.Column1),
            m32: Vector2<T>.Dot(left.Row3, right.Column2),
            m33: Vector2<T>.Dot(left.Row3, right.Column3),
            m41: Vector2<T>.Dot(left.Row4, right.Column1),
            m42: Vector2<T>.Dot(left.Row4, right.Column2),
            m43: Vector2<T>.Dot(left.Row4, right.Column3)
        );
    }
    
    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x4<T> operator *(Matrix4x2<T> left, Matrix2x4<T> right)
    {
        return new(
            m11: Vector2<T>.Dot(left.Row1, right.Column1),
            m12: Vector2<T>.Dot(left.Row1, right.Column2),
            m13: Vector2<T>.Dot(left.Row1, right.Column3),
            m14: Vector2<T>.Dot(left.Row1, right.Column4),
            m21: Vector2<T>.Dot(left.Row2, right.Column1),
            m22: Vector2<T>.Dot(left.Row2, right.Column2),
            m23: Vector2<T>.Dot(left.Row2, right.Column3),
            m24: Vector2<T>.Dot(left.Row2, right.Column4),
            m31: Vector2<T>.Dot(left.Row3, right.Column1),
            m32: Vector2<T>.Dot(left.Row3, right.Column2),
            m33: Vector2<T>.Dot(left.Row3, right.Column3),
            m34: Vector2<T>.Dot(left.Row3, right.Column4),
            m41: Vector2<T>.Dot(left.Row4, right.Column1),
            m42: Vector2<T>.Dot(left.Row4, right.Column2),
            m43: Vector2<T>.Dot(left.Row4, right.Column3),
            m44: Vector2<T>.Dot(left.Row4, right.Column4)
        );
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Matrix4x2<T> matrix1, Matrix4x2<T> matrix2)
    {
        return matrix1.M11 == matrix2.M11
            && matrix1.M12 == matrix2.M12
            && matrix1.M21 == matrix2.M21
            && matrix1.M22 == matrix2.M22
            && matrix1.M31 == matrix2.M31
            && matrix1.M32 == matrix2.M32
            && matrix1.M41 == matrix2.M41
            && matrix1.M42 == matrix2.M42;
    }

    /// <summary>Checks two matrices for inequality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Matrix4x2<T> matrix1, Matrix4x2<T> matrix2) => !(matrix1 == matrix2);

    /// <summary>Converts a matrix to a tuple.</summary>
    /// <param name="matrix">The matrix to convert.</param>
    public static implicit operator (T M11, T M12, T M21, T M22, T M31, T M32, T M41, T M42)(Matrix4x2<T> matrix) => (matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.M31, matrix.M32, matrix.M41, matrix.M42);

    /// <summary>Converts a tuple to a matrix.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Matrix4x2<T>((T M11, T M12, T M21, T M22, T M31, T M32, T M41, T M42) tuple) => new(tuple.M11, tuple.M12, tuple.M21, tuple.M22, tuple.M31, tuple.M32, tuple.M41, tuple.M42);
}

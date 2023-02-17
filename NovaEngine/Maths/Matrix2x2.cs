using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a 2x2 matrix using floating-point numbers.</summary>
public struct Matrix2x2<T> : IEquatable<Matrix2x2<T>>
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


    /*********
    ** Properties
    *********/
    /// <summary>Whether the matrix is an identity matrix.</summary>
    public readonly bool IsIdentity => this == Identity;

    /// <summary>The determinant of the matrix.</summary>
    public readonly T Determinant => M11 * M22 - M12 * M21;

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
    public readonly Matrix2x2<T> Transposed
    {
        get
        {
            var matrix = this;
            matrix.Transpose();
            return matrix;
        }
    }

    /// <summary>The inverse of the matrix.</summary>
    public readonly Matrix2x2<T> Inverse
    {
        get
        {
            var matrix = this;
            matrix.Invert();
            return matrix;
        }
    }

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

    /// <summary>The first column of the matrix.</summary>
    public Vector2<T> Column1
    {
        readonly get => new(M11, M21);
        set
        {
            M11 = value.X;
            M21 = value.Y;
        }
    }

    /// <summary>The second column of the matrix.</summary>
    public Vector2<T> Column2
    {
        readonly get => new(M12, M22);
        set
        {
            M12 = value.X;
            M22 = value.Y;
        }
    }

    /// <summary>A zero matrix.</summary>
    public static Matrix2x2<T> Zero => new(T.Zero);

    /// <summary>An identity matrix.</summary>
    public static Matrix2x2<T> Identity => new(T.One, T.Zero, T.Zero, T.One);

    /// <summary>Gets or sets the value at a specified position.</summary>
    /// <param name="index">The position index.</param>
    /// <returns>The value at the specified position.</returns>
    public T this[int index]
    {
        readonly get
        {
            if (index < 0 || index > 3)
                throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 3 (inclusive)");

            return index switch
            {
                0 => M11,
                1 => M12,
                2 => M21,
                _ => M22
            };
        }
        set
        {
            if (index < 0 || index > 3)
                throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 3 (inclusive)");

            switch (index)
            {
                case 0: M11 = value; break;
                case 1: M12 = value; break;
                case 2: M21 = value; break;
                default: M22 = value; break;
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
            if (rowIndex < 0 || rowIndex > 1)
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Must be between 0 => 1 (inclusive)");
            if (columnIndex < 0 || columnIndex > 1)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Must be between 0 => 1 (inclusive)");

            return (rowIndex, columnIndex) switch
            {
                (0, 0) => M11,
                (0, 1) => M12,
                (1, 0) => M21,
                _ => M22,
            };
        }
        set
        {
            if (rowIndex < 0 || rowIndex > 1)
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Must be between 0 => 1 (inclusive)");
            if (columnIndex < 0 || columnIndex > 1)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Must be between 0 => 1 (inclusive)");

            switch ((rowIndex, columnIndex))
            {
                case (0, 0): M11 = value; break;
                case (0, 1): M12 = value; break;
                case (1, 0): M21 = value; break;
                default: M22 = value; break;
            }
        }
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="value">The value of all the matrix components.</param>
    public Matrix2x2(T value)
    {
        M11 = value;
        M12 = value;
        M21 = value;
        M22 = value;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="m11">The first element of the first row.</param>
    /// <param name="m12">The second element of the first row.</param>
    /// <param name="m21">The first element of the second row.</param>
    /// <param name="m22">The second element of the second row.</param>
    public Matrix2x2(T m11, T m12, T m21, T m22)
    {
        M11 = m11;
        M12 = m12;
        M21 = m21;
        M22 = m22;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="row1">The first row of the matrix.</param>
    /// <param name="row2">The second row of the matrix.</param>
    public Matrix2x2(in Vector2<T> row1, in Vector2<T> row2)
    {
        M11 = row1.X;
        M12 = row1.Y;
        M21 = row2.X;
        M22 = row2.Y;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Transposes the matrix.</summary>
    public void Transpose() => (M12, M21) = (M21, M12);

    /// <summary>Inverts the matrix.</summary>
    public void Invert()
    {
        // ensure matrix can be inverted
        if (Determinant == T.Zero)
            return;

        var inverseDeterminant = T.One / Determinant;

        var oldM11 = M11;
        M11 = M22 * inverseDeterminant;
        M12 = -M12 * inverseDeterminant;
        M21 = -M21 * inverseDeterminant;
        M22 = oldM11 * inverseDeterminant;
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="other">The matrix to check equality with.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Matrix2x2<T> other) => this == other;

    /// <summary>Checks the matrix and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the matrix and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Matrix2x2<T> matrix && this == matrix;

    /// <summary>Retrieves the hash code of the matrix.</summary>
    /// <returns>The hash code of the matrix.</returns>
    public readonly override int GetHashCode() => (M11, M12, M21, M22).GetHashCode();

    /// <summary>Calculates a string representation of the matrix.</summary>
    /// <returns>A string representation of the matrix.</returns>
    public readonly override string ToString() => $"<M11: {M11}, M12: {M12}, M21: {M21}, M22: {M22}>";

    /// <summary>Creates a rotation matrix.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix2x2<T> CreateRotation(T angle)
    {
        angle = MathsHelper<T>.DegreesToRadians(angle);
        var sinAngle = T.Sin(angle);
        var cosAngle = T.Cos(angle);

        return new(cosAngle, -sinAngle, sinAngle, cosAngle);
    }

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The uniform scale factor.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix2x2<T> CreateScale(T scale) => new(scale, T.Zero, T.Zero, scale);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="xScale">The scale factor of the X axis.</param>
    /// <param name="yScale">The scale factor of the Y axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix2x2<T> CreateScale(T xScale, T yScale) => new(xScale, T.Zero, T.Zero, yScale);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The scale factor of the X and Y axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix2x2<T> CreateScale(Vector2<T> scale) => new(scale.X, T.Zero, T.Zero, scale.Y);


    /*********
    ** Operators
    *********/
    /// <summary>Adds two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Matrix2x2<T> operator +(Matrix2x2<T> left, Matrix2x2<T> right)
    {
        return new(
            m11: left.M11 + right.M11,
            m12: left.M12 + right.M12,
            m21: left.M21 + right.M21,
            m22: left.M22 + right.M22
        );
    }

    /// <summary>Subtracts a matrix from another matrix.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Matrix2x2<T> operator -(Matrix2x2<T> left, Matrix2x2<T> right)
    {
        return new(
            m11: left.M11 - right.M11,
            m12: left.M12 - right.M12,
            m21: left.M21 - right.M21,
            m22: left.M22 - right.M22
        );
    }

    /// <summary>Flips the sign of each component of a matrix.</summary>
    /// <param name="matrix">The matrix to flip the component signs of.</param>
    /// <returns><paramref name="matrix"/> with the sign of its components flipped.</returns>
    public static Matrix2x2<T> operator -(Matrix2x2<T> matrix) => matrix * T.CreateChecked(-1);

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix2x2<T> operator *(T left, Matrix2x2<T> right)
    {
        return new(
            m11: left * right.M11,
            m12: left * right.M12,
            m21: left * right.M21,
            m22: left * right.M22
        );
    }

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix2x2<T> operator *(Matrix2x2<T> left, T right) => right * left;

    /// <summary>Mulltiples a matrix by a vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2<T> operator *(Matrix2x2<T> left, Vector2<T> right)
    {
        return new(
            Vector2<T>.Dot(left.Row1, right),
            Vector2<T>.Dot(left.Row2, right)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix2x2<T> operator *(Matrix2x2<T> left, Matrix2x2<T> right)
    {
        return new(
            m11: Vector2<T>.Dot(left.Row1, right.Column1),
            m12: Vector2<T>.Dot(left.Row1, right.Column2),
            m21: Vector2<T>.Dot(left.Row2, right.Column1),
            m22: Vector2<T>.Dot(left.Row2, right.Column2)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix2x3<T> operator *(Matrix2x2<T> left, Matrix2x3<T> right)
    {
        return new(
            m11: Vector2<T>.Dot(left.Row1, right.Column1),
            m12: Vector2<T>.Dot(left.Row1, right.Column2),
            m13: Vector2<T>.Dot(left.Row1, right.Column3),
            m21: Vector2<T>.Dot(left.Row2, right.Column1),
            m22: Vector2<T>.Dot(left.Row2, right.Column2),
            m23: Vector2<T>.Dot(left.Row2, right.Column3)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix2x4<T> operator *(Matrix2x2<T> left, Matrix2x4<T> right)
    {
        return new(
            m11: Vector2<T>.Dot(left.Row1, right.Column1),
            m12: Vector2<T>.Dot(left.Row1, right.Column2),
            m13: Vector2<T>.Dot(left.Row1, right.Column3),
            m14: Vector2<T>.Dot(left.Row1, right.Column4),
            m21: Vector2<T>.Dot(left.Row2, right.Column1),
            m22: Vector2<T>.Dot(left.Row2, right.Column2),
            m23: Vector2<T>.Dot(left.Row2, right.Column3),
            m24: Vector2<T>.Dot(left.Row2, right.Column4)
        );
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Matrix2x2<T> matrix1, Matrix2x2<T> matrix2)
    {
        return matrix1.M11 == matrix2.M11
            && matrix1.M12 == matrix2.M12
            && matrix1.M21 == matrix2.M21
            && matrix1.M22 == matrix2.M22;
    }

    /// <summary>Checks two matrices for inequality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Matrix2x2<T> matrix1, Matrix2x2<T> matrix2) => !(matrix1 == matrix2);

    /// <summary>Converts a matrix to a tuple.</summary>
    /// <param name="matrix">The matrix to convert.</param>
    public static implicit operator (T M11, T M12, T M21, T M22)(Matrix2x2<T> matrix) => (matrix.M11, matrix.M12, matrix.M21, matrix.M22);

    /// <summary>Converts a tuple to a matrix.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Matrix2x2<T>((T M11, T M12, T M21, T M22) tuple) => new(tuple.M11, tuple.M12, tuple.M21, tuple.M22);
}

namespace NovaEngine.Maths;

/// <summary>Represents a 2x2 matrix using single-precision floating-point numbers.</summary>
public struct Matrix2x2 : IEquatable<Matrix2x2>
{
    /*********
    ** Fields
    *********/
    /// <summary>The first element of the first row.</summary>
    public float M11;

    /// <summary>The second element of the first row.</summary>
    public float M12;

    /// <summary>The first element of the second row.</summary>
    public float M21;

    /// <summary>The second element of the second row.</summary>
    public float M22;


    /*********
    ** Accessors
    *********/
    /// <summary>Whether the matrix is an identity matrix.</summary>
    public readonly bool IsIdentity => this == Identity;

    /// <summary>The determinant of the matrix.</summary>
    public readonly float Determinant => M11 * M22 - M12 * M21;

    /// <summary>The trace of the matrix (the sum of the values along the diagonal).</summary>
    public readonly float Trace => M11 + M22;

    /// <summary>The diagonal of the matrix.</summary>
    public Vector2 Diagonal
    {
        readonly get => new(M11, M22);
        set
        {
            M11 = value.X;
            M22 = value.Y;
        }
    }

    /// <summary>The transposed matrix.</summary>
    public readonly Matrix2x2 Transposed
    {
        get
        {
            var matrix = this;
            matrix.Transpose();
            return matrix;
        }
    }

    /// <summary>The inverse of the matrix.</summary>
    public readonly Matrix2x2 Inverse
    {
        get
        {
            var matrix = this;
            matrix.Invert();
            return matrix;
        }
    }

    /// <summary>The first row of the matrix.</summary>
    public Vector2 Row1
    {
        readonly get => new(M11, M12);
        set
        {
            M11 = value.X;
            M12 = value.Y;
        }
    }

    /// <summary>The second row of the matrix.</summary>
    public Vector2 Row2
    {
        readonly get => new(M21, M22);
        set
        {
            M21 = value.X;
            M22 = value.Y;
        }
    }

    /// <summary>The first column of the matrix.</summary>
    public Vector2 Column1
    {
        readonly get => new(M11, M21);
        set
        {
            M11 = value.X;
            M21 = value.Y;
        }
    }

    /// <summary>The second column of the matrix.</summary>
    public Vector2 Column2
    {
        readonly get => new(M12, M22);
        set
        {
            M12 = value.X;
            M22 = value.Y;
        }
    }

    /// <summary>The zero matrix.</summary>
    public static Matrix2x2 Zero => new(0);

    /// <summary>The identity matrix.</summary>
    public static Matrix2x2 Identity => new(1, 0, 0, 1);

    /// <summary>Gets or sets the value at a specified position.</summary>
    /// <param name="index">The position index.</param>
    /// <returns>The value at the specified position.</returns>
    public float this[int index]
    {
        readonly get
        {
            if (index < 0 || index > 3)
                throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 => 3 (inclusive)");

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
                throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 => 3 (inclusive)");

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
    public float this[int rowIndex, int columnIndex]
    {
        readonly get
        {
            if (rowIndex < 0 || rowIndex > 1)
                throw new IndexOutOfRangeException($"{nameof(rowIndex)} must be between 0 => 1 (inclusive)");
            if (columnIndex < 0 || columnIndex > 1)
                throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 1 (inclusive)");

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
                throw new IndexOutOfRangeException($"{nameof(rowIndex)} must be between 0 => 1 (inclusive)");
            if (columnIndex < 0 || columnIndex > 1)
                throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 1 (inclusive)");

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
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="value">The value of all the matrix components.</param>
    public Matrix2x2(float value)
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
    public Matrix2x2(float m11, float m12, float m21, float m22)
    {
        M11 = m11;
        M12 = m12;
        M21 = m21;
        M22 = m22;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="row1">The first row of the matrix.</param>
    /// <param name="row2">The second row of the matrix.</param>
    public Matrix2x2(in Vector2 row1, in Vector2 row2)
    {
        M11 = row1.X;
        M12 = row1.Y;
        M21 = row2.X;
        M22 = row2.Y;
    }

    /// <summary>Transposes the matrix.</summary>
    public void Transpose() => (M12, M21) = (M21, M12);

    /// <summary>Inverts the matrix.</summary>
    public void Invert()
    {
        // ensure matrix can be inverted
        if (Determinant == 0)
            return;

        var inverseDeterminant = 1 / Determinant;

        var oldM11 = M11;
        M11 = M22 * inverseDeterminant;
        M12 = -M12 * inverseDeterminant;
        M21 = -M21 * inverseDeterminant;
        M22 = oldM11 * inverseDeterminant;
    }

    /// <summary>Gets the matrix as a <see cref="Matrix2x2D"/>.</summary>
    /// <returns>The matrix as a <see cref="Matrix2x2D"/>.</returns>
    public readonly Matrix2x2D ToMatrix2x2D() => new(M11, M12, M21, M22);

    /// <inheritdoc/>
    public readonly bool Equals(Matrix2x2 other) => this == other;

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj) => obj is Matrix2x2 matrix && this == matrix;

    /// <inheritdoc/>
    public readonly override int GetHashCode() => (M11, M12, M21, M22).GetHashCode();

    /// <inheritdoc/>
    public readonly override string ToString() => $"<M11: {M11}, M12: {M12}, M21: {M21}, M22: {M22}>";

    /// <summary>Creates a rotation matrix.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix2x2 CreateRotation(float angle)
    {
        angle = MathsHelper.DegreesToRadians(angle);
        var sinAngle = MathF.Sin(angle);
        var cosAngle = MathF.Cos(angle);

        return new(cosAngle, -sinAngle, sinAngle, cosAngle);
    }

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The uniform scale factor.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix2x2 CreateScale(float scale) => new(scale, 0, 0, scale);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="xScale">The scale factor of the X axis.</param>
    /// <param name="yScale">The scale factor of the Y axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix2x2 CreateScale(float xScale, float yScale) => new(xScale, 0, 0, yScale);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The scale factor of the X and Y axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix2x2 CreateScale(Vector2 scale) => new(scale.X, 0, 0, scale.Y);


    /*********
    ** Operators
    *********/
    /// <summary>Adds two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Matrix2x2 operator +(Matrix2x2 left, Matrix2x2 right)
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
    public static Matrix2x2 operator -(Matrix2x2 left, Matrix2x2 right)
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
    public static Matrix2x2 operator -(Matrix2x2 matrix) => matrix * -1;

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix2x2 operator *(float left, Matrix2x2 right)
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
    public static Matrix2x2 operator *(Matrix2x2 left, float right) => right * left;

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix2x2 operator *(Matrix2x2 left, Matrix2x2 right)
    {
        return new(
            m11: Vector2.Dot(left.Row1, right.Column1),
            m12: Vector2.Dot(left.Row1, right.Column2),
            m21: Vector2.Dot(left.Row2, right.Column1),
            m22: Vector2.Dot(left.Row2, right.Column2)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix2x3 operator *(Matrix2x2 left, Matrix2x3 right)
    {
        return new(
            m11: Vector2.Dot(left.Row1, right.Column1),
            m12: Vector2.Dot(left.Row1, right.Column2),
            m13: Vector2.Dot(left.Row1, right.Column3),
            m21: Vector2.Dot(left.Row2, right.Column1),
            m22: Vector2.Dot(left.Row2, right.Column2),
            m23: Vector2.Dot(left.Row2, right.Column3)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix2x4 operator *(Matrix2x2 left, Matrix2x4 right)
    {
        return new(
            m11: Vector2.Dot(left.Row1, right.Column1),
            m12: Vector2.Dot(left.Row1, right.Column2),
            m13: Vector2.Dot(left.Row1, right.Column3),
            m14: Vector2.Dot(left.Row1, right.Column4),
            m21: Vector2.Dot(left.Row2, right.Column1),
            m22: Vector2.Dot(left.Row2, right.Column2),
            m23: Vector2.Dot(left.Row2, right.Column3),
            m24: Vector2.Dot(left.Row2, right.Column4)
        );
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Matrix2x2 matrix1, Matrix2x2 matrix2)
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
    public static bool operator !=(Matrix2x2 matrix1, Matrix2x2 matrix2) => !(matrix1 == matrix2);

    /// <summary>Converts a matrix to a tuple.</summary>
    /// <param name="matrix">The matrix to convert.</param>
    public static implicit operator (float M11, float M12, float M21, float M22)(Matrix2x2 matrix) => (matrix.M11, matrix.M12, matrix.M21, matrix.M22);

    /// <summary>Converts a tuple to a matrix.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Matrix2x2((float M11, float M12, float M21, float M22) tuple) => new(tuple.M11, tuple.M12, tuple.M21, tuple.M22);

    /// <summary>Converts a <see cref="Matrix2x2"/> to a <see cref="Matrix2x2D"/>.</summary>
    /// <param name="matrix">The matrix to convert.</param>
    public static implicit operator Matrix2x2D(Matrix2x2 matrix) => matrix.ToMatrix2x2D();
}

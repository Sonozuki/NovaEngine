using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a 2x3 matrix using single-precision floating-point numbers.</summary>
    public struct Matrix2x3 : IEquatable<Matrix2x3>
    {
        /*********
        ** Fields
        *********/
        /// <summary>The first element of the first row.</summary>
        public float M11;

        /// <summary>The second element of the first row.</summary>
        public float M12;

        /// <summary>The third element of the first row.</summary>
        public float M13;

        /// <summary>The first element of the second row.</summary>
        public float M21;

        /// <summary>The second element of the second row.</summary>
        public float M22;

        /// <summary>The third element of the second row.</summary>
        public float M23;


        /*********
        ** Accessors
        *********/
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
        public Matrix3x2 Transposed => new(M11, M21, M12, M22, M13, M23);

        /// <summary>The first row of the matrix.</summary>
        public Vector3 Row1
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
        public Vector3 Row2
        {
            readonly get => new(M21, M22, M23);
            set
            {
                M21 = value.X;
                M22 = value.Y;
                M23 = value.Z;
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

        /// <summary>The third column of the matrix.</summary>
        public Vector2 Column3
        {
            readonly get => new(M13, M23);
            set
            {
                M13 = value.X;
                M23 = value.Y;
            }
        }

        /// <summary>The zero matrix.</summary>
        public static Matrix2x3 Zero => new(0);

        /// <summary>Gets or sets the value at a specified position.</summary>
        /// <param name="index">The position index.</param>
        /// <returns>The value at the specified position.</returns>
        public float this[int index]
        {
            readonly get
            {
                if (index < 0 || index > 5)
                    throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 => 5 (inclusive)");

                return index switch
                {
                    0 => M11,
                    1 => M12,
                    2 => M13,
                    3 => M21,
                    4 => M22,
                    _ => M23,
                };
            }
            set
            {
                if (index < 0 || index > 5)
                    throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 => 5 (inclusive)");

                switch (index)
                {
                    case 0: M11 = value; break;
                    case 1: M12 = value; break;
                    case 2: M13 = value; break;
                    case 3: M21 = value; break;
                    case 4: M22 = value; break;
                    default: M23 = value; break;
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
                if (columnIndex < 0 || columnIndex > 2)
                    throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 2 (inclusive)");

                return (rowIndex, columnIndex) switch
                {
                    (0, 0) => M11,
                    (0, 1) => M12,
                    (0, 2) => M13,
                    (1, 0) => M21,
                    (1, 1) => M22,
                    _ => M23,
                };
            }
            set
            {
                if (rowIndex < 0 || rowIndex > 1)
                    throw new IndexOutOfRangeException($"{nameof(rowIndex)} must be between 0 => 1 (inclusive)");
                if (columnIndex < 0 || columnIndex > 2)
                    throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be between 0 => 2 (inclusive)");

                switch ((rowIndex, columnIndex))
                {
                    case (0, 0): M11 = value; break;
                    case (0, 1): M12 = value; break;
                    case (0, 2): M13 = value; break;
                    case (1, 0): M21 = value; break;
                    case (1, 1): M22 = value; break;
                    default: M23 = value; break;
                }
            }
        }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="value">The value of all the matrix components.</param>
        public Matrix2x3(float value)
        {
            M11 = value;
            M12 = value;
            M13 = value;
            M21 = value;
            M22 = value;
            M23 = value;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="m11">The first element of the first row.</param>
        /// <param name="m12">The second element of the first row.</param>
        /// <param name="m13">The third element of the first row.</param>
        /// <param name="m21">The first element of the second row.</param>
        /// <param name="m22">The second element of the second row.</param>
        /// <param name="m23">The third element of the second row.</param>
        public Matrix2x3(float m11, float m12, float m13, float m21, float m22, float m23)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="row1">The first row of the matrix.</param>
        /// <param name="row2">The second row of the matrix.</param>
        public Matrix2x3(in Vector3 row1, in Vector3 row2)
        {
            M11 = row1.X;
            M12 = row1.Y;
            M13 = row1.Z;
            M21 = row2.X;
            M22 = row2.Y;
            M23 = row2.Z;
        }

        /// <summary>Constructs an instance.</summary>
        /// <param name="matrix">The top left 2x2 matrix.</param>
        /// <param name="m13">The third element of the first row.</param>
        /// <param name="m23">The third element of the second row.</param>
        public Matrix2x3(in Matrix2x2 matrix, float m13 = 0, float m23 = 0)
        {
            M11 = matrix.M11;
            M12 = matrix.M12;
            M13 = m13;
            M21 = matrix.M21;
            M22 = matrix.M22;
            M23 = m23;
        }

        /// <summary>Gets the matrix as a <see cref="Matrix2x3D"/>.</summary>
        /// <returns>The matrix as a <see cref="Matrix2x3D"/>.</returns>
        public readonly Matrix2x3D ToMatrix2x3D() => new(M11, M12, M13, M21, M22, M23);

        /// <inheritdoc/>
        public readonly bool Equals(Matrix2x3 other) => this == other;

        /// <inheritdoc/>
        public readonly override bool Equals(object? obj) => obj is Matrix2x3 matrix && this == matrix;

        /// <inheritdoc/>
        public readonly override int GetHashCode() => (M11, M12, M13, M21, M22, M23).GetHashCode();

        /// <inheritdoc/>
        public readonly override string ToString() => $"<M11: {M11}, M12: {M12}, M13: {M13}, M21: {M21}, M22: {M22}, M23: {M23}>";

        /// <summary>Creates a rotation matrix.</summary>
        /// <param name="angle">The clockwise angle, in degrees.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix2x3 CreateRotation(float angle)
        {
            angle = MathsHelper.DegreesToRadians(angle);
            var sinAngle = MathF.Sin(angle);
            var cosAngle = MathF.Cos(angle);

            return new(cosAngle, -sinAngle, 0, sinAngle, cosAngle, 0);
        }

        /// <summary>Creates a scale matrix.</summary>
        /// <param name="scale">The uniform scale factor.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix2x3 CreateScale(float scale) => new(scale, 0, 0, 0, scale, 0);

        /// <summary>Creates a scale matrix.</summary>
        /// <param name="xScale">The scale factor of the X axis.</param>
        /// <param name="yScale">The scale factor of the Y axis.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix2x3 CreateScale(float xScale, float yScale) => new(xScale, 0, 0, 0, yScale, 0);

        /// <summary>Creates a scale matrix.</summary>
        /// <param name="scale">The scale factor of the X and Y axis.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix2x3 CreateScale(Vector2 scale) => new(scale.X, 0, 0, 0, scale.Y, 0);


        /*********
        ** Operators
        *********/
        /// <summary>Adds two matrices together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Matrix2x3 operator +(Matrix2x3 left, Matrix2x3 right)
        {
            return new(
                m11: left.M11 + right.M11,
                m12: left.M12 + right.M12,
                m13: left.M13 + right.M13,
                m21: left.M21 + right.M21,
                m22: left.M22 + right.M22,
                m23: left.M23 + right.M23
            );
        }

        /// <summary>Subtracts a matrix from another matrix.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Matrix2x3 operator -(Matrix2x3 left, Matrix2x3 right)
        {
            return new(
                m11: left.M11 - right.M11,
                m12: left.M12 - right.M12,
                m13: left.M13 - right.M13,
                m21: left.M21 - right.M21,
                m22: left.M22 - right.M22,
                m23: left.M23 - right.M23
            );
        }

        /// <summary>Flips the sign of each component of a matrix.</summary>
        /// <param name="matrix">The matrix to flip the component signs of.</param>
        /// <returns><paramref name="matrix"/> with the sign of its components flipped.</returns>
        public static Matrix2x3 operator -(Matrix2x3 matrix) => matrix * -1;

        /// <summary>Multiplies a matrix by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix2x3 operator *(float left, Matrix2x3 right)
        {
            return new(
                m11: left * right.M11,
                m12: left * right.M12,
                m13: left * right.M13,
                m21: left * right.M21,
                m22: left * right.M22,
                m23: left * right.M23
            );
        }

        /// <summary>Multiplies a matrix by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix2x3 operator *(Matrix2x3 left, float right) => right * left;

        /// <summary>Multiplies two matrices together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix2x2 operator *(Matrix2x3 left, Matrix3x2 right)
        {
            return new(
                m11: Vector3.Dot(left.Row1, right.Column1),
                m12: Vector3.Dot(left.Row1, right.Column2),
                m21: Vector3.Dot(left.Row2, right.Column1),
                m22: Vector3.Dot(left.Row2, right.Column2)
            );
        }

        /// <summary>Multiplies two matrices together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix2x3 operator *(Matrix2x3 left, Matrix3x3 right)
        {
            return new(
                m11: Vector3.Dot(left.Row1, right.Column1),
                m12: Vector3.Dot(left.Row1, right.Column2),
                m13: Vector3.Dot(left.Row1, right.Column3),
                m21: Vector3.Dot(left.Row2, right.Column1),
                m22: Vector3.Dot(left.Row2, right.Column2),
                m23: Vector3.Dot(left.Row2, right.Column3)
            );
        }

        /// <summary>Multiplies two matrices together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix2x4 operator *(Matrix2x3 left, Matrix3x4 right)
        {
            return new(
                m11: Vector3.Dot(left.Row1, right.Column1),
                m12: Vector3.Dot(left.Row1, right.Column2),
                m13: Vector3.Dot(left.Row1, right.Column3),
                m14: Vector3.Dot(left.Row1, right.Column4),
                m21: Vector3.Dot(left.Row2, right.Column1),
                m22: Vector3.Dot(left.Row2, right.Column2),
                m23: Vector3.Dot(left.Row2, right.Column3),
                m24: Vector3.Dot(left.Row2, right.Column4)
            );
        }

        /// <summary>Checks two matrices for equality.</summary>
        /// <param name="matrix1">The first matrix.</param>
        /// <param name="matrix2">The second matrix.</param>
        /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Matrix2x3 matrix1, Matrix2x3 matrix2)
        {
            return matrix1.M11 == matrix2.M11
                && matrix1.M12 == matrix2.M12
                && matrix1.M13 == matrix2.M13
                && matrix1.M21 == matrix2.M21
                && matrix1.M22 == matrix2.M22
                && matrix1.M23 == matrix2.M23;
        }

        /// <summary>Checks two matrices for inequality.</summary>
        /// <param name="matrix1">The first matrix.</param>
        /// <param name="matrix2">The second matrix.</param>
        /// <returns><see langword="true"/> if the matrices are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Matrix2x3 matrix1, Matrix2x3 matrix2) => !(matrix1 == matrix2);

        /// <summary>Converts a matrix to a tuple.</summary>
        /// <param name="matrix">The matrix to convert.</param>
        public static implicit operator (float M11, float M12, float M13, float M21, float M22, float M23)(Matrix2x3 matrix) => (matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23);

        /// <summary>Converts a tuple to a matrix.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator Matrix2x3((float M11, float M12, float M13, float M21, float M22, float M23) tuple) => new(tuple.M11, tuple.M12, tuple.M13, tuple.M21, tuple.M22, tuple.M23);

        /// <summary>Converts a <see cref="Matrix2x3"/> to a <see cref="Matrix2x3D"/>.</summary>
        /// <param name="matrix">The matrix to convert.</param>
        public static implicit operator Matrix2x3D(Matrix2x3 matrix) => matrix.ToMatrix2x3D();
    }
}

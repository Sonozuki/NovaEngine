﻿using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Represents a 4x4 matrix using floating-point numbers.</summary>
public struct Matrix4x4<T> : IEquatable<Matrix4x4<T>>
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

    /// <summary>The first element of the fourth row.</summary>
    public T M41;

    /// <summary>The second element of the fourth row.</summary>
    public T M42;

    /// <summary>The third element of the fourth row.</summary>
    public T M43;

    /// <summary>The fourth element of the fourth row.</summary>
    public T M44;


    /*********
    ** Properties
    *********/
    /// <summary>Whether the matrix is an identity matrix.</summary>
    public readonly bool IsIdentity => this == Identity;

    /// <summary>The determinant of the matrix.</summary>
    public readonly T Determinant => M11 * new Matrix3x3<T>(M22, M23, M24, M32, M33, M34, M42, M43, M44).Determinant
                                   - M12 * new Matrix3x3<T>(M21, M23, M24, M31, M33, M34, M41, M43, M44).Determinant
                                   + M13 * new Matrix3x3<T>(M21, M22, M24, M31, M32, M34, M41, M42, M44).Determinant
                                   - M14 * new Matrix3x3<T>(M21, M22, M23, M31, M32, M33, M41, M42, M43).Determinant;

    /// <summary>The trace of the matrix (the sum of the values along the diagonal).</summary>
    public readonly T Trace => M11 + M22 + M33 + M44;

    /// <summary>The diagonal of the matrix.</summary>
    public Vector4<T> Diagonal
    {
        readonly get => new(M11, M22, M33, M44);
        set
        {
            M11 = value.X;
            M22 = value.Y;
            M33 = value.Z;
            M44 = value.W;
        }
    }

    /// <summary>The transposed matrix.</summary>
    public readonly Matrix4x4<T> Transposed
    {
        get
        {
            var matrix = this;
            matrix.Transpose();
            return matrix;
        }
    }

    /// <summary>The inverse of the matrix.</summary>
    /// <remarks>If the matrix can't be inverted (<see cref="Determinant"/> is 0), then the matrix will be unchanged.</remarks>
    public readonly Matrix4x4<T> Inverse
    {
        get
        {
            var matrix = this;
            matrix.Invert();
            return matrix;
        }
    }

    /// <summary>The matrix without the translation.</summary>
    public readonly Matrix4x4<T> TranslationRemoved
    {
        get
        {
            var matrix = this;
            matrix.RemoveTranslation();
            return matrix;
        }
    }

    /// <summary>The matrix without the rotation.</summary>
    public readonly Matrix4x4<T> RotationRemoved
    {
        get
        {
            var matrix = this;
            matrix.RemoveRotation();
            return matrix;
        }
    }

    /// <summary>The matrix without the scale.</summary>
    public readonly Matrix4x4<T> ScaleRemoved
    {
        get
        {
            var matrix = this;
            matrix.RemoveScale();
            return matrix;
        }
    }

    /// <summary>The matrix without the projection.</summary>
    public readonly Matrix4x4<T> ProjectionRemoved
    {
        get
        {
            var matrix = this;
            matrix.RemoveProjection();
            return matrix;
        }
    }

    /// <summary>The translation of the matrix.</summary>
    public readonly Vector3<T> Translation => new(M41, M42, M43);

    /// <summary>The rotation of the matrix.</summary>
    public readonly Quaternion<T> Rotation => new Matrix3x3<T>(M11, M12, M13, M21, M22, M23, M31, M32, M33).Rotation;

    /// <summary>The scale of the matrix.</summary>
    public readonly Vector3<T> Scale => new(Row1.XYZ.Length, Row2.XYZ.Length, Row3.XYZ.Length);

    /// <summary>The projection of the matrix.</summary>
    public readonly Vector4<T> Projection => Column4;

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

    /// <summary>The fourth row of the matrix.</summary>
    public Vector4<T> Row4
    {
        readonly get => new(M41, M42, M43, M44);
        set
        {
            M41 = value.X;
            M42 = value.Y;
            M43 = value.Z;
            M44 = value.W;
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

    /// <summary>The fourth column of the matrix.</summary>
    public Vector4<T> Column4
    {
        readonly get => new(M14, M24, M34, M44);
        set
        {
            M14 = value.X;
            M24 = value.Y;
            M34 = value.Z;
            M44 = value.W;
        }
    }

    /// <summary>A zero matrix.</summary>
    public static Matrix4x4<T> Zero => new(T.Zero);

    /// <summary>An identity matrix.</summary>
    public static Matrix4x4<T> Identity => new(T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>Gets or sets the value at a specified position.</summary>
    /// <param name="index">The position index.</param>
    /// <returns>The value at the specified position.</returns>
    public T this[int index]
    {
        readonly get
        {
            if (index < 0 || index > 15)
                throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 15 (inclusive)");

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
                11 => M34,
                12 => M41,
                13 => M42,
                14 => M43,
                _ => M44,
            };
        }
        set
        {
            if (index < 0 || index > 15)
                throw new ArgumentOutOfRangeException(nameof(index), "Must be between 0 => 15 (inclusive)");

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
                case 11: M34 = value; break;
                case 12: M41 = value; break;
                case 13: M42 = value; break;
                case 14: M43 = value; break;
                default: M44 = value; break;
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
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Must be between 0 => 3 (inclusive)");
            if (columnIndex < 0 || columnIndex > 3)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Must be between 0 => 3 (inclusive)");

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
                (2, 3) => M34,
                (3, 0) => M41,
                (3, 1) => M42,
                (3, 2) => M43,
                _ => M44,
            };
        }
        set
        {
            if (rowIndex < 0 || rowIndex > 3)
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Must be between 0 => 3 (inclusive)");
            if (columnIndex < 0 || columnIndex > 3)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Must be between 0 => 3 (inclusive)");

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
                case (2, 3): M34 = value; break;
                case (3, 0): M41 = value; break;
                case (3, 1): M42 = value; break;
                case (3, 2): M43 = value; break;
                default: M44 = value; break;
            }
        }
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="value">The value of all the matrix components.</param>
    public Matrix4x4(T value)
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
        M41 = value;
        M42 = value;
        M43 = value;
        M44 = value;
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
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    /// <param name="m44">The fourth element of the fourth row.</param>
    public Matrix4x4(T m11, T m12, T m13, T m14, T m21, T m22, T m23, T m24, T m31, T m32, T m33, T m34, T m41, T m42, T m43, T m44)
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
        M41 = m41;
        M42 = m42;
        M43 = m43;
        M44 = m44;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="row1">The first row of the matrix.</param>
    /// <param name="row2">The second row of the matrix.</param>
    /// <param name="row3">The third row of the matrix.</param>
    /// <param name="row4">The fourth row of the matrix.</param>
    public Matrix4x4(in Vector4<T> row1, in Vector4<T> row2, in Vector4<T> row3, in Vector4<T> row4)
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
        M41 = row4.X;
        M42 = row4.Y;
        M43 = row4.Z;
        M44 = row4.W;
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
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    /// <param name="m44">The fourth element of the fourth row.</param>
    public Matrix4x4(in Matrix2x2<T> matrix, T m13, T m14, T m23, T m24, T m31, T m32, T m33, T m34, T m41, T m42, T m43, T m44)
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
        M41 = m41;
        M42 = m42;
        M43 = m43;
        M44 = m44;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 2x3 matrix.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    /// <param name="m44">The fourth element of the fourth row.</param>
    public Matrix4x4(in Matrix2x3<T> matrix, T m14, T m24, T m31, T m32, T m33, T m34, T m41, T m42, T m43, T m44)
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
        M41 = m41;
        M42 = m42;
        M43 = m43;
        M44 = m44;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 2x4 matrix.</param>
    /// <param name="m31">The first element of the third row.</param>
    /// <param name="m32">The second element of the third row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    /// <param name="m44">The fourth element of the fourth row.</param>
    public Matrix4x4(in Matrix2x4<T> matrix, T m31, T m32, T m33, T m34, T m41, T m42, T m43, T m44)
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
        M41 = m41;
        M42 = m42;
        M43 = m43;
        M44 = m44;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 3x2 matrix.</param>
    /// <param name="m13">The third element of the first row.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m23">The third element of the second row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    /// <param name="m44">The fourth element of the fourth row.</param>
    public Matrix4x4(in Matrix3x2<T> matrix, T m13, T m14, T m23, T m24, T m33, T m34, T m41, T m42, T m43, T m44)
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
        M41 = m41;
        M42 = m42;
        M43 = m43;
        M44 = m44;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 3x3 matrix.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    /// <param name="m44">The fourth element of the fourth row.</param>
    public Matrix4x4(in Matrix3x3<T> matrix, T m14, T m24, T m34, T m41, T m42, T m43, T m44)
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
        M41 = m41;
        M42 = m42;
        M43 = m43;
        M44 = m44;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 3x4 matrix.</param>
    /// <param name="m41">The first element of the fourth row.</param>
    /// <param name="m42">The second element of the fourth row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    /// <param name="m44">The fourth element of the fourth row.</param>
    public Matrix4x4(in Matrix3x4<T> matrix, T m41, T m42, T m43, T m44)
    {
        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = matrix.M13;
        M14 = matrix.M14;
        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = matrix.M23;
        M24 = matrix.M24;
        M31 = matrix.M31;
        M32 = matrix.M32;
        M33 = matrix.M33;
        M34 = matrix.M34;
        M41 = m41;
        M42 = m42;
        M43 = m43;
        M44 = m44;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 4x2 matrix.</param>
    /// <param name="m13">The third element of the first row.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m23">The third element of the second row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m33">The third element of the third row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    /// <param name="m43">The third element of the fourth row.</param>
    /// <param name="m44">The fourth element of the fourth row.</param>
    public Matrix4x4(in Matrix4x2<T> matrix, T m13, T m14, T m23, T m24, T m33, T m34, T m43, T m44)
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
        M41 = matrix.M41;
        M42 = matrix.M42;
        M43 = m43;
        M44 = m44;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="matrix">The top left 4x3 matrix.</param>
    /// <param name="m14">The fourth element of the first row.</param>
    /// <param name="m24">The fourth element of the second row.</param>
    /// <param name="m34">The fourth element of the third row.</param>
    /// <param name="m44">The fourth element of the fourth row.</param>
    public Matrix4x4(in Matrix4x3<T> matrix, T m14, T m24, T m34, T m44)
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
        M41 = matrix.M41;
        M42 = matrix.M42;
        M43 = matrix.M43;
        M44 = m44;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Transposes the matrix.</summary>
    public void Transpose() => (M12, M13, M14, M21, M23, M24, M31, M32, M34, M41, M42, M43) = (M21, M31, M41, M12, M32, M42, M13, M23, M43, M14, M24, M34);

    /// <summary>Inverts the matrix.</summary>
    /// <remarks>If the matrix can't be inverted (<see cref="Determinant"/> is 0), then the matrix will be unchanged.</remarks>
    public void Invert()
    {
        // ensure matrix can be inverted
        if (Determinant == T.Zero)
            return;

        // create matrix of minors
        var minorsMatrix = new Matrix4x4<T>();
        for (var row = 0; row < 4; row++)
            for (var column = 0; column < 4; column++)
            {
                // create the 3x3 matrix from the elements that don't intersect the current element's row or column
                var matrix3x3 = new Matrix3x3<T>();
                var currentMatrix3x3Element = 0;
                for (var matrix3x3Row = 0; matrix3x3Row < 4; matrix3x3Row++)
                    for (var matrix3x3Column = 0; matrix3x3Column < 4; matrix3x3Column++)
                    {
                        // ensure current matrix element doesn't intersect with the row or column of this element
                        if (matrix3x3Row == row || matrix3x3Column == column)
                            continue;

                        matrix3x3[currentMatrix3x3Element++] = this[matrix3x3Row, matrix3x3Column];
                    }

                minorsMatrix[row, column] = matrix3x3.Determinant;
            }

        // create matrix of cofactors
        var cofactorsMatrix = minorsMatrix;
        for (var row = 0; row < 4; row++)
            for (var column = 0; column < 4; column++)
                if ((row + column) % 2 != 0)
                    cofactorsMatrix[row, column] *= T.CreateChecked(-1);

        // get the inverted matrix
        var invertedMatrix = cofactorsMatrix.Transposed * (T.One / Determinant);

        // copy over the inverted matrix to this instance
        for (var i = 0; i < 16; i++)
            this[i] = invertedMatrix[i];
    }

    /// <summary>Removes the translation from the matrix.</summary>
    public void RemoveTranslation() => Row4 = new(T.Zero, T.Zero, T.Zero, M44);

    /// <summary>Removes the rotation from the matrix.</summary>
    public void RemoveRotation()
    {
        Row1 = new(Row1.XYZ.Length, T.Zero, T.Zero, M14);
        Row2 = new(T.Zero, Row2.XYZ.Length, T.Zero, M24);
        Row3 = new(T.Zero, T.Zero, Row3.XYZ.Length, M34);
    }

    /// <summary>Removes the scale from the matrix.</summary>
    public void RemoveScale()
    {
        Row1 = new(Row1.XYZ.Normalised, M14);
        Row2 = new(Row2.XYZ.Normalised, M24);
        Row3 = new(Row3.XYZ.Normalised, M34);
    }

    /// <summary>Removes the projection from the matrix.</summary>
    public void RemoveProjection() => Column4 = Vector4<T>.Zero;

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="other">The matrix to check equality with.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Matrix4x4<T> other) => this == other;

    /// <summary>Checks the matrix and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the matrix and object are equal; otherwise, <see langword="false"/>.</returns>
    public readonly override bool Equals(object? obj) => obj is Matrix4x4<T> matrix && this == matrix;

    /// <summary>Retrieves the hash code of the matrix.</summary>
    /// <returns>The hash code of the matrix.</returns>
    public readonly override int GetHashCode() => (M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44).GetHashCode();

    /// <summary>Calculates a string representation of the matrix.</summary>
    /// <returns>A string representation of the matrix.</returns>
    public readonly override string ToString() => $"<M11: {M11}, M12: {M12}, M13: {M13}, M14: {M14}, M21: {M21}, M22: {M22}, M23: {M23}, M24: {M24}, M31: {M31}, M32: {M32}, M33: {M33}, M34: {M34}, M41: {M41}, M42: {M42}, M43: {M43}, M44: {M44}>";

    /// <summary>Creates a translation matrix.</summary>
    /// <param name="x">The X translation.</param>
    /// <param name="y">The Y translation.</param>
    /// <param name="z">The Z translation.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateTranslation(T x, T y, T z)
    {
        var result = Matrix4x4<T>.Identity;
        result.M41 = x;
        result.M42 = y;
        result.M43 = z;
        return result;
    }

    /// <summary>Creates a translation matrix.</summary>
    /// <param name="translation">The translation.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateTranslation(Vector3<T> translation) => Matrix4x4<T>.CreateTranslation(translation.X, translation.Y, translation.Z);

    /// <summary>Creates a rotation matrix from an axis and an angle.</summary>
    /// <param name="axis">The axis to rotate around.</param>
    /// <param name="angle">The angle, in degrees, to rotate around the axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateFromAxisAngle(Vector3<T> axis, T angle) => new(Matrix3x3<T>.CreateFromAxisAngle(axis, angle), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>Creates a rotation matrix from a quaternion.</summary>
    /// <param name="quaternion">The quaternion to create a rotation matrix from.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateFromQuaternion(Quaternion<T> quaternion) => new(Matrix3x3<T>.CreateFromQuaternion(quaternion), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>Creates a rotation matrix for a rotation about the X axis.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateRotationX(T angle) => new(Matrix3x3<T>.CreateRotationX(angle), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>Creates a rotation matrix for a rotation about the Y axis.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateRotationY(T angle) => new(Matrix3x3<T>.CreateRotationY(angle), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>Creates a rotation matrix for a rotation about the Z axis.</summary>
    /// <param name="angle">The clockwise angle, in degrees.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateRotationZ(T angle) => new(Matrix3x3<T>.CreateRotationZ(angle), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The uniform scale factor.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateScale(T scale) => new(Matrix3x3<T>.CreateScale(scale), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="x">The scale factor of the X axis.</param>
    /// <param name="y">The scale factor of the Y axis.</param>
    /// <param name="z">The scale factor of the Z axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateScale(T x, T y, T z) => new(Matrix3x3<T>.CreateScale(x, y, z), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>Creates a scale matrix.</summary>
    /// <param name="scale">The scale factor of the X, Y, and Z axis.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateScale(Vector3<T> scale) => new(Matrix3x3<T>.CreateScale(scale), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>Creates an orthographic projection matrix.</summary>
    /// <param name="width">The width of the view volume.</param>
    /// <param name="height">The height of the view volume.</param>
    /// <param name="zNearPlane">The minimum Z value of the view volume.</param>
    /// <param name="zFarPlane">The maximum Z value of the view volume.</param>
    /// <returns>The created matrix.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="width"/> or <paramref name="height"/> are zero.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="zNearPlane"/> and <paramref name="zFarPlane"/> are the same value.</exception>
    public static Matrix4x4<T> CreateOrthographic(T width, T height, T zNearPlane, T zFarPlane)
    {
        if (width == T.Zero)
            throw new ArgumentOutOfRangeException(nameof(width), "Must not be zero.");
        if (height == T.Zero)
            throw new ArgumentOutOfRangeException(nameof(height), "Must not be zero.");
        if (zNearPlane == zFarPlane)
            throw new ArgumentException($"{nameof(width)} and {nameof(height)} must not be the same value.");

        var result = Matrix4x4<T>.Identity;
        result.M11 = T.CreateChecked(2) / width;
        result.M22 = T.CreateChecked(2) / height;
        result.M33 = T.One / (zNearPlane - zFarPlane);
        result.M43 = zNearPlane / (zNearPlane - zFarPlane);
        return result;
    }

    /// <summary>Creates an orthographic projection matrix.</summary>
    /// <param name="left">The minimum X value of the view volume.</param>
    /// <param name="right">The maximum X value of the view volume.</param>
    /// <param name="bottom">The minimum Y value of the view volume.</param>
    /// <param name="top">The maximum Y value of the view volume.</param>
    /// <param name="zNearPlane">The minimum Z value of the view volume.</param>
    /// <param name="zFarPlane">The maximum Z value of the view volume.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateOrthographicOffCentre(T left, T right, T bottom, T top, T zNearPlane, T zFarPlane)
    {
        var result = Matrix4x4<T>.Identity;
        result.M11 = T.CreateChecked(2) / (right - left);
        result.M22 = T.CreateChecked(2) / (top - bottom);
        result.M33 = T.One / (zNearPlane - zFarPlane);
        result.M41 = (left + right) / (left - right);
        result.M42 = (top + bottom) / (bottom - top);
        result.M43 = zNearPlane / (zNearPlane - zFarPlane);
        return result;
    }

    /// <summary>Creates a perspective projection matrix.</summary>
    /// <param name="width">The width of the view volume at the near clipping plane.</param>
    /// <param name="height">The height of the view volume at the near clipping plane.</param>
    /// <param name="nearClippingPlane">The distance to the near clipping plane.</param>
    /// <param name="farClippingPlane">The distance to the far clipping plane.</param>
    /// <returns>The created matrix.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="nearClippingPlane"/> or <paramref name="farClippingPlane"/> are less than zero, or if <paramref name="farClippingPlane"/> is less than or equal to <paramref name="nearClippingPlane"/>.</exception>
    public static Matrix4x4<T> CreatePerspective(T width, T height, T nearClippingPlane, T farClippingPlane)
    {
        if (nearClippingPlane <= T.Zero)
            throw new ArgumentOutOfRangeException(nameof(nearClippingPlane), "Must be more than zero.");
        if (farClippingPlane <= T.Zero)
            throw new ArgumentOutOfRangeException(nameof(farClippingPlane), "Must be more than zero.");
        if (nearClippingPlane >= farClippingPlane)
            throw new ArgumentOutOfRangeException(nameof(nearClippingPlane), $"Must be more than {nameof(farClippingPlane)}.");

        var result = new Matrix4x4<T>
        {
            M11 = T.CreateChecked(2) * nearClippingPlane / width,
            M22 = T.CreateChecked(2) * nearClippingPlane / height,
            M33 = T.IsPositiveInfinity(farClippingPlane) ? T.CreateChecked(-1) : farClippingPlane / (nearClippingPlane - farClippingPlane),
            M34 = T.CreateChecked(-1)
        };
        result.M43 = nearClippingPlane * result.M33;
        return result;
    }

    /// <summary>Creates a perspective projection matrix.</summary>
    /// <param name="left">The minimum X value of the view volume.</param>
    /// <param name="right">The maximum X value of the view volume.</param>
    /// <param name="bottom">The minimum Y value of the view volume.</param>
    /// <param name="top">The maximum Y value of the view volume.</param>
    /// <param name="nearClippingPlane">The minimum Z value of the view volume.</param>
    /// <param name="farClippingPlane">The maximum Z value of the view volume.</param>
    /// <returns>The created matrix.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="nearClippingPlane"/> or <paramref name="farClippingPlane"/> are less than zero, or if <paramref name="farClippingPlane"/> is less than or equal to <paramref name="nearClippingPlane"/>.</exception>
    public static Matrix4x4<T> CreatePerspectiveOffCentre(T left, T right, T bottom, T top, T nearClippingPlane, T farClippingPlane)
    {
        if (nearClippingPlane <= T.Zero)
            throw new ArgumentOutOfRangeException(nameof(nearClippingPlane), "Must be more than zero.");
        if (farClippingPlane <= T.Zero)
            throw new ArgumentOutOfRangeException(nameof(farClippingPlane), "Must be more than zero.");
        if (nearClippingPlane >= farClippingPlane)
            throw new ArgumentOutOfRangeException(nameof(nearClippingPlane), $"Must be more than {nameof(farClippingPlane)}.");

        var result = new Matrix4x4<T>
        {
            M11 = T.CreateChecked(2) * nearClippingPlane / (right - left),
            M22 = T.CreateChecked(2) * nearClippingPlane / (top - bottom),
            M31 = (left + right) / (right - left),
            M32 = (top + bottom) / (top - bottom),
            M33 = T.IsPositiveInfinity(farClippingPlane) ? T.CreateChecked(-1) : farClippingPlane / (nearClippingPlane - farClippingPlane),
            M34 = T.CreateChecked(-1)
        };
        result.M43 = nearClippingPlane * result.M33;
        return result;
    }

    /// <summary>Creates a perspective projection matrix.</summary>
    /// <param name="fieldOfView">The field of view in the Y direction, in degrees.</param>
    /// <param name="aspectRatio">The aspect ratio.</param>
    /// <param name="nearClippingPlane">The distance to the near clipping plane.</param>
    /// <param name="farClippingPlane">The distance to the far clipping plane.</param>
    /// <returns>The created matrix.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="fieldOfView"/>, <paramref name="nearClippingPlane"/>, or <paramref name="farClippingPlane"/> are less than zero, or if <paramref name="farClippingPlane"/> is less than or equal to <paramref name="nearClippingPlane"/>.</exception>
    public static Matrix4x4<T> CreatePerspectiveFieldOfView(T fieldOfView, T aspectRatio, T nearClippingPlane, T farClippingPlane)
    {
        if (fieldOfView <= T.Zero)
            throw new ArgumentOutOfRangeException(nameof(fieldOfView), "Must be more than zero.");
        if (nearClippingPlane <= T.Zero)
            throw new ArgumentOutOfRangeException(nameof(nearClippingPlane), "Must be more than zero.");
        if (farClippingPlane <= T.Zero)
            throw new ArgumentOutOfRangeException(nameof(farClippingPlane), "Must be more than zero.");
        if (nearClippingPlane >= farClippingPlane)
            throw new ArgumentOutOfRangeException(nameof(nearClippingPlane), $"Must be more than {nameof(farClippingPlane)}.");

        var yScale = T.One / T.Tan(MathsHelper<T>.DegreesToRadians(fieldOfView) / T.CreateChecked(2));
        var xScale = yScale / aspectRatio;

        var result = new Matrix4x4<T>
        {
            M11 = xScale,
            M22 = yScale,
            M33 = T.IsPositiveInfinity(farClippingPlane) ? T.CreateChecked(-1) : farClippingPlane / (nearClippingPlane - farClippingPlane),
            M34 = T.CreateChecked(-1)
        };
        result.M43 = nearClippingPlane * result.M33;
        return result;
    }

    /// <summary>Creates a view matrix.</summary>
    /// <param name="cameraPosition">The position of the camera.</param>
    /// <param name="cameraTarget">The target towards which the camera is pointing.</param>
    /// <param name="cameraUpVector">The direction that is 'up' from the camera's point of view.</param>
    /// <returns>The created matrix.</returns>
    public static Matrix4x4<T> CreateLookAt(Vector3<T> cameraPosition, Vector3<T> cameraTarget, Vector3<T> cameraUpVector)
    {
        var zAxis = (cameraPosition - cameraTarget).Normalised;
        var xAxis = Vector3<T>.Cross(cameraUpVector, zAxis).Normalised;
        var yAxis = Vector3<T>.Cross(zAxis, xAxis).Normalised;

        var result = Matrix4x4<T>.Identity;
        result.M11 = xAxis.X;
        result.M12 = yAxis.X;
        result.M13 = zAxis.X;
        result.M21 = xAxis.Y;
        result.M22 = yAxis.Y;
        result.M23 = zAxis.Y;
        result.M31 = xAxis.Z;
        result.M32 = yAxis.Z;
        result.M33 = zAxis.Z;
        result.M41 = -Vector3<T>.Dot(xAxis, cameraPosition);
        result.M42 = -Vector3<T>.Dot(yAxis, cameraPosition);
        result.M43 = -Vector3<T>.Dot(zAxis, cameraPosition);
        return result;
    }

    
    /*********
    ** Operators
    *********/
    /// <summary>Adds two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the addition.</returns>
    public static Matrix4x4<T> operator +(Matrix4x4<T> left, Matrix4x4<T> right)
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
            m34: left.M34 + right.M34,
            m41: left.M41 + right.M41,
            m42: left.M42 + right.M42,
            m43: left.M43 + right.M43,
            m44: left.M44 + right.M44
        );
    }

    /// <summary>Subtracts a matrix from another matrix.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Matrix4x4<T> operator -(Matrix4x4<T> left, Matrix4x4<T> right)
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
            m34: left.M34 - right.M34,
            m41: left.M41 - right.M41,
            m42: left.M42 - right.M42,
            m43: left.M43 - right.M43,
            m44: left.M44 - right.M44
        );
    }

    /// <summary>Flips the sign of each component of a matrix.</summary>
    /// <param name="matrix">The matrix to flip the component signs of.</param>
    /// <returns><paramref name="matrix"/> with the sign of its components flipped.</returns>
    public static Matrix4x4<T> operator -(Matrix4x4<T> matrix) => matrix * T.CreateChecked(-1);

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x4<T> operator *(T left, Matrix4x4<T> right)
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
            m34: left * right.M34,
            m41: left * right.M41,
            m42: left * right.M42,
            m43: left * right.M43,
            m44: left * right.M44
        );
    }

    /// <summary>Multiplies a matrix by a scalar.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x4<T> operator *(Matrix4x4<T> left, T right) => right * left;

    /// <summary>Mulltiples a matrix by a vector.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector4<T> operator *(Matrix4x4<T> left, Vector4<T> right)
    {
        return new(
            Vector4<T>.Dot(left.Row1, right),
            Vector4<T>.Dot(left.Row2, right),
            Vector4<T>.Dot(left.Row3, right),
            Vector4<T>.Dot(left.Row4, right)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x2<T> operator *(Matrix4x4<T> left, Matrix4x2<T> right)
    {
        return new(
            m11: Vector4<T>.Dot(left.Row1, right.Column1),
            m12: Vector4<T>.Dot(left.Row1, right.Column2),
            m21: Vector4<T>.Dot(left.Row2, right.Column1),
            m22: Vector4<T>.Dot(left.Row2, right.Column2),
            m31: Vector4<T>.Dot(left.Row3, right.Column1),
            m32: Vector4<T>.Dot(left.Row3, right.Column2),
            m41: Vector4<T>.Dot(left.Row4, right.Column1),
            m42: Vector4<T>.Dot(left.Row4, right.Column2)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x3<T> operator *(Matrix4x4<T> left, Matrix4x3<T> right)
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
            m33: Vector4<T>.Dot(left.Row3, right.Column3),
            m41: Vector4<T>.Dot(left.Row4, right.Column1),
            m42: Vector4<T>.Dot(left.Row4, right.Column2),
            m43: Vector4<T>.Dot(left.Row4, right.Column3)
        );
    }

    /// <summary>Multiplies two matrices together.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x4<T> operator *(Matrix4x4<T> left, Matrix4x4<T> right)
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
            m34: Vector4<T>.Dot(left.Row3, right.Column4),
            m41: Vector4<T>.Dot(left.Row4, right.Column1),
            m42: Vector4<T>.Dot(left.Row4, right.Column2),
            m43: Vector4<T>.Dot(left.Row4, right.Column3),
            m44: Vector4<T>.Dot(left.Row4, right.Column4)
        );
    }

    /// <summary>Checks two matrices for equality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Matrix4x4<T> matrix1, Matrix4x4<T> matrix2)
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
            && matrix1.M34 == matrix2.M34
            && matrix1.M41 == matrix2.M41
            && matrix1.M42 == matrix2.M42
            && matrix1.M43 == matrix2.M43
            && matrix1.M44 == matrix2.M44;
    }

    /// <summary>Checks two matrices for inequality.</summary>
    /// <param name="matrix1">The first matrix.</param>
    /// <param name="matrix2">The second matrix.</param>
    /// <returns><see langword="true"/> if the matrices are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Matrix4x4<T> matrix1, Matrix4x4<T> matrix2) => !(matrix1 == matrix2);

    /// <summary>Converts a matrix to a tuple.</summary>
    /// <param name="matrix">The matrix to convert.</param>
    public static implicit operator (T M11, T M12, T M13, T M14, T M21, T M22, T M23, T M24, T M31, T M32, T M33, T M34, T M41, T M42, T M43, T M44)(Matrix4x4<T> matrix) => (matrix.M11, matrix.M12, matrix.M13, matrix.M14, matrix.M21, matrix.M22, matrix.M23, matrix.M24, matrix.M31, matrix.M32, matrix.M33, matrix.M34, matrix.M41, matrix.M42, matrix.M43, matrix.M44);

    /// <summary>Converts a tuple to a matrix.</summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Matrix4x4<T>((T M11, T M12, T M13, T M14, T M21, T M22, T M23, T M24, T M31, T M32, T M33, T M34, T M41, T M42, T M43, T M44) tuple) => new(tuple.M11, tuple.M12, tuple.M13, tuple.M14, tuple.M21, tuple.M22, tuple.M23, tuple.M24, tuple.M31, tuple.M32, tuple.M33, tuple.M34, tuple.M41, tuple.M42, tuple.M43, tuple.M44);
}

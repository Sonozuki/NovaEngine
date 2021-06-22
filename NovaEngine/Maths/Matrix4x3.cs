using System;

namespace NovaEngine.Maths
{
    /// <summary>Represents a 4x3 matrix using single-precision floating-point numbers.</summary>
    public struct Matrix4x3 : IEquatable<Matrix4x3>
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

        /// <summary>The first element of the third row.</summary>
        public float M31;

        /// <summary>The second element of the third row.</summary>
        public float M32;

        /// <summary>The third element of the third row.</summary>
        public float M33;

        /// <summary>The first element of the fourth row.</summary>
        public float M41;

        /// <summary>The second element of the fourth row.</summary>
        public float M42;

        /// <summary>The third element of the fourth row.</summary>
        public float M43;


        /*********
        ** Accessors
        *********/
        /// <summary>The trace of the matrix (the sum of the values along the diagonal).</summary>
        public float Trace => M11 + M22 + M33;

        /// <summary>The diagonal of the matrix.</summary>
        public Vector3 Diagonal
        {
            get => new(M11, M22, M33);
            set
            {
                M11 = value.X;
                M22 = value.Y;
                M33 = value.Z;
            }
        }

        /// <summary>The transposed matrix.</summary>
        public Matrix3x4 Transposed => new(M11, M21, M31, M41, M12, M22, M32, M42, M13, M23, M33, M43);

        /// <summary>The matrix without the translation.</summary>
        public Matrix4x3 TranslationRemoved
        {
            get
            {
                var matrix = this;
                matrix.RemoveTranslation();
                return matrix;
            }
        }

        /// <summary>The matrix without the rotation.</summary>
        public Matrix4x3 RotationRemoved
        {
            get
            {
                var matrix = this;
                matrix.RemoveRotation();
                return matrix;
            }
        }

        /// <summary>The matrix without the scale.</summary>
        public Matrix4x3 ScaleRemoved
        {
            get
            {
                var matrix = this;
                matrix.RemoveScale();
                return matrix;
            }
        }

        /// <summary>The translation of the matrix.</summary>
        public Vector3 Translation => new(M41, M42, M43);

        /// <summary>The rotation of the matrix.</summary>
        public Quaternion Rotation
        {
            get
            {
                var copy = this;
                copy.RemoveScale();

                var quaternion = new Quaternion();
                if (Trace > 0)
                {
                    var sq = .5f / MathF.Sqrt(Trace + 1);
                    quaternion.W = .25f / sq;
                    quaternion.X = (M31 - M23) * sq;
                    quaternion.Y = (M13 - M31) * sq;
                    quaternion.Z = (M21 - M12) * sq;
                }
                else if (M11 > M22 && M11 > M33)
                {
                    var sq = 2 * MathF.Sqrt(1 + M11 - M22 - M33);
                    quaternion.X = .25f * sq;
                    quaternion.Y = (M12 + M21) / sq;
                    quaternion.Z = (M13 + M31) / sq;
                    quaternion.W = (M32 - M23) / sq;
                }
                else if (M22 > M33)
                {
                    var sq = 2 * MathF.Sqrt(1 + M22 - M11 - M33);
                    quaternion.X = (M12 + M21) / sq;
                    quaternion.Y = .25f * sq;
                    quaternion.Z = (M23 + M32) / sq;
                    quaternion.W = (M13 - M31) / sq;
                }
                else
                {
                    var sq = 2 * MathF.Sqrt(1 + M33 - M11 - M22);
                    quaternion.X = (M13 + M31) / sq;
                    quaternion.Y = (M23 + M32) / sq;
                    quaternion.Z = .25f * sq;
                    quaternion.W = (M21 - M12) / sq;
                }

                return quaternion.Normalised;
            }
        }

        /// <summary>The scale of the matrix.</summary>
        public Vector3 Scale => new(Row1.Length, Row2.Length, Row3.Length);

        /// <summary>The first row of the matrix.</summary>
        public Vector3 Row1
        {
            get => new(M11, M12, M13);
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
            get => new(M21, M22, M23);
            set
            {
                M21 = value.X;
                M22 = value.Y;
                M23 = value.Z;
            }
        }

        /// <summary>The third row of the matrix.</summary>
        public Vector3 Row3
        {
            get => new(M31, M32, M33);
            set
            {
                M31 = value.X;
                M32 = value.Y;
                M33 = value.Z;
            }
        }

        /// <summary>The fourth row of the matrix.</summary>
        public Vector3 Row4
        {
            get => new(M41, M42, M43);
            set
            {
                M41 = value.X;
                M42 = value.Y;
                M43 = value.Z;
            }
        }

        /// <summary>The first column of the matrix.</summary>
        public Vector4 Column1
        {
            get => new(M11, M21, M31, M41);
            set
            {
                M11 = value.X;
                M21 = value.Y;
                M31 = value.Z;
                M41 = value.W;
            }
        }

        /// <summary>The second column of the matrix.</summary>
        public Vector4 Column2
        {
            get => new(M12, M22, M32, M42);
            set
            {
                M12 = value.X;
                M22 = value.Y;
                M32 = value.Z;
                M42 = value.W;
            }
        }

        /// <summary>The third column of the matrix.</summary>
        public Vector4 Column3
        {
            get => new(M13, M23, M33, M43);
            set
            {
                M13 = value.X;
                M23 = value.Y;
                M33 = value.Z;
                M43 = value.W;
            }
        }

        /// <summary>The zero matrix.</summary>
        public static Matrix4x3 Zero => new(0);

        /// <summary>Gets or sets the value at a specified position.</summary>
        /// <param name="index">The position index.</param>
        /// <returns>The value at the specified position.</returns>
        public float this[int index]
        {
            get
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
        public float this[int rowIndex, int columnIndex]
        {
            get
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
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="value">The value of all the matrix components.</param>
        public Matrix4x3(float value)
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
        public Matrix4x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33, float m41, float m42, float m43)
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
        public Matrix4x3(Vector3 row1, Vector3 row2, Vector3 row3, Vector3 row4)
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
        public Matrix4x3(Matrix2x2 matrix, float m13 = 0, float m23 = 0, float m31 = 0, float m32 = 0, float m33 = 1, float m41 = 0, float m42 = 0, float m43 = 0)
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
        public Matrix4x3(Matrix2x3 matrix, float m31 = 0, float m32 = 0, float m33 = 1, float m41 = 0, float m42 = 0, float m43 = 0)
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
        public Matrix4x3(Matrix3x2 matrix, float m13 = 0, float m23 = 0, float m33 = 1, float m41 = 0, float m42 = 0, float m43 = 0)
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
        public Matrix4x3(Matrix3x3 matrix, float m41 = 0, float m42 = 0, float m43 = 0)
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
        public Matrix4x3(Matrix4x2 matrix, float m13 = 0, float m23 = 0, float m33 = 1, float m43 = 0)
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

        /// <summary>Removes the translation from the matrix.</summary>
        public void RemoveTranslation() => Row4 = Vector3.Zero;

        /// <summary>Removes the rotation from the matrix.</summary>
        public void RemoveRotation()
        {
            Row1 = new(Row1.Length, 0, 0);
            Row2 = new(0, Row2.Length, 0);
            Row3 = new(0, 0, Row3.Length);
        }

        /// <summary>Removes the scale from the matrix.</summary>
        public void RemoveScale()
        {
            Row1.Normalise();
            Row2.Normalise();
            Row3.Normalise();
        }

        /// <summary>Gets the matrix as a <see cref="Matrix4x3D"/>.</summary>
        /// <returns>The matrix as a <see cref="Matrix4x3D"/>.</returns>
        public Matrix4x3D ToMatrix4x3D() => new(M11, M12, M13, M21, M22, M23, M31, M32, M33, M41, M42, M43);

        /// <inheritdoc/>
        public bool Equals(Matrix4x3 other) => this == other;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Matrix4x3 matrix && this == matrix;

        /// <inheritdoc/>
        public override int GetHashCode() => (M11, M12, M13, M21, M22, M23, M31, M32, M33, M41, M42, M43).GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => $"<M11: {M11}, M12: {M12}, M13: {M13}, M21: {M21}, M22: {M22}, M23: {M23}, M31: {M31}, M32: {M32}, M33: {M33}, M41: {M41}, M42: {M42}, M43: {M43}>";

        /// <summary>Creates a rotation matrix from an axis and an angle.</summary>
        /// <param name="axis">The axis to rotate around.</param>
        /// <param name="angle">The angle, in degrees, to rotate around the axis.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix4x3 CreateFromAxisAngle(Vector3 axis, float angle)
        {
            var rotationMatrix = Matrix3x3.CreateFromAxisAngle(axis, angle);
            return new(rotationMatrix);
        }

        /// <summary>Creates a rotation matrix from a quaternion.</summary>
        /// <param name="quaternion">The quaternion to create a rotation matrix from.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix4x3 CreateFromQuaternion(Quaternion quaternion)
        {
            quaternion.GetAxisAngle(out var axis, out var angle);
            return Matrix4x3.CreateFromAxisAngle(axis, angle);
        }

        /// <summary>Creates a rotation matrix for a rotation about the X axis.</summary>
        /// <param name="angle">The anti-clockwise angle, in degrees.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix4x3 CreateRotationX(float angle)
        {
            angle = MathsHelper.DegreesToRadians(angle);
            var sinAngle = MathF.Sin(angle);
            var cosAngle = MathF.Cos(angle);

            return new()
            {
                M11 = 1,
                M22 = cosAngle,
                M23 = sinAngle,
                M32 = -sinAngle,
                M33 = cosAngle
            };
        }

        /// <summary>Creates a rotation matrix for a rotation about the Y axis.</summary>
        /// <param name="angle">The anti-clockwise angle, in degrees.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix4x3 CreateRotationY(float angle)
        {
            angle = MathsHelper.DegreesToRadians(angle);
            var sinAngle = MathF.Sin(angle);
            var cosAngle = MathF.Cos(angle);

            return new()
            {
                M11 = cosAngle,
                M13 = -sinAngle,
                M22 = 1,
                M31 = sinAngle,
                M33 = cosAngle
            };
        }

        /// <summary>Creates a rotation matrix for a rotation about the Z axis.</summary>
        /// <param name="angle">The anti-clockwise angle, in degrees.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix4x3 CreateRotationZ(float angle)
        {
            angle = MathsHelper.DegreesToRadians(angle);
            var sinAngle = MathF.Sin(angle);
            var cosAngle = MathF.Cos(angle);

            return new()
            {
                M11 = cosAngle,
                M12 = sinAngle,
                M21 = -sinAngle,
                M22 = cosAngle,
                M33 = 1
            };
        }

        /// <summary>Creates a scale matrix.</summary>
        /// <param name="scale">The uniform scale factor.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix4x3 CreateScale(float scale) => new(scale, 0, 0, 0, scale, 0, 0, 0, scale, 0, 0, 0);

        /// <summary>Creates a scale matrix.</summary>
        /// <param name="x">The scale factor of the X axis.</param>
        /// <param name="y">The scale factor of the Y axis.</param>
        /// <param name="z">The scale factor of the Z axis.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix4x3 CreateScale(float x, float y, float z) => new(x, 0, 0, 0, y, 0, 0, 0, z, 0, 0, 0);

        /// <summary>Creates a scale matrix.</summary>
        /// <param name="scale">The scale factor of the X, Y, and Z axis.</param>
        /// <returns>The created matrix.</returns>
        public static Matrix4x3 CreateScale(Vector3 scale) => new(scale.X, 0, 0, 0, scale.Y, 0, 0, 0, scale.Z, 0, 0, 0);


        /*********
        ** Operators
        *********/
        /// <summary>Adds two matrices together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the addition.</returns>
        public static Matrix4x3 operator +(Matrix4x3 left, Matrix4x3 right)
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
        public static Matrix4x3 operator -(Matrix4x3 left, Matrix4x3 right)
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
        public static Matrix4x3 operator -(Matrix4x3 matrix) => matrix * -1;

        /// <summary>Multiplies a matrix by a scalar.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix4x3 operator *(float left, Matrix4x3 right)
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
        public static Matrix4x3 operator *(Matrix4x3 left, float right) => right * left;

        /// <summary>Multiplies two matrices together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix4x2 operator *(Matrix4x3 left, Matrix3x2 right)
        {
            return new(
                m11: Vector3.Dot(left.Row1, right.Column1),
                m12: Vector3.Dot(left.Row1, right.Column2),
                m21: Vector3.Dot(left.Row2, right.Column1),
                m22: Vector3.Dot(left.Row2, right.Column2),
                m31: Vector3.Dot(left.Row3, right.Column1),
                m32: Vector3.Dot(left.Row3, right.Column2),
                m41: Vector3.Dot(left.Row4, right.Column1),
                m42: Vector3.Dot(left.Row4, right.Column2)
            );
        }

        /// <summary>Multiplies two matrices together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix4x3 operator *(Matrix4x3 left, Matrix3x3 right)
        {
            return new(
                m11: Vector3.Dot(left.Row1, right.Column1),
                m12: Vector3.Dot(left.Row1, right.Column2),
                m13: Vector3.Dot(left.Row1, right.Column3),
                m21: Vector3.Dot(left.Row2, right.Column1),
                m22: Vector3.Dot(left.Row2, right.Column2),
                m23: Vector3.Dot(left.Row2, right.Column3),
                m31: Vector3.Dot(left.Row3, right.Column1),
                m32: Vector3.Dot(left.Row3, right.Column2),
                m33: Vector3.Dot(left.Row3, right.Column3),
                m41: Vector3.Dot(left.Row4, right.Column1),
                m42: Vector3.Dot(left.Row4, right.Column2),
                m43: Vector3.Dot(left.Row4, right.Column3)
            );
        }

        /// <summary>Multiplies two matrices together.</summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix4x4 operator *(Matrix4x3 left, Matrix3x4 right)
        {
            return new(
                m11: Vector3.Dot(left.Row1, right.Column1),
                m12: Vector3.Dot(left.Row1, right.Column2),
                m13: Vector3.Dot(left.Row1, right.Column3),
                m14: Vector3.Dot(left.Row1, right.Column4),
                m21: Vector3.Dot(left.Row2, right.Column1),
                m22: Vector3.Dot(left.Row2, right.Column2),
                m23: Vector3.Dot(left.Row2, right.Column3),
                m24: Vector3.Dot(left.Row2, right.Column4),
                m31: Vector3.Dot(left.Row3, right.Column1),
                m32: Vector3.Dot(left.Row3, right.Column2),
                m33: Vector3.Dot(left.Row3, right.Column3),
                m34: Vector3.Dot(left.Row3, right.Column4),
                m41: Vector3.Dot(left.Row4, right.Column1),
                m42: Vector3.Dot(left.Row4, right.Column2),
                m43: Vector3.Dot(left.Row4, right.Column3),
                m44: Vector3.Dot(left.Row4, right.Column4)
            );
        }

        /// <summary>Checks two matrices for equality.</summary>
        /// <param name="matrix1">The first matrix.</param>
        /// <param name="matrix2">The second matrix.</param>
        /// <returns><see langword="true"/> if the matrices are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Matrix4x3 matrix1, Matrix4x3 matrix2)
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
        public static bool operator !=(Matrix4x3 matrix1, Matrix4x3 matrix2) => !(matrix1 == matrix2);

        /// <summary>Converts a matrix to a tuple.</summary>
        /// <param name="matrix">The matrix to convert.</param>
        public static implicit operator (float M11, float M12, float M13, float M21, float M22, float M23, float M31, float M32, float M33, float M41, float M42, float M43)(Matrix4x3 matrix) => (matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23, matrix.M31, matrix.M32, matrix.M33, matrix.M41, matrix.M42, matrix.M43);

        /// <summary>Converts a tuple to a matrix.</summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator Matrix4x3((float M11, float M12, float M13, float M21, float M22, float M23, float M31, float M32, float M33, float M41, float M42, float M43) tuple) => new(tuple.M11, tuple.M12, tuple.M13, tuple.M21, tuple.M22, tuple.M23, tuple.M31, tuple.M32, tuple.M33, tuple.M41, tuple.M42, tuple.M43);

        /// <summary>Converts a <see cref="Matrix4x3"/> to a <see cref="Matrix4x3D"/>.</summary>
        /// <param name="matrix">The matrix to convert.</param>
        public static implicit operator Matrix4x3D(Matrix4x3 matrix) => matrix.ToMatrix4x3D();
    }
}

using NovaEngine.Maths;
using NUnit.Framework;
using System;

namespace NovaEngine.Tests.Maths
{
    /// <summary>The <see cref="Matrix2x3"/> tests.</summary>
    [TestFixture]
    public class Matrix2x3Tests
    {
        /*********
        ** Constants
        *********/
        /// <summary>The delta to use when checking if floating-point numbers are equal.</summary>
        private const double FloatingPointEqualsDelta = .0000001;


        /*********
        ** Public Methods
        *********/
        /// <summary>Tests <see cref="Matrix2x3.Trace"/>.</summary>
        /// <remarks>This tests that the trace is calculated correctly.</remarks>
        [Test]
        public void Trace_Get()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var trace = matrix.Trace;
            Assert.AreEqual(6, trace);
        }

        /// <summary>Tests <see cref="Matrix2x3.Diagonal"/>.</summary>
        /// <remarks>This tests that the diagonal is retrieved correctly.</remarks>
        [Test]
        public void Diagonal_Get()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var diagonal = matrix.Diagonal;
            Assert.AreEqual(new Vector2(1, 5), diagonal);
        }

        /// <summary>Tests <see cref="Matrix2x3.Diagonal"/>.</summary>
        /// <remarks>This tests that the diagonal is set correctly.</remarks>
        [Test]
        public void Diagonal_Set()
        {
            var matrix = new Matrix2x3();
            matrix.Diagonal = new Vector2(1, 2);
            Assert.AreEqual(new Matrix2x3(1, 0, 0, 0, 2, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.Transposed"/>.</summary>
        /// <remarks>This tests that the transpose is calculated correctly.</remarks>
        [Test]
        public void Transposed_Get()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var transposed = matrix.Transposed;
            Assert.AreEqual(new Matrix3x2(1, 4, 2, 5, 3, 6), transposed);
        }

        /// <summary>Tests <see cref="Matrix2x3.Row1"/>.</summary>
        /// <remarks>This tests that the first row is retrieved correctly.</remarks>
        [Test]
        public void Row1_Get()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var row1 = matrix.Row1;
            Assert.AreEqual(new Vector3(1, 2, 3), row1);
        }

        /// <summary>Tests <see cref="Matrix2x3.Row1"/>.</summary>
        /// <remarks>This tests that the first row is set correctly.</remarks>
        [Test]
        public void Row1_Set()
        {
            var matrix = new Matrix2x3();
            matrix.Row1 = new Vector3(1, 2, 3);
            Assert.AreEqual(new Matrix2x3(1, 2, 3, 0, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.Row2"/>.</summary>
        /// <remarks>This tests that the second row is retrieved correctly.</remarks>
        [Test]
        public void Row2_Get()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var row2 = matrix.Row2;
            Assert.AreEqual(new Vector3(4, 5, 6), row2);
        }

        /// <summary>Tests <see cref="Matrix2x3.Row2"/>.</summary>
        /// <remarks>This tests that the second row is set correctly.</remarks>
        [Test]
        public void Row2_Set()
        {
            var matrix = new Matrix2x3();
            matrix.Row2 = new Vector3(4, 5, 6);
            Assert.AreEqual(new Matrix2x3(0, 0, 0, 4, 5, 6), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.Column1"/>.</summary>
        /// <remarks>This tests that the first column is retrieved correctly.</remarks>
        [Test]
        public void Column1_Get()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var column1 = matrix.Column1;
            Assert.AreEqual(new Vector2(1, 4), column1);
        }

        /// <summary>Tests <see cref="Matrix2x3.Column1"/>.</summary>
        /// <remarks>This tests that the first column is set correctly.</remarks>
        [Test]
        public void Column1_Set()
        {
            var matrix = new Matrix2x3();
            matrix.Column1 = new Vector2(1, 4);
            Assert.AreEqual(new Matrix2x3(1, 0, 0, 4, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.Column2"/>.</summary>
        /// <remarks>This tests that the second column is retrieved correctly.</remarks>
        [Test]
        public void Column2_Get()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var column2 = matrix.Column2;
            Assert.AreEqual(new Vector2(2, 5), column2);
        }

        /// <summary>Tests <see cref="Matrix2x3.Column2"/>.</summary>
        /// <remarks>This tests that the second column is set correctly.</remarks>
        [Test]
        public void Column2_Set()
        {
            var matrix = new Matrix2x3();
            matrix.Column2 = new Vector2(2, 5);
            Assert.AreEqual(new Matrix2x3(0, 2, 0, 0, 5, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.Column3"/>.</summary>
        /// <remarks>This tests that the second column is retrieved correctly.</remarks>
        [Test]
        public void Column3_Get()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var column3 = matrix.Column3;
            Assert.AreEqual(new Vector2(3, 6), column3);
        }

        /// <summary>Tests <see cref="Matrix2x3.Column3"/>.</summary>
        /// <remarks>This tests that the second column is set correctly.</remarks>
        [Test]
        public void Column3_Set()
        {
            var matrix = new Matrix2x3();
            matrix.Column3 = new Vector2(3, 6);
            Assert.AreEqual(new Matrix2x3(0, 0, 3, 0, 0, 6), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.Zero"/>.</summary>
        /// <remarks>This tests that a zero matrix is returned.</remarks>
        [Test]
        public void Zero_Get()
        {
            var zeroMatrix = Matrix2x3.Zero;
            Assert.AreEqual(new Matrix2x3(), zeroMatrix);
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing in-bounds returns the correct element.</remarks>
        [Test]
        public void IndexerIntGet_IndexBetweenZeroAndFive_ReturnsElement()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var m11 = matrix[0];
            var m12 = matrix[1];
            var m13 = matrix[2];
            var m21 = matrix[3];
            var m22 = matrix[4];
            var m23 = matrix[5];
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m21);
            Assert.AreEqual(5, m22);
            Assert.AreEqual(6, m23);
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntGet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => _ = matrix[-1];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing more than '5' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntGet_IndexMoreThanFive_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => _ = matrix[6];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing in-bounds sets the correct element.</remarks>
        [Test]
        public void IndexerIntSet_IndexBetweenZeroAndFive_SetsElement()
        {
            var matrix = new Matrix2x3();
            matrix[0] = 1;
            matrix[1] = 2;
            matrix[2] = 3;
            matrix[3] = 4;
            matrix[4] = 5;
            matrix[5] = 6;
            Assert.AreEqual(new Matrix2x3(1, 2, 3, 4, 5, 6), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntSet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => matrix[-1] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing more than '3' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntSet_IndexMoreThanFive_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => matrix[6] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing in-bounds returns the correct element.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexXBetweenZeroAndOneAndIndexYBetweenZeroAndTwo_ReturnsElement()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var m11 = matrix[0, 0];
            var m12 = matrix[0, 1];
            var m13 = matrix[0, 2];
            var m21 = matrix[1, 0];
            var m22 = matrix[1, 1];
            var m23 = matrix[1, 2];
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m21);
            Assert.AreEqual(5, m22);
            Assert.AreEqual(6, m23);
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexXLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => _ = matrix[-1, 0];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing more than '1' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexXMoreThanOne_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => _ = matrix[2, 0];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexYLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => _ = matrix[0, -1];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing more than '2' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexYMoreThanTwo_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => _ = matrix[0, 3];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing in-bounds sets the correct element.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexBetweenZeroAndOne_SetsElement()
        {
            var matrix = new Matrix2x3();
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 4;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;
            Assert.AreEqual(new Matrix2x3(1, 2, 3, 4, 5, 6), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing less than '0' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexXLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => matrix[-1, 0] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing more than '1' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexXMoreThanOne_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => matrix[2, 0] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing less than '0' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexYLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => matrix[0, -1] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing more than '2' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexYMoreThanTwo_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x3();
            Action actual = () => matrix[0, 3] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x3(float)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorFloat_SetsElements()
        {
            var matrix = new Matrix2x3(1);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(1, m12);
            Assert.AreEqual(1, m13);
            Assert.AreEqual(1, m21);
            Assert.AreEqual(1, m22);
            Assert.AreEqual(1, m23);
        }

        /// <summary>Tests <see cref="Matrix2x3(float, float, float, float, float, float)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorFloatFloatFloatFloatFloatFloat_SetsElements()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m21);
            Assert.AreEqual(5, m22);
            Assert.AreEqual(6, m23);
        }

        /// <summary>Tests <see cref="Matrix2x3(in Vector3, in Vector3)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorVector3Vector3_SetsElements()
        {
            var matrix = new Matrix2x3(new(1, 2, 3), new(4, 5, 6));
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m21);
            Assert.AreEqual(5, m22);
            Assert.AreEqual(6, m23);
        }

        /// <summary>Tests <see cref="Matrix2x3(in Matrix2x2, float, float)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorMatrix2x2FloatFloat_SetsElements()
        {
            var matrix = new Matrix2x3(new(1, 2, 4, 5), 3, 6);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m21);
            Assert.AreEqual(5, m22);
            Assert.AreEqual(6, m23);
        }

        /// <summary>Tests <see cref="Matrix2x3.ToMatrix2x3D"/>.</summary>
        /// <remarks>This tests that the matrix is converted correctly.</remarks>
        [Test]
        public void ToMatrix2x3D_ConvertsMatrix()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrixD = matrix.ToMatrix2x3D();
            Assert.AreEqual(new Matrix2x3D(1, 2, 3, 4, 5, 6), matrixD);
        }

        /// <summary>Tests <see cref="Matrix2x3.Equals(Matrix2x3)"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void EqualsMatrix2x3_ValuesAreEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var areEqual = matrix1.Equals(matrix2);
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x3.Equals(Matrix2x3)"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void EqualsMatrix2x3_ValuesAreNotEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3();
            var areEqual = matrix1.Equals(matrix2);
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x3.Equals(object)"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void EqualsObject_ValuesAreEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var areEqual = matrix1.Equals((object)matrix2);
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x3.Equals(object)"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void EqualsObject_ValuesAreNotEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3();
            var areEqual = matrix1.Equals((object)matrix2);
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x3.CreateRotation(float)"/>.</summary>
        /// <remarks>This tests that an identity matrix is returned when no rotation is specified.</remarks>
        [Test]
        public void CreateRotation_NoRotation_ReturnsIdentitiyMatrix()
        {
            var matrix = Matrix2x3.CreateRotation(0);
            Assert.AreEqual(new Matrix2x3(1, 0, 0, 0, 1, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.CreateRotation(float)"/>.</summary>
        /// <remarks>This tests that a rotation matrix is correctly created with a specified rotation.</remarks>
        [Test]
        public void CreateRotation_WithRotation_ReturnsRotationMatrix()
        {
            var matrix = Matrix2x3.CreateRotation(90);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            Assert.AreEqual(0, m11, FloatingPointEqualsDelta);
            Assert.AreEqual(-1, m12, FloatingPointEqualsDelta);
            Assert.AreEqual(0, m13, FloatingPointEqualsDelta);
            Assert.AreEqual(1, m21, FloatingPointEqualsDelta);
            Assert.AreEqual(0, m22, FloatingPointEqualsDelta);
            Assert.AreEqual(0, m23, FloatingPointEqualsDelta);
        }

        /// <summary>Tests <see cref="Matrix2x3.CreateScale(float)"/>.</summary>
        /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified.</remarks>
        [Test]
        public void CreateScaleFloat_ScaleOfOne_ReturnsIdentityMatrix()
        {
            var matrix = Matrix2x3.CreateScale(1);
            Assert.AreEqual(new Matrix2x3(1, 0, 0, 0, 1, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.CreateScale(float)"/>.</summary>
        /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
        [Test]
        public void CreateScaleFloat_ScaleNotOne_ReturnsScaleMatrix()
        {
            var matrix = Matrix2x3.CreateScale(5);
            Assert.AreEqual(new Matrix2x3(5, 0, 0, 0, 5, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.CreateScale(float, float)"/>.</summary>
        /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified for both parameters.</remarks>
        [Test]
        public void CreateScaleFloatFloat_XScaleOfOneAndYScaleOfOne_ReturnsIdentityMatrix()
        {
            var matrix = Matrix2x3.CreateScale(1, 1);
            Assert.AreEqual(new Matrix2x3(1, 0, 0, 0, 1, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.CreateScale(float, float)"/>.</summary>
        /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
        [Test]
        public void CreateScaleFloatFloat_XYScaleNotOne_ReturnsScaleMatrix()
        {
            var matrix = Matrix2x3.CreateScale(2, 3);
            Assert.AreEqual(new Matrix2x3(2, 0, 0, 0, 3, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.CreateScale(Vector2)"/>.</summary>
        /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified for both parameters.</remarks>
        [Test]
        public void CreateScaleVector2_XScaleOfOneAndYScaleOfOne_ReturnsIdentityMatrix()
        {
            var matrix = Matrix2x3.CreateScale(Vector2.One);
            Assert.AreEqual(new Matrix2x3(1, 0, 0, 0, 1, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3.CreateScale(Vector2)"/>.</summary>
        /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
        [Test]
        public void CreateScaleVector2_XYScaleNotOne_ReturnsScaleMatrix()
        {
            var matrix = Matrix2x3.CreateScale(new Vector2(2, 3));
            Assert.AreEqual(new Matrix2x3(2, 0, 0, 0, 3, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> + <see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices are added correctly.</remarks>
        [Test]
        public void OperatorAddMatrixMatrix_AddsMatrixComponents()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3(1);
            var result = matrix1 + matrix2;
            Assert.AreEqual(new Matrix2x3(2, 3, 4, 5, 6, 7), result);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> - <see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices are subtracted correctly.</remarks>
        [Test]
        public void OperatorSubtractMatrixMatrix_SubtractsMatrixComponents()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3(1);
            var result = matrix1 - matrix2;
            Assert.AreEqual(new Matrix2x3(0, 1, 2, 3, 4, 5), result);
        }

        /// <summary>Tests -<see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices are inverted correctly.</remarks>
        [Test]
        public void OperatorInvert_InvertsMatrixComponents()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var result = -matrix;
            Assert.AreEqual(new Matrix2x3(-1, -2, -3, -4, -5, -6), result);
        }

        /// <summary>Tests <see cref="float"/> * <see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices are scaled correctly.</remarks>
        [Test]
        public void OperatorMultiplyFloatMatrix2x3_ScalesMatrixComponents()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var result = 2 * matrix;
            Assert.AreEqual(new Matrix2x3(2, 4, 6, 8, 10, 12), result);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> * <see cref="float"/>.</summary>
        /// <remarks>This tests that matrices are scaled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x3Float_ScalesMatrixComponents()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var result = matrix * 2;
            Assert.AreEqual(new Matrix2x3(2, 4, 6, 8, 10, 12), result);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> * <see cref="Matrix3x2"/>.</summary>
        /// <remarks>This tests that matrices are multipled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x3Matrix3x2_MultipliesMatrices()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix3x2(7, 8, 9, 10, 11, 12);
            var result = matrix1 * matrix2;
            Assert.AreEqual(new Matrix2x2(58, 64, 139, 154), result);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> * <see cref="Matrix3x3"/>.</summary>
        /// <remarks>This tests that matrices are multipled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x3Matrix3x3_MultipliesMatrices()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix3x3(7, 8, 9, 10, 11, 12, 13, 14, 15);
            var result = matrix1 * matrix2;
            Assert.AreEqual(new Matrix2x3(66, 72, 78, 156, 171, 186), result);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> * <see cref="Matrix3x4"/>.</summary>
        /// <remarks>This tests that matrices are multipled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x3Matrix3x4_MultipliesMatrices()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix3x4(7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18);
            var result = matrix1 * matrix2;
            Assert.AreEqual(new Matrix2x4(74, 80, 86, 92, 173, 188, 203, 218), result);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> == <see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void OperatorEquals_ValuesAreEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var areEqual = matrix1 == matrix2;
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> == <see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void OperatorEquals_ValuesAreNotEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3();
            var areEqual = matrix1 == matrix2;
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> != <see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void OperatorNotEquals_ValuesAreNotEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3();
            var areEqual = matrix1 != matrix2;
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x3"/> != <see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void OperatorNotEquals_ValuesAreEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrix2 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var areEqual = matrix1 != matrix2;
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests (<see cref="Matrix2x3"/>)(<see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>).</summary>
        /// <remarks>This tests that matrices are cast correctly.</remarks>
        [Test]
        public void OperatorCastMatrix2x3ToTupleFloatFloatFloatFloatFloatFloat_CastsMatrix()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var tuple = ((float M11, float M12, float M13, float M21, float M22, float M23))matrix;
            Assert.AreEqual((1, 2, 3, 4, 5, 6), tuple);
        }

        /// <summary>Tests ((<see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>))<see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices are cast correctly.</remarks>
        [Test]
        public void OperatorCastTupleFloatFloatFloatFloatFloatFloatToMatrix2x3_CastsMatrix()
        {
            var tuple = (1, 2, 3, 4, 5, 6);
            var matrix = (Matrix2x3)tuple;
            Assert.AreEqual(new Matrix2x3(1, 2, 3, 4, 5, 6), matrix);
        }

        /// <summary>Tests (<see cref="Matrix2x3D"/>)<see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices are cast correctly.</remarks>
        [Test]
        public void OperatorCastMatrix2x3ToMatrix2x3D_CastsMatrix()
        {
            var matrix = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var matrixD = (Matrix2x3D)matrix;
            Assert.AreEqual(new Matrix2x3D(1, 2, 3, 4, 5, 6), matrixD);
        }
    }
}

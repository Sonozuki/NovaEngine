using NovaEngine.Maths;
using NUnit.Framework;
using System;

namespace NovaEngine.Tests.Maths
{
    /// <summary>The <see cref="Matrix2x2"/> tests.</summary>
    [TestFixture]
    public class Matrix2x2Tests
    {
        /*********
        ** Constants
        *********/
        /// <summary>The delta to use when checking if floating-point numbers are equal.</summary>
        private const double FloatingPointEqualsDelta = .0000001;


        /*********
        ** Public Methods
        *********/
        /// <summary>Tests <see cref="Matrix2x2.IsIdentity"/>.</summary>
        /// <remarks>This tests that <see langword="true"/> is returned when the matrix is an identity matrix.</remarks>
        [Test]
        public void IsIdentity_IsIdentity_ReturnsTrue()
        {
            var matrix = new Matrix2x2(1, 0, 0, 1);
            Assert.IsTrue(matrix.IsIdentity);
        }

        /// <summary>Tests <see cref="Matrix2x2.IsIdentity"/>.</summary>
        /// <remarks>This tests that <see langword="false"/> is returned when the matrix is not an identity matrix.</remarks>
        [Test]
        public void IsIdentity_IsNotIdentity_ReturnsFalse()
        {
            var matrix = new Matrix2x2();
            Assert.IsFalse(matrix.IsIdentity);
        }

        /// <summary>Tests <see cref="Matrix2x2.Determinant"/>.</summary>
        /// <remarks>This tests that the determinant is calculated correctly.</remarks>
        [Test]
        public void Determinant_Get()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var determinant = matrix.Determinant;
            Assert.AreEqual(-2, determinant);
        }

        /// <summary>Tests <see cref="Matrix2x2.Trace"/>.</summary>
        /// <remarks>This tests that the trace is calculated correctly.</remarks>
        [Test]
        public void Trace_Get()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var trace = matrix.Trace;
            Assert.AreEqual(5, trace);
        }

        /// <summary>Tests <see cref="Matrix2x2.Diagonal"/>.</summary>
        /// <remarks>This tests that the diagonal is retrieved correctly.</remarks>
        [Test]
        public void Diagonal_Get()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var diagonal = matrix.Diagonal;
            Assert.AreEqual(new Vector2(1, 4), diagonal);
        }

        /// <summary>Tests <see cref="Matrix2x2.Diagonal"/>.</summary>
        /// <remarks>This tests that the diagonal is set correctly.</remarks>
        [Test]
        public void Diagonal_Set()
        {
            var matrix = new Matrix2x2();
            matrix.Diagonal = new Vector2(1, 2);
            Assert.AreEqual(new Matrix2x2(1, 0, 0, 2), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.Transposed"/>.</summary>
        /// <remarks>This tests that the transpose is calculated correctly.</remarks>
        [Test]
        public void Transposed_Get()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var transposed = matrix.Transposed;
            Assert.AreEqual(new Matrix2x2(1, 3, 2, 4), transposed);
        }

        /// <summary>Tests <see cref="Matrix2x2.Inverse"/>.</summary>
        /// <remarks>This tests that the unchanged matrix is returned when the determinant is '0'.</remarks>
        [Test]
        public void Inverted_DeterminantIsZero_ReturnsUnchangedMatrix()
        {
            var matrix = Matrix2x2.Identity;
            var inverted = matrix.Inverse;
            Assert.AreEqual(matrix, inverted);
        }

        /// <summary>Tests <see cref="Matrix2x2.Inverse"/>.</summary>
        /// <remarks>This tests that the inverse of the matrix is returned when the determinant is not '0'.</remarks>
        [Test]
        public void Inverted_DeterminantIsNotZero_ReturnsInvertedMatrix()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var inverted = matrix.Inverse;
            Assert.AreEqual(new Matrix2x2(-2, 1, 1.5f, -.5f), inverted);
        }

        /// <summary>Tests <see cref="Matrix2x2.Row1"/>.</summary>
        /// <remarks>This tests that the first row is retrieved correctly.</remarks>
        [Test]
        public void Row1_Get()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var row1 = matrix.Row1;
            Assert.AreEqual(new Vector2(1, 2), row1);
        }

        /// <summary>Tests <see cref="Matrix2x2.Row1"/>.</summary>
        /// <remarks>This tests that the first row is set correctly.</remarks>
        [Test]
        public void Row1_Set()
        {
            var matrix = new Matrix2x2();
            matrix.Row1 = new Vector2(1, 2);
            Assert.AreEqual(new Matrix2x2(1, 2, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.Row2"/>.</summary>
        /// <remarks>This tests that the second row is retrieved correctly.</remarks>
        [Test]
        public void Row2_Get()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var row2 = matrix.Row2;
            Assert.AreEqual(new Vector2(3, 4), row2);
        }

        /// <summary>Tests <see cref="Matrix2x2.Row2"/>.</summary>
        /// <remarks>This tests that the second row is set correctly.</remarks>
        [Test]
        public void Row2_Set()
        {
            var matrix = new Matrix2x2();
            matrix.Row2 = new Vector2(3, 4);
            Assert.AreEqual(new Matrix2x2(0, 0, 3, 4), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.Column1"/>.</summary>
        /// <remarks>This tests that the first column is retrieved correctly.</remarks>
        [Test]
        public void Column1_Get()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var column1 = matrix.Column1;
            Assert.AreEqual(new Vector2(1, 3), column1);
        }

        /// <summary>Tests <see cref="Matrix2x2.Column1"/>.</summary>
        /// <remarks>This tests that the first column is set correctly.</remarks>
        [Test]
        public void Column1_Set()
        {
            var matrix = new Matrix2x2();
            matrix.Column1 = new Vector2(1, 3);
            Assert.AreEqual(new Matrix2x2(1, 0, 3, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.Column2"/>.</summary>
        /// <remarks>This tests that the second column is retrieved correctly.</remarks>
        [Test]
        public void Column2_Get()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var column2 = matrix.Column2;
            Assert.AreEqual(new Vector2(2, 4), column2);
        }

        /// <summary>Tests <see cref="Matrix2x2.Column2"/>.</summary>
        /// <remarks>This tests that the second column is set correctly.</remarks>
        [Test]
        public void Column2_Set()
        {
            var matrix = new Matrix2x2();
            matrix.Column2 = new Vector2(2, 4);
            Assert.AreEqual(new Matrix2x2(0, 2, 0, 4), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.Zero"/>.</summary>
        /// <remarks>This tests that a zero matrix is returned.</remarks>
        [Test]
        public void Zero_Get()
        {
            var zeroMatrix = Matrix2x2.Zero;
            Assert.AreEqual(new Matrix2x2(), zeroMatrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.Identity"/>.</summary>
        /// <remarks>This tests that an identity matrix is returned.</remarks>
        [Test]
        public void Identity_Get()
        {
            var identityMatrix = Matrix2x2.Identity;
            Assert.AreEqual(new Matrix2x2(1, 0, 0, 1), identityMatrix);
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing in-bounds returns the correct element.</remarks>
        [Test]
        public void IndexerIntGet_IndexBetweenZeroAndThree_ReturnsElement()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var m11 = matrix[0];
            var m12 = matrix[1];
            var m21 = matrix[2];
            var m22 = matrix[3];
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m21);
            Assert.AreEqual(4, m22);
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntGet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => _ = matrix[-1];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing more than '3' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntGet_IndexMoreThanThree_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => _ = matrix[4];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing in-bounds sets the correct element.</remarks>
        [Test]
        public void IndexerIntSet_IndexBetweenZeroAndThree_SetsElement()
        {
            var matrix = new Matrix2x2();
            matrix[0] = 1;
            matrix[1] = 2;
            matrix[2] = 3;
            matrix[3] = 4;
            Assert.AreEqual(new Matrix2x2(1, 2, 3, 4), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntSet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => matrix[-1] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing more than '3' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntSet_IndexMoreThanThree_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => matrix[4] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing in-bounds returns the correct element.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexBetweenZeroAndOne_ReturnsElement()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var m11 = matrix[0, 0];
            var m12 = matrix[0, 1];
            var m21 = matrix[1, 0];
            var m22 = matrix[1, 1];
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m21);
            Assert.AreEqual(4, m22);
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexXLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => _ = matrix[-1, 0];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing more than '1' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexXMoreThanOne_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => _ = matrix[2, 0];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexYLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => _ = matrix[0, -1];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing more than '1' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexYMoreThanOne_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => _ = matrix[0, 2];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing in-bounds sets the correct element.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexBetweenZeroAndOne_SetsElement()
        {
            var matrix = new Matrix2x2();
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[1, 0] = 3;
            matrix[1, 1] = 4;
            Assert.AreEqual(new Matrix2x2(1, 2, 3, 4), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing less than '0' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexXLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => matrix[-1, 0] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing more than '1' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexXMoreThanOne_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => matrix[2, 0] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing less than '0' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexYLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => matrix[0, -1] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing more than '1' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexYMoreThanOne_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x2();
            Action actual = () => matrix[0, 2] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x2(float)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorFloat_SetsElements()
        {
            var matrix = new Matrix2x2(1);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(1, m12);
            Assert.AreEqual(1, m21);
            Assert.AreEqual(1, m22);
        }

        /// <summary>Tests <see cref="Matrix2x2(float, float, float, float)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorFloatFloatFloatFloat_SetsElements()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m21);
            Assert.AreEqual(4, m22);
        }

        /// <summary>Tests <see cref="Matrix2x2(in Vector2, in Vector2)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorVector2Vector2_SetsElements()
        {
            var matrix = new Matrix2x2(new(1, 2), new(3, 4));
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m21);
            Assert.AreEqual(4, m22);
        }

        /// <summary>Tests <see cref="Matrix2x2.Transpose"/>.</summary>
        /// <remarks>This tests that the transpose is calculated correctly.</remarks>
        [Test]
        public void Transpose_TransposesMatrix()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            matrix.Transpose();
            Assert.AreEqual(new Matrix2x2(1, 3, 2, 4), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.Invert"/>.</summary>
        /// <remarks>This tests that the matrix is unchanged when the determinant is '0'.</remarks>
        [Test]
        public void Invert_DeterminantIsZero_DoesntChangeMatrix()
        {
            var matrix = Matrix2x2.Identity;
            matrix.Invert();
            Assert.AreEqual(Matrix2x2.Identity, matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.Invert"/>.</summary>
        /// <remarks>This tests that the matrix is inverted when the determinant is not '0'.</remarks>
        [Test]
        public void Invert_DeterminantIsNotZero_InvertsMatrix()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            matrix.Invert();
            Assert.AreEqual(new Matrix2x2(-2, 1, 1.5f, -.5f), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.ToMatrix2x2D"/>.</summary>
        /// <remarks>This tests that the matrix is converted correctly.</remarks>
        [Test]
        public void ToMatrix2x2D_ConvertsMatrix()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var matrixD = matrix.ToMatrix2x2D();
            Assert.AreEqual(new Matrix2x2D(1, 2, 3, 4), matrixD);
        }

        /// <summary>Tests <see cref="Matrix2x2.Equals(Matrix2x2)"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void EqualsMatrix2x2_ValuesAreEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2(1, 2, 3, 4);
            var areEqual = matrix1.Equals(matrix2);
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x2.Equals(Matrix2x2)"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void EqualsMatrix2x2_ValuesAreNotEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2();
            var areEqual = matrix1.Equals(matrix2);
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x2.Equals(object)"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void EqualsObject_ValuesAreEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2(1, 2, 3, 4);
            var areEqual = matrix1.Equals((object)matrix2);
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x2.Equals(object)"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void EqualsObject_ValuesAreNotEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2();
            var areEqual = matrix1.Equals((object)matrix2);
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x2.CreateRotation(float)"/>.</summary>
        /// <remarks>This tests that an identity matrix is returned when no rotation is specified.</remarks>
        [Test]
        public void CreateRotation_NoRotation_ReturnsIdentitiyMatrix()
        {
            var matrix = Matrix2x2.CreateRotation(0);
            Assert.AreEqual(Matrix2x2.Identity, matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.CreateRotation(float)"/>.</summary>
        /// <remarks>This tests that a rotation matrix is correctly created with a specified rotation.</remarks>
        [Test]
        public void CreateRotation_WithRotation_ReturnsRotationMatrix()
        {
            var matrix = Matrix2x2.CreateRotation(90);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            Assert.AreEqual(0, m11, FloatingPointEqualsDelta);
            Assert.AreEqual(-1, m12, FloatingPointEqualsDelta);
            Assert.AreEqual(1, m21, FloatingPointEqualsDelta);
            Assert.AreEqual(0, m22, FloatingPointEqualsDelta);
        }

        /// <summary>Tests <see cref="Matrix2x2.CreateScale(float)"/>.</summary>
        /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified.</remarks>
        [Test]
        public void CreateScaleFloat_ScaleOfOne_ReturnsIdentityMatrix()
        {
            var matrix = Matrix2x2.CreateScale(1);
            Assert.AreEqual(Matrix2x2.Identity, matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.CreateScale(float)"/>.</summary>
        /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
        [Test]
        public void CreateScaleFloat_ScaleNotOne_ReturnsScaleMatrix()
        {
            var matrix = Matrix2x2.CreateScale(5);
            Assert.AreEqual(new Matrix2x2(5, 0, 0, 5), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.CreateScale(float, float)"/>.</summary>
        /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified for both parameters.</remarks>
        [Test]
        public void CreateScaleFloatFloat_XScaleOfOneAndYScaleOfOne_ReturnsIdentityMatrix()
        {
            var matrix = Matrix2x2.CreateScale(1, 1);
            Assert.AreEqual(Matrix2x2.Identity, matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.CreateScale(float, float)"/>.</summary>
        /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
        [Test]
        public void CreateScaleFloatFloat_XYScaleNotOne_ReturnsScaleMatrix()
        {
            var matrix = Matrix2x2.CreateScale(2, 3);
            Assert.AreEqual(new Matrix2x2(2, 0, 0, 3), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.CreateScale(Vector2)"/>.</summary>
        /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified for both parameters.</remarks>
        [Test]
        public void CreateScaleVector2_XScaleOfOneAndYScaleOfOne_ReturnsIdentityMatrix()
        {
            var matrix = Matrix2x2.CreateScale(Vector2.One);
            Assert.AreEqual(Matrix2x2.Identity, matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2.CreateScale(Vector2)"/>.</summary>
        /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
        [Test]
        public void CreateScaleVector2_XYScaleNotOne_ReturnsScaleMatrix()
        {
            var matrix = Matrix2x2.CreateScale(new Vector2(2, 3));
            Assert.AreEqual(new Matrix2x2(2, 0, 0, 3), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> + <see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices are added correctly.</remarks>
        [Test]
        public void OperatorAddMatrixMatrix_AddsMatrixComponents()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2(1);
            var result = matrix1 + matrix2;
            Assert.AreEqual(new Matrix2x2(2, 3, 4, 5), result);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> - <see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices are subtracted correctly.</remarks>
        [Test]
        public void OperatorSubtractMatrixMatrix_SubtractsMatrixComponents()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2(1);
            var result = matrix1 - matrix2;
            Assert.AreEqual(new Matrix2x2(0, 1, 2, 3), result);
        }

        /// <summary>Tests -<see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices are inverted correctly.</remarks>
        [Test]
        public void OperatorInvert_InvertsMatrixComponents()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var result = -matrix;
            Assert.AreEqual(new Matrix2x2(-1, -2, -3, -4), result);
        }

        /// <summary>Tests <see cref="float"/> * <see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices are scaled correctly.</remarks>
        [Test]
        public void OperatorMultiplyFloatMatrix2x2_ScalesMatrixComponents()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var result = 2 * matrix;
            Assert.AreEqual(new Matrix2x2(2, 4, 6, 8), result);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> * <see cref="float"/>.</summary>
        /// <remarks>This tests that matrices are scaled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x2Float_ScalesMatrixComponents()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var result = matrix * 2;
            Assert.AreEqual(new Matrix2x2(2, 4, 6, 8), result);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> * <see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices are multipled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x2Matrix2x2_MultipliesMatrices()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2(1, 2, 3, 4);
            var result = matrix1 * matrix2;
            Assert.AreEqual(new Matrix2x2(7, 10, 15, 22), result);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> * <see cref="Matrix2x3"/>.</summary>
        /// <remarks>This tests that matrices are multipled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x2Matrix2x3_MultipliesMatrices()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x3(1, 2, 3, 4, 5, 6);
            var result = matrix1 * matrix2;
            Assert.AreEqual(new Matrix2x3(9, 12, 15, 19, 26, 33), result);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> * <see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices are multipled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x2Matrix2x4_MultipliesMatrices()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var result = matrix1 * matrix2;
            Assert.AreEqual(new Matrix2x4(11, 14, 17, 20, 23, 30, 37, 44), result);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> == <see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void OperatorEquals_ValuesAreEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2(1, 2, 3, 4);
            var areEqual = matrix1 == matrix2;
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> == <see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void OperatorEquals_ValuesAreNotEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2();
            var areEqual = matrix1 == matrix2;
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> != <see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void OperatorNotEquals_ValuesAreNotEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2();
            var areEqual = matrix1 != matrix2;
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x2"/> != <see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void OperatorNotEquals_ValuesAreEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x2(1, 2, 3, 4);
            var matrix2 = new Matrix2x2(1, 2, 3, 4);
            var areEqual = matrix1 != matrix2;
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests (<see cref="Matrix2x2"/>)(<see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>).</summary>
        /// <remarks>This tests that matrices are cast correctly.</remarks>
        [Test]
        public void OperatorCastMatrix2x2ToTupleFloatFloatFloatFloat_CastsMatrix()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var tuple = ((float M11, float M12, float M21, float M22))matrix;
            Assert.AreEqual((1, 2, 3, 4), tuple);
        }

        /// <summary>Tests ((<see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>))<see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices are cast correctly.</remarks>
        [Test]
        public void OperatorCastTupleFloatFloatFloatFloatToMatrix2x2_CastsMatrix()
        {
            var tuple = (1, 2, 3, 4);
            var matrix = (Matrix2x2)tuple;
            Assert.AreEqual(new Matrix2x2(1, 2, 3, 4), matrix);
        }

        /// <summary>Tests (<see cref="Matrix2x2D"/>)<see cref="Matrix2x2"/>.</summary>
        /// <remarks>This tests that matrices are cast correctly.</remarks>
        [Test]
        public void OperatorCastMatrix2x2ToMatrix2x2D_CastsMatrix()
        {
            var matrix = new Matrix2x2(1, 2, 3, 4);
            var matrixD = (Matrix2x2D)matrix;
            Assert.AreEqual(new Matrix2x2D(1, 2, 3, 4), matrixD);
        }
    }
}

﻿using NovaEngine.Maths;
using NUnit.Framework;
using System;

namespace NovaEngine.Tests.Maths
{
    /// <summary>The <see cref="Matrix2x4"/> tests.</summary>
    [TestFixture]
    public class Matrix2x4Tests
    {
        /*********
        ** Constants
        *********/
        /// <summary>The delta to use when checking if floating-point numbers are equal.</summary>
        private const double FloatingPointEqualsDelta = .0000001;


        /*********
        ** Public Methods
        *********/
        /// <summary>Tests <see cref="Matrix2x4.Trace"/>.</summary>
        /// <remarks>This tests that the trace is calculated correctly.</remarks>
        [Test]
        public void Trace_Get()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var trace = matrix.Trace;
            Assert.AreEqual(7, trace);
        }

        /// <summary>Tests <see cref="Matrix2x4.Diagonal"/>.</summary>
        /// <remarks>This tests that the diagonal is retrieved correctly.</remarks>
        [Test]
        public void Diagonal_Get()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var diagonal = matrix.Diagonal;
            Assert.AreEqual(new Vector2(1, 6), diagonal);
        }

        /// <summary>Tests <see cref="Matrix2x4.Diagonal"/>.</summary>
        /// <remarks>This tests that the diagonal is set correctly.</remarks>
        [Test]
        public void Diagonal_Set()
        {
            var matrix = new Matrix2x4();
            matrix.Diagonal = new Vector2(1, 2);
            Assert.AreEqual(new Matrix2x4(1, 0, 0, 0, 0, 2, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.Transposed"/>.</summary>
        /// <remarks>This tests that the transpose is calculated correctly.</remarks>
        [Test]
        public void Transposed_Get()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var transposed = matrix.Transposed;
            Assert.AreEqual(new Matrix4x2(1, 5, 2, 6, 3, 7, 4, 8), transposed);
        }

        /// <summary>Tests <see cref="Matrix2x4.Row1"/>.</summary>
        /// <remarks>This tests that the first row is retrieved correctly.</remarks>
        [Test]
        public void Row1_Get()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var row1 = matrix.Row1;
            Assert.AreEqual(new Vector4(1, 2, 3, 4), row1);
        }

        /// <summary>Tests <see cref="Matrix2x4.Row1"/>.</summary>
        /// <remarks>This tests that the first row is set correctly.</remarks>
        [Test]
        public void Row1_Set()
        {
            var matrix = new Matrix2x4();
            matrix.Row1 = new Vector4(1, 2, 3, 4);
            Assert.AreEqual(new Matrix2x4(1, 2, 3, 4, 0, 0, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.Row2"/>.</summary>
        /// <remarks>This tests that the second row is retrieved correctly.</remarks>
        [Test]
        public void Row2_Get()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var row2 = matrix.Row2;
            Assert.AreEqual(new Vector4(5, 6, 7, 8), row2);
        }

        /// <summary>Tests <see cref="Matrix2x4.Row2"/>.</summary>
        /// <remarks>This tests that the second row is set correctly.</remarks>
        [Test]
        public void Row2_Set()
        {
            var matrix = new Matrix2x4();
            matrix.Row2 = new Vector4(5, 6, 7, 8);
            Assert.AreEqual(new Matrix2x4(0, 0, 0, 0, 5, 6, 7, 8), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.Column1"/>.</summary>
        /// <remarks>This tests that the first column is retrieved correctly.</remarks>
        [Test]
        public void Column1_Get()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var column1 = matrix.Column1;
            Assert.AreEqual(new Vector2(1, 5), column1);
        }

        /// <summary>Tests <see cref="Matrix2x4.Column1"/>.</summary>
        /// <remarks>This tests that the first column is set correctly.</remarks>
        [Test]
        public void Column1_Set()
        {
            var matrix = new Matrix2x4();
            matrix.Column1 = new Vector2(1, 5);
            Assert.AreEqual(new Matrix2x4(1, 0, 0, 0, 5, 0, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.Column2"/>.</summary>
        /// <remarks>This tests that the second column is retrieved correctly.</remarks>
        [Test]
        public void Column2_Get()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var column2 = matrix.Column2;
            Assert.AreEqual(new Vector2(2, 6), column2);
        }

        /// <summary>Tests <see cref="Matrix2x4.Column2"/>.</summary>
        /// <remarks>This tests that the second column is set correctly.</remarks>
        [Test]
        public void Column2_Set()
        {
            var matrix = new Matrix2x4();
            matrix.Column2 = new Vector2(2, 5);
            Assert.AreEqual(new Matrix2x4(0, 2, 0, 0, 0, 5, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.Column3"/>.</summary>
        /// <remarks>This tests that the second column is retrieved correctly.</remarks>
        [Test]
        public void Column3_Get()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var column3 = matrix.Column3;
            Assert.AreEqual(new Vector2(3, 7), column3);
        }

        /// <summary>Tests <see cref="Matrix2x4.Column3"/>.</summary>
        /// <remarks>This tests that the second column is set correctly.</remarks>
        [Test]
        public void Column3_Set()
        {
            var matrix = new Matrix2x4();
            matrix.Column3 = new Vector2(3, 6);
            Assert.AreEqual(new Matrix2x4(0, 0, 3, 0, 0, 0, 6, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.Column4"/>.</summary>
        /// <remarks>This tests that the second column is retrieved correctly.</remarks>
        [Test]
        public void Column4_Get()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var column4 = matrix.Column4;
            Assert.AreEqual(new Vector2(4, 8), column4);
        }

        /// <summary>Tests <see cref="Matrix2x4.Column4"/>.</summary>
        /// <remarks>This tests that the second column is set correctly.</remarks>
        [Test]
        public void Column4_Set()
        {
            var matrix = new Matrix2x4();
            matrix.Column4 = new Vector2(4, 8);
            Assert.AreEqual(new Matrix2x4(0, 0, 0, 4, 0, 0, 0, 8), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.Zero"/>.</summary>
        /// <remarks>This tests that a zero matrix is returned.</remarks>
        [Test]
        public void Zero_Get()
        {
            var zeroMatrix = Matrix2x4.Zero;
            Assert.AreEqual(new Matrix2x4(), zeroMatrix);
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing in-bounds returns the correct element.</remarks>
        [Test]
        public void IndexerIntGet_IndexBetweenZeroAndSeven_ReturnsElement()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var m11 = matrix[0];
            var m12 = matrix[1];
            var m13 = matrix[2];
            var m14 = matrix[3];
            var m21 = matrix[4];
            var m22 = matrix[5];
            var m23 = matrix[6];
            var m24 = matrix[7];
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m14);
            Assert.AreEqual(5, m21);
            Assert.AreEqual(6, m22);
            Assert.AreEqual(7, m23);
            Assert.AreEqual(8, m24);
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntGet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => _ = matrix[-1];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing more than '5' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntGet_IndexMoreThanSeven_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => _ = matrix[8];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing in-bounds sets the correct element.</remarks>
        [Test]
        public void IndexerIntSet_IndexBetweenZeroAndSeven_SetsElement()
        {
            var matrix = new Matrix2x4();
            matrix[0] = 1;
            matrix[1] = 2;
            matrix[2] = 3;
            matrix[3] = 4;
            matrix[4] = 5;
            matrix[5] = 6;
            matrix[6] = 7;
            matrix[7] = 8;
            Assert.AreEqual(new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntSet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => matrix[-1] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing more than '3' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntSet_IndexMoreThanSeven_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => matrix[8] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing in-bounds returns the correct element.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexXBetweenZeroAndOneAndIndexYBetweenZeroAndThree_ReturnsElement()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var m11 = matrix[0, 0];
            var m12 = matrix[0, 1];
            var m13 = matrix[0, 2];
            var m14 = matrix[0, 3];
            var m21 = matrix[1, 0];
            var m22 = matrix[1, 1];
            var m23 = matrix[1, 2];
            var m24 = matrix[1, 3];
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m14);
            Assert.AreEqual(5, m21);
            Assert.AreEqual(6, m22);
            Assert.AreEqual(7, m23);
            Assert.AreEqual(8, m24);
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexXLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => _ = matrix[-1, 0];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing more than '1' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexXMoreThanOne_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => _ = matrix[2, 0];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing less than '0' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexYLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => _ = matrix[0, -1];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that getting an element via indexing more than '2' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntGet_IndexYMoreThanThree_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => _ = matrix[0, 4];
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing in-bounds sets the correct element.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexBetweenZeroAndOne_SetsElement()
        {
            var matrix = new Matrix2x4();
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[0, 3] = 4;
            matrix[1, 0] = 5;
            matrix[1, 1] = 6;
            matrix[1, 2] = 7;
            matrix[1, 3] = 8;
            Assert.AreEqual(new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing less than '0' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexXLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => matrix[-1, 0] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing more than '1' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexXMoreThanOne_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => matrix[2, 0] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing less than '0' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexYLessThanZero_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => matrix[0, -1] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4"/>[<see langword="int"/>, <see langword="int"/>].</summary>
        /// <remarks>This tests that setting an element via indexing more than '2' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
        [Test]
        public void IndexerIntIntSet_IndexYMoreThanThree_ThrowsIndexOutOfRangeException()
        {
            var matrix = new Matrix2x4();
            Action actual = () => matrix[0, 4] = 0;
            Assert.Throws<IndexOutOfRangeException>(new(actual));
        }

        /// <summary>Tests <see cref="Matrix2x4(float)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorFloat_SetsElements()
        {
            var matrix = new Matrix2x4(1);
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

        /// <summary>Tests <see cref="Matrix2x4(float, float, float, float, float, float, float, float)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorFloatFloatFloatFloatFloatFloatFloatFloat_SetsElements()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m14 = matrix.M14;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            var m24 = matrix.M24;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m14);
            Assert.AreEqual(5, m21);
            Assert.AreEqual(6, m22);
            Assert.AreEqual(7, m23);
            Assert.AreEqual(8, m24);
        }

        /// <summary>Tests <see cref="Matrix2x4(in Vector4, in Vector4)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorVector4Vector4_SetsElements()
        {
            var matrix = new Matrix2x4(new(1, 2, 3, 4), new(5, 6, 7, 8));
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m14 = matrix.M14;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            var m24 = matrix.M24;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m14);
            Assert.AreEqual(5, m21);
            Assert.AreEqual(6, m22);
            Assert.AreEqual(7, m23);
            Assert.AreEqual(8, m24);
        }

        /// <summary>Tests <see cref="Matrix2x4(in Matrix2x2, float, float, float, float)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorMatrix2x2FloatFloatFloatFloat_SetsElements()
        {
            var matrix = new Matrix2x4(new(1, 2, 5, 6), 3, 4, 7, 8);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m14 = matrix.M14;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            var m24 = matrix.M24;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m14);
            Assert.AreEqual(5, m21);
            Assert.AreEqual(6, m22);
            Assert.AreEqual(7, m23);
            Assert.AreEqual(8, m24);
        }

        /// <summary>Tests <see cref="Matrix2x4(in Matrix2x3, float, float)"/>.</summary>
        /// <remarks>This tests that the elements get set correctly.</remarks>
        [Test]
        public void ConstructorMatrix2x3FloatFloat_SetsElements()
        {
            var matrix = new Matrix2x4(new(1, 2, 3, 5, 6, 7), 4, 8);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m14 = matrix.M14;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            var m24 = matrix.M24;
            Assert.AreEqual(1, m11);
            Assert.AreEqual(2, m12);
            Assert.AreEqual(3, m13);
            Assert.AreEqual(4, m14);
            Assert.AreEqual(5, m21);
            Assert.AreEqual(6, m22);
            Assert.AreEqual(7, m23);
            Assert.AreEqual(8, m24);
        }

        /// <summary>Tests <see cref="Matrix2x4.ToMatrix2x4D"/>.</summary>
        /// <remarks>This tests that the matrix is converted correctly.</remarks>
        [Test]
        public void ToMatrix2x4D_ConvertsMatrix()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrixD = matrix.ToMatrix2x4D();
            Assert.AreEqual(new Matrix2x4D(1, 2, 3, 4, 5, 6, 7, 8), matrixD);
        }

        /// <summary>Tests <see cref="Matrix2x4.Equals(Matrix2x4)"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void EqualsMatrix2x4_ValuesAreEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var areEqual = matrix1.Equals(matrix2);
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x4.Equals(Matrix2x4)"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void EqualsMatrix2x4_ValuesAreNotEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4();
            var areEqual = matrix1.Equals(matrix2);
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x4.Equals(object)"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void EqualsObject_ValuesAreEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var areEqual = matrix1.Equals((object)matrix2);
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x4.Equals(object)"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void EqualsObject_ValuesAreNotEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4();
            var areEqual = matrix1.Equals((object)matrix2);
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x4.CreateRotation(float)"/>.</summary>
        /// <remarks>This tests that an identity matrix is returned when no rotation is specified.</remarks>
        [Test]
        public void CreateRotation_NoRotation_ReturnsIdentitiyMatrix()
        {
            var matrix = Matrix2x4.CreateRotation(0);
            Assert.AreEqual(new Matrix2x4(1, 0, 0, 0, 0, 1, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.CreateRotation(float)"/>.</summary>
        /// <remarks>This tests that a rotation matrix is correctly created with a specified rotation.</remarks>
        [Test]
        public void CreateRotation_WithRotation_ReturnsRotationMatrix()
        {
            var matrix = Matrix2x4.CreateRotation(90);
            var m11 = matrix.M11;
            var m12 = matrix.M12;
            var m13 = matrix.M13;
            var m14 = matrix.M14;
            var m21 = matrix.M21;
            var m22 = matrix.M22;
            var m23 = matrix.M23;
            var m24 = matrix.M24;
            Assert.AreEqual(0, m11, FloatingPointEqualsDelta);
            Assert.AreEqual(-1, m12, FloatingPointEqualsDelta);
            Assert.AreEqual(0, m13, FloatingPointEqualsDelta);
            Assert.AreEqual(0, m14, FloatingPointEqualsDelta);
            Assert.AreEqual(1, m21, FloatingPointEqualsDelta);
            Assert.AreEqual(0, m22, FloatingPointEqualsDelta);
            Assert.AreEqual(0, m23, FloatingPointEqualsDelta);
            Assert.AreEqual(0, m24, FloatingPointEqualsDelta);
        }

        /// <summary>Tests <see cref="Matrix2x4.CreateScale(float)"/>.</summary>
        /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified.</remarks>
        [Test]
        public void CreateScaleFloat_ScaleOfOne_ReturnsIdentityMatrix()
        {
            var matrix = Matrix2x4.CreateScale(1);
            Assert.AreEqual(new Matrix2x4(1, 0, 0, 0, 0, 1, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.CreateScale(float)"/>.</summary>
        /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
        [Test]
        public void CreateScaleFloat_ScaleNotOne_ReturnsScaleMatrix()
        {
            var matrix = Matrix2x4.CreateScale(5);
            Assert.AreEqual(new Matrix2x4(5, 0, 0, 0, 0, 5, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.CreateScale(float, float)"/>.</summary>
        /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified for both parameters.</remarks>
        [Test]
        public void CreateScaleFloatFloat_XScaleOfOneAndYScaleOfOne_ReturnsIdentityMatrix()
        {
            var matrix = Matrix2x4.CreateScale(1, 1);
            Assert.AreEqual(new Matrix2x4(1, 0, 0, 0, 0, 1, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.CreateScale(float, float)"/>.</summary>
        /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
        [Test]
        public void CreateScaleFloatFloat_XYScaleNotOne_ReturnsScaleMatrix()
        {
            var matrix = Matrix2x4.CreateScale(2, 3);
            Assert.AreEqual(new Matrix2x4(2, 0, 0, 0, 0, 3, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.CreateScale(Vector2)"/>.</summary>
        /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified for both parameters.</remarks>
        [Test]
        public void CreateScaleVector2_XScaleOfOneAndYScaleOfOne_ReturnsIdentityMatrix()
        {
            var matrix = Matrix2x4.CreateScale(Vector2.One);
            Assert.AreEqual(new Matrix2x4(1, 0, 0, 0, 0, 1, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4.CreateScale(Vector2)"/>.</summary>
        /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
        [Test]
        public void CreateScaleVector2_XYScaleNotOne_ReturnsScaleMatrix()
        {
            var matrix = Matrix2x4.CreateScale(new Vector2(2, 3));
            Assert.AreEqual(new Matrix2x4(2, 0, 0, 0, 0, 3, 0, 0), matrix);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> + <see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices are added correctly.</remarks>
        [Test]
        public void OperatorAddMatrixMatrix_AddsMatrixComponents()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4(1);
            var result = matrix1 + matrix2;
            Assert.AreEqual(new Matrix2x4(2, 3, 4, 5, 6, 7, 8, 9), result);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> - <see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices are subtracted correctly.</remarks>
        [Test]
        public void OperatorSubtractMatrixMatrix_SubtractsMatrixComponents()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4(1);
            var result = matrix1 - matrix2;
            Assert.AreEqual(new Matrix2x4(0, 1, 2, 3, 4, 5, 6, 7), result);
        }

        /// <summary>Tests -<see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices are inverted correctly.</remarks>
        [Test]
        public void OperatorInvert_InvertsMatrixComponents()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var result = -matrix;
            Assert.AreEqual(new Matrix2x4(-1, -2, -3, -4, -5, -6, -7, -8), result);
        }

        /// <summary>Tests <see cref="float"/> * <see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices are scaled correctly.</remarks>
        [Test]
        public void OperatorMultiplyFloatMatrix2x4_ScalesMatrixComponents()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var result = 2 * matrix;
            Assert.AreEqual(new Matrix2x4(2, 4, 6, 8, 10, 12, 14, 16), result);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> * <see cref="float"/>.</summary>
        /// <remarks>This tests that matrices are scaled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x4Float_ScalesMatrixComponents()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var result = matrix * 2;
            Assert.AreEqual(new Matrix2x4(2, 4, 6, 8, 10, 12, 14, 16), result);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> * <see cref="Matrix4x2"/>.</summary>
        /// <remarks>This tests that matrices are multipled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x4Matrix4x2_MultipliesMatrices()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix4x2(9, 10, 11, 12, 13, 14, 15, 16);
            var result = matrix1 * matrix2;
            Assert.AreEqual(new Matrix2x2(130, 140, 322, 348), result);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> * <see cref="Matrix4x3"/>.</summary>
        /// <remarks>This tests that matrices are multipled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x4Matrix4x3_MultipliesMatrices()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix4x3(9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20);
            var result = matrix1 * matrix2;
            Assert.AreEqual(new Matrix2x3(150, 160, 170, 366, 392, 418), result);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> * <see cref="Matrix4x4"/>.</summary>
        /// <remarks>This tests that matrices are multipled correctly.</remarks>
        [Test]
        public void OperatorMultiplyMatrix2x4Matrix4x4_MultipliesMatrices()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix4x4(9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24);
            var result = matrix1 * matrix2;
            Assert.AreEqual(new Matrix2x4(170, 180, 190, 200, 410, 436, 462, 488), result);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> == <see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void OperatorEquals_ValuesAreEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var areEqual = matrix1 == matrix2;
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> == <see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void OperatorEquals_ValuesAreNotEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4();
            var areEqual = matrix1 == matrix2;
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> != <see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
        [Test]
        public void OperatorNotEquals_ValuesAreNotEqual_ReturnsTrue()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4();
            var areEqual = matrix1 != matrix2;
            Assert.IsTrue(areEqual);
        }

        /// <summary>Tests <see cref="Matrix2x4"/> != <see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
        [Test]
        public void OperatorNotEquals_ValuesAreEqual_ReturnsFalse()
        {
            var matrix1 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrix2 = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var areEqual = matrix1 != matrix2;
            Assert.IsFalse(areEqual);
        }

        /// <summary>Tests (<see cref="Matrix2x4"/>)(<see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>).</summary>
        /// <remarks>This tests that matrices are cast correctly.</remarks>
        [Test]
        public void OperatorCastMatrix2x4ToTupleFloatFloatFloatFloatFloatFloatFloatFloat_CastsMatrix()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var tuple = ((float M11, float M12, float M13, float M14, float M21, float M22, float M23, float M24))matrix;
            Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, 8), tuple);
        }

        /// <summary>Tests ((<see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>))<see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices are cast correctly.</remarks>
        [Test]
        public void OperatorCastTupleFloatFloatFloatFloatFloatFloatFloatFloatToMatrix2x4_CastsMatrix()
        {
            var tuple = (1, 2, 3, 4, 5, 6, 7, 8);
            var matrix = (Matrix2x4)tuple;
            Assert.AreEqual(new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8), matrix);
        }

        /// <summary>Tests (<see cref="Matrix2x4D"/>)<see cref="Matrix2x4"/>.</summary>
        /// <remarks>This tests that matrices are cast correctly.</remarks>
        [Test]
        public void OperatorCastMatrix2x4ToMatrix2x4D_CastsMatrix()
        {
            var matrix = new Matrix2x4(1, 2, 3, 4, 5, 6, 7, 8);
            var matrixD = (Matrix2x4D)matrix;
            Assert.AreEqual(new Matrix2x4D(1, 2, 3, 4, 5, 6, 7, 8), matrixD);
        }
    }
}

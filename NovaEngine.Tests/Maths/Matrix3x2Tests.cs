using NovaEngine.Maths;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaEngine.Tests.Maths;

/// <summary>The <see cref="Matrix3x2"/> tests.</summary>
[TestFixture]
public class Matrix3x2Tests
{
    /*********
    ** Constants
    *********/
    /// <summary>The delta to use when checking if floating-point numbers are equal.</summary>
    private const double FloatingPointEqualsDelta = .0000001;


    /*********
    ** Public Methods
    *********/
    /// <summary>Tests <see cref="Matrix3x2.Trace"/>.</summary>
    /// <remarks>This tests that the trace is calculated correctly.</remarks>
    [Test]
    public void Trace_Get()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var trace = matrix.Trace;
        Assert.AreEqual(5, trace);
    }

    /// <summary>Tests <see cref="Matrix3x2.Diagonal"/>.</summary>
    /// <remarks>This tests that the diagonal is retrieved correctly.</remarks>
    [Test]
    public void Diagonal_Get()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var diagonal = matrix.Diagonal;
        Assert.AreEqual(new Vector2(1, 4), diagonal);
    }

    /// <summary>Tests <see cref="Matrix3x2.Diagonal"/>.</summary>
    /// <remarks>This tests that the diagonal is set correctly.</remarks>
    [Test]
    public void Diagonal_Set()
    {
        var matrix = new Matrix3x2();
        matrix.Diagonal = new Vector2(1, 2);
        Assert.AreEqual(new Matrix3x2(1, 0, 0, 2, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.Transposed"/>.</summary>
    /// <remarks>This tests that the transpose is calculated correctly.</remarks>
    [Test]
    public void Transposed_Get()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var transposed = matrix.Transposed;
        Assert.AreEqual(new Matrix2x3(1, 3, 5, 2, 4, 6), transposed);
    }

    /// <summary>Tests <see cref="Matrix3x2.Row1"/>.</summary>
    /// <remarks>This tests that the first row is retrieved correctly.</remarks>
    [Test]
    public void Row1_Get()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var row1 = matrix.Row1;
        Assert.AreEqual(new Vector2(1, 2), row1);
    }

    /// <summary>Tests <see cref="Matrix3x2.Row1"/>.</summary>
    /// <remarks>This tests that the first row is set correctly.</remarks>
    [Test]
    public void Row1_Set()
    {
        var matrix = new Matrix3x2();
        matrix.Row1 = new Vector2(1, 2);
        Assert.AreEqual(new Matrix3x2(1, 2, 0, 0, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.Row2"/>.</summary>
    /// <remarks>This tests that the second row is retrieved correctly.</remarks>
    [Test]
    public void Row2_Get()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var row2 = matrix.Row2;
        Assert.AreEqual(new Vector2(3, 4), row2);
    }

    /// <summary>Tests <see cref="Matrix3x2.Row2"/>.</summary>
    /// <remarks>This tests that the second row is set correctly.</remarks>
    [Test]
    public void Row2_Set()
    {
        var matrix = new Matrix3x2();
        matrix.Row2 = new Vector2(3, 4);
        Assert.AreEqual(new Matrix3x2(0, 0, 3, 4, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.Row3"/>.</summary>
    /// <remarks>This tests that the second row is retrieved correctly.</remarks>
    [Test]
    public void Row3_Get()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var row3 = matrix.Row3;
        Assert.AreEqual(new Vector2(5, 6), row3);
    }

    /// <summary>Tests <see cref="Matrix3x2.Row3"/>.</summary>
    /// <remarks>This tests that the second row is set correctly.</remarks>
    [Test]
    public void Row3_Set()
    {
        var matrix = new Matrix3x2();
        matrix.Row3 = new Vector2(5, 6);
        Assert.AreEqual(new Matrix3x2(0, 0, 0, 0, 5, 6), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.Column1"/>.</summary>
    /// <remarks>This tests that the first column is retrieved correctly.</remarks>
    [Test]
    public void Column1_Get()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var column1 = matrix.Column1;
        Assert.AreEqual(new Vector3(1, 3, 5), column1);
    }

    /// <summary>Tests <see cref="Matrix3x2.Column1"/>.</summary>
    /// <remarks>This tests that the first column is set correctly.</remarks>
    [Test]
    public void Column1_Set()
    {
        var matrix = new Matrix3x2();
        matrix.Column1 = new Vector3(1, 3, 5);
        Assert.AreEqual(new Matrix3x2(1, 0, 3, 0, 5, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.Column2"/>.</summary>
    /// <remarks>This tests that the second column is retrieved correctly.</remarks>
    [Test]
    public void Column2_Get()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var column2 = matrix.Column2;
        Assert.AreEqual(new Vector3(2, 4, 6), column2);
    }

    /// <summary>Tests <see cref="Matrix3x2.Column2"/>.</summary>
    /// <remarks>This tests that the second column is set correctly.</remarks>
    [Test]
    public void Column2_Set()
    {
        var matrix = new Matrix3x2();
        matrix.Column2 = new Vector3(2, 4, 6);
        Assert.AreEqual(new Matrix3x2(0, 2, 0, 4, 0, 6), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.Zero"/>.</summary>
    /// <remarks>This tests that a zero matrix is returned.</remarks>
    [Test]
    public void Zero_Get()
    {
        var zeroMatrix = Matrix3x2.Zero;
        Assert.AreEqual(new Matrix3x2(), zeroMatrix);
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing in-bounds returns the correct element.</remarks>
    [Test]
    public void IndexerIntGet_IndexBetweenZeroAndFive_ReturnsElement()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
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

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing less than '0' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntGet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => _ = matrix[-1];
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing more than '5' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntGet_IndexMoreThanFive_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => _ = matrix[6];
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing in-bounds sets the correct element.</remarks>
    [Test]
    public void IndexerIntSet_IndexBetweenZeroAndFive_SetsElement()
    {
        var matrix = new Matrix3x2();
        matrix[0] = 1;
        matrix[1] = 2;
        matrix[2] = 3;
        matrix[3] = 4;
        matrix[4] = 5;
        matrix[5] = 6;
        Assert.AreEqual(new Matrix3x2(1, 2, 3, 4, 5, 6), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing less than '0' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntSet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => matrix[-1] = 0;
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing more than '3' throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntSet_IndexMoreThanFive_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => matrix[6] = 0;
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing in-bounds returns the correct element.</remarks>
    [Test]
    public void IndexerIntIntGet_IndexXBetweenZeroAndTwoAndIndexYBetweenZeroAndOne_ReturnsElement()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var m11 = matrix[0, 0];
        var m12 = matrix[0, 1];
        var m21 = matrix[1, 0];
        var m22 = matrix[1, 1];
        var m31 = matrix[2, 0];
        var m32 = matrix[2, 1];
        Assert.AreEqual(1, m11);
        Assert.AreEqual(2, m12);
        Assert.AreEqual(3, m21);
        Assert.AreEqual(4, m22);
        Assert.AreEqual(5, m31);
        Assert.AreEqual(6, m32);
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing less than '0' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntIntGet_IndexXLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => _ = matrix[-1, 0];
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing more than '1' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntIntGet_IndexXMoreThanTwo_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => _ = matrix[3, 0];
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing less than '0' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntIntGet_IndexYLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => _ = matrix[0, -1];
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing more than '2' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntIntGet_IndexYMoreThanOne_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => _ = matrix[0, 2];
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing in-bounds sets the correct element.</remarks>
    [Test]
    public void IndexerIntIntSet_IndexBetweenZeroAndOne_SetsElement()
    {
        var matrix = new Matrix3x2();
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[1, 0] = 3;
        matrix[1, 1] = 4;
        matrix[2, 0] = 5;
        matrix[2, 1] = 6;
        Assert.AreEqual(new Matrix3x2(1, 2, 3, 4, 5, 6), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing less than '0' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntIntSet_IndexXLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => matrix[-1, 0] = 0;
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing more than '1' on the first index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntIntSet_IndexXMoreThanTwo_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => matrix[3, 0] = 0;
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing less than '0' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntIntSet_IndexYLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => matrix[0, -1] = 0;
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2"/>[<see langword="int"/>, <see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing more than '2' on the second index dimension throws a <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerIntIntSet_IndexYMoreThanOne_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix3x2();
        Action actual = () => matrix[0, 2] = 0;
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Matrix3x2(float)"/>.</summary>
    /// <remarks>This tests that the elements get set correctly.</remarks>
    [Test]
    public void ConstructorFloat_SetsElements()
    {
        var matrix = new Matrix3x2(1);
        var m11 = matrix.M11;
        var m12 = matrix.M12;
        var m21 = matrix.M21;
        var m22 = matrix.M22;
        var m31 = matrix.M31;
        var m32 = matrix.M32;
        Assert.AreEqual(1, m11);
        Assert.AreEqual(1, m12);
        Assert.AreEqual(1, m21);
        Assert.AreEqual(1, m22);
        Assert.AreEqual(1, m31);
        Assert.AreEqual(1, m32);
    }

    /// <summary>Tests <see cref="Matrix3x2(float, float, float, float, float, float)"/>.</summary>
    /// <remarks>This tests that the elements get set correctly.</remarks>
    [Test]
    public void ConstructorFloatFloatFloatFloatFloatFloat_SetsElements()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var m11 = matrix.M11;
        var m12 = matrix.M12;
        var m21 = matrix.M21;
        var m22 = matrix.M22;
        var m31 = matrix.M31;
        var m32 = matrix.M32;
        Assert.AreEqual(1, m11);
        Assert.AreEqual(2, m12);
        Assert.AreEqual(3, m21);
        Assert.AreEqual(4, m22);
        Assert.AreEqual(5, m31);
        Assert.AreEqual(6, m32);
    }

    /// <summary>Tests <see cref="Matrix3x2(in Vector2, in Vector2, in Vector2)"/>.</summary>
    /// <remarks>This tests that the elements get set correctly.</remarks>
    [Test]
    public void ConstructorVector2Vector2Vector2_SetsElements()
    {
        var matrix = new Matrix3x2(new Vector2(1, 2), new(3, 4), new(5, 6));
        var m11 = matrix.M11;
        var m12 = matrix.M12;
        var m21 = matrix.M21;
        var m22 = matrix.M22;
        var m31 = matrix.M31;
        var m32 = matrix.M32;
        Assert.AreEqual(1, m11);
        Assert.AreEqual(2, m12);
        Assert.AreEqual(3, m21);
        Assert.AreEqual(4, m22);
        Assert.AreEqual(5, m31);
        Assert.AreEqual(6, m32);
    }

    /// <summary>Tests <see cref="Matrix3x2(in Matrix2x2, float, float)"/>.</summary>
    /// <remarks>This tests that the elements get set correctly.</remarks>
    [Test]
    public void ConstructorMatrix2x2FloatFloat_SetsElements()
    {
        var matrix = new Matrix3x2(new(1, 2, 3, 4), 5, 6);
        var m11 = matrix.M11;
        var m12 = matrix.M12;
        var m21 = matrix.M21;
        var m22 = matrix.M22;
        var m31 = matrix.M31;
        var m32 = matrix.M32;
        Assert.AreEqual(1, m11);
        Assert.AreEqual(2, m12);
        Assert.AreEqual(3, m21);
        Assert.AreEqual(4, m22);
        Assert.AreEqual(5, m31);
        Assert.AreEqual(6, m32);
    }

    /// <summary>Tests <see cref="Matrix3x2.ToMatrix3x2D"/>.</summary>
    /// <remarks>This tests that the matrix is converted correctly.</remarks>
    [Test]
    public void ToMatrix3x2D_ConvertsMatrix()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrixD = matrix.ToMatrix3x2D();
        Assert.AreEqual(new Matrix3x2D(1, 2, 3, 4, 5, 6), matrixD);
    }

    /// <summary>Tests <see cref="Matrix3x2.Equals(Matrix3x2)"/>.</summary>
    /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
    [Test]
    public void EqualsMatrix3x2_ValuesAreEqual_ReturnsTrue()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var areEqual = matrix1.Equals(matrix2);
        Assert.IsTrue(areEqual);
    }

    /// <summary>Tests <see cref="Matrix3x2.Equals(Matrix3x2)"/>.</summary>
    /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
    [Test]
    public void EqualsMatrix3x2_ValuesAreNotEqual_ReturnsFalse()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2();
        var areEqual = matrix1.Equals(matrix2);
        Assert.IsFalse(areEqual);
    }

    /// <summary>Tests <see cref="Matrix3x2.Equals(object)"/>.</summary>
    /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
    [Test]
    public void EqualsObject_ValuesAreEqual_ReturnsTrue()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var areEqual = matrix1.Equals((object)matrix2);
        Assert.IsTrue(areEqual);
    }

    /// <summary>Tests <see cref="Matrix3x2.Equals(object)"/>.</summary>
    /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
    [Test]
    public void EqualsObject_ValuesAreNotEqual_ReturnsFalse()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2();
        var areEqual = matrix1.Equals((object)matrix2);
        Assert.IsFalse(areEqual);
    }

    /// <summary>Tests <see cref="Matrix3x2.CreateRotation(float)"/>.</summary>
    /// <remarks>This tests that an identity matrix is returned when no rotation is specified.</remarks>
    [Test]
    public void CreateRotation_NoRotation_ReturnsIdentitiyMatrix()
    {
        var matrix = Matrix3x2.CreateRotation(0);
        Assert.AreEqual(new Matrix3x2(1, 0, 0, 1, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.CreateRotation(float)"/>.</summary>
    /// <remarks>This tests that a rotation matrix is correctly created with a specified rotation.</remarks>
    [Test]
    public void CreateRotation_WithRotation_ReturnsRotationMatrix()
    {
        var matrix = Matrix3x2.CreateRotation(90);
        var m11 = matrix.M11;
        var m12 = matrix.M12;
        var m21 = matrix.M21;
        var m22 = matrix.M22;
        var m31 = matrix.M31;
        var m32 = matrix.M32;
        Assert.AreEqual(0, m11, FloatingPointEqualsDelta);
        Assert.AreEqual(-1, m12, FloatingPointEqualsDelta);
        Assert.AreEqual(1, m21, FloatingPointEqualsDelta);
        Assert.AreEqual(0, m22, FloatingPointEqualsDelta);
        Assert.AreEqual(0, m31, FloatingPointEqualsDelta);
        Assert.AreEqual(0, m32, FloatingPointEqualsDelta);
    }

    /// <summary>Tests <see cref="Matrix3x2.CreateScale(float)"/>.</summary>
    /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified.</remarks>
    [Test]
    public void CreateScaleFloat_ScaleOfOne_ReturnsIdentityMatrix()
    {
        var matrix = Matrix3x2.CreateScale(1);
        Assert.AreEqual(new Matrix3x2(1, 0, 0, 1, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.CreateScale(float)"/>.</summary>
    /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
    [Test]
    public void CreateScaleFloat_ScaleNotOne_ReturnsScaleMatrix()
    {
        var matrix = Matrix3x2.CreateScale(5);
        Assert.AreEqual(new Matrix3x2(5, 0, 0, 5, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.CreateScale(float, float)"/>.</summary>
    /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified for both parameters.</remarks>
    [Test]
    public void CreateScaleFloatFloat_XScaleOfOneAndYScaleOfOne_ReturnsIdentityMatrix()
    {
        var matrix = Matrix3x2.CreateScale(1, 1);
        Assert.AreEqual(new Matrix3x2(1, 0, 0, 1, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.CreateScale(float, float)"/>.</summary>
    /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
    [Test]
    public void CreateScaleFloatFloat_XYScaleNotOne_ReturnsScaleMatrix()
    {
        var matrix = Matrix3x2.CreateScale(2, 3);
        Assert.AreEqual(new Matrix3x2(2, 0, 0, 3, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.CreateScale(Vector2)"/>.</summary>
    /// <remarks>This tests that an identitiy matrix is returned when a scale of '1' is specified for both parameters.</remarks>
    [Test]
    public void CreateScaleVector2_XScaleOfOneAndYScaleOfOne_ReturnsIdentityMatrix()
    {
        var matrix = Matrix3x2.CreateScale(Vector2.One);
        Assert.AreEqual(new Matrix3x2(1, 0, 0, 1, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2.CreateScale(Vector2)"/>.</summary>
    /// <remarks>This tests that a scale matrix is correctly created with the specified scale.</remarks>
    [Test]
    public void CreateScaleVector2_XYScaleNotOne_ReturnsScaleMatrix()
    {
        var matrix = Matrix3x2.CreateScale(new Vector2(2, 3));
        Assert.AreEqual(new Matrix3x2(2, 0, 0, 3, 0, 0), matrix);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> + <see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices are added correctly.</remarks>
    [Test]
    public void OperatorAddMatrixMatrix_AddsMatrixComponents()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2(1);
        var result = matrix1 + matrix2;
        Assert.AreEqual(new Matrix3x2(2, 3, 4, 5, 6, 7), result);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> - <see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices are subtracted correctly.</remarks>
    [Test]
    public void OperatorSubtractMatrixMatrix_SubtractsMatrixComponents()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2(1);
        var result = matrix1 - matrix2;
        Assert.AreEqual(new Matrix3x2(0, 1, 2, 3, 4, 5), result);
    }

    /// <summary>Tests -<see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices are inverted correctly.</remarks>
    [Test]
    public void OperatorInvert_InvertsMatrixComponents()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var result = -matrix;
        Assert.AreEqual(new Matrix3x2(-1, -2, -3, -4, -5, -6), result);
    }

    /// <summary>Tests <see cref="float"/> * <see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices are scaled correctly.</remarks>
    [Test]
    public void OperatorMultiplyFloatMatrix3x2_ScalesMatrixComponents()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var result = 2 * matrix;
        Assert.AreEqual(new Matrix3x2(2, 4, 6, 8, 10, 12), result);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> * <see cref="float"/>.</summary>
    /// <remarks>This tests that matrices are scaled correctly.</remarks>
    [Test]
    public void OperatorMultiplyMatrix3x2Float_ScalesMatrixComponents()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var result = matrix * 2;
        Assert.AreEqual(new Matrix3x2(2, 4, 6, 8, 10, 12), result);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> * <see cref="Matrix2x2"/>.</summary>
    /// <remarks>This tests that matrices are multipled correctly.</remarks>
    [Test]
    public void OperatorMultiplyMatrix3x2Matrix2x2_MultipliesMatrices()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix2x2(7, 8, 9, 10);
        var result = matrix1 * matrix2;
        Assert.AreEqual(new Matrix3x2(25, 28, 57, 64, 89, 100), result);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> * <see cref="Matrix2x3"/>.</summary>
    /// <remarks>This tests that matrices are multipled correctly.</remarks>
    [Test]
    public void OperatorMultiplyMatrix3x2Matrix2x3_MultipliesMatrices()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix2x3(7, 8, 9, 10, 11, 12);
        var result = matrix1 * matrix2;
        Assert.AreEqual(new Matrix3x3(27, 30, 33, 61, 68, 75, 95, 106, 117), result);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> * <see cref="Matrix2x4"/>.</summary>
    /// <remarks>This tests that matrices are multipled correctly.</remarks>
    [Test]
    public void OperatorMultiplyMatrix3x2Matrix2x4_MultipliesMatrices()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix2x4(7, 8, 9, 10, 11, 12, 13, 14);
        var result = matrix1 * matrix2;
        Assert.AreEqual(new Matrix3x4(29, 32, 35, 38, 65, 72, 79, 86, 101, 112, 123, 134), result);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> == <see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
    [Test]
    public void OperatorEquals_ValuesAreEqual_ReturnsTrue()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var areEqual = matrix1 == matrix2;
        Assert.IsTrue(areEqual);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> == <see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
    [Test]
    public void OperatorEquals_ValuesAreNotEqual_ReturnsFalse()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2();
        var areEqual = matrix1 == matrix2;
        Assert.IsFalse(areEqual);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> != <see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices aren't considered equals when they aren't equal.</remarks>
    [Test]
    public void OperatorNotEquals_ValuesAreNotEqual_ReturnsTrue()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2();
        var areEqual = matrix1 != matrix2;
        Assert.IsTrue(areEqual);
    }

    /// <summary>Tests <see cref="Matrix3x2"/> != <see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices are considered equals when they are equal.</remarks>
    [Test]
    public void OperatorNotEquals_ValuesAreEqual_ReturnsFalse()
    {
        var matrix1 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrix2 = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var areEqual = matrix1 != matrix2;
        Assert.IsFalse(areEqual);
    }

    /// <summary>Tests (<see cref="Matrix3x2"/>)(<see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>).</summary>
    /// <remarks>This tests that matrices are cast correctly.</remarks>
    [Test]
    public void OperatorCastMatrix3x2ToTupleFloatFloatFloatFloatFloatFloat_CastsMatrix()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var tuple = ((float M11, float M12, float M21, float M22, float M31, float M32))matrix;
        Assert.AreEqual((1, 2, 3, 4, 5, 6), tuple);
    }

    /// <summary>Tests ((<see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>, <see cref="float"/>))<see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices are cast correctly.</remarks>
    [Test]
    public void OperatorCastTupleFloatFloatFloatFloatFloatFloatToMatrix3x2_CastsMatrix()
    {
        var tuple = (1, 2, 3, 4, 5, 6);
        var matrix = (Matrix3x2)tuple;
        Assert.AreEqual(new Matrix3x2(1, 2, 3, 4, 5, 6), matrix);
    }

    /// <summary>Tests (<see cref="Matrix3x2D"/>)<see cref="Matrix3x2"/>.</summary>
    /// <remarks>This tests that matrices are cast correctly.</remarks>
    [Test]
    public void OperatorCastMatrix3x2ToMatrix3x2D_CastsMatrix()
    {
        var matrix = new Matrix3x2(1, 2, 3, 4, 5, 6);
        var matrixD = (Matrix3x2D)matrix;
        Assert.AreEqual(new Matrix3x2D(1, 2, 3, 4, 5, 6), matrixD);
    }
}

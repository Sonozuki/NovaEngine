namespace NovaEngine.Tests.Maths;

/// <summary>The <see cref="Vector2D"/> tests.</summary>
[TestFixture]
public class Vector2DTests
{
    /*********
    ** Constants
    *********/
    /// <summary>The delta to use when checking if floating-point numbers are equal.</summary>
    private const double FloatingPointEqualsDelta = .0000001;


    /*********
    ** Public Methods
    *********/
    /// <summary>Tests <see cref="Vector2D.Length"/>.</summary>
    /// <remarks>This tests that the length is calculated correctly.</remarks>
    [Test]
    public void Length_Get()
    {
        var vector = new Vector2D(3, 4);
        var length = vector.Length;
        Assert.AreEqual(5, length);
    }

    /// <summary>Tests <see cref="Vector2D.LengthSquared"/>.</summary>
    /// <remarks>This tests that the squared length is calculated correctly.</remarks>
    [Test]
    public void LengthSquared_Get()
    {
        var vector = new Vector2D(3, 4);
        var lengthSquared = vector.LengthSquared;
        Assert.AreEqual(25, lengthSquared);
    }

    /// <summary>Tests <see cref="Vector2D.PerpendicularLeft"/>.</summary>
    /// <remarks>This tests that the left perpendicular is calculated correctly.</remarks>
    [Test]
    public void PerpendicularLeft_Get()
    {
        var vector = new Vector2D(1, 2);
        var perpendicularLeft = vector.PerpendicularLeft;
        Assert.AreEqual(new Vector2D(-2, 1), perpendicularLeft);
    }

    /// <summary>Tests <see cref="Vector2D.PerpendicularRight"/>.</summary>
    /// <remarks>This tests that the right perpendicular is calculated correctly.</remarks>
    [Test]
    public void PerpendicularRight_Get()
    {
        var vector = new Vector2D(1, 2);
        var perpendicularRight = vector.PerpendicularRight;
        Assert.AreEqual(new Vector2D(2, -1), perpendicularRight);
    }

    /// <summary>Tests <see cref="Vector2D.Normalised"/>.</summary>
    /// <remarks>This tests that the vector is unchanged when the length is '0'.</remarks>
    [Test]
    public void Normalised_LengthEqualsZero_ReturnsIdenticalVector()
    {
        var vector = new Vector2D(0);
        var normalised = vector.Normalised;
        Assert.AreEqual(new Vector2D(0), normalised);
    }

    /// <summary>Tests <see cref="Vector2D.Normalised"/>.</summary>
    /// <remarks>This tests that the vector is normalised when the length isn't '0'.</remarks>
    [Test]
    public void Normalised_LengthIsNotZero_ReturnsNormalisedVector()
    {
        var vector = new Vector2D(1, 2);
        var normalised = vector.Normalised;
        Assert.AreEqual(.4472135, normalised.X, FloatingPointEqualsDelta);
        Assert.AreEqual(.8944271, normalised.Y, FloatingPointEqualsDelta);
    }

    /// <summary>Tests <see cref="Vector2D.YX"/>.</summary>
    /// <remarks>This tests that the YX swizzle is retrieved correctly.</remarks>
    [Test]
    public void YX_Get()
    {
        var vector = new Vector2D(1, 2);
        var yx = vector.YX;
        Assert.AreEqual(new Vector2D(2, 1), yx);
    }

    /// <summary>Tests <see cref="Vector2D.YX"/>.</summary>
    /// <remarks>This tests that the YX swizzle is set correctly.</remarks>
    [Test]
    public void YX_Set()
    {
        var vector = new Vector2D(1, 2);
        vector.YX = new Vector2D(3, 4);
        Assert.AreEqual(new Vector2D(4, 3), vector);
    }

    /// <summary>Tests <see cref="Vector2D.Zero"/>.</summary>
    /// <remarks>This tests a zero vector is returned.</remarks>
    [Test]
    public void Zero_Get()
    {
        var zero = Vector2D.Zero;
        Assert.AreEqual(new Vector2D(0), zero);
    }

    /// <summary>Tests <see cref="Vector2D.One"/>.</summary>
    /// <remarks>This tests that a vector with 1 x and 1 y is returned.</remarks>
    [Test]
    public void One_Get()
    {
        var one = Vector2D.One;
        Assert.AreEqual(new Vector2D(1), one);
    }

    /// <summary>Tests <see cref="Vector2D.UnitX"/>.</summary>
    /// <remarks>This tests that a unit x vector is returned.</remarks>
    [Test]
    public void UnitX_Get()
    {
        var unitX = Vector2D.UnitX;
        Assert.AreEqual(new Vector2D(1, 0), unitX);
    }

    /// <summary>Tests <see cref="Vector2D.UnitY"/>.</summary>
    /// <remarks>This tests that a unit y vector is returned.</remarks>
    [Test]
    public void UnitY_Get()
    {
        var unitY = Vector2D.UnitY;
        Assert.AreEqual(new Vector2D(0, 1), unitY);
    }

    /// <summary>Tests <see cref="Vector2D"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing in-bounds returns the correct element.</remarks>
    [Test]
    public void IndexerGet_IndexBetweenZeroAndOne_ReturnsElement()
    {
        var vector = new Vector2D(1, 2);
        var x = vector[0];
        var y = vector[1];
        Assert.AreEqual(1, x);
        Assert.AreEqual(2, y);
    }

    /// <summary>Tests <see cref="Vector2D"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing less than '0' throws an <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerGet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var vector = new Vector2D(1, 2);
        Action actual = () => _ = vector[-1];
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Vector2D"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that getting an element via indexing more than '1' throws an <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerGet_IndexerMoreThanOne_ThrowsIndexOutOfRangeException()
    {
        var vector = new Vector2D(1, 2);
        Action actual = () => _ = vector[2];
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Vector2D"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing in-bounds sets the correct element.</remarks>
    [Test]
    public void IndexerSet_IndexBetweenZeroAndOne_ReturnsElement()
    {
        var vector = new Vector2D();
        vector[0] = 1;
        vector[1] = 2;
        Assert.AreEqual(new Vector2D(1, 2), vector);
    }

    /// <summary>Tests <see cref="Vector2D"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing less than '0' throws an <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerSet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var vector = new Vector2D();
        Action actual = () => vector[-1] = 0;
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Vector2D"/>[<see langword="int"/>].</summary>
    /// <remarks>This tests that setting an element via indexing more than '1' throws an <see cref="IndexOutOfRangeException"/>.</remarks>
    [Test]
    public void IndexerSet_IndexerMoreThanOne_ThrowsIndexOutOfRangeException()
    {
        var vector = new Vector2D();
        Action actual = () => vector[2] = 0;
        Assert.Throws<IndexOutOfRangeException>(new(actual));
    }

    /// <summary>Tests <see cref="Vector2D(double)"/>.</summary>
    /// <remarks>This tests that the elements get set correctly.</remarks>
    [Test]
    public void ConstructorFloat_SetsElements()
    {
        var vector = new Vector2D(1);
        var x = vector.X;
        var y = vector.Y;
        Assert.AreEqual(1, x);
        Assert.AreEqual(1, y);
    }

    /// <summary>Tests <see cref="Vector2D(double, double)"/>.</summary>
    /// <remarks>This tests that the elements get set correctly.</remarks>
    [Test]
    public void ConstructorFloatFloat_SetsElements()
    {
        var vector = new Vector2D(1, 2);
        var x = vector.X;
        var y = vector.Y;
        Assert.AreEqual(1, x);
        Assert.AreEqual(2, y);
    }

    /// <summary>Tests <see cref="Vector2D.Normalise"/>.</summary>
    /// <remarks>This tests that the vector is unchanged when the length is '0'.</remarks>
    [Test]
    public void Normalise_LengthEqualsZero_VectorIsUnchanged()
    {
        var vector = new Vector2D(0);
        var normalised = vector.Normalised;
        Assert.AreEqual(new Vector2D(0), normalised);
    }

    /// <summary>Tests <see cref="Vector2D.Normalise"/>.</summary>
    /// <remarks>This tests that the vector is normalised when the length isn't '0'.</remarks>
    [Test]
    public void Normalise_LengthIsNotZero_VectorIsNormalised()
    {
        var vector = new Vector2D(1, 2);
        var normalised = vector.Normalised;
        Assert.AreEqual(.4472135, normalised.X, FloatingPointEqualsDelta);
        Assert.AreEqual(.8944271, normalised.Y, FloatingPointEqualsDelta);
    }

    /// <summary>Tests <see cref="Vector2D.ToVector2D"/>.</summary>
    /// <remarks>This tests that the vector is converted correctly.</remarks>
    [Test]
    public void ToVector2_ConvertsElements()
    {
        var vectorD = new Vector2D(.123456789, .567898765);
        var vector = vectorD.ToVector2();
        Assert.AreEqual(.123456789f, vector.X, FloatingPointEqualsDelta);
        Assert.AreEqual(.567898765f, vector.Y, FloatingPointEqualsDelta);
    }

    /// <summary>Tests <see cref="Vector2D.ToFlooredVector2I"/>.</summary>
    /// <remarks>This tests that the vector components get floored when the decimal portion is less than '.5'.</remarks>
    [Test]
    public void ToFlooredVector2I_DecimalLessThanPointFive_RoundsDown()
    {
        var vector = new Vector2D(1.49, 1.49);
        var vectorI = vector.ToFlooredVector2I();
        Assert.AreEqual(new Vector2I(1), vectorI);
    }

    /// <summary>Tests <see cref="Vector2D.ToFlooredVector2I"/>.</summary>
    /// <remarks>This tests that the vector components get floored when the decimal portion is more than '.5'.</remarks>
    [Test]
    public void ToFlooredVector2I_DecimalMoreThanPointFive_RoundsDown()
    {
        var vector = new Vector2D(1.51, 1.51);
        var vectorI = vector.ToFlooredVector2I();
        Assert.AreEqual(new Vector2I(1), vectorI);
    }

    /// <summary>Tests <see cref="Vector2D.ToRoundedVector2I"/>.</summary>
    /// <remarks>This tests that vector components get rounded down when the decimal portion is less than '.5'.</remarks>
    [Test]
    public void ToRoundedVector2I_DecimalLessThanPointFive_RoundsDown()
    {
        var vector = new Vector2D(1.49, 1.49);
        var vectorI = vector.ToRoundedVector2I();
        Assert.AreEqual(new Vector2I(1), vectorI);
    }

    /// <summary>Tests <see cref="Vector2D.ToRoundedVector2I"/>.</summary>
    /// <remarks>This tests that the vector components get rounded up when the decimal portion is '.5'.</remarks>
    [Test]
    public void ToRoundedVector2I_DecimalEqualsPointFive_RoundsUp()
    {
        var vector = new Vector2D(1.5, 1.5);
        var vectorI = vector.ToRoundedVector2I();
        Assert.AreEqual(new Vector2I(2), vectorI);
    }

    /// <summary>Tests <see cref="Vector2D.ToRoundedVector2I"/>.</summary>
    /// <remarks>This tests that the vector components get rounded up when the decimal portion is more than '.5'.</remarks>
    [Test]
    public void ToRoundedVector2I_DecimalMoreThanPointFive_RoundsUp()
    {
        var vector = new Vector2D(1.51, 1.51);
        var vectorI = vector.ToRoundedVector2I();
        Assert.AreEqual(new Vector2I(2), vectorI);
    }

    /// <summary>Tests <see cref="Vector2D.ToCeilingedVector2I"/>.</summary>
    /// <remarks>This tests that the vector components are ceilinged when the decimal portion is less than '.5'.</remarks>
    [Test]
    public void ToCeilingVector2I_DecimalLessThanPointFive_RoundsUp()
    {
        var vector = new Vector2D(1.49, 1.49);
        var vectorI = vector.ToCeilingedVector2I();
        Assert.AreEqual(new Vector2I(2), vectorI);
    }

    /// <summary>Tests <see cref="Vector2D.ToCeilingedVector2I"/>.</summary>
    /// <remarks>This tests that the vector components are ceilinged when the decimal portion is more than '.5'.</remarks>
    [Test]
    public void ToCeilingVector2I_DecimalMoreThanPointFive_RoundsUp()
    {
        var vector = new Vector2D(1.51, 1.51);
        var vectorI = vector.ToCeilingedVector2I();
        Assert.AreEqual(new Vector2I(2), vectorI);
    }

    /// <summary>Tests <see cref="Vector2D.ToFlooredVector2U"/>.</summary>
    /// <remarks>This tests that the vector components get floored when the decimal portion is less than '.5'.</remarks>
    [Test]
    public void ToFlooredVector2U_DecimalLessThanPointFive_RoundsDown()
    {
        var vector = new Vector2D(1.49, 1.49);
        var vectorU = vector.ToFlooredVector2U();
        Assert.AreEqual(new Vector2U(1), vectorU);
    }

    /// <summary>Tests <see cref="Vector2D.ToFlooredVector2U"/>.</summary>
    /// <remarks>This tests that the vector components get floored when the decimal portion is more than '.5'.</remarks>
    [Test]
    public void ToFlooredVector2U_DecimalMoreThanPointFive_RoundsDown()
    {
        var vector = new Vector2D(1.51, 1.51);
        var vectorU = vector.ToFlooredVector2U();
        Assert.AreEqual(new Vector2U(1), vectorU);
    }

    /// <summary>Tests <see cref="Vector2D.ToRoundedVector2U"/>.</summary>
    /// <remarks>This tests that vector components get rounded down when the decimal portion is less than '.5'.</remarks>
    [Test]
    public void ToRoundedVector2U_DecimalLessThanPointFive_RoundsDown()
    {
        var vector = new Vector2D(1.49, 1.49);
        var vectorU = vector.ToRoundedVector2U();
        Assert.AreEqual(new Vector2U(1), vectorU);
    }

    /// <summary>Tests <see cref="Vector2D.ToRoundedVector2U"/>.</summary>
    /// <remarks>This tests that the vector components get rounded up when the decimal portion is '.5'.</remarks>
    [Test]
    public void ToRoundedVector2U_DecimalEqualsPointFive_RoundsUp()
    {
        var vector = new Vector2D(1.5, 1.5);
        var vectorU = vector.ToRoundedVector2U();
        Assert.AreEqual(new Vector2U(2), vectorU);
    }

    /// <summary>Tests <see cref="Vector2D.ToRoundedVector2U"/>.</summary>
    /// <remarks>This tests that the vector components get rounded up when the decimal portion is more than '.5'.</remarks>
    [Test]
    public void ToRoundedVector2U_DecimalMoreThanPointFive_RoundsUp()
    {
        var vector = new Vector2D(1.51, 1.51);
        var vectorU = vector.ToRoundedVector2U();
        Assert.AreEqual(new Vector2U(2), vectorU);
    }

    /// <summary>Tests <see cref="Vector2D.ToCeilingedVector2U"/>.</summary>
    /// <remarks>This tests that the vector components are ceilinged when the decimal portion is less than '.5'.</remarks>
    [Test]
    public void ToCeilingVector2U_DecimalLessThanPointFive_RoundsUp()
    {
        var vector = new Vector2D(1.49, 1.49);
        var vectorU = vector.ToCeilingedVector2U();
        Assert.AreEqual(new Vector2U(2), vectorU);
    }

    /// <summary>Tests <see cref="Vector2D.ToCeilingedVector2U"/>.</summary>
    /// <remarks>This tests that the vector components are ceilinged when the decimal portion is more than '.5'.</remarks>
    [Test]
    public void ToCeilingVector2U_DecimalMoreThanPointFive_RoundsUp()
    {
        var vector = new Vector2D(1.51, 1.51);
        var vectorU = vector.ToCeilingedVector2U();
        Assert.AreEqual(new Vector2U(2), vectorU);
    }

    /// <summary>Tests <see cref="Vector2D.Equals(Vector2D)"/>.</summary>
    /// <remarks>This tests that vectors are considered equal when they are equal.</remarks>
    [Test]
    public void EqualsVector2D_ValuesAreEqual_ReturnsTrue()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D(1, 2);
        var areEqual = vector1.Equals(vector2);
        Assert.IsTrue(areEqual);
    }

    /// <summary>Tests <see cref="Vector2D.Equals(Vector2D)"/>.</summary>
    /// <remarks>This tests that vectors aren't considered equal when they aren't equal.</remarks>
    [Test]
    public void EqualsVector2D_ValuesAreNotEqual_ReturnsFalse()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D();
        var areEqual = vector1.Equals(vector2);
        Assert.IsFalse(areEqual);
    }

    /// <summary>Tests <see cref="Vector2D.Equals(object?)"/>.</summary>
    /// <remarks>This tests that vectors are considered equal when they are equal.</remarks>
    [Test]
    public void EqualsObject_ValuesAreEqual_ReturnsTrue()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D(1, 2);
        var areEqual = vector1.Equals((object)vector2);
        Assert.IsTrue(areEqual);
    }

    /// <summary>Tests <see cref="Vector2D.Equals(object?)"/>.</summary>
    /// <remarks>This tests that vectors aren't considered equal when they aren't equal.</remarks>
    [Test]
    public void EqualsObject_ValuesAreNotEqual_ReturnsFalse()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D();
        var areEqual = vector1.Equals((object)vector2);
        Assert.IsFalse(areEqual);
    }

    /// <summary>Tests <see cref="Vector2D.CompareTo(Vector2D)"/>.</summary>
    /// <remarks>This tests that a vector is considered preceeding a vector with both components bigger.</remarks>
    [Test]
    public void CompareToVector2D_BothThisComponentsAreLessThanOtherComponents_ReturnsLessThanZero()
    {
        var thisVector = new Vector2D(.1f);
        var otherVector = new Vector2D(1);
        var result = thisVector.CompareTo(otherVector);
        Assert.Less(result, 0);
    }

    /// <summary>Tests <see cref="Vector2D.CompareTo(Vector2D)"/>.</summary>
    /// <remarks>This tests that a vector is considered equal to a vector with equal components.</remarks>
    [Test]
    public void CompareToVector2D_BothThisComponentsAreEqualToOtherComponents_ReturnsZero()
    {
        var thisVector = new Vector2D(.1f);
        var otherVector = new Vector2D(.1f);
        var result = thisVector.CompareTo(otherVector);
        Assert.AreEqual(result, 0);
    }

    /// <summary>Tests <see cref="Vector2D.CompareTo(Vector2D)"/>.</summary>
    /// <remarks>This tests that a vector is considered following a vector with both components smaller.</remarks>
    [Test]
    public void CompareToVector2D_BothThisComponentsAreMoreThanOtherComponents_ReturnsMoreThanZero()
    {
        var thisVector = new Vector2D(1);
        var otherVector = new Vector2D(.1f);
        var result = thisVector.CompareTo(otherVector);
        Assert.Greater(result, 0);
    }

    /// <summary>Tests <see cref="Vector2D.CompareTo(Vector2D)"/>.</summary>
    /// <remarks>This tests that a vector is considered preceeding a vector with a bigger X component.</remarks>
    [Test]
    public void CompareToVector2D_ThisXComponentIsLessThanOtherXComponent_ReturnsLessThanZero()
    {
        var thisVector = new Vector2D(.1f, 0);
        var otherVector = new Vector2D(1, 0);
        var result = thisVector.CompareTo(otherVector);
        Assert.Less(result, 0);
    }

    /// <summary>Tests <see cref="Vector2D.CompareTo(Vector2D)"/>.</summary>
    /// <remarks>This tests that a vector is considered preceeding a vector with a bigger Y component.</remarks>
    [Test]
    public void CompareToVector2D_ThisYComponentIsLessThanOtherYComponent_ReturnsLessThanZero()
    {
        var thisVector = new Vector2D(0, .1f);
        var otherVector = new Vector2D(0, 1);
        var result = thisVector.CompareTo(otherVector);
        Assert.Less(result, 0);
    }

    /// <summary>Tests <see cref="Vector2D.CompareTo(Vector2D)"/>.</summary>
    /// <remarks>This tests that a vector is considered following a vector with a smaller X component.</remarks>
    [Test]
    public void CompareToVector2D_ThisXComponentIsMoreThanOtherXComponent_ReturnsMoreThanZero()
    {
        var thisVector = new Vector2D(1, 0);
        var otherVector = new Vector2D(.1f, 0);
        var result = thisVector.CompareTo(otherVector);
        Assert.Greater(result, 0);
    }

    /// <summary>Tests <see cref="Vector2D.CompareTo(Vector2D)"/>.</summary>
    /// <remarks>This tests that a vector is considered following a vector with a smaller Y component.</remarks>
    [Test]
    public void CompareToVector2D_ThisYComponentIsMoreThanOtherYComponent_ReturnsMoreThanZero()
    {
        var thisVector = new Vector2D(0, 1);
        var otherVector = new Vector2D(0, .1f);
        var result = thisVector.CompareTo(otherVector);
        Assert.Greater(result, 0);
    }

    /// <summary>Tests <see cref="Vector2D.Angle(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that giving identical vectors returns an angle of 0 degrees.</remarks>
    [Test]
    public void Angle_VectorsAreIdentical_ReturnsZero()
    {
        var vector1 = new Vector2D(1, 0);
        var vector2 = new Vector2D(1, 0);
        var angle = Vector2D.Angle(vector1, vector2);
        Assert.AreEqual(0, angle);
    }

    /// <summary>Tests <see cref="Vector2D.Angle(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that perpendicular vectors returns an angle of 90 degrees.</remarks>
    [Test]
    public void Angle_VectorsArePerpendicularLeft_Returns90()
    {
        var vector1 = new Vector2D(1, 0);
        var vector2 = new Vector2D(0, 1);
        var angle = Vector2D.Angle(vector1, vector2);
        Assert.AreEqual(90, angle);
    }

    /// <summary>Tests <see cref="Vector2D.Angle(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that perpendicular vectors returns an angle of 90 degrees.</remarks>
    [Test]
    public void Angle_VectorsArePerpendicularRight_Returns90()
    {
        var vector1 = new Vector2D(1, 0);
        var vector2 = new Vector2D(0, -1);
        var angle = Vector2D.Angle(vector1, vector2);
        Assert.AreEqual(90, angle);
    }

    /// <summary>Tests <see cref="Vector2D.Angle(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that giving opposite vectors returns an angle of 180 degrees.</remarks>
    [Test]
    public void Angle_VectorsAreOpposite_Returns180()
    {
        var vector1 = new Vector2D(1, 0);
        var vector2 = new Vector2D(-1, 0);
        var angle = Vector2D.Angle(vector1, vector2);
        Assert.AreEqual(180, angle);
    }

    /// <summary>Tests <see cref="Vector2D.Angle(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that giving unnormalised vectors returns the correct angle degrees.</remarks>
    [Test]
    public void Angle_VectorsAreUnnormalised_ReturnsCorrectAngle()
    {
        var vector1 = new Vector2D(20, 30);
        var vector2 = new Vector2D(-30, 20);
        var angle = Vector2D.Angle(vector1, vector2);
        Assert.AreEqual(90, angle);
    }

    /// <summary>Tests <see cref="Vector2D.Clamp(in Vector2D, in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that giving a value that is between the min and max will result in the passed value.</remarks>
    [Test]
    public void Clamp_ValuesBetweenMinMax_ReturnsValue()
    {
        var value = new Vector2D(1, 2);
        var min = new Vector2D();
        var max = new Vector2D(3);
        var clampedValue = Vector2D.Clamp(value, min, max);
        Assert.AreEqual(new Vector2D(1, 2), clampedValue);
    }

    /// <summary>Tests <see cref="Vector2D.Clamp(in Vector2D, in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that giving a value that is less than the min will result in min.</remarks>
    [Test]
    public void Clamp_ValuesLessThanMin_ReturnsMin()
    {
        var value = new Vector2D(1, 2);
        var min = new Vector2D(4);
        var max = new Vector2D(5);
        var clampedValue = Vector2D.Clamp(value, min, max);
        Assert.AreEqual(new Vector2D(4), clampedValue);
    }

    /// <summary>Tests <see cref="Vector2D.Clamp(in Vector2D, in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that giving a value that is more than the max will result in max.</remarks>
    [Test]
    public void Clamp_ValuesMoreThanMax_ReturnsMax()
    {
        var value = new Vector2D(4, 5);
        var min = new Vector2D(2);
        var max = new Vector2D(3);
        var clampedValue = Vector2D.Clamp(value, min, max);
        Assert.AreEqual(new Vector2D(3), clampedValue);
    }

    /// <summary>Tests <see cref="Vector2D.ComponentMin(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that vector1's X component is used if it's less than vector2's.</remarks>
    [Test]
    public void ComponentMin_Vector1XLessThanVector2X_UsesVector1X()
    {
        var vector1 = new Vector2D(1, 0);
        var vector2 = new Vector2D(2, 0);
        var minVector = Vector2D.ComponentMin(vector1, vector2);
        Assert.AreEqual(new Vector2D(1, 0), minVector);
    }

    /// <summary>Tests <see cref="Vector2D.ComponentMin(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that vector2's X component is used if it's less than vector1's.</remarks>
    [Test]
    public void ComponentMin_Vector1XMoreThanVector2X_UsesVector2X()
    {
        var vector1 = new Vector2D(2, 0);
        var vector2 = new Vector2D(1, 0);
        var minVector = Vector2D.ComponentMin(vector1, vector2);
        Assert.AreEqual(new Vector2D(1, 0), minVector);
    }

    /// <summary>Tests <see cref="Vector2D.ComponentMin(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that vector1's Y component is used if it's less than vector2's.</remarks>
    [Test]
    public void ComponentMin_Vector1YLessThanVector2Y_UsesVector1Y()
    {
        var vector1 = new Vector2D(0, 1);
        var vector2 = new Vector2D(0, 2);
        var minVector = Vector2D.ComponentMin(vector1, vector2);
        Assert.AreEqual(new Vector2D(0, 1), minVector);
    }

    /// <summary>Tests <see cref="Vector2D.ComponentMin(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that vector2's Y component is used if it's less than vector1's.</remarks>
    [Test]
    public void ComponentMin_Vector1YMoreThanVector2Y_UsesVector2Y()
    {
        var vector1 = new Vector2D(0, 2);
        var vector2 = new Vector2D(0, 1);
        var minVector = Vector2D.ComponentMin(vector1, vector2);
        Assert.AreEqual(new Vector2D(0, 1), minVector);
    }

    /// <summary>Tests <see cref="Vector2D.ComponentMax(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that vector2's X component is used if it's more than vector1's.</remarks>
    [Test]
    public void ComponentMax_Vector1XLessThanVector2X_UsesVector2X()
    {
        var vector1 = new Vector2D(1, 0);
        var vector2 = new Vector2D(2, 0);
        var maxVector = Vector2D.ComponentMax(vector1, vector2);
        Assert.AreEqual(new Vector2D(2, 0), maxVector);
    }

    /// <summary>Tests <see cref="Vector2D.ComponentMax(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that vector1's X component is used if it's more than vector2's.</remarks>
    [Test]
    public void ComponentMax_Vector1XMoreThanVector2X_UsesVector1X()
    {
        var vector1 = new Vector2D(2, 0);
        var vector2 = new Vector2D(1, 0);
        var maxVector = Vector2D.ComponentMax(vector1, vector2);
        Assert.AreEqual(new Vector2D(2, 0), maxVector);
    }

    /// <summary>Tests <see cref="Vector2D.ComponentMax(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that vector2's Y component is used if it's more than vector1's.</remarks>
    [Test]
    public void ComponentMax_Vector1YLessThanVector2Y_UsesVector2Y()
    {
        var vector1 = new Vector2D(0, 1);
        var vector2 = new Vector2D(0, 2);
        var maxVector = Vector2D.ComponentMax(vector1, vector2);
        Assert.AreEqual(new Vector2D(0, 2), maxVector);
    }

    /// <summary>Tests <see cref="Vector2D.ComponentMax(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that vector1's Y component is used if it's more than vector2's.</remarks>
    [Test]
    public void ComponentMax_Vector1YMoreThanVector2Y_UsesVector1Y()
    {
        var vector1 = new Vector2D(0, 2);
        var vector2 = new Vector2D(0, 1);
        var maxVector = Vector2D.ComponentMax(vector1, vector2);
        Assert.AreEqual(new Vector2D(0, 2), maxVector);
    }

    /// <summary>Tests <see cref="Vector2D.Distance(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that the distance is calculated correctly.</remarks>
    [Test]
    public void Distance_Get()
    {
        var vector1 = Vector2D.Zero;
        var vector2 = Vector2D.One;
        var distance = Vector2D.Distance(vector1, vector2);
        Assert.AreEqual(1.4142135f, distance, FloatingPointEqualsDelta);
    }

    /// <summary>Tests <see cref="Vector2D.DistanceSquared(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that the squared distance is calculated correctly.</remarks>
    [Test]
    public void DistanceSquared_Get()
    {
        var vector1 = Vector2D.Zero;
        var vector2 = Vector2D.One;
        var distance = Vector2D.DistanceSquared(vector1, vector2);
        Assert.AreEqual(2, distance);
    }

    /// <summary>Tests <see cref="Vector2D.Dot(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that the dot product of two opposite vectors is '-1'.</remarks>
    [Test]
    public void Dot_VectorsOpposite_ReturnsMinusOne()
    {
        var vector1 = new Vector2D(0, 1);
        var vector2 = new Vector2D(0, -1);
        var dot = Vector2D.Dot(vector1, vector2);
        Assert.AreEqual(-1, dot);
    }

    /// <summary>Tests <see cref="Vector2D.Dot(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that the dot product of two perpendicular vectors is '0'.</remarks>
    [Test]
    public void Dot_VectorsPerpendicular_ReturnsZero()
    {
        var vector1 = new Vector2D(0, 1);
        var vector2 = new Vector2D(1, 0);
        var dot = Vector2D.Dot(vector1, vector2);
        Assert.AreEqual(0, dot);
    }

    /// <summary>Tests <see cref="Vector2D.Dot(in Vector2D, in Vector2D)"/>.</summary>
    /// <remarks>This tests that the dot product between two identical vecrtors is '1'.</remarks>
    [Test]
    public void Dot_VectorsSame_ReturnsOne()
    {
        var vector1 = new Vector2D(0, 1);
        var vector2 = new Vector2D(0, 1);
        var dot = Vector2D.Dot(vector1, vector2);
        Assert.AreEqual(1, dot);
    }

    /// <summary>Tests <see cref="Vector2D.Lerp(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving an amount that is in between '0' and '1' will result in a value that's correctly interpolated between the two values.</remarks>
    [Test]
    public void Lerp_AmountBetweenZeroAndOne_ReturnsInterpolatedValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.Lerp(vector1, vector2, .5);
        Assert.AreEqual(new Vector2D(5), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.Lerp(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving an amount that is less than '0' will result in a value that's less than the first value (meaning the method is unclamped).</remarks>
    [Test]
    public void Lerp_AmountLessThanZero_ReturnsLessThanFirstValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.Lerp(vector1, vector2, -1);
        Assert.AreEqual(new Vector2D(-10), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.Lerp(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving an amount that is more than '1' will result in a value that's more than the second value (meaning the method is unclamped).</remarks>
    [Test]
    public void Lerp_AmountMoreThanOne_ReturnsMoreThanSecondValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.Lerp(vector1, vector2, 2);
        Assert.AreEqual(new Vector2D(20), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.Lerp(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving the amount '0' will result in the first value.</remarks>
    [Test]
    public void Lerp_AmountIsZero_ReturnsFirstValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.Lerp(vector1, vector2, 0);
        Assert.AreEqual(new Vector2D(0), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.Lerp(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving the amount '1' will result in the second value.</remarks>
    [Test]
    public void Lerp_AmountIsOne_ReturnsSecondValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.Lerp(vector1, vector2, 1);
        Assert.AreEqual(new Vector2D(10), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.LerpClamped(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving an amount that is in between '0' and '1' will result in a value that's correctly interpolated between the two values.</remarks>
    [Test]
    public void LerpClamped_AmountBetweenZeroAndOne_ReturnsInterpolatedValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.LerpClamped(vector1, vector2, .5);
        Assert.AreEqual(new Vector2D(5), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.LerpClamped(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving an amount that is less than '0' will result in the first value (meaning the method is clamped).</remarks>
    [Test]
    public void LerpClamped_AmountLessThanZero_ReturnsFirstValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.LerpClamped(vector1, vector2, -1);
        Assert.AreEqual(new Vector2D(0), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.LerpClamped(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving an amount that is more than '1' will result in the second value (meaning the method is clamped).</remarks>
    [Test]
    public void LerpClamped_AmountMoreThanOne_ReturnsSecondValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.LerpClamped(vector1, vector2, 2);
        Assert.AreEqual(new Vector2D(10), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.LerpClamped(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving the amount '0' will result in the first value.</remarks>
    [Test]
    public void LerpClamped_AmountIsZero_ReturnsFirstValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.LerpClamped(vector1, vector2, 0);
        Assert.AreEqual(new Vector2D(0), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.LerpClamped(in Vector2D, in Vector2D, double)"/>.</summary>
    /// <remarks>This tests that giving the amount '1' will result in the second value.</remarks>
    [Test]
    public void LerpClamped_AmountIsOne_ReturnsSecondValue()
    {
        var vector1 = new Vector2D(0);
        var vector2 = new Vector2D(10);
        var lerpValue = Vector2D.LerpClamped(vector1, vector2, 1);
        Assert.AreEqual(new Vector2D(10), lerpValue);
    }

    /// <summary>Tests <see cref="Vector2D.Reflect(in Vector2D, Vector2D)"/>.</summary>
    /// <remarks>This tests that the vector correctly gets reflected when the direction comes from the positive half-space.<br/>Example tested: <c>d🡖 🡑n 🡕r</c></remarks>
    [Test]
    public void Reflect_DirectionFromPositiveHalfSpace_ReturnsReflectedDirection()
    {
        var direction = new Vector2D(1, -1);
        var normal = new Vector2D(0, 1);
        var reflected = Vector2D.Reflect(direction, normal);
        Assert.AreEqual(new Vector2D(1, 1), reflected);
    }

    /// <summary>Tests <see cref="Vector2D.Reflect(in Vector2D, Vector2D)"/>.</summary>
    /// <remarks>This tests that the vector correctly get reflected when the direction comes from the negative half-space.<br/>Example tested: <c>d🡖 🡒n 🡗r</c></remarks>
    [Test]
    public void Reflect_DirectionFromNegativeHalfSpace_ReturnsReflectedDirection()
    {
        var direction = new Vector2D(1, -1);
        var normal = new Vector2D(1, 0);
        var reflected = Vector2D.Reflect(direction, normal);
        Assert.AreEqual(new Vector2D(-1, -1), reflected);
    }

    /// <summary>Tests <see cref="Vector2D.Reflect(in Vector2D, Vector2D)"/>.</summary>
    /// <remarks>This tests that the vector doesn't get reflected when the direction and normal are perpendicular (meaning the reflection plane and direction are parallel).<br/>Example tested: <c>d🡓 🡐n 🡓r</c></remarks>
    [Test]
    public void Reflect_DirectionAndNormalArePerpendicular_ReturnsNonReflectedDirection()
    {
        var direction = new Vector2D(0, -1);
        var normal = new Vector2D(-1, 0);
        var reflected = Vector2D.Reflect(direction, normal);
        Assert.AreEqual(new Vector2D(0, -1), reflected);
    }

    /// <summary>Tests <see cref="Vector2D"/> + <see langword="double"/>.</summary>
    /// <remarks>This tests that components are added correctly.</remarks>
    [Test]
    public void OperatorAddVector2DDouble_AddsVectorComponents()
    {
        var vector = new Vector2D(1, 2);
        var result = vector + 1;
        Assert.AreEqual(new Vector2D(2, 3), result);
    }

    /// <summary>Tests <see cref="Vector2D"/> + <see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are added correctly.</remarks>
    [Test]
    public void OperatorAddVector2DVector2D_AddsVectorsComponents()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D(3, 4);
        var result = vector1 + vector2;
        Assert.AreEqual(new Vector2D(4, 6), result);
    }

    /// <summary>Tests <see cref="Vector2D"/> - <see langword="double"/>.</summary>
    /// <remarks>This tests that components are subtracted correctly.</remarks>
    [Test]
    public void OperatorSubtractVector2DDouble_SubtractsVectorComponents()
    {
        var vector = new Vector2D(1, 2);
        var result = vector - 1;
        Assert.AreEqual(new Vector2D(0, 1), result);
    }

    /// <summary>Tests <see cref="Vector2D"/> - <see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are subtracted correctly.</remarks>
    [Test]
    public void OperatorSubtractVector2DVector2D_SubtractsVectorComponents()
    {
        var vector1 = new Vector2D(3, 4);
        var vector2 = new Vector2D(1, 3);
        var result = vector1 - vector2;
        Assert.AreEqual(new Vector2D(2, 1), result);
    }

    /// <summary>Tests -<see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are inverted correctly.</remarks>
    [Test]
    public void OperatorInvert_InvertsVectorComponents()
    {
        var vector = new Vector2D(1, 2);
        var result = -vector;
        Assert.AreEqual(new Vector2D(-1, -2), result);
    }

    /// <summary>Tests <see langword="double"/> * <see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are scaled correctly.</remarks>
    [Test]
    public void OperatorMultiplyDoubleVector2D_MultiplesVectorComponents()
    {
        var vector = new Vector2D(1, 2);
        var result = 2 * vector;
        Assert.AreEqual(new Vector2D(2, 4), result);
    }

    /// <summary>Tests <see cref="Vector2D"/> * <see langword="double"/>.</summary>
    /// <remarks>This tests that vectors are scaled correctly.</remarks>
    [Test]
    public void OperatorMultipleVector2DDouble_MultiplesVectorComponents()
    {
        var vector = new Vector2D(1, 2);
        var result = vector * 2;
        Assert.AreEqual(new Vector2D(2, 4), result);
    }

    /// <summary>Tests <see cref="Vector2D"/> * <see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are scaled correctly.</remarks>
    [Test]
    public void OperatorMultipleVector2DVector2D_MultiplesVectorComponents()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D(3, 4);
        var result = vector1 * vector2;
        Assert.AreEqual(new Vector2D(3, 8), result);
    }

    /// <summary>Tests <see cref="Vector2D"/> / <see langword="double"/>.</summary>
    /// <remarks>This tests that vectors are divided correctly.</remarks>
    [Test]
    public void OperatorDivideVector2DDouble_DividesVectorComponents()
    {
        var vector = new Vector2D(2, 3);
        var result = vector / 2;
        Assert.AreEqual(new Vector2D(1, 1.5), result);
    }

    /// <summary>Tests <see cref="Vector2D"/> / <see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are divided correctly.</remarks>
    [Test]
    public void OperatorDivideVector2DVector2D_DividesVectorComponents()
    {
        var vector1 = new Vector2D(2, 12);
        var vector2 = new Vector2D(2, 4);
        var result = vector1 / vector2;
        Assert.AreEqual(new Vector2D(1, 3), result);
    }

    /// <summary>Tests <see cref="Vector2D"/> == <see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are considered equal when they are equal.</remarks>
    [Test]
    public void OperatorEquals_ValuesAreEqual_ReturnsTrue()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D(1, 2);
        var areEqual = vector1 == vector2;
        Assert.IsTrue(areEqual);
    }

    /// <summary>Tests <see cref="Vector2D"/> == <see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors aren't considered equal when they aren't equal.</remarks>
    [Test]
    public void OperatorEquals_ValuesAreNotEqual_ReturnsFalse()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D();
        var areEqual = vector1 == vector2;
        Assert.IsFalse(areEqual);
    }

    /// <summary>Tests <see cref="Vector2D"/> != <see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are considered equal when they are equal.</remarks>
    [Test]
    public void OperatorNotEquals_ValuesAreEqual_ReturnsFalse()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D(1, 2);
        var areEqual = vector1 != vector2;
        Assert.IsFalse(areEqual);
    }

    /// <summary>Tests <see cref="Vector2D"/> != <see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors aren't considered equal when they aren't equal.</remarks>
    [Test]
    public void OperatorNotEquals_ValuesAreNotEqual_ReturnsTrue()
    {
        var vector1 = new Vector2D(1, 2);
        var vector2 = new Vector2D();
        var areEqual = vector1 != vector2;
        Assert.IsTrue(areEqual);
    }

    /// <summary>Tests (<see cref="Vector2D"/>)(<see langword="double"/>, <see langword="double"/>).</summary>
    /// <remarks>This tests that tuples are cast correctly.</remarks>
    [Test]
    public void OperatorCastTupleDoubleDoubleToVector2D_CastsTuple()
    {
        var tuple = (1.5, 2.5);
        var vector = (Vector2D)tuple;
        Assert.AreEqual(new Vector2D(1.5, 2.5), vector);
    }

    /// <summary>Tests ((<see langword="double"/>, <see langword="double"/>))<see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are cast correctly.</remarks>
    [Test]
    public void OperatorCastVector2ToTupleFloatFloat_CastsVector()
    {
        var vector = new Vector2D(1.5, 2.5);
        var tuple = ((double, double))vector;
        Assert.AreEqual((1.5, 2.5), tuple);
    }

    /// <summary>Tests (<see cref="Vector2D"/>)<see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are cast correctly.</remarks>
    [Test]
    public void OperatorCastVector2DToVector2_CastsVector()
    {
        var vector = new Vector2D(1.5, 2.5);
        var vectorD = (Vector2)vector;
        Assert.AreEqual(new Vector2(1.5f, 2.5f), vectorD);
    }

    /// <summary>Tests (<see cref="Vector2I"/>)<see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are cast correctly.</remarks>
    [Test]
    public void OperatorCastVector2DToVector2I_CastsVector()
    {
        var vector = new Vector2D(1.49, 2.51);
        var vectorI = (Vector2I)vector;
        Assert.AreEqual(new Vector2I(1, 2), vectorI);
    }

    /// <summary>Tests (<see cref="Vector2U"/>)<see cref="Vector2D"/>.</summary>
    /// <remarks>This tests that vectors are cast correctly.</remarks>
    [Test]
    public void OperatorCastVector2DToVector2U_CastsVector()
    {
        var vector = new Vector2D(1.49, 2.51);
        var vectorU = (Vector2U)vector;
        Assert.AreEqual(new Vector2U(1, 2), vectorU);
    }
}

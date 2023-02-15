namespace NovaEngine.Tests.Maths;

/// <summary>The <see cref="Vector2{T}"/> tests.</summary>
internal class Vector2Tests
{
    /*********
    ** Public Methods
    *********/
    [Test]
    public void Length_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(3, 4).Length, Is.EqualTo(5f));
            Assert.That(new Vector2<double>(3, 4).Length, Is.EqualTo(5d));
        });

    [Test]
    public void LengthSquared_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(3, 4).LengthSquared, Is.EqualTo(25f));
            Assert.That(new Vector2<double>(3, 4).LengthSquared, Is.EqualTo(25d));
        });

    [Test]
    public void PerpendicularLeft_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2).PerpendicularLeft, Is.EqualTo(new Vector2<float>(-2, 1)));
            Assert.That(new Vector2<double>(1, 2).PerpendicularLeft, Is.EqualTo(new Vector2<double>(-2, 1)));
        });

    [Test]
    public void PerpendicularRight_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2).PerpendicularRight, Is.EqualTo(new Vector2<float>(2, -1)));
            Assert.That(new Vector2<double>(1, 2).PerpendicularRight, Is.EqualTo(new Vector2<double>(2, -1)));
        });

    [Test]
    public void Normalised_LengthEqualsZero_ReturnsIdenticalVector() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(0).Normalised, Is.EqualTo(new Vector2<float>(0)));
            Assert.That(new Vector2<double>(0).Normalised, Is.EqualTo(new Vector2<double>(0)));
        });

    [Test]
    public void Normalised_LengthIsNotZero_ReturnsNormalisedVector() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2).Normalised, Is.EqualTo(new Vector2<float>(.4472136f, .8944272f)));
            Assert.That(new Vector2<double>(1, 2).Normalised, Is.EqualTo(new Vector2<double>(.4472135954999579, .8944271909999159)));
        });

    [Test]
    public void YX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2).YX, Is.EqualTo(new Vector2<float>(2, 1)));
            Assert.That(new Vector2<double>(1, 2).YX, Is.EqualTo(new Vector2<double>(2, 1)));
        });

    [Test]
    public void YX_Set()
    {
        var vectorFloat = new Vector2<float>(1, 2);
        var vectorDouble = new Vector2<double>(1, 2);

        vectorFloat.YX = new(3, 4);
        vectorDouble.YX = new(3, 4);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector2<float>(4, 3)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector2<double>(4, 3)));
        });
    }

    [Test]
    public void Zero_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Zero, Is.EqualTo(new Vector2<float>(0)));
            Assert.That(Vector2<double>.Zero, Is.EqualTo(new Vector2<double>(0)));
        });

    [Test]
    public void One_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.One, Is.EqualTo(new Vector2<float>(1)));
            Assert.That(Vector2<double>.One, Is.EqualTo(new Vector2<double>(1)));
        });

    [Test]
    public void UnitX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.UnitX, Is.EqualTo(new Vector2<float>(1, 0)));
            Assert.That(Vector2<double>.UnitX, Is.EqualTo(new Vector2<double>(1, 0)));
        });

    [Test]
    public void UnitY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.UnitY, Is.EqualTo(new Vector2<float>(0, 1)));
            Assert.That(Vector2<double>.UnitY, Is.EqualTo(new Vector2<double>(0, 1)));
        });

    [Test]
    public void IndexerGet_IndexBetweenZeroAndOne_ReturnsElement()
    {
        var vectorFloat = new Vector2<float>(1, 2);
        var vectorDouble = new Vector2<double>(1, 2);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat[0], Is.EqualTo(1));
            Assert.That(vectorFloat[1], Is.EqualTo(2));

            Assert.That(vectorDouble[0], Is.EqualTo(1));
            Assert.That(vectorDouble[1], Is.EqualTo(2));
        });
    }

    [Test]
    public void IndexerGet_IndexLessThanZero_ThrowsIndexOutOfRangeException() =>
        Assert.Multiple(() =>
        {
            Assert.That(() => Vector2<float>.One[-1], Throws.InstanceOf<IndexOutOfRangeException>());
            Assert.That(() => Vector2<double>.One[-1], Throws.InstanceOf<IndexOutOfRangeException>());
        });

    [Test]
    public void IndexerGet_IndexerMoreThanOne_ThrowsIndexOutOfRangeException() =>
        Assert.Multiple(() =>
        {
            Assert.That(() => Vector2<float>.One[2], Throws.InstanceOf<IndexOutOfRangeException>());
            Assert.That(() => Vector2<double>.One[2], Throws.InstanceOf<IndexOutOfRangeException>());
        });

    [Test]
    public void IndexerSet_IndexBetweenZeroAndOne_ReturnsElement()
    {
        var vectorFloat = new Vector2<float>();
        var vectorDouble = new Vector2<double>();

        vectorFloat[0] = 1;
        vectorFloat[1] = 2;

        vectorDouble[0] = 1;
        vectorDouble[1] = 2;

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector2<float>(1, 2)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector2<double>(1, 2)));
        });
    }

    [Test]
    public void IndexerSet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var vectorFloat = new Vector2<float>();
        var vectorDouble = new Vector2<double>();

        Assert.Multiple(() =>
        {
            Assert.That(() => vectorFloat[-1] = 0, Throws.InstanceOf<IndexOutOfRangeException>());
            Assert.That(() => vectorDouble[-1] = 0, Throws.InstanceOf<IndexOutOfRangeException>());
        });
    }

    [Test]
    public void IndexerSet_IndexerMoreThanOne_ThrowsIndexOutOfRangeException()
    {
        var vectorFloat = new Vector2<float>();
        var vectorDouble = new Vector2<double>();

        Assert.Multiple(() =>
        {
            Assert.That(() => vectorFloat[2] = 0, Throws.InstanceOf<IndexOutOfRangeException>());
            Assert.That(() => vectorDouble[2] = 0, Throws.InstanceOf<IndexOutOfRangeException>());
        });
    }

    [Test]
    public void ConstructorFloatingPoint_SetsElements()
    {
        var vectorFloat = new Vector2<float>(1);
        var vectorDouble = new Vector2<double>(1);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(1));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(1));
        });
    }

    [Test]
    public void ConstructorFloatingPointFloatingPoint_SetsElements()
    {
        var vectorFloat = new Vector2<float>(1, 2);
        var vectorDouble = new Vector2<double>(1, 2);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(2));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(2));
        });
    }

    [Test]
    public void Normalise_LengthEqualsZero_VectorIsUnchanged()
    {
        var vectorFloat = new Vector2<float>(0);
        var vectorDouble = new Vector2<double>(0);

        vectorFloat.Normalise();
        vectorDouble.Normalise();

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector2<float>(0)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector2<double>(0)));
        });
    }

    [Test]
    public void Normalise_LengthIsNotZero_VectorIsNormalised()
    {
        var vectorFloat = new Vector2<float>(1, 2);
        var vectorDouble = new Vector2<double>(1, 2);

        vectorFloat.Normalise();
        vectorDouble.Normalise();

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector2<float>(.4472136f, .8944272f)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector2<double>(.4472135954999579, .8944271909999159)));
        });
    }

    [Test]
    public void EqualsVector2_ValuesAreEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2).Equals(new Vector2<float>(1, 2)), Is.True);
            Assert.That(new Vector2<double>(1, 2).Equals(new Vector2<double>(1, 2)), Is.True);
        });

    [Test]
    public void EqualsVector2_ValuesAreNotEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2).Equals(new Vector2<float>(2, 1)), Is.False);
            Assert.That(new Vector2<double>(1, 2).Equals(new Vector2<double>(2, 1)), Is.False);
        });

    [Test]
    public void EqualsObject_ValuesAreEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2).Equals((object)new Vector2<float>(1, 2)), Is.True);
            Assert.That(new Vector2<double>(1, 2).Equals((object)new Vector2<double>(1, 2)), Is.True);
        });

    [Test]
    public void EqualsObject_ValuesAreNotEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2).Equals((object)new Vector2<float>(2, 1)), Is.False);
            Assert.That(new Vector2<double>(1, 2).Equals((object)new Vector2<double>(2, 1)), Is.False);
        });

    [Test]
    public void CompareTo_BothFirstComponentsAreLessThanSecondComponents_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(.1f).CompareTo(new Vector2<float>(1)), Is.LessThan(0));
            Assert.That(new Vector2<double>(.1).CompareTo(new Vector2<double>(1)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_BothFirstComponentsAreEqualToSecondComponents_ReturnsZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(.1f).CompareTo(new Vector2<float>(.1f)), Is.Zero);
            Assert.That(new Vector2<double>(.1).CompareTo(new Vector2<double>(.1)), Is.Zero);
        });

    [Test]
    public void CompareTo_BothFirstComponentsAreMoreThanSecondComponents_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1).CompareTo(new Vector2<float>(.1f)), Is.GreaterThan(0));
            Assert.That(new Vector2<double>(1).CompareTo(new Vector2<double>(.1)), Is.GreaterThan(0));
        });

    [Test]
    public void CompareTo_FirstXComponentIsLessThanSecondXComponent_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(.1f, 0).CompareTo(new Vector2<float>(1, 0)), Is.LessThan(0));
            Assert.That(new Vector2<double>(.1, 0).CompareTo(new Vector2<double>(1, 0)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_FirstYComponentIsLessThanSecondYComponent_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(0, .1f).CompareTo(new Vector2<float>(0, 1)), Is.LessThan(0));
            Assert.That(new Vector2<double>(0, .1).CompareTo(new Vector2<double>(0, 1)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_FirstXComponentIsMoreThanSecondXComponent_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 0).CompareTo(new Vector2<float>(.1f, 0)), Is.GreaterThan(0));
            Assert.That(new Vector2<double>(1, 0).CompareTo(new Vector2<double>(.1, 0)), Is.GreaterThan(0));
        });

    [Test]
    public void CompareTo_FirstYComponentIsMoreThanSecondYComponent_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(0, 1).CompareTo(new Vector2<float>(0, .1f)), Is.GreaterThan(0));
            Assert.That(new Vector2<double>(0, 1).CompareTo(new Vector2<double>(0, .1)), Is.GreaterThan(0));
        });

    [Test]
    public void Angle_VectorsAreIdentical_ReturnsZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Angle(new(1, 0), new(1, 0)), Is.EqualTo(0));
            Assert.That(Vector2<double>.Angle(new(1, 0), new(1, 0)), Is.EqualTo(0));
        });

    [Test]
    public void Angle_VectorsArePerpendicularLeft_Returns90() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Angle(new(1, 0), new(0, 1)), Is.EqualTo(90));
            Assert.That(Vector2<double>.Angle(new(1, 0), new(0, 1)), Is.EqualTo(90));
        });

    [Test]
    public void Angle_VectorsArePerpendicularRight_Returns90() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Angle(new(1, 0), new(0, -1)), Is.EqualTo(90));
            Assert.That(Vector2<double>.Angle(new(1, 0), new(0, -1)), Is.EqualTo(90));
        });

    [Test]
    public void Angle_VectorsAreOpposite_Returns180() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Angle(new(1, 0), new(-1, 0)), Is.EqualTo(180));
            Assert.That(Vector2<double>.Angle(new(1, 0), new(-1, 0)), Is.EqualTo(180));
        });

    [Test]
    public void Angle_VectorsAreUnnormalised_ReturnsCorrectAngle() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Angle(new(20, 30), new(-30, 20)), Is.EqualTo(90));
            Assert.That(Vector2<double>.Angle(new(20, 30), new(-30, 20)), Is.EqualTo(90));
        });

    [Test]
    public void Clamp_ValuesBetweenMinMax_ReturnsValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Clamp(new(1, 2), new(0), new(3)), Is.EqualTo(new Vector2<float>(1, 2)));
            Assert.That(Vector2<double>.Clamp(new(1, 2), new(0), new(3)), Is.EqualTo(new Vector2<double>(1, 2)));
        });

    [Test]
    public void Clamp_ValuesLessThanMin_ReturnsMin() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Clamp(new(1, 2), new(3), new(6)), Is.EqualTo(new Vector2<float>(3)));
            Assert.That(Vector2<double>.Clamp(new(1, 2), new(3), new(6)), Is.EqualTo(new Vector2<double>(3)));
        });

    [Test]
    public void Clamp_ValuesMoreThanMax_ReturnsMax() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Clamp(new(4, 5), new(0), new(3)), Is.EqualTo(new Vector2<float>(3)));
            Assert.That(Vector2<double>.Clamp(new(4, 5), new(0), new(3)), Is.EqualTo(new Vector2<double>(3)));
        });

    [Test]
    public void ClampValueXLessThanMin_ReturnsMinXValueY() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Clamp(new(1, 5), new(3), new(6)), Is.EqualTo(new Vector2<float>(3, 5)));
            Assert.That(Vector2<double>.Clamp(new(1, 5), new(3), new(6)), Is.EqualTo(new Vector2<double>(3, 5)));
        });

    [Test]
    public void ClampValueYLessThanMin_ReturnsValueXMinY() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Clamp(new(5, 1), new(3), new(6)), Is.EqualTo(new Vector2<float>(5, 3)));
            Assert.That(Vector2<double>.Clamp(new(5, 1), new(3), new(6)), Is.EqualTo(new Vector2<double>(5, 3)));
        });

    [Test]
    public void ClampValueXMoreThanMax_ReturnsMaxXValueY() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Clamp(new(5, 2), new(0), new(3)), Is.EqualTo(new Vector2<float>(3, 2)));
            Assert.That(Vector2<double>.Clamp(new(5, 2), new(0), new(3)), Is.EqualTo(new Vector2<double>(3, 2)));
        });

    [Test]
    public void ClampValueYMoreThanMax_ReturnsValueXMaxY() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Clamp(new(2, 5), new(0), new(3)), Is.EqualTo(new Vector2<float>(2, 3)));
            Assert.That(Vector2<double>.Clamp(new(2, 5), new(0), new(3)), Is.EqualTo(new Vector2<double>(2, 3)));
        });

    [Test]
    public void ComponentMax_Vector1XLessThanVector2X_UsesVector2X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.ComponentMax(new(1, 0), new(2, 0)), Is.EqualTo(new Vector2<float>(2, 0)));
            Assert.That(Vector2<double>.ComponentMax(new(1, 0), new(2, 0)), Is.EqualTo(new Vector2<double>(2, 0)));
        });

    [Test]
    public void ComponentMax_Vector1XMoreThanVector2X_UsesVector1X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.ComponentMax(new(2, 0), new(1, 0)), Is.EqualTo(new Vector2<float>(2, 0)));
            Assert.That(Vector2<double>.ComponentMax(new(2, 0), new(1, 0)), Is.EqualTo(new Vector2<double>(2, 0)));
        });

    [Test]
    public void ComponentMax_Vector1YLessThanVector2Y_UsesVector2Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.ComponentMax(new(0, 1), new(0, 2)), Is.EqualTo(new Vector2<float>(0, 2)));
            Assert.That(Vector2<double>.ComponentMax(new(0, 1), new(0, 2)), Is.EqualTo(new Vector2<double>(0, 2)));
        });

    [Test]
    public void ComponentMax_Vector1YMoreThanVector2Y_UsesVector1Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.ComponentMax(new(0, 2), new(0, 1)), Is.EqualTo(new Vector2<float>(0, 2)));
            Assert.That(Vector2<double>.ComponentMax(new(0, 2), new(0, 1)), Is.EqualTo(new Vector2<double>(0, 2)));
        });

    [Test]
    public void ComponentMin_Vector1XLessThanVector2X_UsesVector1X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.ComponentMin(new(1, 0), new(2, 0)), Is.EqualTo(new Vector2<float>(1, 0)));
            Assert.That(Vector2<double>.ComponentMin(new(1, 0), new(2, 0)), Is.EqualTo(new Vector2<double>(1, 0)));
        });

    [Test]
    public void ComponentMin_Vector1XMoreThanVector2X_UsesVector2X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.ComponentMin(new(2, 0), new(1, 0)), Is.EqualTo(new Vector2<float>(1, 0)));
            Assert.That(Vector2<double>.ComponentMin(new(2, 0), new(1, 0)), Is.EqualTo(new Vector2<double>(1, 0)));
        });

    [Test]
    public void ComponentMin_Vector1YLessThanVector2Y_UsesVector1Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.ComponentMin(new(0, 1), new(0, 2)), Is.EqualTo(new Vector2<float>(0, 1)));
            Assert.That(Vector2<double>.ComponentMin(new(0, 1), new(0, 2)), Is.EqualTo(new Vector2<double>(0, 1)));
        });

    [Test]
    public void ComponentMin_Vector1YMoreThanVector2Y_UsesVector2Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.ComponentMin(new(0, 2), new(0, 1)), Is.EqualTo(new Vector2<float>(0, 1)));
            Assert.That(Vector2<double>.ComponentMin(new(0, 2), new(0, 1)), Is.EqualTo(new Vector2<double>(0, 1)));
        });

    [Test]
    public void Distance_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Distance(new(0), new(1)), Is.EqualTo(1.4142135f));
            Assert.That(Vector2<double>.Distance(new(0), new(1)), Is.EqualTo(1.4142135623730951));
        });

    [Test]
    public void DistanceSquared_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.DistanceSquared(new(0), new(1)), Is.EqualTo(2));
            Assert.That(Vector2<double>.DistanceSquared(new(0), new(1)), Is.EqualTo(2));
        });

    [Test]
    public void Dot_VectorsOpposite_ReturnsMinusOne() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Dot(new(0, 1), new(0, -1)), Is.EqualTo(-1));
            Assert.That(Vector2<double>.Dot(new(0, 1), new(0, -1)), Is.EqualTo(-1));
        });

    [Test]
    public void Dot_VectorsPerpendicular_ReturnsZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Dot(new(0, 1), new(1, 0)), Is.EqualTo(0));
            Assert.That(Vector2<double>.Dot(new(0, 1), new(1, 0)), Is.EqualTo(0));
        });

    [Test]
    public void Dot_VectorsSame_ReturnsOne() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Dot(new(0, 1), new(0, 1)), Is.EqualTo(1));
            Assert.That(Vector2<double>.Dot(new(0, 1), new(0, 1)), Is.EqualTo(1));
        });

    [Test]
    public void Lerp_AmountBetweenZeroAndOne_ReturnsInterpolatedValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Lerp(new(0), new(10), .5f), Is.EqualTo(new Vector2<float>(5)));
            Assert.That(Vector2<double>.Lerp(new(0), new(10), .5), Is.EqualTo(new Vector2<double>(5)));
        });

    [Test]
    public void Lerp_AmountLessThanZero_ReturnsLessThanFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Lerp(new(0), new(10), -1), Is.EqualTo(new Vector2<float>(-10)));
            Assert.That(Vector2<double>.Lerp(new(0), new(10), -1), Is.EqualTo(new Vector2<double>(-10)));
        });

    [Test]
    public void Lerp_AmountMoreThanOne_ReturnsMoreThanSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Lerp(new(0), new(10), 2), Is.EqualTo(new Vector2<float>(20)));
            Assert.That(Vector2<double>.Lerp(new(0), new(10), 2), Is.EqualTo(new Vector2<double>(20)));
        });

    [Test]
    public void Lerp_AmountIsZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Lerp(new(0), new(10), 0), Is.EqualTo(new Vector2<float>(0)));
            Assert.That(Vector2<double>.Lerp(new(0), new(10), 0), Is.EqualTo(new Vector2<double>(0)));
        });

    [Test]
    public void Lerp_AmountIsOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Lerp(new(0), new(10), 1), Is.EqualTo(new Vector2<float>(10)));
            Assert.That(Vector2<double>.Lerp(new(0), new(10), 1), Is.EqualTo(new Vector2<double>(10)));
        });

    [Test]
    public void LerpClamped_AmountBetweenZeroAndOne_ReturnsInterpolatedValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.LerpClamped(new(0), new(10), .5f), Is.EqualTo(new Vector2<float>(5)));
            Assert.That(Vector2<double>.LerpClamped(new(0), new(10), .5f), Is.EqualTo(new Vector2<double>(5)));
        });

    [Test]
    public void LerpClamped_AmountLessThanZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.LerpClamped(new(0), new(10), -1), Is.EqualTo(new Vector2<float>(0)));
            Assert.That(Vector2<double>.LerpClamped(new(0), new(10), -1), Is.EqualTo(new Vector2<double>(0)));
        });

    [Test]
    public void LerpClamped_AmountMoreThanOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.LerpClamped(new(0), new(10), 2), Is.EqualTo(new Vector2<float>(10)));
            Assert.That(Vector2<double>.LerpClamped(new(0), new(10), 2), Is.EqualTo(new Vector2<double>(10)));
        });

    [Test]
    public void LerpClamped_AmountIsZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.LerpClamped(new(0), new(10), 0), Is.EqualTo(new Vector2<float>(0)));
            Assert.That(Vector2<double>.LerpClamped(new(0), new(10), 0), Is.EqualTo(new Vector2<double>(0)));
        });

    [Test]
    public void LerpClamped_AmountIsOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.LerpClamped(new(0), new(10), 1), Is.EqualTo(new Vector2<float>(10)));
            Assert.That(Vector2<double>.LerpClamped(new(0), new(10), 1), Is.EqualTo(new Vector2<double>(10)));
        });

    [Test]
    public void Reflect_DirectionFromPositiveHalfSpace_ReturnsReflectedDirection() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Reflect(new(1, -1), new(0, 1)), Is.EqualTo(new Vector2<float>(1, 1)));
            Assert.That(Vector2<double>.Reflect(new(1, -1), new(0, 1)), Is.EqualTo(new Vector2<double>(1, 1)));
        });

    [Test]
    public void Reflect_DirectionFromNegativeHalfSpace_ReturnsReflectedDirection() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Reflect(new(1, -1), new(1, 0)), Is.EqualTo(new Vector2<float>(-1, -1)));
            Assert.That(Vector2<double>.Reflect(new(1, -1), new(1, 0)), Is.EqualTo(new Vector2<double>(-1, -1)));
        });

    [Test]
    public void Reflect_DirectionAndNormalArePerpendicular_ReturnsNonReflectedDirection() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector2<float>.Reflect(new(0, -1), new(-1, 0)), Is.EqualTo(new Vector2<float>(0, -1)));
            Assert.That(Vector2<double>.Reflect(new(0, -1), new(-1, 0)), Is.EqualTo(new Vector2<double>(0, -1)));
        });

    [Test]
    public void OperatorAddVector2FloatingPoint_AddsVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2) + 1, Is.EqualTo(new Vector2<float>(2, 3)));
            Assert.That(new Vector2<double>(1, 2) + 1, Is.EqualTo(new Vector2<double>(2, 3)));
        });

    [Test]
    public void OperatorAddVector2Vector2_AddsVectorsComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2) + new Vector2<float>(3, 4), Is.EqualTo(new Vector2<float>(4, 6)));
            Assert.That(new Vector2<double>(1, 2) + new Vector2<double>(3, 4), Is.EqualTo(new Vector2<double>(4, 6)));
        });

    [Test]
    public void OperatorSubtractVector2FloatingPoint_SubtractsVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2) - 1, Is.EqualTo(new Vector2<float>(0, 1)));
            Assert.That(new Vector2<double>(1, 2) - 1, Is.EqualTo(new Vector2<double>(0, 1)));
        });

    [Test]
    public void OperatorSubtractVector2Vector2_SubtractsVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(3, 4) - new Vector2<float>(1, 3), Is.EqualTo(new Vector2<float>(2, 1)));
            Assert.That(new Vector2<double>(3, 4) - new Vector2<double>(1, 3), Is.EqualTo(new Vector2<double>(2, 1)));
        });

    [Test]
    public void OperatorNegate_NegatesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(-new Vector2<float>(1, 2), Is.EqualTo(new Vector2<float>(-1, -2)));
            Assert.That(-new Vector2<double>(1, 2), Is.EqualTo(new Vector2<double>(-1, -2)));
        });

    [Test]
    public void OperatorMultiplyFloatingPointVector2_MultiplesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(2 * new Vector2<float>(1, 2), Is.EqualTo(new Vector2<float>(2, 4)));
            Assert.That(2 * new Vector2<double>(1, 2), Is.EqualTo(new Vector2<double>(2, 4)));
        });

    [Test]
    public void OperatorMultipleVector2FloatingPoint_MultiplesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2) * 2, Is.EqualTo(new Vector2<float>(2, 4)));
            Assert.That(new Vector2<double>(1, 2) * 2, Is.EqualTo(new Vector2<double>(2, 4)));
        });

    [Test]
    public void OperatorMultipleVector2Vector2_MultiplesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2) * new Vector2<float>(3, 4), Is.EqualTo(new Vector2<float>(3, 8)));
            Assert.That(new Vector2<double>(1, 2) * new Vector2<double>(3, 4), Is.EqualTo(new Vector2<double>(3, 8)));
        });

    [Test]
    public void OperatorDivideVector2FloatingPoint_DividesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(2, 3) / 2, Is.EqualTo(new Vector2<float>(1, 1.5f)));
            Assert.That(new Vector2<double>(2, 3) / 2, Is.EqualTo(new Vector2<double>(1, 1.5)));
        });

    [Test]
    public void OperatorDivideVector2Vector2_DividesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(2, 12) / new Vector2<float>(2, 4), Is.EqualTo(new Vector2<float>(1, 3)));
            Assert.That(new Vector2<double>(2, 12) / new Vector2<double>(2, 4), Is.EqualTo(new Vector2<double>(1, 3)));
        });

    [Test]
    public void OperatorEquals_ValuesAreEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2) == new Vector2<float>(1, 2), Is.True);
            Assert.That(new Vector2<double>(1, 2) == new Vector2<double>(1, 2), Is.True);
        });

    [Test]
    public void OperatorEquals_ValuesAreNotEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2) == new Vector2<float>(), Is.False);
            Assert.That(new Vector2<double>(1, 2) == new Vector2<double>(), Is.False);
        });

    [Test]
    public void OperatorNotEquals_ValuesAreEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2) != new Vector2<float>(1, 2), Is.False);
            Assert.That(new Vector2<double>(1, 2) != new Vector2<double>(1, 2), Is.False);
        });

    [Test]
    public void OperatorNotEquals_ValuesAreNotEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector2<float>(1, 2) != new Vector2<float>(), Is.True);
            Assert.That(new Vector2<double>(1, 2) != new Vector2<double>(), Is.True);
        });
}

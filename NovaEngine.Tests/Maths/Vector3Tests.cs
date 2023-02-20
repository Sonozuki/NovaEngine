namespace NovaEngine.Tests.Maths;

/// <summary>The <see cref="Vector3{T}"/> tests.</summary>
public class Vector3Tests
{
    /*********
    ** Public Methods
    *********/
    [Test]
    public void Length_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(3, 4, 5).Length, Is.EqualTo(7.07106781f));
            Assert.That(new Vector3<double>(3, 4, 5).Length, Is.EqualTo(7.0710678118654755d));
        });

    [Test]
    public void LengthSquared_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(3, 4, 5).LengthSquared, Is.EqualTo(50f));
            Assert.That(new Vector3<double>(3, 4, 5).LengthSquared, Is.EqualTo(50d));
        });

    [Test]
    public void Normalised_LengthEqualsZero_ReturnsIdenticalVector() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(0).Normalised, Is.EqualTo(new Vector3<float>(0)));
            Assert.That(new Vector3<double>(0).Normalised, Is.EqualTo(new Vector3<double>(0)));
        });

    [Test]
    public void Normalised_LengthIsNotZero_ReturnsNormalisedVector() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).Normalised, Is.EqualTo(new Vector3<float>(.26726124f, .5345225f, .8017837f)));
            Assert.That(new Vector3<double>(1, 2, 3).Normalised, Is.EqualTo(new Vector3<double>(.2672612419124244, .5345224838248488, .8017837257372732)));
        });

    [Test]
    public void XZY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).XZY, Is.EqualTo(new Vector3<float>(1, 3, 2)));
            Assert.That(new Vector3<double>(1, 2, 3).XZY, Is.EqualTo(new Vector3<double>(1, 3, 2)));
        });

    [Test]
    public void XZY_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.XZY = new(4, 5, 6);
        vectorDouble.XZY = new(4, 5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(4, 6, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(4, 6, 5)));
        });
    }
    
    [Test]
    public void YXZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).YXZ, Is.EqualTo(new Vector3<float>(2, 1, 3)));
            Assert.That(new Vector3<double>(1, 2, 3).YXZ, Is.EqualTo(new Vector3<double>(2, 1, 3)));
        });

    [Test]
    public void YXZ_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.YXZ = new(4, 5, 6);
        vectorDouble.YXZ = new(4, 5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(5, 4, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(5, 4, 6)));
        });
    }
    
    [Test]
    public void YZX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).YZX, Is.EqualTo(new Vector3<float>(2, 3, 1)));
            Assert.That(new Vector3<double>(1, 2, 3).YZX, Is.EqualTo(new Vector3<double>(2, 3, 1)));
        });

    [Test]
    public void YZX_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.YZX = new(4, 5, 6);
        vectorDouble.YZX = new(4, 5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(6, 4, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(6, 4, 5)));
        });
    }
    
    [Test]
    public void ZXY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).ZXY, Is.EqualTo(new Vector3<float>(3, 1, 2)));
            Assert.That(new Vector3<double>(1, 2, 3).ZXY, Is.EqualTo(new Vector3<double>(3, 1, 2)));
        });

    [Test]
    public void ZXY_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.ZXY = new(4, 5, 6);
        vectorDouble.ZXY = new(4, 5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(5, 6, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(5, 6, 4)));
        });
    }
    
    [Test]
    public void ZYX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).ZYX, Is.EqualTo(new Vector3<float>(3, 2, 1)));
            Assert.That(new Vector3<double>(1, 2, 3).ZYX, Is.EqualTo(new Vector3<double>(3, 2, 1)));
        });

    [Test]
    public void ZYX_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.ZYX = new(4, 5, 6);
        vectorDouble.ZYX = new(4, 5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(6, 5, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(6, 5, 4)));
        });
    }

    [Test]
    public void XY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).XY, Is.EqualTo(new Vector2<float>(1, 2)));
            Assert.That(new Vector3<double>(1, 2, 3).XY, Is.EqualTo(new Vector2<double>(1, 2)));
        });

    [Test]
    public void XY_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.XY = new(4, 5);
        vectorDouble.XY = new(4, 5);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(4, 5, 3)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(4, 5, 3)));
        });
    }

    [Test]
    public void YX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).YX, Is.EqualTo(new Vector2<float>(2, 1)));
            Assert.That(new Vector3<double>(1, 2, 3).YX, Is.EqualTo(new Vector2<double>(2, 1)));
        });

    [Test]
    public void YX_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.YX = new(4, 5);
        vectorDouble.YX = new(4, 5);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(5, 4, 3)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(5, 4, 3)));
        });
    }
    
    [Test]
    public void XZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).XZ, Is.EqualTo(new Vector2<float>(1, 3)));
            Assert.That(new Vector3<double>(1, 2, 3).XZ, Is.EqualTo(new Vector2<double>(1, 3)));
        });

    [Test]
    public void XZ_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.XZ = new(4, 5);
        vectorDouble.XZ = new(4, 5);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(4, 2, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(4, 2, 5)));
        });
    }
    
    [Test]
    public void ZX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).ZX, Is.EqualTo(new Vector2<float>(3, 1)));
            Assert.That(new Vector3<double>(1, 2, 3).ZX, Is.EqualTo(new Vector2<double>(3, 1)));
        });

    [Test]
    public void ZX_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.ZX = new(4, 5);
        vectorDouble.ZX = new(4, 5);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(5, 2, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(5, 2, 4)));
        });
    }
    
    [Test]
    public void YZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).YZ, Is.EqualTo(new Vector2<float>(2, 3)));
            Assert.That(new Vector3<double>(1, 2, 3).YZ, Is.EqualTo(new Vector2<double>(2, 3)));
        });

    [Test]
    public void YZ_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.YZ = new(4, 5);
        vectorDouble.YZ = new(4, 5);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(1, 4, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(1, 4, 5)));
        });
    }
    
    [Test]
    public void ZY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).ZY, Is.EqualTo(new Vector2<float>(3, 2)));
            Assert.That(new Vector3<double>(1, 2, 3).ZY, Is.EqualTo(new Vector2<double>(3, 2)));
        });

    [Test]
    public void ZY_Set()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.ZY = new(4, 5);
        vectorDouble.ZY = new(4, 5);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(1, 5, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(1, 5, 4)));
        });
    }
    
    [Test]
    public void Zero_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Zero, Is.EqualTo(new Vector3<float>(0)));
            Assert.That(Vector3<double>.Zero, Is.EqualTo(new Vector3<double>(0)));
        });

    [Test]
    public void One_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.One, Is.EqualTo(new Vector3<float>(1)));
            Assert.That(Vector3<double>.One, Is.EqualTo(new Vector3<double>(1)));
        });

    [Test]
    public void UnitX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.UnitX, Is.EqualTo(new Vector3<float>(1, 0, 0)));
            Assert.That(Vector3<double>.UnitX, Is.EqualTo(new Vector3<double>(1, 0, 0)));
        });

    [Test]
    public void UnitY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.UnitY, Is.EqualTo(new Vector3<float>(0, 1, 0)));
            Assert.That(Vector3<double>.UnitY, Is.EqualTo(new Vector3<double>(0, 1, 0)));
        });
    
    [Test]
    public void UnitZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.UnitZ, Is.EqualTo(new Vector3<float>(0, 0, 1)));
            Assert.That(Vector3<double>.UnitZ, Is.EqualTo(new Vector3<double>(0, 0, 1)));
        });

    [Test]
    public void IndexerGet_IndexBetweenZeroAndTwo_ReturnsElement()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat[0], Is.EqualTo(1));
            Assert.That(vectorFloat[1], Is.EqualTo(2));
            Assert.That(vectorFloat[2], Is.EqualTo(3));

            Assert.That(vectorDouble[0], Is.EqualTo(1));
            Assert.That(vectorDouble[1], Is.EqualTo(2));
            Assert.That(vectorDouble[2], Is.EqualTo(3));
        });
    }

    [Test]
    public void IndexerGet_IndexLessThanZero_ThrowsArgumentOutOfRangeException() =>
        Assert.Multiple(() =>
        {
            Assert.That(() => Vector3<float>.One[-1], Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Vector3<double>.One[-1], Throws.InstanceOf<ArgumentOutOfRangeException>());
        });

    [Test]
    public void IndexerGet_IndexerMoreThanTwo_ThrowsArgumentOutOfRangeException() =>
        Assert.Multiple(() =>
        {
            Assert.That(() => Vector3<float>.One[3], Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Vector3<double>.One[3], Throws.InstanceOf<ArgumentOutOfRangeException>());
        });

    [Test]
    public void IndexerSet_IndexBetweenZeroAndTwo_ReturnsElement()
    {
        var vectorFloat = new Vector3<float>();
        var vectorDouble = new Vector3<double>();

        vectorFloat[0] = 1;
        vectorFloat[1] = 2;
        vectorFloat[2] = 3;

        vectorDouble[0] = 1;
        vectorDouble[1] = 2;
        vectorDouble[2] = 3;

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(1, 2, 3)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(1, 2, 3)));
        });
    }

    [Test]
    public void IndexerSet_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
    {
        var vectorFloat = new Vector3<float>();
        var vectorDouble = new Vector3<double>();

        Assert.Multiple(() =>
        {
            Assert.That(() => vectorFloat[-1] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => vectorDouble[-1] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
        });
    }

    [Test]
    public void IndexerSet_IndexerMoreThanTwo_ThrowsArgumentOutOfRangeException()
    {
        var vectorFloat = new Vector3<float>();
        var vectorDouble = new Vector3<double>();

        Assert.Multiple(() =>
        {
            Assert.That(() => vectorFloat[3] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => vectorDouble[3] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
        });
    }

    [Test]
    public void ConstructorFloatingPoint_SetsElements()
    {
        var vectorFloat = new Vector3<float>(1);
        var vectorDouble = new Vector3<double>(1);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(1));
            Assert.That(vectorFloat.Z, Is.EqualTo(1));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(1));
            Assert.That(vectorDouble.Z, Is.EqualTo(1));
        });
    }

    [Test]
    public void ConstructorFloatingPointFloatingPointFloatingPoint_SetsElements()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(2));
            Assert.That(vectorFloat.Z, Is.EqualTo(3));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(2));
            Assert.That(vectorDouble.Z, Is.EqualTo(3));
        });
    }
    
    [Test]
    public void ConstructorVector2FloatingPoint_SetsElements()
    {
        var vectorFloat = new Vector3<float>(new(1, 2), 3);
        var vectorDouble = new Vector3<double>(new(1, 2), 3);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(2));
            Assert.That(vectorFloat.Z, Is.EqualTo(3));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(2));
            Assert.That(vectorDouble.Z, Is.EqualTo(3));
        });
    }

    [Test]
    public void Normalise_LengthEqualsZero_VectorIsUnchanged()
    {
        var vectorFloat = new Vector3<float>(0);
        var vectorDouble = new Vector3<double>(0);

        vectorFloat.Normalise();
        vectorDouble.Normalise();

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(0)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(0)));
        });
    }

    [Test]
    public void Normalise_LengthIsNotZero_VectorIsNormalised()
    {
        var vectorFloat = new Vector3<float>(1, 2, 3);
        var vectorDouble = new Vector3<double>(1, 2, 3);

        vectorFloat.Normalise();
        vectorDouble.Normalise();

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector3<float>(.26726124f, .5345225f, .8017837f)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector3<double>(.2672612419124244, .5345224838248488, .8017837257372732)));
        });
    }

    [Test]
    public void EqualsVector3_ValuesAreEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).Equals(new Vector3<float>(1, 2, 3)), Is.True);
            Assert.That(new Vector3<double>(1, 2, 3).Equals(new Vector3<double>(1, 2, 3)), Is.True);
        });

    [Test]
    public void EqualsVector3_ValuesAreNotEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).Equals(new Vector3<float>(2, 1, 3)), Is.False);
            Assert.That(new Vector3<double>(1, 2, 3).Equals(new Vector3<double>(2, 1, 3)), Is.False);
        });

    [Test]
    public void EqualsObject_ValuesAreEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).Equals((object)new Vector3<float>(1, 2, 3)), Is.True);
            Assert.That(new Vector3<double>(1, 2, 3).Equals((object)new Vector3<double>(1, 2, 3)), Is.True);
        });

    [Test]
    public void EqualsObject_ValuesAreNotEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3).Equals((object)new Vector3<float>(2, 1, 3)), Is.False);
            Assert.That(new Vector3<double>(1, 2, 3).Equals((object)new Vector3<double>(2, 1, 3)), Is.False);
        });

    [Test]
    public void CompareTo_AllFirstComponentsAreLessThanSecondComponents_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(.1f).CompareTo(new Vector3<float>(1)), Is.LessThan(0));
            Assert.That(new Vector3<double>(.1).CompareTo(new Vector3<double>(1)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_AllFirstComponentsAreEqualToSecondComponents_ReturnsZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(.1f).CompareTo(new Vector3<float>(.1f)), Is.Zero);
            Assert.That(new Vector3<double>(.1).CompareTo(new Vector3<double>(.1)), Is.Zero);
        });

    [Test]
    public void CompareTo_AllFirstComponentsAreMoreThanSecondComponents_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1).CompareTo(new Vector3<float>(.1f)), Is.GreaterThan(0));
            Assert.That(new Vector3<double>(1).CompareTo(new Vector3<double>(.1)), Is.GreaterThan(0));
        });

    [Test]
    public void CompareTo_FirstXComponentIsLessThanSecondXComponent_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(.1f, 0, 0).CompareTo(new Vector3<float>(1, 0, 0)), Is.LessThan(0));
            Assert.That(new Vector3<double>(.1, 0, 0).CompareTo(new Vector3<double>(1, 0, 0)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_FirstYComponentIsLessThanSecondYComponent_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(0, .1f, 0).CompareTo(new Vector3<float>(0, 1, 0)), Is.LessThan(0));
            Assert.That(new Vector3<double>(0, .1, 0).CompareTo(new Vector3<double>(0, 1, 0)), Is.LessThan(0));
        });
    
    [Test]
    public void CompareTo_FirstZComponentIsLessThanSecondZComponent_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(0, 0, .1f).CompareTo(new Vector3<float>(0, 0, 1)), Is.LessThan(0));
            Assert.That(new Vector3<double>(0, 0, .1).CompareTo(new Vector3<double>(0, 0, 1)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_FirstXComponentIsMoreThanSecondXComponent_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 0, 0).CompareTo(new Vector3<float>(.1f, 0, 0)), Is.GreaterThan(0));
            Assert.That(new Vector3<double>(1, 0, 0).CompareTo(new Vector3<double>(.1, 0, 0)), Is.GreaterThan(0));
        });

    [Test]
    public void CompareTo_FirstYComponentIsMoreThanSecondYComponent_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(0, 1, 0).CompareTo(new Vector3<float>(0, .1f, 0)), Is.GreaterThan(0));
            Assert.That(new Vector3<double>(0, 1, 0).CompareTo(new Vector3<double>(0, .1, 0)), Is.GreaterThan(0));
        });
    
    [Test]
    public void CompareTo_FirstZComponentIsMoreThanSecondZComponent_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(0, 0, 1).CompareTo(new Vector3<float>(0, 0, .1f)), Is.GreaterThan(0));
            Assert.That(new Vector3<double>(0, 0, 1).CompareTo(new Vector3<double>(0, 0, .1)), Is.GreaterThan(0));
        });

    [Test]
    public void Clamp_ValuesBetweenMinMax_ReturnsValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Clamp(new(1, 2, 3), new(0), new(3)), Is.EqualTo(new Vector3<float>(1, 2, 3)));
            Assert.That(Vector3<double>.Clamp(new(1, 2, 3), new(0), new(3)), Is.EqualTo(new Vector3<double>(1, 2, 3)));
        });

    [Test]
    public void Clamp_ValuesLessThanMin_ReturnsMin() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Clamp(new(1, 2, 2.5f), new(3), new(6)), Is.EqualTo(new Vector3<float>(3)));
            Assert.That(Vector3<double>.Clamp(new(1, 2, 2.5), new(3), new(6)), Is.EqualTo(new Vector3<double>(3)));
        });

    [Test]
    public void Clamp_ValuesMoreThanMax_ReturnsMax() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Clamp(new(4, 5, 6), new(0), new(3)), Is.EqualTo(new Vector3<float>(3)));
            Assert.That(Vector3<double>.Clamp(new(4, 5, 6), new(0), new(3)), Is.EqualTo(new Vector3<double>(3)));
        });

    [Test]
    public void ClampValueXLessThanMin_ReturnsMinXValueYValueZ() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Clamp(new(1, 5, 5), new(3), new(6)), Is.EqualTo(new Vector3<float>(3, 5, 5)));
            Assert.That(Vector3<double>.Clamp(new(1, 5, 5), new(3), new(6)), Is.EqualTo(new Vector3<double>(3, 5, 5)));
        });

    [Test]
    public void ClampValueYLessThanMin_ReturnsValueXMinYValueZ() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Clamp(new(5, 1, 5), new(3), new(6)), Is.EqualTo(new Vector3<float>(5, 3, 5)));
            Assert.That(Vector3<double>.Clamp(new(5, 1, 5), new(3), new(6)), Is.EqualTo(new Vector3<double>(5, 3, 5)));
        });
    
    [Test]
    public void ClampValueZLessThanMin_ReturnsValueXValueYMinZ() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Clamp(new(5, 5, 1), new(3), new(6)), Is.EqualTo(new Vector3<float>(5, 5, 3)));
            Assert.That(Vector3<double>.Clamp(new(5, 5, 1), new(3), new(6)), Is.EqualTo(new Vector3<double>(5, 5, 3)));
        });

    [Test]
    public void ClampValueXMoreThanMax_ReturnsMaxXValueYValueZ() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Clamp(new(5, 2, 2), new(0), new(3)), Is.EqualTo(new Vector3<float>(3, 2, 2)));
            Assert.That(Vector3<double>.Clamp(new(5, 2, 2), new(0), new(3)), Is.EqualTo(new Vector3<double>(3, 2, 2)));
        });

    [Test]
    public void ClampValueYMoreThanMax_ReturnsValueXMaxYValueZ() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Clamp(new(2, 5, 2), new(0), new(3)), Is.EqualTo(new Vector3<float>(2, 3, 2)));
            Assert.That(Vector3<double>.Clamp(new(2, 5, 2), new(0), new(3)), Is.EqualTo(new Vector3<double>(2, 3, 2)));
        });
    
    [Test]
    public void ClampValueZMoreThanMax_ReturnsValueXValueYMaxZ() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Clamp(new(2, 2, 5), new(0), new(3)), Is.EqualTo(new Vector3<float>(2, 2, 3)));
            Assert.That(Vector3<double>.Clamp(new(2, 2, 5), new(0), new(3)), Is.EqualTo(new Vector3<double>(2, 2, 3)));
        });

    [Test]
    public void ComponentMax_Vector1XLessThanVector2X_UsesVector2X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMax(new(1, 0, 0), new(2, 0, 0)), Is.EqualTo(new Vector3<float>(2, 0, 0)));
            Assert.That(Vector3<double>.ComponentMax(new(1, 0, 0), new(2, 0, 0)), Is.EqualTo(new Vector3<double>(2, 0, 0)));
        });

    [Test]
    public void ComponentMax_Vector1XMoreThanVector2X_UsesVector1X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMax(new(2, 0, 0), new(1, 0, 0)), Is.EqualTo(new Vector3<float>(2, 0, 0)));
            Assert.That(Vector3<double>.ComponentMax(new(2, 0, 0), new(1, 0, 0)), Is.EqualTo(new Vector3<double>(2, 0, 0)));
        });

    [Test]
    public void ComponentMax_Vector1YLessThanVector2Y_UsesVector2Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMax(new(0, 1, 0), new(0, 2, 0)), Is.EqualTo(new Vector3<float>(0, 2, 0)));
            Assert.That(Vector3<double>.ComponentMax(new(0, 1, 0), new(0, 2, 0)), Is.EqualTo(new Vector3<double>(0, 2, 0)));
        });

    [Test]
    public void ComponentMax_Vector1YMoreThanVector2Y_UsesVector1Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMax(new(0, 2, 0), new(0, 1, 0)), Is.EqualTo(new Vector3<float>(0, 2, 0)));
            Assert.That(Vector3<double>.ComponentMax(new(0, 2, 0), new(0, 1, 0)), Is.EqualTo(new Vector3<double>(0, 2, 0)));
        });
    
    [Test]
    public void ComponentMax_Vector1ZLessThanVector2Z_UsesVector2Z() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMax(new(0, 0, 1), new(0, 0, 2)), Is.EqualTo(new Vector3<float>(0, 0, 2)));
            Assert.That(Vector3<double>.ComponentMax(new(0, 0, 1), new(0, 0, 2)), Is.EqualTo(new Vector3<double>(0, 0, 2)));
        });

    [Test]
    public void ComponentMax_Vector1ZMoreThanVector2Z_UsesVector1Z() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMax(new(0, 0, 2), new(0, 0, 1)), Is.EqualTo(new Vector3<float>(0, 0, 2)));
            Assert.That(Vector3<double>.ComponentMax(new(0, 0, 2), new(0, 0, 1)), Is.EqualTo(new Vector3<double>(0, 0, 2)));
        });

    [Test]
    public void ComponentMin_Vector1XLessThanVector2X_UsesVector1X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMin(new(1, 0, 0), new(2, 0, 0)), Is.EqualTo(new Vector3<float>(1, 0, 0)));
            Assert.That(Vector3<double>.ComponentMin(new(1, 0, 0), new(2, 0, 0)), Is.EqualTo(new Vector3<double>(1, 0, 0)));
        });

    [Test]
    public void ComponentMin_Vector1XMoreThanVector2X_UsesVector2X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMin(new(2, 0, 0), new(1, 0, 0)), Is.EqualTo(new Vector3<float>(1, 0, 0)));
            Assert.That(Vector3<double>.ComponentMin(new(2, 0, 0), new(1, 0, 0)), Is.EqualTo(new Vector3<double>(1, 0, 0)));
        });

    [Test]
    public void ComponentMin_Vector1YLessThanVector2Y_UsesVector1Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMin(new(0, 1, 0), new(0, 2, 0)), Is.EqualTo(new Vector3<float>(0, 1, 0)));
            Assert.That(Vector3<double>.ComponentMin(new(0, 1, 0), new(0, 2, 0)), Is.EqualTo(new Vector3<double>(0, 1, 0)));
        });

    [Test]
    public void ComponentMin_Vector1YMoreThanVector2Y_UsesVector2Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMin(new(0, 2, 0), new(0, 1, 0)), Is.EqualTo(new Vector3<float>(0, 1, 0)));
            Assert.That(Vector3<double>.ComponentMin(new(0, 2, 0), new(0, 1, 0)), Is.EqualTo(new Vector3<double>(0, 1, 0)));
        });
    
    [Test]
    public void ComponentMin_Vector1ZLessThanVector2Z_UsesVector1Z() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMin(new(0, 0, 1), new(0, 0, 2)), Is.EqualTo(new Vector3<float>(0, 0, 1)));
            Assert.That(Vector3<double>.ComponentMin(new(0, 0, 1), new(0, 0, 2)), Is.EqualTo(new Vector3<double>(0, 0, 1)));
        });

    [Test]
    public void ComponentMin_Vector1ZMoreThanVector2Z_UsesVector2Z() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.ComponentMin(new(0, 0, 2), new(0, 0, 1)), Is.EqualTo(new Vector3<float>(0, 0, 1)));
            Assert.That(Vector3<double>.ComponentMin(new(0, 0, 2), new(0, 0, 1)), Is.EqualTo(new Vector3<double>(0, 0, 1)));
        });

    [Test]
    public void Distance_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Distance(new(0), new(1)), Is.EqualTo(1.73205078f));
            Assert.That(Vector3<double>.Distance(new(0), new(1)), Is.EqualTo(1.7320508075688772d));
        });

    [Test]
    public void DistanceSquared_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.DistanceSquared(new(0), new(1)), Is.EqualTo(3));
            Assert.That(Vector3<double>.DistanceSquared(new(0), new(1)), Is.EqualTo(3));
        });

    [Test]
    public void Dot_VectorsOpposite_ReturnsMinusOne() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Dot(new(0, 0, 1), new(0, 0, -1)), Is.EqualTo(-1));
            Assert.That(Vector3<double>.Dot(new(0, 0, 1), new(0, 0, -1)), Is.EqualTo(-1));
        });

    [Test]
    public void Dot_VectorsPerpendicular_ReturnsZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Dot(new(0, 0, 1), new(0, 1, 0)), Is.EqualTo(0));
            Assert.That(Vector3<double>.Dot(new(0, 0, 1), new(0, 1, 0)), Is.EqualTo(0));
        });

    [Test]
    public void Dot_VectorsSame_ReturnsOne() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Dot(new(0, 0, 1), new(0, 0, 1)), Is.EqualTo(1));
            Assert.That(Vector3<double>.Dot(new(0, 0, 1), new(0, 0, 1)), Is.EqualTo(1));
        });

    [Test]
    public void Lerp_AmountBetweenZeroAndOne_ReturnsInterpolatedValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Lerp(new(0), new(10), .5f), Is.EqualTo(new Vector3<float>(5)));
            Assert.That(Vector3<double>.Lerp(new(0), new(10), .5), Is.EqualTo(new Vector3<double>(5)));
        });

    [Test]
    public void Lerp_AmountLessThanZero_ReturnsLessThanFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Lerp(new(0), new(10), -1), Is.EqualTo(new Vector3<float>(-10)));
            Assert.That(Vector3<double>.Lerp(new(0), new(10), -1), Is.EqualTo(new Vector3<double>(-10)));
        });

    [Test]
    public void Lerp_AmountMoreThanOne_ReturnsMoreThanSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Lerp(new(0), new(10), 2), Is.EqualTo(new Vector3<float>(20)));
            Assert.That(Vector3<double>.Lerp(new(0), new(10), 2), Is.EqualTo(new Vector3<double>(20)));
        });

    [Test]
    public void Lerp_AmountIsZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Lerp(new(0), new(10), 0), Is.EqualTo(new Vector3<float>(0)));
            Assert.That(Vector3<double>.Lerp(new(0), new(10), 0), Is.EqualTo(new Vector3<double>(0)));
        });

    [Test]
    public void Lerp_AmountIsOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Lerp(new(0), new(10), 1), Is.EqualTo(new Vector3<float>(10)));
            Assert.That(Vector3<double>.Lerp(new(0), new(10), 1), Is.EqualTo(new Vector3<double>(10)));
        });

    [Test]
    public void LerpClamped_AmountBetweenZeroAndOne_ReturnsInterpolatedValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.LerpClamped(new(0), new(10), .5f), Is.EqualTo(new Vector3<float>(5)));
            Assert.That(Vector3<double>.LerpClamped(new(0), new(10), .5f), Is.EqualTo(new Vector3<double>(5)));
        });

    [Test]
    public void LerpClamped_AmountLessThanZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.LerpClamped(new(0), new(10), -1), Is.EqualTo(new Vector3<float>(0)));
            Assert.That(Vector3<double>.LerpClamped(new(0), new(10), -1), Is.EqualTo(new Vector3<double>(0)));
        });

    [Test]
    public void LerpClamped_AmountMoreThanOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.LerpClamped(new(0), new(10), 2), Is.EqualTo(new Vector3<float>(10)));
            Assert.That(Vector3<double>.LerpClamped(new(0), new(10), 2), Is.EqualTo(new Vector3<double>(10)));
        });

    [Test]
    public void LerpClamped_AmountIsZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.LerpClamped(new(0), new(10), 0), Is.EqualTo(new Vector3<float>(0)));
            Assert.That(Vector3<double>.LerpClamped(new(0), new(10), 0), Is.EqualTo(new Vector3<double>(0)));
        });

    [Test]
    public void LerpClamped_AmountIsOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.LerpClamped(new(0), new(10), 1), Is.EqualTo(new Vector3<float>(10)));
            Assert.That(Vector3<double>.LerpClamped(new(0), new(10), 1), Is.EqualTo(new Vector3<double>(10)));
        });

    [Test]
    public void Reflect_DirectionFromPositiveHalfSpace_ReturnsReflectedDirection() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Reflect(new(1, -1, 0), new(0, 1, 0)), Is.EqualTo(new Vector3<float>(1, 1, 0)));
            Assert.That(Vector3<double>.Reflect(new(1, -1, 0), new(0, 1, 0)), Is.EqualTo(new Vector3<double>(1, 1, 0)));
        });

    [Test]
    public void Reflect_DirectionFromNegativeHalfSpace_ReturnsReflectedDirection() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Reflect(new(1, -1, 0), new(1, 0, 0)), Is.EqualTo(new Vector3<float>(-1, -1, 0)));
            Assert.That(Vector3<double>.Reflect(new(1, -1, 0), new(1, 0, 0)), Is.EqualTo(new Vector3<double>(-1, -1, 0)));
        });

    [Test]
    public void Reflect_DirectionAndNormalArePerpendicular_ReturnsNonReflectedDirection() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector3<float>.Reflect(new(0, -1, 0), new(-1, 0, 0)), Is.EqualTo(new Vector3<float>(0, -1, 0)));
            Assert.That(Vector3<double>.Reflect(new(0, -1, 0), new(-1, 0, 0)), Is.EqualTo(new Vector3<double>(0, -1, 0)));
        });

    [Test]
    public void OperatorAddVector3FloatingPoint_AddsVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3) + 1, Is.EqualTo(new Vector3<float>(2, 3, 4)));
            Assert.That(new Vector3<double>(1, 2, 3) + 1, Is.EqualTo(new Vector3<double>(2, 3, 4)));
        });

    [Test]
    public void OperatorAddVector3Vector3_AddsVectorsComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3) + new Vector3<float>(3, 4, 5), Is.EqualTo(new Vector3<float>(4, 6, 8)));
            Assert.That(new Vector3<double>(1, 2, 3) + new Vector3<double>(3, 4, 5), Is.EqualTo(new Vector3<double>(4, 6, 8)));
        });

    [Test]
    public void OperatorSubtractVector3FloatingPoint_SubtractsVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3) - 1, Is.EqualTo(new Vector3<float>(0, 1, 2)));
            Assert.That(new Vector3<double>(1, 2, 3) - 1, Is.EqualTo(new Vector3<double>(0, 1, 2)));
        });

    [Test]
    public void OperatorSubtractVector3Vector3_SubtractsVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(3, 4, 5) - new Vector3<float>(1, 3, 4), Is.EqualTo(new Vector3<float>(2, 1, 1)));
            Assert.That(new Vector3<double>(3, 4, 5) - new Vector3<double>(1, 3, 4), Is.EqualTo(new Vector3<double>(2, 1, 1)));
        });

    [Test]
    public void OperatorNegate_NegatesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(-new Vector3<float>(1, 2, 3), Is.EqualTo(new Vector3<float>(-1, -2, -3)));
            Assert.That(-new Vector3<double>(1, 2, 3), Is.EqualTo(new Vector3<double>(-1, -2, -3)));
        });

    [Test]
    public void OperatorMultiplyFloatingPointVector3_MultiplesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(2 * new Vector3<float>(1, 2, 3), Is.EqualTo(new Vector3<float>(2, 4, 6)));
            Assert.That(2 * new Vector3<double>(1, 2, 3), Is.EqualTo(new Vector3<double>(2, 4, 6)));
        });

    [Test]
    public void OperatorMultipleVector3FloatingPoint_MultiplesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3) * 2, Is.EqualTo(new Vector3<float>(2, 4, 6)));
            Assert.That(new Vector3<double>(1, 2, 3) * 2, Is.EqualTo(new Vector3<double>(2, 4, 6)));
        });

    [Test]
    public void OperatorMultipleVector3Vector3_MultiplesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3) * new Vector3<float>(3, 4, 5), Is.EqualTo(new Vector3<float>(3, 8, 15)));
            Assert.That(new Vector3<double>(1, 2, 3) * new Vector3<double>(3, 4, 5), Is.EqualTo(new Vector3<double>(3, 8, 15)));
        });

    [Test]
    public void OperatorDivideVector3FloatingPoint_DividesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(2, 3, 6) / 2, Is.EqualTo(new Vector3<float>(1, 1.5f, 3)));
            Assert.That(new Vector3<double>(2, 3, 6) / 2, Is.EqualTo(new Vector3<double>(1, 1.5, 3)));
        });

    [Test]
    public void OperatorDivideVector3Vector3_DividesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(2, 12, 15) / new Vector3<float>(2, 4, 3), Is.EqualTo(new Vector3<float>(1, 3, 5)));
            Assert.That(new Vector3<double>(2, 12, 15) / new Vector3<double>(2, 4, 3), Is.EqualTo(new Vector3<double>(1, 3, 5)));
        });

    [Test]
    public void OperatorLessThan_LeftIsLessThanRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1) < new Vector3<float>(2), Is.True);
            Assert.That(new Vector3<double>(1) < new Vector3<double>(2), Is.True);
        });

    [Test]
    public void OperatorLessThan_LeftIsEqualToRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1) < new Vector3<float>(1), Is.False);
            Assert.That(new Vector3<double>(1) < new Vector3<double>(1), Is.False);
        });

    [Test]
    public void OperatorLessThan_LeftIsGreaterThanRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(2) < new Vector3<float>(1), Is.False);
            Assert.That(new Vector3<double>(2) < new Vector3<double>(1), Is.False);
        });

    [Test]
    public void OperatorLessThanEquals_LeftIsLessThanRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1) <= new Vector3<float>(2), Is.True);
            Assert.That(new Vector3<double>(1) <= new Vector3<double>(2), Is.True);
        });

    [Test]
    public void OperatorLessThanEquals_LeftIsEqualToRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1) <= new Vector3<float>(1), Is.True);
            Assert.That(new Vector3<double>(1) <= new Vector3<double>(1), Is.True);
        });

    [Test]
    public void OperatorLessThanEquals_LeftIsGreaterThanRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(2) <= new Vector3<float>(1), Is.False);
            Assert.That(new Vector3<double>(2) <= new Vector3<double>(1), Is.False);
        });

    [Test]
    public void OperatorGreaterThan_LeftIsLessThanRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1) > new Vector3<float>(2), Is.False);
            Assert.That(new Vector3<double>(1) > new Vector3<double>(2), Is.False);
        });

    [Test]
    public void OperatorGreaterThan_LeftIsEqualToRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1) > new Vector3<float>(1), Is.False);
            Assert.That(new Vector3<double>(1) > new Vector3<double>(1), Is.False);
        });

    [Test]
    public void OperatorGreaterThan_LeftIsGreaterThanRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(2) > new Vector3<float>(1), Is.True);
            Assert.That(new Vector3<double>(2) > new Vector3<double>(1), Is.True);
        });

    [Test]
    public void OperatorGreaterThanEquals_LeftIsLessThanRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1) >= new Vector3<float>(2), Is.False);
            Assert.That(new Vector3<double>(1) >= new Vector3<double>(2), Is.False);
        });

    [Test]
    public void OperatorGreaterThanEquals_LeftIsEqualToRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1) >= new Vector3<float>(1), Is.True);
            Assert.That(new Vector3<double>(1) >= new Vector3<double>(1), Is.True);
        });

    [Test]
    public void OperatorGreaterThanEquals_LeftIsGreaterThanRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(2) >= new Vector3<float>(1), Is.True);
            Assert.That(new Vector3<double>(2) >= new Vector3<double>(1), Is.True);
        });

    [Test]
    public void OperatorEquals_ValuesAreEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3) == new Vector3<float>(1, 2, 3), Is.True);
            Assert.That(new Vector3<double>(1, 2, 3) == new Vector3<double>(1, 2, 3), Is.True);
        });

    [Test]
    public void OperatorEquals_ValuesAreNotEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3) == new Vector3<float>(), Is.False);
            Assert.That(new Vector3<double>(1, 2, 3) == new Vector3<double>(), Is.False);
        });

    [Test]
    public void OperatorNotEquals_ValuesAreEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3) != new Vector3<float>(1, 2, 3), Is.False);
            Assert.That(new Vector3<double>(1, 2, 3) != new Vector3<double>(1, 2, 3), Is.False);
        });

    [Test]
    public void OperatorNotEquals_ValuesAreNotEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector3<float>(1, 2, 3) != new Vector3<float>(), Is.True);
            Assert.That(new Vector3<double>(1, 2, 3) != new Vector3<double>(), Is.True);
        });
}

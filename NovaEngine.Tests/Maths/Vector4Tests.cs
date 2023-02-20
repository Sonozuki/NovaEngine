namespace NovaEngine.Tests.Maths;

/// <summary>The <see cref="Vector4{T}"/> tests.</summary>
public class Vector4Tests
{
    /*********
    ** Public Methods
    *********/
    [Test]
    public void Length_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(3, 4, 5, 6).Length, Is.EqualTo(9.2736187f));
            Assert.That(new Vector4<double>(3, 4, 5, 6).Length, Is.EqualTo(9.2736184954957039d));
        });

    [Test]
    public void LengthSquared_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(3, 4, 5, 6).LengthSquared, Is.EqualTo(86f));
            Assert.That(new Vector4<double>(3, 4, 5, 6).LengthSquared, Is.EqualTo(86d));
        });

    [Test]
    public void Normalised_LengthEqualsZero_ReturnsIdenticalVector() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(0).Normalised, Is.EqualTo(new Vector4<float>(0)));
            Assert.That(new Vector4<double>(0).Normalised, Is.EqualTo(new Vector4<double>(0)));
        });

    [Test]
    public void Normalised_LengthIsNotZero_ReturnsNormalisedVector() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).Normalised, Is.EqualTo(new Vector4<float>(.18257418f, .36514837f, .5477226f, .73029673f)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).Normalised, Is.EqualTo(new Vector4<double>(.18257418583505536, .3651483716701107, .5477225575051661, .7302967433402214)));
        });

    [Test]
    public void XYWZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XYWZ, Is.EqualTo(new Vector4<float>(1, 2, 4, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XYWZ, Is.EqualTo(new Vector4<double>(1, 2, 4, 3)));
        });

    [Test]
    public void XYWZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XYWZ = new(5, 6, 7, 8);
        vectorDouble.XYWZ = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 6, 8, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 6, 8, 7)));
        });
    }
    
    [Test]
    public void XZYW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XZYW, Is.EqualTo(new Vector4<float>(1, 3, 2, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XZYW, Is.EqualTo(new Vector4<double>(1, 3, 2, 4)));
        });

    [Test]
    public void XZYW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XZYW = new(5, 6, 7, 8);
        vectorDouble.XZYW = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 7, 6, 8)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 7, 6, 8)));
        });
    }
    
    [Test]
    public void XZWY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XZWY, Is.EqualTo(new Vector4<float>(1, 3, 4, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XZWY, Is.EqualTo(new Vector4<double>(1, 3, 4, 2)));
        });

    [Test]
    public void XZWY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XZWY = new(5, 6, 7, 8);
        vectorDouble.XZWY = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 8, 6, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 8, 6, 7)));
        });
    }
    
    [Test]
    public void XWYZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XWYZ, Is.EqualTo(new Vector4<float>(1, 4, 2, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XWYZ, Is.EqualTo(new Vector4<double>(1, 4, 2, 3)));
        });

    [Test]
    public void XWYZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XWYZ = new(5, 6, 7, 8);
        vectorDouble.XWYZ = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 7, 8, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 7, 8, 6)));
        });
    }
    
    [Test]
    public void XWZY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XWZY, Is.EqualTo(new Vector4<float>(1, 4, 3, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XWZY, Is.EqualTo(new Vector4<double>(1, 4, 3, 2)));
        });

    [Test]
    public void XWZY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XWZY = new(5, 6, 7, 8);
        vectorDouble.XWZY = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 8, 7, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 8, 7, 6)));
        });
    }
    
    [Test]
    public void YXZW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YXZW, Is.EqualTo(new Vector4<float>(2, 1, 3, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YXZW, Is.EqualTo(new Vector4<double>(2, 1, 3, 4)));
        });

    [Test]
    public void YXZW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YXZW = new(5, 6, 7, 8);
        vectorDouble.YXZW = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 5, 7, 8)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 5, 7, 8)));
        });
    }
    
    [Test]
    public void YXWZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YXWZ, Is.EqualTo(new Vector4<float>(2, 1, 4, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YXWZ, Is.EqualTo(new Vector4<double>(2, 1, 4, 3)));
        });

    [Test]
    public void YXWZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YXWZ = new(5, 6, 7, 8);
        vectorDouble.YXWZ = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 5, 8, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 5, 8, 7)));
        });
    }
    
    [Test]
    public void YZXW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YZXW, Is.EqualTo(new Vector4<float>(2, 3, 1, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YZXW, Is.EqualTo(new Vector4<double>(2, 3, 1, 4)));
        });

    [Test]
    public void YZXW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YZXW = new(5, 6, 7, 8);
        vectorDouble.YZXW = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 5, 6, 8)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 5, 6, 8)));
        });
    }
    
    [Test]
    public void YZWX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YZWX, Is.EqualTo(new Vector4<float>(2, 3, 4, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YZWX, Is.EqualTo(new Vector4<double>(2, 3, 4, 1)));
        });

    [Test]
    public void YZWX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YZWX = new(5, 6, 7, 8);
        vectorDouble.YZWX = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(8, 5, 6, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(8, 5, 6, 7)));
        });
    }
    
    [Test]
    public void YWXZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YWXZ, Is.EqualTo(new Vector4<float>(2, 4, 1, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YWXZ, Is.EqualTo(new Vector4<double>(2, 4, 1, 3)));
        });

    [Test]
    public void YWXZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YWXZ = new(5, 6, 7, 8);
        vectorDouble.YWXZ = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 5, 8, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 5, 8, 6)));
        });
    }
    
    [Test]
    public void YWZX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YWZX, Is.EqualTo(new Vector4<float>(2, 4, 3, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YWZX, Is.EqualTo(new Vector4<double>(2, 4, 3, 1)));
        });

    [Test]
    public void YWZX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YWZX = new(5, 6, 7, 8);
        vectorDouble.YWZX = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(8, 5, 7, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(8, 5, 7, 6)));
        });
    }
    
    [Test]
    public void ZXYW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZXYW, Is.EqualTo(new Vector4<float>(3, 1, 2, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZXYW, Is.EqualTo(new Vector4<double>(3, 1, 2, 4)));
        });

    [Test]
    public void ZXYW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZXYW = new(5, 6, 7, 8);
        vectorDouble.ZXYW = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 7, 5, 8)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 7, 5, 8)));
        });
    }
    
    [Test]
    public void ZXWY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZXWY, Is.EqualTo(new Vector4<float>(3, 1, 4, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZXWY, Is.EqualTo(new Vector4<double>(3, 1, 4, 2)));
        });

    [Test]
    public void ZXWY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZXWY = new(5, 6, 7, 8);
        vectorDouble.ZXWY = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 8, 5, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 8, 5, 7)));
        });
    }

    [Test]
    public void ZYXW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZYXW, Is.EqualTo(new Vector4<float>(3, 2, 1, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZYXW, Is.EqualTo(new Vector4<double>(3, 2, 1, 4)));
        });

    [Test]
    public void ZYXW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZYXW = new(5, 6, 7, 8);
        vectorDouble.ZYXW = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 6, 5, 8)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 6, 5, 8)));
        });
    }
    
    [Test]
    public void ZYWX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZYWX, Is.EqualTo(new Vector4<float>(3, 2, 4, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZYWX, Is.EqualTo(new Vector4<double>(3, 2, 4, 1)));
        });

    [Test]
    public void ZYWX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZYWX = new(5, 6, 7, 8);
        vectorDouble.ZYWX = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(8, 6, 5, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(8, 6, 5, 7)));
        });
    }
    
    [Test]
    public void ZWXY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZWXY, Is.EqualTo(new Vector4<float>(3, 4, 1, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZWXY, Is.EqualTo(new Vector4<double>(3, 4, 1, 2)));
        });

    [Test]
    public void ZWXY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZWXY = new(5, 6, 7, 8);
        vectorDouble.ZWXY = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 8, 5, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 8, 5, 6)));
        });
    }
    
    [Test]
    public void ZWYX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZWYX, Is.EqualTo(new Vector4<float>(3, 4, 2, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZWYX, Is.EqualTo(new Vector4<double>(3, 4, 2, 1)));
        });

    [Test]
    public void ZWYX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZWYX = new(5, 6, 7, 8);
        vectorDouble.ZWYX = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(8, 7, 5, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(8, 7, 5, 6)));
        });
    }
    
    [Test]
    public void WXYZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WXYZ, Is.EqualTo(new Vector4<float>(4, 1, 2, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WXYZ, Is.EqualTo(new Vector4<double>(4, 1, 2, 3)));
        });

    [Test]
    public void WXYZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WXYZ = new(5, 6, 7, 8);
        vectorDouble.WXYZ = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 7, 8, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 7, 8, 5)));
        });
    }
    
    [Test]
    public void WXZY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WXZY, Is.EqualTo(new Vector4<float>(4, 1, 3, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WXZY, Is.EqualTo(new Vector4<double>(4, 1, 3, 2)));
        });

    [Test]
    public void WXZY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WXZY = new(5, 6, 7, 8);
        vectorDouble.WXZY = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 8, 7, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 8, 7, 5)));
        });
    }
    
    [Test]
    public void WYXZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WYXZ, Is.EqualTo(new Vector4<float>(4, 2, 1, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WYXZ, Is.EqualTo(new Vector4<double>(4, 2, 1, 3)));
        });

    [Test]
    public void WYXZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WYXZ = new(5, 6, 7, 8);
        vectorDouble.WYXZ = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 6, 8, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 6, 8, 5)));
        });
    }
    
    [Test]
    public void WYZX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WYZX, Is.EqualTo(new Vector4<float>(4, 2, 3, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WYZX, Is.EqualTo(new Vector4<double>(4, 2, 3, 1)));
        });

    [Test]
    public void WYZX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WYZX = new(5, 6, 7, 8);
        vectorDouble.WYZX = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(8, 6, 7, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(8, 6, 7, 5)));
        });
    }
    
    [Test]
    public void WZXY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WZXY, Is.EqualTo(new Vector4<float>(4, 3, 1, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WZXY, Is.EqualTo(new Vector4<double>(4, 3, 1, 2)));
        });

    [Test]
    public void WZXY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WZXY = new(5, 6, 7, 8);
        vectorDouble.WZXY = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 8, 6, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 8, 6, 5)));
        });
    }
    
    [Test]
    public void WZYX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WZYX, Is.EqualTo(new Vector4<float>(4, 3, 2, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WZYX, Is.EqualTo(new Vector4<double>(4, 3, 2, 1)));
        });

    [Test]
    public void WZYX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WZYX = new(5, 6, 7, 8);
        vectorDouble.WZYX = new(5, 6, 7, 8);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(8, 7, 6, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(8, 7, 6, 5)));
        });
    }
    
    [Test]
    public void XYZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XYZ, Is.EqualTo(new Vector3<float>(1, 2, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XYZ, Is.EqualTo(new Vector3<double>(1, 2, 3)));
        });

    [Test]
    public void XYZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XYZ = new(5, 6, 7);
        vectorDouble.XYZ = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 6, 7, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 6, 7, 4)));
        });
    }
    
    [Test]
    public void XYW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XYW, Is.EqualTo(new Vector3<float>(1, 2, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XYW, Is.EqualTo(new Vector3<double>(1, 2, 4)));
        });

    [Test]
    public void XYW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XYW = new(5, 6, 7);
        vectorDouble.XYW = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 6, 3, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 6, 3, 7)));
        });
    }
    
    [Test]
    public void XZY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XZY, Is.EqualTo(new Vector3<float>(1, 3, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XZY, Is.EqualTo(new Vector3<double>(1, 3, 2)));
        });

    [Test]
    public void XZY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XZY = new(5, 6, 7);
        vectorDouble.XZY = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 7, 6, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 7, 6, 4)));
        });
    }
    
    [Test]
    public void XZW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XZW, Is.EqualTo(new Vector3<float>(1, 3, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XZW, Is.EqualTo(new Vector3<double>(1, 3, 4)));
        });

    [Test]
    public void XZW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XZW = new(5, 6, 7);
        vectorDouble.XZW = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 2, 6, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 2, 6, 7)));
        });
    }
    
    [Test]
    public void XWY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XWY, Is.EqualTo(new Vector3<float>(1, 4, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XWY, Is.EqualTo(new Vector3<double>(1, 4, 2)));
        });

    [Test]
    public void XWY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XWY = new(5, 6, 7);
        vectorDouble.XWY = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 7, 3, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 7, 3, 6)));
        });
    }
    
    [Test]
    public void XWZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XWZ, Is.EqualTo(new Vector3<float>(1, 4, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XWZ, Is.EqualTo(new Vector3<double>(1, 4, 3)));
        });

    [Test]
    public void XWZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XWZ = new(5, 6, 7);
        vectorDouble.XWZ = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 2, 7, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 2, 7, 6)));
        });
    }
    
    [Test]
    public void YXZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YXZ, Is.EqualTo(new Vector3<float>(2, 1, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YXZ, Is.EqualTo(new Vector3<double>(2, 1, 3)));
        });

    [Test]
    public void YXZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YXZ = new(5, 6, 7);
        vectorDouble.YXZ = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 5, 7, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 5, 7, 4)));
        });
    }
    
    [Test]
    public void YXW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YXW, Is.EqualTo(new Vector3<float>(2, 1, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YXW, Is.EqualTo(new Vector3<double>(2, 1, 4)));
        });

    [Test]
    public void YXW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YXW = new(5, 6, 7);
        vectorDouble.YXW = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 5, 3, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 5, 3, 7)));
        });
    }
    
    [Test]
    public void YZX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YZX, Is.EqualTo(new Vector3<float>(2, 3, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YZX, Is.EqualTo(new Vector3<double>(2, 3, 1)));
        });

    [Test]
    public void YZX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YZX = new(5, 6, 7);
        vectorDouble.YZX = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 5, 6, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 5, 6, 4)));
        });
    }
    
    [Test]
    public void YZW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YZW, Is.EqualTo(new Vector3<float>(2, 3, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YZW, Is.EqualTo(new Vector3<double>(2, 3, 4)));
        });

    [Test]
    public void YZW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YZW = new(5, 6, 7);
        vectorDouble.YZW = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 5, 6, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 5, 6, 7)));
        });
    }
    
    [Test]
    public void YWX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YWX, Is.EqualTo(new Vector3<float>(2, 4, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YWX, Is.EqualTo(new Vector3<double>(2, 4, 1)));
        });

    [Test]
    public void YWX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YWX = new(5, 6, 7);
        vectorDouble.YWX = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 5, 3, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 5, 3, 6)));
        });
    }
    
    [Test]
    public void YWZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YWZ, Is.EqualTo(new Vector3<float>(2, 4, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YWZ, Is.EqualTo(new Vector3<double>(2, 4, 3)));
        });

    [Test]
    public void YWZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YWZ = new(5, 6, 7);
        vectorDouble.YWZ = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 5, 7, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 5, 7, 6)));
        });
    }
    
    [Test]
    public void ZXY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZXY, Is.EqualTo(new Vector3<float>(3, 1, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZXY, Is.EqualTo(new Vector3<double>(3, 1, 2)));
        });

    [Test]
    public void ZXY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZXY = new(5, 6, 7);
        vectorDouble.ZXY = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 7, 5, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 7, 5, 4)));
        });
    }
    
    [Test]
    public void ZXW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZXW, Is.EqualTo(new Vector3<float>(3, 1, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZXW, Is.EqualTo(new Vector3<double>(3, 1, 4)));
        });

    [Test]
    public void ZXW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZXW = new(5, 6, 7);
        vectorDouble.ZXW = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 2, 5, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 2, 5, 7)));
        });
    }
    
    [Test]
    public void ZYX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZYX, Is.EqualTo(new Vector3<float>(3, 2, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZYX, Is.EqualTo(new Vector3<double>(3, 2, 1)));
        });

    [Test]
    public void ZYX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZYX = new(5, 6, 7);
        vectorDouble.ZYX = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 6, 5, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 6, 5, 4)));
        });
    }
    
    [Test]
    public void ZYW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZYW, Is.EqualTo(new Vector3<float>(3, 2, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZYW, Is.EqualTo(new Vector3<double>(3, 2, 4)));
        });

    [Test]
    public void ZYW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZYW = new(5, 6, 7);
        vectorDouble.ZYW = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 6, 5, 7)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 6, 5, 7)));
        });
    }
    
    [Test]
    public void WXY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WXY, Is.EqualTo(new Vector3<float>(4, 1, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WXY, Is.EqualTo(new Vector3<double>(4, 1, 2)));
        });

    [Test]
    public void WXY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WXY = new(5, 6, 7);
        vectorDouble.WXY = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 7, 3, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 7, 3, 5)));
        });
    }
    
    [Test]
    public void WXZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WXZ, Is.EqualTo(new Vector3<float>(4, 1, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WXZ, Is.EqualTo(new Vector3<double>(4, 1, 3)));
        });

    [Test]
    public void WXZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WXZ = new(5, 6, 7);
        vectorDouble.WXZ = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 2, 7, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 2, 7, 5)));
        });
    }
    
    [Test]
    public void WYX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WYX, Is.EqualTo(new Vector3<float>(4, 2, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WYX, Is.EqualTo(new Vector3<double>(4, 2, 1)));
        });

    [Test]
    public void WYX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WYX = new(5, 6, 7);
        vectorDouble.WYX = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 6, 3, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 6, 3, 5)));
        });
    }
    
    [Test]
    public void WYZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WYZ, Is.EqualTo(new Vector3<float>(4, 2, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WYZ, Is.EqualTo(new Vector3<double>(4, 2, 3)));
        });

    [Test]
    public void WYZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WYZ = new(5, 6, 7);
        vectorDouble.WYZ = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 6, 7, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 6, 7, 5)));
        });
    }
    
    [Test]
    public void WZX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WZX, Is.EqualTo(new Vector3<float>(4, 3, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WZX, Is.EqualTo(new Vector3<double>(4, 3, 1)));
        });

    [Test]
    public void WZX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WZX = new(5, 6, 7);
        vectorDouble.WZX = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(7, 2, 6, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(7, 2, 6, 5)));
        });
    }
    
    [Test]
    public void WZY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WZY, Is.EqualTo(new Vector3<float>(4, 3, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WZY, Is.EqualTo(new Vector3<double>(4, 3, 2)));
        });

    [Test]
    public void WZY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WZY = new(5, 6, 7);
        vectorDouble.WZY = new(5, 6, 7);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 7, 6, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 7, 6, 5)));
        });
    }
    
    [Test]
    public void XY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XY, Is.EqualTo(new Vector2<float>(1, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XY, Is.EqualTo(new Vector2<double>(1, 2)));
        });

    [Test]
    public void XY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XY = new(5, 6);
        vectorDouble.XY = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 6, 3, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 6, 3, 4)));
        });
    }
    
    [Test]
    public void XZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XZ, Is.EqualTo(new Vector2<float>(1, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XZ, Is.EqualTo(new Vector2<double>(1, 3)));
        });

    [Test]
    public void XZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XZ = new(5, 6);
        vectorDouble.XZ = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 2, 6, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 2, 6, 4)));
        });
    }
    
    [Test]
    public void XW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).XW, Is.EqualTo(new Vector2<float>(1, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).XW, Is.EqualTo(new Vector2<double>(1, 4)));
        });

    [Test]
    public void XW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.XW = new(5, 6);
        vectorDouble.XW = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(5, 2, 3, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(5, 2, 3, 6)));
        });
    }
    
    [Test]
    public void YX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YX, Is.EqualTo(new Vector2<float>(2, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YX, Is.EqualTo(new Vector2<double>(2, 1)));
        });

    [Test]
    public void YX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YX = new(5, 6);
        vectorDouble.YX = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 5, 3, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 5, 3, 4)));
        });
    }
    
    [Test]
    public void YZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YZ, Is.EqualTo(new Vector2<float>(2, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YZ, Is.EqualTo(new Vector2<double>(2, 3)));
        });

    [Test]
    public void YZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YZ = new(5, 6);
        vectorDouble.YZ = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 5, 6, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 5, 6, 4)));
        });
    }
    
    [Test]
    public void YW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).YW, Is.EqualTo(new Vector2<float>(2, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).YW, Is.EqualTo(new Vector2<double>(2, 4)));
        });

    [Test]
    public void YW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.YW = new(5, 6);
        vectorDouble.YW = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 5, 3, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 5, 3, 6)));
        });
    }
    
    [Test]
    public void ZX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZX, Is.EqualTo(new Vector2<float>(3, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZX, Is.EqualTo(new Vector2<double>(3, 1)));
        });

    [Test]
    public void ZX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZX = new(5, 6);
        vectorDouble.ZX = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 2, 5, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 2, 5, 4)));
        });
    }
    
    [Test]
    public void ZY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZY, Is.EqualTo(new Vector2<float>(3, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZY, Is.EqualTo(new Vector2<double>(3, 2)));
        });

    [Test]
    public void ZY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZY = new(5, 6);
        vectorDouble.ZY = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 6, 5, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 6, 5, 4)));
        });
    }
    
    [Test]
    public void ZW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).ZW, Is.EqualTo(new Vector2<float>(3, 4)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).ZW, Is.EqualTo(new Vector2<double>(3, 4)));
        });

    [Test]
    public void ZW_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.ZW = new(5, 6);
        vectorDouble.ZW = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 2, 5, 6)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 2, 5, 6)));
        });
    }
    
    [Test]
    public void WX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WX, Is.EqualTo(new Vector2<float>(4, 1)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WX, Is.EqualTo(new Vector2<double>(4, 1)));
        });

    [Test]
    public void WX_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WX = new(5, 6);
        vectorDouble.WX = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(6, 2, 3, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(6, 2, 3, 5)));
        });
    }
    
    [Test]
    public void WY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WY, Is.EqualTo(new Vector2<float>(4, 2)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WY, Is.EqualTo(new Vector2<double>(4, 2)));
        });

    [Test]
    public void WY_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WY = new(5, 6);
        vectorDouble.WY = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 6, 3, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 6, 3, 5)));
        });
    }
    
    [Test]
    public void WZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).WZ, Is.EqualTo(new Vector2<float>(4, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4).WZ, Is.EqualTo(new Vector2<double>(4, 3)));
        });

    [Test]
    public void WZ_Set()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.WZ = new(5, 6);
        vectorDouble.WZ = new(5, 6);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 2, 6, 5)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 2, 6, 5)));
        });
    }

    [Test]
    public void Zero_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Zero, Is.EqualTo(new Vector4<float>(0)));
            Assert.That(Vector4<double>.Zero, Is.EqualTo(new Vector4<double>(0)));
        });

    [Test]
    public void One_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.One, Is.EqualTo(new Vector4<float>(1)));
            Assert.That(Vector4<double>.One, Is.EqualTo(new Vector4<double>(1)));
        });

    [Test]
    public void UnitX_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.UnitX, Is.EqualTo(new Vector4<float>(1, 0, 0, 0)));
            Assert.That(Vector4<double>.UnitX, Is.EqualTo(new Vector4<double>(1, 0, 0, 0)));
        });

    [Test]
    public void UnitY_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.UnitY, Is.EqualTo(new Vector4<float>(0, 1, 0, 0)));
            Assert.That(Vector4<double>.UnitY, Is.EqualTo(new Vector4<double>(0, 1, 0, 0)));
        });

    [Test]
    public void UnitZ_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.UnitZ, Is.EqualTo(new Vector4<float>(0, 0, 1, 0)));
            Assert.That(Vector4<double>.UnitZ, Is.EqualTo(new Vector4<double>(0, 0, 1, 0)));
        });
    
    [Test]
    public void UnitW_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.UnitW, Is.EqualTo(new Vector4<float>(0, 0, 0, 1)));
            Assert.That(Vector4<double>.UnitW, Is.EqualTo(new Vector4<double>(0, 0, 0, 1)));
        });

    [Test]
    public void IndexerGet_IndexBetweenZeroAndThree_ReturnsElement()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat[0], Is.EqualTo(1));
            Assert.That(vectorFloat[1], Is.EqualTo(2));
            Assert.That(vectorFloat[2], Is.EqualTo(3));
            Assert.That(vectorFloat[3], Is.EqualTo(4));

            Assert.That(vectorDouble[0], Is.EqualTo(1));
            Assert.That(vectorDouble[1], Is.EqualTo(2));
            Assert.That(vectorDouble[2], Is.EqualTo(3));
            Assert.That(vectorDouble[3], Is.EqualTo(4));
        });
    }

    [Test]
    public void IndexerGet_IndexLessThanZero_ThrowsArgumentOutOfRangeException() =>
        Assert.Multiple(() =>
        {
            Assert.That(() => Vector4<float>.One[-1], Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Vector4<double>.One[-1], Throws.InstanceOf<ArgumentOutOfRangeException>());
        });

    [Test]
    public void IndexerGet_IndexerMoreThanThree_ThrowsArgumentOutOfRangeException() =>
        Assert.Multiple(() =>
        {
            Assert.That(() => Vector4<float>.One[4], Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Vector4<double>.One[4], Throws.InstanceOf<ArgumentOutOfRangeException>());
        });

    [Test]
    public void IndexerSet_IndexBetweenZeroAndThree_ReturnsElement()
    {
        var vectorFloat = new Vector4<float>();
        var vectorDouble = new Vector4<double>();

        vectorFloat[0] = 1;
        vectorFloat[1] = 2;
        vectorFloat[2] = 3;
        vectorFloat[3] = 4;

        vectorDouble[0] = 1;
        vectorDouble[1] = 2;
        vectorDouble[2] = 3;
        vectorDouble[3] = 4;

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(1, 2, 3, 4)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(1, 2, 3, 4)));
        });
    }

    [Test]
    public void IndexerSet_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
    {
        var vectorFloat = new Vector4<float>();
        var vectorDouble = new Vector4<double>();

        Assert.Multiple(() =>
        {
            Assert.That(() => vectorFloat[-1] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => vectorDouble[-1] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
        });
    }

    [Test]
    public void IndexerSet_IndexerMoreThanThree_ThrowsArgumentOutOfRangeException()
    {
        var vectorFloat = new Vector4<float>();
        var vectorDouble = new Vector4<double>();

        Assert.Multiple(() =>
        {
            Assert.That(() => vectorFloat[4] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => vectorDouble[4] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
        });
    }

    [Test]
    public void ConstructorFloatingPoint_SetsElements()
    {
        var vectorFloat = new Vector4<float>(1);
        var vectorDouble = new Vector4<double>(1);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(1));
            Assert.That(vectorFloat.Z, Is.EqualTo(1));
            Assert.That(vectorFloat.W, Is.EqualTo(1));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(1));
            Assert.That(vectorDouble.Z, Is.EqualTo(1));
            Assert.That(vectorDouble.W, Is.EqualTo(1));
        });
    }

    [Test]
    public void ConstructorFloatingPointFloatingPointFloatingPointFloatingPoint_SetsElements()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(2));
            Assert.That(vectorFloat.Z, Is.EqualTo(3));
            Assert.That(vectorFloat.W, Is.EqualTo(4));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(2));
            Assert.That(vectorDouble.Z, Is.EqualTo(3));
            Assert.That(vectorDouble.W, Is.EqualTo(4));
        });
    }

    [Test]
    public void ConstructorVector2Vector2_SetsElements()
    {
        var vectorFloat = new Vector4<float>(new Vector2<float>(1, 2), new(3, 4));
        var vectorDouble = new Vector4<double>(new Vector2<double>(1, 2), new(3, 4));

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(2));
            Assert.That(vectorFloat.Z, Is.EqualTo(3));
            Assert.That(vectorFloat.W, Is.EqualTo(4));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(2));
            Assert.That(vectorDouble.Z, Is.EqualTo(3));
            Assert.That(vectorDouble.W, Is.EqualTo(4));
        });
    }
    
    [Test]
    public void ConstructorVector2FloatingPointFloatingPoint_SetsElements()
    {
        var vectorFloat = new Vector4<float>(new(1, 2), 3, 4);
        var vectorDouble = new Vector4<double>(new(1, 2), 3, 4);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(2));
            Assert.That(vectorFloat.Z, Is.EqualTo(3));
            Assert.That(vectorFloat.W, Is.EqualTo(4));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(2));
            Assert.That(vectorDouble.Z, Is.EqualTo(3));
            Assert.That(vectorDouble.W, Is.EqualTo(4));
        });
    }

    [Test]
    public void ConstructorVector3FloatingPoint_SetsElements()
    {
        var vectorFloat = new Vector4<float>(new(1, 2, 3), 4);
        var vectorDouble = new Vector4<double>(new(1, 2, 3), 4);

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat.X, Is.EqualTo(1));
            Assert.That(vectorFloat.Y, Is.EqualTo(2));
            Assert.That(vectorFloat.Z, Is.EqualTo(3));
            Assert.That(vectorFloat.W, Is.EqualTo(4));

            Assert.That(vectorDouble.X, Is.EqualTo(1));
            Assert.That(vectorDouble.Y, Is.EqualTo(2));
            Assert.That(vectorDouble.Z, Is.EqualTo(3));
            Assert.That(vectorDouble.W, Is.EqualTo(4));
        });
    }

    [Test]
    public void Normalise_LengthEqualsZero_VectorIsUnchanged()
    {
        var vectorFloat = new Vector4<float>(0);
        var vectorDouble = new Vector4<double>(0);

        vectorFloat.Normalise();
        vectorDouble.Normalise();

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(0)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(0)));
        });
    }

    [Test]
    public void Normalise_LengthIsNotZero_VectorIsNormalised()
    {
        var vectorFloat = new Vector4<float>(1, 2, 3, 4);
        var vectorDouble = new Vector4<double>(1, 2, 3, 4);

        vectorFloat.Normalise();
        vectorDouble.Normalise();

        Assert.Multiple(() =>
        {
            Assert.That(vectorFloat, Is.EqualTo(new Vector4<float>(.18257418f, .36514837f, .5477226f, .73029673f)));
            Assert.That(vectorDouble, Is.EqualTo(new Vector4<double>(.18257418583505536, .3651483716701107, .5477225575051661, .7302967433402214)));
        });
    }

    [Test]
    public void EqualsVector4_ValuesAreEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).Equals(new Vector4<float>(1, 2, 3, 4)), Is.True);
            Assert.That(new Vector4<double>(1, 2, 3, 4).Equals(new Vector4<double>(1, 2, 3, 4)), Is.True);
        });

    [Test]
    public void EqualsVector4_ValuesAreNotEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).Equals(new Vector4<float>(2, 1, 3, 4)), Is.False);
            Assert.That(new Vector4<double>(1, 2, 3, 4).Equals(new Vector4<double>(2, 1, 3, 4)), Is.False);
        });

    [Test]
    public void EqualsObject_ValuesAreEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).Equals((object)new Vector4<float>(1, 2, 3, 4)), Is.True);
            Assert.That(new Vector4<double>(1, 2, 3, 4).Equals((object)new Vector4<double>(1, 2, 3, 4)), Is.True);
        });

    [Test]
    public void EqualsObject_ValuesAreNotEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4).Equals((object)new Vector4<float>(2, 1, 3, 4)), Is.False);
            Assert.That(new Vector4<double>(1, 2, 3, 4).Equals((object)new Vector4<double>(2, 1, 3, 4)), Is.False);
        });

    [Test]
    public void CompareTo_AllFirstComponentsAreLessThanSecondComponents_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(.1f).CompareTo(new Vector4<float>(1)), Is.LessThan(0));
            Assert.That(new Vector4<double>(.1).CompareTo(new Vector4<double>(1)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_AllFirstComponentsAreEqualToSecondComponents_ReturnsZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(.1f).CompareTo(new Vector4<float>(.1f)), Is.Zero);
            Assert.That(new Vector4<double>(.1).CompareTo(new Vector4<double>(.1)), Is.Zero);
        });

    [Test]
    public void CompareTo_AllFirstComponentsAreMoreThanSecondComponents_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1).CompareTo(new Vector4<float>(.1f)), Is.GreaterThan(0));
            Assert.That(new Vector4<double>(1).CompareTo(new Vector4<double>(.1)), Is.GreaterThan(0));
        });

    [Test]
    public void CompareTo_FirstXComponentIsLessThanSecondXComponent_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(.1f, 0, 0, 0).CompareTo(new Vector4<float>(1, 0, 0, 0)), Is.LessThan(0));
            Assert.That(new Vector4<double>(.1, 0, 0, 0).CompareTo(new Vector4<double>(1, 0, 0, 0)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_FirstYComponentIsLessThanSecondYComponent_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(0, .1f, 0, 0).CompareTo(new Vector4<float>(0, 1, 0, 0)), Is.LessThan(0));
            Assert.That(new Vector4<double>(0, .1, 0, 0).CompareTo(new Vector4<double>(0, 1, 0, 0)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_FirstZComponentIsLessThanSecondZComponent_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(0, 0, .1f, 0).CompareTo(new Vector4<float>(0, 0, 1, 0)), Is.LessThan(0));
            Assert.That(new Vector4<double>(0, 0, .1, 0).CompareTo(new Vector4<double>(0, 0, 1, 0)), Is.LessThan(0));
        });
    
    [Test]
    public void CompareTo_FirstWComponentIsLessThanSecondWComponent_ReturnsLessThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(0, 0, 0, .1f).CompareTo(new Vector4<float>(0, 0, 0, 1)), Is.LessThan(0));
            Assert.That(new Vector4<double>(0, 0, 0, .1).CompareTo(new Vector4<double>(0, 0, 0, 1)), Is.LessThan(0));
        });

    [Test]
    public void CompareTo_FirstXComponentIsMoreThanSecondXComponent_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 0, 0, 0).CompareTo(new Vector4<float>(.1f, 0, 0, 0)), Is.GreaterThan(0));
            Assert.That(new Vector4<double>(1, 0, 0, 0).CompareTo(new Vector4<double>(.1, 0, 0, 0)), Is.GreaterThan(0));
        });

    [Test]
    public void CompareTo_FirstYComponentIsMoreThanSecondYComponent_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(0, 1, 0, 0).CompareTo(new Vector4<float>(0, .1f, 0, 0)), Is.GreaterThan(0));
            Assert.That(new Vector4<double>(0, 1, 0, 0).CompareTo(new Vector4<double>(0, .1, 0, 0)), Is.GreaterThan(0));
        });

    [Test]
    public void CompareTo_FirstZComponentIsMoreThanSecondZComponent_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(0, 0, 1, 0).CompareTo(new Vector4<float>(0, 0, .1f, 0)), Is.GreaterThan(0));
            Assert.That(new Vector4<double>(0, 0, 1, 0).CompareTo(new Vector4<double>(0, 0, .1, 0)), Is.GreaterThan(0));
        });
    
    [Test]
    public void CompareTo_FirstWComponentIsMoreThanSecondWComponent_ReturnsMoreThanZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(0, 0, 0, 1).CompareTo(new Vector4<float>(0, 0, 0, .1f)), Is.GreaterThan(0));
            Assert.That(new Vector4<double>(0, 0, 0, 1).CompareTo(new Vector4<double>(0, 0, 0, .1)), Is.GreaterThan(0));
        });

    [Test]
    public void Clamp_ValuesBetweenMinMax_ReturnsValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(1, 2, 3, 3), new(0), new(3)), Is.EqualTo(new Vector4<float>(1, 2, 3, 3)));
            Assert.That(Vector4<double>.Clamp(new(1, 2, 3, 3), new(0), new(3)), Is.EqualTo(new Vector4<double>(1, 2, 3, 3)));
        });

    [Test]
    public void Clamp_ValuesLessThanMin_ReturnsMin() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(1, 2, 2.5f, 2.6f), new(3), new(6)), Is.EqualTo(new Vector4<float>(3)));
            Assert.That(Vector4<double>.Clamp(new(1, 2, 2.5, 2.6), new(3), new(6)), Is.EqualTo(new Vector4<double>(3)));
        });

    [Test]
    public void Clamp_ValuesMoreThanMax_ReturnsMax() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(4, 5, 6, 7), new(0), new(3)), Is.EqualTo(new Vector4<float>(3)));
            Assert.That(Vector4<double>.Clamp(new(4, 5, 6, 7), new(0), new(3)), Is.EqualTo(new Vector4<double>(3)));
        });

    [Test]
    public void ClampValueXLessThanMin_ReturnsMinXValueYValueZValueW() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(1, 5, 5, 5), new(3), new(6)), Is.EqualTo(new Vector4<float>(3, 5, 5, 5)));
            Assert.That(Vector4<double>.Clamp(new(1, 5, 5, 5), new(3), new(6)), Is.EqualTo(new Vector4<double>(3, 5, 5, 5)));
        });

    [Test]
    public void ClampValueYLessThanMin_ReturnsValueXMinYValueZValueW() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(5, 1, 5, 5), new(3), new(6)), Is.EqualTo(new Vector4<float>(5, 3, 5, 5)));
            Assert.That(Vector4<double>.Clamp(new(5, 1, 5, 5), new(3), new(6)), Is.EqualTo(new Vector4<double>(5, 3, 5, 5)));
        });

    [Test]
    public void ClampValueZLessThanMin_ReturnsValueXValueYMinZValueW() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(5, 5, 1, 5), new(3), new(6)), Is.EqualTo(new Vector4<float>(5, 5, 3, 5)));
            Assert.That(Vector4<double>.Clamp(new(5, 5, 1, 5), new(3), new(6)), Is.EqualTo(new Vector4<double>(5, 5, 3, 5)));
        });
    
    [Test]
    public void ClampValueWLessThanMin_ReturnsValueXValueYValueZMinW() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(5, 5, 5, 1), new(3), new(6)), Is.EqualTo(new Vector4<float>(5, 5, 5, 3)));
            Assert.That(Vector4<double>.Clamp(new(5, 5, 5, 1), new(3), new(6)), Is.EqualTo(new Vector4<double>(5, 5, 5, 3)));
        });

    [Test]
    public void ClampValueXMoreThanMax_ReturnsMaxXValueYValueZValueW() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(5, 2, 2, 2), new(0), new(3)), Is.EqualTo(new Vector4<float>(3, 2, 2, 2)));
            Assert.That(Vector4<double>.Clamp(new(5, 2, 2, 2), new(0), new(3)), Is.EqualTo(new Vector4<double>(3, 2, 2, 2)));
        });

    [Test]
    public void ClampValueYMoreThanMax_ReturnsValueXMaxYValueZValueW() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(2, 5, 2, 2), new(0), new(3)), Is.EqualTo(new Vector4<float>(2, 3, 2, 2)));
            Assert.That(Vector4<double>.Clamp(new(2, 5, 2, 2), new(0), new(3)), Is.EqualTo(new Vector4<double>(2, 3, 2, 2)));
        });

    [Test]
    public void ClampValueZMoreThanMax_ReturnsValueXValueYMaxZValueW() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(2, 2, 5, 2), new(0), new(3)), Is.EqualTo(new Vector4<float>(2, 2, 3, 2)));
            Assert.That(Vector4<double>.Clamp(new(2, 2, 5, 2), new(0), new(3)), Is.EqualTo(new Vector4<double>(2, 2, 3, 2)));
        });
    
    [Test]
    public void ClampValueWMoreThanMax_ReturnsValueXValueYValueZMaxW() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Clamp(new(2, 2, 2, 5), new(0), new(3)), Is.EqualTo(new Vector4<float>(2, 2, 2, 3)));
            Assert.That(Vector4<double>.Clamp(new(2, 2, 2, 5), new(0), new(3)), Is.EqualTo(new Vector4<double>(2, 2, 2, 3)));
        });

    [Test]
    public void ComponentMax_Vector1XLessThanVector2X_UsesVector2X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMax(new(1, 0, 0, 0), new(2, 0, 0, 0)), Is.EqualTo(new Vector4<float>(2, 0, 0, 0)));
            Assert.That(Vector4<double>.ComponentMax(new(1, 0, 0, 0), new(2, 0, 0, 0)), Is.EqualTo(new Vector4<double>(2, 0, 0, 0)));
        });

    [Test]
    public void ComponentMax_Vector1XMoreThanVector2X_UsesVector1X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMax(new(2, 0, 0, 0), new(1, 0, 0, 0)), Is.EqualTo(new Vector4<float>(2, 0, 0, 0)));
            Assert.That(Vector4<double>.ComponentMax(new(2, 0, 0, 0), new(1, 0, 0, 0)), Is.EqualTo(new Vector4<double>(2, 0, 0, 0)));
        });

    [Test]
    public void ComponentMax_Vector1YLessThanVector2Y_UsesVector2Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMax(new(0, 1, 0, 0), new(0, 2, 0, 0)), Is.EqualTo(new Vector4<float>(0, 2, 0, 0)));
            Assert.That(Vector4<double>.ComponentMax(new(0, 1, 0, 0), new(0, 2, 0, 0)), Is.EqualTo(new Vector4<double>(0, 2, 0, 0)));
        });

    [Test]
    public void ComponentMax_Vector1YMoreThanVector2Y_UsesVector1Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMax(new(0, 2, 0, 0), new(0, 1, 0, 0)), Is.EqualTo(new Vector4<float>(0, 2, 0, 0)));
            Assert.That(Vector4<double>.ComponentMax(new(0, 2, 0, 0), new(0, 1, 0, 0)), Is.EqualTo(new Vector4<double>(0, 2, 0, 0)));
        });

    [Test]
    public void ComponentMax_Vector1ZLessThanVector2Z_UsesVector2Z() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMax(new(0, 0, 1, 0), new(0, 0, 2, 0)), Is.EqualTo(new Vector4<float>(0, 0, 2, 0)));
            Assert.That(Vector4<double>.ComponentMax(new(0, 0, 1, 0), new(0, 0, 2, 0)), Is.EqualTo(new Vector4<double>(0, 0, 2, 0)));
        });

    [Test]
    public void ComponentMax_Vector1ZMoreThanVector2Z_UsesVector1Z() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMax(new(0, 0, 2, 0), new(0, 0, 1, 0)), Is.EqualTo(new Vector4<float>(0, 0, 2, 0)));
            Assert.That(Vector4<double>.ComponentMax(new(0, 0, 2, 0), new(0, 0, 1, 0)), Is.EqualTo(new Vector4<double>(0, 0, 2, 0)));
        });
    
    [Test]
    public void ComponentMax_Vector1WLessThanVector2W_UsesVector2W() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMax(new(0, 0, 0, 1), new(0, 0, 0, 2)), Is.EqualTo(new Vector4<float>(0, 0, 0, 2)));
            Assert.That(Vector4<double>.ComponentMax(new(0, 0, 0, 1), new(0, 0, 0, 2)), Is.EqualTo(new Vector4<double>(0, 0, 0, 2)));
        });

    [Test]
    public void ComponentMax_Vector1WMoreThanVector2W_UsesVector1W() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMax(new(0, 0, 0, 2), new(0, 0, 0, 1)), Is.EqualTo(new Vector4<float>(0, 0, 0, 2)));
            Assert.That(Vector4<double>.ComponentMax(new(0, 0, 0, 2), new(0, 0, 0, 1)), Is.EqualTo(new Vector4<double>(0, 0, 0, 2)));
        });

    [Test]
    public void ComponentMin_Vector1XLessThanVector2X_UsesVector1X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMin(new(1, 0, 0, 0), new(2, 0, 0, 0)), Is.EqualTo(new Vector4<float>(1, 0, 0, 0)));
            Assert.That(Vector4<double>.ComponentMin(new(1, 0, 0, 0), new(2, 0, 0, 0)), Is.EqualTo(new Vector4<double>(1, 0, 0, 0)));
        });

    [Test]
    public void ComponentMin_Vector1XMoreThanVector2X_UsesVector2X() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMin(new(2, 0, 0, 0), new(1, 0, 0, 0)), Is.EqualTo(new Vector4<float>(1, 0, 0, 0)));
            Assert.That(Vector4<double>.ComponentMin(new(2, 0, 0, 0), new(1, 0, 0, 0)), Is.EqualTo(new Vector4<double>(1, 0, 0, 0)));
        });

    [Test]
    public void ComponentMin_Vector1YLessThanVector2Y_UsesVector1Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMin(new(0, 1, 0, 0), new(0, 2, 0, 0)), Is.EqualTo(new Vector4<float>(0, 1, 0, 0)));
            Assert.That(Vector4<double>.ComponentMin(new(0, 1, 0, 0), new(0, 2, 0, 0)), Is.EqualTo(new Vector4<double>(0, 1, 0, 0)));
        });

    [Test]
    public void ComponentMin_Vector1YMoreThanVector2Y_UsesVector2Y() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMin(new(0, 2, 0, 0), new(0, 1, 0, 0)), Is.EqualTo(new Vector4<float>(0, 1, 0, 0)));
            Assert.That(Vector4<double>.ComponentMin(new(0, 2, 0, 0), new(0, 1, 0, 0)), Is.EqualTo(new Vector4<double>(0, 1, 0, 0)));
        });

    [Test]
    public void ComponentMin_Vector1ZLessThanVector2Z_UsesVector1Z() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMin(new(0, 0, 1, 0), new(0, 0, 2, 0)), Is.EqualTo(new Vector4<float>(0, 0, 1, 0)));
            Assert.That(Vector4<double>.ComponentMin(new(0, 0, 1, 0), new(0, 0, 2, 0)), Is.EqualTo(new Vector4<double>(0, 0, 1, 0)));
        });

    [Test]
    public void ComponentMin_Vector1ZMoreThanVector2Z_UsesVector2Z() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMin(new(0, 0, 2, 0), new(0, 0, 1, 0)), Is.EqualTo(new Vector4<float>(0, 0, 1, 0)));
            Assert.That(Vector4<double>.ComponentMin(new(0, 0, 2, 0), new(0, 0, 1, 0)), Is.EqualTo(new Vector4<double>(0, 0, 1, 0)));
        });
    
    [Test]
    public void ComponentMin_Vector1WLessThanVector2W_UsesVector1W() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMin(new(0, 0, 0, 1), new(0, 0, 0, 2)), Is.EqualTo(new Vector4<float>(0, 0, 0, 1)));
            Assert.That(Vector4<double>.ComponentMin(new(0, 0, 0, 1), new(0, 0, 0, 2)), Is.EqualTo(new Vector4<double>(0, 0, 0, 1)));
        });

    [Test]
    public void ComponentMin_Vector1WMoreThanVector2W_UsesVector2W() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.ComponentMin(new(0, 0, 0, 2), new(0, 0, 0, 1)), Is.EqualTo(new Vector4<float>(0, 0, 0, 1)));
            Assert.That(Vector4<double>.ComponentMin(new(0, 0, 0, 2), new(0, 0, 0, 1)), Is.EqualTo(new Vector4<double>(0, 0, 0, 1)));
        });

    [Test]
    public void Distance_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Distance(new(0), new(1)), Is.EqualTo(2));
            Assert.That(Vector4<double>.Distance(new(0), new(1)), Is.EqualTo(2));
        });

    [Test]
    public void DistanceSquared_Get() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.DistanceSquared(new(0), new(1)), Is.EqualTo(4));
            Assert.That(Vector4<double>.DistanceSquared(new(0), new(1)), Is.EqualTo(4));
        });

    [Test]
    public void Dot_VectorsOpposite_ReturnsMinusOne() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Dot(new(0, 0, 0, 1), new(0, 0, 0, -1)), Is.EqualTo(-1));
            Assert.That(Vector4<double>.Dot(new(0, 0, 0, 1), new(0, 0, 0, -1)), Is.EqualTo(-1));
        });

    [Test]
    public void Dot_VectorsPerpendicular_ReturnsZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Dot(new(0, 0, 0, 1), new(0, 0, 1, 0)), Is.EqualTo(0));
            Assert.That(Vector4<double>.Dot(new(0, 0, 0, 1), new(0, 0, 1, 0)), Is.EqualTo(0));
        });

    [Test]
    public void Dot_VectorsSame_ReturnsOne() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Dot(new(0, 0, 0, 1), new(0, 0, 0, 1)), Is.EqualTo(1));
            Assert.That(Vector4<double>.Dot(new(0, 0, 0, 1), new(0, 0, 0, 1)), Is.EqualTo(1));
        });

    [Test]
    public void Lerp_AmountBetweenZeroAndOne_ReturnsInterpolatedValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Lerp(new(0), new(10), .5f), Is.EqualTo(new Vector4<float>(5)));
            Assert.That(Vector4<double>.Lerp(new(0), new(10), .5), Is.EqualTo(new Vector4<double>(5)));
        });

    [Test]
    public void Lerp_AmountLessThanZero_ReturnsLessThanFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Lerp(new(0), new(10), -1), Is.EqualTo(new Vector4<float>(-10)));
            Assert.That(Vector4<double>.Lerp(new(0), new(10), -1), Is.EqualTo(new Vector4<double>(-10)));
        });

    [Test]
    public void Lerp_AmountMoreThanOne_ReturnsMoreThanSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Lerp(new(0), new(10), 2), Is.EqualTo(new Vector4<float>(20)));
            Assert.That(Vector4<double>.Lerp(new(0), new(10), 2), Is.EqualTo(new Vector4<double>(20)));
        });

    [Test]
    public void Lerp_AmountIsZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Lerp(new(0), new(10), 0), Is.EqualTo(new Vector4<float>(0)));
            Assert.That(Vector4<double>.Lerp(new(0), new(10), 0), Is.EqualTo(new Vector4<double>(0)));
        });

    [Test]
    public void Lerp_AmountIsOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.Lerp(new(0), new(10), 1), Is.EqualTo(new Vector4<float>(10)));
            Assert.That(Vector4<double>.Lerp(new(0), new(10), 1), Is.EqualTo(new Vector4<double>(10)));
        });

    [Test]
    public void LerpClamped_AmountBetweenZeroAndOne_ReturnsInterpolatedValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.LerpClamped(new(0), new(10), .5f), Is.EqualTo(new Vector4<float>(5)));
            Assert.That(Vector4<double>.LerpClamped(new(0), new(10), .5f), Is.EqualTo(new Vector4<double>(5)));
        });

    [Test]
    public void LerpClamped_AmountLessThanZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.LerpClamped(new(0), new(10), -1), Is.EqualTo(new Vector4<float>(0)));
            Assert.That(Vector4<double>.LerpClamped(new(0), new(10), -1), Is.EqualTo(new Vector4<double>(0)));
        });

    [Test]
    public void LerpClamped_AmountMoreThanOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.LerpClamped(new(0), new(10), 2), Is.EqualTo(new Vector4<float>(10)));
            Assert.That(Vector4<double>.LerpClamped(new(0), new(10), 2), Is.EqualTo(new Vector4<double>(10)));
        });

    [Test]
    public void LerpClamped_AmountIsZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.LerpClamped(new(0), new(10), 0), Is.EqualTo(new Vector4<float>(0)));
            Assert.That(Vector4<double>.LerpClamped(new(0), new(10), 0), Is.EqualTo(new Vector4<double>(0)));
        });

    [Test]
    public void LerpClamped_AmountIsOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(Vector4<float>.LerpClamped(new(0), new(10), 1), Is.EqualTo(new Vector4<float>(10)));
            Assert.That(Vector4<double>.LerpClamped(new(0), new(10), 1), Is.EqualTo(new Vector4<double>(10)));
        });

    [Test]
    public void OperatorAddVector4FloatingPoint_AddsVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4) + 1, Is.EqualTo(new Vector4<float>(2, 3, 4, 5)));
            Assert.That(new Vector4<double>(1, 2, 3, 4) + 1, Is.EqualTo(new Vector4<double>(2, 3, 4, 5)));
        });

    [Test]
    public void OperatorAddVector4Vector4_AddsVectorsComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4) + new Vector4<float>(3, 4, 5, 6), Is.EqualTo(new Vector4<float>(4, 6, 8, 10)));
            Assert.That(new Vector4<double>(1, 2, 3, 4) + new Vector4<double>(3, 4, 5, 6), Is.EqualTo(new Vector4<double>(4, 6, 8, 10)));
        });

    [Test]
    public void OperatorSubtractVector4FloatingPoint_SubtractsVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4) - 1, Is.EqualTo(new Vector4<float>(0, 1, 2, 3)));
            Assert.That(new Vector4<double>(1, 2, 3, 4) - 1, Is.EqualTo(new Vector4<double>(0, 1, 2, 3)));
        });

    [Test]
    public void OperatorSubtractVector4Vector4_SubtractsVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(3, 4, 5, 6) - new Vector4<float>(1, 3, 4, 6), Is.EqualTo(new Vector4<float>(2, 1, 1, 0)));
            Assert.That(new Vector4<double>(3, 4, 5, 6) - new Vector4<double>(1, 3, 4, 6), Is.EqualTo(new Vector4<double>(2, 1, 1, 0)));
        });

    [Test]
    public void OperatorNegate_NegatesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(-new Vector4<float>(1, 2, 3, 4), Is.EqualTo(new Vector4<float>(-1, -2, -3, -4)));
            Assert.That(-new Vector4<double>(1, 2, 3, 4), Is.EqualTo(new Vector4<double>(-1, -2, -3, -4)));
        });

    [Test]
    public void OperatorMultiplyFloatingPointVector4_MultiplesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(2 * new Vector4<float>(1, 2, 3, 4), Is.EqualTo(new Vector4<float>(2, 4, 6, 8)));
            Assert.That(2 * new Vector4<double>(1, 2, 3, 4), Is.EqualTo(new Vector4<double>(2, 4, 6, 8)));
        });

    [Test]
    public void OperatorMultipleVector4FloatingPoint_MultiplesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4) * 2, Is.EqualTo(new Vector4<float>(2, 4, 6, 8)));
            Assert.That(new Vector4<double>(1, 2, 3, 4) * 2, Is.EqualTo(new Vector4<double>(2, 4, 6, 8)));
        });

    [Test]
    public void OperatorMultipleVector4Vector4_MultiplesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4) * new Vector4<float>(3, 4, 5, 6), Is.EqualTo(new Vector4<float>(3, 8, 15, 24)));
            Assert.That(new Vector4<double>(1, 2, 3, 4) * new Vector4<double>(3, 4, 5, 6), Is.EqualTo(new Vector4<double>(3, 8, 15, 24)));
        });

    [Test]
    public void OperatorDivideVector4FloatingPoint_DividesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(2, 3, 6, 8) / 2, Is.EqualTo(new Vector4<float>(1, 1.5f, 3, 4)));
            Assert.That(new Vector4<double>(2, 3, 6, 8) / 2, Is.EqualTo(new Vector4<double>(1, 1.5, 3, 4)));
        });

    [Test]
    public void OperatorDivideVector4Vector4_DividesVectorComponents() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(2, 12, 15, 24) / new Vector4<float>(2, 4, 3, 6), Is.EqualTo(new Vector4<float>(1, 3, 5, 4)));
            Assert.That(new Vector4<double>(2, 12, 15, 24) / new Vector4<double>(2, 4, 3, 6), Is.EqualTo(new Vector4<double>(1, 3, 5, 4)));
        });

    [Test]
    public void OperatorLessThan_LeftIsLessThanRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1) < new Vector4<float>(2), Is.True);
            Assert.That(new Vector4<double>(1) < new Vector4<double>(2), Is.True);
        });

    [Test]
    public void OperatorLessThan_LeftIsEqualToRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1) < new Vector4<float>(1), Is.False);
            Assert.That(new Vector4<double>(1) < new Vector4<double>(1), Is.False);
        });

    [Test]
    public void OperatorLessThan_LeftIsGreaterThanRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(2) < new Vector4<float>(1), Is.False);
            Assert.That(new Vector4<double>(2) < new Vector4<double>(1), Is.False);
        });

    [Test]
    public void OperatorLessThanEquals_LeftIsLessThanRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1) <= new Vector4<float>(2), Is.True);
            Assert.That(new Vector4<double>(1) <= new Vector4<double>(2), Is.True);
        });

    [Test]
    public void OperatorLessThanEquals_LeftIsEqualToRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1) <= new Vector4<float>(1), Is.True);
            Assert.That(new Vector4<double>(1) <= new Vector4<double>(1), Is.True);
        });

    [Test]
    public void OperatorLessThanEquals_LeftIsGreaterThanRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(2) <= new Vector4<float>(1), Is.False);
            Assert.That(new Vector4<double>(2) <= new Vector4<double>(1), Is.False);
        });

    [Test]
    public void OperatorGreaterThan_LeftIsLessThanRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1) > new Vector4<float>(2), Is.False);
            Assert.That(new Vector4<double>(1) > new Vector4<double>(2), Is.False);
        });

    [Test]
    public void OperatorGreaterThan_LeftIsEqualToRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1) > new Vector4<float>(1), Is.False);
            Assert.That(new Vector4<double>(1) > new Vector4<double>(1), Is.False);
        });

    [Test]
    public void OperatorGreaterThan_LeftIsGreaterThanRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(2) > new Vector4<float>(1), Is.True);
            Assert.That(new Vector4<double>(2) > new Vector4<double>(1), Is.True);
        });

    [Test]
    public void OperatorGreaterThanEquals_LeftIsLessThanRight_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1) >= new Vector4<float>(2), Is.False);
            Assert.That(new Vector4<double>(1) >= new Vector4<double>(2), Is.False);
        });

    [Test]
    public void OperatorGreaterThanEquals_LeftIsEqualToRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1) >= new Vector4<float>(1), Is.True);
            Assert.That(new Vector4<double>(1) >= new Vector4<double>(1), Is.True);
        });

    [Test]
    public void OperatorGreaterThanEquals_LeftIsGreaterThanRight_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(2) >= new Vector4<float>(1), Is.True);
            Assert.That(new Vector4<double>(2) >= new Vector4<double>(1), Is.True);
        });

    [Test]
    public void OperatorEquals_ValuesAreEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4) == new Vector4<float>(1, 2, 3, 4), Is.True);
            Assert.That(new Vector4<double>(1, 2, 3, 4) == new Vector4<double>(1, 2, 3, 4), Is.True);
        });

    [Test]
    public void OperatorEquals_ValuesAreNotEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4) == new Vector4<float>(), Is.False);
            Assert.That(new Vector4<double>(1, 2, 3, 4) == new Vector4<double>(), Is.False);
        });

    [Test]
    public void OperatorNotEquals_ValuesAreEqual_ReturnsFalse() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4) != new Vector4<float>(1, 2, 3, 4), Is.False);
            Assert.That(new Vector4<double>(1, 2, 3, 4) != new Vector4<double>(1, 2, 3, 4), Is.False);
        });

    [Test]
    public void OperatorNotEquals_ValuesAreNotEqual_ReturnsTrue() =>
        Assert.Multiple(() =>
        {
            Assert.That(new Vector4<float>(1, 2, 3, 4) != new Vector4<float>(), Is.True);
            Assert.That(new Vector4<double>(1, 2, 3, 4) != new Vector4<double>(), Is.True);
        });
}

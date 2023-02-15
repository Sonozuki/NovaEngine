namespace NovaEngine.Tests.Maths;

/// <summary>The <see cref="MathsHelper{T}"/> tests.</summary>
internal class MathsHelperTests
{
    /*********
    ** Public Methods
    *********/
    [Test]
    public void Lerp_AmountBetweenZeroAndOne_ReturnsInterpolatedValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.Lerp((Half)0, (Half)10, (Half).5), Is.EqualTo((Half)5));
            Assert.That(MathsHelper<float>.Lerp(0, 10, .5f), Is.EqualTo(5f));
            Assert.That(MathsHelper<double>.Lerp(0, 10, .5), Is.EqualTo(5d));
            Assert.That(MathsHelper<decimal>.Lerp(0, 10, .5m), Is.EqualTo(5m));
        });

    [Test]
    public void Lerp_AmountLessThanZero_ReturnsLessThanFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.Lerp((Half)0, (Half)10, (Half)(-1)), Is.EqualTo((Half)(-10)));
            Assert.That(MathsHelper<float>.Lerp(0, 10, -1), Is.EqualTo(-10f));
            Assert.That(MathsHelper<double>.Lerp(0, 10, -1), Is.EqualTo(-10d));
            Assert.That(MathsHelper<decimal>.Lerp(0, 10, -1), Is.EqualTo(-10m));
        });

    [Test]
    public void Lerp_AmountMoreThanOne_ReturnsMoreThanSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.Lerp((Half)0, (Half)10, (Half)2), Is.EqualTo((Half)20));
            Assert.That(MathsHelper<float>.Lerp(0, 10, 2), Is.EqualTo(20f));
            Assert.That(MathsHelper<double>.Lerp(0, 10, 2), Is.EqualTo(20d));
            Assert.That(MathsHelper<decimal>.Lerp(0, 10, 2), Is.EqualTo(20m));
        });

    [Test]
    public void Lerp_AmountIsZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.Lerp((Half)0, (Half)10, (Half)0), Is.EqualTo((Half)0));
            Assert.That(MathsHelper<float>.Lerp(0, 10, 0), Is.EqualTo(0f));
            Assert.That(MathsHelper<double>.Lerp(0, 10, 0), Is.EqualTo(0d));
            Assert.That(MathsHelper<decimal>.Lerp(0, 10, 0), Is.EqualTo(0m));
        });

    [Test]
    public void Lerp_AmountIsOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.Lerp((Half)0, (Half)10, (Half)1), Is.EqualTo((Half)10));
            Assert.That(MathsHelper<float>.Lerp(0, 10, 1), Is.EqualTo(10f));
            Assert.That(MathsHelper<double>.Lerp(0, 10, 1), Is.EqualTo(10d));
            Assert.That(MathsHelper<decimal>.Lerp(0, 10, 1), Is.EqualTo(10m));
        });

    [Test]
    public void LerpClamped_AmountBetweenZeroAndOne_ReturnsInterpolatedValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.LerpClamped((Half)0, (Half)10, (Half).5), Is.EqualTo((Half)5));
            Assert.That(MathsHelper<float>.LerpClamped(0, 10, .5f), Is.EqualTo(5f));
            Assert.That(MathsHelper<double>.LerpClamped(0, 10, .5), Is.EqualTo(5d));
            Assert.That(MathsHelper<decimal>.LerpClamped(0, 10, .5m), Is.EqualTo(5m));
        });

    [Test]
    public void LerpClamped_AmountLessThanZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.LerpClamped((Half)0, (Half)10, (Half)(-1)), Is.EqualTo((Half)0));
            Assert.That(MathsHelper<float>.LerpClamped(0, 10, -1), Is.EqualTo(0f));
            Assert.That(MathsHelper<double>.LerpClamped(0, 10, -1), Is.EqualTo(0d));
            Assert.That(MathsHelper<decimal>.LerpClamped(0, 10, -1), Is.EqualTo(0m));
        });

    [Test]
    public void LerpClamped_AmountMoreThanOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.LerpClamped((Half)0, (Half)10, (Half)2), Is.EqualTo((Half)10));
            Assert.That(MathsHelper<float>.LerpClamped(0, 10, 2), Is.EqualTo(10f));
            Assert.That(MathsHelper<double>.LerpClamped(0, 10, 2), Is.EqualTo(10d));
            Assert.That(MathsHelper<decimal>.LerpClamped(0, 10, 2), Is.EqualTo(10m));
        });

    [Test]
    public void LerpClamped_AmountIsZero_ReturnsFirstValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.LerpClamped((Half)0, (Half)10, (Half)0), Is.EqualTo((Half)0));
            Assert.That(MathsHelper<float>.LerpClamped(0, 10, 0), Is.EqualTo(0f));
            Assert.That(MathsHelper<double>.LerpClamped(0, 10, 0), Is.EqualTo(0d));
            Assert.That(MathsHelper<decimal>.LerpClamped(0, 10, 0), Is.EqualTo(0m));
        });

    [Test]
    public void LerpClamped_AmountIsOne_ReturnsSecondValue() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.LerpClamped((Half)0, (Half)10, (Half)1), Is.EqualTo((Half)10));
            Assert.That(MathsHelper<float>.LerpClamped(0, 10, 1), Is.EqualTo(10f));
            Assert.That(MathsHelper<double>.LerpClamped(0, 10, 1), Is.EqualTo(10d));
            Assert.That(MathsHelper<decimal>.LerpClamped(0, 10, 1), Is.EqualTo(10m));
        });

    [Test]
    public void DegreesToRadians_DegreesIsZero_ReturnsZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.DegreesToRadians((Half)0), Is.EqualTo((Half)0));
            Assert.That(MathsHelper<float>.DegreesToRadians(0), Is.EqualTo(0f));
            Assert.That(MathsHelper<double>.DegreesToRadians(0), Is.EqualTo(0d));
            Assert.That(MathsHelper<decimal>.DegreesToRadians(0), Is.EqualTo(0m));
        });

    [Test]
    public void DegreesToRadians_DegreesIsNotZero_ReturnsRadians() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.DegreesToRadians((Half)90), Is.EqualTo((Half)1.569));
            Assert.That(MathsHelper<float>.DegreesToRadians(90), Is.EqualTo(1.5707964f));
            Assert.That(MathsHelper<double>.DegreesToRadians(90), Is.EqualTo(1.5707963267948966));
            Assert.That(MathsHelper<decimal>.DegreesToRadians(90), Is.EqualTo(1.570796326794896619231321693m));
        });

    [Test]
    public void RadiansToDegrees_RadiansIsZero_ReturnsZero() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.RadiansToDegrees((Half)0), Is.EqualTo((Half)0));
            Assert.That(MathsHelper<float>.RadiansToDegrees(0), Is.EqualTo(0f));
            Assert.That(MathsHelper<double>.RadiansToDegrees(0), Is.EqualTo(0d));
            Assert.That(MathsHelper<decimal>.RadiansToDegrees(0), Is.EqualTo(0m));
        });

    [Test]
    public void RadiansToDegrees_RadiansIsNotZero_ReturnsDegrees() =>
        Assert.Multiple(() =>
        {
            Assert.That(MathsHelper<Half>.RadiansToDegrees((Half)1.569), Is.EqualTo((Half)89.94));
            Assert.That(MathsHelper<float>.RadiansToDegrees(1.5707964f), Is.EqualTo(90f));
            Assert.That(MathsHelper<double>.RadiansToDegrees(1.5707963267948966), Is.EqualTo(90d));
            Assert.That(MathsHelper<decimal>.RadiansToDegrees(1.570796326794896619231321692m), Is.EqualTo(90.00000000000000000000000002m));
        });
}

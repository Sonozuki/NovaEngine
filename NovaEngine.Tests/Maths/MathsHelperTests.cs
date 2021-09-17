using NovaEngine.Maths;
using NUnit.Framework;

namespace NovaEngine.Tests.Maths
{
    /// <summary>The <see cref="MathsHelper"/> tests.</summary>
    [TestFixture]
    public class MathsHelperTests
    {
        /*********
        ** Public Methods
        *********/
        /****
        ** Lerp (float)
        ****/
        /// <summary>Tests <see cref="MathsHelper.Lerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving an amount that is in between '0' and '1' will result in a value that's correctly interpolated between the two values.</remarks>
        [Test]
        public void LerpFloat_AmountBetweenZeroAndOne_ReturnsInterpolatedValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, .5f);
            Assert.AreEqual(5, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.Lerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving an amount that is less than '0' will result in a value that's less than the first value (meaning the method is unclamped).</remarks>
        [Test]
        public void LerpFloat_AmountLessThanZero_ReturnsLessThanFirstValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, -1f);
            Assert.AreEqual(-10, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.Lerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving an amount that is more than '1' will result in a value that's more than the second value (meaning the method is unclamped).</remarks>
        [Test]
        public void LerpFloat_AmountMoreThanOne_ReturnsMoreThanSecondValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, 2f);
            Assert.AreEqual(20, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.Lerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving the amount '0' will result in the first value.</remarks>
        [Test]
        public void LerpFloat_AmountIsZero_ReturnsFirstValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, 0f);
            Assert.AreEqual(0, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.Lerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving the amount '1' will result in the second value.</remarks>
        [Test]
        public void LerpFloat_AmountIsOne_ReturnsSecondValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, 1f);
            Assert.AreEqual(10, lerpValue);
        }

        /****
        ** Lerp (double)
        ****/
        /// <summary>Tests <see cref="MathsHelper.Lerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving an amount that is in between '0' and '1' will result in a value that's correctly interpolated between the two values.</remarks>
        [Test]
        public void LerpDouble_AmountBetweenZeroAndOne_ReturnsInterpolatedValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, .5d);
            Assert.AreEqual(5, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.Lerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving an amount that is less than '0' will result in a value that's less than the first value (meaning the method is unclamped).</remarks>
        [Test]
        public void LerpDouble_AmountLessThanZero_ReturnsLessThanFirstValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, -1d);
            Assert.AreEqual(-10, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.Lerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving an amount that is more than '1' will result in a value that's more than the second value (meaning the method is unclamped).</remarks>
        [Test]
        public void LerpDouble_AmountMoreThanOne_ReturnsMoreThanSecondValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, 2d);
            Assert.AreEqual(20, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.Lerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving the amount '0' will result in the first value.</remarks>
        [Test]
        public void LerpDouble_AmountIsZero_ReturnsFirstValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, 0d);
            Assert.AreEqual(0, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.Lerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving the amount '1' will result in the second value.</remarks>
        [Test]
        public void LerpDouble_AmountIsOne_ReturnsSecondValue()
        {
            var lerpValue = MathsHelper.Lerp(0, 10, 1d);
            Assert.AreEqual(10, lerpValue);
        }

        /****
        ** ClampedLerp (float)
        ****/
        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving an amount that is in between '0' and '1' will result in a value that's correctly interpolated between the two values.</remarks>
        [Test]
        public void ClampedLerpFloat_AmountBetweenZeroAndOne_ReturnsInterpolatedValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, .5f);
            Assert.AreEqual(5, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving an amount that is less than '0' will result in the first value (meaning the method is clamped).</remarks>
        [Test]
        public void ClampedLerpFloat_AmountLessThanZero_ReturnsFirstValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, -1f);
            Assert.AreEqual(00, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving an amount that is more than '1' will result in the second value (meaning the method is clamped).</remarks>
        [Test]
        public void ClampedLerpFloat_AmountMoreThanOne_ReturnsSecondValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, 2f);
            Assert.AreEqual(10, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving the amount '0' will result in the first value.</remarks>
        [Test]
        public void ClampedLerpFloat_AmountIsZero_ReturnsFirstValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, 0f);
            Assert.AreEqual(0, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(float, float, float)"/>.</summary>
        /// <remarks>This tests that giving the amount '1' will result in the second value.</remarks>
        [Test]
        public void ClampedLerpFloat_AmountIsOne_ReturnsSecondValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, 1f);
            Assert.AreEqual(10, lerpValue);
        }

        /****
        ** ClampedLerp (double)
        ****/
        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving an amount that is in between '0' and '1' will result in a value that's correctly interpolated between the two values.</remarks>
        [Test]
        public void ClampedLerpDouble_AmountBetweenZeroAndOne_ReturnsInterpolatedValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, .5d);
            Assert.AreEqual(5, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving an amount that is less than '0' will result in the first value (meaning the method is clamped).</remarks>
        [Test]
        public void ClampedLerpDouble_AmountLessThanZero_ReturnsFirstValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, -1d);
            Assert.AreEqual(0, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving an amount that is more than '1' will result in the second value (meaning the method is clamped).</remarks>
        [Test]
        public void ClampedLerpDouble_AmountMoreThanOne_ReturnsSecondValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, 2d);
            Assert.AreEqual(10, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving the amount '0' will result in the first value.</remarks>
        [Test]
        public void ClampedLerpDouble_AmountIsZero_ReturnsFirstValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, 0d);
            Assert.AreEqual(0, lerpValue);
        }

        /// <summary>Tests <see cref="MathsHelper.ClampedLerp(double, double, double)"/>.</summary>
        /// <remarks>This tests that giving the amount '1' will result in the second value.</remarks>
        [Test]
        public void ClampedLerpDouble_AmountIsOne_ReturnsSecondValue()
        {
            var lerpValue = MathsHelper.ClampedLerp(0, 10, 1d);
            Assert.AreEqual(10, lerpValue);
        }

        /****
        ** DegreesToRadians (float)
        ****/
        /// <summary>Tests <see cref="MathsHelper.DegreesToRadians(float)"/>.</summary>
        /// <remarks>This tests that giving the degrees '0' will result in '0'.</remarks>
        [Test]
        public void DegreesToRadiansFloat_DegreesIsZero_ReturnsZero()
        {
            var radians = MathsHelper.DegreesToRadians(0f);
            Assert.AreEqual(0, radians);
        }

        /// <summary>Tests <see cref="MathsHelper.DegreesToRadians(float)"/>.</summary>
        /// <remarks>This tests that any given value will result in the radians equivalent.</remarks>
        [Test]
        public void DegreesToRadiansFloat_DegreesIsNotZero_ReturnsRadians()
        {
            var radians = MathsHelper.DegreesToRadians(90f);
            Assert.AreEqual(1.57079633f, radians);
        }

        /****
        ** DegreesToRadians (double)
        ****/
        /// <summary>Tests <see cref="MathsHelper.DegreesToRadians(double)"/>.</summary>
        /// <remarks>This tests that giving the degrees '0' will result in '0'.</remarks>
        [Test]
        public void DegreesToRadiansDouble_DegreesIsZero_ReturnsZero()
        {
            var radians = MathsHelper.DegreesToRadians(0d);
            Assert.AreEqual(0, radians);
        }

        /// <summary>Tests <see cref="MathsHelper.DegreesToRadians(double)"/>.</summary>
        /// <remarks>This tests that any given value will result in the radians equivalent.</remarks>
        [Test]
        public void DegreesToRadiansDouble_DegreesIsNotZero_ReturnsRadians()
        {
            var radians = MathsHelper.DegreesToRadians(90d);
            Assert.AreEqual(1.5707963267948966d, radians);
        }

        /****
        ** RadiansToDegrees (float)
        ****/
        /// <summary>Tests <see cref="MathsHelper.RadiansToDegrees(float)"/>.</summary>
        /// <remarks>This tests that giving the radians '0' will result in '0'.</remarks>
        [Test]
        public void RadiansToDegreesFloat_RadiansIsZero_ReturnsZero()
        {
            var degrees = MathsHelper.RadiansToDegrees(0f);
            Assert.AreEqual(0, degrees);
        }

        /// <summary>Tests <see cref="MathsHelper.RadiansToDegrees(float)"/>.</summary>
        /// <remarks>This tests that any given value will result in the degrees equivalent.</remarks>
        [Test]
        public void RadiansToDegreesFloat_RadiansIsNotZero_ReturnsDegrees()
        {
            var degrees = MathsHelper.RadiansToDegrees(1.57079633f);
            Assert.AreEqual(90, degrees);
        }

        /****
        ** RadiansToDegrees (double)
        ****/
        /// <summary>Tests <see cref="MathsHelper.RadiansToDegrees(double)"/>.</summary>
        /// <remarks>This tests that giving the radians '0' will result in '0'.</remarks>
        [Test]
        public void RadiansToDegreesDouble_RadiansIsZero_ReturnsZero()
        {
            var degrees = MathsHelper.RadiansToDegrees(0d);
            Assert.AreEqual(0, degrees);
        }

        /// <summary>Tests <see cref="MathsHelper.RadiansToDegrees(double)"/>.</summary>
        /// <remarks>This tests that any given value will result in the degrees equivalent.</remarks>
        [Test]
        public void RadiansToDegreesDouble_RadiansIsNotZero_ReturnsDegrees()
        {
            var degrees = MathsHelper.RadiansToDegrees(1.5707963267948966d);
            Assert.AreEqual(90, degrees);
        }
    }
}

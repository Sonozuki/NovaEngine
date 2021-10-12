using System;

namespace NovaEngine.Maths
{
    /// <summary>Contains helpful mathematical methods and constants.</summary>
    public static class MathsHelper
    {
        /*********
        ** Constants
        *********/
        /// <summary>Degrees to radians conversion constant.</summary>
        public const float DegToRad = MathF.PI / 180;

        /// <summary>Radians to degrees conversion constant.</summary>
        public const float RadToDeg = 180 / MathF.PI;

        /// <summary>Degrees to radians conversion constant.</summary>
        private const double DegToRadD = Math.PI / 180;

        /// <summary>Radians to degrees conversion constant.</summary>
        private const double RadToDegD = 180 / Math.PI;


        /*********
        ** Public Methods
        *********/
        /// <summary>Linearly interpolates between two values.</summary>
        /// <param name="value1">The source value.</param>
        /// <param name="value2">The destination value.</param>
        /// <param name="amount">The amount to interpolate between <paramref name="value1"/> and <paramref name="value2"/>.</param>
        /// <returns>The interpolated value.</returns>
        public static float Lerp(float value1, float value2, float amount) => amount * (value2 - value1) + value1;

        /// <summary>Linearly interpolates between two values.</summary>
        /// <param name="value1">The source value.</param>
        /// <param name="value2">The destination value.</param>
        /// <param name="amount">The amount to interpolate between <paramref name="value1"/> and <paramref name="value2"/>.</param>
        /// <returns>The interpolated value.</returns>
        public static double Lerp(double value1, double value2, double amount) => amount * (value2 - value1) + value1;

        /// <summary>Linearly interpolates between two values.</summary>
        /// <param name="value1">The source value.</param>
        /// <param name="value2">The destination value.</param>
        /// <param name="amount">The amount to interpolate between <paramref name="value1"/> and <paramref name="value2"/>.</param>
        /// <returns>The interpolated value.</returns>
        /// <remarks>This clamps <paramref name="amount"/> before performing the linear interpolation.</remarks>
        public static float ClampedLerp(float value1, float value2, float amount)
        {
            amount = Math.Clamp(amount, 0, 1);
            return MathsHelper.Lerp(value1, value2, amount);
        }

        /// <summary>Linearly interpolates between two values.</summary>
        /// <param name="value1">The source value.</param>
        /// <param name="value2">The destination value.</param>
        /// <param name="amount">The amount to interpolate between <paramref name="value1"/> and <paramref name="value2"/>.</param>
        /// <returns>The interpolated value.</returns>
        /// <remarks>This clamps <paramref name="amount"/> before performing the linear interpolation.</remarks>
        public static double ClampedLerp(double value1, double value2, double amount)
        {
            amount = Math.Clamp(amount, 0, 1);
            return MathsHelper.Lerp(value1, value2, amount);
        }

        /// <summary>Converts degrees to radians.</summary>
        /// <param name="degrees">The angle, in degrees.</param>
        /// <returns>The angle, in radians.</returns>
        public static float DegreesToRadians(float degrees) => degrees * DegToRad;

        /// <summary>Converts degrees to radians.</summary>
        /// <param name="degrees">The angle, in degrees.</param>
        /// <returns>The angle, in radians.</returns>
        public static double DegreesToRadians(double degrees) => degrees * DegToRadD;

        /// <summary>Converts radians to degrees.</summary>
        /// <param name="radians">The angle, in radians.</param>
        /// <returns>The angle, in degrees.</returns>
        public static float RadiansToDegrees(float radians) => radians * RadToDeg;

        /// <summary>Converts radians to degrees.</summary>
        /// <param name="radians">The angle, in radians.</param>
        /// <returns>The angle, in degrees.</returns>
        public static double RadiansToDegrees(double radians) => radians * RadToDegD;
    }
}

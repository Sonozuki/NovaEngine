using System;

namespace NovaEngine.Maths
{
    /// <summary>Contains helpful mathematical methods and constants.</summary>
    public static class MathsHelper
    {
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
            amount = MathsHelper.Clamp(amount, 0, 1);
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
            amount = MathsHelper.Clamp(amount, 0, 1);
            return MathsHelper.Lerp(value1, value2, amount);
        }

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value, float min, float max) => MathF.Max(min, Math.Min(max, value));

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static double Clamp(double value, double min, double max) => Math.Max(min, Math.Min(max, value));

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static byte Clamp(byte value, byte min, byte max) => Math.Max(min, Math.Min(max, value));

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static sbyte Clamp(sbyte value, sbyte min, sbyte max) => Math.Max(min, Math.Min(max, value));

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static short Clamp(short value, short min, short max) => Math.Max(min, Math.Min(max, value));

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static ushort Clamp(ushort value, ushort min, ushort max) => Math.Max(min, Math.Min(max, value));

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max) => Math.Max(min, Math.Min(max, value));

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static uint Clamp(uint value, uint min, uint max) => Math.Max(min, Math.Min(max, value));

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static long Clamp(long value, long min, long max) => Math.Max(min, Math.Min(max, value));

        /// <summary>Clamps a value to the specified minimum and maximum values.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static ulong Clamp(ulong value, ulong min, ulong max) => Math.Max(min, Math.Min(max, value));

        /// <summary>Converts degrees to radians.</summary>
        /// <param name="degrees">The angle, in degrees.</param>
        /// <returns>The angle, in radians.</returns>
        public static float DegreesToRadians(float degrees) => degrees * MathF.PI / 180;

        /// <summary>Converts degrees to radians.</summary>
        /// <param name="degrees">The angle, in degrees.</param>
        /// <returns>The angle, in radians.</returns>
        public static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

        /// <summary>Converts radians to degrees.</summary>
        /// <param name="radians">The angle, in radians.</param>
        /// <returns>The angle, in degrees.</returns>
        public static float RadiansToDegrees(float radians) => radians * 180 / MathF.PI;

        /// <summary>Converts radians to degrees.</summary>
        /// <param name="radians">The angle, in radians.</param>
        /// <returns>The angle, in degrees.</returns>
        public static double RadiansToDegrees(double radians) => radians * 180 / Math.PI;
    }
}

using System.Numerics;

namespace NovaEngine.Maths;

/// <summary>Contains helpful mathematical methods and constants.</summary>
public static class MathsHelper<T>
    where T : IFloatingPoint<T>, IFloatingPointConstants<T>
{
    /*********
    ** Fields
    *********/
    /// <summary>Degrees to radians conversion constant.</summary>
    public static readonly T DegToRad = T.Pi / T.CreateChecked(180);

    /// <summary>Radians to degrees conversion constant.</summary>
    public static readonly T RadToDeg = T.CreateChecked(180) / T.Pi;


    /*********
    ** Public Methods
    *********/
    /// <summary>Linearly interpolates between two values.</summary>
    /// <param name="value1">The source value.</param>
    /// <param name="value2">The destination value.</param>
    /// <param name="amount">The amount to interpolate between the values.</param>
    /// <returns>The interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Lerp(T value1, T value2, T amount) => amount * (value2 - value1) + value1;

    /// <summary>Linearly interpolates between two values.</summary>
    /// <param name="value1">The source value.</param>
    /// <param name="value2">The destination value.</param>
    /// <param name="amount">The amount to interpolate between the values.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>This clamps <paramref name="amount"/> before performing the linear interpolation.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T LerpClamped(T value1, T value2, T amount)
    {
        amount = T.Clamp(amount, T.Zero, T.One);
        return Lerp(value1, value2, amount);
    }

    /// <summary>Converts degrees to radians.</summary>
    /// <param name="degrees">The angle, in degrees.</param>
    /// <returns>The angle, in radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T DegreesToRadians(T degrees) => degrees * DegToRad;

    /// <summary>Converts radians to degrees.</summary>
    /// <param name="radians">The angle, in radians.</param>
    /// <returns>The angle, in degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T RadiansToDegrees(T radians) => radians * RadToDeg;
}

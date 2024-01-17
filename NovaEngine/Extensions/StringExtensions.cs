namespace NovaEngine.Extensions;

/// <summary>Extension methods for <see langword="string"/>.</summary>
public static class StringExtensions
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Returns a new string where the substring specified by <paramref name="startIndex"/> and <paramref name="length"/> is replaced with <paramref name="newValue"/>.</summary>
    /// <param name="value">The string to perform the substring replace on.</param>
    /// <param name="startIndex">The start index of the substring to replace with <paramref name="newValue"/>.</param>
    /// <param name="length">The length of the substring to replace with <paramref name="newValue"/>.</param>
    /// <param name="newValue">The value that should replace the substring specified with <paramref name="startIndex"/> and <paramref name="length"/>.</param>
    /// <returns>A new string where the substring specified by <paramref name="startIndex"/> and <paramref name="length"/> has been replaced with <paramref name="newValue"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> or <paramref name="newValue"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="length"/> is less than zero, or if <paramref name="startIndex"/> + <paramref name="length"/> is longer than the length of <paramref name="value"/>.</exception>
    public static string Replace(this string value, int startIndex, int length, string newValue)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(newValue);

        if (length < 0)
            throw new ArgumentException("Cannot be less than zero.", nameof(length));
        if (startIndex + length > value.Length)
            throw new ArgumentException($"Length of {nameof(value)} cannot be less than {nameof(startIndex)} + {nameof(length)}.");

        return value[..startIndex] + newValue + value[(startIndex + length)..];
    }
}

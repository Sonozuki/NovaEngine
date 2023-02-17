namespace NovaEngine.ContentPipeline.Models.Font;

/// <summary>Represents a signed distance.</summary>
internal sealed class SignedDistance
{
    /*********
    ** Properties
    *********/
    /// <summary>The signed distance.</summary>
    public float Distance { get; set; }

    /// <summary>The dot product of the signed distance.</summary>
    public float Dot { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public SignedDistance()
        : this(float.NegativeInfinity, 1) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="distance">The signed distance.</param>
    /// <param name="dot">The dot product of the signed distance.</param>
    public SignedDistance(float distance, float dot)
    {
        Distance = distance;
        Dot = dot;
    }


    /*********
    ** Operators
    *********/
    /// <summary>Checks if a signed distance is less than another.</summary>
    /// <param name="left">The first signed distance.</param>
    /// <param name="right">The second signed distance.</param>
    /// <returns><see langword="true"/>, if the left signed distance is less than the right; otherwise, <see langword="false"/>.</returns>
    public static bool operator <(SignedDistance left, SignedDistance right) =>
        MathF.Abs(left.Distance) < MathF.Abs(right.Distance) || (MathF.Abs(left.Distance) == MathF.Abs(right.Distance) && left.Dot < right.Dot);

    /// <summary>Checks if a signed distance is greater than another.</summary>
    /// <param name="left">The first signed distance.</param>
    /// <param name="right">The second signed distance.</param>
    /// <returns><see langword="true"/>, if the left signed distance is greater than the right; otherwise, <see langword="false"/>.</returns>
    public static bool operator >(SignedDistance left, SignedDistance right) =>
        MathF.Abs(left.Distance) > MathF.Abs(right.Distance) || (MathF.Abs(left.Distance) == MathF.Abs(right.Distance) && left.Dot > right.Dot);
}

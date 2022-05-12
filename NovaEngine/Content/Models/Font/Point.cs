namespace NovaEngine.Content.Models.Font;

/// <summary>Represents a point that makes up a segment.</summary>
internal class Point
{
    /*********
    ** Accessors
    *********/
    /// <summary>The X coordinate of the point.</summary>
    public int X { get; set; }

    /// <summary>The Y coordinate of the point.</summary>
    public int Y { get; set; }

    /// <summary>Whether the point is on the curve.</summary>
    /// <remarks>If a point is off-curve, it means it's a control point for a quadratic segment.</remarks>
    public bool IsOnCurve { get; set; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="isOnCurve">Whether the point is on the curve.</param>
    public Point(bool isOnCurve)
    {
        IsOnCurve = isOnCurve;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="x">The X coordinate of the point.</param>
    /// <param name="y">The Y coordinate of the point.</param>
    /// <param name="isOnCurve">Whether the point is on the curve.</param>
    public Point(int x, int y, bool isOnCurve)
    {
        X = x;
        Y = y;
        IsOnCurve = isOnCurve;
    }

    /// <summary>Gets the point as a <see cref="Vector2"/>.</summary>
    /// <returns>The point as a <see cref="Vector2"/>.</returns>
    public Vector2 ToVector2() => new(X, Y);
}

﻿namespace NovaEngine.ContentPipeline.Font;

/// <summary>Represents a point that makes up a segment.</summary>
internal sealed class Point
{
    /*********
    ** Properties
    *********/
    /// <summary>The X coordinate of the point.</summary>
    public int X { get; }

    /// <summary>The Y coordinate of the point.</summary>
    public int Y { get; }

    /// <summary>Whether the point is on the curve.</summary>
    /// <remarks>If a point is off-curve, it means it's a control point for a quadratic segment.</remarks>
    public bool IsOnCurve { get; }


    /*********
    ** Constructors
    *********/
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


    /*********
    ** Operators
    *********/
    /// <summary>Converts a point to a vector.</summary>
    /// <param name="point">The point to convert.</param>
    public static implicit operator Vector2<float>(Point point) => new(point.X, point.Y);
}

namespace NovaEngine.Content.Models.Font.EdgeSegments;

/// <summary>Represents an edge segment that makes up a contour.</summary>
internal abstract class EdgeSegmentBase
{
    /*********
    ** Accessors
    *********/
    /// <summary>The points in the edge segment.</summary>
    public abstract Vector2[] Points { get; }

    /// <summary>The colour of the edge.</summary>
    /// <remarks>This is used for atlas generation.</remarks>
    public EdgeColour Colour { get; set; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Calculates the direction of the edge segment.</summary>
    /// <param name="amount">The amount to lerp the direction by (this is only useful on quadratic segments where the direction at the start can be different from the direction at the end).</param>
    /// <returns>The direction of the segment at the specified amount.</returns>
    public abstract Vector2 Direction(float amount);

    /// <summary>Linearly interpolates between the segment points.</summary>
    /// <param name="amount">The amount to interpolate between the points by.</param>
    /// <returns>The interpolated value.</returns>
    public abstract Vector2 Lerp(float amount);

    /// <summary>Gets the signed distance from a point to the segment.</summary>
    /// <param name="point">The point to get the signed distance for.</param>
    /// <param name="t">How far along the segment, the point is. Between 0 and 1 means it's closest to somewhere on the segment, &lt;0 means the closest point is somewhere on the segment if it carried on before the first point, and &gt;1 means the closest point is somewhere on the segment if it carried on after the last point.</param>
    /// <returns>The signed distance from the point to the segment.</returns>
    public abstract SignedDistance SignedDistance(Vector2 point, out float t);

    /// <summary>Splits the edge segment into three new edge segments.</summary>
    /// <param name="part1">The first new edge segment.</param>
    /// <param name="part2">The second new edge segment.</param>
    /// <param name="part3">The third new edge segment.</param>
    /// <remarks>This is used to increase the number of edge segments when a contour only has one corner for edge colouring.</remarks>
    public abstract void SplitIntoThree(out EdgeSegmentBase part1, out EdgeSegmentBase part2, out EdgeSegmentBase part3);

    /// <summary>Converts a signed distance to a signed pseudo-distance.</summary>
    /// <param name="distance">The distance to convert.</param>
    /// <param name="point">The point to get the signed pseudo-distance for.</param>
    /// <param name="t">How far along the segment the point is.</param>
    public void DistanceToPseudoDistance(SignedDistance distance, Vector2 point, float t)
    {
        if (t < 0)
        {
            var direction = Direction(0).Normalised;
            var apDirection = point - Lerp(0);

            var ts = Vector2.Dot(apDirection, direction);
            if (ts < 0)
            {
                var pseudoDistance = apDirection.X * direction.Y - apDirection.Y * direction.X;
                if (MathF.Abs(pseudoDistance) <= MathF.Abs(distance.Distance))
                {
                    distance.Distance = pseudoDistance;
                    distance.Dot = 0;
                }
            }
        }
        else if (t > 1)
        {
            var direction = Direction(1).Normalised;
            var bpDirection = point - Lerp(1);

            var ts = Vector2.Dot(bpDirection, direction);
            if (ts > 0)
            {
                var pseudoDistance = bpDirection.X * direction.Y - bpDirection.Y * direction.X;
                if (MathF.Abs(pseudoDistance) <= MathF.Abs(distance.Distance))
                {
                    distance.Distance = pseudoDistance;
                    distance.Dot = 0;
                }
            }
        }
    }
}

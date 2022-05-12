namespace NovaEngine.Content.Models.Font.EdgeSegments;

/// <summary>Represents a linear edge segment.</summary>
internal class LinearSegment : EdgeSegmentBase
{
    /*********
    ** Accessors
    *********/
    /// <inheritdoc/>
    public override Vector2[] Points { get; } = new Vector2[2];


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="point0">The first point making up the segment.</param>
    /// <param name="point1">The second point making up the segment.</param>
    public LinearSegment(Vector2 point0, Vector2 point1)
    {
        Points[0] = point0;
        Points[1] = point1;
    }

    /// <inheritdoc/>
    public override Vector2 Direction(float amount) => Points[1] - Points[0];

    /// <inheritdoc/>
    public override Vector2 Lerp(float amount) => Vector2.Lerp(Points[0], Points[1], amount);

    /// <inheritdoc/>
    public override SignedDistance SignedDistance(Vector2 point, out float t)
    {
        var apDirection = point - Points[0];
        var abDirection = Points[1] - Points[0];
        t = Vector2.Dot(apDirection, abDirection) / Vector2.Dot(abDirection, abDirection);

        var endpointPDirection = Points[Convert.ToInt32(t > .5f)] - point; // direction from the closest endpoint to the point being sampled
        var endpointDistance = endpointPDirection.Length;

        if (t > 0 && t < 1)
        {
            // the point is closest to somewhere in the segment
            var orthonormalDistance = Vector2.Dot(abDirection.PerpendicularRight.Normalised, apDirection);
            if (MathF.Abs(orthonormalDistance) < endpointDistance)
                return new(orthonormalDistance, 0);
        }

        // the point is closest to one of the end points
        return new(
            distance: apDirection.X * abDirection.Y - apDirection.Y * abDirection.X < 0 ? -endpointDistance : endpointDistance, // convert the unsigned endpointDistance to signed by using the cross product
            dot: MathF.Abs(Vector2.Dot(abDirection.Normalised, endpointPDirection.Normalised)));
    }

    /// <inheritdoc/>
    public override void SplitIntoThree(out EdgeSegmentBase part1, out EdgeSegmentBase part2, out EdgeSegmentBase part3)
    {
        var oneThird = Vector2.Lerp(Points[0], Points[1], 1 / 3f);
        var twoThirds = Vector2.Lerp(Points[0], Points[1], 2 / 3f);

        part1 = new LinearSegment(Points[0], oneThird);
        part2 = new LinearSegment(oneThird, twoThirds);
        part3 = new LinearSegment(twoThirds, Points[1]);
    }
}

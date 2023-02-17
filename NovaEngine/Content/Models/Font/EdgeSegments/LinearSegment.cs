namespace NovaEngine.Content.Models.Font.EdgeSegments;

/// <summary>Represents a linear edge segment.</summary>
internal sealed class LinearSegment : EdgeSegmentBase
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public override Vector2<float>[] Points { get; } = new Vector2<float>[2];


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="point0">The first point making up the segment.</param>
    /// <param name="point1">The second point making up the segment.</param>
    public LinearSegment(Vector2<float> point0, Vector2<float> point1)
    {
        Points[0] = point0;
        Points[1] = point1;
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override Vector2<float> Direction(float amount) => Points[1] - Points[0];

    /// <inheritdoc/>
    public override Vector2<float> Lerp(float amount) => Vector2<float>.Lerp(Points[0], Points[1], amount);

    /// <inheritdoc/>
    public override SignedDistance SignedDistance(Vector2<float> point, out float t)
    {
        var apDirection = point - Points[0];
        var abDirection = Points[1] - Points[0];
        t = Vector2<float>.Dot(apDirection, abDirection) / Vector2<float>.Dot(abDirection, abDirection);

        var endpointPDirection = Points[Convert.ToInt32(t > .5f)] - point; // direction from the closest endpoint to the point being sampled
        var endpointDistance = endpointPDirection.Length;

        if (t > 0 && t < 1)
        {
            // the point is closest to somewhere in the segment
            var orthonormalDistance = Vector2<float>.Dot(abDirection.PerpendicularRight.Normalised, apDirection);
            if (MathF.Abs(orthonormalDistance) < endpointDistance)
                return new(orthonormalDistance, 0);
        }

        // the point is closest to one of the end points
        return new(
            distance: apDirection.X * abDirection.Y - apDirection.Y * abDirection.X < 0 ? -endpointDistance : endpointDistance, // convert the unsigned endpointDistance to signed by using the cross product
            dot: MathF.Abs(Vector2<float>.Dot(abDirection.Normalised, endpointPDirection.Normalised)));
    }

    /// <inheritdoc/>
    public override void SplitIntoThree(out EdgeSegmentBase part1, out EdgeSegmentBase part2, out EdgeSegmentBase part3)
    {
        var oneThird = Vector2<float>.Lerp(Points[0], Points[1], 1 / 3f);
        var twoThirds = Vector2<float>.Lerp(Points[0], Points[1], 2 / 3f);

        part1 = new LinearSegment(Points[0], oneThird);
        part2 = new LinearSegment(oneThird, twoThirds);
        part3 = new LinearSegment(twoThirds, Points[1]);
    }
}

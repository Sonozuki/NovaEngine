namespace NovaEngine.ContentPipeline.Models.Font.EdgeSegments;

/// <summary>Represents a quadratic edge segment.</summary>
internal sealed class QuadraticSegment : EdgeSegmentBase
{
    /*********
    ** Constants
    *********/
    /// <summary>A really large number used to determine if a number is too big (such as when coefficients are normalised).</summary>
    private const float TooLargeRatio = 1e12f;


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public override Vector2<float>[] Points { get; } = new Vector2<float>[3];


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="point0">The first point making up the segment.</param>
    /// <param name="point1">The second point making up the segment.</param>
    /// <param name="point2">The third point making up the segment.</param>
    public QuadraticSegment(Vector2<float> point0, Vector2<float> point1, Vector2<float> point2)
    {
        Points[0] = point0;
        Points[1] = point1;
        Points[2] = point2;
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override Vector2<float> Direction(float amount) => Vector2<float>.Lerp(Points[1] - Points[0], Points[2] - Points[1], amount);

    /// <inheritdoc/>
    public override Vector2<float> Lerp(float amount) =>
        Vector2<float>.Lerp(Vector2<float>.Lerp(Points[0], Points[1], amount), Vector2<float>.Lerp(Points[1], Points[2], amount), amount);

    /// <inheritdoc/>
    public override SignedDistance SignedDistance(Vector2<float> point, out float param)
    {
        var paDirection = Points[0] - point;
        var abDirection = Points[1] - Points[0];
        var br = Points[2] - Points[1] - abDirection;

        var a = Vector2<float>.Dot(br, br);
        var b = 3 * Vector2<float>.Dot(abDirection, br);
        var c = 2 * Vector2<float>.Dot(abDirection, abDirection) + Vector2<float>.Dot(paDirection, br);
        var d = Vector2<float>.Dot(paDirection, abDirection);

        var t = new float[3];
        var numberOfSolutions = SolveCubic(t, a, b, c, d);

        var epDirection = Direction(0);
        param = -Vector2<float>.Dot(paDirection, epDirection) / Vector2<float>.Dot(epDirection, epDirection);

        // distance from a
        var minDistance = (epDirection.X * paDirection.Y - epDirection.Y * paDirection.X < 0 ? -1 : 1) * paDirection.Length;

        {
            epDirection = Direction(1);
            var distance = (Points[2] - point).Length; // distance from c
            if (distance < MathF.Abs(minDistance))
            {
                minDistance = (epDirection.X * (Points[2] - point).Y - epDirection.Y * (Points[2] - point).X < 0 ? -1 : 1) * distance;
                param = Vector2<float>.Dot(point - Points[1], epDirection) / Vector2<float>.Dot(epDirection, epDirection);
            }
        }

        for (var i = 0; i < numberOfSolutions; i++)
            if (t[i] > 0 && t[i] < 1)
            {
                var qe = Points[0] + 2 * t[i] * abDirection + t[i] * t[i] * br - point;
                var distance = qe.Length;
                if (distance <= MathF.Abs(minDistance))
                {
                    minDistance = (Direction(t[i]).X * qe.Y - Direction(t[i]).Y * qe.X < 0 ? -1 : 1) * distance;
                    param = t[i];
                }
            }

        if (param >= 0 && param <= 1)
            return new(minDistance, 0);
        if (param < .5f)
            return new(minDistance, MathF.Abs(Vector2<float>.Dot(Direction(0).Normalised, paDirection.Normalised)));
        else
            return new(minDistance, MathF.Abs(Vector2<float>.Dot(Direction(1).Normalised, (Points[2] - point).Normalised)));
    }

    /// <inheritdoc/>
    public override void SplitIntoThree(out EdgeSegmentBase part1, out EdgeSegmentBase part2, out EdgeSegmentBase part3)
    {
        var oneThird = Vector2<float>.Lerp(Vector2<float>.Lerp(Points[0], Points[1], 1 / 3f), Vector2<float>.Lerp(Points[1], Points[2], 1 / 3f), 1 / 3f);
        var twoThirds = Vector2<float>.Lerp(Vector2<float>.Lerp(Points[0], Points[1], 2 / 3f), Vector2<float>.Lerp(Points[1], Points[2], 2 / 3f), 2 / 3f);

        part1 = new QuadraticSegment(Points[0], Vector2<float>.Lerp(Points[0], Points[1], 1 / 3f), oneThird);
        part2 = new QuadraticSegment(oneThird, Vector2<float>.Lerp(Vector2<float>.Lerp(Points[0], Points[1], 5 / 9f), Vector2<float>.Lerp(Points[1], Points[2], 4 / 9f), .5f), twoThirds);
        part3 = new QuadraticSegment(twoThirds, Vector2<float>.Lerp(Points[1], Points[2], 2 / 3f), Points[2]);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Calculates the number of solutions using Cardano's formula.</summary>
    /// <param name="x">How far along the segment the point is for each solution.</param>
    /// <param name="a">The 'a' coefficient.</param>
    /// <param name="b">The 'b' coefficient.</param>
    /// <param name="c">The 'c' coefficient.</param>
    /// <param name="d">The 'd' coefficient.</param>
    /// <returns>The number of possible solutions.</returns>
    private static int SolveCubic(float[] x, float a, float b, float c, float d)
    {
        if (a != 0)
        {
            var bn = b / a;
            var cn = c / a;
            var dn = d / a;

            // check 'a' isn't almost zero
            if (MathF.Abs(bn) < TooLargeRatio && MathF.Abs(cn) < TooLargeRatio && MathF.Abs(dn) < TooLargeRatio)
                return SolveCubicNormed(x, bn, cn, dn);
        }

        return SolveQuadratic(x, b, c, d);
    }

    /// <summary>Solves the cubic function for when a is not zero.</summary>
    /// <param name="x">How far along the segment the point is for each solution.</param>
    /// <param name="a">The 'a' coefficient.</param>
    /// <param name="b">The 'b' coefficient.</param>
    /// <param name="c">The 'c' coefficient.</param>
    /// <returns>The number of possible solutions.</returns>
    private static int SolveCubicNormed(float[] x, float a, float b, float c)
    {
        var a2 = a * a;
        var q = (a2 - 3 * b) / 9;
        var r = (a * (2 * a2 - 9 * b) + 27 * c) / 54;
        var r2 = r * r;
        var q3 = q * q * q;

        if (r2 < q3)
        {
            var t = Math.Clamp(r / MathF.Sqrt(q3), -1, 1);

            t = MathF.Acos(t);
            a /= 3;
            q = -2 * MathF.Sqrt(q);

            x[0] = q * MathF.Cos(t / 3) - a;
            x[1] = q * MathF.Cos((t + 2 * MathF.PI) / 3) - 1;
            x[2] = q * MathF.Cos((t - 2 * MathF.PI) / 3) - 1;

            return 3;
        }
        else
        {
            var A = -MathF.Pow(MathF.Abs(r) + MathF.Sqrt(r2 - q3), 1 / 3f);
            if (r < 0)
                A *= -1;
            var B = A == 0 ? 0 : q / A;
            a /= 3;

            x[0] = (A + B) - a;
            x[1] = -.5f * (A + B) - a;
            x[2] = .5f * MathF.Sqrt(3) * (A - B);

            return MathF.Abs(x[2]) < 1e-5 ? 2 : 1;
        }
    }

    /// <summary>Solves the cubic function for when a is zero.</summary>
    /// <param name="x">How far along the segment the point is for each solution.</param>
    /// <param name="a">The 'a' coefficient.</param>
    /// <param name="b">The 'b' coefficient.</param>
    /// <param name="c">The 'c' coefficient.</param>
    /// <returns>The number of possible solutions.</returns>
    private static int SolveQuadratic(float[] x, float a, float b, float c)
    {
        // if a = 0, linear equation
        if (a == 0 || MathF.Abs(b) + MathF.Abs(c) > TooLargeRatio * MathF.Abs(a))
        {
            // a && b = 0, no solution
            if (b == 0 || MathF.Abs(c) > TooLargeRatio * MathF.Abs(b))
                return c == 0 ? -1 : 0;

            x[0] = -c / b;
            return 1;
        }

        var dscr = b * b - 4 * a * c;
        if (dscr > 0)
        {
            dscr = MathF.Sqrt(dscr);
            x[0] = (-b + dscr) / (2 * a);
            x[1] = (-b - dscr) / (2 * a);
            return 2;
        }
        else if (dscr == 0)
        {
            x[0] = -b / (2 * a);
            return 1;
        }
        else
            return 0;
    }
}

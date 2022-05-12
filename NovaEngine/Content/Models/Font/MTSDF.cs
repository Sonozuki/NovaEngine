using NovaEngine.Content.Models.Font.EdgeSegments;

namespace NovaEngine.Content.Models.Font;

/// <summary>Contains utility methods for the multi-channel true signed distance field (MTSDF) implementation.</summary>
/// <remarks>This is based off of <see href="https://github.com/Chlumsky/msdfgen">https://github.com/Chlumsky/msdfgen</see> and the accompanying paper <see href="https://github.com/Chlumsky/msdfgen/files/3050967/thesis.pdf">https://github.com/Chlumsky/msdfgen/files/3050967/thesis.pdf</see>.</remarks>
internal static class MTSDF
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Colours the edges of a glyph.</summary>
    /// <param name="glyph">The glyph to calculate the edge colours of.</param>
    /// <param name="angleThreshold">The angle threshold to determine how a corner is defined.</param>
    public static void ColourEdges(Glyph glyph, float angleThreshold = 3)
    {
        var sinThreshold = MathF.Sin(angleThreshold);
        var corners = new List<int>();

        foreach (var contour in glyph.Contours)
        {
            // identify corners
            corners.Clear();
            if (contour.Edges.Any())
            {
                var previousDirection = contour.Edges.Last().Direction(1); // get direction from final point to first point
                var index = 0;
                foreach (var edge in contour.Edges)
                {
                    if (IsCorner(previousDirection.Normalised, edge.Direction(0).Normalised, sinThreshold))
                        corners.Add(index++);
                    previousDirection = edge.Direction(1);
                }
            }

            // smooth contour
            if (!corners.Any())
                foreach (var edge in contour.Edges)
                    edge.Colour = EdgeColour.White;

            // 'tear drop' case
            else if (corners.Count == 1)
            {
                var colours = new[] { EdgeColour.White, EdgeColour.White, EdgeColour.White };

                colours[0] = SwitchColour(colours[0]);
                colours[2] = SwitchColour(colours[0]);
                var corner = corners[0];

                if (contour.Edges.Count >= 3)
                {
                    var numberOfEdges = contour.Edges.Count;
                    for (int i = 0; i < numberOfEdges; i++)
                        contour.Edges[(corner + i) & numberOfEdges].Colour = colours[(int)(3 + 2.875f * i / (numberOfEdges - 1) - 1.4375f + .5f) - 2];
                }
                else if (contour.Edges.Count >= 1)
                {
                    // less that three edge segments for three colours, so edges need to be split
                    var parts = new EdgeSegmentBase[7];
                    contour.Edges[0].SplitIntoThree(out parts[0 + 3 * corner], out parts[1 + 3 * corner], out parts[2 + 3 * corner]);
                    if (contour.Edges.Count >= 2)
                    {
                        contour.Edges[1].SplitIntoThree(out parts[3 - 3 * corner], out parts[4 - 3 * corner], out parts[5 - 3 * corner]);
                        parts[0].Colour = parts[1].Colour = colours[0];
                        parts[2].Colour = parts[3].Colour = colours[1];
                        parts[4].Colour = parts[5].Colour = colours[2];
                    }
                    else
                    {
                        parts[0].Colour = colours[0];
                        parts[1].Colour = colours[1];
                        parts[2].Colour = colours[2];
                    }

                    contour.Edges.Clear();
                    foreach (var part in parts)
                        if (part != null)
                            contour.Edges.Add(part);
                }
            }

            // multiple corners
            else
            {
                var spline = 0;
                var startIndex = corners[0];
                var colour = SwitchColour(EdgeColour.White);
                var initialColour = colour;

                for (int i = 0; i < contour.Edges.Count; i++)
                {
                    var index = (startIndex + i) % contour.Edges.Count;
                    if (spline + 1 < corners.Count && corners[spline + 1] == index)
                    {
                        spline++;
                        colour = SwitchColour(colour, spline == corners.Count - 1 ? initialColour : EdgeColour.Black); // ban the initial colour on the last edge as they touch which would cause them to be the same colour
                    }

                    contour.Edges[index].Colour = colour;
                }
            }
        }
    }

    // TODO: calculate the frame and texture size based off of the glyph size automatically
    /// <summary>Generates the MTSDF texture of a glyph.</summary>
    /// <param name="glyph">The glyph to calculate the texture of.</param>
    /// <returns>The MTSDF texture of the glyph.</returns>
    public static Colour128[,] GenerateMTSDF(Glyph glyph)
    {
        // bounds
        var points = glyph.Contours.SelectMany(contour => contour.Edges).SelectMany(edge => edge.Points);
        var xMin = points.Min(point => point.X);
        var yMin = points.Min(point => point.Y);
        var xMax = points.Max(point => point.X);
        var yMax = points.Max(point => point.Y);
        var bounds = new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);

        var frame = new Vector2(500, 500);

        var scale = 1f;
        var translate = new Vector2();
        if (bounds.Width * frame.Y < bounds.Height * frame.X)
        {
            scale = frame.Y / bounds.Height;
            translate = new(.5f * (frame.X / frame.Y * bounds.Height - bounds.Width) - bounds.X, -bounds.Y);
        }
        else
        {
            scale = frame.X / bounds.Width;
            translate = new(-bounds.X, .5f * (frame.Y / frame.X * bounds.Width - bounds.Height) - bounds.Y);
        }
        translate += 1 / scale;

        var range = 10 / scale;

        // create texture
        var pixels = new Colour128[512, 512];

        for (int y = 0; y < 512; y++)
            for (int x = 0; x < 512; x++)
            {
                var point = new Vector2(x + .5f, y + .5f) / scale - translate;

                var minDistance = new SignedDistance();
                var r = new Channel();
                var g = new Channel();
                var b = new Channel();

                // calculate the closest edge to the pixel for each channel
                foreach (var edge in glyph.Contours.SelectMany(contour => contour.Edges))
                {
                    var distance = edge.SignedDistance(point, out var param);
                    if (distance < minDistance)
                        minDistance = distance;
                    if (edge.Colour.HasFlag(EdgeColour.Red) && distance < r.MinDistance)
                    {
                        r.MinDistance = distance;
                        r.NearEdge = edge;
                        r.NearParam = param;
                    }
                    if (edge.Colour.HasFlag(EdgeColour.Green) && distance < g.MinDistance)
                    {
                        g.MinDistance = distance;
                        g.NearEdge = edge;
                        g.NearParam = param;
                    }
                    if (edge.Colour.HasFlag(EdgeColour.Blue) && distance < b.MinDistance)
                    {
                        b.MinDistance = distance;
                        b.NearEdge = edge;
                        b.NearParam = param;
                    }
                }

                // calculate the pseudo distances for each channel
                var a = minDistance.Distance;
                if (r.NearEdge != null)
                    r.NearEdge.DistanceToPseudoDistance(r.MinDistance, point, r.NearParam);
                if (g.NearEdge != null)
                    g.NearEdge.DistanceToPseudoDistance(g.MinDistance, point, g.NearParam);
                if (b.NearEdge != null)
                    b.NearEdge.DistanceToPseudoDistance(b.MinDistance, point, b.NearParam);

                pixels[x, y] = new Colour128(
                    r.MinDistance.Distance / range + .5f,
                    g.MinDistance.Distance / range + .5f,
                    b.MinDistance.Distance / range + .5f,
                    a / range + .5f
                );
            }

        return pixels;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Determines if two vectors are considered a corner.</summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <param name="sinThreshold">The angle threshold to determine how a corner is defined.</param>
    /// <returns><see langword="true"/>, if the vectors are considered a corner; otherwise, <see langword="false"/>.</returns>
    private static bool IsCorner(Vector2 a, Vector2 b, float sinThreshold) => Vector2.Dot(a, b) <= 0 || Math.Abs(a.X * b.Y - a.Y * b.X) > sinThreshold;

    /// <summary>Calculates the colour of the next edge.</summary>
    /// <param name="colour">The colour of the current edge.</param>
    /// <param name="bannedColour">The colour that the next edge colour cannot share any channels with.</param>
    /// <returns>The colour of the next edge.</returns>
    private static EdgeColour SwitchColour(EdgeColour colour, EdgeColour bannedColour = EdgeColour.Black)
    {
        // create colour based off banned channels (if a banned colour was specified)
        var combinedColour = colour & bannedColour;
        if (combinedColour == EdgeColour.Red || combinedColour == EdgeColour.Green || combinedColour == EdgeColour.Blue)
            return combinedColour ^ EdgeColour.White;

        // return cyan if an unshiftable colour was passed (unshiftable in that shifting it would result in the same colour)
        if (colour == EdgeColour.Black || colour == EdgeColour.White)
            return EdgeColour.Cyan;

        // shift the colour
        var shifted = (int)colour << 1;
        var wrapped = shifted | shifted >> 3; // wrap any overflow of the 3 channel bits
        return (EdgeColour)wrapped & EdgeColour.White;
    }
}

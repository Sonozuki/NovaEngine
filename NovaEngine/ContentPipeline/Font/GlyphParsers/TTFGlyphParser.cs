using NovaEngine.ContentPipeline.Font.Tables;
using NovaEngine.ContentPipeline.Font.EdgeSegments;

namespace NovaEngine.ContentPipeline.Font.GlyphParsers;

/// <summary>Represents a TrueType outline parser.</summary>
internal sealed class TTFGlyphParser : GlyphParserBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The binary reader to parse the glyphs from.</summary>
    private readonly BinaryReader BinaryReader;

    /// <summary>The 'loca' table read from the font.</summary>
    private readonly LocaTable LocaTable;

    /// <summary>The offset of the 'glyf' table.</summary>
    private readonly uint GlyfTableOffset;

    /// <summary>The cached glyphs.</summary>
    private readonly Dictionary<ushort, Glyph> ParsedGlyphs = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">The binary reader to parse the glyphs from.</param>
    /// <param name="locaTable">The 'loca' table read from the font.</param>
    /// <param name="glyfTableOffset">The offset of the 'glyf' table.</param>
    public TTFGlyphParser(BinaryReader binaryReader, LocaTable locaTable, uint glyfTableOffset)
    {
        BinaryReader = binaryReader;
        LocaTable = locaTable;
        GlyfTableOffset = glyfTableOffset;
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override Glyph Parse(ushort glyphIndex)
    {
        if (ParsedGlyphs.TryGetValue(glyphIndex, out var glyph))
            return glyph;

        BinaryReader.BaseStream.Position = GlyfTableOffset + LocaTable.GlyphOffsets[glyphIndex];

        glyph = new Glyph(BinaryReader);
        if (glyph.IsComposite)
            ParseCompositeGlyph(glyph);
        else
            ParseSimpleGlyph(glyph);

        CalculateGlyphContours(glyph);

        ParsedGlyphs[glyphIndex] = glyph;
        return glyph;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Parses a composite glyph's contour ends and points.</summary>
    /// <param name="glyph">The glyph to parse contour ends and points for.</param>
    /// <remarks>This assumes <see cref="BinaryReader"/> has had its position set for the glyph to be read.</remarks>
    private void ParseCompositeGlyph(Glyph glyph)
    {
        var contourEnds = new List<ushort>();
        var points = new List<Point>();

        CompositeGlyphFlags flags;

        do
        {
            flags = (CompositeGlyphFlags)BinaryReader.ReadUInt16BigEndian();
            var component = new CompositeGlyphComponent(glyphIndex: BinaryReader.ReadUInt16BigEndian());

            // read scale and position of the component glyph
            short arg1;
            short arg2;
            if (flags.HasFlag(CompositeGlyphFlags.Arg1And2AreWords))
            {
                arg1 = BinaryReader.ReadInt16BigEndian();
                arg2 = BinaryReader.ReadInt16BigEndian();
            }
            else
            {
                arg1 = BinaryReader.ReadSByte();
                arg2 = BinaryReader.ReadSByte();
            }

            if (flags.HasFlag(CompositeGlyphFlags.ArgsAreXYValues))
            {
                component.Matrix.M31 = arg1;
                component.Matrix.M32 = arg2;
            }
            else
                throw new NotImplementedException();

            if (flags.HasFlag(CompositeGlyphFlags.WeHaveAScale))
            {
                component.Matrix.M22 = component.Matrix.M11 = BinaryReader.Read2Dot14();
            }
            else if (flags.HasFlag(CompositeGlyphFlags.WeHaveAnXAndYScale))
            {
                component.Matrix.M11 = BinaryReader.Read2Dot14();
                component.Matrix.M22 = BinaryReader.Read2Dot14();
            }
            else if (flags.HasFlag(CompositeGlyphFlags.WeHaveATwoByTwo))
            {
                component.Matrix.M11 = BinaryReader.Read2Dot14();
                component.Matrix.M12 = BinaryReader.Read2Dot14();
                component.Matrix.M21 = BinaryReader.Read2Dot14();
                component.Matrix.M22 = BinaryReader.Read2Dot14();
            }

            var oldPosition = BinaryReader.BaseStream.Position;
            var componentGlyph = Parse(component.GlyphIndex);
            BinaryReader.BaseStream.Position = oldPosition;

            // merge the component glyph's contour ends into this glyphs's contour ends
            var contourEndOffset = points.Count;
            for (var i = 0; i < componentGlyph.ContourEnds.Length; i++)
                contourEnds.Add((ushort)(componentGlyph.ContourEnds[i] + contourEndOffset));

            // merge the component glyph's points into this glyphs's points
            for (var i = 0; i < componentGlyph.Points.Length; i++)
            {
                var point = componentGlyph.Points[i];

                var x = (int)(component.Matrix.M11 * point.X + component.Matrix.M12 * point.Y + component.Matrix.M31);
                var y = (int)(component.Matrix.M21 * point.X + component.Matrix.M22 * point.Y + component.Matrix.M32);
                points.Add(new(x, y, point.IsOnCurve));
            }
        }
        while (flags.HasFlag(CompositeGlyphFlags.MoreComponents));

        glyph.ContourEnds = contourEnds.ToImmutableArray();
        glyph.Points = points.ToImmutableArray();
    }

    /// <summary>Parses a simple glyph's contour ends and points.</summary>
    /// <param name="glyph">The glyph to parse contour ends and points for.</param>
    /// <remarks>This assumes <see cref="BinaryReader"/> has had its position set for the glyph to be read.</remarks>
    private void ParseSimpleGlyph(Glyph glyph)
    {
        if (glyph.NumberOfContours == 0)
            return;

        var contourEnds = new List<ushort>();
        for (var i = 0; i < glyph.NumberOfContours; i++)
            contourEnds.Add(BinaryReader.ReadUInt16BigEndian());
        glyph.ContourEnds = contourEnds.ToImmutableArray();

        // skip byte code instructions
        var byteCodeLength = BinaryReader.ReadUInt16BigEndian();
        BinaryReader.BaseStream.Position += byteCodeLength;

        // read oncurve values
        var numberOfPoints = contourEnds.Last() + 1;

        var flags = new SimpleGlyphFlags[numberOfPoints];
        var pointIsOnCurves = new bool[numberOfPoints];

        for (var i = 0; i < numberOfPoints; i++)
        {
            var flag = (SimpleGlyphFlags)BinaryReader.ReadByte();
            flags[i] = flag;
            pointIsOnCurves[i] = flag.HasFlag(SimpleGlyphFlags.OnCurve);

            if (flag.HasFlag(SimpleGlyphFlags.Repeat))
            {
                var repeatCount = BinaryReader.ReadByte();
                for (var j = 1; j <= repeatCount; j++)
                {
                    flags[i + j] = flag;
                    pointIsOnCurves[i + j] = flag.HasFlag(SimpleGlyphFlags.OnCurve);
                }
                i += repeatCount;
            }
        }

        // read coordinate values and pack into points
        var pointXCoordinates = ReadCoordinates(SimpleGlyphFlags.XShortVector, SimpleGlyphFlags.XIsSameOrPositiveXShortVector);
        var pointYCoordinates = ReadCoordinates(SimpleGlyphFlags.YShortVector, SimpleGlyphFlags.YIsSameOrPositiveYShortVector);

        var points = new List<Point>();
        for (var i = 0; i < numberOfPoints; i++)
            points.Add(new(pointXCoordinates[i], pointYCoordinates[i], pointIsOnCurves[i]));
        glyph.Points = points.ToImmutableArray();

        // Sets a coordinate of the points that make up the glyph.
        int[] ReadCoordinates(SimpleGlyphFlags coordinateIsByteFlag, SimpleGlyphFlags coordinateIsSameOrPositiveFlag)
        {
            var coordinates = new int[numberOfPoints];
            var value = 0;

            for (var i = 0; i < numberOfPoints; i++)
            {
                var flag = flags[i];
                if (flag.HasFlag(coordinateIsByteFlag))
                {
                    if (flag.HasFlag(coordinateIsSameOrPositiveFlag))
                        value += BinaryReader.ReadByte();
                    else
                        value -= BinaryReader.ReadByte();
                }
                else if ((~flag).HasFlag(coordinateIsSameOrPositiveFlag))
                    value += BinaryReader.ReadInt16BigEndian();

                coordinates[i] = value;
            }

            return coordinates;
        }
    }

    /// <summary>Calculates a glyph's contours.</summary>
    /// <param name="glyph">The glyph to calculate the contours of.</param>
    private void CalculateGlyphContours(Glyph glyph)
    {
        // calculate implicit on-curve points and correct contour ends (corrected to accomodate for the implicit points now be explicit)
        var glyphPoints = new List<Point>();
        var glyphContourEnds = new List<int>();
        for (var i = 0; i < glyph.Points.Length; i++)
        {
            var point = glyph.Points[i];
            if (i != 0)
            {
                var previousPoint = glyphPoints.Last();
                // if this point and the previous point are both off-curve points, add the implicit on-curve point between them.
                if (!previousPoint.IsOnCurve && !point.IsOnCurve)
                    glyphPoints.Add(new((previousPoint.X + point.X) / 2, (previousPoint.Y + point.Y) / 2, true));
            }
            glyphPoints.Add(point);

            // check for a contour end to add
            if (glyph.ContourEnds.Contains((ushort)i))
                glyphContourEnds.Add(glyphPoints.Count - 1);
        }

        // calculate contours
        var contours = new List<Contour>();
        var edges = new List<EdgeSegmentBase>();

        var currentContourStartIndex = 0;
        var currentContourEndIndex = 0;

        for (var i = 1; i < glyphPoints.Count; i++)
        {
            var previousPoint = glyphPoints[i - 1];
            var point = glyphPoints[i];

            if (previousPoint.IsOnCurve)
            {
                // if both the previous and current point is on-curve, it's a straight line segment
                if (point.IsOnCurve)
                    edges.Add(new LinearSegment(previousPoint, point));
            }
            else
            {
                // if the previous point is off-curve, it means it's a control point for a quadratic segment, of which this is the final point
                var previousPreviousPoint = glyphPoints[i - 2];
                edges.Add(new QuadraticSegment(previousPreviousPoint, previousPoint, point));
            }

            // check if this is the end of the contour
            if (i == glyphContourEnds[currentContourEndIndex])
            {
                var contourStartPoint = glyphPoints[currentContourStartIndex];
                if (point.IsOnCurve)
                    edges.Add(new LinearSegment(point, contourStartPoint));
                else // if this point is off-curve, it means it's a control point for a quadratric segment that needs to link to the start of the contour
                    edges.Add(new QuadraticSegment(previousPoint, point, contourStartPoint));

                currentContourStartIndex = i + 1;
                currentContourEndIndex++;

                i++; // this is required as the segments are calculated by looking back at the previous point, without this it will look back at a point from a different contour

                contours.Add(new(edges));
                edges = new();
            }
        }

        glyph.Contours = contours.ToImmutableArray();
    }
}

using NovaEngine.Content.Models.Font.Cmap;
using NovaEngine.Content.Models.Font.EdgeSegments;
using NovaEngine.Content.Models.Font.Kern;

namespace NovaEngine.Content.Models.Font;

/// <summary>Represents a true type font.</summary>
internal class TrueTypeFont : IDisposable
{
    /*********
    ** Constants
    *********/
    /// <summary>The max height of the glyphs after being scaled, in pixels.</summary>
    public const float MaxGlyphHeight = 48;


    /*********
    ** Fields
    *********/
    /// <summary>The reader for the font file.</summary>
    private readonly BinaryReader BinaryReader;

    /// <summary>The tables present in the font.</summary>
    private readonly Dictionary<string, Table> Tables = new();

    /// <summary>The character code mappings in the font.</summary>
    private readonly List<ICmapFormat> CMaps = new();

    /// <summary>The kernings in the font.</summary>
    private readonly List<IKernFormat> Kernings = new();

    /// <summary>The number of glyphs in the font.</summary>
    private ushort NumberOfGlyphs;

    /// <summary>Determines whether the font uses short offset or long offsets (0 for short, 1 for long).</summary>
    private short IndexToLocFormat;

    /// <summary>The number of horizontal metric entries in the 'hmtx' table.</summary>
    private ushort NumberOfHorizontalMetrics;

    /// <summary>The horizontal metrics for each glyph index.</summary>
    private readonly Dictionary<ushort, HorizontalMetrics> HorizontalMetrics = new();

    /// <summary>The offsets for each glyph index.</summary>
    private readonly Dictionary<ushort, uint> GlyphOffsets = new();


    /*********
    ** Accessors
    *********/
    /// <summary>The name of the font.</summary>
    public string Name { get; internal set; }

    /// <summary>All the glyphs in the font file.</summary>
    public List<Glyph> Glyphs { get; } = new();


    /*********
    ** Public Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="ttfFile">The path to the ttf file to load.</param>
    public TrueTypeFont(string ttfFile)
    {
        BinaryReader = new BinaryReader(File.OpenRead(ttfFile));

        ReadTables();
        ReadNameTable();
        ReadHeadTable();
        ReadHheaTable();
        ReadCmapTable();
        ReadKernTable();

        ReadGlyphs();
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <inheritdoc/>
    public void Dispose() => BinaryReader.Dispose();


    /*********
    ** Private Methods
    *********/
    /// <summary>Reads the tables in the font file.</summary>
    private void ReadTables()
    {
        BinaryReader.BaseStream.Position = 4;
        var numberOfTables = BinaryReader.ReadUInt16BigEndian();

        BinaryReader.BaseStream.Position = 12;
        for (int i = 0; i < numberOfTables; i++)
            Tables[Encoding.UTF8.GetString(BinaryReader.ReadBytes(4))] =
                new Table(BinaryReader.ReadUInt32BigEndian(), BinaryReader.ReadUInt32BigEndian(), BinaryReader.ReadUInt32BigEndian());

        // TODO: validate tables with checksums
    }

    /// <summary>Reads the content of the name table.</summary>
    /// <exception cref="ContentException">Thrown if the font doesn't contain a "name" table or if the "name" table is invalid.</exception>
    private void ReadNameTable()
    {
        if (!Tables.ContainsKey("name"))
            throw new ContentException("'name' table doesn't exist.");

        var tableOffset = Tables["name"].Offset;
        BinaryReader.BaseStream.Position = tableOffset + 2;

        var numberOfNameRecords = BinaryReader.ReadUInt16BigEndian();
        var stringOffset = BinaryReader.ReadUInt16BigEndian();

        var fontFamilyName = "";
        var fontSubfamilyName = "";
        for (int i = 0; i < numberOfNameRecords; i++)
        {
            var platformId = BinaryReader.ReadUInt16BigEndian();
            BinaryReader.BaseStream.Position += 4;

            // check what this record is corresponding to, and ignore any that aren't relevant (font family, font subfamily)
            var nameId = BinaryReader.ReadUInt16BigEndian();
            if (nameId != 1 && nameId != 2) // 1 = font family name, 2 = font subfamily name
            {
                BinaryReader.BaseStream.Position += 4;
                continue;
            }

            // read name record value
            var length = BinaryReader.ReadUInt16BigEndian();
            var offset = BinaryReader.ReadUInt16BigEndian();

            var oldPosition = BinaryReader.BaseStream.Position;
            BinaryReader.BaseStream.Position = tableOffset + stringOffset + offset;

            var name = "";
            if (platformId == 0 || platformId == 3)
                for (int j = 0; j < length; j += 2) // unicode needs to have endian reversed (as ttf files are big endian)
                {
                    var bytes = BinaryReader.ReadBytes(2);
                    name += new string(Encoding.Unicode.GetChars(new[] { bytes[1], bytes[0] })); // TODO: could do with cleaning up
                }
            else
                name = Encoding.ASCII.GetString(BinaryReader.ReadBytes(length));

            BinaryReader.BaseStream.Position = oldPosition;

            // set respective name
            if (nameId == 1)
                fontFamilyName = name;
            else if (nameId == 2)
                fontSubfamilyName = name;
        }

        // calculate final font name
        Name = $"{fontFamilyName} ({fontSubfamilyName})";
    }

    /// <summary>Reads the content of the font header table.</summary>
    /// <exception cref="ContentException">Thrown if the font doesn't contain a "head" table or if the "head" table is invalid.</exception>
    private void ReadHeadTable()
    {
        if (!Tables.ContainsKey("head"))
            throw new ContentException("'head' table doesn't exist.");

        BinaryReader.BaseStream.Position = Tables["head"].Offset + 12;
        if (BinaryReader.ReadUInt32BigEndian() != 0x5f0f3cf5)
            throw new ContentException("Head table isn't valid.");

        BinaryReader.BaseStream.Position += 34;
        IndexToLocFormat = BinaryReader.ReadInt16BigEndian();
    }

    /// <summary>Reads the content of the horizontal header table.</summary>
    /// <exception cref="ContentException">Thrown if the font doesn't contain a "hhea" table.</exception>
    private void ReadHheaTable()
    {
        if (!Tables.ContainsKey("hhea"))
            throw new ContentException("'hhea' table doesn't exist.");

        BinaryReader.BaseStream.Position = Tables["hhea"].Offset + 34;
        NumberOfHorizontalMetrics = BinaryReader.ReadUInt16BigEndian();
    }

    /// <summary>Reads the content of the character code mapping table.</summary>
    /// <exception cref="ContentException">Thrown if the font doesn't contain a "cmap" table.</exception>
    private void ReadCmapTable()
    {
        if (!Tables.ContainsKey("cmap"))
            throw new ContentException("'cmap' table doesn't exist.");

        BinaryReader.BaseStream.Position = Tables["cmap"].Offset + 2;

        var numberOfSubtables = BinaryReader.ReadUInt16BigEndian();
        for (int i = 0; i < numberOfSubtables; i++)
        {
            var platformId = BinaryReader.ReadUInt16BigEndian();
            var platformSpecificId = BinaryReader.ReadUInt16BigEndian();
            var offset = BinaryReader.ReadUInt32BigEndian();
            if (platformId == 3 && platformSpecificId <= 1)
                ReadCmap(Tables["cmap"].Offset + offset);
        }
    }

    /// <summary>Reads a character code mapping subtable.</summary>
    /// <param name="offset">The offset of the subtable.</param>
    /// <exception cref="ContentException">Thrown if the required cmap format hasn't been implemented.</exception>
    private void ReadCmap(uint offset)
    {
        var oldOffset = BinaryReader.BaseStream.Position;
        BinaryReader.BaseStream.Position = offset;

        // cmap header
        var format = BinaryReader.ReadUInt16BigEndian();
        BinaryReader.BaseStream.Position += 4;

        // cmap content
        switch (format)
        {
            case 0:
                CMaps.Add(new CmapFormat0(BinaryReader));
                break;
            case 4:
                CMaps.Add(new CmapFormat4(BinaryReader));
                break;
            default:
                throw new ContentException($"CMap format: '{format}' is unknown.");
        }

        BinaryReader.BaseStream.Position = oldOffset;
    }

    /// <summary>Reads the content of the kerning table.</summary>
    /// <exception cref="ContentException">Thrown if the required kern format hasn't been implemented.</exception>
    private void ReadKernTable()
    {
        if (!Tables.ContainsKey("kern"))
            return;

        BinaryReader.BaseStream.Position = Tables["kern"].Offset + 2;

        var numberOfSubtables = BinaryReader.ReadUInt16BigEndian();
        for (int i = 0; i < numberOfSubtables; i++)
        {
            // kern header
            BinaryReader.BaseStream.Position += 4;
            var coverage = BinaryReader.ReadUInt16BigEndian();
            var isVertical = (coverage & 0x8000) == 0;
            var hasCrossStream = (coverage & 0x4000) == 0;
            var format = coverage & 0x00FF;

            // kern content
            switch (format)
            {
                case 0:
                    Kernings.Add(new KernFormat0(BinaryReader, isVertical, hasCrossStream));
                    break;
                default:
                    throw new ContentException($"Kerning format: '{format}' is unknown.");
            }
        }
    }

    /// <summary>Reads all the glyphs from the font.</summary>
    private void ReadGlyphs()
    {
        if (!Tables.ContainsKey("glyf"))
            throw new ContentException("'glyf' table doesn't exist.");
        var glyfTable = Tables["glyf"];

        ReadNumberOfGlyphs();
        ReadHorizontalMetrics();
        ReadGlyphOffsets(glyfTable.Offset);

        // read glyphs
        // TODO: currently only reads ASCII glyphs
        {
            var characterMap = CMaps.First();

            var glyph = ReadGlyph(characterMap.Map(BinaryReader, -1), '\0');
            glyph.HorizontalMetrics = HorizontalMetrics[0];
            Glyphs.Add(glyph);

            for (ushort i = 0x21; i < 0x7F; i++)
            {
                var glyphIndex = characterMap.Map(BinaryReader, i);

                glyph = ReadGlyph(characterMap.Map(BinaryReader, i), (char)i);
                glyph.HorizontalMetrics = HorizontalMetrics[glyphIndex];
                Glyphs.Add(glyph);
            }
            NumberOfGlyphs = (ushort)Glyphs.Count;
        }

        CalculateGlyphContours();

        // calculate glyph edge colours
        for (int i = 0; i < NumberOfGlyphs; i++)
            MTSDF.ColourEdges(Glyphs[i]);

        // calculate bounds
        var maxGlyphHeight = -1;
        foreach (var glyph in Glyphs)
        {
            var points = glyph.Contours.SelectMany(contour => contour.Edges).SelectMany(edge => edge.Points);
            var xMin = points.Min(point => point.X);
            var yMin = points.Min(point => point.Y);
            var xMax = points.Max(point => point.X);
            var yMax = points.Max(point => point.Y);
            glyph.UnscaledBounds = new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);

            // record height so glyphs can be correctly scaled in the atlas
            if (glyph.UnscaledBounds.Height > maxGlyphHeight)
                maxGlyphHeight = (int)glyph.UnscaledBounds.Height;
        }

        // scale glyphs
        var scale = maxGlyphHeight / MaxGlyphHeight;
        foreach (var glyph in Glyphs)
        {
            glyph.ScaledBounds.Width = MathF.Ceiling(glyph.UnscaledBounds.Width / scale);
            glyph.ScaledBounds.Height = MathF.Ceiling(glyph.UnscaledBounds.Height / scale);

            glyph.HorizontalMetrics.AdvanceWidth = (ushort)MathF.Round(glyph.HorizontalMetrics.AdvanceWidth / scale);
            glyph.HorizontalMetrics.LeftSideBearing = (ushort)MathF.Round(glyph.HorizontalMetrics.LeftSideBearing / scale);
        }
    }

    /// <summary>Reads the horizontal metrics of glyphs in the font.</summary>
    /// <exception cref="ContentException">Thrown if the font doesn't contain a "hmtx" table.</exception>
    private void ReadHorizontalMetrics()
    {
        if (!Tables.ContainsKey("hmtx"))
            throw new ContentException("'hmtx' table doesn't exist.");
        var hmtxTable = Tables["hmtx"];

        for (ushort i = 0; i < NumberOfGlyphs; i++)
            if (i < NumberOfHorizontalMetrics)
            {
                BinaryReader.BaseStream.Position = hmtxTable.Offset + i * 4;
                HorizontalMetrics[i] = new(advanceWidth: BinaryReader.ReadUInt16BigEndian(), leftSideBearing: BinaryReader.ReadUInt16BigEndian());
            }
            else
            {
                // if the glyph index is higher than the number of horizontal metrics, use the last horizontal metric
                BinaryReader.BaseStream.Position = hmtxTable.Offset + (NumberOfHorizontalMetrics - 1) * 4;
                var advanceWidth = BinaryReader.ReadUInt16BigEndian();

                // get the left side bearing from the lsb array after the final advance width
                BinaryReader.BaseStream.Position = hmtxTable.Offset + NumberOfHorizontalMetrics * 4 + 2 * (i - NumberOfHorizontalMetrics);
                var leftSideBearing = BinaryReader.ReadUInt16BigEndian();

                HorizontalMetrics[i] = new(advanceWidth, leftSideBearing);
            }
    }

    /// <summary>Reads the number of glyphs in the font.</summary>
    /// <remarks>This sets <see cref="NumberOfGlyphs"/>.</remarks>
    /// <exception cref="ContentException">Thrown if the font doesn't contain a "maxp" table.</exception>
    private void ReadNumberOfGlyphs()
    {
        if (!Tables.ContainsKey("maxp"))
            throw new ContentException("'maxp' table doesn't exist.");

        BinaryReader.BaseStream.Position = Tables["maxp"].Offset + 4;
        NumberOfGlyphs = BinaryReader.ReadUInt16BigEndian();
    }

    /// <summary>Reads the offset of each glyph.</summary>
    /// <param name="glyfTableOffset">The offset of the "glyf" table.</param>
    /// <remarks>This sets <see cref="GlyphOffsets"/>.</remarks>
    /// <exception cref="ContentException">Thrown if the font doesn't contain a "loca" table.</exception>
    private void ReadGlyphOffsets(uint glyfTableOffset)
    {
        if (!Tables.ContainsKey("loca"))
            throw new ContentException("'loca' table doesn't exist.");
        var locaTable = Tables["loca"];

        BinaryReader.BaseStream.Position = locaTable.Offset;

        if (IndexToLocFormat == 1)
            for (ushort i = 0; i < NumberOfGlyphs; i++)
                GlyphOffsets[i] = BinaryReader.ReadUInt32BigEndian() + glyfTableOffset;
        else
            for (ushort i = 0; i < NumberOfGlyphs; i++)
                GlyphOffsets[i] = BinaryReader.ReadUInt16BigEndian() * 2u + glyfTableOffset;
    }

    /// <summary>Retrieves the glyph of a specified index.</summary>
    /// <param name="index">The index of the glyph.</param>
    /// <param name="character">The character the glyph represents.</param>
    /// <returns>The glyph.</returns>
    /// <exception cref="ArgumentException">Thrown if the font doesn't contain a "glyf" table, or the glyph offset is invalid, or if the glyph contours are invlaid.</exception>
    private Glyph ReadGlyph(ushort index, char? character = null)
    {
        if (!Tables.ContainsKey("glyf"))
            throw new ArgumentException("'glyf' table doesn't exist.");
        var glyfTable = Tables["glyf"];

        // calculate glyph offset
        var offset = GlyphOffsets[index];
        if (offset >= glyfTable.Offset + glyfTable.Length)
            throw new ContentException("Glyph offset is outside of glyph table.");

        BinaryReader.BaseStream.Position = offset;

        // create glyph
        var glyph = new Glyph(BinaryReader.ReadInt16BigEndian());
        if (glyph.NumberOfContours < -1)
            throw new ContentException($"Number of contours for glyph: {index} is invalid.");

        BinaryReader.BaseStream.Position += 8;

        if (glyph.NumberOfContours == -1)
            ReadCompoundGlyph(glyph);
        else
            ReadSimpleGlyph(glyph);

        if (character != null)
            glyph.Character = character.Value;

        return glyph;
    }

    /// <summary>Calculates the contours that make up each glyph.</summary>
    private void CalculateGlyphContours()
    {
        foreach (var glyph in Glyphs)
        {
            // calculate implicit on-curve points and correct contour ends (corrected to accomodate for the implicit points now be explicit)
            var glyphPoints = new List<Point>();
            var glyphContourEnds = new List<int>();
            for (int i = 0; i < glyph.Points.Count; i++)
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
            var edges = new List<EdgeSegmentBase>();

            var currentContourStartIndex = 0;
            var currentContourEndIndex = 0;

            for (int i = 1; i < glyphPoints.Count; i++)
            {
                var previousPoint = glyphPoints[i - 1];
                var point = glyphPoints[i];

                if (previousPoint.IsOnCurve)
                {
                    // if both the previous and current point is on-curve, it's a straight line segment
                    if (point.IsOnCurve)
                        edges.Add(new LinearSegment(previousPoint.ToVector2(), point.ToVector2()));
                }
                else
                {
                    // if the previous point is off-curve, it means it's a control point for a quadratic segment, of which this is the final point
                    var previousPreviousPoint = glyphPoints[i - 2];
                    edges.Add(new QuadraticSegment(previousPreviousPoint.ToVector2(), previousPoint.ToVector2(), point.ToVector2()));
                }

                // check if this is the end or the contour
                if (i == glyphContourEnds[currentContourEndIndex])
                {
                    var contourStartPoint = glyphPoints[currentContourStartIndex];
                    if (point.IsOnCurve)
                        edges.Add(new LinearSegment(point.ToVector2(), contourStartPoint.ToVector2()));
                    else // if this point is off-curve, it means it's a control point for a quadratric segment that needs to link to the start of the contour
                        edges.Add(new QuadraticSegment(previousPoint.ToVector2(), point.ToVector2(), contourStartPoint.ToVector2()));

                    currentContourStartIndex = i + 1;
                    currentContourEndIndex++;

                    i++; // this is required as the segments are calculated by looking back at the previous point, without this it will look back at a point from a different contour

                    glyph.Contours.Add(new(edges));
                    edges = new();
                }
            }
        }
    }

    /// <summary>Reads a simple glyph to populate its points and contour ends.</summary>
    /// <param name="glyph">The glyph to populate.</param>
    private void ReadSimpleGlyph(Glyph glyph)
    {
        glyph.Flush(); // TODO: instead of flushing and rereading the glyph, just skip it as it already contains all the points

        // read contour ends
        for (int i = 0; i < glyph.NumberOfContours; i++)
            glyph.ContourEnds.Add(BinaryReader.ReadUInt16BigEndian());

        BinaryReader.BaseStream.Position += BinaryReader.ReadInt16BigEndian() + 2; // skip over instructions

        if (glyph.NumberOfContours == 0)
            return;

        // read (non populated) glyph points
        var flags = new List<SimpleGlyphFlags>();

        var numberOfPoints = glyph.ContourEnds.Max() + 1;
        for (int i = 0; i < numberOfPoints; i++)
        {
            var flag = (SimpleGlyphFlags)BinaryReader.ReadByte();
            flags.Add(flag);
            glyph.Points.Add(new(flag.HasFlag(SimpleGlyphFlags.OnCurve)));

            if (flag.HasFlag(SimpleGlyphFlags.Repeat))
            {
                var repeatCount = BinaryReader.ReadByte();
                i += repeatCount;
                for (int j = 0; j < repeatCount; j++)
                {
                    flags.Add(flag);
                    glyph.Points.Add(new(flag.HasFlag(SimpleGlyphFlags.OnCurve)));
                }
            }
        }

        // populate glyph points
        SetCoordinate(true);
        SetCoordinate(false);

        // Sets a coordinate of the points that make up the glyph.
        // setXCoord: Whether the X coordinate should be set (false to set the Y coordinate).
        void SetCoordinate(bool setXCoord)
        {
            var byteFlag = setXCoord ? SimpleGlyphFlags.XIsByte : SimpleGlyphFlags.YIsByte;
            var deltaFlag = setXCoord ? SimpleGlyphFlags.XDelta : SimpleGlyphFlags.YDelta;

            var value = 0;

            for (int i = 0; i < numberOfPoints; i++)
            {
                var flag = flags[i];
                if (flag.HasFlag(byteFlag))
                {
                    if (flag.HasFlag(deltaFlag))
                        value += BinaryReader.ReadByte();
                    else
                        value -= BinaryReader.ReadByte();
                }
                else if ((~flag).HasFlag(deltaFlag))
                    value += BinaryReader.ReadInt16BigEndian();

                if (setXCoord)
                    glyph.Points[i].X = value;
                else
                    glyph.Points[i].Y = value;
            }
        }
    }

    /// <summary>Reads a compound glyph to populate its points and contour ends..</summary>
    /// <param name="glyph">The glyph to populate.</param>
    private void ReadCompoundGlyph(Glyph glyph)
    {
        var flags = CompoundGlyphFlags.MoreComponents;

        glyph.Flush();

        while (flags.HasFlag(CompoundGlyphFlags.MoreComponents))
        {
            flags = (CompoundGlyphFlags)BinaryReader.ReadUInt16BigEndian();
            var component = new CompoundGlyphComponent() { GlyphIndex = BinaryReader.ReadUInt16BigEndian(), Matrix = new Matrix3x2(1, 0, 0, 1, 0, 0) };

            // read scale and position of the component glyph
            short arg1;
            short arg2;
            if (flags.HasFlag(CompoundGlyphFlags.Arg1And2AreWords))
            {
                arg1 = BinaryReader.ReadInt16BigEndian();
                arg2 = BinaryReader.ReadInt16BigEndian();
            }
            else
            {
                arg1 = BinaryReader.ReadSByte();
                arg2 = BinaryReader.ReadSByte();
            }

            if (flags.HasFlag(CompoundGlyphFlags.ArgsAreXYValues))
            {
                component.Matrix.M31 = arg1;
                component.Matrix.M32 = arg2;
            }

            if (flags.HasFlag(CompoundGlyphFlags.WeHaveAScale))
            {
                component.Matrix.M22 = component.Matrix.M11 = BinaryReader.Read2Dot14();
            }
            else if (flags.HasFlag(CompoundGlyphFlags.WeHaveAnXAndYScale))
            {
                component.Matrix.M11 = BinaryReader.Read2Dot14();
                component.Matrix.M22 = BinaryReader.Read2Dot14();
            }
            else if (flags.HasFlag(CompoundGlyphFlags.WeHaveATwoByTwo))
            {
                component.Matrix.M11 = BinaryReader.Read2Dot14();
                component.Matrix.M12 = BinaryReader.Read2Dot14();
                component.Matrix.M21 = BinaryReader.Read2Dot14();
                component.Matrix.M22 = BinaryReader.Read2Dot14();
            }

            // read the component glyph
            var oldPosition = BinaryReader.BaseStream.Position;
            var simpleGlyph = ReadGlyph(component.GlyphIndex);
            BinaryReader.BaseStream.Position = oldPosition;

            // merge the component glyph's contour ends into this glyphs's contour ends
            var pointOffset = glyph.Points.Count;
            for (int i = 0; i < simpleGlyph.ContourEnds.Count; i++)
                glyph.ContourEnds.Add((ushort)(simpleGlyph.ContourEnds[i] + pointOffset));

            // merge the component glyph's points into this glyphs's points
            for (int i = 0; i < simpleGlyph.Points.Count; i++)
            {
                var point = simpleGlyph.Points[i];

                var x = (int)(component.Matrix.M11 * point.X + component.Matrix.M12 * point.Y + component.Matrix.M31);
                var y = (int)(component.Matrix.M21 * point.X + component.Matrix.M22 * point.Y + component.Matrix.M32);
                glyph.Points.Add(new(x, y, point.IsOnCurve));
            }
        }

        glyph.NumberOfContours = (short)glyph.ContourEnds.Count;

        if (flags.HasFlag(CompoundGlyphFlags.WeHaveInstructions))
            BinaryReader.BaseStream.Position += BinaryReader.ReadUInt16BigEndian();
    }
}

namespace NovaEngine.ContentPipeline.Models.Font;

/// <summary>A guillotine 2D single bin packer.</summary>
internal static class GlyphPacker
{
    /*********
    ** Constants
    *********/
    /// <summary>The worst fit value when evaluatng how well a glyph and space fit each other.</summary>
    private const int WorstFit = int.MaxValue;


    /*********
    ** Fields
    *********/
    /// <summary>The spaces used when packing glyphs.</summary>
    private static readonly List<Rectangle> Spaces = new();


    /*********
    ** Public Methods
    *********/
    /// <summary>Sets the size of the space.</summary>
    /// <param name="edgeLength">The width and height of the space.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="edgeLength"/> is less than one.</exception>
    public static void SetInitialSpaceSize(int edgeLength)
    {
        if (edgeLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(edgeLength), "Must be more than zero.");

        Spaces.Clear();
        Spaces.Add(new Rectangle(0, 0, edgeLength, edgeLength));
    }

    /// <summary>Tries to pack glyphs into the space that was defined when creating the packer.</summary>
    /// <param name="glyphs">The glyphs to pack.</param>
    /// <param name="padding">The amount of padding between glyphs in the atlas.</param>
    /// <returns><see langword="true"/>, if all the glyphs could be successfully packed; otherwise, <see langword="false"/>.</returns>
    /// <remarks>The final atlas position of each glyph is stored in the scaled bounds X/Y of the glyph.</remarks>
    public static bool TryPack(List<Glyph> glyphs, int padding)
    {
        var halfPadding = padding / 2;
        var remainingGlyphs = new List<Glyph>(glyphs);

        while (remainingGlyphs.Any())
        {
            FindBestGlyphSpacePair(remainingGlyphs, padding, out var bestSpaceIndex, out var bestGlyphIndex);
            if (bestSpaceIndex < 0 || bestGlyphIndex < 0)
                break;

            var bestGlyph = remainingGlyphs[bestGlyphIndex];
            var bestSpace = Spaces[bestSpaceIndex];

            bestGlyph.ScaledBounds.X = halfPadding + bestSpace.X;
            bestGlyph.ScaledBounds.Y = halfPadding + bestSpace.Y;

            SplitSpace(bestSpaceIndex, (int)bestGlyph.ScaledBounds.Width + padding, (int)bestGlyph.ScaledBounds.Height + padding);
            remainingGlyphs.RemoveAt(bestGlyphIndex);
        }

        return !remainingGlyphs.Any();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Finds a glyph and space pair that fit each other the best.</summary>
    /// <param name="remainingGlyphs">The remaining glpyhs to check.</param>
    /// <param name="padding">The amount of padding between glyphs in the atlas.</param>
    /// <param name="bestSpaceIndex">The index of the space in the best pair.</param>
    /// <param name="bestGlyphIndex">The index of the glyph in the best pair.</param>
    private static void FindBestGlyphSpacePair(List<Glyph> remainingGlyphs, int padding, out int bestSpaceIndex, out int bestGlyphIndex)
    {
        bestSpaceIndex = -1;
        bestGlyphIndex = -1;
        var bestFit = WorstFit;

        for (var i = 0; i < Spaces.Count; i++)
        {
            var space = Spaces[i];
            for (var j = 0; j < remainingGlyphs.Count; j++)
            {
                var glyph = remainingGlyphs[j];

                var glyphWidth = glyph.ScaledBounds.Width + padding;
                var glyphHeight = glyph.ScaledBounds.Height + padding;

                // check if any glyphs fit a space perfectly
                if (glyphWidth == space.Width && glyphHeight == space.Height)
                {
                    bestSpaceIndex = i;
                    bestGlyphIndex = j;
                    return;
                }

                // otherwise, check which space and glyph fit the best together
                if (glyphWidth <= space.Width && glyphHeight <= space.Height)
                {
                    var fit = RateFit((int)glyphWidth, (int)glyphHeight, (int)space.Width, (int)space.Height);
                    if (fit < bestFit)
                    {
                        bestFit = fit;
                        bestSpaceIndex = i;
                        bestGlyphIndex = j;
                    }
                }
            }
        }
    }

    /// <summary>Splits a space around a specified width and height.</summary>
    /// <param name="index">The index of the space to split.</param>
    /// <param name="width">The width from the top left corner that no split space should overlap.</param>
    /// <param name="height">The height from the top left corner that no split space should overlap.</param>
    /// <remarks>This is used to create seperate spaces around a glyph. When a glyph is positioned in a space, the width and height is passed here to which the remaining space the glyph didn't take up is split into new spaces that other glyphs can occupy.</remarks>
    private static void SplitSpace(int index, int width, int height)
    {
        var space = Spaces[index];
        Spaces.RemoveAt(index);

        // divide the space along the width and height
        var a = new Rectangle(space.X, space.Y + height, width, space.Height - height);
        var b = new Rectangle(space.X + width, space.Y, space.Width - width, height);

        // correct spaces
        if (width * (space.Height - height) <= height * (space.Width - width))
            a.Width = space.Width;
        else
            b.Height = space.Height;

        // add split spaces
        if (a.Width > 0 && a.Height > 0)
            Spaces.Add(a);
        if (b.Width > 0 && b.Height > 0)
            Spaces.Add(b);
    }

    /// <summary>Rates how well a glyph and space fit together.</summary>
    /// <param name="glyphWidth">The width of the glyph.</param>
    /// <param name="glyphHeight">The height of the glyph.</param>
    /// <param name="spaceWidth">The width of the space.</param>
    /// <param name="spaceHeight">The height of the space.</param>
    /// <returns>A number determining how well the glyph fits with the space; lower values means they fit together better.</returns>
    private static int RateFit(int glyphWidth, int glyphHeight, int spaceWidth, int spaceHeight) => Math.Min(spaceWidth - glyphWidth, spaceHeight - glyphHeight);
}

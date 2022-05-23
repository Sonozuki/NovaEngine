namespace NovaEngine.Content.Models.Font;

/// <summary>The glyph atlas packer.</summary>
internal static class AtlasPacker
{
    /*********
    ** Constants
    *********/
    /// <summary>The amount of padding between glyphs in the atlas.</summary>
    private const int Padding = 1;

    /// <summary>The range (in pixels) of the signed distance around the glyphs.</summary>
    private const float Range = 2;


    /*********
    ** Public Methods
    *********/
    /// <summary>Creates the atlas for a font.</summary>
    /// <param name="font">The font to create the atlas for.</param>
    /// <returns>The atlas for the font.</returns>
    public static Colour128[,] CreateAtlas(TrueTypeFont font)
    {
        var nonWhitespaceGlyphs = font.Glyphs.Where(glyph => glyph.ScaledBounds.Width > 0 && glyph.ScaledBounds.Height > 0).ToList();
        Pack(nonWhitespaceGlyphs, out var atlasEdgeLength);

        var atlas = new Colour128[atlasEdgeLength, atlasEdgeLength];
        foreach (var glyph in nonWhitespaceGlyphs)
            MTSDF.GenerateMTSDF(glyph, atlas, Range);

        return atlas;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Packs a collection of glyphs into the smallest possible atlas.</summary>
    /// <param name="glyphs">The glyphs to pack.</param>
    /// <param name="atlasEdgeLength">The minimum edge length of the atlas.</param>
    private static void Pack(List<Glyph> glyphs, out int atlasEdgeLength)
    {
        // calculate minimum possible required area
        var totalArea = 0;
        foreach (var glyph in glyphs)
            totalArea += (int)(glyph.ScaledBounds.Width + Padding * glyph.ScaledBounds.Height + Padding);

        // calculate the minimum atlas size and glyph positions
        atlasEdgeLength = (int)MathF.Ceiling(MathF.Sqrt(totalArea) / 4f) * 4;
        var isAtlasBigEnough = false;

        while (!isAtlasBigEnough)
        {
            atlasEdgeLength += 4;
            GlyphPacker.SetInitialSpaceSize(atlasEdgeLength);
            isAtlasBigEnough = GlyphPacker.TryPack(glyphs, Padding);

            // TODO: make sure the texture is broken up when a large enough texture size is reached (only really an issue with languages such as Japanese)
        }
    }
}

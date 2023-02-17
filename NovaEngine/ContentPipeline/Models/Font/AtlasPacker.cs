namespace NovaEngine.ContentPipeline.Models.Font;

/// <summary>The glyph atlas packer.</summary>
internal static class AtlasPacker
{
    /*********
    ** Constants
    *********/
    /// <summary>The range, in pixels, of the signed distance around the glyphs.</summary>
    public const int PixelRange = 4;

    /// <summary>The amount of padding around the glyphs in the atlas.</summary>
    private const int Padding = 1;


    /*********
    ** Public Methods
    *********/
    /// <summary>Creates the atlas for a font.</summary>
    /// <param name="font">The font to create the atlas for.</param>
    /// <returns>The atlas for the font.</returns>
    public static Colour32[,] CreateAtlas(TrueTypeFont font)
    {
        Pack(font.Glyphs, out var atlasEdgeLength);

        var atlas = new Colour32[atlasEdgeLength, atlasEdgeLength];
        foreach (var glyph in font.Glyphs)
            MTSDF.GenerateMTSDF(glyph, atlas, PixelRange);

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
        var totalPadding = (Padding + PixelRange) * 2;
        var totalArea = 0;
        foreach (var glyph in glyphs)
            totalArea += (int)((glyph.ScaledBounds.Width + totalPadding) * (glyph.ScaledBounds.Height + totalPadding));

        // calculate the minimum atlas size and glyph positions
        atlasEdgeLength = (int)MathF.Ceiling(MathF.Sqrt(totalArea) / 4f) * 4;
        while (true)
        {
            // TODO: improve algorithm for finding smallest atlas size
            atlasEdgeLength += 4;
            GlyphPacker.SetInitialSpaceSize(atlasEdgeLength);
            if (GlyphPacker.TryPack(glyphs, totalPadding))
                break;

            // TODO: make sure the texture is broken up when a large enough texture size is reached (only really an issue with languages such as Japanese)
        }
    }
}

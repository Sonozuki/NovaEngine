namespace NovaEngine.ContentPipeline.Font.Models;

/// <summary>Represents an advance width and left side bearing pair.</summary>
internal sealed class HorizontalMetric
{
    /*********
    ** Properties
    *********/
    /// <summary>The advance width of the glyph, in font design units.</summary>
    public ushort AdvanceWidth { get; }

    /// <summary>The left side bearing of the glyph, in font design units.</summary>
    public short LeftSideBearing { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="advanceWidth">The advance width of the glyph, in font design units.</param>
    /// <param name="leftSideBearing">The left side bearing of the glyph, in font design units.</param>
    public HorizontalMetric(ushort advanceWidth, short leftSideBearing)
    {
        AdvanceWidth = advanceWidth;
        LeftSideBearing = leftSideBearing;
    }
}

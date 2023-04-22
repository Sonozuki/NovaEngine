namespace NovaEngine.ContentPipeline.Font;

/// <summary>Represents an advance width and left side bearing pair.</summary>
public sealed class HorizontalMetrics
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
    public HorizontalMetrics(ushort advanceWidth, short leftSideBearing)
    {
        AdvanceWidth = advanceWidth;
        LeftSideBearing = leftSideBearing;
    }
}

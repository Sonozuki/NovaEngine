namespace NovaEngine.Content.Models.Font;

/// <summary>The horizontal metrics of a glyph.</summary>
public class HorizontalMetrics
{
    /*********
    ** Fields
    *********/
    /// <summary>The advance width of the glyph.</summary>
    public ushort AdvanceWidth;

    /// <summary>The left side bearing of the glyph.</summary>
    public ushort LeftSideBearing;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="advanceWidth">The advance width of the glyph.</param>
    /// <param name="leftSideBearing">The left side bearing of the glyph.</param>
    public HorizontalMetrics(ushort advanceWidth, ushort leftSideBearing)
    {
        AdvanceWidth = advanceWidth;
        LeftSideBearing = leftSideBearing;
    }
}

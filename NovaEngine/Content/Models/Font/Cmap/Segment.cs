namespace NovaEngine.Content.Models.Font.Cmap;

/// <summary>Represents a segment in a format 4 cmap subtable.</summary>
internal class Segment
{
    /*********
    ** Properties
    *********/
    /// <summary>The start code of the segment.</summary>
    public ushort StartCode { get; set; }

    /// <summary>The end code of the segment.</summary>
    public ushort EndCode { get; set; }

    /// <summary>The delta for all character codes in the segment.</summary>
    public ushort IdDelta { get; set; }

    /// <summary>The offset, in <see langword="byte"/>s, to the glyph index array, or 0.</summary>
    public ushort IdRangeOffset { get; set; }
}

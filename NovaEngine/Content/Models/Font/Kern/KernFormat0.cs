namespace NovaEngine.Content.Models.Font.Kern;

/// <summary>Represents a format 0 kerning subtable.</summary>
internal class KernFormat0 : IKernFormat
{
    /*********
    ** Accessors
    *********/
    /// <summary>Whether the axis should be swapped when calculating the kerning of a character pair.</summary>
    public bool ShouldSwap { get; }

    /// <summary>The kerning pairs present.</summary>
    /// <remarks>The key is the left glyph index (high order) and right glyph index (low order).</remarks>
    public Dictionary<uint, short> Pairs { get; } = new();


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">The binary reader whose current position is at the start of the format 0 kerning subtable content.</param>
    /// <param name="isVertical">Whether the subtable has vertical kerning values.</param>
    /// <param name="hasCrossStream">Whether the subtable has cross-stream kerning values.</param>
    public KernFormat0(BinaryReader binaryReader, bool isVertical, bool hasCrossStream)
    {
        ShouldSwap = (isVertical && !hasCrossStream) || (!isVertical && hasCrossStream);
        var numberOfPairs = binaryReader.ReadUInt16BigEndian();

        binaryReader.BaseStream.Position += 6;
        for (int i = 0; i < numberOfPairs; i++)
        {
            var leftGlyphIndex = binaryReader.ReadUInt16BigEndian();
            var rightGlyphIndex = binaryReader.ReadUInt16BigEndian();
            Pairs[(uint)((leftGlyphIndex << 16) | rightGlyphIndex)] = binaryReader.ReadInt16BigEndian();
        }
    }
}

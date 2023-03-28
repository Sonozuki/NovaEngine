namespace NovaEngine.ContentPipeline.Font.Tables;

/// <summary>Represents the OS/2 and Windows specific metrics table.</summary>
internal sealed class OS2Table
{
    /*********
    ** Properties
    *********/
    /// <summary>The version of the table. This is between 0 and 5 (inclusive).</summary>
    public ushort Version { get; }

    /// <summary>The average width of all non-zero width glyphs in the font, in font design units.</summary>
    public short AverageCharWidth { get; }

    /// <summary>The visual weight of the characters in the font. Values from 1 to 1000 are valid.</summary>
    public FontWeight WeightClass { get; }

    /// <summary>The relative change from the normal aspect ratio as specified by a font designer for the glyphs in the font.</summary>
    public FontWidth WidthClass { get; }

    /// <summary>Font embedding licensing rights for the font.</summary>
    public EmbeddingLicensingFlags EmbeddingLicensingFlags { get; }

    /// <summary>The recommended horizontal size, in font design units, for subscripts for this font.</summary>
    /// <remarks>
    /// If a font has two recommended sizes for subscripts, e.g., numerics and other, the numeric sizes should be stressed. This size field maps to the em size of the font being used for a subscript. The horizontal font size specifies a font designer’s recommended horizontal size of subscript glyphs associated with this font. If a font does not include all of the required subscript glyphs for an application, and the application can substitute glyphs by scaling the glyphs of a font or by substituting glyphs from another font, this parameter specifies the recommended nominal width for those subscript glyphs.<br/><br/>
    /// For example, if the em for a font is 2048 units and <see cref="SubscriptXSize"/> is set to 205, then the horizontal size for a simulated subscript glyph would be 1/10th the size of the normal glyph.
    /// </remarks>
    public short SubscriptXSize { get; }

    /// <summary>The recommended vertical size, in font design units, for subscripts for this font.</summary>
    /// <remarks>
    /// If a font has two recommended sizes for subscripts, e.g. numerics and other, the numeric sizes should be stressed. This size field maps to the em size of the font being used for a subscript. The vertical font size specifies a font designer’s recommendation for vertical size of subscript glyphs associated with this font. If a font does not include all of the required subscript glyphs for an application, and the application can substitute glyphs by scaling the glyphs in a font or by substituting glyphs from another font, this parameter specifies the recommended nominal height for those subscript glyphs.<br/><br/>
    /// For example, if the em for a font is 2048 units and <see cref="SubscriptYSize"/> is set to 205, then the vertical size for a simulated subscript glyph would be 1/10th the size of the normal glyph.
    /// </remarks>
    public short SubscriptYSize { get; }

    /// <summary>The recommended horizontal offset, in font design units, for subscripts for this font.</summary>
    /// <remarks>This specifies a font designer’s recommended horizontal offset — from the glyph origin to the glyph origin of the subscript’s glyph — for subscript glyphs associated with this font. If a font does not include all of the required subscript glyphs for an application, and the application can substitute glyphs, this parameter specifies the recommended horizontal position from the glyph escapement point of the last glyph before the first subscript glyph. For upright glyphs, this value is usually zero; however, if the glyphs of a font have an incline (italic or slant), the reference point for subscript glyphs is usually adjusted to compensate for the angle of incline.</remarks>
    public short SubscriptXOffset { get; }

    /// <summary>The recommended vertical offset, in font design units, for subscripts for this font.</summary>
    /// <remarks>This specifies a font designer’s recommended vertical offset from the glyph baseline to the glyph baseline for subscript glyphs associated with this font. Values are expressed as a positive offset below the glyph baseline. If a font does not include all of the required subscript glyphs for an application, this parameter specifies the recommended vertical distance below the glyph baseline for those subscript glyphs.</remarks>
    public short SubscriptYOffset { get; }

    /// <summary>The recommended horizontal size, in font design units, for superscripts for this font.</summary>
    /// <remarks>
    /// If a font has two recommended sizes for superscripts, e.g., numerics and other, the numeric sizes should be stressed. This size field maps to the em size of the font being used for a superscript. The horizontal font size specifies a font designer’s recommended horizontal size for superscript glyphs associated with this font. If a font does not include all of the required superscript glyphs for an application, and the application can substitute glyphs by scaling the glyphs of a font or by substituting glyphs from another font, this parameter specifies the recommended nominal width for those superscript glyphs.<br/><br/>
    /// For example, if the em for a font is 2048 units and <see cref="SuperscriptXSize"/> is set to 205, then the horizontal size for a simulated superscript glyph would be 1/10th the size of the normal glyph.
    /// </remarks>
    public short SuperscriptXSize { get; }

    /// <summary>The recommeded vertical size, in font design units, for superscripts for this font.</summary>
    /// <remarks>
    /// If a font has two recommended sizes for superscripts, e.g., numerics and other, the numeric sizes should be stressed. This size field maps to the em size of the font being used for a superscript. The vertical font size specifies a font designer’s recommended vertical size for superscript glyphs associated with this font. If a font does not include all of the required superscript glyphs for an application, and the application can substitute glyphs by scaling the glyphs of a font or by substituting glyphs from another font, this parameter specifies the recommended nominal height for those superscript glyphs.<br/><br/>
    /// For example, if the em for a font is 2048 units and <see cref="SuperscriptYSize"/> is set to 205, then the vertical size for a simulated superscript glyph would be 1/10th the size of the normal glyph.
    /// </remarks>
    public short SuperscriptYSize { get; }

    /// <summary>The recommended horizontal offset, in font design units, for superscripts for this font.</summary>
    /// <remarks>This specifies a font designer’s recommended horizontal offset — from the glyph’s origin to the superscript glyph’s origin for the superscript characters associated with this font. If a font does not include all of the required superscript characters for an application, this parameter specifies the recommended horizontal position from the escapement point of the character before the first superscript character. For upright characters, this value is usually zero; however, if the characters of a font have an incline (italic characters) the reference point for superscript characters is usually adjusted to compensate for the angle of incline.</remarks>
    public short SuperscriptXOffset { get; }

    /// <summary>The recommended vertical offset, in font design units, for superscripts for this font.</summary>
    /// <remarks>This specifies a font designer’s recommended vertical offset — from the glyph’s baseline to the superscript glyph’s baseline associated with this font. Values for this parameter are expressed as a positive offset above the character baseline. If a font does not include all of the required superscript characters for an application, this parameter specifies the recommended vertical distance above the character baseline for those superscript characters.</remarks>
    public short SuperscriptYOffset { get; }

    /// <summary>The thickness of the strikeout stroke, in font design units.</summary>
    public short StrikeoutSize { get; }

    /// <summary>The position of the top of the stikeout stroke relative to the baseline, in font design units.</summary>
    public short StrikeoutPosition { get; }

    /// <summary>The font-family class and subclass.</summary>
    /// <remarks>The font class and font subclass are registered values assigned by IBM to each font family. This parameter is intended for use in selecting an alternate font when the requested font is not available. The font class is the most general and the font subclass is the most specific. The high byte of this field contains the family class, while the low byte contains the family subclass. <see href="https://learn.microsoft.com/en-us/typography/opentype/spec/ibmfc">More information about this field.</see></remarks>
    public short FamilyClass { get; }

    /// <summary>A 10-byte series of numbers used to describe the visual characteristics of the typeface.</summary>
    /// <remarks>The Panose values are fully described in the <see href="https://monotype.github.io/panose/">PANOSE Classification Metrics Guide</see>.</remarks>
    public byte[] Panose { get; }

    /// <summary>The first 32-bit value specifying the Unicode blocks or ranges encompassed by the font file in 'cmap' subtables for <see cref="PlatformId.Windows"/> EncodingId 1 (Unicode BMP) and <see cref="PlatformId.Windows"/> EncodingId 10 (Unicode full repertoire).</summary>
    /// <remarks>
    /// If a bit is set, then the unicode ranges assigned to that bit are considered functional.<br/>
    /// If a bit is clear, then the range is not considered functional.<br/><br/>
    /// Each of the bits is treated as an independent flag and the bits can be set in any combination. The determination of "functional" is left up the font designer, although character set selection should attempt to be functional by ranges if at all possible.
    /// </remarks>
    public uint UnicodeRange1 { get; }

    /// <summary>The second 32-bit value specifying the Unicode blocks or ranges encompassed by the font file in 'cmap' subtables for <see cref="PlatformId.Windows"/> EncodingId 1 (Unicode BMP) and <see cref="PlatformId.Windows"/> EncodingId 10 (Unicode full repertoire).</summary>
    /// <remarks>
    /// If a bit is set, then the unicode ranges assigned to that bit are considered functional.<br/>
    /// If a bit is clear, then the range is not considered functional.<br/><br/>
    /// Each of the bits is treated as an independent flag and the bits can be set in any combination. The determination of "functional" is left up the font designer, although character set selection should attempt to be functional by ranges if at all possible.
    /// </remarks>
    public uint UnicodeRange2 { get; }

    /// <summary>The third 32-bit value specifying the Unicode blocks or ranges encompassed by the font file in 'cmap' subtables for <see cref="PlatformId.Windows"/> encoding id 1 (Unicode BMP) and <see cref="PlatformId.Windows"/> EncodingId 10 (Unicode full repertoire).</summary>
    /// <remarks>
    /// If a bit is set, then the unicode ranges assigned to that bit are considered functional.<br/>
    /// If a bit is clear, then the range is not considered functional.<br/><br/>
    /// Each of the bits is treated as an independent flag and the bits can be set in any combination. The determination of "functional" is left up the font designer, although character set selection should attempt to be functional by ranges if at all possible.
    /// </remarks>
    public uint UnicodeRange3 { get; }

    /// <summary>The fourth 32-bit value specifying the Unicode blocks or ranges encompassed by the font file in 'cmap' subtables for <see cref="PlatformId.Windows"/> encoding id 1 (Unicode BMP) and <see cref="PlatformId.Windows"/> EncodingId 10 (Unicode full repertoire).</summary>
    /// <remarks>
    /// If a bit is set, then the unicode ranges assigned to that bit are considered functional.<br/>
    /// If a bit is clear, then the range is not considered functional.<br/><br/>
    /// Each of the bits is treated as an independent flag and the bits can be set in any combination. The determination of "functional" is left up the font designer, although character set selection should attempt to be functional by ranges if at all possible.
    /// </remarks>
    public uint UnicodeRange4 { get; }

    /// <summary>The four-character identifier for the vendor of the given type face.</summary>
    /// <remarks>Unregistered vendors may be used but is discouraged, for a list of registered vendors, see <see href="https://learn.microsoft.com/en-gb/typography/vendors/">Registered Typography Vendors</see>.</remarks>
    public Tag VendorId { get; }

    /// <summary>The font selection flags.</summary>
    public FontSelectionFlags SelectionFlags { get; }

    /// <summary>The minimum Unicode index (character code) in the font, according to the 'cmap' subtable for <see cref="PlatformId.Windows"/> and platform-specific encoding id 0 or 1.</summary>
    public ushort FirstCharIndex { get; }

    /// <summary>The maximum Unicode index (character code) in the font, according to the 'cmap' subtable for <see cref="PlatformId.Windows"/> and platform-specific encoding id 0 or 1.</summary>
    public ushort LastCharIndex { get; }

    /// <summary>The typographic ascendor for this font.</summary>
    /// <remarks>
    /// This should be combined with <see cref="TypoDescender"/> and <see cref="TypoLineGap"/> to determine default line spacing.<br/>
    /// <see cref="FontSelectionFlags.UseTypoMetrics"/> of <see cref="SelectionFlags"/> is used to choose between using Typo* and Win* values for default line metrics.
    /// </remarks>
    public short TypoAscender { get; }

    /// <summary>The typographic descender for this font.</summary>
    /// <remarks>
    /// This should be combined with <see cref="TypoAscender"/> and <see cref="TypoLineGap"/> to determine default line spacing.<br/>
    /// <see cref="FontSelectionFlags.UseTypoMetrics"/> of <see cref="SelectionFlags"/> is used to choose between using Typo* and Win* values for default line metrics.
    /// </remarks>
    public short TypoDescender { get; }

    /// <summary>The typographic line gap for this font.</summary>
    /// <remarks>
    /// This should be combined with <see cref="TypoAscender"/> and <see cref="TypoDescender"/> to determine default line spacing.<br/>
    /// <see cref="FontSelectionFlags.UseTypoMetrics"/> of <see cref="SelectionFlags"/> is used to choose between using Typo* and Win* values for default line metrics.
    /// </remarks>
    public short TypoLineGap { get; }

    /// <summary>The "Windows ascender" metric. This should be used to specify the height above the baseline for the clipping region.</summary>
    public ushort WinAscent { get; }

    /// <summary>The "Windows descender" metric. This should be used to specify the vertical extent below the baseline for a clipping region.</summary>
    public ushort WinDescent { get; }

    /// <summary>The first 32-bit value specifying the code pages encompassed by the font file in the 'cmap' subtable for <see cref="PlatformId.Windows"/> encoding id 1 (Unicode BMP).</summary>
    /// <remarks>
    /// This is only set when <see cref="Version"/> is 1 or higher.<br/><br/>
    /// If a bit is set, then the code page is considered functional.<br/>
    /// If a bit is clear, then the code page is not considered functional.<br/><br/>
    /// Each of the bits is treated as an independent flag and the bits can be set in any combination. The determination of "functional" is left up the font designer, although character set selection should attempt to be functional by ranges if at all possible.
    /// </remarks>
    public uint CodePageRange1 { get; }

    /// <summary>The second 32-bit value specifying the code pages encompassed by the font file in the 'cmap' subtable for <see cref="PlatformId.Windows"/> encoding id 1 (Unicode BMP).</summary>
    /// <remarks>
    /// This is only set when <see cref="Version"/> is 1 or higher.<br/><br/>
    /// If a bit is set, then the code page is considered functional.<br/>
    /// If a bit is clear, then the code page is not considered functional.<br/><br/>
    /// Each of the bits is treated as an independent flag and the bits can be set in any combination. The determination of "functional" is left up the font designer, although character set selection should attempt to be functional by ranges if at all possible.
    /// </remarks>
    public uint CodePageRange2 { get; }

    /// <summary>Specifies the distance between the baseline and the approximate height of non-ascending lowercase letters measured in FUnits.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 2 or higher.</remarks>
    public short XHeight { get; }

    /// <summary>Specifies the distance between the baseline and the approximate height of uppercase letters measured in FUnits.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 2 or higher.</remarks>
    public short CapHeight { get; }

    /// <summary>The Unicode code point, in UTF-16 encoding, of a character that can be used for a default glyph if a requested character is not supported in the font.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 2 or higher.</remarks>
    public ushort DefaultChar { get; }

    /// <summary>This is the Unicode code point, in UTF-16 encoding, of a character that can be used as a default break character. The break character is used to separate words and justify text. Most fonts specify U+0020 SPACE as the break character. This field cannot represent supplementary-plane character values (code points greater than 0xFFFF), and so applications are strongly discouraged from using this field.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 2 or higher.</remarks>
    public ushort BreakChar { get; }

    /// <summary>The maximum length of a target glyph context for any feature in this font. For example, a font which has only a pair kerning feature should set this field to 2. If the font also has a ligature feature in which the glyph sequence “f f i” is substituted by the ligature “ffi”, then this field should be set to 3. This field could be useful to sophisticated line-breaking engines in determining how far they should look ahead to test whether something could change that affects the line breaking. For chaining contextual lookups, the length of the string (covered glyph) + (input sequence) + (lookahead sequence) should be considered.</summary>
    /// <remarks>This is only set when <see cref="Version"/> is 2 or higher.</remarks>
    public ushort MaxContext { get; }

    /// <summary>The lower value of the size range for which this font has been designed. The units for this field are TWIPs (one-twentieth of a point, or 1440 per inch). The value is inclusive — meaning that that font was designed to work best at this point size through, but not including, the point size indicated by <see cref="UpperOpticalPointSize"/>.</summary>
    /// <remarks>
    /// This field has been superseeded by the 'STAT' table.<br/><br/>
    /// This is only set when <see cref="Version"/> is 5.<br/><br/>
    /// This field is used for fonts with multiple optical styles.
    /// </remarks>
    public ushort LowerOpticalPointSize { get; }

    /// <summary>This value is the upper value of the size range for which this font has been designed. The units for this field are TWIPs (one-twentieth of a point, or 1440 per inch). The value is exclusive — meaning that that font was designed to work best below this point size down to the <see cref="LowerOpticalPointSize"/> threshold.</summary>
    /// <remarks>
    /// This field has been superseeded by the 'STAT' table.<br/><br/>
    /// This is only set when <see cref="Version"/> is 5.<br/><br/>
    /// This field is used for fonts with multiple optical styles.
    /// </remarks>
    public ushort UpperOpticalPointSize { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="binaryReader">
    /// The binary reader to use when reading the table.<br/>
    /// This assumes the reader has been positioned for the table to be read.
    /// </param>
    public OS2Table(BinaryReader binaryReader)
    {
        Version = binaryReader.ReadUInt16BigEndian();
        AverageCharWidth = binaryReader.ReadInt16BigEndian();
        WeightClass = (FontWeight)binaryReader.ReadUInt16BigEndian();
        WidthClass = (FontWidth)binaryReader.ReadUInt16BigEndian();
        EmbeddingLicensingFlags = (EmbeddingLicensingFlags)binaryReader.ReadUInt16BigEndian();
        SubscriptXSize = binaryReader.ReadInt16BigEndian();
        SubscriptYSize = binaryReader.ReadInt16BigEndian();
        SubscriptXOffset = binaryReader.ReadInt16BigEndian();
        SubscriptYOffset = binaryReader.ReadInt16BigEndian();
        SuperscriptXSize = binaryReader.ReadInt16BigEndian();
        SuperscriptYSize = binaryReader.ReadInt16BigEndian();
        SuperscriptXOffset = binaryReader.ReadInt16BigEndian();
        SuperscriptYOffset = binaryReader.ReadInt16BigEndian();
        StrikeoutSize = binaryReader.ReadInt16BigEndian();
        StrikeoutPosition = binaryReader.ReadInt16BigEndian();
        FamilyClass = binaryReader.ReadInt16BigEndian();
        Panose = binaryReader.ReadBytes(10);
        UnicodeRange1 = binaryReader.ReadUInt32BigEndian();
        UnicodeRange2 = binaryReader.ReadUInt32BigEndian();
        UnicodeRange3 = binaryReader.ReadUInt32BigEndian();
        UnicodeRange4 = binaryReader.ReadUInt32BigEndian();
        VendorId = binaryReader.ReadOTFTag();
        SelectionFlags = (FontSelectionFlags)binaryReader.ReadInt16BigEndian();
        FirstCharIndex = binaryReader.ReadUInt16BigEndian();
        LastCharIndex = binaryReader.ReadUInt16BigEndian();
        TypoAscender = binaryReader.ReadInt16BigEndian();
        TypoDescender = binaryReader.ReadInt16BigEndian();
        TypoLineGap = binaryReader.ReadInt16BigEndian();
        WinAscent = binaryReader.ReadUInt16BigEndian();
        WinDescent = binaryReader.ReadUInt16BigEndian();

        if (Version >= 1)
        {
            CodePageRange1 = binaryReader.ReadUInt32BigEndian();
            CodePageRange2 = binaryReader.ReadUInt32BigEndian();
        }

        if (Version >= 2)
        {
            XHeight = binaryReader.ReadInt16BigEndian();
            CapHeight = binaryReader.ReadInt16BigEndian();
            DefaultChar = binaryReader.ReadUInt16BigEndian();
            BreakChar = binaryReader.ReadUInt16BigEndian();
            MaxContext = binaryReader.ReadUInt16BigEndian();
        }

        if (Version == 5)
        {
            LowerOpticalPointSize = binaryReader.ReadUInt16BigEndian();
            UpperOpticalPointSize = binaryReader.ReadUInt16BigEndian();
        }

        if (Version < 0 || Version > 5)
            throw new FontException("'OS/2' version is invalid.");
    }
}

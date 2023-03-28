using NovaEngine.ContentPipeline.Font.Models;

namespace NovaEngine.ContentPipeline.Font;

/// <summary>The ids of a <see cref="NameRecord"/>.</summary>
internal enum NameId : ushort
{
    /// <summary>
    /// The copyright notice.<br/>
    /// <b>e.g.</b> <i>© Copyright the Monotype Corporation plc, 1990</i>
    /// </summary>
    Copyright,

    /// <summary>
    /// The font family name.<br/>
    /// <b>e.g.</b> <i>Times New Roman</i>
    /// </summary>
    FontFamilyName,

    /// <summary>
    /// The font subfamily name.<br/>
    /// <b>e.g.</b> <i>Bold</i>
    /// </summary>
    FontSubfamilyName,

    /// <summary>
    /// The unique font identifier.<br/>
    /// <b>e.g.</b> <i>Monotype: Times New Roman Bold:1990</i>
    /// </summary>
    FontIdentifier,

    /// <summary>
    /// The full font name that reflects all family and relevant subfamily descriptors.<br/>
    /// <b>e.g.</b> <i>Times New Roman Bold</i>
    /// </summary>
    FullName,

    /// <summary>
    /// The version string.<br/>
    /// <b>e.g.</b> <i>Version 1.00 June 1, 1990, initial release</i>
    /// </summary>
    Version,

    /// <summary>
    /// The PostScript name for the font.<br/>
    /// <b>e.g.</b> <i>TimesNewRoman-Bold</i>
    /// </summary>
    PostScriptName,

    /// <summary>
    /// Any trademark notice/information for the font.<br/>
    /// <b>e.g.</b> <i>Times New Roman is a registered trademark of the Monotype Corporation.</i>
    /// </summary>
    Trademark,

    /// <summary>
    /// The font manufacturer name.<br/>
    /// <b>e.g.</b> <i>Monotype Corporation plc</i>
    /// </summary>
    ManufacturerName,

    /// <summary>
    /// The name of the designer of the typeface.<br/>
    /// <b>e.g.</b> <i>Stanley Morison</i>
    /// </summary>
    Designer,

    /// <summary>
    /// The description of the typeface. Can contain revision information, usage recommendations, history, features, etc.<br/>
    /// <b>e.g.</b> <i>Designed in 1932 for the Times of London newspaper. Excellent readability and a narrow overall width, allowing more words per line than most fonts.</i>
    /// </summary>
    Description,

    /// <summary>
    /// The URL of the font vendor (with protocol, e.g., http://, ftp://).<br/>
    /// <b>e.g.</b> <i>http://www.monotype.com</i>
    /// </summary>
    URLVendor,

    /// <summary>
    /// The URL of the typeface designer (with protocol, e.g., http://, ftp://).<br/>
    /// <b>e.g.</b> <i>http://www.monotype.com</i>
    /// </summary>
    URLDesigner,

    /// <summary>
    /// The description of how the font may be legally used, or different example scenarios for licensed use.<br/>
    /// <b>e.g.</b> <i>This font may be installed on all of your machines and printers, but you may not sell or give these fonts to anyone else.</i>
    /// </summary>
    LicenseDescription,

    /// <summary>
    /// URL where additional licensing information can be found.<br/>
    /// <b>e.g.</b> <i>http://www.monotype.com/license/</i>
    /// </summary>
    LicenseInfoURL,

    // 15 reserved

    /// <summary>
    /// The typographic family grouping doesn’t impose any constraints on the number of faces within it, in contrast with the 4-style family grouping (<see cref="FontFamilyName"/>), which is present both for historical reasons and to express style linking groups. If <see cref="TypographyFamilyName"/> is absent, then <see cref="FontFamilyName"/> is considered to be the typographic family name. (In earlier versions of the specification, <see cref="TypographyFamilyName"/> was known as "Preferred Family".)<br/>
    /// <b>e.g.</b> No name string present, since it is the same as <see cref="FontFamilyName"/>
    /// </summary>
    TypographyFamilyName = 16,

    /// <summary>
    /// This allows font designers to specify a subfamily name within the typographic family grouping. This string must be unique within a particular typographic family. If it is absent, then <see cref="FontSubfamilyName"/> is considered to be the typographic subfamily name. (In earlier versions of the specification, <see cref="TypographySubfamilyName"/> was known as "Preferred Subfamily".)<br/>
    /// <b>e.g.</b> No name string present, since it is the same as <see cref="FontSubfamilyName"/>
    /// </summary>
    TypographySubfamilyName,

    /// <summary>
    /// On the Macintosh, the menu name is constructed using the FOND resource. This usually matches the Full Name.<br/>
    /// <b>e.g.</b> No name string present, since it is the same as <see cref="FullName"/>
    /// </summary>
    CompatibleFull,

    /// <summary>
    /// This can be the font name, or any other text that the designer thinks is the best sample to display the font in.<br/>
    /// <b>e.g.</b> <i>The quick brown fox jumps over the lazy dog.</i>
    /// </summary>
    SampleText,

    /// <summary>
    /// Its presence in a font means that <see cref="PostScriptName"/> holds a PostScript font name that is meant to be used with the "composefont" invocation in order to invoke the font in a PostScript interpreter.<br/>
    /// The value held in the <see cref="PostScriptCIDFindFontName"/> string is interpreted as a PostScript font name that is meant to be used with the "findfont" invocation, in order to invoke the font in a PostScript interpreter.<br/>
    /// <b>e.g.</b> No name string present. Thus, the PostScript Name defined by <see cref="PostScriptName"/> should be used with the "findfont" invocation for locating the font in the context of a PostScript interpreter. 
    /// </summary>
    PostScriptCIDFindFontName,

    /// <summary>
    /// Used to provide a WWS-conformant family name in case the entries for <see cref="TypographyFamilyName"/> and <see cref="TypographySubfamilyName"/> do not conform to the WWS model.<br/>
    /// <b>e.g.</b> Since Times New Roman is a WWS font, this field does not need to be specified. If the font contained styles such as "caption", "display", "handwriting", etc, that would be noted here.
    /// </summary>
    WWSFamilyName,

    /// <summary>
    /// Used in conjunction with <see cref="WWSFamilyName"/>, this ID provides a WWS-conformant subfamily name (reflecting only weight, width and slope attributes) in case the entries for <see cref="TypographyFamilyName"/> and <see cref="TypographySubfamilyName"/> do not conform to the WWS model.<br/>
    /// <b>e.g.</b> Since Times New Roman is a WWS font, this field does not need to be specified.
    /// </summary>
    WWSSubfamilyName,

    /// <summary>
    /// This ID, if used in the CPAL table’s Palette Labels Array, specifies that the corresponding color palette in the CPAL table is appropriate to use with the font when displaying it on a light background such as white. Strings for this ID are for use as user interface strings associated with this palette.<br/>
    /// <b>e.g.</b> No name string present, since this is not a color font.
    /// </summary>
    LightBackgroundPalette,

    /// <summary>
    /// This ID, if used in the CPAL table’s Palette Labels Array, specifies that the corresponding color palette in the CPAL table is appropriate to use with the font when displaying it on a dark background such as black. Strings for this ID are for use as user interface strings associated with this palette.<br/>
    /// <b>e.g.</b> No name string present, since this is not a color font.
    /// </summary>
    DarkBackgroundPalette,

    /// <summary>
    /// If present in a variable font, it may be used as the family prefix in the PostScript Name Generation for Variation Fonts algorithm.<br/>
    /// <b>e.g.</b> No name string present, since this is not a variable font.
    /// </summary>
    VariationsPostScriptNamePrefix
}

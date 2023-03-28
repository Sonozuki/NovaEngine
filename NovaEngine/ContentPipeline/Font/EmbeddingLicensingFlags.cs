namespace NovaEngine.ContentPipeline.Font;

/// <summary>Font embedding licensing rights for the font.</summary>
/// <remarks>
/// <i>Applications that implement support for font embedding must not embed fonts which are not licensed to permit embedding. Also, when embedding a font into a document, applications must not modify the embedding permissions and restrictions indicated in this field. In addition, applications loading embedded fonts for temporary use (<see cref="PreviewAndPrint"/> or <see cref="Editable"/> embedding) must delete the fonts when the document containing the embedded font is closed.</i><br/><br/>
/// If the 'OS/2' table is version 0 to 1, then <see cref="NoSubsetting"/> and <see cref="BitmapOnly"/> are to be ignored.<br/>
/// If the 'OS/2' table is version 0 to 2, then <see cref="RestrictedLicense"/>, <see cref="PreviewAndPrint"/>, and <see cref="Editable"/> aren't mutually exclusive, instead the least restrictive permission indicated takes precedence.<br/>
/// If the 'OS/2' table is version 3 or later, then <see cref="RestrictedLicense"/>, <see cref="PreviewAndPrint"/>, and <see cref="Editable"/> are mutually exclusive: fonts should never have more than of these bits set.<br/><br/>
/// To check if embedding has the lowest 4 bit unset (installable embedding) use the <see cref="EmbeddingLicensingFlagsExtensions.IsInstallable"/> extension method.
/// </remarks>
[Flags]
internal enum EmbeddingLicensingFlags : short
{
    /// <summary>The font must not be modified, embedded, or exchanged in any manner without first obtaining explicit permission of the legal owner.</summary>
    RestrictedLicense = 1 << 1,

    /// <summary>The font may be embedded, and may be temporarily loaded on other systems for purposes of viewing or printing the document. Documents containing <see cref="PreviewAndPrint"/> fonts must be opened "read-only"; no edits can be applied to the document.</summary>
    PreviewAndPrint = 1 << 2,

    /// <summary>The font may be embedded, and may be temporarily loaded on other systems. As with <see cref="PreviewAndPrint"/> embedding, documents containing <see cref="Editable"/> fonts may be opened for reading. In addition, editing is permitted, including ability to format new text using the embedded font, and changes may be saved.</summary>
    Editable = 1 << 3,

    // 4 - 7 reserved

    /// <summary>The font may not be subsetted prior to embedding. Other embedding restrictions specified in <see cref="RestrictedLicense"/>, <see cref="PreviewAndPrint"/>, <see cref="Editable"/>, and <see cref="BitmapOnly"/> also apply.</summary>
    NoSubsetting = 1 << 8,

    /// <summary>Only bitmaps contained in the font may be embedded. No outline data may be embedded. If there are no bitmaps available in the font, then the font is considered unembeddable and the embedding services will fail. Other embedding restrictions specified in <see cref="RestrictedLicense"/>, <see cref="PreviewAndPrint"/>, <see cref="Editable"/>, and <see cref="NoSubsetting"/> also apply.</summary>
    BitmapOnly = 1 << 9

    // 10 - 15 reserved
}

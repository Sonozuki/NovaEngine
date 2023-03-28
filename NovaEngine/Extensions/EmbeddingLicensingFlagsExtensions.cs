using NovaEngine.ContentPipeline.Font;

namespace NovaEngine.Extensions;

/// <summary>Extension methods for <see cref="EmbeddingLicensingFlags"/>.</summary>
internal static class EmbeddingLicensingFlagsExtensions
{
    /*********
    ** Public Methods
    *********/
    /// <summary>The font may be embedded, and may be permanently installed for use on a remote system, or for use by other users. The user of the remote system acquires the identical rights, obligations, and licenses for that font as the original purchaser of the font, and is subject to the same end-user license agreement, copyright, design patent, and/or trademark as was the original pruchaser.</summary>
    /// <remarks>This will check that <see cref="EmbeddingLicensingFlags.RestrictedLicense"/>, <see cref="EmbeddingLicensingFlags.PreviewAndPrint"/>, and <see cref="EmbeddingLicensingFlags.Editable"/> aren't set (lowest 4 bits are unset).</remarks>
    public static bool IsInstallable(this EmbeddingLicensingFlags flags) => ((ushort)flags & 0b00001111) == 0;
}

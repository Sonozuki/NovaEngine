using NovaEngine.Content.Models.Font.EdgeSegments;

namespace NovaEngine.Content.Models.Font;

/// <summary>Represents a colour channel for a specific pixel in an MTSDF texture for a glyph.</summary>
internal class Channel
{
    /*********
    ** Accessors
    *********/
    /// <summary>The minimum distance from the pixel to an edge segment whose edge colour has a colour channel this channel is representing.</summary>
    public SignedDistance MinDistance { get; set; } = new();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>The edge segment that is closest to the pixel whose edge colour has a colour channel this channel is representing.</summary>
    public EdgeSegmentBase NearEdge { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>The out 'param' parameter of the edge segment closest to the pixel whose edge colour has a colour channel this channel is representing.</summary>
    public float NearParam { get; set; }
}

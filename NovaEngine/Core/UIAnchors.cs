namespace NovaEngine.Core;

/// <summary>Represents the anchors of a UI element.</summary>
public class UIAnchors
{
    /*********
    ** Accessors
    *********/
    /// <summary>The horizontal anchors of the UI element.</summary>
    public HorizontalAnchor Horizontal { get; set; } = HorizontalAnchor.Centre;

    /// <summary>The vertical anchors of the UI element.</summary>
    public VerticalAnchor Vertical { get; set; } = VerticalAnchor.Centre;
}
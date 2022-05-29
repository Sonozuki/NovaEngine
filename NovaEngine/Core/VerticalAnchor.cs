namespace NovaEngine.Core;

/// <summary>The vertical anchor of a UI element.</summary>
public enum VerticalAnchor
{
    /// <summary>The Y position of the element is relative to the top of the screen.</summary>
    Top,

    /// <summary>The Y position of the element is relative to the centre of the screen.</summary>
    Centre,

    /// <summary>The Y position of the element is relative to the bottom of the screen.</summary>
    Bottom,

    /// <summary>The UI element stretches so the distance between the top/bottom of the element and top/bottom of the screen remains constant.</summary>
    Stretch
}
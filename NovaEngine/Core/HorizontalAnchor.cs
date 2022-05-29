namespace NovaEngine.Core;

/// <summary>The horizontal anchor of a UI element.</summary>
public enum HorizontalAnchor
{
    /// <summary>The X position of the element is relative to the left side of the screen.</summary>
    Left,

    /// <summary>The X position of the element is relative to the centre of the screen.</summary>
    Centre,

    /// <summary>The X position of the element is relative to the right of the screen.</summary>
    Right,

    /// <summary>The UI element stretches so the distance between the left/right of the element and left/right of the screen remains constant.</summary>
    Stretch
}

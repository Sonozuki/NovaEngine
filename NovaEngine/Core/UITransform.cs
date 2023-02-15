namespace NovaEngine.Core;

/// <summary>Represents the transform of a UI element.</summary>
public sealed class UITransform : Transform
{
    /*********
    ** Properties
    *********/
    /// <summary>The anchors of the UI element.</summary>
    public UIAnchors Anchors { get; } = new();

    /// <summary>The width of the UI element, in pixels.</summary>
    /// <remarks>This is only used when the element has <see cref="HorizontalAnchor.Left"/>, <see cref="HorizontalAnchor.Centre"/>, or <see cref="HorizontalAnchor.Right"/>.</remarks>
    public float Width { get; set; }

    /// <summary>The height of the UI element, in pixels.</summary>
    /// <remarks>This is only used when the element has <see cref="VerticalAnchor.Top"/>, <see cref="VerticalAnchor.Centre"/>, or <see cref="VerticalAnchor.Bottom"/>.</remarks>
    public float Height { get; set; }

    /// <summary>The distance from the top of the element to the top of the screen, in pixels.</summary>
    /// <remarks>This is only used when the element has <see cref="VerticalAnchor.Stretch"/>.</remarks>
    public float Top { get; set; }

    /// <summary>The distance from the bottom of the element to the bottom of the screen, in pixels.</summary>
    /// <remarks>This is only used when the element has <see cref="VerticalAnchor.Stretch"/>.</remarks>
    public float Bottom { get; set; }

    /// <summary>The distance from the left of the element to the left of the screen, in pixels.</summary>
    /// <remarks>This is only used when the element has <see cref="HorizontalAnchor.Stretch"/>.</remarks>
    public float Left { get; set; }

    /// <summary>The distance from the right of the element to the right of the screen, in pixels.</summary>
    /// <remarks>This is only used when the element has <see cref="HorizontalAnchor.Stretch"/>.</remarks>
    public float Right { get; set; }


    /*********
    ** Constructors
    *********/
    /// <inheritdoc/>
    internal UITransform(GameObject gameObject)
        : base(gameObject) { }
}

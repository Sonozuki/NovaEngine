﻿#pragma warning disable CA1823 // Unused field.

namespace NovaEngine.Renderer.Vulkan.ShaderModels;

/// <summary>The rendering parameters for the MTSDF fragment shader.</summary>
internal struct MTSDFParameters
{
    /*********
    ** Fields
    *********/
    /// <summary>The pixel range that was used when generating the atlas in screen pixels.</summary>
    public float ScreenPixelRange;

    /// <summary>How the fill of the MTSDF should be rendered.</summary>
    public TextFillType FillType;

    /// <summary>How the border of the MTSDF should be rendered.</summary>
    public TextBorderType BorderType;

    /// <summary>The width (in pixels) of the border.</summary>
    public float BorderWidth;

    /// <summary>The power of the bloom.</summary>
    public float BloomPower;

    /// <summary>The brightness of the bloom.</summary>
    public float BloomBrightness;

    /// <summary>Unused.</summary>
    private Vector2<float> _Padding;

    /// <summary>The colour of the fill (if <see cref="FillType"/> is <see cref="TextFillType.Colour"/>).</summary>
    public Colour32 FillColour;

    /// <summary>The colour of the border (if the <see cref="BorderType"/> is <see cref="TextBorderType.Colour"/>).</summary>
    public Colour32 BorderColour;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="screenPixelRange">The pixel range that was used when generating the atlas in screen pixels.</param>
    /// <param name="fillType">How the fill (inside) of the MTSDF should be rendered.</param>
    /// <param name="borderType">How the border of the MTSDF should be rendered.</param>
    /// <param name="borderWidth">The width (in pixels) of the border.</param>
    /// <param name="bloomPower">The power of the bloom.</param>
    /// <param name="bloomBrightness">The brightness of the bloom.</param>
    /// <param name="fillColour">The colour of the fill (inside) (if <paramref name="fillType"/> is <see cref="TextFillType.Colour"/>).</param>
    /// <param name="borderColour">The colour of the border (if the <paramref name="borderType"/> is <see cref="TextBorderType.Colour"/>).</param>
    public MTSDFParameters(float screenPixelRange, TextFillType fillType, TextBorderType borderType, float borderWidth, float bloomPower, float bloomBrightness, Colour32 fillColour, Colour32 borderColour)
    {
        ScreenPixelRange = screenPixelRange;
        FillType = fillType;
        BorderType = borderType;
        BorderWidth = borderWidth;
        BloomPower = bloomPower;
        BloomBrightness = bloomBrightness;
        FillColour = fillColour;
        BorderColour = borderColour;
    }
}

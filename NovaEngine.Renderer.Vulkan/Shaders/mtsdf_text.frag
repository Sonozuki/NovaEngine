#version 450

/*********
** Constants
*********/
// The no fill type.
const uint FILL_TYPE_NONE = 0;

// The solid colour fill type.
const uint FILL_TYPE_COLOUR = 1;

// The no border type.
const uint BORDER_TYPE_NONE = 0;

// The solid colour border type.
const uint BORDER_TYPE_COLOUR = 1;

// The texture border type.
const uint BORDER_TYPE_TEXTURE = 2;


/*********
** Layouts
*********/
// The texture coordinate in the FontAtlasSampler for this fragment.
layout(location = 0) in vec2 inTextureCoordinate;

// The final fragment colour.
layout(location = 0) out vec4 outColour;

// The sampler to the MTSDF font atlas
layout(binding = 1) uniform sampler2D FontAtlasSampler;

// The sampler to the border texture.
layout(binding = 2) uniform sampler1D BorderTextureSampler;

// The parameters for rendering the MTSDF.
layout(push_constant) uniform PushConstants
{
    // The range that was used when generating the atlas in screen pixels.
    float ScreenPixelRange;

    // How the fill (inside) should be rendered (FILL_TYPE_*).
    uint FillType;
    
    // How the border should be rendered (BORDER_TYPE_*).
    uint BorderType;
    
    // The width (in pixels) of the border.
    float BorderWidth;

    // The colour of the fill (if FillType is FILL_TYPE_COLOUR).
    vec4 FillColour;
    
    // The colour of the border (if the BorderType is BORDER_TYPE_COLOUR).
    vec4 BorderColour;
} Params;


/*********
** Functions
*********/
// Retrieves the median (middle value) channel.
float median(float r, float g, float b)
{
    return max(min(r, g), min(max(r, g), b));
}

// The shader entry point.
void main()
{
    // calculate signed distance
    vec4 mtsdf = texture(FontAtlasSampler, inTextureCoordinate);
    float signedDistance = median(mtsdf.r, mtsdf.g, mtsdf.b);
    float screenPixelDistance = Params.ScreenPixelRange * (signedDistance - 0.5) + 0.5;

    // initial colour for outside the border, or the fill/border when *_TYPE_NONE is used
    outColour = vec4(0);

    // calculate fragment colour
    if (Params.BorderType != BORDER_TYPE_NONE && abs(screenPixelDistance) < Params.BorderWidth * 0.5) // fragment is part of the border
    {
        if (Params.BorderType == BORDER_TYPE_COLOUR)
            outColour = Params.BorderColour;
        else if (Params.BorderType == BORDER_TYPE_TEXTURE)
        {
            float t = screenPixelDistance / (Params.BorderWidth * 0.5);
            outColour = texture(BorderTextureSampler, (t + 1) * 0.5);
        }
    }
    else if (screenPixelDistance > 0) // fragment is "inside" (fill) of the MTSDF
    {
        if (Params.FillType == FILL_TYPE_COLOUR)
            outColour = Params.FillColour;
    }
}
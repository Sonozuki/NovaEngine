#version 450

layout(location = 0) in vec2 inTextureCoordinate;

layout(location = 0) out vec4 outColour;

// A sampler to the font atlas
layout(binding = 1) uniform sampler2D FontAtlasSampler;

void main()
{
    outColour = texture(FontAtlasSampler, inTextureCoordinate);
}
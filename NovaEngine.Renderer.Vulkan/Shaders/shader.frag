#version 450
#extension GL_ARB_separate_shader_objects : enable

layout(location = 0) in vec2 inTextureCoordinate;

layout(location = 0) out vec4 outColour;

layout(binding = 1) uniform sampler2D textureSampler;

void main() {
    outColour = texture(textureSampler, inTextureCoordinate);
}
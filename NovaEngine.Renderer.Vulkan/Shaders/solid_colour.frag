#version 450

layout(location = 0) out vec4 outColour;

layout(push_constant) uniform PushConstants
{
    vec3 colour;
} pushConstants;

void main()
{
    outColour = vec4(pushConstants.colour, 1);
}

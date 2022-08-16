#version 450

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec2 inTextureCoordinate;
layout(location = 2) in vec3 inNormal;

layout(location = 0) out vec2 outTextureCoordinate;

layout(binding = 0) uniform UniformBufferObject
{
    mat4 model;
    mat4 view;
    mat4 projection;
} ubo;

void main()
{
    outTextureCoordinate = inTextureCoordinate;

    gl_Position = ubo.projection * ubo.model * vec4(inPosition.x, inPosition.y, 0, 1);
}
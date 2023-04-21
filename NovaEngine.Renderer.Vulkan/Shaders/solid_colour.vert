#version 450

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec3 inNormal;
layout(location = 2) in vec2 inTextureCoordinate;

layout(binding = 0) uniform UniformBufferObject
{
    mat4 model;
    mat4 view;
    mat4 projection;
} ubo;

void main()
{
    gl_Position = ubo.projection * ubo.view * ubo.model * vec4(inPosition.x, inPosition.y, inPosition.z * -1, 1);
}

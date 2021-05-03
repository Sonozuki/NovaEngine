#version 450
#extension GL_ARB_separate_shader_objects : enable

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

void main() {
    gl_Position = vec4(inPosition, 1);

    outTextureCoordinate = inTextureCoordinate;
}
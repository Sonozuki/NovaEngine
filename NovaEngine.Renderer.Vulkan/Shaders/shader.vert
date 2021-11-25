#version 450

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec2 inTextureCoordinate;
layout(location = 2) in vec3 inNormal;

layout(location = 0) out vec3 outWorldPosition;
layout(location = 1) out vec2 outTextureCoordinate;
layout(location = 2) out vec3 outNormal;

layout(binding = 0) uniform UniformBufferObject
{
    mat4 model;
    mat4 view;
    mat4 projection;
    vec3 cameraPosition;
} ubo;

layout(push_constant) uniform PushConstants
{
    vec3 objectPosition;
};

void main()
{
    vec3 localPosition = vec3(ubo.model * vec4(inPosition, 1));

    outWorldPosition = localPosition + objectPosition;
    outTextureCoordinate = inTextureCoordinate;
    outNormal = inNormal;
    
    gl_Position = ubo.projection * ubo.view * vec4(outWorldPosition, 1);
}
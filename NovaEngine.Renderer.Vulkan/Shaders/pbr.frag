#version 450


// The size in the X and Y axis of a frustum tile.
const uint TILE_SIZE = 16;


/*********
** Structs
*********/
// Represents a light.
struct Light
{
    // The position of the light in world space (for point and spot lights).
    vec4 PositionWorldSpace;

    // The direction of the light in world space (for spot and directional lights).
    vec4 DirectionWorldSpace;

    // The position of the light in view space (for point and spot lights).
    vec4 PositionViewSpace;

    // The direction of the light in view space (for spot and directional lights).
    vec4 DirectionViewSpace;

    // The colour of the light.
    vec4 Colour;

    // The half angle of the spotlight cone.
    float SpotlightAngle;

    // The range of the light.
    float Range;

    // The intensity of the light.
    float Intensity;

    // The type of the light.
    uint Type;
};

layout(location = 0) in vec3 inWorldPosition;
layout(location = 1) in vec2 inTextureCoordinate;
layout(location = 2) in vec3 inNormal;

layout(location = 0) out vec4 outColour;

layout(binding = 0) uniform UBO
{
    mat4 model;
    mat4 view;
    mat4 projection;
} ubo;

// The parameters of the shader.
layout (binding = 1) uniform ParamsUniform
{
    // The inverse of the projection matrix, this is used to convert from screen space to view space.
    mat4 InverseProjection;

    // The current screen resolution, this is used to ensure the a thread isn't used if it's out of bounds of the screen.
    ivec2 ScreenResolution;

    // The number of horizontal tiles.
    uint NumberOfTilesWide;
} Params;

// The buffer containing the lights to cull.
layout (binding = 2) buffer LightsBuffer
{
    Light Lights[];
};

// The buffer to store the light indices for opaque geometry.
layout (binding = 3) buffer OpaqueLightIndexListBuffer
{
    uint OpaqueLightIndexList[];
};

// The buffer to store the light indices for transparent geometry.
layout (binding = 4) buffer TransparentLightIndexListBuffer
{
    uint TransparentLightIndexList[];
};

// The buffer to store the opaque light grid.
layout (binding = 5) buffer OpaqueLightGridBuffer
{
    uvec2 OpaqueLightGrid[];
};

// The buffer to store the opaque light grid.
layout (binding = 6) buffer TransparentLightGridBuffer
{
    uvec2 TransparentLightGrid[];
};

layout(push_constant) uniform PushConstants
{
    vec3 colour;
    float roughness;
    float metallicness;
} material;

const float PI = 3.14159265359;

// The normal distribution function
// This calculates the distribution of microfacets for the surface
float D_GGX(float dotNH, float roughness)
{
    float alpha = roughness * roughness;
    float alpha2 = alpha * alpha;

    float denominator = dotNH * dotNH * (alpha2 - 1) + 1;
    return alpha2 / (PI * denominator * denominator);
}

// The geometric shadowing function
// This calculates the microfacet shadowing
float G_SchlicksmithGGX(float dotNL, float dotNV, float roughness)
{
    float r = roughness + 1;
    float k = r * r / 8;
    float gLight = dotNL / (dotNL * (1 - k) + k);
    float gView = dotNV / (dotNV * (1 - k) + k);
    return gLight * gView;
}

// The fresnel function
// This calculates the reflectance depending on angle of incidence
vec3 F_Schlick(float cosTheta, float metallicness)
{
    vec3 F0 = mix(vec3(.04), material.colour, metallicness);
    return F0 + (1 - F0) * pow(1 - cosTheta, 5);
}

// Specular BRDF composition
vec3 BRDF(vec3 L, vec3 V, vec3 N, float metallicness, float roughness)
{
    // precompute
    vec3 H = normalize(V + L);
    float dotNV = clamp(dot(N, V), 0, 1);
    float dotNL = clamp(dot(N, L), 0, 1);
    float dotLH = clamp(dot(L, H), 0, 1);
    float dotNH = clamp(dot(N, H), 0, 1);

    if (dotNL <= 0) {
        return vec3(0);
    }

    // calculate colour    
    vec3 lightColour = vec3(1); // TODO: this should ofc not be fixed when lighting is properly done

    float D = D_GGX(dotNH, roughness); // distribution of microfacets
    float G = G_SchlicksmithGGX(dotNL, dotNV, roughness); // microfacet shadowing
    vec3 F = F_Schlick(dotNV, metallicness); // reflectance depending on angle of incidence

    vec3 specular = D * F * G / (4 * dotNL * dotNV);
    return specular * dotNL * lightColour;
}

void main()
{
//    vec3 N = normalize(inNormal);
//    vec3 V = normalize(ubo.cameraPosition - inWorldPosition);

    // specular contribution
//    vec3 Lo = vec3(0);
//    for (int i = 0; i < uboParams.lights.length(); i++) {
//        vec3 L = normalize(uboParams.lights[i].xyz - inWorldPosition);
//        Lo += BRDF(L, V, N, material.metallicness, material.roughness);
//    }

    // apply tint
//    vec3 colour = material.colour * 0.02;
//    colour += Lo;

    // apply gamma correction
//    colour = pow(colour, vec3(.4545));

    uvec2 tileIndex = uvec2(floor(gl_FragCoord.xy / TILE_SIZE));
    uint index = tileIndex.y * Params.NumberOfTilesWide + tileIndex.x;
    uint lightCount = OpaqueLightGrid[index].y;
    outColour = vec4(lightCount, 0, 0, 1);
    //outColour = vec4(material.colour, 1);
}
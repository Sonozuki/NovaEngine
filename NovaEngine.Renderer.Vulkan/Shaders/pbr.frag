#version 450

layout(location = 0) in vec3 inWorldPosition;
layout(location = 1) in vec2 inTextureCoordinate;
layout(location = 2) in vec3 inNormal;

layout(location = 0) out vec4 outColour;

layout(binding = 0) uniform UBO
{
    mat4 model;
    mat4 view;
    mat4 projection;
    vec3 cameraPosition;
} ubo;

layout (binding = 1) uniform UBOShared
{
	vec4 lights[4]; // TODO: this will not be a fixed length when proper clustered rendering is set up
} uboParams;

layout(push_constant) uniform PushConstants
{
    layout(offset = 12) float red;
    layout(offset = 16) float green;
    layout(offset = 20) float blue;
    layout(offset = 24) float roughness;
    layout(offset = 28) float metallicness;
} material;

const float PI = 3.14159265359;

vec3 getMaterialTint()
{
    return vec3(material.red, material.green, material.blue);
}

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
    vec3 F0 = mix(vec3(.04), getMaterialTint(), metallicness);
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
    vec3 N = normalize(inNormal);
    vec3 V = normalize(ubo.cameraPosition - inWorldPosition);

    // specular contribution
    vec3 Lo = vec3(0);
    for (int i = 0; i < uboParams.lights.length(); i++) {
        vec3 L = normalize(uboParams.lights[i].xyz - inWorldPosition);
        Lo += BRDF(L, V, N, material.metallicness, material.roughness);
    }

    // apply tint
    vec3 colour = getMaterialTint() * 0.02;
    colour += Lo;

    // apply gamma correction
    colour = pow(colour, vec3(.4545));

    outColour = vec4(colour, 1);
}
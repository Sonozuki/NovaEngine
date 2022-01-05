﻿using NovaEngine.Maths;

namespace NovaEngine.Renderer.Vulkan.ShaderModels;

/// <summary>Represents the parameters of the compute shaders.</summary>
internal struct ParametersUBO
{
    /*********
    ** Fields
    *********/
    /// <summary>The inverse projection matrix.</summary>
    public Matrix4x4 InverseProjection;

    /// <summary>The resolution of the render target.</summary>
    public Vector2I ScreenResolution;

    /// <summary>The number of lights to cull.</summary>
    public int NumberOfLights;
}
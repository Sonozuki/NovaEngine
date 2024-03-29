﻿#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace NovaEngine.Renderer.Vulkan.ShaderModels;

/// <summary>Represents a plane.</summary>
internal struct Plane
{
    /*********
    ** Fields
    *********/
    /// <summary>The normal of the plane.</summary>
    public Vector3<float> Normal;

    /// <summary>The distance to the origin.</summary>
    public float Distance;
}

namespace NovaEngine.Renderer.Vulkan.ShaderModels;

/// <summary>Represents a plane.</summary>
internal struct Plane
{
    /*********
    ** Fields
    *********/
    /// <summary>The normal of the plane.</summary>
    public Vector3 Normal;

    /// <summary>The distance to the origin.</summary>
    public float Distance;
}

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace NovaEngine.Renderer.Vulkan.ShaderModels;

/// <summary>Represents a frustum comprised of four planes (excluding the near and far planes).</summary>
internal struct Frustum
{
    /*********
    ** Fields
    *********/
    /// <summary>The top plane the frustum is comprised of.</summary>
    public Plane TopPlane;

    /// <summary>The left plane the frustum is comprised of.</summary>
    public Plane LeftPlane;

    /// <summary>The right plane the frustum is comprised of.</summary>
    public Plane RightPlane;

    /// <summary>The bottom plane the frustum is comprised of.</summary>
    public Plane BottomPlane;
}

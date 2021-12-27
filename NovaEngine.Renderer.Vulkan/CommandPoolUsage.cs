namespace NovaEngine.Renderer.Vulkan;

/// <summary>The ways a <see cref="VulkanCommandPool"/> can be used.</summary>
internal enum CommandPoolUsage
{
    /// <summary>The pool will be used for graphics operations.</summary>
    Graphics,

    /// <summary>The pool will be used for transfer operations.</summary>
    Transfer,

    /// <summary>The pool will be used for compute operations.</summary>
    Compute
}

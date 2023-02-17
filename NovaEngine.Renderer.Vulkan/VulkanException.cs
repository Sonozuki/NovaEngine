namespace NovaEngine.Renderer.Vulkan;

/// <summary>The exception that is thrown when an error occurs in a Vulkan renderer.</summary>
internal sealed class VulkanException : RendererException
{
    /*********
    ** Constructors
    *********/
    /// <summary>Contructs an instance.</summary>
    public VulkanException() { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    public VulkanException(string? message)
        : base(message) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public VulkanException(string? message, Exception? innerException = null)
        : base(message, innerException) { }
}

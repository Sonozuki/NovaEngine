using NovaEngine.Graphics;
using NovaEngine.Rendering;
using System;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan;

/// <summary>Contains useful miscellaneous methods for rendering with Vulkan.</summary>
internal unsafe static class VulkanUtilities
{
    /*********
    ** Accessors
    *********/
    /// <summary>The attribute descriptions for a vertex.</summary>
    /// <remarks>Describes how to extract the vertex attributes from a chunk of vertex data from the <see cref="VertexBindingDescription"/>.</remarks>
    public static VkVertexInputAttributeDescription[] VertexAttributeDesciptions { get; } =
        new VkVertexInputAttributeDescription[]
        {
            new() { Location = 0, Binding = 0, Format = VkFormat.R32G32B32SFloat, Offset = 0 },
            new() { Location = 1, Binding = 0, Format = VkFormat.R32G32SFloat, Offset = 3 * sizeof(float) },
            new() { Location = 2, Binding = 0, Format = VkFormat.R32G32B32SFloat, Offset = 5 * sizeof(float) }
        };

    /// <summary>The binding description for a vertex.</summary>
    /// <remarks>This specifies the number of bytes between data entries and whether to move to the next data entry after each vertex of after each instance.</remarks>
    public static VkVertexInputBindingDescription[] VertexBindingDescription { get; } =
        new VkVertexInputBindingDescription[]
        {
            new() { Binding = 0, Stride = (uint)sizeof(Vertex), InputRate = VkVertexInputRate.Vertex }
        };


    /*********
    ** Public Methods
    *********/
    /// <summary>Converts a <see cref="SampleCount"/> to the corresponding <see cref="VkSampleCountFlags"/>.</summary>
    /// <param name="sampleCount">The sample count to convert.</param>
    /// <returns>The converted <paramref name="sampleCount"/>.</returns>
    public static VkSampleCountFlags ConvertSampleCount(SampleCount sampleCount) =>
        sampleCount switch
        {
            SampleCount.Count64 => VkSampleCountFlags.Count64,
            SampleCount.Count32 => VkSampleCountFlags.Count32,
            SampleCount.Count16 => VkSampleCountFlags.Count16,
            SampleCount.Count8 => VkSampleCountFlags.Count8,
            SampleCount.Count4 => VkSampleCountFlags.Count4,
            SampleCount.Count2 => VkSampleCountFlags.Count2,
            _ => VkSampleCountFlags.Count1,
        };

    /// <summary>Converts a <see cref="VkSampleCountFlags"/> to the corresponding <see cref="SampleCount"/>.</summary>
    /// <param name="sampleCount">The sample count to convert.</param>
    /// <returns>The converted <paramref name="sampleCount"/>.</returns>
    public static SampleCount ConvertSampleCount(VkSampleCountFlags sampleCount) =>
        sampleCount switch
        {
            VkSampleCountFlags when sampleCount.HasFlag(VkSampleCountFlags.Count64) => SampleCount.Count64,
            VkSampleCountFlags when sampleCount.HasFlag(VkSampleCountFlags.Count32) => SampleCount.Count32,
            VkSampleCountFlags when sampleCount.HasFlag(VkSampleCountFlags.Count16) => SampleCount.Count16,
            VkSampleCountFlags when sampleCount.HasFlag(VkSampleCountFlags.Count8) => SampleCount.Count8,
            VkSampleCountFlags when sampleCount.HasFlag(VkSampleCountFlags.Count4) => SampleCount.Count4,
            VkSampleCountFlags when sampleCount.HasFlag(VkSampleCountFlags.Count2) => SampleCount.Count2,
            _ => SampleCount.Count1
        };

    /// <summary>Creates a vertex buffer.</summary>
    /// <param name="vertices">The vertex data to populate the vertex buffer with.</param>
    public static VulkanBuffer CreateVertexBuffer(Span<Vertex> vertices)
    {
        var bufferSize = vertices.Length * sizeof(Vertex);

        // create staging buffer and copy verex data to it
        using var stagingBuffer = new VulkanBuffer(bufferSize, VkBufferUsageFlags.TransferSource, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);
        stagingBuffer.CopyFrom(vertices);

        // create vertex buffer and copy staging buffer to it
        var vertexBuffer = new VulkanBuffer(bufferSize, VkBufferUsageFlags.TransferDestination | VkBufferUsageFlags.VertexBuffer, VkMemoryPropertyFlags.DeviceLocal);
        vertexBuffer.CopyFrom(stagingBuffer);

        return vertexBuffer;
    }

    /// <summary>Creates an index buffer.</summary>
    /// <param name="indices">The index data to populate the index buffer with.</param>
    public static VulkanBuffer CreateIndexBuffer(Span<uint> indices)
    {
        var bufferSize = indices.Length * sizeof(uint);

        // create staging buffer and copy index data to it
        using var stagingBuffer = new VulkanBuffer(bufferSize, VkBufferUsageFlags.TransferSource, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);
        stagingBuffer.CopyFrom(indices);

        // create index buffer and copy staging buffer to it
        var indexBuffer = new VulkanBuffer(bufferSize, VkBufferUsageFlags.TransferDestination | VkBufferUsageFlags.IndexBuffer, VkMemoryPropertyFlags.DeviceLocal);
        indexBuffer.CopyFrom(stagingBuffer);

        return indexBuffer;
    }
}

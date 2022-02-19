using NovaEngine.Extensions;
using NovaEngine.Logging;
using System;
using System.Runtime.InteropServices;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan;

/// <summary>Encapsulates a <see cref="VkBuffer"/>.</summary>
internal unsafe class VulkanBuffer : IDisposable
{
    /*********
    ** Fields
    *********/
    /// <summary>The memory for <see cref="NativeBuffer"/>.</summary>
    private readonly VkDeviceMemory NativeMemory;

    /// <summary>The properties of the buffer memory.</summary>
    private readonly VkMemoryPropertyFlags MemoryProperties;

    /// <summary>The command pool that will be used for the transfer operations.</summary>
    private readonly VulkanCommandPool TransferCommandPool;


    /*********
    ** Accessors
    *********/
    /// <summary>The size of <see cref="NativeBuffer"/>.</summary>
    public VkDeviceSize Size { get; }

    /// <summary>The Vulkan buffer.</summary>
    public VkBuffer NativeBuffer { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="size">The size of the buffer.</param>
    /// <param name="usageFlags">The usage of the buffer.</param>
    /// <param name="memoryProperties">The memory properties of the buffer.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="size"/> is 0.</exception>
    /// <exception cref="ApplicationException">Thrown if the buffer couldn't be created or the memory couldn't be allocated or bound.</exception>
    public VulkanBuffer(VkDeviceSize size, VkBufferUsageFlags usageFlags, VkMemoryPropertyFlags memoryProperties)
    {
        if (size == 0)
            throw new ArgumentOutOfRangeException(nameof(size), "Must be at least 1");

        Size = size;
        MemoryProperties = memoryProperties;

        // create buffer
        var bufferCreateInfo = new VkBufferCreateInfo
        {
            SType = VkStructureType.BufferCreateInfo,
            Size = Size,
            Usage = usageFlags,
            SharingMode = VkSharingMode.Exclusive
        };

        if (VK.CreateBuffer(VulkanRenderer.Instance.Device.NativeDevice, ref bufferCreateInfo, null, out var nativeBuffer) != VkResult.Success)
            throw new ApplicationException("Failed to create buffer.").Log(LogSeverity.Fatal);
        NativeBuffer = nativeBuffer;

        // allocate & bind buffer memory
        VK.GetBufferMemoryRequirements(VulkanRenderer.Instance.Device.NativeDevice, nativeBuffer, out var memoryRequirements);

        var memoryAllocateInfo = new VkMemoryAllocateInfo
        {
            SType = VkStructureType.MemoryAllocateInfo,
            AllocationSize = memoryRequirements.Size,
            MemoryTypeIndex = VulkanRenderer.Instance.Device.GetMemoryTypeIndex(memoryRequirements.MemoryTypeBits, MemoryProperties)
        };

        if (VK.AllocateMemory(VulkanRenderer.Instance.Device.NativeDevice, ref memoryAllocateInfo, null, out NativeMemory) != VkResult.Success)
            throw new ApplicationException("Failed to allocate buffer memory.").Log(LogSeverity.Fatal);

        if (VK.BindBufferMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeBuffer, NativeMemory, 0) != VkResult.Success)
            throw new ApplicationException("Failed to bind buffer memory.").Log(LogSeverity.Fatal);

        // create a command pool for the transfer commands
        TransferCommandPool = new(CommandPoolUsage.Transfer, VkCommandPoolCreateFlags.Transient);
    }

    /// <summary>Copies data to the buffer.</summary>
    /// <typeparam name="T">The type of the data to copy to the buffer.</typeparam>
    /// <param name="data">The data to copy to the buffer.</param>
    public void CopyFrom<T>(T data)
        where T : unmanaged
    {
        if (sizeof(T) > Size)
            throw new InvalidOperationException($"{nameof(data)} is bigger (byte size) than the buffer.");

        if (MemoryProperties.HasFlag(VkMemoryPropertyFlags.HostVisible))
        {
            // copy data to buffer
            void* bufferPointer;
            VK.MapMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeMemory, 0, sizeof(T), 0, &bufferPointer);
            Marshal.StructureToPtr(data, (IntPtr)bufferPointer, false);
            VK.UnmapMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeMemory);
        }
        else
        {
            // copy data to buffer using a staging buffer
            using var stagingBuffer = new VulkanBuffer(sizeof(T), VkBufferUsageFlags.TransferSource | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);
            stagingBuffer.CopyFrom(data);
            stagingBuffer.CopyTo(this);
        }
    }

    /// <summary>Copies data to the buffer.</summary>
    /// <typeparam name="T">The type of the data to copy to the buffer.</typeparam>
    /// <param name="data">The data to copy to the buffer.</param>
    /// <exception cref="InvalidOperationException">Thrown if <paramref name="data"/> is bigger (<see langword="byte"/> size) than the buffer.</exception>
    public void CopyFrom<T>(Span<T> data)
        where T : unmanaged
    {
        if (data.Length * sizeof(T) > Size)
            throw new InvalidOperationException($"{nameof(data)} is bigger (byte size) than the buffer.");

        if (MemoryProperties.HasFlag(VkMemoryPropertyFlags.HostVisible))
        {
            // copy data to buffer
            void* bufferPointer;
            VK.MapMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeMemory, 0, data.Length * sizeof(T), 0, &bufferPointer);
            fixed (void* dataPointer = &data.GetPinnableReference())
                Buffer.MemoryCopy(dataPointer, bufferPointer, Size, data.Length * sizeof(T));
            VK.UnmapMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeMemory);
        }
        else
        {
            // copy data to buffer using staging buffer
            using var stagingBuffer = new VulkanBuffer(data.Length * sizeof(T), VkBufferUsageFlags.TransferSource | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);
            stagingBuffer.CopyFrom(data);
            stagingBuffer.CopyTo(this);
        }
    }

    /// <summary>Copies the buffer to another <see cref="VulkanBuffer"/>.</summary>
    /// <param name="buffer">The buffer to copy this buffer to.</param>
    public void CopyFrom(VulkanBuffer buffer)
    {
        // copy buffer to buffer
        var commandBuffer = TransferCommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);
        var bufferCopy = new VkBufferCopy { SourceOffset = 0, DestinationOffset = 0, Size = Size };
        VK.CommandCopyBuffer(commandBuffer, buffer.NativeBuffer, NativeBuffer, 1, new[] { bufferCopy });
        TransferCommandPool.SubmitCommandBuffer(true, commandBuffer);
    }

    /// <summary>Copies a <see cref="VulkanTexture"/> to the buffer.</summary>
    /// <param name="texture">The texture to copy to the buffer.</param>
    public void CopyFrom(VulkanTexture texture)
    {
        // copy image to buffer
        var commandBuffer = TransferCommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);

        var bufferImageCopy = new VkBufferImageCopy
        {
            BufferOffset = 0,
            BufferRowLength = 0,
            BufferImageHeight = 0,
            ImageSubresource = new(texture.AspectFlags, 0, 0, texture.LayerCount),
            ImageOffset = VkOffset3D.Zero,
            ImageExtent = new(texture.Width, texture.Height, texture.Depth)
        };

        VK.CommandCopyImageToBuffer(commandBuffer, texture.NativeImage, VkImageLayout.TransferSourceOptimal, NativeBuffer, 1, new[] { bufferImageCopy });

        TransferCommandPool.SubmitCommandBuffer(true, commandBuffer);
    }

    /// <summary>Copies the buffer to a span.</summary>
    /// <typeparam name="T">The type of the data to copy from the buffer.</typeparam>
    /// <param name="data">The span to populate with the buffer data.</param>
    /// <exception cref="InvalidOperationException">Thrown if <paramref name="data"/> is smaller (<see langword="byte"/> size) than the buffer.</exception>
    public void CopyTo<T>(Span<T> data)
        where T : unmanaged
    {
        if (data.Length * sizeof(T) < Size)
            throw new InvalidOperationException($"{nameof(data)} is smaller (byte size) than the buffer.");

        if (MemoryProperties.HasFlag(VkMemoryPropertyFlags.HostVisible))
        {
            // copy data from buffer
            void* bufferPointer;
            VK.MapMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeMemory, 0, data.Length * sizeof(T), 0, &bufferPointer);
            fixed (void* dataPointer = &data.GetPinnableReference())
                Buffer.MemoryCopy(bufferPointer, dataPointer, data.Length * sizeof(T), Size);
            VK.UnmapMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeMemory);
        }
        else
        {
            // copy data from buffer using staging buffer
            using var stagingBuffer = new VulkanBuffer(data.Length * sizeof(T), VkBufferUsageFlags.TransferSource | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);
            stagingBuffer.CopyFrom(this);
            stagingBuffer.CopyTo(data);
        }
    }

    /// <summary>Copies the buffer to another <see cref="VulkanBuffer"/>.</summary>
    /// <param name="buffer">The buffer to copy this buffer to.</param>
    public void CopyTo(VulkanBuffer buffer)
    {
        // copy buffer to buffer
        var commandBuffer = TransferCommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);
        var bufferCopy = new VkBufferCopy { SourceOffset = 0, DestinationOffset = 0, Size = Size };
        VK.CommandCopyBuffer(commandBuffer, NativeBuffer, buffer.NativeBuffer, 1, new[] { bufferCopy });
        TransferCommandPool.SubmitCommandBuffer(true, commandBuffer);
    }

    /// <summary>Copies the buffer to a <see cref="VulkanTexture"/>.</summary>
    /// <param name="texture">The texture to copy this buffer to.</param>
    public void CopyTo(VulkanTexture texture)
    {
        // copy buffer to image
        var commandBuffer = TransferCommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);

        var bufferImageCopy = new VkBufferImageCopy
        {
            BufferOffset = 0,
            BufferRowLength = 0,
            BufferImageHeight = 0,
            ImageSubresource = new(texture.AspectFlags, 0, 0, texture.LayerCount),
            ImageOffset = VkOffset3D.Zero,
            ImageExtent = new(texture.Width, texture.Height, texture.Depth)
        };
        
        VK.CommandCopyBufferToImage(commandBuffer, NativeBuffer, texture.NativeImage, VkImageLayout.TransferDestinationOptimal, 1, new[] { bufferImageCopy });

        TransferCommandPool.SubmitCommandBuffer(true, commandBuffer);
    }

    /// <summary>Disposes unmanaged buffer resources.</summary>
    public void Dispose()
    {
        TransferCommandPool.Dispose();
        VK.DestroyBuffer(VulkanRenderer.Instance.Device.NativeDevice, NativeBuffer, null);
        VK.FreeMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeMemory, null);
    }
}

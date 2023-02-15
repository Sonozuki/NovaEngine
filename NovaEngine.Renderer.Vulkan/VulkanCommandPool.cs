namespace NovaEngine.Renderer.Vulkan;

/// <summary>Encapsulate a <see cref="VkCommandPool"/>.</summary>
internal unsafe class VulkanCommandPool : IDisposable
{
    /*********
    ** Fields
    *********/
    /// <summary>The <see cref="VkQueue"/> that <see cref="VkCommandBuffer"/>s will be submitted to.</summary>
    private readonly VkQueue Queue;

    /// <summary>The Vulkan command pool.</summary>
    private readonly VkCommandPool NativeCommandPool;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="usage">How the <see cref="VkCommandPool"/> will be used.</param>
    /// <param name="commandPoolCreateFlags">The create flags of the command pool creation.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="usage"/> isn't valid.</exception>
    /// <exception cref="ApplicationException">Thrown if the command pool couldn't be created.</exception>
    public VulkanCommandPool(CommandPoolUsage usage, VkCommandPoolCreateFlags? commandPoolCreateFlags = null)
    {
        // determine which queue family should be used based on usage
        var indices = new QueueFamilyIndices(VulkanRenderer.Instance.Device.NativePhysicalDevice);
        uint queueFamilyIndex;
        (queueFamilyIndex, Queue) = usage switch
        {
            CommandPoolUsage.Graphics => (indices.GraphicsFamily, VulkanRenderer.Instance.Device.GraphicsQueue),
            CommandPoolUsage.Transfer => (indices.TransferFamily, VulkanRenderer.Instance.Device.TransferQueue),
            CommandPoolUsage.Compute => (indices.ComputeFamily, VulkanRenderer.Instance.Device.ComputeQueue),
            _ => throw new ArgumentException($"Command pool usage: {usage} isn't valid")
        };

        // create command pool
        var commandPoolCreateInfo = new VkCommandPoolCreateInfo
        {
            SType = VkStructureType.CommandPoolCreateInfo,
            QueueFamilyIndex = queueFamilyIndex,
            Flags = commandPoolCreateFlags ?? 0
        };

        if (VK.CreateCommandPool(VulkanRenderer.Instance.Device.NativeDevice, ref commandPoolCreateInfo, null, out var nativeCommandPool) != VkResult.Success)
            throw new ApplicationException("Failed to create command pool.").Log(LogSeverity.Fatal);
        NativeCommandPool = nativeCommandPool;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Allocates a command buffer from the command pool.</summary>
    /// <param name="beginCommandBuffer">Whether the command buffer should begin recording commands automatically.</param>
    /// <param name="commandBufferUsageFlags">The <see cref="VkCommandBufferUsageFlags"/> to use when starting the command buffer (this is only used if <paramref name="beginCommandBuffer"/> is <see langword="true"/>).</param>
    /// <returns>A command buffer.</returns>
    /// <exception cref="ApplicationException">Thrown if the command buffer couldn't be allocated or started (if <paramref name="beginCommandBuffer"/> is <see langword="true"/>).</exception>
    public VkCommandBuffer AllocateCommandBuffer(bool beginCommandBuffer, VkCommandBufferUsageFlags? commandBufferUsageFlags = null)
    {
        // allocate command buffer
        var commandBufferAllocateInfo = new VkCommandBufferAllocateInfo
        {
            SType = VkStructureType.CommandBufferAllocateInfo,
            CommandPool = NativeCommandPool,
            CommandBufferCount = 1,
            Level = VkCommandBufferLevel.Primary
        };

        var commandBuffers = new VkCommandBuffer[1];
        if (VK.AllocateCommandBuffers(VulkanRenderer.Instance.Device.NativeDevice, ref commandBufferAllocateInfo, commandBuffers) != VkResult.Success)
            throw new ApplicationException("Failed to allocate command buffer.").Log(LogSeverity.Fatal);
        var commandBuffer = commandBuffers[0];

        // begin and return command buffer
        if (beginCommandBuffer)
        {
            var commandBufferBeginInfo = new VkCommandBufferBeginInfo
            {
                SType = VkStructureType.CommandBufferBeginInfo,
                Flags = commandBufferUsageFlags == null ? 0 : commandBufferUsageFlags.Value
            };

            if (VK.BeginCommandBuffer(commandBuffer, &commandBufferBeginInfo) != VkResult.Success)
                throw new ApplicationException("Failed to start command buffer.").Log(LogSeverity.Fatal);
        }

        return commandBuffer;
    }

    /// <summary>Submits and frees a command buffer.</summary>
    /// <param name="endCommandBuffer">Whether the command buffer should stop recording commands.</param>
    /// <param name="commandBuffer">The command buffer to submit and free.</param>
    /// <param name="freeCommandBuffer">Whether the command buffer should be freed after it has been submitted.</param>
    /// <param name="waitSemaphores">The semaphores to wait on before executing the command buffer.</param>
    /// <param name="waitDestinationStageMask">The pipeline stages at which each corresponding semaphore wait will occur.</param>
    /// <param name="signalSemaphores">The semaphores to signal when the command buffer finishes execution.</param>
    /// <param name="signalFence">The fence to signal when the command buffer finishes execution.</param>
    /// <exception cref="ApplicationException">Thrown if the command buffer couldn't be ended, submited, or the queue couldn't be waited.</exception>
    public void SubmitCommandBuffer(bool endCommandBuffer, VkCommandBuffer commandBuffer, bool freeCommandBuffer = true, VkSemaphore[]? waitSemaphores = null, VkPipelineStageFlags[]? waitDestinationStageMask = null, VkSemaphore[]? signalSemaphores = null, VkFence? signalFence = null)
    {
        // end recording commands
        if (endCommandBuffer)
            if (VK.EndCommandBuffer(commandBuffer) != VkResult.Success)
                throw new ApplicationException("Failed to end command buffer.").Log(LogSeverity.Fatal);

        waitSemaphores ??= Array.Empty<VkSemaphore>();
        waitDestinationStageMask ??= Array.Empty<VkPipelineStageFlags>();
        signalSemaphores ??= Array.Empty<VkSemaphore>();

        fixed (VkSemaphore* waitSemaphoresPointer = waitSemaphores)
        fixed (VkPipelineStageFlags* waitDestinationStageMaskPointer = waitDestinationStageMask)
        fixed (VkSemaphore* signalSemaphoresPointer = signalSemaphores)
        {
            // submit command buffer
            var submitInfo = new VkSubmitInfo
            {
                SType = VkStructureType.SubmitInfo,
                CommandBufferCount = 1,
                CommandBuffers = &commandBuffer,
                WaitSemaphoreCount = (uint)waitSemaphores.Length,
                WaitSemaphores = waitSemaphoresPointer,
                WaitDestinationStageMask = waitDestinationStageMaskPointer,
                SignalSemaphoreCount = (uint)signalSemaphores.Length,
                SignalSemaphores = signalSemaphoresPointer
            };

            if (VK.QueueSubmit(Queue, 1, new[] { submitInfo }, VkFence.Null) != VkResult.Success)
                throw new ApplicationException("Failed to submit command buffer.").Log(LogSeverity.Fatal);

            if (VK.QueueWaitIdle(Queue) != VkResult.Success)
                throw new ApplicationException("Failed to queue wait idle.").Log(LogSeverity.Fatal);
        }

        // free command buffer
        if (freeCommandBuffer)
            VK.FreeCommandBuffers(VulkanRenderer.Instance.Device.NativeDevice, NativeCommandPool, 1, new[] { commandBuffer });
    }

    /// <summary>Frees command buffers.</summary>
    /// <param name="commandBuffers">The command buffers to free.</param>
    public void FreeCommandBuffers(VkCommandBuffer[] commandBuffers) => VK.FreeCommandBuffers(VulkanRenderer.Instance.Device.NativeDevice, NativeCommandPool, (uint)commandBuffers.Length, commandBuffers);

    /// <inheritdoc/>
    public void Dispose() => VK.DestroyCommandPool(VulkanRenderer.Instance.Device.NativeDevice, NativeCommandPool, null);
}

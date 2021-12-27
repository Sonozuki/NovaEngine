using Vulkan;

namespace NovaEngine.Renderer.Vulkan;

/// <summary>Contains the indices for the required queue families.</summary>
internal struct QueueFamilyIndices
{
    /*********
    ** Fields
    *********/
    /// <summary>The index of the graphics queue family.</summary>
    private readonly uint? _GraphicsFamily;

    /// <summary>The index of the transfer queue family.</summary>
    private readonly uint? _TransferFamily;

    /// <summary>The index of the compute queue family.</summary>
    private readonly uint? _ComputeFamily;


    /*********
    ** Accessors
    *********/
    /// <summary>The index of the graphics queue family.</summary>
    public uint GraphicsFamily => _GraphicsFamily ?? default;

    /// <summary>The index of the transfer queue family.</summary>
    public uint TransferFamily => _TransferFamily ?? default;

    /// <summary>The index of the compute queue family.</summary>
    public uint ComputeFamily => _ComputeFamily ?? default;

    /// <summary>Whether all the required queue family indices are available.</summary>
    public bool IsComplete => _GraphicsFamily != null && _TransferFamily != null && _ComputeFamily != null;


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="physicalDevice">The physical device to get the queue family indices from.</param>
    public QueueFamilyIndices(VkPhysicalDevice physicalDevice)
    {
        _GraphicsFamily = null;
        _TransferFamily = null;
        _ComputeFamily = null;

        var queueFamilyCount = 0u;
        VK.GetPhysicalDeviceQueueFamilyProperties(physicalDevice, ref queueFamilyCount, null);

        var queueFamilies = new VkQueueFamilyProperties[queueFamilyCount];
        VK.GetPhysicalDeviceQueueFamilyProperties(physicalDevice, ref queueFamilyCount, queueFamilies);

        for (uint i = 0; i < queueFamilies.Length; i++)
        {
            var queueFamily = queueFamilies[i];

            // get the index of the first queue family that supports graphics operations
            if (_GraphicsFamily == null && (queueFamily.QueueFlags & VkQueueFlags.Graphics) != 0)
                _GraphicsFamily = i;

            // get the index of the first queue family that supports transfer operations but not graphics or compute operations
            if (_TransferFamily == null && (queueFamily.QueueFlags & VkQueueFlags.Transfer) != 0 && (queueFamily.QueueFlags & VkQueueFlags.Graphics) == 0 && (queueFamily.QueueFlags & VkQueueFlags.Compute) == 0)
                _TransferFamily = i;

            // get the index of the first queue family that supports compute operations but not graphics operations
            if (_ComputeFamily == null && (queueFamily.QueueFlags & VkQueueFlags.Compute) != 0 && (queueFamily.QueueFlags & VkQueueFlags.Graphics) == 0)
                _ComputeFamily = i;
        }

        // ensure a transfer and compute queue family were found, otherwise check again but allow for graphics queue families
        if (_TransferFamily == null || _ComputeFamily == null)
            for (uint i = 0; i < queueFamilies.Length; i++)
            {
                var queueFamily = queueFamilies[i];

                // get the index of the first queue family that supports transfer operations or graphics operations
                if (_TransferFamily == null && ((queueFamily.QueueFlags & VkQueueFlags.Transfer) != 0 || (queueFamily.QueueFlags & VkQueueFlags.Graphics) != 0))
                    _TransferFamily = i;

                // get the index of the first queue family that supports compute operations
                if (_ComputeFamily == null && (queueFamily.QueueFlags & VkQueueFlags.Compute) != 0)
                    _ComputeFamily = i;
            }
    }
}

using System.Collections.Generic;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan;

/// <summary>Encapsulates a <see cref="VkDescriptorSet"/>.</summary>
internal unsafe class VulkanDescriptorSet
{
    /*********
    ** Fields
    *********/
    /// <summary>The list of descriptor writes that have yet to be updated.</summary>
    private readonly List<VkWriteDescriptorSet> DescriptorWrites = new();


    /*********
    ** Accessors
    *********/
    /// <summary>The underlying descriptor set.</summary>
    public VkDescriptorSet NativeDescriptorSet { get; private set; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="nativeDescriptorSet">The underlying descriptor set.</param>
    public VulkanDescriptorSet(VkDescriptorSet nativeDescriptorSet)
    {
        NativeDescriptorSet = nativeDescriptorSet;
    }

    /// <summary>Binds a buffer info.</summary>
    /// <param name="destinationBinding">Where to bind <paramref name="bufferInfo"/>.</param>
    /// <param name="bufferInfo">The buffer info to bind to <paramref name="destinationBinding"/>.</param>
    /// <param name="bufferType">The type of the buffer being written to.</param>
    /// <returns>The current instance, used for chaining calls.</returns>
    /// <remarks>This doesn't update the binding immediately, call <see cref="UpdateBindings"/> to update all pending bindings.</remarks>
    public VulkanDescriptorSet Bind(uint destinationBinding, VkDescriptorBufferInfo* bufferInfo, VkDescriptorType bufferType)
    {
        DescriptorWrites.Add(new VkWriteDescriptorSet()
        {
            SType = VkStructureType.WriteDescriptorSet,
            DestinationSet = NativeDescriptorSet,
            DestinationBinding = destinationBinding,
            DestinationArrayElement = 0,
            DescriptorType = bufferType,
            DescriptorCount = 1,
            BufferInfo = bufferInfo
        });

        return this;
    }

    /// <summary>Binds an image info.</summary>
    /// <param name="destinationBinding">Where to bind <paramref name="imageInfo"/>.</param>
    /// <param name="imageInfo">The image info to bind <paramref name="destinationBinding"/> to.</param>
    /// <returns>The current instance, used for chaining calls.</returns>
    /// <remarks>This doesn't update the binding immediately, call <see cref="UpdateBindings"/> to update all pending bindings.</remarks>
    public VulkanDescriptorSet Bind(uint destinationBinding, VkDescriptorImageInfo* imageInfo)
    {
        DescriptorWrites.Add(new VkWriteDescriptorSet()
        {
            SType = VkStructureType.WriteDescriptorSet,
            DestinationSet = NativeDescriptorSet,
            DestinationBinding = destinationBinding,
            DestinationArrayElement = 0,
            DescriptorType = VkDescriptorType.CombinedImageSampler,
            DescriptorCount = 1,
            ImageInfo = imageInfo
        });

        return this;
    }

    /// <summary>Updates all pending bindings.</summary>
    public void UpdateBindings()
    {
        VK.UpdateDescriptorSets(VulkanRenderer.Instance.Device.NativeDevice, (uint)DescriptorWrites.Count, DescriptorWrites.ToArray(), 0, null);
        DescriptorWrites.Clear();
    }
}

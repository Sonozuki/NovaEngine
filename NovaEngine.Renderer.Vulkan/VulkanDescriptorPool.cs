using NovaEngine.Extensions;
using NovaEngine.Logging;
using System;
using System.Collections.Generic;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan;

/// <summary>Encapsulates a <see cref="VkDescriptorPool"/>.</summary>
internal unsafe class VulkanDescriptorPool : IDisposable
{
    /*********
    ** Constants
    *********/
    /// <summary>The max number of sets that a pool can allocate.</summary>
    private uint MaxNumberOfSets = 256;


    /*********
    ** Fields
    *********/
    /// <summary>The descriptor set layout of the descriptor pool.</summary>
    private readonly VkDescriptorSetLayout NativeDescriptorSetLayout;

    /// <summary>The descriptor sets to reuse.</summary>
    private readonly Queue<VulkanDescriptorSet> ReusableDescriptorSets = new();

    /// <summary>The underlying descriptor pool.</summary>
    private readonly VkDescriptorPool NativeDescriptorPool;


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    public VulkanDescriptorPool(VkDescriptorSetLayout descriptorSetLayout)
    {
        NativeDescriptorSetLayout = descriptorSetLayout;

        // TODO: create a VulkanDescriptorSetLayout that contains the relevant info on what the pool sizes should be
        var poolSizes = new VkDescriptorPoolSize[]
        {
            new() { Type = VkDescriptorType.UniformBuffer, DescriptorCount = 10 * MaxNumberOfSets },
            new() { Type = VkDescriptorType.CombinedImageSampler, DescriptorCount = 10 * MaxNumberOfSets },
            new() { Type = VkDescriptorType.StorageBuffer, DescriptorCount = 10 * MaxNumberOfSets }
        };

        fixed (VkDescriptorPoolSize* poolSizesPointer = poolSizes)
        {
            var descriptorPoolCreateInfo = new VkDescriptorPoolCreateInfo() // TODO: dynamically expand / shrink descriptor pool
            {
                SType = VkStructureType.DescriptorPoolCreateInfo,
                MaxSets = MaxNumberOfSets,
                PoolSizeCount = (uint)poolSizes.Length,
                PoolSizes = poolSizesPointer
            };

            if (VK.CreateDescriptorPool(VulkanRenderer.Instance.Device.NativeDevice, ref descriptorPoolCreateInfo, null, out var descriptorPool) != VkResult.Success)
                throw new ApplicationException("Failed to create descriptor pool.").Log(LogSeverity.Fatal);
            NativeDescriptorPool = descriptorPool;
        }
    }

    /// <summary>Retrieves a descriptor set.</summary>
    /// <returns>A descriptor set.</returns>
    public VulkanDescriptorSet GetDescriptorSet()
    {
        // check if there is a reusablable descriptor set already allocated
        if (ReusableDescriptorSets.Count > 0)
            return ReusableDescriptorSets.Dequeue();

        // allocate a new descriptor set
        var descriptorSetLayout = NativeDescriptorSetLayout;
        var allocateInfo = new VkDescriptorSetAllocateInfo()
        {
            SType = VkStructureType.DescriptorSetAllocateInfo,
            DescriptorPool = NativeDescriptorPool,
            DescriptorSetCount = 1,
            SetLayouts = &descriptorSetLayout
        };

        var descriptorSets = new VkDescriptorSet[1];
        VK.AllocateDescriptorSets(VulkanRenderer.Instance.Device.NativeDevice, ref allocateInfo, descriptorSets);
        return new(descriptorSets[0]);
    }

    /// <summary>Disposes a descriptor set.</summary>
    /// <param name="descriptorSet">The descriptor set to dispose.</param>
    public void DisposeDescriptorSet(VulkanDescriptorSet descriptorSet) => ReusableDescriptorSets.Enqueue(descriptorSet);

    /// <inheritdoc/>
    public void Dispose() => VK.DestroyDescriptorPool(VulkanRenderer.Instance.Device.NativeDevice, NativeDescriptorPool, null);
}

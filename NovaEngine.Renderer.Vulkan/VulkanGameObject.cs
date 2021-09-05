using NovaEngine.Core;
using NovaEngine.Core.Components;
using NovaEngine.External.Rendering;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using NovaEngine.Rendering;
using System;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan
{
    /// <summary>Represents a Vulkan game object.</summary>
    public unsafe class VulkanGameObject : RendererGameObjectBase
    {
        /*********
        ** Fields
        *********/
        /// <summary>The uniform buffer.</summary>
        private VulkanBuffer UniformBuffer;


        /*********
        ** Accessors
        *********/
        /// <summary>The number of vertices in the vertex buffer.</summary>
        internal int VertexCount { get; private set; }

        /// <summary>The number of indices in the index buffer.</summary>
        internal int IndexCount { get; private set; }

        /// <summary>The Vulkan vertex buffer.</summary>
        internal VulkanBuffer? VertexBuffer { get; private set; }

        /// <summary>The Vulkan index buffer.</summary>
        internal VulkanBuffer? IndexBuffer { get; private set; }

        /// <summary>The descriptor set.</summary>
        internal VulkanDescriptorSet DescriptorSet { get; private set; }


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public override void UpdateMesh(Vertex[] vertices, uint[] indices)
        {
            VertexCount = vertices.Length;
            IndexCount = indices.Length;

            if (VertexBuffer == null)
                VertexBuffer = VulkanUtilities.CreateVertexBuffer(vertices);
            else
                VertexBuffer.CopyFrom(vertices.AsSpan());

            if (IndexBuffer == null)
                IndexBuffer = VulkanUtilities.CreateIndexBuffer(indices);
            else
                IndexBuffer.CopyFrom(indices.AsSpan());
        }

        /// <inheritdoc/>
        public override void UpdateUBO(Camera camera)
        {
            // create UBO
            var ubo = new UniformBufferObject()
            {
                Model = BaseGameObject.Transform.Matrix,
                View = camera.ViewMatrix,
                Projection = camera.ProjectionMatrix
            };
            ubo.Projection.M22 *= -1;

            // copy UBO data to uniform buffer
            UniformBuffer.CopyFrom(ubo);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            VulkanRenderer.Instance.DescriptorPool.DisposeDescriptorSet(DescriptorSet);
            UniformBuffer.Dispose();
            VertexBuffer?.Dispose();
            IndexBuffer?.Dispose();
        }


        /*********
        ** Internal Methods
        *********/
        /// <inheritdoc/>
        internal VulkanGameObject(GameObject baseGameObject)
            : base(baseGameObject)
        {
            // create the uniform buffer
            UniformBuffer = new VulkanBuffer(sizeof(UniformBufferObject), VkBufferUsageFlags.UniformBuffer, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);

            // create a descriptor set for the object
            var bufferInfo = new VkDescriptorBufferInfo()
            {
                Buffer = UniformBuffer.NativeBuffer,
                Offset = 0,
                Range = sizeof(UniformBufferObject)
            };

            var imageInfo = new VkDescriptorImageInfo()
            {
                ImageLayout = VkImageLayout.ShaderReadOnlyOptimal,
                ImageView = (Texture2D.Undefined.RendererTexture as VulkanTexture)!.NativeImageView,
                Sampler = (Texture2D.Undefined.RendererTexture as VulkanTexture)!.NativeSampler
            };

            DescriptorSet = VulkanRenderer.Instance.DescriptorPool.GetDescriptorSet();
            DescriptorSet
                .Bind(0, &bufferInfo)
                .Bind(1, &imageInfo)
                .UpdateBindings();
        }
    }
}

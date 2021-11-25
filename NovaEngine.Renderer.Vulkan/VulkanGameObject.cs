using NovaEngine.Components;
using NovaEngine.Core;
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

        /// <summary>The type of the mesh.</summary>
        internal MeshType MeshType { get; private set; }

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
        public override void UpdateMesh(Mesh mesh)
        {
            VertexCount = mesh.VertexData.Length;
            IndexCount = mesh.IndexData.Length;
            MeshType = mesh.Type;

            if (VertexBuffer == null)
                VertexBuffer = VulkanUtilities.CreateVertexBuffer(mesh.VertexData);
            else
                VertexBuffer.CopyFrom(mesh.VertexData.AsSpan());

            if (IndexBuffer == null)
                IndexBuffer = VulkanUtilities.CreateIndexBuffer(mesh.IndexData);
            else
                IndexBuffer.CopyFrom(mesh.IndexData.AsSpan());
        }

        /// <inheritdoc/>
        public override void UpdateUBO(Camera camera)
        {
            // create UBO
            var ubo = new UniformBufferObject(
                model: BaseGameObject.Transform.Matrix,
                view: camera.ViewMatrix,
                projection: camera.ProjectionMatrix,
                cameraPosition: Utilities.ConvertEngineCoordinatesToRendererCoordinates(camera.Transform?.GlobalPosition ?? Vector3.Zero)
            );
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
            var bufferInfo1 = new VkDescriptorBufferInfo()
            {
                Buffer = UniformBuffer.NativeBuffer,
                Offset = 0,
                Range = sizeof(UniformBufferObject)
            };

            var bufferInfo2 = new VkDescriptorBufferInfo()
            {
                Buffer = VulkanRenderer.Instance.LightsBuffer.NativeBuffer,
                Offset = 0,
                Range = sizeof(UniformBufferObjectLights)
            };

            DescriptorSet = VulkanRenderer.Instance.DescriptorPool.GetDescriptorSet();
            DescriptorSet
                .Bind(0, &bufferInfo1)
                .Bind(1, &bufferInfo2)
                .UpdateBindings();
        }
    }
}

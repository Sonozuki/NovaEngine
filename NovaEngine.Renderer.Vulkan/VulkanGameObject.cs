using NovaEngine.SceneManagement;

namespace NovaEngine.Renderer.Vulkan;

/// <summary>Represents a Vulkan game object.</summary>
public class VulkanGameObject : RendererGameObjectBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The mvp buffer.</summary>
    private readonly VulkanBuffer MVPBuffer;


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

    /// <summary>The depth pre-pass descriptor set.</summary>
    internal VulkanDescriptorSet DepthPrepassDescriptorSet { get; private set; }

    /// <summary>The pbr descriptor set.</summary>
    internal VulkanDescriptorSet PBRDescriptorSet { get; private set; }

    /// <summary>The MTSDF text descriptor set.</summary>
    internal VulkanDescriptorSet MTSDFTextDescriptorSet { get; private set; }


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
    public override unsafe void PrepareForCamera(Camera camera)
    {
        var vulkanCamera = (camera.RendererCamera as VulkanCamera)!;
        var isInUIScene = BaseGameObject.Scene is UIScene;

        // MVP UBO
        if (isInUIScene)
        {
            // TODO: calculate position based on anchors
            var uiTransform = (BaseGameObject.Transform as UITransform)!;

            var position = new Vector3<float>(uiTransform.Left, uiTransform.Top, 0);
            var rotation = Quaternion<float>.Identity;
            var modelMatrix = Matrix4x4<float>.CreateScale(Vector3<float>.One)
                            * Matrix4x4<float>.CreateFromQuaternion(new(-rotation.X, -rotation.Y, rotation.Z, rotation.W))
                            * Matrix4x4<float>.CreateTranslation(new(position.X, position.Y, -position.Z));

            var ubo = new MVPBuffer(
                model: modelMatrix,
                view: Matrix4x4<float>.Identity,
                projection: Matrix4x4<float>.CreateTranslation(-Program.MainWindow.Size.X / 2f, -Program.MainWindow.Size.Y / 2f, 0)
                          * Matrix4x4<float>.CreateOrthographic(Program.MainWindow.Size.X, Program.MainWindow.Size.Y, 0, 1) // TODO: temp
            );

            MVPBuffer.CopyFrom(ubo);
        }
        else
        {
            var position = BaseGameObject.Transform.GlobalPosition;
            var rotation = BaseGameObject.Transform.GlobalRotation;
            var modelMatrix = Matrix4x4<float>.CreateScale(BaseGameObject.Transform.GlobalScale)
                            * Matrix4x4<float>.CreateFromQuaternion(new(-rotation.X, -rotation.Y, rotation.Z, rotation.W))
                            * Matrix4x4<float>.CreateTranslation(new(position.X, position.Y, -position.Z));

            position = -camera.Transform.GlobalPosition;
            rotation = camera.Transform.GlobalRotation.Inverse;
            var viewMatrix = Matrix4x4<float>.CreateTranslation(new(position.X, position.Y, -position.Z))
                           * Matrix4x4<float>.CreateFromQuaternion(new(-rotation.X, -rotation.Y, rotation.Z, rotation.W));

            var ubo = new MVPBuffer(
                model: modelMatrix,
                view: viewMatrix,
                projection: camera.ProjectionMatrix
            );
            ubo.Projection.M22 *= -1;

            MVPBuffer.CopyFrom(ubo);
        }

        // pbr descriptor set
        {
            var bufferInfo1 = new VkDescriptorBufferInfo { Buffer = vulkanCamera.ParametersBuffer.NativeBuffer, Offset = 0, Range = vulkanCamera.ParametersBuffer.Size };
            var bufferInfo2 = new VkDescriptorBufferInfo { Buffer = vulkanCamera.LightsBuffer.NativeBuffer, Offset = 0, Range = vulkanCamera.LightsBuffer.Size };
            var bufferInfo3 = new VkDescriptorBufferInfo { Buffer = vulkanCamera.OpaqueLightIndexListBuffer.NativeBuffer, Offset = 0, Range = vulkanCamera.OpaqueLightIndexListBuffer.Size };
            var bufferInfo4 = new VkDescriptorBufferInfo { Buffer = vulkanCamera.TransparentLightIndexListBuffer.NativeBuffer, Offset = 0, Range = vulkanCamera.TransparentLightIndexListBuffer.Size };
            var bufferInfo5 = new VkDescriptorBufferInfo { Buffer = vulkanCamera.OpaqueLightGridBuffer.NativeBuffer, Offset = 0, Range = vulkanCamera.OpaqueLightGridBuffer.Size };
            var bufferInfo6 = new VkDescriptorBufferInfo { Buffer = vulkanCamera.TransparentLightGridBuffer.NativeBuffer, Offset = 0, Range = vulkanCamera.TransparentLightGridBuffer.Size };

            PBRDescriptorSet
                .Bind(1, &bufferInfo1, VkDescriptorType.UniformBuffer)
                .Bind(2, &bufferInfo2, VkDescriptorType.StorageBuffer)
                .Bind(3, &bufferInfo3, VkDescriptorType.StorageBuffer)
                .Bind(4, &bufferInfo4, VkDescriptorType.StorageBuffer)
                .Bind(5, &bufferInfo5, VkDescriptorType.StorageBuffer)
                .Bind(6, &bufferInfo6, VkDescriptorType.StorageBuffer)
                .UpdateBindings();
        }
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        DescriptorPools.DepthPrepassDescriptorPool.DisposeDescriptorSet(DepthPrepassDescriptorSet);
        DescriptorPools.PBRDescriptorPool.DisposeDescriptorSet(PBRDescriptorSet);
        DescriptorPools.MTSDFTextDescriptorPool.DisposeDescriptorSet(MTSDFTextDescriptorSet);

        MVPBuffer.Dispose();
        VertexBuffer?.Dispose();
        IndexBuffer?.Dispose();
    }


    /*********
    ** Internal Methods
    *********/
    /// <inheritdoc/>
    internal unsafe VulkanGameObject(GameObject baseGameObject)
        : base(baseGameObject)
    {
        // create the uniform buffer
        MVPBuffer = new VulkanBuffer(sizeof(MVPBuffer), VkBufferUsageFlags.UniformBuffer, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);

        // create the descriptor sets for the object
        var bufferInfo = new VkDescriptorBufferInfo { Buffer = MVPBuffer.NativeBuffer, Offset = 0, Range = sizeof(MVPBuffer) };

        // TODO: temp
        VkDescriptorImageInfo? imageInfo1 = null;
        VkDescriptorImageInfo? imageInfo2 = null;
        var textRenderer = baseGameObject.Components.Get<TextRenderer>();
        if (textRenderer != null)
        {
            var fontAtlas = (textRenderer.Font.Atlas.RendererTexture as VulkanTexture)!;
            var borderTexture = (textRenderer.BorderTexture.RendererTexture as VulkanTexture)!;
            imageInfo1 = new() { ImageLayout = VkImageLayout.ShaderReadOnlyOptimal, ImageView = fontAtlas.NativeImageView, Sampler = fontAtlas.NativeSampler };
            imageInfo2 = new() { ImageLayout = VkImageLayout.ShaderReadOnlyOptimal, ImageView = borderTexture.NativeImageView, Sampler = borderTexture.NativeSampler };
        }

        DepthPrepassDescriptorSet = DescriptorPools.DepthPrepassDescriptorPool.GetDescriptorSet();
        DepthPrepassDescriptorSet
            .Bind(0, &bufferInfo, VkDescriptorType.UniformBuffer)
            .UpdateBindings();

        PBRDescriptorSet = DescriptorPools.PBRDescriptorPool.GetDescriptorSet();
        PBRDescriptorSet
            .Bind(0, &bufferInfo, VkDescriptorType.UniformBuffer)
            .UpdateBindings();

        MTSDFTextDescriptorSet = DescriptorPools.MTSDFTextDescriptorPool.GetDescriptorSet();
        MTSDFTextDescriptorSet.Bind(0, &bufferInfo, VkDescriptorType.UniformBuffer);

        // TODO: temp
        if (textRenderer != null)
        {
            var imageInfo1Copy = imageInfo1.Value;
            var imageInfo2Copy = imageInfo2.Value;
            MTSDFTextDescriptorSet
                .Bind(1, &imageInfo1Copy)
                .Bind(2, &imageInfo2Copy)
                .UpdateBindings();
        }
        else
            MTSDFTextDescriptorSet.UpdateBindings();
    }
}

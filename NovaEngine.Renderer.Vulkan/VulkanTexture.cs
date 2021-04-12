using NovaEngine.Graphics;
using NovaEngine.Rendering;

namespace NovaEngine.Renderer.Vulkan
{
    /// <summary>Represents a Vulkan specific texture.</summary>
    public class VulkanTexture : RendererTextureBase
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="baseTexture">The underlying texture.</param>
        /// <param name="generateMipChain">Whether the mip chain should be generated for the texture.</param>
        public VulkanTexture(TextureBase baseTexture, bool generateMipChain)
            : base(baseTexture) 
        {

        }

        /// <inheritdoc/>
        public override void Set1DPixels(Colour[] pixels, int xOffset = 0)
        {
        }

        /// <inheritdoc/>
        public override void Set2DPixels(Colour[,] pixels, int xOffset = 0, int yOffset = 0)
        {
        }

        /// <inheritdoc/>
        public override void Set3DPixels(Colour[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0)
        {
        }

        /// <inheritdoc/>
        public override void GenerateMipChain()
        {
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
        }
    }
}

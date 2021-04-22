using NovaEngine.Rendering;

namespace NovaEngine.Graphics
{
    /// <summary>Represents a depth texture.</summary>
    public class DepthTexture : Texture2D
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        internal override TextureUsage Usage => TextureUsage.Depth;


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public DepthTexture(uint width, uint height, SampleCount sampleCount)
            : base(width, height, sampleCount: sampleCount) { }
    }
}

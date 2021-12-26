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
        /// <summary>Constructs an instance/</summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="sampleCount">The number of samples per pixel of the texture.</param>
        public DepthTexture(uint width, uint height, SampleCount sampleCount)
            : base(width, height, automaticallyGenerateMipChain: false, sampleCount: sampleCount) { }
    }
}

using NovaEngine.Graphics;
using System;
using System.Reflection;

namespace NovaEngine.Rendering
{
    /// <summary>Represents a renderer texture.</summary>
    public abstract class RendererTextureBase : IDisposable
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The underlying texture.</summary>
        public TextureBase BaseTexture { get; }

        /// <summary>The width of the texture.</summary>
        public uint Width { get; }
        
        /// <summary>The height of the texture.</summary>
        public uint Height { get; }
        
        /// <summary>The depth of the texture.</summary>
        public uint Depth { get; }
        
        /// <summary>The mip LOD (level of detail) bias of the texture.</summary>
        public float MipLodBias { get; }
        
        /// <summary>Whether the texture has anisotropic filtering enabled.</summary>
        public bool AnisotropicFilteringEnabled { get; }
        
        /// <summary>The max anisotropic filtering level of the texture.</summary>
        public float MaxAnisotropicFilteringLevel { get; }
        
        /// <summary>The number of mip levels the texture has.</summary>
        public uint MipLevels
        {
            get => this.BaseTexture.MipLevels;
            set => typeof(TextureBase).GetField("_MipLevels", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(this.BaseTexture, value);
        }
        
        /// <summary>The number of layers the texture has.</summary>
        public uint LayerCount { get; }
        
        /// <summary>The usage of the texture.</summary>
        public TextureUsage Usage { get; }
        
        /// <summary>The type of the texture.</summary>
        public TextureType Type { get; }
        
        /// <summary>The texture wrap mode of the U axis.</summary>
        public TextureWrapMode WrapModeU { get; }
        
        /// <summary>The texture wrap mode of the V axis.</summary>
        public TextureWrapMode WrapModeV { get; }
        
        /// <summary>The texture wrap mode of the W axis.</summary>
        public TextureWrapMode WrapModeW { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="baseTexture">The underlying texture.</param>
        public RendererTextureBase(TextureBase baseTexture)
        {
            BaseTexture = baseTexture;

            // fill in convenience properties. reflection is used instead of changing the accessibility as exposing these would lead to rather confusing Texture types (for example, having a '_Height' and 'Height' members, both of which would be public).
            Width = this.BaseTexture.Width;
            Height = (uint?)typeof(TextureBase).GetField("_Height", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this.BaseTexture) ?? throw new MissingMemberException("Couldn't find '_Height' field.");
            Depth = (uint?)typeof(TextureBase).GetField("_Depth", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this.BaseTexture) ?? throw new MissingMemberException("Couldn't find '_Depth' field.");
            MipLodBias = this.BaseTexture.MipLodBias;
            AnisotropicFilteringEnabled = this.BaseTexture.AnisotropicFilteringEnabled;
            MaxAnisotropicFilteringLevel = this.BaseTexture.MaxAnisotropicFilteringLevel;
            LayerCount = (uint?)typeof(TextureBase).GetField("_LayerCount", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this.BaseTexture) ?? throw new MissingMemberException("Couldn't find '_LayerCount' field.");
            Usage = (TextureUsage?)typeof(TextureBase).GetProperty("Usage", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this.BaseTexture) ?? throw new MissingMemberException("Couldn't find 'Usage' property.");
            Type = (TextureType?)typeof(TextureBase).GetProperty("Type", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this.BaseTexture) ?? throw new MissingMemberException("Couldn't find 'Type' property.");
            WrapModeU = this.BaseTexture.WrapModeU;
            WrapModeV = (TextureWrapMode?)typeof(TextureBase).GetField("_WrapModeV", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this.BaseTexture) ?? throw new MissingMemberException("Couldn't find '_WrapModeV' field.");
            WrapModeW = (TextureWrapMode?)typeof(TextureBase).GetField("_WrapModeW", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this.BaseTexture) ?? throw new MissingMemberException("Couldn't find '_WrapModeW' field.");
        }

        /// <summary>Sets pixels specific colours when the texture is a one-dimensional texture.</summary>
        /// <param name="pixels">The rectangle of pixels to set.</param>
        /// <param name="xOffset">The X offset of the pixels (from top left).</param>
        public abstract void Set1DPixels(Colour[] pixels, int xOffset = 0);

        /// <summary>Sets pixels specific colours when the texture is a two-dimensional texture.</summary>
        /// <param name="pixels">The rectangle of pixels to set.</param>
        /// <param name="xOffset">The X offset of the pixels (from top left).</param>
        /// <param name="yOffset">The Y offset of the pixels (from top left).</param>
        public abstract void Set2DPixels(Colour[,] pixels, int xOffset = 0, int yOffset = 0);

        /// <summary>Sets pixels specific colours when the texture is a three-dimensional texture.</summary>
        /// <param name="pixels">The rectangle of pixels to set.</param>
        /// <param name="xOffset">The X offset of the pixels (from top left).</param>
        /// <param name="yOffset">The Y offset of the pixels (from top left).</param>
        /// <param name="zOffset">The Z offset of the pixels (from top left).</param>
        public abstract void Set3DPixels(Colour[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0);

        /// <summary>Generates the mip chain for the texture.</summary>
        public abstract void GenerateMipChain();

        /// <summary>Disposes unmanaged texture resources.</summary>
        public abstract void Dispose();
    }
}

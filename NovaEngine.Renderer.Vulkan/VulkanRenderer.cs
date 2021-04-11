using NovaEngine.Maths;
using NovaEngine.Rendering;
using System;

namespace NovaEngine.Renderer.Vulkan
{
    /// <summary>Represents a graphics renderer using Vulkan.</summary>
    public class VulkanRenderer : IRenderer, IDisposable
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool CanUseOnPlatform => OperatingSystem.IsWindows();


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public void OnInitialise(IntPtr windowHandle)
        {
        }
        
        /// <inheritdoc/>
        public void OnWindowResize(Size newSize)
        {
        }

        /// <inheritdoc/>
        public void OnRenderFrame()
        {
        }

        /// <inheritdoc/>
        public void OnCleanUp() => Dispose();

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}

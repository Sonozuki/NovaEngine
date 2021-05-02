using Vulkan;

namespace NovaEngine.Renderer.Vulkan
{
    /// <summary>Contains details about the swapchain support.</summary>
    internal struct SwapchainSupportDetails
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The surface capabilities.</summary>
        public VkSurfaceCapabilitiesKHR Capabilities { get; }

        /// <summary>The surface formats.</summary>
        public VkSurfaceFormatKHR[] Formats { get; }

        /// <summary>The available presentation modes.</summary>
        public VkPresentModeKHR[] PresentationModes { get; }

        /// <summary>Whether the swapchain supports at least one format and presentation mode.</summary>
        public bool IsComplete => Formats.Length > 0 && PresentationModes.Length > 0;


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="physicalDevice">The physical device to get the swapchain support details of.</param>
        /// <param name="surface">The surface that the swapchain will present to.</param>
        public SwapchainSupportDetails(VkPhysicalDevice physicalDevice, VkSurfaceKHR surface)
        {
            VK.GetPhysicalDeviceSurfaceCapabilitiesKHR(physicalDevice, surface, out var capabilities);

            var formatsCount = 0u;
            VK.GetPhysicalDeviceSurfaceFormatsKHR(physicalDevice, surface, ref formatsCount, null);
            var formats = new VkSurfaceFormatKHR[formatsCount];
            VK.GetPhysicalDeviceSurfaceFormatsKHR(physicalDevice, surface, ref formatsCount, formats);

            var presentModesCount = 0u;
            VK.GetPhysicalDeviceSurfacePresentModesKHR(physicalDevice, surface, ref presentModesCount, null);
            var presentModes = new VkPresentModeKHR[presentModesCount];
            VK.GetPhysicalDeviceSurfacePresentModesKHR(physicalDevice, surface, ref presentModesCount, presentModes);

            Capabilities = capabilities;
            Formats = formats;
            PresentationModes = presentModes;
        }
    }
}

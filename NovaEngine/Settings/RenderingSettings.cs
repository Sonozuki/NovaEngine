using NovaEngine.Maths;
using NovaEngine.Rendering;

namespace NovaEngine.Settings
{
    /// <summary>The application settings related to rendering.</summary>
    /// <remarks>These are automatically saved to / loaded from the file: 'Documents/My Games/[Application Name]/Settings/RenderingSettings.json'.</remarks> // TODO: save / load this file
    public static class RenderingSettings
    {
        /*********
        ** Fields
        *********/
        /// <summary>The current number of samples per pixel to use in multisample anti aliasing (MSAA).</summary>
        private static SampleCount _SampleCount = SampleCount.Count8;


        /*********
        ** Accessors
        *********/
        /// <summary>The maximum number of samples per pixel that can be used in multisample anti aliasing (MSAA).</summary>
        public static SampleCount MaxSampleCount => RendererManager.CurrentRenderer.MaxSampleCount;

        /// <summary>The current number of samples per pixel to use in multisample anti aliasing (MSAA).</summary>
        public static SampleCount SampleCount
        {
            get => _SampleCount;
            set => _SampleCount = (SampleCount)MathsHelper.Clamp((int)value, 0, (int)MaxSampleCount);
        }
    }
}

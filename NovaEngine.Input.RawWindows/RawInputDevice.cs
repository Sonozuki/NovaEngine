using NovaEngine.External.Input;
using System;

namespace NovaEngine.Input.RawWindows
{
    /// <summary>Defines information for the raw input device.</summary>
    internal struct RawInputDevice
    {
        /*********
        ** Fields
        *********/
        /// <summary>The usage page for the raw input device.</summary>
        public HidUsagePage UsagePage;

        /// <summary>The usage id for the raw input device.</summary>
        public ushort Usage;

        /// <summary>Mode flag that specifies how to interpret the information provided by <see cref="UsagePage"/> and <see cref="Usage"/>.</summary>
        public RawInputDeviceFlags Flags;

        /// <summary>A handle to the target window. If <see langword="null"/> it follows the keyboard focus.</summary>
        public IntPtr Target;


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="usage">The usage id for the raw input device.</param>
        /// <param name="flags">Mode flag that specifies how to interpret the information provided by <see cref="UsagePage"/> and <see cref="Usage"/>.</param>
        /// <param name="target">A handle to the target window. If <see langword="null"/> it follows the keyboard focus.</param>
        public RawInputDevice(HidUsageGenericDesktop usage, RawInputDeviceFlags flags, IntPtr target)
        {
            UsagePage = HidUsagePage.GenericDesktop;
            Usage = (ushort)usage;
            Flags = flags;
            Target = target;
        }
    }
}

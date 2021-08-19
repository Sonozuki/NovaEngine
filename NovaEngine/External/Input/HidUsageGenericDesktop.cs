namespace NovaEngine.External.Input
{
    /// <summary>The uses of a <see cref="HidUsagePage.GenericDesktop"/> device.</summary>
    /// <remarks>Not all usages are defined here, for a full list go to: <see href="https://www.usb.org/sites/default/files/hut1_21.pdf#page=34"/>.</remarks>
    public enum HidUsageGenericDesktop : ushort
    {
        /// <summary>A hand-held, button-activated input device that when rolled along a flat surface, directs an indicator to move correspondingly about a computer screen, allowing the operator to move the indicator freely in select operations or to manipulate text or graphics. A mouse typically consists of two axes (X and Y) and one, two, or three buttons.</summary>
        Mouse = 0x02,

        /// <summary>The primary computer input device. A Keyboard minimally consists of 103 buttons as defined by the Boot Keyboard definition.</summary>
        Keyboard = 0x06
    }
}

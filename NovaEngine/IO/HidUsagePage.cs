namespace NovaEngine.IO
{
    /// <summary>The usage pages for a Human Interface Device (HID).</summary>
    /// <remarks>Not all usage pages are defined here, for a full list go to: <see href="https://www.usb.org/sites/default/files/hut1_21.pdf#page=16"/>.</remarks>
    public enum HidUsagePage : ushort
    {
        /// <summary>The HID is a generic desktop device.</summary>
        GenericDesktop = 0x01
    }
}

namespace NovaEngine.Common.Windows.Native
{
    /// <summary>The types of a raw input device.</summary>
    public enum RawInputDeviceType
    {
        /// <summary>Raw input comes from a mouse.</summary>
        Mouse,

        /// <summary>Raw input comes from a keyboard.</summary>
        Keyboard,

        /// <summary>Raw input comes from some device this is not a keyboard or a mouse.</summary>
        Hid
    }
}

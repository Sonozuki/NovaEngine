using System;

namespace NovaEngine.Platform.Windows.Input
{
    /// <summary>The flags a <see cref="RawInputDevice"/> can have.</summary>
    [Flags]
    internal enum RawInputDeviceFlags
    {
        /// <summary>Removes the top level collection from the inclusion list. This tell the OS to stop reading from a device which matches the top level collection/</summary>
        Remove = 0x00000001,

        /// <summary>Specifies the top level collection to exclude when reading a complete usage page. This flag only affects a top level collection whose usage page is already specified with <see cref="PageOnly"/>.</summary>
        Exclude = 0x00000010,

        /// <summary>Specifies all devices whose top level collection is from the specified <see cref="RawInputDevice.UsagePage"/>. Note that <see cref="RawInputDevice.Usage"/> must be zero. To exlude a particular top level collection, use <see cref="Exclude"/>.</summary>
        PageOnly = 0x00000020,

        /// <summary>Prevents and devices specified by <see cref="RawInputDevice.UsagePage"/> or <see cref="RawInputDevice.Usage"/> from generating legacy messages. This is only for the mouse and keyboard.</summary>
        NoLegacy = 0x00000030,

        /// <summary>Enables the caller to receive the input even when the caller is not in the foreground. Note that <see cref="RawInputDevice.Target"/> must be specified.</summary>
        InputSink = 0x00000100,

        /// <summary>Mouse buton click does not activate the other window. <see cref="CaptureMouse"/> can only be specified if <see cref="NoLegacy"/> is specified for a mouse device.</summary>
        CaptureMouse = 0x00000200,

        /// <summary>Applicated-defined keyboard device hotkeys are not handled. However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled. By default, all keyboard hotkeys are handled. <see cref="NoHotkeys"/> can be specified even if <see cref="NoLegacy"/> is not specified and <see cref="RawInputDevice.Target"/> is <see langword="null"/>.</summary>
        NoHotkeys = 0x00000200,

        /// <summary>Application command keys are handled. <see cref="AppKeys"/> can be speciifed only is <see cref="NoLegacy"/> is specified for a keyboard device.</summary>
        AppKeys = 0x00000400,

        /// <summary>Enables the called to receive input in the background only if the foreground application does not process it. In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input.</summary>
        ExInputSink = 0x00001000,

        /// <summary>Enables the caller to receive notification for device arrival and device removal.</summary>
        DevNotify = 0x00002000
    }
}

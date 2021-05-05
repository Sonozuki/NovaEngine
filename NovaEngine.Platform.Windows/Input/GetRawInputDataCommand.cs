using NovaEngine.Platform.Windows.Api;
using System;

namespace NovaEngine.Platform.Windows.Input
{
    /// <summary>Specifies what data will be returned in the 'data' paramater of <see cref="User32.GetRawInputData(IntPtr, GetRawInputDataCommand, IntPtr, ref uint, uint)"/>.</summary>
    internal enum GetRawInputDataCommand
    {
        /// <summary>Get the header information from the <see cref="RawInput"/> structure.</summary>
        Header = 0x10000005,

        /// <summary>Get the raw data from the <see cref="RawInput"/> structure.</summary>
        Input = 0x10000003
    }
}

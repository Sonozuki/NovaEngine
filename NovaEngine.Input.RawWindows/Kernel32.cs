﻿using System.Runtime.InteropServices;

namespace NovaEngine.InputHandler.RawWindows
{
    /// <summary>Exposes neccessary User32.dll apis.</summary>
    internal static class Kernel32
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Sets the last error code for the calling thread.</summary>
        /// <param name="errorCode">The last error code for the thread.</param>
        [DllImport("Kernel32", SetLastError = true)]
        public static extern void SetLastError(int errorCode);
    }
}

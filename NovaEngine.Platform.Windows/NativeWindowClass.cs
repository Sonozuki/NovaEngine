using System;

namespace NovaEngine.Platform.Windows
{
    /// <summary>Contains the window class attributes that are registered by the <see cref="User32.RegisterClass(in NativeWindowClass)"/> method.</summary>
    public struct NativeWindowClass
    {
        /*********
        ** Fields
        *********/
        /// <summary>The class style(s).</summary>
        public uint Style;

        /// <summary>The window procedure.</summary>
        public WindowProcedureDelegate WindowProcedure;

        /// <summary>The number of extra bytes to allocate following the window-class structure.</summary>
        public int ClsExtra;

        /// <summary>The number of extra bytes to allocate following the window instance.</summary>
        public int WndExtra;

        /// <summary>The application handle that contains the window procedure for the class.</summary>
        public IntPtr Handle;

        /// <summary>A handle to the class icon.</summary>
        public IntPtr Icon;

        /// <summary>A handle to the class cursor.</summary>
        public IntPtr Cursor;

        /// <summary>A handle to the background brush.</summary>
        public IntPtr Brush;

        /// <summary>The resource name of the class menu.</summary>
        public string MenuName;

        /// <summary>The window class name.</summary>
        public string ClassName;
    }
}

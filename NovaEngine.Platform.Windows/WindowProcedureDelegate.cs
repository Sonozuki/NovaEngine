using System;

namespace NovaEngine.Platform.Windows
{
    /// <summary>An application-defined method that processess messges sent to a window.</summary>
    /// <param name="window">A handle to the window.</param>
    /// <param name="message">The message.</param>
    /// <param name="wParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
    /// <param name="lParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
    /// <returns>The result of the message processing, which depends on the message sent.</returns>
    public delegate IntPtr WindowProcedureDelegate(IntPtr window, Message message, IntPtr wParam, IntPtr lParam);
}

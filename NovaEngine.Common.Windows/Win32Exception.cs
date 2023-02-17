namespace NovaEngine.Common.Windows;

/// <summary>The exception that is thrown when an error occurs in a win32 platform.</summary>
public sealed class Win32Exception : PlatformException
{
    /*********
    ** Constructors
    *********/
    /// <summary>Contructs an instance.</summary>
    public Win32Exception() { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    public Win32Exception(string? message)
        : base(message) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public Win32Exception(string? message, Exception? innerException = null)
        : base(message, innerException) { }
}

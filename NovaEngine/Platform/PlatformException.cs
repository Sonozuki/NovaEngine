namespace NovaEngine.Platform;

/// <summary>The exception that is thrown when an error occurs in a platform.</summary>
public abstract class PlatformException : Exception
{
    /*********
    ** Constructors
    *********/
    /// <summary>Contructs an instance.</summary>
    protected PlatformException() { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    protected PlatformException(string? message)
        : base(message) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    protected PlatformException(string? message, Exception? innerException = null)
        : base(message, innerException) { }
}

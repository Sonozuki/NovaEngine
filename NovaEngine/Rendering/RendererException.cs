namespace NovaEngine.Rendering;

/// <summary>The exception that is thrown when an error occurs in a renderer.</summary>
public abstract class RendererException : Exception
{
    /*********
    ** Constructors
    *********/
    /// <summary>Contructs an instance.</summary>
    protected RendererException() { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    protected RendererException(string? message)
        : base(message) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    protected RendererException(string? message, Exception? innerException = null)
        : base(message, innerException) { }
}

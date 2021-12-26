namespace NovaEngine.Content;

/// <summary>The exception that is thrown when an error occurs in the content pipeline.</summary>
public class ContentException : Exception
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ContentException(string? message, Exception? innerException = null)
        : base(message, innerException) { }
}

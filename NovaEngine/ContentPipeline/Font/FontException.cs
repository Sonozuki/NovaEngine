namespace NovaEngine.ContentPipeline.Font;

/// <summary>The exception that is thrown when an error occurs in the font parser.</summary>
public class FontException : Exception
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public FontException() { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    public FontException(string message)
        : base(message) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public FontException(string message, Exception innerException)
        : base(message, innerException) { }
}

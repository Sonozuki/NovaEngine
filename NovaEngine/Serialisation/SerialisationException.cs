namespace NovaEngine.Serialisation;

/// <summary>The exception that is thrown when an error occures in the <see cref="Serialiser"/>.</summary>
public class SerialisationException : Exception
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    public SerialisationException(string message)
        : base(message) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public SerialisationException(string message, Exception innerException)
            : base(message, innerException) { }
}

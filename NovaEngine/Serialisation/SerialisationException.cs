﻿namespace NovaEngine.Serialisation;

/// <summary>The exception that is thrown when an error occures in the <see cref="Serialiser"/>.</summary>
public sealed class SerialisationException : Exception
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public SerialisationException() { }

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

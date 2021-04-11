using System;

namespace NovaEngine.Content
{
    /// <summary>The exception that is thrown when an error occurs in the content pipeline.</summary>
    public class ContentLoaderException : Exception
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="message">The message that descibes the error.</param>
        public ContentLoaderException(string message)
            : base(message) { }
    }
}

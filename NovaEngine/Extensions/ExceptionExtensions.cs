using NovaEngine.Logging;
using System;

namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="Exception"/> class.</summary>
    public static class ExceptionExtensions
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Logs the message of the exception.</summary>
        /// <param name="exception">The exception whose message should be logged.</param>
        /// <param name="severity">The severity of create the log as.</param>
        /// <returns>The current instance, used so you can log and throw in a single statement.</returns>
        public static Exception Log(this Exception exception, LogSeverity severity) // TODO: currently this doesn't get inlined resulting in all logging to happen from the engine (can't use aggressive inlining as that will only work in release)
        {
            Logger.Log(exception.Message, severity);
            return exception;
        }
    }
}

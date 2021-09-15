﻿using NovaEngine.Logging;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception Log(this Exception exception, LogSeverity severity)
        {
            Logger.Log(Assembly.GetCallingAssembly(), severity, exception.Message);
            return exception;
        }
    }
}

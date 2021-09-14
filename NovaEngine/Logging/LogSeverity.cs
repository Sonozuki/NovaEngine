using System;

namespace NovaEngine.Logging
{
    /// <summary>The severity of a log.</summary>
    public enum LogSeverity
    {
        /// <summary>Debugging information intended for developers.</summary>
        [LogSeverityInfo("DEBUG", ConsoleColor.DarkGray, ConsoleColor.DarkGray)]
        Debug,

        /// <summary>Information to help players.</summary>
        [LogSeverityInfo("HELP ", ConsoleColor.Blue, ConsoleColor.White)]
        Help,

        /// <summary>Information relavant to players.</summary>
        [LogSeverityInfo("INFO ", ConsoleColor.DarkGreen, ConsoleColor.White)]
        Info,

        /// <summary>An issue the the player should be aware of.</summary>
        [LogSeverityInfo("WARN ", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow)]
        Warning,

        /// <summary>An error that has occured.</summary>
        [LogSeverityInfo("ERROR", ConsoleColor.Red, ConsoleColor.Red)]
        Error,

        /// <summary>An unrecoverable fatal error has occured.</summary>
        /// <remarks>This should be used rarely, for example: if a renderer failed to initialise.</remarks>
        [LogSeverityInfo("FATAL", ConsoleColor.DarkRed, ConsoleColor.White, ConsoleColor.DarkRed)]
        Fatal
    }
}

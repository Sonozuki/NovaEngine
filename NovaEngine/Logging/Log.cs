using NovaEngine.Extensions;
using System;
using System.IO;
using System.Text;

namespace NovaEngine.Logging
{
    /// <summary>Represents a log.</summary>
    internal class Log
    {
        /*********
        ** Accessors
        *********/
        /// <summary>When the log was created, in UTC.</summary>
        public DateTime DateTime { get; }

        /// <summary>The creator of the log.</summary>
        public LogCreator Creator { get; }

        /// <summary>The severity info of the log.</summary>
        public LogSeverityInfoAttribute SeverityInfo { get; }

        /// <summary>The message being logged.</summary>
        public string Message { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="creator">The creator of the log.</param>
        /// <param name="severity">The severity of the log.</param>
        /// <param name="message">The message being logged.</param>
        public Log(LogCreator creator, LogSeverity severity, string message)
        {
            DateTime = DateTime.UtcNow;
            Creator = creator;
            SeverityInfo = severity.GetAttribute<LogSeverityInfoAttribute>()
                ?? LogSeverity.Info.GetAttribute<LogSeverityInfoAttribute>()!;
            Message = message;
        }

        /// <summary>Writes the log to the console.</summary>
        public void WriteToConsole()
        {
            // write label
            Console.ResetColor();
            Console.Write($"[{DateTime.ToLocalTime():HH:mm:ss} ");

            Console.ForegroundColor = SeverityInfo.LabelColour;
            Console.Write(SeverityInfo.Label);

            Console.ResetColor();
            if (Creator.IsInternal)
                Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" {Creator.Name}");
            Console.ResetColor();
            Console.Write("] ");

            // write message
            Console.ForegroundColor = SeverityInfo.ForegroundColour;
            if (SeverityInfo.BackgroundColour.HasValue)
                Console.BackgroundColor = SeverityInfo.BackgroundColour.Value;

            Console.WriteLine(Message);

            // explicitly resetting the colour here as the VS debugger console stays open between sessions, which meant it was keeping the
            // background colour from a fatal log and setting the entire console background to that on the next debug session for some reason
            Console.ResetColor();
        }

        /// <summary>Writes the log to a stream.</summary>
        /// <param name="stream">The stream to write the log to.</param>
        public void WriteToStream(Stream stream)
        {
            var message = $"[{DateTime:HH:mm:ss} {SeverityInfo.Label} {Creator.Name}] {Message}\n";
            stream.Write(Encoding.UTF8.GetBytes(message));
        }
    }
}

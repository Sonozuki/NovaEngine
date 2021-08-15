using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NovaEngine.Logging
{
    /// <summary>Handles logging for the engine, game, and mods.</summary>
    public static class Logger
    {
        /*********
        ** Fields
        *********/
        /// <summary>The log file.</summary>
        private static string LogFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", Program.Name, "Logs", "LatestLog.txt");


        /*********
        ** Accessors
        *********/
        /// <summary>The stream to the current log file.</summary>
        internal static Stream LogFileStream { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Initialises the class.</summary>
        static Logger()
        {
            // ensure directory exists before attempting to create log file
            var directoryName = new FileInfo(LogFileName).DirectoryName!;
            Directory.CreateDirectory(directoryName);

            LogFileStream = File.Create(LogFileName);
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => LogFileStream?.Dispose();

            Logger.LogHeader();
        }

        /// <summary>Logs a message.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="severity">The severity of the log.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Log(string message, LogSeverity severity = LogSeverity.Info)
        {
            // get the log caller
            var callingAssembly = Assembly.GetCallingAssembly();
            var callingLibraryName = Path.GetFileNameWithoutExtension(callingAssembly.ManifestModule.Name);
            var caller = callingLibraryName switch // TODO: mods
            {
                "NovaEngine" => LogCaller.Engine,
                _ => LogCaller.Game
            };

            // create a log
            var log = new Log(caller, severity, message);
            log.WriteToConsole();
            log.WriteToStream(LogFileStream);
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Logs the general engine and system details.</summary>
        private static void LogHeader()
        {
            Logger.Log($"NovaEngine {Constants.EngineVersion} running {Program.Name} on {Environment.OSVersion}");
        }
    }
}

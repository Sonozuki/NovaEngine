using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NovaEngine.Logging
{
    /// <summary>Handles logging for the engine, game, and mods.</summary>
    public static class Logger
    {
        /*********
        ** Public Methods
        *********/
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
            
            // TODO: write to a log file
        }
    }
}

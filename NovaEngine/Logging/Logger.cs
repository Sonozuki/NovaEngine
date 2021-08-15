using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace NovaEngine.Logging
{
    /// <summary>Handles logging for the engine, game, and mods.</summary>
    public static class Logger
    {
        /*********
        ** Fields
        *********/
        /// <summary>The pattern used for determining if an assembly is a platform assembly.</summary>
        private static Regex PlatformAssemblyPattern = new("^NovaEngine.Platform.*", RegexOptions.Compiled);

        /// <summary>The pattern used for determining if an assembly is a renderer assembly.</summary>
        private static Regex RendererAssemblyPattern = new("^NovaEngine.Renderer.*", RegexOptions.Compiled);


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
            var directoryName = new FileInfo(Constants.LogFilePath).DirectoryName!;
            Directory.CreateDirectory(directoryName);

            LogFileStream = File.Create(Constants.LogFilePath);
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
            LogCreator creator = callingLibraryName switch // TODO: mods
            {
                "NovaEngine" => new("Engine", true),
                var assembly when PlatformAssemblyPattern.IsMatch(assembly) => new("Platform", true),
                var assembly when RendererAssemblyPattern.IsMatch(assembly) => new("Renderer", true),
                var assembly => new(assembly, false)
            };

            // create a log
            var log = new Log(creator, severity, message);
            log.WriteToConsole();
            log.WriteToStream(LogFileStream);
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Logs the general engine and system details.</summary>
        private static void LogHeader()
        {
            Logger.Log($"{Constants.EngineName} {Constants.EngineVersion} running {Program.Name} on {Environment.OSVersion}");
        }
    }
}

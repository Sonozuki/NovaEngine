using System;
using System.Collections.Generic;
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
        private static readonly Regex PlatformAssemblyPattern = new("^NovaEngine.Platform.*", RegexOptions.Compiled);

        /// <summary>The pattern used for determining if an assembly is a renderer assembly.</summary>
        private static readonly Regex RendererAssemblyPattern = new("^NovaEngine.Renderer.*", RegexOptions.Compiled);

        /// <summary>The pattern used for determining if an assembly is an input handler.</summary>
        private static readonly Regex InputHandlerAssemblyPattern = new("^NovaEngine.InputHandler.*", RegexOptions.Compiled);


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
        public static void Log(string? message = "", LogSeverity severity = LogSeverity.Info)
        {
            // get the log creator
            var callingAssembly = Assembly.GetCallingAssembly();
            var callingLibraryName = Path.GetFileNameWithoutExtension(callingAssembly.ManifestModule.Name);
            LogCreator creator = callingLibraryName switch // TODO: mods
            {
                "NovaEngine" => new("Engine", true),
                var assembly when PlatformAssemblyPattern.IsMatch(assembly) => new("Platform", true),
                var assembly when RendererAssemblyPattern.IsMatch(assembly) => new("Renderer", true),
                var assembly when InputHandlerAssemblyPattern.IsMatch(assembly) => new("Input Handler", true),
                var assembly => new(assembly, false)
            };

            // create a log
            var log = new Log(creator, severity, message ?? "");
            log.WriteToConsole();
            log.WriteToStream(LogFileStream);
        }

        /// <summary>Logs a collection.</summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection to log.</param>
        /// <param name="severity">The sevirity of the log.</param>
        public static void Log<T>(IEnumerable<T> collection, LogSeverity severity = LogSeverity.Info) => Logger.Log($"[{string.Join(", ", collection)}]", severity);

        /// <summary>Logs an object.</summary>
        /// <param name="object">The object to log.</param>
        /// <param name="severity">The severity of the log.</param>
        public static void Log(object? @object, LogSeverity severity = LogSeverity.Info) => Logger.Log(@object?.ToString(), severity);


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

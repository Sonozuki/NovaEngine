namespace NovaEngine.Logging;

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
        var directoryName = new FileInfo(Constants.LogFilePath).DirectoryName!;
        Directory.CreateDirectory(directoryName);

        LogFileStream = File.Create(Constants.LogFilePath);
        AppDomain.CurrentDomain.ProcessExit += (sender, e) => LogFileStream?.Dispose();

        LogHeader();
    }

    /// <summary>Logs a message.</summary>
    /// <param name="message">The message to log.</param>
    /// <param name="severity">The severity of the log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Log(string? message = "", LogSeverity severity = LogSeverity.Info) => Log(Assembly.GetCallingAssembly(), severity, message);

    /// <summary>Logs a collection.</summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="collection">The collection to log.</param>
    /// <param name="severity">The severity of the log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Log<T>(IEnumerable<T> collection, LogSeverity severity = LogSeverity.Info) => Log(Assembly.GetCallingAssembly(), severity, $"[{string.Join(", ", collection)}]");

    /// <summary>Logs an object.</summary>
    /// <param name="object">The object to log.</param>
    /// <param name="severity">The severity of the log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Log(object? @object, LogSeverity severity = LogSeverity.Info) => Log(Assembly.GetCallingAssembly(), severity, @object?.ToString());

    /// <summary>Logs a message with <see cref="LogSeverity.Debug"/>.</summary>
    /// <param name="message">The message to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogDebug(string? message = "") => Log(Assembly.GetCallingAssembly(), LogSeverity.Debug, message);

    /// <summary>Logs a collection with <see cref="LogSeverity.Debug"/>.</summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="collection">The collection to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogDebug<T>(IEnumerable<T> collection) => Log(Assembly.GetCallingAssembly(), LogSeverity.Debug, $"[{string.Join(", ", collection)}]");

    /// <summary>Logs an object with <see cref="LogSeverity.Debug"/>.</summary>
    /// <param name="object">The object to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogDebug(object? @object) => Log(Assembly.GetCallingAssembly(), LogSeverity.Debug, @object?.ToString());

    /// <summary>Logs a message with <see cref="LogSeverity.Help"/>.</summary>
    /// <param name="message">The message to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogHelp(string? message = "") => Log(Assembly.GetCallingAssembly(), LogSeverity.Help, message);

    /// <summary>Logs a collection with <see cref="LogSeverity.Help"/>.</summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="collection">The collection to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogHelp<T>(IEnumerable<T> collection) => Log(Assembly.GetCallingAssembly(), LogSeverity.Help, $"[{string.Join(", ", collection)}]");

    /// <summary>Logs an object with <see cref="LogSeverity.Help"/>.</summary>
    /// <param name="object">The object to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogHelp(object? @object) => Log(Assembly.GetCallingAssembly(), LogSeverity.Help, @object?.ToString());

    /// <summary>Logs a message with <see cref="LogSeverity.Info"/>.</summary>
    /// <param name="message">The message to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogInfo(string? message = "") => Log(Assembly.GetCallingAssembly(), LogSeverity.Info, message);

    /// <summary>Logs a collection with <see cref="LogSeverity.Info"/>.</summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="collection">The collection to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogInfo<T>(IEnumerable<T> collection) => Log(Assembly.GetCallingAssembly(), LogSeverity.Info, $"[{string.Join(", ", collection)}]");

    /// <summary>Logs an object with <see cref="LogSeverity.Info"/>.</summary>
    /// <param name="object">The object to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogInfo(object? @object) => Log(Assembly.GetCallingAssembly(), LogSeverity.Info, @object?.ToString());

    /// <summary>Logs a message with <see cref="LogSeverity.Alert"/>.</summary>
    /// <param name="message">The message to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogAlert(string? message = "") => Log(Assembly.GetCallingAssembly(), LogSeverity.Alert, message);

    /// <summary>Logs a collection with <see cref="LogSeverity.Alert"/>.</summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="collection">The collection to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogAlert<T>(IEnumerable<T> collection) => Log(Assembly.GetCallingAssembly(), LogSeverity.Alert, $"[{string.Join(", ", collection)}]");

    /// <summary>Logs an object with <see cref="LogSeverity.Alert"/>.</summary>
    /// <param name="object">The object to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogAlert(object? @object) => Log(Assembly.GetCallingAssembly(), LogSeverity.Alert, @object?.ToString());

    /// <summary>Logs a message with <see cref="LogSeverity.Warning"/>.</summary>
    /// <param name="message">The message to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogWarning(string? message = "") => Log(Assembly.GetCallingAssembly(), LogSeverity.Warning, message);

    /// <summary>Logs a collection with <see cref="LogSeverity.Warning"/>.</summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="collection">The collection to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogWarning<T>(IEnumerable<T> collection) => Log(Assembly.GetCallingAssembly(), LogSeverity.Warning, $"[{string.Join(", ", collection)}]");

    /// <summary>Logs an object with <see cref="LogSeverity.Warning"/>.</summary>
    /// <param name="object">The object to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogWarning(object? @object) => Log(Assembly.GetCallingAssembly(), LogSeverity.Warning, @object?.ToString());

    /// <summary>Logs a message with <see cref="LogSeverity.Error"/>.</summary>
    /// <param name="message">The message to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogError(string? message = "") => Log(Assembly.GetCallingAssembly(), LogSeverity.Error, message);

    /// <summary>Logs a collection with <see cref="LogSeverity.Error"/>.</summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="collection">The collection to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogError<T>(IEnumerable<T> collection) => Log(Assembly.GetCallingAssembly(), LogSeverity.Error, $"[{string.Join(", ", collection)}]");

    /// <summary>Logs an object with <see cref="LogSeverity.Error"/>.</summary>
    /// <param name="object">The object to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogError(object? @object) => Log(Assembly.GetCallingAssembly(), LogSeverity.Error, @object?.ToString());

    /// <summary>Logs a message with <see cref="LogSeverity.Fatal"/>.</summary>
    /// <param name="message">The message to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogFatal(string? message = "") => Log(Assembly.GetCallingAssembly(), LogSeverity.Fatal, message);

    /// <summary>Logs a collection with <see cref="LogSeverity.Fatal"/>.</summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="collection">The collection to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogFatal<T>(IEnumerable<T> collection) => Log(Assembly.GetCallingAssembly(), LogSeverity.Fatal, $"[{string.Join(", ", collection)}]");

    /// <summary>Logs an object with <see cref="LogSeverity.Fatal"/>.</summary>
    /// <param name="object">The object to log.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void LogFatal(object? @object) => Log(Assembly.GetCallingAssembly(), LogSeverity.Fatal, @object?.ToString());


    /*********
    ** Internal Methods
    *********/
    /// <summary>Logs a message as an assembly.</summary>
    /// <param name="callingAssembly">The assembly to log the message as.</param>
    /// <param name="severity">The severity of the log.</param>
    /// <param name="message">The message to log.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Log(Assembly callingAssembly, LogSeverity severity, string? message)
    {
        var callingAssemblyName = Path.GetFileNameWithoutExtension(callingAssembly.ManifestModule.Name);
        LogCreator creator = callingAssemblyName switch // TODO: mods
        {
            "NovaEngine" => new("Engine", true),
            var assembly when PlatformAssemblyPattern.IsMatch(assembly) => new("Platform", true),
            var assembly when RendererAssemblyPattern.IsMatch(assembly) => new("Renderer", true),
            var assembly when InputHandlerAssemblyPattern.IsMatch(assembly) => new("Input Handler", true),
            var assembly => new(assembly, false)
        };

        Log(creator, severity, message);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Logs the general engine and system details.</summary>
    private static void LogHeader() => Logger.LogInfo($"{Constants.EngineName} {Constants.EngineVersion} running {Program.Name} on {Environment.OSVersion}");

    /// <summary>Logs a message as a creator.</summary>
    /// <param name="creator">The creator to log the message as.</param>
    /// <param name="severity">The severity of the log.</param>
    /// <param name="message">The message to log.</param>
    private static void Log(LogCreator creator, LogSeverity severity, string? message)
    {
        var log = new Log(creator, severity, message ?? "");
        log.WriteToConsole();
        log.WriteToStream(LogFileStream);
    }
}

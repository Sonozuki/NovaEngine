namespace NovaEngine.Logging
{
    /// <summary>The caller of a log.</summary>
    internal enum LogCaller
    {
        /// <summary>Logged by the engine.</summary>
        Engine,

        /// <summary>Logged by the game.</summary>
        Game,

        /// <summary>Logged by a mod.</summary>
        Mod
    }
}

using System.Reflection;

namespace NovaEngine
{
    /// <summary>Contains application constants.</summary>
    public static class Constants
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The current version of the engine.</summary>
        public static string EngineVersion => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "[version_unknown]";
    }
}

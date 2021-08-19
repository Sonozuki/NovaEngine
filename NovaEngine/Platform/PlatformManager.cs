using NovaEngine.External.Platform;
using NovaEngine.Logging;
using NovaEngine.Platform.Fake;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NovaEngine.Platform
{
    /// <summary>Handles running on difference enironment platforms.</summary>
    public static class PlatformManager
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The platform that is currently being run on.</summary>
        public static IPlatform CurrentPlatform { get; }


        /*********
        ** Public Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>Initialises the class.</summary>
        static PlatformManager()
        {
            // ensure a platform should actually be used
            if (!Program.HasProgramInstance)
            {
                CurrentPlatform = new FakePlatform();
                return;
            }

            // get the current platform object
            var platformFiles = Directory.GetFiles(Environment.CurrentDirectory, "NovaEngine.Platform.*.dll");
            var types = platformFiles
                .Select(platformFile => Assembly.LoadFrom(platformFile))
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.IsClass && !type.IsAbstract);
                
            foreach (var type in types)
            {
                // ensure type is a platform
                if (!type.GetInterfaces().Contains(typeof(IPlatform)))
                    continue;

                // try to create an instance
                var platform = (IPlatform?)Activator.CreateInstance(type);
                if (platform == null)
                    continue;

                // set to the current platform if it's usable
                if (!platform.IsCurrentPlatform)
                    continue;
            
                CurrentPlatform = platform;
                Logger.Log($"Using platform: {type.Assembly.ManifestModule.Name}", LogSeverity.Debug);
            }

            if (CurrentPlatform == null)
                Logger.Log("The environment operating system isn't supported.", LogSeverity.Fatal);
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

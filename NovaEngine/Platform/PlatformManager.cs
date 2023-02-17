using NovaEngine.Platform.Fake;

namespace NovaEngine.Platform;

/// <summary>Handles running on difference enironment platforms.</summary>
internal static class PlatformManager
{
    /*********
    ** Properties
    *********/
    /// <summary>The platform that is currently being run on.</summary>
    public static IPlatform CurrentPlatform { get; }


    /*********
    ** Constructors
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Initialises the class.</summary>
    static PlatformManager()
    {
        if (!Program.HasProgramInstance) // ensure the app is being executed, not called externally
        {
            CurrentPlatform = new FakePlatform();
            return;
        }

        var platformFiles = Directory.GetFiles(Environment.CurrentDirectory, "NovaEngine.Platform.*.dll");
        var types = platformFiles
            .Select(Assembly.LoadFrom)
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Where(type => type.IsClass && !type.IsAbstract);
            
        foreach (var type in types)
        {
            if (!type.IsAssignableTo(typeof(IPlatform)))
                continue;

            var platform = (IPlatform?)Activator.CreateInstance(type);
            if (platform == null)
                continue;

            if (!platform.IsCurrentPlatform)
                continue;
        
            CurrentPlatform = platform;
            Logger.LogDebug($"Using platform: {type.Assembly.ManifestModule.Name}");
            break;
        }

        if (CurrentPlatform == null)
            Logger.LogFatal("The environment operating system isn't supported.");
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

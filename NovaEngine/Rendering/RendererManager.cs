using NovaEngine.Rendering.Fake;

namespace NovaEngine.Rendering;

/// <summary>Handles using difference renderers.</summary>
internal static class RendererManager
{
    /*********
    ** Properties
    *********/
    /// <summary>The renderer that is currently being used.</summary>
    public static IRenderer CurrentRenderer { get; }


    /*********
    ** Constructors
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

    /// <summary>Initialises the class.</summary>
    static RendererManager()
    {
        if (!Program.HasProgramInstance) // ensure the app is being executed, not called externally
        {
            CurrentRenderer = new FakeRenderer();
            return;
        }

        var rendererFiles = Directory.GetFiles(Environment.CurrentDirectory, "NovaEngine.Renderer.*.dll");
        var types = rendererFiles
            .Select(Assembly.LoadFrom)
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Where(type => type.IsClass && !type.IsAbstract);

        foreach (var type in types)
        {
            if (!type.IsAssignableTo(typeof(IRenderer)))
                continue;

            var renderer = (IRenderer?)Activator.CreateInstance(type);
            if (renderer == null)
                continue;

            if (!renderer.CanUseOnPlatform)
                continue;

            CurrentRenderer = renderer;
            Logger.LogDebug($"Using renderer: {type.Assembly.ManifestModule.Name}");
            break;
        }

        if (CurrentRenderer == null)
            Logger.LogFatal("The environment operating system doesn't have a supported renderer.");
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
}

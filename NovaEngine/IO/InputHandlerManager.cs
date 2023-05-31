using NovaEngine.IO.Fake;

namespace NovaEngine.IO;

/// <summary>Handles using difference input handlers.</summary>
internal static class InputHandlerManager
{
    /*********
    ** Properties
    *********/
    /// <summary>The input handler that is currently being used.</summary>
    public static IInputHandler CurrentInputHandler { get; }


    /*********
    ** Constructors
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

    /// <summary>Initialises the class.</summary>
    static InputHandlerManager()
    {
        if (!Program.HasProgramInstance) // ensure the app is being executed, not called externally
        {
            CurrentInputHandler = new FakeInputHandler();
            return;
        }

        var inputHandlerFiles = Directory.GetFiles(Constants.AssemblyDirectory, "NovaEngine.InputHandler.*.dll");
        var types = inputHandlerFiles
            .Select(Assembly.LoadFrom)
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Where(type => type.IsClass && !type.IsAbstract);

        foreach (var type in types)
        {
            if (!type.IsAssignableTo(typeof(IInputHandler)))
                continue;

            var inputHandler = (IInputHandler?)Activator.CreateInstance(type);
            if (inputHandler == null)
                continue;

            if (!inputHandler.CanUseOnPlatform)
                continue;

            CurrentInputHandler = inputHandler;
            Logger.LogDebug($"Using input handler: {type.Assembly.ManifestModule.Name}");
            break;
        }

        if (CurrentInputHandler == null)
            Logger.LogFatal("The environment operating system doesn't have a supported input handler.");
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
}

using NovaEngine.Logging;
using NovaEngine.Rendering.Dummy;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NovaEngine.Rendering
{
    /// <summary>Handles using difference renderers.</summary>
    public abstract class RendererManager
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The renderer that is currently being used.</summary>
        public static IRenderer CurrentRenderer { get; }


        /*********
        ** Public Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. (If the current renderer is null, it's handled and the program is killed before it could cause any null references.)

        /// <summary>Initialises the class.</summary>
        static RendererManager()
        {
            // ensure a renderer should actually be used
            if (!Program.HasProgramInstance)
            {
                CurrentRenderer = new DummyRenderer();
                return;
            }

            // get the current platform object
            var rendererFiles = Directory.GetFiles(Environment.CurrentDirectory, "NovaEngine.Renderer.*.dll");
            var types = rendererFiles
                .Select(rendererFile => Assembly.LoadFrom(rendererFile))
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.IsClass && !type.IsAbstract);

            foreach (var type in types)
            {
                // ensure type is a renderer
                if (!(type.GetInterfaces().Contains(typeof(IRenderer))))
                    continue;

                // try to create an instance
                var renderer = (IRenderer?)Activator.CreateInstance(type);
                if (renderer == null)
                    continue;

                // set to the current renderer if it's usable
                if (!renderer.CanUseOnPlatform)
                    continue;

                CurrentRenderer = renderer;
                Logger.Log($"Using renderer: {type.Assembly.ManifestModule.Name}", LogSeverity.Debug);
            }

            if (CurrentRenderer == null)
                Logger.Log("The environment operating system doesn't have a supported renderer.", LogSeverity.Fatal);
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    }
}

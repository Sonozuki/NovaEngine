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
        /// <summary>Initialises the class.</summary>
        static RendererManager()
        {
            // get the current platform object
            var rendererFiles = Directory.GetFiles(Environment.CurrentDirectory, "NovaEngine.Renderer.*.dll");
            var types = rendererFiles
                .Select(rendererFile => Assembly.LoadFile(rendererFile))
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.IsClass && !type.IsAbstract);

            foreach (var type in types)
            {
                // ensure type is a platform
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
            }

            if (CurrentRenderer == null)
                throw new PlatformNotSupportedException("The environment operating system doesn't have a supported renderer.");
        }
    }
}

using NovaEngine.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NovaEngine.SceneManagement
{
    /// <summary>Manages the game scenes.</summary>
    public static class SceneManager
    {
        /*********
        ** Constants
        *********/
        /// <summary>The file extension of scene files.</summary>
        public const string SceneFileExtension = ".novascene";


        /*********
        ** Fields
        *********/
        /// <summary>The currently loaded scenes.</summary>
        private static List<Scene> LoadedScenes { get; } = new();


        /*********
        ** Public Methods
        *********/
        /// <summary>Loads a scene.</summary>
        /// <param name="path">The path to the scene file to load.</param>
        public static void LoadScene(string path)
        {
            var loadedScene = ContentLoader.Load<Scene>(path + SceneFileExtension);
            LoadedScenes.Add(loadedScene);

            foreach (var rootObject in loadedScene.RootGameObjects)
                rootObject.Start();
        }

        /// <summary>Unloads a scene.</summary>
        /// <param name="name">The case-insensitive name of the scene to unload.</param>
        public static void UnloadScene(string name)
        {
            var sceneToUnload = LoadedScenes.FirstOrDefault(loadedScene => loadedScene.Name.ToLower() == name.ToLower());
            if (sceneToUnload == null)
                throw new ArgumentException($"No scene with the name: {name} is loaded.");

            foreach (var rootObject in sceneToUnload.RootGameObjects)
                rootObject.Dispose();
        }
    }
}

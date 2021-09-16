using NovaEngine.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NovaEngine.SceneManagement
{
    /// <summary>Manages the game scenes.</summary>
    public static class SceneManager
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The currently loaded scenes.</summary>
        public static List<Scene> LoadedScenes { get; } = new();


        /*********
        ** Public Methods
        *********/
        /// <summary>Loads a scene.</summary>
        /// <param name="name">The name of the scene to load.</param>
        public static void LoadScene(string name)
        {
            var loadedScene = ContentLoader.Load<Scene>(Path.Combine(Constants.RelativeSceneDirectory, name + Constants.SceneFileExtension));
            LoadedScenes.Add(loadedScene);
            loadedScene.Start();
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

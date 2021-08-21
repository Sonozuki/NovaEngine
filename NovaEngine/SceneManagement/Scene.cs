using NovaEngine.Core;
using System;
using System.Collections.Generic;

namespace NovaEngine.SceneManagement
{
    /// <summary>Represents a scene.</summary>
    public class Scene : IDisposable
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The name of the scene.</summary>
        public string Name { get; set; }

        /// <summary>Whether the scene is active.</summary>
        public bool IsActive { get; set; }

        /// <summary>The root game objects of the scene.</summary>
        public List<GameObject> RootGameObjects { get; } = new List<GameObject>();


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="name">The name of the scene.</param>
        /// <param name="isActive">Whether the scene is active.</param>
        public Scene(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }

        /// <summary>Updates the scene.</summary>
        public void Update() => RootGameObjects.ForEach(gameObject => gameObject.Update());

        /// <inheritdoc/>
        public void Dispose()
        {
            foreach (var gameObject in RootGameObjects)
                gameObject.Dispose();
        }


        /*********
        ** Private Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>Constructs an instance.</summary>
        private Scene() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

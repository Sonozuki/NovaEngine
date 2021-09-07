using NovaEngine.Core;
using NovaEngine.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>Starts the scene.</summary>
        public void Start()
        {
            var components = RootGameObjects.SelectMany(gameObject => gameObject.GetAllComponents(true));
            Parallel.ForEach(components, component =>
            {
                try
                {
                    component.OnStart();
                }
                catch (Exception ex)
                {
                    Logger.Log($"Component crashed on start. Technical details:\n{ex}", LogSeverity.Error);
                }
            });
        }

        /// <summary>Updates the scene.</summary>
        public void Update()
        {
            var components = RootGameObjects.SelectMany(gameObject => gameObject.GetAllComponents(false));
            Parallel.ForEach(components, component =>
            {
                try
                {
                    if (ApplicationLoop.IsComponentUpdatingPaused && component.IsPausable)
                        return;

                    component.OnUpdate();
                }
                catch (Exception ex)
                {
                    Logger.Log($"Component crashed on update. Technical details:\n{ex}", LogSeverity.Error);
                }
            });
        }

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

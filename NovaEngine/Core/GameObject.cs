using NovaEngine.Components;
using NovaEngine.External.Rendering;
using NovaEngine.Rendering;
using NovaEngine.Serialisation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NovaEngine.Core
{
    /// <summary>Represents a game object.</summary>
    public class GameObject : IDisposable
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The name of the game object.</summary>
        public string Name { get; set; }

        /// <summary>Whether the game object is enabled.</summary>
        public bool IsEnabled { get; set; }

        /// <summary>The parent of the game object.</summary>
        /// <remarks>This is <see langword="null"/> when it's a root game object.</remarks>
        public GameObject? Parent { get; internal set; } // TODO: make a public setter for this note: scene root objects will need to be updated etc

        /// <summary>The children of the game object.</summary>
        public GameObjectChildren Children { get; }

        /// <summary>The components of the game object.</summary>
        public GameObjectComponents Components { get; }

        /// <summary>The transform component of the game object.</summary>
        public Transform Transform { get; }

        /// <summary>The renderer specific game object.</summary>
        [NonSerialisable]
        public RendererGameObjectBase RendererGameObject { get; private set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="name">The name of the game object.</param>
        /// <param name="parent">The parent of the game object.</param>
        /// <param name="sceneName">The name of the scene to add the game object to.<br/>NOTE: this is only used when <paramref name="parent"/> is <see langword="null"/>.<br/>If this is <see langword="null"/>, empty, or is an invalid scene name, then the game object will not be added to a scene.</param>
        /// <param name="isEnabled">Whether the game object is enabled.</param>
        public GameObject(string name, GameObject? parent = null, string? sceneName = null, bool isEnabled = true)
            : this()
        {
            Name = name;
            Children = new(this);
            Components = new(this);
            Transform = new(this);
            IsEnabled = isEnabled;

            // add the gameobject to the parent or as a scene root object
            if (parent != null)
            {
                Parent = parent;
                Parent.Children.Add(this);
            }
            // TODO: add game object to scene
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            RendererGameObject.Dispose();

            foreach (var component in Components)
                component.Dispose();

            foreach (var child in Children)
                child.Dispose();
        }


        /*********
        ** Internal Methods
        *********/
        /// <summary>Retrieves the components of this game object and all children recursively.</summary>
        /// <param name="includeDisabled">Where disabled components and components from disabled game objects should get retrieved.</param>
        /// <returns>The components of this game object and all children recursively.</returns>
        internal List<ComponentBase> GetAllComponents(bool includeDisabled)
        {
            var components = new List<ComponentBase>(Components);
            if (!includeDisabled)
                components = components.Where(component => component.IsEnabled).ToList();
            
            foreach (var child in Children)
                if (includeDisabled || child.IsEnabled)
                    components.AddRange(child.GetAllComponents(includeDisabled));

            return components;
        }


        /*********
        ** Private Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        
        /// <summary>Constructs an instance.</summary>
        private GameObject()
        {
            // technically, this should be ran in a [SerialiserCalled] method, however, because Components.MeshRenderer.set requires
            // the RendererGameObject to be non null, it has be populated here. this will only be a problem if the RendererGameObject
            // requires the members of this object to be filled, which isn't the case for the Vulkan renderer and hopefully not others
            RendererGameObject = RendererManager.CurrentRenderer.CreateRendererGameObject(this);
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

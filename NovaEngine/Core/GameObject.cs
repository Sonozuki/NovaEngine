using NovaEngine.Core.Components;
using NovaEngine.Logging;
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
        ** Fields
        *********/
        /// <summary>The components of the game object.</summary>
        [Serialisable]
        internal readonly List<ComponentBase> Components = new();

        /// <summary>The mesh renderer component of the game object.</summary>
        private MeshRenderer? _MeshRenderer;


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
        public ChildrenCollection Children { get; }

        /// <summary>The transform component of the game object.</summary>
        public Transform Transform { get; }

        /// <summary>The renderer specific game object.</summary>
        [NonSerialisable]
        public RendererGameObjectBase RendererGameObject { get; }

        /// <summary>The mesh renderer component of the game object.</summary>
        [Serialisable]
        public MeshRenderer? MeshRenderer
        {
            get => _MeshRenderer;
            private set
            {
                _MeshRenderer = value;
                if (_MeshRenderer != null)
                    RendererGameObject.UpdateMesh(_MeshRenderer!.Mesh.VertexData, _MeshRenderer.Mesh.IndexData);
            }
        }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="name">The name of the game object.</param>
        /// <param name="parent">The parent of the game object.</param>
        /// <param name="sceneName">The name of the scene to add the game object to.<br/>NOTE: this is only used when <paramref name="parent"/> is <see langword="null"/>.<br/>If this is <see langword="null"/>, empty, or is an invalid scene name, then the game object will not be added to a scene.</param>
        /// <param name="isEnabled">Whether the game object is enabled.</param>
        public GameObject(string name, GameObject? parent = null, string? sceneName = null, bool isEnabled = true)
        {
            Name = name;
            Children = new(this);
            Transform = new(this);
            IsEnabled = isEnabled;
            RendererGameObject = RendererManager.CurrentRenderer.CreateRendererGameObject(this);

            // add the gameobject to the parent or as a scene root object
            if (parent != null)
            {
                Parent = parent;
                Parent.Children.Add(this);
            }
            // TODO: add game object to scene
        }

        /// <summary>Adds a component to the game object.</summary>
        /// <param name="component">The component to add.</param>
        public void AddComponent(ComponentBase component)
        {
            // add component
            component.GameObject = this;
            Components.Add(component);

            // cache specific components
            if (component is MeshRenderer)
                MeshRenderer = GetComponent<MeshRenderer>();
        }

        /// <summary>Removes a type of component from the game object.</summary>
        /// <typeparam name="T">The type of component to remove.</typeparam>
        /// <param name="removeAll">Whether all components of the type <typeparamref name="T"/> should be removed (as opposed to just the first component with that type).</param>
        public void RemoveComponent<T>(bool removeAll)
            where T : ComponentBase
        {
            for (int i = 0; i < Components.Count; i++)
            {
                var component = Components[i];
                if (component.GetType() != typeof(T))
                    continue;

                component.GameObject = null;
                Components.RemoveAt(i--);

                // exit early if only the first component should be removed
                if (!removeAll)
                    break;
            }

            // update caches
            if (typeof(T) == typeof(MeshRenderer))
                MeshRenderer = GetComponent<MeshRenderer>(); // call GetComponent as there may be another mesh renderer component
        }

        /// <summary>Gets the first component with a specified type.</summary>
        /// <typeparam name="T">The type of the component to get.</typeparam>
        /// <param name="includeDisabled">Whether disabled components should be included.</param>
        /// <returns>The first component with the specified type; otherwise, <see langword="null"/> if no component was found.</returns>
        public T? GetComponent<T>(bool includeDisabled = true) where T : ComponentBase => GetComponents<T>(includeDisabled).FirstOrDefault();

        /// <summary>Gets the components with a specified type.</summary>
        /// <typeparam name="T">The type of the components to get.</typeparam>
        /// <param name="includeDisabled">Whether disabled components should be included.</param>
        /// <returns>The components with the specified type.</returns>
        public List<T> GetComponents<T>(bool includeDisabled = true)
            where T : ComponentBase
        {
            // get all the components of type T
            var components = Components
                .Where(component => component is T)
                .Cast<T>()
                .ToList();

            // filter out disabled ones if necessary
            if (!includeDisabled)
                components = components
                    .Where(component => component.IsEnabled)
                    .ToList();

            return components;
        }

        /// <summary>Gets the first component with a specified type from the children.</summary>
        /// <typeparam name="T">The type of the component to get.</typeparam>
        /// <param name="includeDisabled">Whether disabled components should be included.</param>
        /// <returns>The first component with the specified type in the children; otherwise, <see langword="null"/> if no component was found.</returns>
        public T? GetComponentInChildren<T>(bool includeDisabled = true) where T : ComponentBase => GetComponentsInChildren<T>(includeDisabled).FirstOrDefault();

        /// <summary>Gets the components with a specified type from the children.</summary>
        /// <typeparam name="T">The type of the components to get.</typeparam>
        /// <param name="includeDisabled">Whether disabled components should be included.</param>
        /// <returns>The components with the specified type from the children.</returns>
        public List<T> GetComponentsInChildren<T>(bool includeDisabled = true)
            where T : ComponentBase
        {
            // get all the components of type T
            var components = new List<T>();
            foreach (var child in Children)
                components.AddRange(child.GetComponents<T>(includeDisabled));

            return components;
        }

        /// <summary>Gets the first component with a specified type from the parent.</summary>
        /// <typeparam name="T">The type of the component to get.</typeparam>
        /// <param name="includeDisabled">Whether disabled components should be included.</param>
        /// <returns>The first component with the specified type in the parent; otherwise, <see langword="null"/> if no component was found.</returns>
        public T? GetComponentInParent<T>(bool includeDisabled = true) where T : ComponentBase => GetComponentsInParent<T>(includeDisabled).FirstOrDefault();

        /// <summary>Gets the components with a specified type from the parent.</summary>
        /// <typeparam name="T">The type of the components to get.</typeparam>
        /// <param name="includeDisabled">Whether disabled components should be included.</param>
        /// <returns>The components with the specified type from the parent.</returns>
        public List<T> GetComponentsInParent<T>(bool includeDisabled = true) where T : ComponentBase => Parent?.GetComponents<T>(includeDisabled) ?? new List<T>();

        /// <summary>Gets the components with a specified type from this game object and all recursive children (all nodes to leaf nodes).</summary>
        /// <typeparam name="T">The type of the components to get.</typeparam>
        /// <param name="includeDisabled">Whether disabled components should be included.</param>
        /// <returns>The components with the specified type from this game object and all recursive children (all nodes to leaf nodes).</returns>
        public List<T> GetAllComponents<T>(bool includeDisabled = true)
            where T : ComponentBase
        {
            // get all components of type T
            var components = GetComponents<T>(includeDisabled);
            foreach (var child in Children)
                components.AddRange(child.GetAllComponents<T>(includeDisabled));

            return components;
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
        /// <summary>Starts the game object.</summary>
        internal void Start()
        {
            // for loops here so the objects can freely edit themselves
            for (int i = 0; i < Components.Count; i++)
                try
                {
                    Components[i].OnStart();
                }
                catch (Exception ex)
                {
                    Logger.Log($"Component crashed on start. Technical details:\n{ex}", LogSeverity.Error);
                }

            for (int i = 0; i < Children.Count; i++)
                Children[i].Start();
        }

        /// <summary>Updates the game object.</summary>
        internal void Update()
        {
            // for loops here so the objects can freely edit themselves
            for (int i = 0; i < Components.Count; i++)
                try
                {
                    if (Components[i].IsEnabled)
                        Components[i].OnUpdate();
                }
                catch (Exception ex)
                {
                    Logger.Log($"Component crashed on update. Technical detailed:\n{ex}", LogSeverity.Error);
                }

            for (int i = 0; i < Children.Count; i++)
                if (Children[i].IsEnabled)
                    Children[i].Update();
        }


        /*********
        ** Private Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>Constructs an instance.</summary>
        private GameObject()
        {
            RendererGameObject = RendererManager.CurrentRenderer.CreateRendererGameObject(this);
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

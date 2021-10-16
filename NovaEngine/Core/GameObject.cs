using NovaEngine.Components;
using NovaEngine.External.Rendering;
using NovaEngine.Maths;
using NovaEngine.Rendering;
using NovaEngine.Serialisation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NovaEngine.Core
{
    /// <summary>Represents a game object.</summary>
    public class GameObject : IDisposable
    {
        /*********
        ** Fields
        *********/
        /// <summary>The parent of the game object.</summary>
        /// <remarks>This is <see langword="null"/> when it's a root game object.</remarks>
        [Serialisable]
        private GameObject? _Parent;


        /*********
        ** Accessors
        *********/
        /// <summary>The name of the game object.</summary>
        public string Name { get; set; }

        /// <summary>Whether the game object is enabled.</summary>
        public bool IsEnabled { get; set; }

        /// <summary>The transform component of the game object.</summary>
        public Transform Transform { get; }

        /// <summary>The parent of the game object.</summary>
        /// <remarks>This is <see langword="null"/> when it's a root game object.</remarks>
        public GameObject? Parent
        {
            get => _Parent;
            set
            {
                // TODO: ensure the new parent isn't a recursive child of this object
                // TODO: remove the object as a child from it's current parent
                // TODO: if the game object isn't already a child add it as a child

                _Parent = value;

                Transform.ParentPosition = Parent?.Transform.GlobalPosition ?? Vector3.Zero;
                Transform.ParentRotation = Parent?.Transform.GlobalRotation ?? Quaternion.Identity;
                Transform.ParentScale = Parent?.Transform.GlobalScale ?? Vector3.One;
            }
        }

        /// <summary>The children of the game object.</summary>
        public GameObjectChildren Children { get; }

        /// <summary>The components of the game object.</summary>
        public GameObjectComponents Components { get; }

        /// <summary>The renderer specific game object.</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [NonSerialisable]
        public RendererGameObjectBase RendererGameObject { get; private set; }

        /// <summary>A game object with a mesh renderer containing a mesh of a unit size cube.</summary>
        /// <remarks>The same instance is always returned. Make sure to clone the object if using it in a scene.</remarks>
        internal static GameObject Cube => new("Cube", components: new[] { new MeshRenderer(Mesh.Cube) });


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="name">The name of the game object.</param>
        /// <param name="parent">The parent of the game object.</param>
        /// <param name="isEnabled">Whether the game object is enabled.</param>
        /// <param name="components">The components to add to the game object.</param>
        /// <param name="children">The children to add to the game object.</param>
        public GameObject(string name, GameObject? parent = null, bool isEnabled = true, IEnumerable<ComponentBase>? components = null, IEnumerable<GameObject>? children = null)
            : this()
        {
            Name = name;
            Children = new(this, children);
            Components = new(this, components);
            Transform = new(this);
            IsEnabled = isEnabled;

            if (parent != null)
                Parent = parent;
        }

        /// <summary>Deep copies the game object, meaning it will clone all value and reference types.</summary>
        /// <returns>A clone of the object.</returns>
        /// <remarks>This relies on the serialiser, any members that don't get serialised won't get cloned.<br/>The cloned object will be a sibling of the original object.</remarks>
        public GameObject Clone() => Serialiser.Deserialise<GameObject>(Serialiser.Serialise(this))!;

        /// <summary>Deep copies the game object, meaning it will clone all value and reference types.</summary>
        /// <param name="position">The position of the cloned object.</param>
        /// <param name="rotation">The rotation of the cloned object. If <see langword="null"/> is specified, the rotation will be set to <see cref="Quaternion.Identity"/>.</param>
        /// <param name="scale">The scale of the cloned object. If <see langword="null"/> is specified, the scale will be set to <see cref="Vector3.One"/>.</param>
        /// <param name="coordinateSpace">The space the <paramref name="position"/>, <paramref name="rotation"/>, and <paramref name="scale"/> should be set as.</param>
        /// <returns>A clone of the object with the specified transform.</returns>
        /// <remarks>This relies on the serialiser, any members that don't get serialised won't get cloned.<br/>The cloned object will be a sibling of the original object.</remarks>
        /// <exception cref="ArgumentException">Thrown if <paramref name="coordinateSpace"/> is an invalid value.</exception>
        public GameObject Clone(Vector3 position, Quaternion? rotation = null, Vector3? scale = null, Space coordinateSpace = Space.Global)
        {
            var clone = Clone();

            switch (coordinateSpace)
            {
                case Space.Local:
                    clone.Transform.LocalPosition = position;
                    clone.Transform.LocalRotation = rotation ?? Quaternion.Identity;
                    clone.Transform.LocalScale = scale ?? Vector3.One;
                    break;
                case Space.Global:
                    clone.Transform.GlobalPosition = position;
                    clone.Transform.GlobalRotation = rotation ?? Quaternion.Identity;
                    clone.Transform.GlobalScale = scale ?? Vector3.One;
                    break;
                default:
                    throw new ArgumentException("Not a valid enumeration value.", nameof(coordinateSpace));
            }

            return clone;
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

        /// <summary>Retrieves this game object and all children recursively.</summary>
        /// <param name="includeDisabled">Whether disabled game objects should be retrieved.</param>
        /// <returns>This game object and all children recursively.</returns>
        internal List<GameObject> GetAllGameObjects(bool includeDisabled)
        {
            var gameObjects = new List<GameObject>();
            if (!IsEnabled && !includeDisabled)
                return gameObjects;

            gameObjects.Add(this);
            gameObjects.AddRange(Children.SelectMany(child => child.GetAllGameObjects(includeDisabled)));
            return gameObjects;
        }


        /*********
        ** Protected Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        
        /// <summary>Constructs an instance.</summary>
        protected GameObject() // required for serialiser
        {
            // technically, this should be ran in a [SerialiserCalled] method, however, because Components.MeshRenderer.set requires
            // the RendererGameObject to be non null, it has be populated here. this will only be a problem if the RendererGameObject
            // requires the members of this object to be filled, which isn't the case for the Vulkan renderer and hopefully not others
            RendererGameObject = RendererManager.CurrentRenderer.CreateRendererGameObject(this);
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

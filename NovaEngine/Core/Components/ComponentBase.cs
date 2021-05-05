using System;

namespace NovaEngine.Core.Components
{
    /// <summary>Represents the base of a <see cref="Core.GameObject"/> component.</summary>
    /// <remarks>A component refers to some scriptable behaviour.</remarks>
    public abstract class ComponentBase : IDisposable
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether the component is enabled.</summary>
        public bool IsEnabled { get; set; }

        /// <summary>The game object the component is currently on.</summary>
        public GameObject? GameObject { get; internal set; }

        /// <summary>The transform of the game object the component is currently attached to.</summary>
        public Transform? Transform => GameObject?.Transform;


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="isEnabled">Whether the component is enabled.</param>
        public ComponentBase(bool isEnabled = true)
        {
            IsEnabled = isEnabled;
        }

        /// <summary>Invoked when the game starts (before the first tick).</summary>
        public virtual void OnStart() { }

        /// <summary>Invoked when the component should get updated (once per tick).</summary>
        public virtual void OnUpdate() { }

        /// <inheritdoc/>
        public virtual void Dispose() { }
    }
}

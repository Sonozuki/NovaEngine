using NovaEngine.Core;
using NovaEngine.Serialisation;
using System;

namespace NovaEngine.Components
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

        /// <summary>Whether this component is affected by <see cref="ApplicationLoop.IsComponentUpdatingPaused"/>.</summary>
        public bool IsPausable { get; set; } = true;

        /// <summary>The game object the component is currently on.</summary>
        [Serialisable]
        protected internal GameObject GameObject { get; internal set; }

        /// <summary>The transform of the game object the component is currently attached to.</summary>
        [Serialisable]
        protected internal Transform Transform => GameObject.Transform;


        /*********
        ** Public Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>Constructs an instance.</summary>
        /// <param name="isEnabled">Whether the component is enabled.</param>
        public ComponentBase(bool isEnabled = true)
        {
            IsEnabled = isEnabled;
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <inheritdoc/>
        public virtual void Dispose() { }


        /*********
        ** Protected Internal Methods
        *********/
        /// <summary>Invoked when the game starts (before the first tick).</summary>
        protected internal virtual void OnStart() { }

        /// <summary>Invoked when the component should get updated (once per tick).</summary>
        protected internal virtual void OnUpdate() { }
    }
}

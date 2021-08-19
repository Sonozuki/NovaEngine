using NovaEngine.Core;
using NovaEngine.Core.Components;
using NovaEngine.Graphics;

namespace NovaEngine.External.Rendering
{
    /// <summary>Represents a renderer game object.</summary>
    public abstract class RendererGameObjectBase
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The underlying game object.</summary>
        public GameObject BaseGameObject { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="baseGameObject">The underlying game object.</param>
        public RendererGameObjectBase(GameObject baseGameObject)
        {
            BaseGameObject = baseGameObject;
        }

        /// <summary>Invoked whenever the mesh of a game object gets updated.</summary>
        /// <param name="vertices">The vertices of the new mesh.</param>
        /// <param name="indices">The indices of the new mesh.</param>
        public abstract void UpdateMesh(Vertex[] vertices, uint[] indices);

        /// <summary>Invoked every tick to update the game object for a specified camera, to update a UBO.</summary>
        /// <param name="camera">The camera to update the game object UBO with.</param>
        public abstract void UpdateUBO(Camera camera);

        /// <inheritdoc/>
        public abstract void Dispose();
    }
}

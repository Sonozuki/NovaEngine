using NovaEngine.Core;
using NovaEngine.Core.Components;
using NovaEngine.Graphics;

namespace NovaEngine.Rendering.Dummy
{
    /// <summary>Represents a game object that is only used when nova is being used without a program instance.</summary>
    public class DummyGameObject : RendererGameObjectBase
    {
        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public DummyGameObject(GameObject baseGameObject)
            : base(baseGameObject) { }

        /// <inheritdoc/>
        public override void UpdateMesh(Vertex[] vertices, uint[] indices) { }

        /// <inheritdoc/>
        public override void UpdateUBO(Camera camera) { }

        /// <inheritdoc/>
        public override void Dispose() { }


        /*********
        ** Private Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        public DummyGameObject() : base(new("Dummy")) { }
    }
}

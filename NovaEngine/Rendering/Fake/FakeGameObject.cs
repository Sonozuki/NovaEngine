using NovaEngine.Components;
using NovaEngine.Core;
using NovaEngine.External.Rendering;
using NovaEngine.Graphics;

namespace NovaEngine.Rendering.Fake
{
    /// <summary>Represents a game object that is only used when nova is being used without a program instance.</summary>
    internal class FakeGameObject : RendererGameObjectBase
    {
        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public FakeGameObject(GameObject baseGameObject)
            : base(baseGameObject) { }

        /// <inheritdoc/>
        public override void UpdateMesh(Vertex[] vertices, uint[] indices, MeshType meshType) { }

        /// <inheritdoc/>
        public override void UpdateUBO(Camera camera) { }

        /// <inheritdoc/>
        public override void Dispose() { }


        /*********
        ** Private Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        public FakeGameObject() : base(new("Fake")) { }
    }
}

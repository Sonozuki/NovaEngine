namespace NovaEngine.Core.Components
{
    /// <summary>Represents a component used for rendering a mesh.</summary>
    public class MeshRenderer : ComponentBase
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The mesh to render.</summary>
        public Mesh Mesh { get; set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="mesh">The mesh to render.</param>
        public MeshRenderer(Mesh mesh)
        {
            Mesh = mesh;
        }
    }
}

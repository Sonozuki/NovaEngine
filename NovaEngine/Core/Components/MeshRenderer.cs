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


        /*********
        ** Private Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>Constructs an instance.</summary>
        private MeshRenderer() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

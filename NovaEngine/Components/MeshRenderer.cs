using NovaEngine.Core;

namespace NovaEngine.Components
{
    /// <summary>Represents a component used for rendering a mesh.</summary>
    public class MeshRenderer : ComponentBase
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The mesh to render.</summary>
        public Mesh Mesh { get; set; }

        /// <summary>The material to use when rendering the mesh.</summary>
        public Material Material { get; private set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="mesh">The mesh to render.</param>
        /// <param name="material">The material to use when rendering the mesh.</param>
        public MeshRenderer(Mesh mesh, Material? material = null)
        {
            Mesh = mesh;
            Material = material ?? Material.Default;
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

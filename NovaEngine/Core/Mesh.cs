using NovaEngine.Graphics;

namespace NovaEngine.Core
{
    /// <summary>Represents a mesh.</summary>
    public class Mesh
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The name of the mesh.</summary>
        public string Name { get; }

        /// <summary>The data for the vertex buffer.</summary>
        public Vertex[] VertexData { get; private set; }

        /// <summary>The data for the index buffer.</summary>
        public uint[] IndexData { get; private set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="name">The name of the mesh.</param>
        /// <param name="vertexData">The data for the vertex buffer.</param>
        /// <param name="indexData">The data for the index buffer.</param>
        public Mesh(string name, Vertex[] vertexData, uint[] indexData)
        {
            Name = name;
            VertexData = vertexData;
            IndexData = indexData;
        }
    }
}

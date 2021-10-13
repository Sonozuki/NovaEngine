using NovaEngine.Graphics;
using NovaEngine.Serialisation;

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

        /// <summary>The mesh for a unit size cube.</summary>
        /// <remarks>The same instance is always returned. Make sure to clone the object if using it in a scene.</remarks>
        internal static Mesh Cube { get; } = new("Cube",
            vertexData: new Vertex[]
            {
                new(new(-.5f, -.5f, -.5f)),
                new(new(-.5f, -.5f, .5f)),
                new(new(-.5f, .5f, -.5f)),
                new(new(-.5f, .5f, .5f)),
                new(new(.5f, -.5f, -.5f)),
                new(new(.5f, -.5f, .5f)),
                new(new(.5f, .5f, -.5f)),
                new(new(.5f, .5f, .5f))
            },
            indexData: new uint[]
            {
                1, 0, 4,
                1, 4, 5,
                2, 4, 0,
                2, 6, 4,
                3, 0, 1,
                3, 2, 0,
                6, 5, 4,
                6, 7, 5,
                7, 1, 5,
                7, 2, 3,
                7, 3, 1,
                7, 6, 2
            });


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


        /*********
        ** Protected Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        
        /// <summary>Constructs an instance.</summary>
        protected Mesh() { } // required for serialiser

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

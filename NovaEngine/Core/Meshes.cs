using NovaEngine.Graphics;

namespace NovaEngine.Core
{
    /// <summary>Contains static primitive meshes.</summary>
    internal static class Meshes
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The mesh for a unit size cube.</summary>
        public static Mesh Cube { get; } = new("Cube",
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
    }
}

using NovaEngine.Graphics;
using NovaEngine.Maths;
using NovaEngine.SceneManagement;

namespace NovaEngine.Debugging
{
    /// <summary>A visual debugging aid used for drawing primitive shapes.</summary>
    public static class Gizmos
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The colour that is used to render the gizmos when a 'colour' parameter isn't specified.</summary>
        public static Colour DefaultColour { get; } = Colour.DarkGray;


        /*********
        ** Public Methods
        *********/
        /// <summary>Draws a cube.</summary>
        /// <param name="position">The position of the centre of the cube.</param>
        /// <param name="scale">The scale of the cube.</param>
        /// <param name="colour">The colour of the cube. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
        public static void DrawCube(Vector3 position, Vector3 scale, Colour? colour = null) => SceneManager.GizmosScene.AddCube(position, scale, colour ?? DefaultColour);

        /// <summary>Draws a sphere.</summary>
        /// <param name="position">The position of the centre of the sphere.</param>
        /// <param name="radius">The radius of the sphere.</param>
        /// <param name="colour">The colour of the sphere. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
        public static void DrawSphere(Vector3 position, float radius, Colour? colour = null) => SceneManager.GizmosScene.AddSphere(position, radius, colour ?? DefaultColour);

        /// <summary>Draws a line.</summary>
        /// <param name="point1">The first point of the line.</param>
        /// <param name="point2">The second point of the line.</param>
        /// <param name="colour">The colour of the line. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
        public static void DrawLine(Vector3 point1, Vector3 point2, Colour? colour = null) => SceneManager.GizmosScene.AddLine(point1, point2, colour ?? DefaultColour);

        /// <summary>Draws a line.</summary>
        /// <param name="position">The position of the first point of the line.</param>
        /// <param name="direction">The direction from <paramref name="position"/> to the second point.</param>
        /// <param name="distance">The distance between the two points.</param>
        /// <param name="colour">The colour of the line. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
        public static void DrawLine(Vector3 position, Vector3 direction, float distance, Colour? colour = null) => DrawLine(position, position + direction.Normalised * distance, colour);
    }
}

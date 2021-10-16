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
    }
}

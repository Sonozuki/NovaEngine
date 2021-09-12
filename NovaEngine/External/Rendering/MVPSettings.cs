using NovaEngine.Components;
using NovaEngine.Core;

namespace NovaEngine.External.Rendering
{
    /// <summary>Represents settings that are used to alter how <see cref="Transform.Matrix"/> and <see cref="Camera.ViewMatrix"/> will be calculated.</summary>
    public class MVPSettings
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether the X position should be inverted for model and view matrices.</summary>
        /// <remarks>Without inversion, the X axis points right.</remarks>
        public bool InvertX { get; }

        /// <summary>Whether the Y position should be inverted for model and view matrices.</summary>
        /// <remarks>Without inversion, the Y axis points up.</remarks>
        public bool InvertY { get; }

        /// <summary>Whether the Z position should be inverted for model and view matrices.</summary>
        /// <remarks>Without inversion, the Z axis points forward.</remarks>
        public bool InvertZ { get; }

        /// <summary>Whether the rotations should be inverted for model and view matrices.</summary>
        /// <remarks>Without inversion, the rotation is clockwise.</remarks>
        public bool InvertRotation { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="invertX"></param>
        /// <param name="invertY"></param>
        /// <param name="invertZ"></param>
        /// <param name="invertRotation"></param>
        public MVPSettings(bool invertX, bool invertY, bool invertZ, bool invertRotation)
        {
            InvertX = invertX;
            InvertY = invertY;
            InvertZ = invertZ;
            InvertRotation = invertRotation;
        }
    }
}

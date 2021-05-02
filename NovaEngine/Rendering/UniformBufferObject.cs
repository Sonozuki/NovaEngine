using NovaEngine.Maths;

namespace NovaEngine.Rendering
{
    /// <summary>A UBO structure.</summary>
    public struct UniformBufferObject
    {
        /*********
        ** Fields
        *********/
        /// <summary>The model matrix.</summary>
        public Matrix4x4 Model;

        /// <summary>The view matrix.</summary>
        public Matrix4x4 View;

        /// <summary>The projection matrix.</summary>
        public Matrix4x4 Projection;
    }
}

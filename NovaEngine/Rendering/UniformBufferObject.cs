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

        /// <summary>The position of the camera.</summary>
        public Vector3 CameraPosition;


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="model">The model matrix.</param>
        /// <param name="view">The view matrix.</param>
        /// <param name="projection">The projection matrix.</param>
        /// <param name="cameraPosition">The position of the camera.</param>
        public UniformBufferObject(Matrix4x4 model, Matrix4x4 view, Matrix4x4 projection, Vector3 cameraPosition)
        {
            Model = model;
            View = view;
            Projection = projection;
            CameraPosition = cameraPosition;
        }
    }
}

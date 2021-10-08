using NovaEngine.Maths;
using NovaEngine.Rendering;

namespace NovaEngine
{
    /// <summary>Creates generic miscellaneous methods.</summary>
    internal static class Utilities
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Creates a model matrix.</summary>
        /// <param name="position">The position of the model.</param>
        /// <param name="scale">The scale of the model.</param>
        /// <returns>The model matrix.</returns>
        public static Matrix4x4 CreateModelMatrix(Vector3 position, Vector3 scale) =>
            Matrix4x4.CreateScale(scale)
          * Matrix4x4.CreateTranslation(
                position.X * (RendererManager.MVPSettings.InvertX ? -1 : 1),
                position.Y * (RendererManager.MVPSettings.InvertY ? -1 : 1),
                position.Z * (RendererManager.MVPSettings.InvertZ ? -1 : 1));

        /// <summary>Creates a model matrix.</summary>
        /// <param name="position">The position of the model.</param>
        /// <param name="rotation">The rotation of the model.</param>
        /// <param name="scale">The scale of the model.</param>
        /// <returns>The model matrix.</returns>
        public static Matrix4x4 CreateModelMatrix(Vector3 position, Quaternion rotation, Vector3 scale) =>
            Matrix4x4.CreateScale(scale)
          * Matrix4x4.CreateTranslation(
                position.X * (RendererManager.MVPSettings.InvertX ? -1 : 1),
                position.Y * (RendererManager.MVPSettings.InvertY ? -1 : 1),
                position.Z * (RendererManager.MVPSettings.InvertZ ? -1 : 1))
          * Matrix4x4.CreateFromQuaternion(RendererManager.MVPSettings.InvertRotation ? rotation.Inverse : rotation);

        /// <summary>Create a view matrix.</summary>
        /// <param name="position">The position of the view.</param>
        /// <param name="rotation">The rotation of the view.</param>
        /// <returns>The view matrix.</returns>
        public static Matrix4x4 CreateViewMatrix(Vector3 position, Quaternion rotation) =>
            Matrix4x4.CreateTranslation(
                position.X * (RendererManager.MVPSettings.InvertX ? 1 : -1),
                position.Y * (RendererManager.MVPSettings.InvertY ? 1 : -1),
                position.Z * (RendererManager.MVPSettings.InvertZ ? 1 : -1))
          * Matrix4x4.CreateFromQuaternion(RendererManager.MVPSettings.InvertRotation ? rotation : rotation.Inverse);
    }
}

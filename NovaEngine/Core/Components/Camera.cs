using NovaEngine.Maths;
using System;

namespace NovaEngine.Core.Components
{
    /// <summary>Represents a 'camera'.</summary>
    public class Camera : ComponentBase
    {
        /*********
        ** Acecssors
        *********/
        /// <summary>The projection to use for the camera.</summary>
        public CameraProjection Projection { get; set; }

        /// <summary>The field of view of the camera in degrees (perspective projection only).</summary>
        public float FieldOfView { get; set; }

        /// <summary>The width of the view frustum (orthograhic projection only).</summary>
        public float Width { get; set; }

        /// <summary>The height of the view frustum (orthograhic projection only).</summary>
        public float Height { get; set; }

        /// <summary>The near clipping plane of the camera.</summary>
        public float NearClippingPlane { get; set; }

        /// <summary>The far clipping plane of the camera.</summary>
        public float FarClippingPlane { get; set; }

        /// <summary>The projection matrix of the camera.</summary>
        public Matrix4x4 Matrix
        {
            get
            {
                if (Projection == CameraProjection.Perspective)
                    return Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, Program.MainWindow.Size.Width / (float)Program.MainWindow.Size.Height, NearClippingPlane, FarClippingPlane);
                else
                    return Matrix4x4.CreateOrthographic(Width, Height, NearClippingPlane, FarClippingPlane);
            }
        }

        /// <summary>The camera to use when rendering.</summary>
        public static Camera? Main { get; set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="setMainCamera">Whether <see cref="Main"/> should be set to this camera.</param>
        public Camera(bool setMainCamera)
            : this(90, .01f, 1000, setMainCamera) { }

        /// <summary>Constructs a perspective instance.</summary>
        /// <param name="fieldOfView">The field of view of the camera, in degrees.</param>
        /// <param name="nearClippingPlane">The near clipping plane of the camera.</param>
        /// <param name="farClippingPlane">The far clipping plane of the camera.</param>
        /// <param name="setMainCamera">Whether <see cref="Main"/> should be set to this camnera.</param>
        public Camera(float fieldOfView, float nearClippingPlane, float farClippingPlane, bool setMainCamera)
            : this(CameraProjection.Perspective, fieldOfView, 0, 0, nearClippingPlane, farClippingPlane, setMainCamera) { }

        /// <summary>Constructs an orthographic instance.</summary>
        /// <param name="width">The width of the view frustum.</param>
        /// <param name="height">The height of the view frustum.</param>
        /// <param name="nearClippingPlane">The near clipping plane of the camera.</param>
        /// <param name="farClippingPlane">The far clipping plane of the camera.</param>
        /// <param name="setMainCamera">Whether <see cref="Main"/> should be set to this camnera.</param>
        public Camera(float width, float height, float nearClippingPlane, float farClippingPlane, bool setMainCamera)
            : this(CameraProjection.Othographic, 0, width, height, nearClippingPlane, farClippingPlane, setMainCamera) { }

        /// <summary>Renders a frame using the camera.</summary>
        public void Render() => throw new NotImplementedException();


        /*********
        ** Private Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        private Camera() { }

        /// <summary>Constructs an instance.</summary>
        /// <param name="projection"></param>
        /// <param name="fieldOfView">The field of view of the camera, in degrees (perspective projection only).</param>
        /// <param name="width">The width of the view frustum (orthograhic projection only).</param>
        /// <param name="height">The height of the view frustum (orthograhic projection only).</param>
        /// <param name="nearClippingPlane">The near clipping place of the camera.</param>
        /// <param name="farClippingPlane">The far clipping place of the camera.</param>
        /// <param name="setMainCamera">Whether <see cref="Main"/> should be set to this camnera.</param>
        private Camera(CameraProjection projection, float fieldOfView, float width, float height, float nearClippingPlane, float farClippingPlane, bool setMainCamera)
        {
            Projection = projection;
            FieldOfView = fieldOfView;
            Width = width;
            Height = height;
            NearClippingPlane = nearClippingPlane;
            FarClippingPlane = farClippingPlane;

            if (setMainCamera)
                Camera.Main = this;
        }
    }
}

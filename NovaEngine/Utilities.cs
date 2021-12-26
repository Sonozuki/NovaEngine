using NovaEngine.Maths;
using System;

namespace NovaEngine
{
    /// <summary>Creates generic miscellaneous methods.</summary>
    public static class Utilities
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Calculates the eight corners of a frustum.</summary>
        /// <param name="position">The position of the tip of the frustum.</param>
        /// <param name="rotation">The rotation of the frustum (when looking from the tip to the base).</param>
        /// <param name="fov">The angle of the tip of the frustum, in degrees.</param>
        /// <param name="aspectRatio">The aspect ratio (width / height) of the frustum.</param>
        /// <param name="nearPlane">The distance from the tip to the near plane of the frustum.</param>
        /// <param name="farPlane">The distance from the tip to the far plane of the frustum.</param>
        /// <returns>The eight points that make up the frustum.<br/><br/>The order of the points is:<br/>[0] far top left<br/>[1] far top right<br/>[2] far bottom left<br/>[3] far bottom right<br/>[4] near top left<br/>[5] near top right<br/>[6] near bottom left<br/>[7] near bottom right.</returns>
        public static Vector3[] CalculatePerspectiveFrustumCorners(Vector3 position, Quaternion rotation, float fov, float aspectRatio, float nearPlane, float farPlane)
        {
            var yFactor = MathF.Tan(fov * MathF.PI / 360); // TODO: constant
            var xFactor = yFactor * aspectRatio;

            var forward = Vector3.UnitZ * rotation;
            var right = Vector3.UnitX * rotation;
            var up = Vector3.UnitY * rotation;

            var forwardFarPlane = forward * farPlane;
            var rightFarPlane = right * xFactor * farPlane;
            var upFarPlane = up * yFactor * farPlane;
            var forwardNearPlane = forward * nearPlane;
            var rightNearPlane = right * xFactor * nearPlane;
            var upNearPlane = up * yFactor * nearPlane;

            return new Vector3[]
            {
                position + forwardFarPlane - rightFarPlane + upFarPlane, // far top left
                position + forwardFarPlane + rightFarPlane + upFarPlane, // far top right
                position + forwardFarPlane - rightFarPlane - upFarPlane, // far bottom left
                position + forwardFarPlane + rightFarPlane - upFarPlane, // far bottom right
                position + forwardNearPlane - rightNearPlane + upNearPlane, // near top left
                position + forwardNearPlane + rightNearPlane + upNearPlane, // near top right
                position + forwardNearPlane - rightNearPlane - upNearPlane, // near bottom left
                position + forwardNearPlane + rightNearPlane - upNearPlane, // near bottom right
            };
        }

        /// <summary>Calculates the eight corners of a frustum.</summary>
        /// <param name="position">The position of the tip of the frustum.</param>
        /// <param name="rotation">The totation of the frustum (when looking from the tip to the base).</param>
        /// <param name="width">The width of the frustum.</param>
        /// <param name="height">The height of the frustum.</param>
        /// <param name="nearPlane">The distance from the tip to the near plane of the frustum.</param>
        /// <param name="farPlane">The distance from the tip to the far plane of the frustum.</param>
        /// <returns>The eight points that make up the frustum.<br/><br/>The order of the points is:<br/>[0] far top left<br/>[1] far top right<br/>[2] far bottom left<br/>[3] far bottom right<br/>[4] near top left<br/>[5] near top right<br/>[6] near bottom left<br/>[7] near bottom right.</returns>
        public static Vector3[] CalculateOrthographicFrustumCorners(Vector3 position, Quaternion rotation, float width, float height, float nearPlane, float farPlane)
        {
            var forward = Vector3.UnitZ * rotation;
            var right = Vector3.UnitX * rotation;
            var up = Vector3.UnitY * rotation;

            var halfWidth = width / 2;
            var halfHeight = height / 2;

            var forwardFarPlane = forward * farPlane;
            var forwardNearPlane = forward * nearPlane;
            var rightPlane = right * halfWidth;
            var upPlane = up * halfHeight;

            return new Vector3[]
            {
                position + forwardFarPlane - rightPlane + upPlane, // far top left
                position + forwardFarPlane + rightPlane + upPlane, // far top right
                position + forwardFarPlane - rightPlane - upPlane, // far bottom left
                position + forwardFarPlane + rightPlane - upPlane, // far bottom right
                position + forwardNearPlane - rightPlane + upPlane, // near top left
                position + forwardNearPlane + rightPlane + upPlane, // near top right
                position + forwardNearPlane - rightPlane - upPlane, // near bottom left
                position + forwardNearPlane + rightPlane - upPlane, // near bottom right
            };
        }
    }
}

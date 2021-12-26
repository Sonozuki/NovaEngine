namespace NovaEngine.Debugging;

/// <summary>A visual debugging aid used for drawing primitive shapes.</summary>
public static class Gizmos
{
    /*********
    ** Accessors
    *********/
    /// <summary>The colour that is used to render the gizmos when a 'colour' parameter isn't specified.</summary>
    public static Colour DefaultColour { get; set; } = Colour.DarkGray;


    /*********
    ** Public Methods
    *********/
    /// <summary>Draws a cube.</summary>
    /// <param name="position">The position of the centre of the cube.</param>
    /// <param name="rotation">The rotation of the cube. If <see langword="null"/>, then <see cref="Quaternion.Identity"/> will be used.</param>
    /// <param name="scale">The scale of the cube. If <see langword="null"/>, then <see cref="Vector3.One"/> will be used.</param>
    /// <param name="colour">The colour of the cube. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
    public static void DrawCube(Vector3 position, Quaternion? rotation = null, Vector3? scale = null, Colour? colour = null) => SceneManager.GizmosScene.AddCube(position, rotation ?? Quaternion.Identity, scale ?? Vector3.One, colour ?? DefaultColour);

    /// <summary>Draws a sphere.</summary>
    /// <param name="position">The position of the centre of the sphere.</param>
    /// <param name="radius">The radius of the sphere. If <see langword="null"/>, then 0.5 will be used.</param>
    /// <param name="colour">The colour of the sphere. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
    public static void DrawSphere(Vector3 position, float? radius = null, Colour? colour = null) => SceneManager.GizmosScene.AddSphere(position, radius ?? .5f, colour ?? DefaultColour);

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

    /// <summary>Draws a frustum.</summary>
    /// <param name="camera">The camera whose view frustum should be drawn.</param>
    /// <param name="colour">The colour of the frustum. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
    public static void DrawCameraFrustum(Camera camera, Colour? colour = null)
    {
        if (camera.Projection == CameraProjection.Perspective)
            DrawPerspectiveFrustum(camera.Transform.GlobalPosition, camera.Transform.GlobalRotation, camera.FieldOfView, camera.AspectRatio, camera.NearClippingPlane, camera.FarClippingPlane, colour);
        else
            DrawOrthographicFrustum(camera.Transform.GlobalPosition, camera.Transform.GlobalRotation, camera.Width, camera.Height, camera.NearClippingPlane, camera.FarClippingPlane, colour);
    }

    /// <summary>Draws a perspective frustum.</summary>
    /// <param name="position">The position of the tip of the frustum.</param>
    /// <param name="rotation">The rotation of the frustum (when looking from the tip to the base).</param>
    /// <param name="fov">The angle of the tip of the frustum, in degrees.</param>
    /// <param name="aspectRatio">The aspect ratio (width / height) of the frustum.</param>
    /// <param name="nearPlane">The distance from the tip to the near plane of the frustum.</param>
    /// <param name="farPlane">The distance from the tip to the far plane of the frustum.</param>
    /// <param name="colour">The colour of the frustum. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
    public static void DrawPerspectiveFrustum(Vector3 position, Quaternion rotation, float fov, float aspectRatio, float nearPlane, float farPlane, Colour? colour = null)
    {
        var points = Utilities.CalculatePerspectiveFrustumCorners(position, rotation, fov, aspectRatio, nearPlane, farPlane);
        DrawEightPointFrustum(points, colour ?? DefaultColour);
    }

    /// <summary>Draws an orthographic frustum.</summary>
    /// <param name="position">The position of the tip of the frustum.</param>
    /// <param name="rotation">The totation of the frustum (when looking from the tip to the base).</param>
    /// <param name="width">The width of the frustum.</param>
    /// <param name="height">The height of the frustum.</param>
    /// <param name="nearPlane">The distance from the tip to the near plane of the frustum.</param>
    /// <param name="farPlane">The distance from the tip to the far plane of the frustum.</param>
    /// <param name="colour">The colour of the frustum. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
    public static void DrawOrthographicFrustum(Vector3 position, Quaternion rotation, float width, float height, float nearPlane, float farPlane, Colour? colour = null)
    {
        var points = Utilities.CalculateOrthographicFrustumCorners(position, rotation, width, height, nearPlane, farPlane);
        DrawEightPointFrustum(points, colour ?? DefaultColour);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Draws a frustum from eight points.</summary>
    /// <param name="points">The eight points to draw the frustum from.</param>
    /// <param name="colour">The colour of the lines.</param>
    /// <remarks>It's the callers responsibility to ensure <paramref name="points"/> contains eight points, the points are assumed to be the following order:<br/>[0] far top left<br/>[1] far top right<br/>[2] far bottom left<br/>[3] far bottom right<br/>[4] near top left<br/>[5] near top right<br/>[6] near bottom left<br/>[7] near bottom right</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DrawEightPointFrustum(Vector3[] points, Colour colour)
    {
        // far plane outline
        DrawLine(points[0], points[1], colour);
        DrawLine(points[0], points[2], colour);
        DrawLine(points[2], points[3], colour);
        DrawLine(points[3], points[1], colour);
        
        // near plane outline
        DrawLine(points[4], points[5], colour);
        DrawLine(points[4], points[6], colour);
        DrawLine(points[6], points[7], colour);
        DrawLine(points[7], points[5], colour);
        
        // lines between near and far planes
        DrawLine(points[0], points[4], colour);
        DrawLine(points[1], points[5], colour);
        DrawLine(points[2], points[6], colour);
        DrawLine(points[3], points[7], colour);
    }
}

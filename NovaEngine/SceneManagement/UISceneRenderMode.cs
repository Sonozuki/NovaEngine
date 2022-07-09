namespace NovaEngine.SceneManagement;

/// <summary>The render modes for a UI scene.</summary>
public enum UISceneRenderMode
{
    /// <summary>All elements are relative to the screen space, elements are rendered as part of all camera's render calls meaning they are present in the camera's render texture.</summary>
    ScreenSpaceOverlay,

    /// <summary>All elements are relative to the screen space, elements are rendered as part of a specific camera's render call meaning they are present in the camera's render texture.</summary>
    ScreenSpaceCamera,

    /// <summary>All elements are relative to the world space, elements are rendered as part of the camera render call meaning they are present in the camera's render texture.</summary>
    WorldSpace
}

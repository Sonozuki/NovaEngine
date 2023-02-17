using System.Globalization;

namespace NovaEngine.Globalisation;

/// <summary>Contains helpful members related to globalisation.</summary>
public static class G11n
{
    /*********
    ** Properties
    *********/
    /// <summary>The culture to use internally throughout the engine.</summary>
    /// <remarks>
    /// This is used to ensure that parsing and serialisation works across cultures which would normally be completely incompatible.<br/>
    /// This is based on the invariant culture.
    /// </remarks>
    public static CultureInfo Culture { get; } = new("");
}

namespace NovaEngine.ContentPipeline.Font;

/// <summary>The defined platforms.</summary>
internal enum PlatformId : ushort
{
    /// <summary>The unicode platform.</summary>
    Unicode,

    /// <summary>The macintosh platform.</summary>
    Macintosh,

    /// <summary>The ISO platform (<i>deprecated</i>).</summary>
    ISO,

    /// <summary>The windows platform.</summary>
    Windows,

    /// <summary>A custom platform.</summary>
    Custom
}

namespace NovaEngine.Rendering;

/// <summary>The number of samples per pixel that can be used for multisample anti-aliasing (MSAA).</summary>
public enum SampleCount
{
    /// <summary>1 sample per pixel will be used (no MSAA).</summary>
    _1,

    /// <summary>2 samples per pixel will be used.</summary>
    _2,

    /// <summary>4 samples per pixel will be used.</summary>
    _4,

    /// <summary>8 samples per pixel will be used.</summary>
    _8,

    /// <summary>16 samples per pixel will be used.</summary>
    _16,

    /// <summary>32 samples per pixel will be used.</summary>
    _32,

    /// <summary>64 samples per pixel will be used.</summary>
    _64
}

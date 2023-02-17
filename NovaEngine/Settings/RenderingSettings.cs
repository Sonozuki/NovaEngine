namespace NovaEngine.Settings;

/// <summary>The application settings related to rendering.</summary>
public sealed class RenderingSettings : SettingsBase<RenderingSettings>
{
    /*********
    ** Fields
    *********/
    /// <summary>The current number of samples per pixel to use in multisample anti aliasing (MSAA).</summary>
    private SampleCount _SampleCount = SampleCount._8;


    /*********
    ** Properties
    *********/
#pragma warning disable CA1822 // Member does not access instance data and can be marked as static.

    /// <summary>The maximum number of samples per pixel that can be used in multisample anti aliasing (MSAA).</summary>
    [JsonIgnore]
    public SampleCount MaxSampleCount => RendererManager.CurrentRenderer.MaxSampleCount;

#pragma warning restore CA1822 // Member does not access instance data and can be marked as static.

    /// <summary>The current number of samples per pixel to use in multisample anti aliasing (MSAA).</summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SampleCount SampleCount
    {
        get => _SampleCount;
        set => _SampleCount = (SampleCount)Math.Clamp((int)value, 0, (int)MaxSampleCount);
    }

    /// <inheritdoc/>
    protected override string Path => Constants.RenderingSettingsFilePath;

    /// <inheritdoc/>
    protected override string InvalidPath => Constants.InvalidRenderingSettingsFilePath;
}

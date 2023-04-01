namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="PanelPair"/>.</summary>
internal sealed class PanelPairViewModel : BindableObject
{
    /*********
    ** Fields
    *********/
    /// <summary>The orientation of the pair.</summary>
    public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(PanelPairViewModel));


    /*********
    ** Properties
    *********/
    /// <summary>The orientation of the pair.</summary>
    public StackOrientation Orientation
    {
        get => (StackOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>The pair of panels.</summary>
    public ObservablePair<PanelBase> Panels { get; } = new();
}

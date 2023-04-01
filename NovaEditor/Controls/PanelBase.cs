namespace NovaEditor.Controls;

/// <summary>Represents a panel.</summary>
/// <remarks>A panel refers to a control that can be docked.</remarks>
public abstract partial class PanelBase : ContentView
{
    /*********
    ** Fields
    *********/
    /// <summary>The title of the panel.</summary>
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(PanelBase));


    /*********
    ** Properties
    *********/
    /// <summary>The title of the panel.</summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}

namespace NovaEditor.Extensions;

/// <summary>Represents the attachment properties for <see cref="Button"/>.</summary>
public sealed class ButtonExtensions
{
    /*********
    ** Fields
    *********/
    /// <summary>Whether the button is rounded.</summary>
    public static readonly DependencyProperty IsRoundedProperty = DependencyProperty.RegisterAttached("IsRounded", typeof(bool), typeof(ButtonExtensions), new FrameworkPropertyMetadata(defaultValue: true, flags: FrameworkPropertyMetadataOptions.AffectsRender));


    /*********
    ** Public Methods
    *********/
    /// <summary>Gets whether the button is rounded.</summary>
    /// <param name="target">The element with the <see cref="IsRoundedProperty"/>.</param>
    /// <returns>Whether the button is rounded.</returns>
    public static bool GetIsRounded(UIElement target)
    {
        ArgumentNullException.ThrowIfNull(target);

        return (bool)target.GetValue(IsRoundedProperty);
    }

    /// <summary>Sets whether the button is rounded.</summary>
    /// <param name="target">The element with the <see cref="IsRoundedProperty"/>.</param>
    /// <param name="value">The new IsRounded value.</param>
    public static void SetIsRounded(UIElement target, bool value)
    {
        ArgumentNullException.ThrowIfNull(target);

        target.SetValue(IsRoundedProperty, value);
    }
}

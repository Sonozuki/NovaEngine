namespace NovaEditor.ValueConverters;

/// <summary>Represents a <see langword="bool"/> to <see cref="Visibility"/> value converter.</summary>
[ValueConversion(typeof(bool), typeof(Visibility))]
public class BoolVisibilityValueConverter : IValueConverter
{
    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => (bool)value ? Visibility.Visible : Visibility.Collapsed;

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        (Visibility)value == Visibility.Visible;
}

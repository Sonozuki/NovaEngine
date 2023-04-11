namespace NovaEditor.MultiValueConverters;

/// <summary>A multi-value converter used for checking equality between multiple data bindings.</summary>
internal sealed class MultiValueEqualityConverter : IMultiValueConverter
{
    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) =>
        values.All(value => value.Equals(values[0]));

    /// <inheritdoc/>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}

namespace NovaEditor.TwoWayBindingWrappers;

/// <summary>Represents a two-way binding for a <see langword="float"/>.</summary>
public class FloatBindingWrapper : BindingWrapperBase<float>
{
    /*********
    ** Fields
    *********/
    /// <summary>The wrapped value being bound to/from.</summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(float), typeof(FloatBindingWrapper), new(ValuePropertyChanged));


    /*********
    ** Properties
    *********/
    /// <summary>The wrapped <see langword="float"/> value being bound.</summary>
    public override float Value
    {
        get => (float)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="getValue">The function to call to retrieve the current value of the variable being wrapped.</param>
    /// <param name="setValue">The function to call to set the value of the variable being wrapped.</param>
    public FloatBindingWrapper(Func<float> getValue, Action<float> setValue)
        : base(getValue, setValue) { }
}

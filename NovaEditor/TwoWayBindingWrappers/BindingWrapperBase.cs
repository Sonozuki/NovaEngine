namespace NovaEditor.TwoWayBindingWrappers;

/// <summary>Represents the generic base class of all two-way binding wrappers.</summary>
/// <typeparam name="TBase">The type of the variable being wrapped.</typeparam>
/// <remarks>This relies on the value being wrapped to have a functioning <see cref="object.GetHashCode()"/> implementation.</remarks>
public abstract class BindingWrapperBase<TBase> : BindingWrapperBaseBase
    where TBase : unmanaged
{
    /*********
    ** Fields
    *********/
    /// <summary>The function to call to retrieve the current value of the variable being wrapped.</summary>
    private readonly Func<TBase> GetWrappedValue;

    /// <summary>The function to call to set the value of the variable being wrapped.</summary>
    private readonly Action<TBase> SetWrappedValue;

    /// <summary>The hash code of the variable being wrapped when <see cref="CheckIfWrappedValueHasChanged"/> was last called.</summary>
    private int PreviousHashCode;


    /*********
    ** Properties
    *********/
    /// <summary>The property to use with a <see cref="DependencyProperty"/> for getting/setting the current value of the variable being wrapped.</summary>
    public abstract TBase Value { get; set; }


    /*********
    ** Constructs
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="getValue">The function to call to retrieve the current value of the variable being wrapped.</param>
    /// <param name="setValue">The function to call to set the value of the variable being wrapped.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="getValue"/> or <paramref name="setValue"/> is <see langword="null"/>.</exception>
    protected BindingWrapperBase(Func<TBase> getValue, Action<TBase> setValue)
        : base()
    {
        GetWrappedValue = getValue ?? throw new ArgumentNullException(nameof(getValue));
        SetWrappedValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        PreviousHashCode = GetWrappedValue().GetHashCode();
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override void CheckIfWrappedValueHasChanged()
    {
        var currentHashCode = GetWrappedValue().GetHashCode();
        if (PreviousHashCode != currentHashCode)
        {
            PreviousHashCode = currentHashCode;
            Value = GetWrappedValue();
        }
    }

    /// <summary>Registers the wrapper for value update checking.</summary>
    /// <remarks>This should be called when the control that contains a wrapper is loaded (<see cref="FrameworkElement.Loaded"/>).</remarks>
    public void RegisterForUpdates() => TwoWayBindingWrapperManager.RegisterWrapper(this);

    /// <summary>Unregisters the wrapper for value update checking.</summary>
    /// <remarks>This should be called when the control that contains a wrapper is unloaded (<see cref="FrameworkElement.Unloaded"/>).</remarks>
    public void UnregisterForUpdates() => TwoWayBindingWrapperManager.UnregisterWrapper(this);


    /*********
    ** Protected Methods
    *********/
    /// <summary>Sets the value of the wrapped variable.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    /// <remarks>This should be added to the callback of the <see cref="DependencyProperty"/> created in derived classes.</remarks>
    protected static void ValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
        (sender as BindingWrapperBase<TBase>)?.SetWrappedValue((TBase)e.NewValue);
}

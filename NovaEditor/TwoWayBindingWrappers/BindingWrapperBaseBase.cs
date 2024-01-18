namespace NovaEditor.TwoWayBindingWrappers;

/// <summary>Represents the non-generic base class of all two-way binding wrappers.</summary>
public abstract class BindingWrapperBaseBase : DependencyObject
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Checks if the value being wrapped has changed since <see cref="CheckIfWrappedValueHasChanged"/> was last called.</summary>
    public abstract void CheckIfWrappedValueHasChanged();
}

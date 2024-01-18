namespace NovaEditor.Controls;

/// <summary>Represents a control that can display or edit a <see langword="float"/>.</summary>
public partial class FloatBox : UserControl
{
    /*********
    ** Properties
    *********/
    /// <summary>The wrapper to bind to the float.</summary>
    public FloatBindingWrapper ValueWrapper { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="getValue">The function to call to retrieve the <see langword="float"/> being bound.</param>
    /// <param name="setValue">The function to call to set the <see langword="float"/> being bound.</param>
    public FloatBox(Func<float> getValue, Action<float> setValue)
    {
        ValueWrapper = new(getValue, setValue);

        InitializeComponent();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Determines if specific text is valid for the float box.</summary>
    /// <param name="text">The text to check.</param>
    /// <returns><see langword="true"/>, if <paramref name="text"/> is a valid float value; otherwise, <see langword="false"/>.</returns>
    // TODO: you cannot input a decimal point at the end of the text box without it getting removed
    private bool IsTextValid(string text) => float.TryParse(text, out _);

    /// <summary>Invoked when text is input in the text box.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnPreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = !IsTextValid(TextBox.Text + e.Text);

    /// <summary>Invoked when data is pasted into the text box.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnPasting(object sender, DataObjectPastingEventArgs e)
    {
        if (!e.DataObject.GetDataPresent(typeof(string)))
        {
            e.CancelCommand();
            return;
        }

        var newInput = TextBox.Text.Replace(TextBox.SelectionStart, TextBox.SelectionLength, (string)e.DataObject.GetData(typeof(string)));
        if (!IsTextValid(newInput))
            e.CancelCommand();
    }

    /// <summary>Invoked when the control is loaded.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnLoaded(object sender, RoutedEventArgs e) => ValueWrapper.RegisterForUpdates();

    /// <summary>Invoked when the control is unloaded.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnUnloaded(object sender, RoutedEventArgs e) => ValueWrapper.UnregisterForUpdates();
}

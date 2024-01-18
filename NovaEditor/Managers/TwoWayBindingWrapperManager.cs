namespace NovaEditor.Managers;

/// <summary>Manages two-way binding wrappers.</summary>
internal static class TwoWayBindingWrapperManager
{
    /*********
    ** Fields
    *********/
    /// <summary>The two-way binding wrappers whose wrapped values should have there value checked for changes each frame.</summary>
    private static readonly List<BindingWrapperBaseBase> Wrappers = [];


    /*********
    ** Constructors
    *********/
    /// <summary>Initialises the class.</summary>
    static TwoWayBindingWrapperManager()
    {
        CompositionTarget.Rendering += OnRendering;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Adds a wrapper whose <see cref="BindingWrapperBaseBase.CheckIfWrappedValueHasChanged"/> should be checked each frame for updated values.</summary>
    /// <param name="wrapper">The wrapper to start checking for changes each frame.</param>
    public static void RegisterWrapper(BindingWrapperBaseBase wrapper) => Wrappers.Add(wrapper);

    /// <summary>Removes a wrapper from having its value checked for changes each frame.</summary>
    /// <param name="wrapper">The wrapper to stop checking for changes each frame.</param>
    public static void UnregisterWrapper(BindingWrapperBaseBase wrapper) => Wrappers.Remove(wrapper);


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked each UI tick before the objects in the composition tree are rendered.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private static void OnRendering(object sender, EventArgs e)
    {
        foreach (var wrapper in Wrappers)
            wrapper.CheckIfWrappedValueHasChanged();
    }
}

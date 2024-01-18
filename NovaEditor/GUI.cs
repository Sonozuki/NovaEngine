namespace NovaEditor;

/// <summary>Provides an interface for creating UI used in the <see cref="PropertiesPanel"/>.</summary>
public static class GUI
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked once all the GUI elements have been recorded and property panels should be repopulated.</summary>
    internal static event Action GUIUpdated;


    /*********
    ** Fields
    *********/
    /// <summary>The functions used for created the necessary controls.</summary>
    /// <remarks>Functions to create the controls are stored so if there are multiple properties panels the necessary controls can be created multiple times.</remarks>
    private static readonly List<Func<UserControl>> CreateControlFunctions = [];


    /*********
    ** Constructors
    *********/
    /// <summary>Initialises the class.</summary>
    static GUI()
    {
        ProjectManager.SelectedGameObjectChanged += OnSelectedGameObjectChanged;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Creates a float box, used for displaying and editing a <see langword="float"/>.</summary>
    /// <param name="label">The label of the float box.</param>
    /// <param name="getValue">The function to call to retrieve the <see langword="float"/> being bound.</param>
    /// <param name="setValue">The function to call to set the <see langword="float"/> being bound.</param>
    public static void CreateFloatBox(string label, Func<float> getValue, Action<float> setValue) => CreateControlFunctions.Add(() => new FloatBox(label, getValue, setValue));

    /// <summary>Creates a vector block, used for displaying and editing a <see cref="Vector3{T}"/> (float).</summary>
    /// <param name="getValue">The function to call to retrieve the <see cref="Vector3{T}"/> (float) being bound.</param>
    /// <param name="setValue">The function to call to set the <see cref="Vector3{T}"/> (float) being bound.</param>
    public static void CreateVectorBlock(Func<Vector3<float>> getValue, Action<Vector3<float>> setValue) => CreateControlFunctions.Add(() => new Vector3FloatBlock(getValue, setValue));


    /*********
    ** Internal Methods
    *********/
    /// <summary>Creates the requested controls for displaying.</summary>
    /// <returns>The created controls.</returns>
    internal static List<UserControl> CreateControls()
    {
        var controls = new List<UserControl>();
        foreach (var createControlFunction in CreateControlFunctions)
            controls.Add(createControlFunction());
        return controls;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when the selected game object is changed.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private static void OnSelectedGameObjectChanged(object sender, SelectedGameObjectChangedEventArgs e)
    {
        // TODO: may need to run this code whenever a change to any component occurs, incase custom ui builders hide members with conditionals etc
        CreateControlFunctions.Clear();

        var components = new List<ComponentBase> { e.NewGameObject.Transform };
        components.AddRange(e.NewGameObject.Components);

        foreach (var component in components)
        {
            // TODO: not the cleanest as we can't use generics to get a concrete type
            var componentEditor = ComponentUIBuilderManager.GetComponentEditor(component.GetType());
            componentEditor.GetType().GetMethod("BuildUI", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(componentEditor, [component]); // TODO: nameof
        }

        GUIUpdated?.Invoke();
    }
}

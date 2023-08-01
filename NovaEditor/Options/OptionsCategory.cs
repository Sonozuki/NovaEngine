namespace NovaEditor.Options;

/// <summary>Represents a node in an options tree.</summary>
public class OptionsCategory
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the category.</summary>
    public string Name { get; }

    /// <summary>The sub-categories in the category.</summary>
    public List<OptionsCategory> SubCategories { get; } = new();

    /// <summary>The types that contain the options for the category.</summary>
    /// <remarks>TODO: temp</remarks>
    public List<Type> TempTypes { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the category.</param>
    public OptionsCategory(string name)
    {
        Name = name;
    }
}

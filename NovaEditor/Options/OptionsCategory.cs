namespace NovaEditor.Options;

/// <summary>Represents a node in an options tree.</summary>
public class OptionsCategory
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the category.</summary>
    public string Name { get; }

    /// <summary>The groups in the category.</summary>
    public List<OptionsGroup> Groups { get; } = new();

    /// <summary>The sub-categories in the category.</summary>
    public List<OptionsCategory> SubCategories { get; } = new();


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

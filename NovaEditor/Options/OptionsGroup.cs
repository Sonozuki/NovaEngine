namespace NovaEditor.Options;

/// <summary>Represents a group of options.</summary>
public class OptionsGroup
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the group.</summary>
    public string Name { get; }

    /// <summary>The types that contain the options for the group.</summary>
    /// <remarks>TODO: temp</remarks>
    public List<Type> TempTypes { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the group.</param>
    public OptionsGroup(string name)
    {
        Name = name;
    }
}

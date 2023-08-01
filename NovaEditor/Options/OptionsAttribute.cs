namespace NovaEditor.Options;

/// <summary>The attribute used for indicating options.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class OptionsAttribute : Attribute
{
    /*********
    ** Properties
    *********/
    /// <summary>The names of the categories the options will be in.</summary>
    /// <remarks>If multiple categories are specified, then subsequent categories will be a child of the previous category.</remarks>
    public ImmutableArray<string> OptionsCategories { get; }

    /// <summary>The name of the group the options will be in.</summary>
    public string OptionsGroup { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="optionsCategories">The names of the categories the options will be in.</param>
    /// <param name="optionsGroup">The name of the group the options will be in.</param>
    public OptionsAttribute(string[] optionsCategories, string optionsGroup)
    {
        ArgumentNullException.ThrowIfNull(optionsCategories);
        ArgumentException.ThrowIfNullOrEmpty(optionsGroup);

        OptionsCategories = optionsCategories.ToImmutableArray();
        if (OptionsCategories.Length == 0)
            throw new ArgumentException("Must contain at least one category.", nameof(optionsCategories));

        OptionsGroup = optionsGroup;
    }
}

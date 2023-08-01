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


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="optionsCategories">The names of the categories the options will be in.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="optionsCategories"/> is <see langword="null"/>.</exception>
    public OptionsAttribute(string[] optionsCategories)
    {
        ArgumentNullException.ThrowIfNull(optionsCategories);

        OptionsCategories = optionsCategories.ToImmutableArray();
        if (OptionsCategories.Length == 0)
            throw new ArgumentException("Must contain at least one category.", nameof(optionsCategories));
    }
}

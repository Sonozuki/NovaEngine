namespace NovaEngine;

/// <summary>Represents a console command.</summary>
internal sealed class Command
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the command.</summary>
    public string Name { get; }

    /// <summary>The documentation of the command.</summary>
    public string Documentation { get; }

    /// <summary>The callback of the command.</summary>
    public Action<string[]> Callback { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the command.</param>
    /// <param name="documentation">The documentation of the command.</param>
    /// <param name="callback">The callback of the command.</param>
    public Command(string name, string documentation, Action<string[]> callback)
    {
        Name = name;
        Documentation = documentation;
        Callback = callback;
    }
}

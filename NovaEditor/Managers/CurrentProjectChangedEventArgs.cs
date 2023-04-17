namespace NovaEditor.Managers;

/// <summary>The event data for <see cref="ProjectManager.CurrentProjectChanged"/>.</summary>
internal sealed class CurrentProjectChangedEventArgs : EventArgs
{
    /*********
    ** Properties
    *********/
    /// <summary>The old project.</summary>
    public string OldProject { get; }

    /// <summary>The new project.</summary>
    public string NewProject { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="oldProject">The old project.</param>
    /// <param name="newProject">The new project.</param>
    public CurrentProjectChangedEventArgs(string oldProject, string newProject)
    {
        OldProject = oldProject;
        NewProject = newProject;
    }
}

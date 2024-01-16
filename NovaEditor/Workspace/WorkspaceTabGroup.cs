namespace NovaEditor.Workspace;

/// <summary>Represents the serialisable version of <see cref="PanelTabGroup"/>.</summary>
internal sealed class WorkspaceTabGroup : WorkspacePanel
{
    /*********
    ** Properties
    *********/
    /// <summary>The panels in the tab group.</summary>
    public List<WorkspacePanel> Panels { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="settings">The persistent settings of the panel.</param>
    public WorkspaceTabGroup(Dictionary<string, string> settings)
        : base(typeof(PanelTabGroup).FullName, settings) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="panels">The panels in the tab group.</param>
    /// <param name="settings">The persistent settings of the panel.</param>
    [JsonConstructor]
    public WorkspaceTabGroup(List<WorkspacePanel> panels, Dictionary<string, string> settings)
        : base(typeof(PanelTabGroup).FullName, settings)
    {
        Panels = panels.ToList();
    }
}

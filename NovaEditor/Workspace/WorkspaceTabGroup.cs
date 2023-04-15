namespace NovaEditor.Workspace;

/// <summary>Represents the serialisable version of <see cref="PanelTabGroup"/>.</summary>
internal sealed class WorkspaceTabGroup : WorkspacePanel
{
    /*********
    ** Properties
    *********/
    /// <summary>The panels in the tab group.</summary>
    public List<WorkspacePanel> Panels { get; set; }

    /// <summary>The index of the active panel in the tab group.</summary>
    public int ActivePanelIndex { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public WorkspaceTabGroup()
        : base(typeof(PanelTabGroup).FullName) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="panels">The panels in the tab group.</param>
    /// <param name="activePanelIndex">The index of the active panel in the tab group.</param>
    public WorkspaceTabGroup(IEnumerable<WorkspacePanel> panels, int activePanelIndex)
        : base(typeof(PanelTabGroup).FullName)
    {
        Panels = panels.ToList();
        ActivePanelIndex = activePanelIndex;
    }
}

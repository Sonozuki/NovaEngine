namespace NovaEditor.Workspace;

/// <summary>Represents the serialisable version of <see cref="PanelTabGroup"/>.</summary>
internal sealed class WorkspaceTabGroupGroup : WorkspacePanel
{
    /*********
    ** Properties
    *********/
    /// <summary>The orientation of the tab group group.</summary>
    public Orientation Orientation { get; set; }

    /// <summary>The tab group panels in the tab group group.</summary>
    public List<WorkspacePanel> Panels { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public WorkspaceTabGroupGroup()
        : base(typeof(PanelTabGroupGroup).FullName, new()) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="orientation">The orientation of the tab group group.</param>
    /// <param name="panels">The tab group panels in the tab group group.</param>
    public WorkspaceTabGroupGroup(Orientation orientation, IEnumerable<WorkspacePanel> panels)
        : base(typeof(PanelTabGroupGroup).FullName, new())
    {
        Orientation = orientation;
        Panels = panels.ToList();
    }
}

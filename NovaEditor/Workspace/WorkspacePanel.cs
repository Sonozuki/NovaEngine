namespace NovaEditor.Workspace;

/// <summary>Represents the serialisable version of <see cref="EditorPanelBase"/>.</summary>
[JsonDerivedType(typeof(WorkspaceTabGroup), typeDiscriminator: "tabGroup")]
[JsonDerivedType(typeof(WorkspaceTabGroupGroup), typeDiscriminator: "tabGroupGroup")]
internal class WorkspacePanel
{
    /*********
    ** Properties
    *********/
    /// <summary>The fully qualified type name of the panel.</summary>
    public string PanelTypeName { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="panelTypeName">The fully qualified type name of the panel.</param>
    public WorkspacePanel(string panelTypeName)
    {
        PanelTypeName = panelTypeName;
    }
}

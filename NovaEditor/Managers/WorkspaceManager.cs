namespace NovaEditor.Managers;

/// <summary>Manages the editor workspace.</summary>
internal static class WorkspaceManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Saves the current workspace.</summary>
    public static void SaveWorkspace()
    {
        var rootTabGroupGroup = (Application.Current as App).MainWindow.RootPanelTabGroupGroup;
        var rootWorkspacePanel = CreatePanel(rootTabGroupGroup);
        File.WriteAllText(Constants.WorkspaceFile, JsonSerializer.Serialize(rootWorkspacePanel));
    }

    /// <summary>Loads the last saved workspace.</summary>
    public static void LoadWorkspace()
    {
        var workspaceRoot = GetWorkspace();
        var rootEditorPanel = CreatePanel(workspaceRoot);
        (Application.Current as App).MainWindow.RootPanelTabGroupGroup.Content = rootEditorPanel;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Retrieves the saved workspace root node.</summary>
    /// <returns>The saved workspace root node.</returns>
    private static WorkspacePanel GetWorkspace()
    {
        if (!File.Exists(Constants.WorkspaceFile))
            return new(null); // TODO: default workspace

        try
        {
            return JsonSerializer.Deserialize<WorkspacePanel>(File.ReadAllText(Constants.WorkspaceFile));
        }
        catch
        {
            File.Delete(Constants.WorkspaceFile);
            return new(null); // TODO: default workspace
        }
    }

    /// <summary>Creates a workspace panel from an editor panel.</summary>
    /// <param name="editorPanel">The editor panel to create the workspace panel from.</param>
    /// <returns>The workspace panel that represents the editor panel.</returns>
    private static WorkspacePanel CreatePanel(EditorPanelBase editorPanel)
    {
        if (editorPanel is PanelTabGroup panelTabGroup)
        {
            var viewModel = (PanelTabGroupViewModel)panelTabGroup.DataContext;
            return new WorkspaceTabGroup(viewModel.Panels.Select(CreatePanel), panelTabGroup.PanelTabControl.SelectedIndex);
        }
        else if (editorPanel is PanelTabGroupGroup panelTabGroupGroup)
        {
            var viewModel = (PanelTabGroupGroupViewModel)panelTabGroupGroup.DataContext;
            return new WorkspaceTabGroupGroup(viewModel.Orientation, viewModel.Panels.Select(CreatePanel));
        }
        else
            return new WorkspacePanel(editorPanel.GetType().FullName);
    }

    /// <summary>Creates an editor panel from a workspace panel.</summary>
    /// <param name="workspacePanel">The workspace panel to create the editor panel from.</param>
    /// <returns>The editor panel that represents the workspace panel.</returns>
    private static EditorPanelBase CreatePanel(WorkspacePanel workspacePanel)
    {
        if (workspacePanel is WorkspaceTabGroup workspaceTabGroup)
        {
            var panelTabGroup = new PanelTabGroup();
            var viewModel = (PanelTabGroupViewModel)panelTabGroup.DataContext;

            foreach (var editorPanel in workspaceTabGroup.Panels.Select(CreatePanel))
                viewModel.Panels.Add(editorPanel);
            panelTabGroup.PanelTabControl.SelectedIndex = workspaceTabGroup.SelectedPanelIndex;

            return panelTabGroup;
        }
        else if (workspacePanel is WorkspaceTabGroupGroup workspaceTabGroupGroup)
        {
            var panelTabGroupGroup = new PanelTabGroupGroup();
            var viewModel = (PanelTabGroupGroupViewModel)panelTabGroupGroup.DataContext;

            viewModel.Orientation = workspaceTabGroupGroup.Orientation;
            foreach (var editorPanel in workspaceTabGroupGroup.Panels.Select(CreatePanel))
                viewModel.Panels.Add(editorPanel);

            return panelTabGroupGroup;
        }
        else
            return (EditorPanelBase)Activator.CreateInstance(Type.GetType(workspacePanel.PanelTypeName))
                ?? throw new InvalidOperationException("Failed to create panel in workspace");
    }
}

﻿namespace NovaEditor.Managers;

/// <summary>Manages the editor workspace.</summary>
internal static class WorkspaceManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Saves the current workspace.</summary>
    public static void SaveWorkspace()
    {
        if (string.IsNullOrEmpty(ProjectManager.CurrentProject))
            return;

        var rootTabGroupGroup = (EditorPanelBase)(Application.Current as App).MainWindow.RootPanelTabGroupGroup.Content;
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

    /// <summary>Loads the project selection workspace.</summary>
    internal static void LoadProjectSelectionWorkspace() => (Application.Current as App).MainWindow.RootPanelTabGroupGroup.Content = new ProjectSelectionPanel();

    /// <summary>Loads the project creation workspace.</summary>
    internal static void LoadProjectCreationWorkspace() => (Application.Current as App).MainWindow.RootPanelTabGroupGroup.Content = new ProjectCreationPanel();


    /*********
    ** Private Methods
    *********/
    /// <summary>Retrieves the saved workspace root node.</summary>
    /// <returns>The saved workspace root node.</returns>
    private static WorkspacePanel GetWorkspace()
    {
        if (!File.Exists(Constants.WorkspaceFile))
            return new(null, new()); // TODO: default workspace

        try
        {
            return JsonSerializer.Deserialize<WorkspacePanel>(File.ReadAllText(Constants.WorkspaceFile));
        }
        catch
        {
            File.Delete(Constants.WorkspaceFile);
            return new(null, new()); // TODO: default workspace
        }
    }

    /// <summary>Creates a workspace panel from an editor panel.</summary>
    /// <param name="editorPanel">The editor panel to create the workspace panel from.</param>
    /// <returns>The workspace panel that represents the editor panel.</returns>
    private static WorkspacePanel CreatePanel(EditorPanelBase editorPanel)
    {
        if (editorPanel is PanelTabGroup panelTabGroup)
            return new WorkspaceTabGroup(panelTabGroup.ViewModel.Panels.Select(CreatePanel).ToList(), panelTabGroup.Settings?.AsDictionary());
        else if (editorPanel is PanelTabGroupGroup panelTabGroupGroup)
            return new WorkspaceTabGroupGroup(panelTabGroupGroup.ViewModel.Orientation, panelTabGroupGroup.ViewModel.Panels.Select(CreatePanel));
        else
            return new WorkspacePanel(editorPanel.GetType().FullName, editorPanel.Settings?.AsDictionary());
    }

    /// <summary>Creates an editor panel from a workspace panel.</summary>
    /// <param name="workspacePanel">The workspace panel to create the editor panel from.</param>
    /// <returns>The editor panel that represents the workspace panel.</returns>
    private static EditorPanelBase CreatePanel(WorkspacePanel workspacePanel)
    {
        if (workspacePanel is WorkspaceTabGroup workspaceTabGroup)
        {
            var settings = new NotificationDictionary<string, string>(workspaceTabGroup.Settings);
            settings.NotificationDictionaryChanged += (_, _) => SaveWorkspace();

            var panelTabGroup = new PanelTabGroup(settings);

            foreach (var editorPanel in workspaceTabGroup.Panels.Select(CreatePanel))
                panelTabGroup.ViewModel.Panels.Add(editorPanel);

            return panelTabGroup;
        }
        else if (workspacePanel is WorkspaceTabGroupGroup workspaceTabGroupGroup)
        {
            var panelTabGroupGroup = new PanelTabGroupGroup();

            panelTabGroupGroup.ViewModel.Orientation = workspaceTabGroupGroup.Orientation;
            foreach (var editorPanel in workspaceTabGroupGroup.Panels.Select(CreatePanel))
                panelTabGroupGroup.ViewModel.Panels.Add(editorPanel);

            return panelTabGroupGroup;
        }
        else
        {
            var settings = new NotificationDictionary<string, string>(workspacePanel.Settings);
            settings.NotificationDictionaryChanged += (_, _) => SaveWorkspace();

            return (EditorPanelBase)Activator.CreateInstance(Type.GetType(workspacePanel.PanelTypeName), new[] { settings })
                ?? throw new InvalidOperationException("Failed to create panel in workspace");
        }
    }
}

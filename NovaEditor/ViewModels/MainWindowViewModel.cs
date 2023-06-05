namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="MainWindow"/>.</summary>
public sealed class MainWindowViewModel
{
    /*********
    ** Properties
    *********/
    /// <summary>The command used to create a new project.</summary>
    public ICommand CreateNewProjectCommand { get; }

    /// <summary>The command used to open a project.</summary>
    public ICommand OpenProjectCommand { get; }

    /// <summary>The command used to save the project.</summary>
    public ICommand SaveProjectCommand { get; }

    /// <summary>The command used to undo the last action.</summary>
    public ICommand UndoCommand { get; }

    /// <summary>The command used to redo the last undo.</summary>
    public ICommand RedoCommand { get; }

    /// <summary>The command used to cut something to the clipboard.</summary>
    public ICommand CutCommand { get; }

    /// <summary>The command used to copy something to the clipboard.</summary>
    public ICommand CopyCommand { get; }

    /// <summary>The command used to paste something from the clipboard.</summary>
    public ICommand PasteCommand { get; }

    /// <summary>The command used to create a new panel.</summary>
    public ICommand CreatePanelCommand { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public MainWindowViewModel()
    {
        CreateNewProjectCommand = new RelayCommand(CreateNewProject);
        OpenProjectCommand = new RelayCommand(OpenProject);
        SaveProjectCommand = new RelayCommand(SaveProject);

        UndoCommand = new RelayCommand(Undo);
        RedoCommand = new RelayCommand(Redo);
        CutCommand = new RelayCommand(Cut);
        CopyCommand = new RelayCommand(Copy);
        PasteCommand = new RelayCommand(Paste);

        CreatePanelCommand = new RelayCommand<string>(CreatePanel);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Opens a dialog to create a new project.</summary>
    private void CreateNewProject() => throw new NotImplementedException();

    /// <summary>Opens a dialog to open a project.</summary>
    private void OpenProject() => throw new NotImplementedException();

    /// <summary>Saves the project.</summary>
    private void SaveProject() => throw new NotImplementedException();

    /// <summary>Undos the last action.</summary>
    private void Undo() => throw new NotImplementedException();

    /// <summary>Redos the last undo.</summary>
    private void Redo() => throw new NotImplementedException();

    /// <summary>Cuts something to the clipboard.</summary>
    private void Cut() => throw new NotImplementedException();

    /// <summary>Copies something to the clipboard.</summary>
    private void Copy() => throw new NotImplementedException();

    /// <summary>Pastes something from the clipboard.</summary>
    private void Paste() => throw new NotImplementedException();

    /// <summary>Creates a panel.</summary>
    /// <param name="panelName">The name of the panel to create.</param>
    private void CreatePanel(string panelName) => throw new NotImplementedException();
}

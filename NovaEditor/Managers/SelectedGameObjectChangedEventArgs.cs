namespace NovaEditor.Managers;

/// <summary>The event data for <see cref="ProjectManager.SelectedGameObjectChanged"/>.</summary>
internal sealed class SelectedGameObjectChangedEventArgs
{
    /*********
    ** Properties
    *********/
    /// <summary>The old game object.</summary>
    public GameObject OldGameObject { get; }

    /// <summary>The new game object.</summary>
    public GameObject NewGameObject { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="oldGameObject">The old game object.</param>
    /// <param name="newGameObject">The new game object.</param>
    public SelectedGameObjectChangedEventArgs(GameObject oldGameObject, GameObject newGameObject)
    {
        OldGameObject = oldGameObject;
        NewGameObject = newGameObject;
    }
}

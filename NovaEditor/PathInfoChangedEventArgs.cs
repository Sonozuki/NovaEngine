namespace NovaEditor;

/// <summary>Represents the <see cref="PathInfo"/> changed event arguments.</summary>
public class PathInfoChangedEventArgs : EventArgs
{
    /*********
    ** Properties
    *********/
    /// <summary>The old <see cref="PathInfo"/> value.</summary>
    public PathInfo OldValue { get; }

    /// <summary>The new <see cref="PathInfo"/> value.</summary>
    public PathInfo NewValue { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="oldValue">The old <see cref="PathInfo"/> value.</param>
    /// <param name="newValue">The new <see cref="PathInfo"/> value.</param>
    public PathInfoChangedEventArgs(PathInfo oldValue, PathInfo newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}

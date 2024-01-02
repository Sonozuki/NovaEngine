namespace NovaEditor.Collections;

/// <summary>The event data for <see cref="NotificationDictionary{TKey, TValue}.NotificationDictionaryChanged"/>.</summary>
public class NotificationDictionaryChangedEventArgs : EventArgs
{
    /*********
    ** Properties
    *********/
    /// <summary>The type of change that occurred to an element.</summary>
    public NotificationDictionaryChangeTypes ChangeType { get; set; }

    /// <summary>The key that was changed.</summary>
    public object Key { get; set; }

    /// <summary>The old value of the change (only used for <see cref="NotificationDictionaryChangeTypes.Change"/> and <see cref="NotificationDictionaryChangeTypes.Remove"/>.</summary>
    public object OldValue { get; set; }

    /// <summary>The new value of the change (only used for <see cref="NotificationDictionaryChangeTypes.Add"/> and <see cref="NotificationDictionaryChangeTypes.Change"/>.</summary>
    public object NewValue { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="changeType">The type of change that occurred to an element.</param>
    /// <param name="key">The key that was changed.</param>
    /// <param name="oldValue">The old value of the change (only used for <see cref="NotificationDictionaryChangeTypes.Change"/> and <see cref="NotificationDictionaryChangeTypes.Remove"/>.</param>
    /// <param name="newValue">The new value of the change (only used for <see cref="NotificationDictionaryChangeTypes.Add"/> and <see cref="NotificationDictionaryChangeTypes.Change"/>.</param>
    public NotificationDictionaryChangedEventArgs(NotificationDictionaryChangeTypes changeType, object key, object oldValue, object newValue)
    {
        ChangeType = changeType;
        Key = key;
        OldValue = oldValue;
        NewValue = newValue;
    }
}

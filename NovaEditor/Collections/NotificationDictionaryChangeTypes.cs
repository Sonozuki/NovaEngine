namespace NovaEditor.Collections;

/// <summary>The types of change events that can occur in a <see cref="NotificationDictionary{TKey, TValue}"/>.</summary>
public enum NotificationDictionaryChangeTypes
{
    /// <summary>An element was added.</summary>
    Add,

    /// <summary>An element was changed.</summary>
    Change,

    /// <summary>An element was removed.</summary>
    Remove
}

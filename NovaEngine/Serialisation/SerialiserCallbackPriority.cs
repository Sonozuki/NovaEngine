namespace NovaEngine.Serialisation;

/// <summary>The priority of a serialiser callback.</summary>
/// <remarks>A number can be passed in place of the enumerator to have finer control over callback priority.</remarks>
public enum SerialiserCallbackPriority
{
    /// <summary>The callback is a high priority callback, therefore getting called earlier than lower priority callbacks.</summary>
    High = 10000,

    /// <summary>The callback is a medium priority callback, therefore getting called later than high priority callbacks but earlier than lower priority callbacks.</summary>
    Normal = 20000,

    /// <summary>The callback is a low priority callback, therefore getting called later than high and medium priority callbacks.</summary>
    Low = 30000
}

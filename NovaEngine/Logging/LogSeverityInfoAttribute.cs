namespace NovaEngine.Logging;

/// <summary>An attribute to specify information of a <see cref="LogSeverity"/></summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
internal sealed class LogSeverityInfoAttribute : Attribute
{
    /*********
    ** Properties
    *********/
    /// <summary>The label of the log.</summary>
    public string Label { get; }

    /// <summary>The colour of the label.</summary>
    public ConsoleColor LabelColour { get; }

    /// <summary>The foreground colour of the log.</summary>
    public ConsoleColor ForegroundColour { get; }

    /// <summary>The background colour of the log.</summary>
    public ConsoleColor? BackgroundColour { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="label">The label of the log.</param>
    /// <param name="labelColour">The colour of the label.</param>
    /// <param name="foregroundColour">The foreground colour of the log.</param>
    public LogSeverityInfoAttribute(string label, ConsoleColor labelColour, ConsoleColor foregroundColour)
    {
        Label = label;
        LabelColour = labelColour;
        ForegroundColour = foregroundColour;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="label">The label of the log.</param>
    /// <param name="labelColour">The colour of the label.</param>
    /// <param name="foregroundColour">The foreground colour of the log.</param>
    /// <param name="backgroundColour">The background colour of the log.</param>
    public LogSeverityInfoAttribute(string label, ConsoleColor labelColour, ConsoleColor foregroundColour, ConsoleColor backgroundColour)
    {
        Label = label;
        LabelColour = labelColour;
        ForegroundColour = foregroundColour;
        BackgroundColour = backgroundColour;
    }
}

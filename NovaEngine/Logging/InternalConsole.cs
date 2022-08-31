namespace NovaEngine.Logging;

/// <summary>Represents the standard input and ouput streams for the engine.</summary>
internal static class InternalConsole
{
    /*********
    ** Fields
    *********/
    //// <summary>The buffered lines to render in the console.</summary>
    private static readonly CircularBuffer<InternalConsoleString> BufferedLines;


    /*********
    ** Accessors
    *********/
    /// <summary>The default colour of any written text when it's 'colour' parameter is <see langword="null"/>.</summary>
    public static Colour DefaultColour { get; set; } = Colour.White; // TODO: get this value from ConsoleSettings

    /// <summary>Whether the console is currently visible.</summary>
    public static bool IsVisible { get; private set; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Initialises the class.</summary>
    static InternalConsole()
    {
        BufferedLines = new(50); // this is a temp buffer that messages generated from getting the ConsoleSettings can write to

        var bufferSize = ConsoleSettings.Instance.MaxNumberOfLines;
        BufferedLines = new(bufferSize, BufferedLines.TakeLast(bufferSize)); // take last to prevent exception being thrown if bufferSize is less than the current size

        Input.AddKeyHandler(Key.OEM_8, PressType.Press, () => IsVisible = !IsVisible);
    }

    /// <summary>Writes a string to the engine console.</summary>
    /// <param name="message">The string to write to the console.</param>
    /// <param name="colour">The colour of the string. If <see langword="null"/>, then <see cref="DefaultColour"/> will be used.</param>
    public static void Write(string message, Colour? colour = null)
    {
        var messageColour = colour ?? DefaultColour;
        var internalString = new InternalConsoleString(message.Select(character => new InternalConsoleCharacter(character, messageColour)));

        // append the message to the previous line
        // if the previous message ended in a new line then the popped line will be a blank string due to how the below split works
        if (!BufferedLines.IsEmpty)
            internalString = BufferedLines.PopBack()! + internalString;

        // split the message on each new line and add them seperately
        foreach (var line in internalString.Split('\n'))
            BufferedLines.PushBack(line);
    }
}

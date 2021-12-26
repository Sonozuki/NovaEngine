namespace NovaEngine
{
    /// <summary>Represents a console command.</summary>
    internal class Command
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The name of the command.</summary>
        public string Name { get; }

        /// <summary>The documentation of the command.</summary>
        public string Documentation { get; }

        /// <summary>The callback of the command.</summary>
        public Action<string[]> Callback { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="documentation">The documentation of the command.</param>
        /// <param name="callback">The callback of the command.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is <see langword="null"/> or white space.</exception>
        /// <exception cref="ArgumentNullException">Thrown is <paramref name="callback"/> is <see langword="null"/>.</exception>
        public Command(string name, string documentation, Action<string[]> callback)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cannot be null or empty.", nameof(name));

            Name = name;
            Documentation = documentation;
            Callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }
    }
}

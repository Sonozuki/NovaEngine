﻿namespace NovaEngine.Logging;

/// <summary>Represents the creator of a <see cref="Log"/>.</summary>
internal sealed class LogCreator
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the creator.</summary>
    public string Name { get; }

    /// <summary>Whether the creator is internal (relative to the engine).</summary>
    public bool IsInternal { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the creator.</param>
    /// <param name="isInternal">Whether the creator is internal (relative to the engine).</param>
    public LogCreator(string name, bool isInternal)
    {
        Name = name;
        IsInternal = isInternal;
    }
}

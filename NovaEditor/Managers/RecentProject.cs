namespace NovaEditor.Managers;

/// <summary>Metadata for a recent project.</summary>
public class RecentProject
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the project file, including extension.</summary>
    public string Name { get; set; }

    /// <summary>The full name of the project file, including extension.</summary>
    [JsonIgnore]
    public string FullName => Path.Combine(Directory, Name);

    /// <summary>The directory the project file is located in.</summary>
    public string Directory { get; set; }

    /// <summary>The last time the project was opened, in UTC.</summary>
    public DateTime LastOpenedUtc { get; set; }

    /// <summary>The last time the project was opened, in local time.</summary>
    [JsonIgnore]
    public string LastOpened
    {
        get
        {
            var localDateTime = LastOpenedUtc.ToLocalTime();
            return $"{localDateTime.ToShortDateString()} {localDateTime.ToShortTimeString()}";
        }
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="name">The name of the project file, including extension.</param>
    /// <param name="directory">The directory the project file is located in.</param>
    /// <param name="lastOpenedUtc">The last time the project was opened, in UTC.</param>
    public RecentProject(string name, string directory, DateTime lastOpenedUtc)
    {
        Name = name;
        Directory = directory;
        LastOpenedUtc = lastOpenedUtc;
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override bool Equals(object obj) =>
        obj is RecentProject project
            && Name == project.Name
            && Directory == project.Directory;

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Name, Directory);
}

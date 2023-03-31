#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable

using UIKit;

namespace NovaEditor;

/// <summary>The application entry point.</summary>
public class Program
{
    /*********
    ** Internal Methods
    *********/
    /// <summary>The application entry point.</summary>
    /// <param name="args">The command-line arguments.</param>
    internal static void Main(string[] args)
    {
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}

namespace NovaEditor;

/// <summary>Represents the panel manager.</summary>
internal static class PanelManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves all panels in all loaded assemblies.</summary>
    /// <returns>All panels in all loaded assemblies.</returns>
    public static IEnumerable<PanelBase> GetAllPanels() =>
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsAbstract && type.IsAssignableTo(typeof(PanelBase)))
            .Select(type => (PanelBase)Activator.CreateInstance(type));
}

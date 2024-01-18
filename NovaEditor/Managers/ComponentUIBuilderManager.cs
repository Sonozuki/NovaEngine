namespace NovaEditor.Managers;

/// <summary>Manages the component UI builders used for creating UI for <see cref="ComponentBase"/>.</summary>
internal static class ComponentUIBuilderManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Creates a <see cref="ComponentUIBuilder{TComponent}"/> using a specified <see cref="Type"/>.</summary>
    /// <param name="componentType">The component type to create the component editor with.</param>
    /// <returns>A <see cref="ComponentUIBuilder{TComponent}"/> with a type of <paramref name="componentType"/>.</returns>
    public static object GetComponentEditor(Type componentType)
    {
        // TODO: caching
        // TODO: check for custom component UI builders
        var componentEditorType = typeof(ComponentUIBuilder<>).MakeGenericType(componentType);
        return Activator.CreateInstance(componentEditorType);
    }
}

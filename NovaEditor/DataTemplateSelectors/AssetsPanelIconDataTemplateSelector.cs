namespace NovaEditor.DataTemplateSelectors;

/// <summary>The data template selector for the assets panel folder/file icons.</summary>
public class AssetsPanelIconDataTemplateSelector : DataTemplateSelector
{
    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        ArgumentNullException.ThrowIfNull(item);

        var pathInfo = (PathInfo)item;
        return (DataTemplate)Application.Current.FindResource(pathInfo.IsDirectory ? "AssetFolderIcon" : "AssetFileIcon");
    }
}

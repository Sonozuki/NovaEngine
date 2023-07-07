namespace NovaEditor.DataTemplateSelectors;

/// <summary>The data template selector for the assets panel folder/file icons.</summary>
public class AssetsPanelIconDataTemplateSelector : DataTemplateSelector
{
    /*********
    ** Fields
    *********/
    /// <summary>The template for the asset folder icon.</summary>
    private readonly DataTemplate AssetFolderIconTemplate = (DataTemplate)Application.Current.FindResource("AssetFolderIcon");

    /// <summary>The template for the asset file icon.</summary>
    private readonly DataTemplate AssetFileIconTemplate = (DataTemplate)Application.Current.FindResource("AssetFileIcon");


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        ArgumentNullException.ThrowIfNull(item);
        return ((PathInfo)item).IsDirectory ? AssetFolderIconTemplate : AssetFileIconTemplate;
    }
}

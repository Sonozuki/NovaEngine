namespace NovaEditor.Controls;

/// <summary>The template selector for a tab in a <see cref="PanelTabGroup"/>.</summary>
internal sealed class PanelTabGroupTabDataTemplateSelector : DataTemplateSelector
{
    /*********
    ** Properties
    *********/
    /// <summary>The template for an active tab.</summary>
    public DataTemplate ActiveTemplate { get; set; }

    /// <summary>The template for an inactive tab.</summary>
    public DataTemplate InactiveTemplate { get; set; }


    /*********
    ** Protected Methods
    *********/
    /// <inheritdoc/>
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item == ((PanelTabGroupViewModel)container.BindingContext).ActivePanel)
            return ActiveTemplate;
        else
            return InactiveTemplate;
    }
}

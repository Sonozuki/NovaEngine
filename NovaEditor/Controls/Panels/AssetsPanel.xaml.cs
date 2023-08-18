namespace NovaEditor.Controls.Panels;

/// <summary>Represents the panel used for managing assets.</summary>
public partial class AssetsPanel : EditorPanelBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The scale of the file and folder icons in the panel.</summary>
    public static readonly DependencyProperty IconScaleProperty = DependencyProperty.Register(nameof(IconScale), typeof(double), typeof(AssetsPanel));
    
    /// <summary>The height of the file and folder icons in the panel.</summary>
    public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(AssetsPanel));


    /*********
    ** Properties
    *********/
    /// <summary>The view model of the panel.</summary>
    public AssetsViewModel ViewModel { get; } = new();

    /// <summary>The scale of the file and folder icons in the panel.</summary>
    public double IconScale
    {
        get => (double)GetValue(IconScaleProperty);
        set => SetValue(IconScaleProperty, value);
    }

    /// <summary>The height of the file and folder icons in the panel.</summary>
    public double IconHeight
    {
        get => (double)GetValue(IconHeightProperty);
        set => SetValue(IconHeightProperty, value);
    }


    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
    public AssetsPanel()
    {
        DataContext = ViewModel;
        InitializeComponent();

        ViewModel.NumberOfColumnsChanged += UpdateIconScale;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Updates the icon scale based on the current number of columns.</summary>
    private void UpdateIconScale()
    {
        // TODO: these shouldn't be hardcoded
        const int iconWidth = 100;
        const int iconHeight = 70;

        var columnWidth = RootItemsControl.ActualWidth / ViewModel.NumberOfColumns - 4; // -4 is 2x the margin applied to each cell in the uniform grid

        IconScale = columnWidth / iconWidth;
        IconHeight = iconHeight * IconScale;
    }
}

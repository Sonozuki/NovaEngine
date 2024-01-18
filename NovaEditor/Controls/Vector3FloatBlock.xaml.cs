namespace NovaEditor.Controls;

/// <summary>Represents a control that can display or edit a <see cref="Vector3{T}"/> (float).</summary>
public partial class Vector3FloatBlock : UserControl
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="getValue">The function to call to retrieve the <see cref="Vector3{T}"/> (float) being bound.</param>
    /// <param name="setValue">The function to call to set the <see cref="Vector3{T}"/> (float) being bound.</param>
    public Vector3FloatBlock(Func<Vector3<float>> getValue, Action<Vector3<float>> setValue)
    {
        InitializeComponent();

        RootStackPanel.Children.Add(new FloatBox("X", () => getValue().X, value => {
            var vector = getValue();
            vector.X = value;
            setValue(vector);
        }));

        RootStackPanel.Children.Add(new FloatBox("Y", () => getValue().Y, value => {
            var vector = getValue();
            vector.Y = value;
            setValue(vector);
        }));

        RootStackPanel.Children.Add(new FloatBox("Z", () => getValue().Z, value => {
            var vector = getValue();
            vector.Z = value;
            setValue(vector);
        }));
    }
}

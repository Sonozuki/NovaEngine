namespace NovaEngine.Components;

/// <summary>Represents a component used for rendering text.</summary>
public class TextRenderer : MeshRenderingComponentBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The text to render.</summary>
    private string _Text = "";

    /// <summary>The font to use to render the text.</summary>
    private readonly SerialiserLoadable<Font> _Font;


    /*********
    ** Accessors
    *********/
    /// <summary>The text to render.</summary>
    public string Text
    {
        get => _Text;
        set
        {
            _Text = value;
            GenerateMesh();
        }
    }

    /// <summary>The font to use to render the text.</summary>
    public Font Font
    {
        get => _Font.Value;
        set
        {
            _Font.Value = value;
            GenerateMesh();
        }
    }

    /// <summary>The max height of the text, in pixels.</summary>
    public float FontSize { get; set; }

    /// <summary>How the fill (inside) of the text should be rendered.</summary>
    public MTSDFFillType FillType { get; set; }

    /// <summary>The colour of the fill (inside).</summary>
    public Colour FillColour { get; set; }

    /// <summary>The width (in pixels) of the border.</summary>
    public float BorderWidth { get; set; }

    /// <summary>How the border of the text should be rendered.</summary>
    public MTSDFBorderType BorderType { get; set; }

    /// <summary>The colour of the border.</summary>
    public Colour BorderColour { get; set; }

    /// <summary>The mesh of the text.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [NonSerialisable]
    public override Mesh Mesh { get; set; }


    /*********
    ** Public Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="text">The text to render.</param>
    /// <param name="fontPath">The path to the font to use to render the text.</param>
    public TextRenderer(string text, string fontPath)
    {
        _Text = text;
        _Font = new(fontPath);

        GenerateMesh();
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <inheritdoc/>
    public override void Dispose() => Font.Dispose();


    /*********
    ** Private Methods
    *********/
    /// <summary>Generates the mesh for <see cref="Text"/>.</summary>
    [OnDeserialised(SerialiserCallbackPriority.High)]
    private void GenerateMesh()
    {
        // check there is text to render.
        if (string.IsNullOrEmpty(Text))
        {
            Mesh = new("Text", Array.Empty<Vertex>(), Array.Empty<uint>());
            return;
        }

        var vertices = new List<Vertex>();
        var indices = new List<uint>();

        var xPosition = 0;
        var yPosition = 0;

        var width = 0; // record the width and height (so the vertex positions can be adjusted to centre the text)
        var height = 0;
        foreach (var character in Text)
        {
            var glyph = Font.Glyphs.FirstOrDefault(g => g.Character == character);
            if (glyph == null)
                glyph = Font.Glyphs[0];

            // draw glyph quad
            // TODO: don't assume 48, derive from font
            vertices.Add(new Vertex(new(xPosition, yPosition + 48 - glyph.Size.Y, 0), new(glyph.AtlasPosition.X, glyph.AtlasPosition.Y)));
            vertices.Add(new Vertex(new(xPosition + glyph.Size.X, yPosition + 48 - glyph.Size.Y, 0), new(glyph.AtlasPosition.X + glyph.AtlasPosition.Width, glyph.AtlasPosition.Y)));
            vertices.Add(new Vertex(new(xPosition, yPosition + 48, 0), new(glyph.AtlasPosition.X, glyph.AtlasPosition.Y + glyph.AtlasPosition.Height)));
            vertices.Add(new Vertex(new(xPosition + glyph.Size.X, yPosition + 48, 0), new(glyph.AtlasPosition.X + glyph.AtlasPosition.Width, glyph.AtlasPosition.Y + glyph.AtlasPosition.Height)));

            var topLeftIndex = (uint)vertices.Count - 4;
            var topRightIndex = (uint)vertices.Count - 3;
            var bottomLeftIndex = (uint)vertices.Count - 2;
            var bottomRightIndex = (uint)vertices.Count - 1;

            indices.Add(topLeftIndex);
            indices.Add(topRightIndex);
            indices.Add(bottomLeftIndex);

            indices.Add(bottomLeftIndex);
            indices.Add(topRightIndex);
            indices.Add(bottomRightIndex);

            xPosition += glyph.HorizontalMetrics.AdvanceWidth;

            width = xPosition;
            if (glyph.Size.Y > height)
                height = (int)glyph.Size.Y; // TODO: consider multiple lines
        }

        // adjust the vertex positions to centre the text mesh
        var verticesArray = vertices.ToArray();

        var halfWidth = width / 2;
        var halfHeight = height / 2;
        for (int i = 0; i < verticesArray.Length; i++)
        {
            ref var vertex = ref verticesArray[i];
            vertex.Position.X -= halfWidth;
            vertex.Position.Y -= halfHeight;
        }

        Mesh = new(Text, verticesArray, indices.ToArray());
    }
}

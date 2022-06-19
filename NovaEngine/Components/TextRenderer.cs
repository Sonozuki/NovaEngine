namespace NovaEngine.Components;

/// <summary>Represents a component used for rendering text.</summary>
public class TextRenderer : MeshRenderer
{
    /*********
    ** Fields
    *********/
    /// <summary>The text to render.</summary>
    [Serialisable]
    private string _Text = "";

    /// <summary>The font to use to render the text.</summary>
    [Serialisable]
    private Font _Font;


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
        get => _Font;
        set
        {
            _Font = value;
            GenerateMesh();
        }
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="text">The text to render.</param>
    /// <param name="font">The font to use to render the text.</param>
    public TextRenderer(string text, Font font)
        : base(null, null)
    {
        _Text = text;
        _Font = font;

        GenerateMesh();
    }


    /*********
    ** Protected Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    protected TextRenderer() { } // required for serialiser

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /*********
    ** Private Methods
    *********/
    /// <summary>Generates the mesh for <see cref="Text"/>.</summary>
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
        foreach (var character in Text)
        {
            var glyph = Font.Glyphs.FirstOrDefault(g => g.Character == character);
            if (glyph == null)
                glyph = Font.Glyphs[0];

            // draw glyph quad
            vertices.Add(new Vertex(new(xPosition, yPosition + glyph.AtlasPosition.Height, 0)));
            vertices.Add(new Vertex(new(xPosition + glyph.AtlasPosition.Width, yPosition + glyph.AtlasPosition.Height, 0)));
            vertices.Add(new Vertex(new(xPosition, yPosition, 0)));
            vertices.Add(new Vertex(new(xPosition + glyph.AtlasPosition.Width, yPosition, 0)));

            var topLeftIndex = (uint)vertices.Count - 4;
            var topRightIndex = (uint)vertices.Count - 3;
            var bottomLeftIndex = (uint)vertices.Count - 2;
            var bottomRightIndex = (uint)vertices.Count - 1;

            indices.Add(topLeftIndex);
            indices.Add(bottomLeftIndex);
            indices.Add(topRightIndex);

            indices.Add(bottomLeftIndex);
            indices.Add(bottomRightIndex);
            indices.Add(topRightIndex);

            xPosition += glyph.HorizontalMetrics.AdvanceWidth;
        }

        Mesh = new(Text, vertices.ToArray(), indices.ToArray());
    }
}

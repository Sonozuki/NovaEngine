namespace NovaEngine.Components;

/// <summary>Represents a component used for rendering text.</summary>
public class TextRenderer : MeshRenderer
{
    /*********
    ** Fields
    *********/
    /// <summary>The text to render.</summary>
    private string _Text = "";

    /// <summary>The font to use to render the text.</summary>
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

        var width = 0; // record the width and height (so the vertex positions can be adjusted to centre the text)
        var height = 0;
        foreach (var character in Text)
        {
            var glyph = Font.Glyphs.FirstOrDefault(g => g.Character == character);
            if (glyph == null)
                glyph = Font.Glyphs[0];

            // draw glyph quad
            // TODO: don't assume 48, derive from font
            vertices.Add(new Vertex(new(xPosition, yPosition + 48 - glyph.Size.Y, 0)));
            vertices.Add(new Vertex(new(xPosition + glyph.Size.X, yPosition + 48 - glyph.Size.Y, 0)));
            vertices.Add(new Vertex(new(xPosition, yPosition + 48, 0)));
            vertices.Add(new Vertex(new(xPosition + glyph.Size.X, yPosition + 48, 0)));

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

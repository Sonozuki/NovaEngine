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
    ** Properties
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

    /// <summary>The texture of the border.</summary>
    public Texture1D BorderTexture { get; set; } = Texture1D.Undefined;

    /// <summary>The power of the bloom.</summary>
    public float BloomPower { get; set; }

    /// <summary>The brightness of the bloom.</summary>
    public float BloomBrightness { get; set; }

    /// <summary>The mesh of the text.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [NonSerialisable]
    public override Mesh Mesh { get; set; }


    /*********
    ** Constructors
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


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override void Dispose()
    {
        Font.Dispose();
        BorderTexture.Dispose();
    }


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

        var xPosition = 0f;
        var yPosition = 0f;

        var width = 0f; // record the width and height (so the vertex positions can be adjusted to centre the text)
        var height = 0f;
        foreach (var character in Text)
        {
            var glyph = Font.Glyphs.FirstOrDefault(g => g.Character == character) ?? Font.Glyphs[0];

            // draw glyph quad
            var glyphScale = FontSize / Font.MaxGlyphHeight;
            var scaledGlyphSize = glyph.Size.ToVector2<float>() * glyphScale;

            vertices.Add(new Vertex(new(xPosition, yPosition + FontSize - scaledGlyphSize.Y, 0), new(glyph.AtlasPosition.X, glyph.AtlasPosition.Y)));
            vertices.Add(new Vertex(new(xPosition + scaledGlyphSize.X, yPosition + FontSize - scaledGlyphSize.Y, 0), new(glyph.AtlasPosition.X + glyph.AtlasPosition.Width, glyph.AtlasPosition.Y)));
            vertices.Add(new Vertex(new(xPosition, yPosition + FontSize, 0), new(glyph.AtlasPosition.X, glyph.AtlasPosition.Y + glyph.AtlasPosition.Height)));
            vertices.Add(new Vertex(new(xPosition + scaledGlyphSize.X, yPosition + FontSize, 0), new(glyph.AtlasPosition.X + glyph.AtlasPosition.Width, glyph.AtlasPosition.Y + glyph.AtlasPosition.Height)));

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

            // TODO: consider multiple lines
            xPosition += glyph.HorizontalMetrics.AdvanceWidth * glyphScale;

            width = xPosition;
            if (scaledGlyphSize.Y > height)
                height = scaledGlyphSize.Y;
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

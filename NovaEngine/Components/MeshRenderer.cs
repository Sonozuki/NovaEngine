namespace NovaEngine.Components;

/// <summary>Represents a component used for rendering a mesh.</summary>
public class MeshRenderer : ComponentBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The mesh to render.</summary>
    [Serialisable]
    protected Mesh _Mesh;


    /*********
    ** Accessors
    *********/
    /// <summary>The mesh to render.</summary>
    public Mesh? Mesh
    {
        get => _Mesh;
        set
        {
            _Mesh = value ?? Meshes.Empty;
            UpdateMesh();
        }
    }

    /// <summary>The material to use when rendering the mesh.</summary>
    public Material Material { get; private set; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="mesh">The mesh to render.</param>
    /// <param name="material">The material to use when rendering the mesh.</param>
    public MeshRenderer(Mesh? mesh, Material? material = null)
    {
        _Mesh = mesh ?? Meshes.Empty;
        Material = material ?? Material.Default;
    }

    /// <summary>Notifies the renderer that the mesh has been changed while the <see cref="Mesh"/> reference hasn't (i.e. vertex data was changed directly such as <c>Mesh.VertexData[0].Position = new(0, 1, 2);</c></summary>
    public void UpdateMesh() => this.GameObject?.RendererGameObject.UpdateMesh(_Mesh);


    /*********
    ** Protected Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    /// <summary>Constructs an instance.</summary>
    protected MeshRenderer() { } // required for serialiser

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

namespace NovaEngine.Components;

/// <summary>Represents a component used for rendering a mesh with a PBR material.</summary>
public class MeshRenderer : MeshRenderingComponentBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The mesh to render.</summary>
    protected Mesh _Mesh;


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public override Mesh Mesh
    {
        get => _Mesh;
        set
        {
            _Mesh = value;
            this.UpdateMesh();
        }
    }

    /// <summary>The material to use when rendering the mesh.</summary>
    public Material Material { get; private set; }


    /*********
    ** Constructors
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="mesh">The mesh to render.</param>
    /// <param name="material">The material to use when rendering the mesh.</param>
    public MeshRenderer(Mesh? mesh, Material? material = null)
    {
        Mesh = mesh ?? Meshes.Empty;
        Material = material ?? Material.Default;
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

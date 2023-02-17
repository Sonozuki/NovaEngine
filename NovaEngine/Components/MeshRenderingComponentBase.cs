namespace NovaEngine.Components;

/// <summary>Represents the base of a component that contains a mesh for a renderer to render.</summary>
public abstract class MeshRenderingComponentBase : ComponentBase
{
    /*********
    ** Properties
    *********/
    /// <summary>The mesh to render.</summary>
    public abstract Mesh Mesh { get; set; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Notifies the renderer that the mesh has been changed while the <see cref="Mesh"/> reference hasn't (i.e. vertex data was changed directly such as <c>Mesh.VertexData[0].Position = new(0, 1, 2);</c></summary>
    [OnDeserialised]
    public void UpdateMesh() => GameObject?.RendererGameObject.UpdateMesh(Mesh);
}

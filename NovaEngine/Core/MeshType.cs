namespace NovaEngine.Core;

/// <summary>The type of a mesh.</summary>
public enum MeshType
{
    /// <summary>The index buffer comprises of groups of two indices each representing a line to be drawn.</summary>
    LineList,

    /// <summary>The index buffer comprises of groups of three indices each representing a triangle to be drawn and filled.</summary>
    TriangleList
}

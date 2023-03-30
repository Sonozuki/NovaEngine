using NovaEngine.ContentPipeline.Font.EdgeSegments;

namespace NovaEngine.ContentPipeline.Font;

/// <summary>Represents a contour that makes up a glpyh.</summary>
internal sealed class Contour
{
    /*********
    ** Properties
    *********/
    /// <summary>The edge segments the contour is comprised of.</summary>
    public List<EdgeSegmentBase> Edges { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="edges">The edge segments the contour is comprised of.</param>
    public Contour(List<EdgeSegmentBase> edges)
    {
        Edges = edges;
    }
}

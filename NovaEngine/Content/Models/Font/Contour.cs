using NovaEngine.Content.Models.Font.EdgeSegments;

namespace NovaEngine.Content.Models.Font;

/// <summary>Represents a contour that makes up a glpyh.</summary>
internal sealed class Contour
{
    /*********
    ** Properties
    *********/
    /// <summary>The edge segments the contour is comprised of.</summary>
    public List<EdgeSegmentBase> Edges { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="edges">The edge segments the contour is comprised of.</param>
    public Contour(IEnumerable<EdgeSegmentBase> edges)
    {
        Edges = edges.ToList() ?? new();
    }
}
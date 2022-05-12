using NovaEngine.Content.Models.Font.EdgeSegments;

namespace NovaEngine.Content.Models.Font;

/// <summary>Represents a contour that makes up a glpyh.</summary>
internal class Contour
{
    /*********
    ** Accessors
    *********/
    /// <summary>The edge segments the contour is comprised of.</summary>
    public List<EdgeSegmentBase> Edges { get; } = new();


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="edges">The edge segments the contour is comprised of.</param>
    public Contour(IEnumerable<EdgeSegmentBase> edges)
    {
        Edges = edges.ToList() ?? new();
    }
}
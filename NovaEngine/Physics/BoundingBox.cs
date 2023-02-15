namespace NovaEngine.Physics;

/// <summary>Represents an axis-aligned bounding box.</summary>
public struct BoundingBox
{
    /*********
    ** Fields
    *********/
    /// <summary>The centre of the bounding box.</summary>
    public Vector3<float> Centre;

    /// <summary>The half-size of the axis.</summary>
    public Vector3<float> HalfSize;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="centre">The centre of the bounding box.</param>
    /// <param name="halfSize">The half-size of the axis.</param>
    public BoundingBox(Vector3<float> centre, Vector3<float> halfSize)
    {
        Centre = centre;
        HalfSize = halfSize;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Calculates whether two bounding boxes are intersecting.</summary>
    /// <param name="other">The bounding box to check for intersection.</param>
    /// <returns><see langword="true"/>, if the two bounding boxes are intersecting; otherwise, <see langword="false"/>.</returns>
    public bool Intersects(BoundingBox other) =>
          MathF.Abs(Centre.X - other.Centre.X) <= (HalfSize.X + other.HalfSize.X)
       && MathF.Abs(Centre.Y - other.Centre.Y) <= (HalfSize.Y + other.HalfSize.Y)
       && MathF.Abs(Centre.Z - other.Centre.Z) <= (HalfSize.Z + other.HalfSize.Z);
}

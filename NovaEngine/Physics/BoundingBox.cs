namespace NovaEngine.Physics;

/// <summary>Represents an axis-aligned bounding box.</summary>
public struct BoundingBox : IEquatable<BoundingBox>
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
        MathF.Abs(Centre.X - other.Centre.X) <= (HalfSize.X + other.HalfSize.X) &&
        MathF.Abs(Centre.Y - other.Centre.Y) <= (HalfSize.Y + other.HalfSize.Y) &&
        MathF.Abs(Centre.Z - other.Centre.Z) <= (HalfSize.Z + other.HalfSize.Z);

    /// <summary>Checks two bounding boxes for equality.</summary>
    /// <param name="other">The bounding box to check equality with.</param>
    /// <returns><see langword="true"/> if the bounding boxes are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(BoundingBox other) => this == other;

    /// <summary>Checks the bounding box and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the bounding box and object are equal; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is BoundingBox state && this == state;

    /// <summary>Retrieves the hash code of the bounding box.</summary>
    /// <returns>The hash code of the bounding box.</returns>
    public override int GetHashCode() => (Centre, HalfSize).GetHashCode();


    /*********
    ** Operators
    *********/
    /// <summary>Checks two bounding boxes for equality.</summary>
    /// <param name="left">The first bounding box.</param>
    /// <param name="right">The second bounding box.</param>
    /// <returns><see langword="true"/> if the bounding boxes are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(BoundingBox left, BoundingBox right) =>
        left.Centre == right.Centre &&
        left.HalfSize == right.HalfSize;

    /// <summary>Checks two bounding boxes for inequality.</summary>
    /// <param name="left">The first bounding box.</param>
    /// <param name="right">The second bounding box.</param>
    /// <returns><see langword="true"/> if the bounding boxes are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(BoundingBox left, BoundingBox right) => !(left == right);
}

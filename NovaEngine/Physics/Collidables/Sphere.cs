namespace NovaEngine.Physics.Collidables;

/// <summary>Represents a sphere collider.</summary>
internal class Sphere : ICollidable
{
    /*********
    ** Fields
    *********/
    /// <summary>The radius of the sphere.</summary>
    private float _Radius;

    /// <summary>The offset of the sphere.</summary>
    private Vector3<float> _Offset;

    /// <summary>The bounding box of the sphere.</summary>
    private BoundingBox _BoundingBox;


    /*********
    ** Accessors
    *********/
    /// <inheritdoc/>
    public CollidableId Id => CollidableId.Sphere;

    /// <summary>The radius of the sphere.</summary>
    public float Radius
    {
        get => _Radius;
        set
        {
            _Radius = value;
            _BoundingBox.HalfSize = new(value);
        }
    }

    /// <inheritdoc/>
    public Vector3<float> Offset
    {
        get => _Offset;
        set => _Offset = _BoundingBox.Centre = value;
    }

    /// <inheritdoc/>
    public BoundingBox BoundingBox => _BoundingBox;


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    public Sphere()
        : this(.5f, Vector3<float>.Zero) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="radius">The radius of the sphere.</param>
    /// <param name="offset">The offset of the sphere.</param>
    public Sphere(float radius, Vector3<float> offset)
    {
        Radius = radius;
        Offset = offset;
    }

    /// <inheritdoc/>
    public Vector3<float> CalculateInertia(float inverseMass) => new(2.5f * inverseMass / (Radius * Radius));
}

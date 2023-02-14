namespace NovaEngine.Physics.Collidables;

/// <summary>Represents a collidable convex shape.</summary>
internal interface ICollidable
{
    /*********
    ** Accessors
    *********/
    /// <summary>The id of the collidable.</summary>
    public CollidableId Id { get; }

    /// <summary>The offset of the collidable.</summary>
    public Vector3<float> Offset { get; set; }

    /// <summary>The bounding box of the collidable.</summary>
    public BoundingBox BoundingBox { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Calculates the inertia for the shape at a specified mass.</summary>
    /// <param name="inverseMass">The inverse of the mass of the shape to calculate the inertia for.</param>
    /// <returns>The inertia of the shape.</returns>
    public Vector3<float> CalculateInertia(float inverseMass);
}

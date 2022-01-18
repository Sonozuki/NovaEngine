namespace NovaEngine.Physics;

/// <summary>Represents a contact point between two collidables.</summary>
internal struct ContactPoint
{
    /*********
    ** Fields
    *********/
    /// <summary>The position of the contact point.</summary>
    public Vector3 Position;

    /// <summary>The normal of the contact point.</summary>
    public Vector3 Normal;

    /// <summary>The penetration depth of the contact point.</summary>
    public float PenetrationDepth;
}

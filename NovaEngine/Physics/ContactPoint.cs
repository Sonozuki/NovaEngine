namespace NovaEngine.Physics;

/// <summary>Represents a contact point between two collidables.</summary>
internal struct ContactPoint
{
    /*********
    ** Fields
    *********/
    /// <summary>The position of the contact point.</summary>
    public Vector3<float> Position;

    /// <summary>The normal of the contact point.</summary>
    public Vector3<float> Normal;

    /// <summary>The penetration depth of the contact point.</summary>
    public float PenetrationDepth;
}

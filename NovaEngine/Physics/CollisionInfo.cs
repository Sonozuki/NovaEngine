namespace NovaEngine.Physics;

/// <summary>Contains info about a collision pair.</summary>
internal struct CollisionInfo
{
    /*********
    ** Fields
    *********/
    /// <summary>The first rigid body of the collision.</summary>
    public readonly RigidBody A;

    /// <summary>The second rigid body of the collision.</summary>
    public readonly RigidBody B;

    /// <summary>The contact point of the collision.</summary>
    public ContactPoint ContactPoint;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="a">The first rigid body of the collision.</param>
    /// <param name="b">The second rigid body of the collision.</param>
    public CollisionInfo(RigidBody a, RigidBody b)
    {
        A = a;
        B = b;
        ContactPoint = default;
    }
}

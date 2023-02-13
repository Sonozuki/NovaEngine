namespace NovaEngine.Physics;

/// <summary>Represents a physics simulation.</summary>
internal class Simulation
{
    /*********
    ** Fields
    *********/
    /// <summary>The target delta time of each update.</summary>
    private float TargetUpdateDeltaTime = 1 / 120f;

    /// <summary>The delta time accumulation.</summary>
    private float DeltaTimeAccumulation;

    /// <summary>The gravity to apply in the simulation.</summary>
    private Vector3 Gravity = new(0, -9.8f, 0);

    /// <summary>The potential collisions the broad phase has calculated.</summary>
    private readonly List<CollisionInfo> BroadPhaseCollisions = new();


    /*********
    ** Accessors
    *********/
    /// <summary>The bodies in the simulation.</summary>
    public List<RigidBody> Bodies { get; } = new();


    /*********
    ** Public Methods
    *********/
    /// <summary>Steps the physics simulation by a specified amount of time.</summary>
    /// <param name="deltaTime">The amount of time to step.</param>
    public void Update(float deltaTime)
    {
        DeltaTimeAccumulation += deltaTime;
        var iterationCount = (int)(DeltaTimeAccumulation / TargetUpdateDeltaTime);

        // update acceleration from external forces
        IntegrateAcceleration(deltaTime);

        for (int i = 0; i < iterationCount; i++)
        {
            BroadPhase();
            NarrowPhase();

            // TODO: constraints
            IntegrateVelocity(TargetUpdateDeltaTime);

            DeltaTimeAccumulation -= TargetUpdateDeltaTime;
        }
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Integrates linear and angular acceleration based off of accumulated forces from the previous frame.</summary>
    /// <param name="deltaTime">The amount of time to integrate.</param>
    private void IntegrateAcceleration(float deltaTime)
    {
        foreach (var body in Bodies)
        {
            if (body.InverseMass == 0)
                continue;

            var linearAcceleration = body.Force * body.InverseMass;
            linearAcceleration += Gravity;
            body.LinearVelocity += linearAcceleration * deltaTime;

            CalculateInverseInertiaTensor(body);
            var angularAcceleration = body.InverseInertiaTensor * body.Torque;
            body.AngularVelocity += angularAcceleration * deltaTime;
        }

        // TODO: temp, collidables should calculate and cache inertia tensor
        void CalculateInverseInertiaTensor(RigidBody body)
        {
            var inverseInertia = new Vector3(2.5f * body.InverseMass / .25f);

            var inverseRotation = Matrix3x3.CreateFromQuaternion(body.GlobalRotation.Inverse);
            var rotation = Matrix3x3.CreateFromQuaternion(body.GlobalRotation);
            body.InverseInertiaTensor = rotation * Matrix3x3.CreateScale(inverseInertia) * inverseRotation;
        }
    }

    /// <summary>Integrates linear and angular velocity into position and rotation.</summary>
    /// <param name="deltaTime">The amount of time to integrate.</param>
    private void IntegrateVelocity(float deltaTime)
    {
        var dampingFactor = .05f;
        var frameDamping = MathF.Pow(dampingFactor, deltaTime);

        foreach (var body in Bodies)
        {
            body.GlobalPosition += body.LinearVelocity * deltaTime;
            body.LinearVelocity *= frameDamping;

            var rotation = body.GlobalRotation;
            rotation += new Quaternion(body.AngularVelocity * deltaTime * .5f, 0) * rotation;
            rotation.Normalise();
            body.GlobalRotation = rotation;

            body.AngularVelocity *= frameDamping;
        }
    }

    /// <summary>Determines potential collision pairs.</summary>
    private void BroadPhase()
    {
        // TODO: change this to use an accelerated structure
        BroadPhaseCollisions.Clear();
        for (int i = 0; i < Bodies.Count; i++)
            for (int j = i + 1; j < Bodies.Count; j++)
            {
                var bodyA = Bodies[i];
                var bodyB = Bodies[j];

                // TODO: bounding boxes should be calculated and cached based off of collider
                var boundingBoxA = new BoundingBox(bodyA.GlobalPosition, new(.5f));
                var boundingBoxB = new BoundingBox(bodyB.GlobalPosition, new(.5f));
                if (boundingBoxA.Intersects(boundingBoxB))
                    BroadPhaseCollisions.Add(new(bodyA, bodyB));
            }
    }

    /// <summary>Determines collision pairs and solves them.</summary>
    private void NarrowPhase()
    {
        var sphereA = new Sphere(.5f, Vector3.Zero);
        var sphereB = new Sphere(.5f, Vector3.Zero);

        for (int i = 0; i < BroadPhaseCollisions.Count; i++)
        {
            var collisionInfo = BroadPhaseCollisions[i];
            if (AreSpheresIntersecting(sphereA, collisionInfo.A.GlobalPosition, sphereB, collisionInfo.B.GlobalPosition, ref collisionInfo))
                ImpulseResolveCollision(collisionInfo.A, collisionInfo.B, collisionInfo.ContactPoint);
        }

        // TODO: should be moved out into a separate system
        static bool AreSpheresIntersecting(Sphere a, Vector3 aPosition, Sphere b, Vector3 bPosition, ref CollisionInfo collisionInfo)
        {
            var radii = a.Radius + b.Radius;
            var positionDelta = aPosition - bPosition;
            var distance = positionDelta.Length;

            if (distance >= radii)
                return false;

            var normal = positionDelta.Normalised;
            collisionInfo.ContactPoint.Position = (-normal * a.Radius + normal * b.Radius) / 2;
            collisionInfo.ContactPoint.Normal = normal;
            collisionInfo.ContactPoint.PenetrationDepth = radii - distance;

            return true;
        }
    }

    /// <summary>Resolves a collision using impulse forces.</summary>
    /// <param name="a">The first rigid body of the collision.</param>
    /// <param name="b">The second rigid body of the collision.</param>
    /// <param name="contactPoint">The contact point of the collision.</param>
    private void ImpulseResolveCollision(RigidBody a, RigidBody b, ContactPoint contactPoint)
    {
        // separate objects
        var totalInverseMass = a.InverseMass + b.InverseMass;

        a.GlobalPosition += contactPoint.Normal * contactPoint.PenetrationDepth * (a.InverseMass / totalInverseMass);
        b.GlobalPosition -= contactPoint.Normal * contactPoint.PenetrationDepth * (b.InverseMass / totalInverseMass);

        // calculate collision velocity
        var relativeA = contactPoint.Position - a.GlobalPosition;
        var relativeB = contactPoint.Position - b.GlobalPosition;

        var angularVelocityA = Vector3.Cross(a.AngularVelocity, relativeA);
        var angularVelocityB = Vector3.Cross(b.AngularVelocity, relativeB);

        var fullVelocityA = a.LinearVelocity + angularVelocityA;
        var fullVelocityB = b.LinearVelocity + angularVelocityB;

        var contactVelocity = fullVelocityA - fullVelocityB;

        // calculate impulse force
        var impulseForce = Vector3.Dot(contactVelocity, contactPoint.Normal);

        var inertiaA = Vector3.Cross(a.InverseInertiaTensor * Vector3.Cross(relativeA, contactPoint.Normal), relativeA);
        var inertiaB = Vector3.Cross(b.InverseInertiaTensor * Vector3.Cross(relativeB, contactPoint.Normal), relativeB);

        var angularEffect = Vector3.Dot(inertiaA + inertiaB, contactPoint.Normal);
        var restitution = .66f; // TODO: unhard-code coefficient of restitution
        var j = -(1 + restitution) * impulseForce / (totalInverseMass + angularEffect);

        var fullImpulse = contactPoint.Normal * j;

        // apply impulse force
        a.ApplyLinearImpulse(fullImpulse);
        b.ApplyLinearImpulse(-fullImpulse);

        a.ApplyAngularImpulse(Vector3.Cross(relativeA, fullImpulse));
        b.ApplyAngularImpulse(Vector3.Cross(relativeB, -fullImpulse));
    }
}

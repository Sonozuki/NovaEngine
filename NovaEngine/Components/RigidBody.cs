namespace NovaEngine.Components;

// TODO: currently every rigid body is assumed to be a sphere, once constraints have been implemented, a rigid body should be able to be constructed with any number of colliders
/// <summary>Represents a physics rigid body.</summary>
public class RigidBody : ComponentBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The mass of the body.</summary>
    private float _Mass;

    /// <summary>Whether the body is kinematic.</summary>
    private bool _IsKinematic;

    /// <summary>The inverse mass of the body, considering <see cref="IsKinematic"/>.</summary>
    private float _InverseMass;


    /*********
    ** Accessors
    *********/
    /// <summary>The mass of the body.</summary>
    public float Mass
    {
        get => _Mass;
        set
        {
            value = Math.Max(.0000001f, value);
            _Mass = value;

            if (IsKinematic)
                _InverseMass = 0;
            else
                _InverseMass = 1 / Mass;
        }
    }

    /// <summary>Whether the body is kinematic.</summary>
    /// <remarks>If a body is kinematic, forces, and collisions won't affect the body.</remarks>
    public bool IsKinematic
    {
        get => _IsKinematic;
        set
        {
            _IsKinematic = value;

            if (IsKinematic)
                _InverseMass = 0;
            else
                _InverseMass = 1 / Mass;
        }
    }

    /// <summary>The inverse mass of the body, considering <see cref="IsKinematic"/>.</summary>
    internal float InverseMass => _InverseMass;

    /// <summary>The linear velocity of the body.</summary>
    internal Vector3<float> LinearVelocity { get; set; }

    /// <summary>The angular velocity of the body.</summary>
    internal Vector3<float> AngularVelocity { get; set; }

    /// <summary>The inverse inertia of the body.</summary>
    internal Vector3<float> InverseInertia { get; set; }

    /// <summary>The inverse inertia tensor of the body.</summary>
    internal Matrix3x3<float> InverseInertiaTensor { get; set; }

    /// <summary>The force of the body.</summary>
    internal Vector3<float> Force { get; set; }

    /// <summary>The torque of the body.</summary>
    internal Vector3<float> Torque { get; set; }

    /// <summary>The global position of the body.</summary>
    internal Vector3<float> GlobalPosition
    {
        get => this.GameObject.Transform.GlobalPosition;
        set => this.GameObject.Transform.GlobalPosition = value;
    }

    /// <summary>The global rotation of the body.</summary>
    internal Quaternion<float> GlobalRotation
    {
        get => this.GameObject.Transform.GlobalRotation;
        set => this.GameObject.Transform.GlobalRotation = value;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Applies a linear impulse.</summary>
    /// <param name="force">The linear force to apply.</param>
    public void ApplyLinearImpulse(Vector3<float> force) => LinearVelocity += force * InverseMass;

    /// <summary>Applies an angular impulse.</summary>
    /// <param name="force">The angular force to apply.</param>
    public void ApplyAngularImpulse(Vector3<float> force) => AngularVelocity += InverseInertiaTensor * force;
}

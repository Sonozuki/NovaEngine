namespace NovaEngine.Core;

/// <summary>Represents the transform for an <see cref="Core.GameObject"/>.</summary>
public class Transform : ComponentBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The position of the object relative to the object's parent.</summary>
    private Vector3<float> _LocalPosition;

    /// <summary>The rotation of the object relative to the object's parent.</summary>
    private Quaternion<float> _LocalRotation = Quaternion<float>.Identity;

    /// <summary>The scale of the object relative to the object's parent.</summary>
    private Vector3<float> _LocalScale = Vector3<float>.One;

    /// <summary>The global position of the parent object.</summary>
    private Vector3<float> _ParentPosition;

    /// <summary>The global rotation of the parent object.</summary>
    private Quaternion<float> _ParentRotation = Quaternion<float>.Identity;

    /// <summary>The global scale of the parent object.</summary>
    private Vector3<float> _ParentScale = Vector3<float>.One;


    /*********
    ** Accessors
    *********/
    /// <summary>The position of the object relative to the object's parent.</summary>
    public Vector3<float> LocalPosition
    {
        get => _LocalPosition;
        set
        {
            _LocalPosition = value;
            foreach (var child in GameObject.Children)
                child.Transform.ParentPosition = GlobalPosition;
        }
    }

    /// <summary>The rotation of the object relative to the object's parent.</summary>
    public Quaternion<float> LocalRotation
    {
        get => _LocalRotation;
        set
        {
            _LocalRotation = value;
            foreach (var child in GameObject.Children)
                child.Transform.ParentRotation = GlobalRotation;
        }
    }

    /// <summary>The scale of the object relative to the object's parent.</summary>
    public Vector3<float> LocalScale
    {
        get => _LocalScale;
        set
        {
            _LocalScale = value;
            foreach (var child in GameObject.Children)
                child.Transform.ParentScale = GlobalScale;
        }
    }

    /// <summary>The world position of the object.</summary>
    public Vector3<float> GlobalPosition
    {
        get => ParentPosition + LocalPosition;
        set => LocalPosition = value - ParentPosition;
    }

    /// <summary>The world rotation of the object.</summary>
    public Quaternion<float> GlobalRotation
    {
        get => ParentRotation * LocalRotation;
        set => LocalRotation = value * ParentRotation.Inverse;
    }

    /// <summary>The world scale of the object.</summary>
    public Vector3<float> GlobalScale
    {
        get => ParentScale * LocalScale;
        set => LocalScale = value / ParentScale;
    }

    /// <summary>The forward direction of the tranform in local space.</summary>
    public Vector3<float> LocalForward => Vector3<float>.UnitZ * LocalRotation;

    /// <summary>The backward direction of the tranform in local space.</summary>
    public Vector3<float> LocalBackward => (-Vector3<float>.UnitZ) * LocalRotation;

    /// <summary>The up direction of the tranform in local space.</summary>
    public Vector3<float> LocalUp => Vector3<float>.UnitY * LocalRotation;

    /// <summary>The down direction of the tranform in local space.</summary>
    public Vector3<float> LocalDown => (-Vector3<float>.UnitY) * LocalRotation;

    /// <summary>The left direction of the tranform in local space.</summary>
    public Vector3<float> LocalLeft => (-Vector3<float>.UnitX) * LocalRotation;

    /// <summary>The right direction of the tranform in local space.</summary>
    public Vector3<float> LocalRight => Vector3<float>.UnitX * LocalRotation;

    /// <summary>The forward direction of the tranform in world space.</summary>
    public Vector3<float> GlobalForward => Vector3<float>.UnitZ * GlobalRotation;

    /// <summary>The backward direction of the tranform in world space.</summary>
    public Vector3<float> GlobalBackward => (-Vector3<float>.UnitZ) * GlobalRotation;

    /// <summary>The up direction of the tranform in world space.</summary>
    public Vector3<float> GlobalUp => Vector3<float>.UnitY * GlobalRotation;

    /// <summary>The down direction of the tranform in world space.</summary>
    public Vector3<float> GlobalDown => (-Vector3<float>.UnitY) * GlobalRotation;

    /// <summary>The left direction of the tranform in world space.</summary>
    public Vector3<float> GlobalLeft => (-Vector3<float>.UnitX) * GlobalRotation;

    /// <summary>The right direction of the tranform in world space.</summary>
    public Vector3<float> GlobalRight => Vector3<float>.UnitX * GlobalRotation;

    /// <summary>The global position of the parent object.</summary>
    internal Vector3<float> ParentPosition
    {
        get => _ParentPosition;
        set
        {
            _ParentPosition = value;
            foreach (var child in GameObject.Children)
                child.Transform.ParentPosition = GlobalPosition;
        }
    }

    /// <summary>The global rotation of the parent object.</summary>
    internal Quaternion<float> ParentRotation
    {
        get => _ParentRotation;
        set
        {
            _ParentRotation = value;
            foreach (var child in GameObject.Children)
                child.Transform.ParentRotation = GlobalRotation;
        }
    }

    /// <summary>The global scale of the parent object.</summary>
    internal Vector3<float> ParentScale
    {
        get => _ParentScale;
        set
        {
            _ParentScale = value;
            foreach (var child in GameObject.Children)
                child.Transform.ParentScale = GlobalScale;
        }
    }


    /*********
    ** Internal Methods
    *********/
    /// <summary>Contructs an instance.</summary>
    /// <param name="gameObject">The game object the transform belongs to.</param>
    internal Transform(GameObject gameObject)
    {
        GameObject = gameObject;
    }
}

namespace NovaEngine.Core;

/// <summary>Represents the transform for an <see cref="Core.GameObject"/>.</summary>
public class Transform
{
    /*********
    ** Fields
    *********/
    /// <summary>The position of the object relative to the object's parent.</summary>
    [Serialisable]
    private Vector3 _LocalPosition;

    /// <summary>The rotation of the object relative to the object's parent.</summary>
    [Serialisable]
    private Quaternion _LocalRotation = Quaternion.Identity;

    /// <summary>The scale of the object relative to the object's parent.</summary>
    [Serialisable]
    private Vector3 _LocalScale = Vector3.One;

    /// <summary>The global position of the parent object.</summary>
    [Serialisable]
    private Vector3 _ParentPosition;

    /// <summary>The global rotation of the parent object.</summary>
    [Serialisable]
    private Quaternion _ParentRotation = Quaternion.Identity;

    /// <summary>The global scale of the parent object.</summary>
    [Serialisable]
    private Vector3 _ParentScale = Vector3.One;


    /*********
    ** Accessors
    *********/
    /// <summary>The position of the object relative to the object's parent.</summary>
    public Vector3 LocalPosition
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
    public Quaternion LocalRotation
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
    public Vector3 LocalScale
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
    public Vector3 GlobalPosition
    {
        get => ParentPosition + LocalPosition;
        set => LocalPosition = value - ParentPosition;
    }

    /// <summary>The world rotation of the object.</summary>
    public Quaternion GlobalRotation
    {
        get => ParentRotation * LocalRotation;
        set => LocalRotation = value * ParentRotation.Inverse;
    }

    /// <summary>The world scale of the object.</summary>
    public Vector3 GlobalScale
    {
        get => ParentScale * LocalScale;
        set => LocalScale = value / ParentScale;
    }

    /// <summary>The game object the transform belongs to.</summary>
    public GameObject GameObject { get; }

    /// <summary>The forward direction of the tranform in local space.</summary>
    public Vector3 LocalForward => Vector3.UnitZ * LocalRotation;

    /// <summary>The backward direction of the tranform in local space.</summary>
    public Vector3 LocalBackward => (-Vector3.UnitZ) * LocalRotation;

    /// <summary>The up direction of the tranform in local space.</summary>
    public Vector3 LocalUp => Vector3.UnitY * LocalRotation;

    /// <summary>The down direction of the tranform in local space.</summary>
    public Vector3 LocalDown => (-Vector3.UnitY) * LocalRotation;

    /// <summary>The left direction of the tranform in local space.</summary>
    public Vector3 LocalLeft => (-Vector3.UnitX) * LocalRotation;

    /// <summary>The right direction of the tranform in local space.</summary>
    public Vector3 LocalRight => Vector3.UnitX * LocalRotation;

    /// <summary>The forward direction of the tranform in world space.</summary>
    public Vector3 GlobalForward => Vector3.UnitZ * GlobalRotation;

    /// <summary>The backward direction of the tranform in world space.</summary>
    public Vector3 GlobalBackward => (-Vector3.UnitZ) * GlobalRotation;

    /// <summary>The up direction of the tranform in world space.</summary>
    public Vector3 GlobalUp => Vector3.UnitY * GlobalRotation;

    /// <summary>The down direction of the tranform in world space.</summary>
    public Vector3 GlobalDown => (-Vector3.UnitY) * GlobalRotation;

    /// <summary>The left direction of the tranform in world space.</summary>
    public Vector3 GlobalLeft => (-Vector3.UnitX) * GlobalRotation;

    /// <summary>The right direction of the tranform in world space.</summary>
    public Vector3 GlobalRight => Vector3.UnitX * GlobalRotation;

    /// <summary>The global position of the parent object.</summary>
    internal Vector3 ParentPosition
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
    internal Quaternion ParentRotation
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
    internal Vector3 ParentScale
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


    /*********
    ** Protected Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    /// <summary>Constructs an instance.</summary>
    protected Transform() { } // required for serialiser

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

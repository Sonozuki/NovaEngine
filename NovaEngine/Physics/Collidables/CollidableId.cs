namespace NovaEngine.Physics.Collidables;

/// <summary>The ids of a collidable.</summary>
internal enum CollidableId
{
    /// <summary>An invalid collidable id.</summary>
    Invalid = 0,

    /// <summary>The sphere collidable id.</summary>
    Sphere = 1 << 0
}

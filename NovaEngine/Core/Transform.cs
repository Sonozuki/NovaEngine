using NovaEngine.Maths;
using NovaEngine.Rendering;

namespace NovaEngine.Core
{
    /// <summary>Represents the transform for an <see cref="Core.GameObject"/>.</summary>
    public class Transform
    {
        /*********
        ** Fields
        *********/
        /// <summary>The position of the object relative to the object's parent.</summary>
        public Vector3 LocalPosition;

        /// <summary>The rotation of the object relative to the object's parent.</summary>
        public Quaternion LocalRotation = Quaternion.Identity;

        /// <summary>The scale of the object relative to the object's parent.</summary>
        public Vector3 LocalScale = Vector3.One;


        /*********
        ** Accessors
        *********/
        /// <summary>The game object the transform belongs to.</summary>
        public GameObject GameObject { get; }

        /// <summary>The forward direction of the tranform in world space.</summary>
        public Vector3 Forward => Vector3.UnitZ * GlobalRotation;

        /// <summary>The backward direction of the tranform in world space.</summary>
        public Vector3 Backward => (-Vector3.UnitZ) * GlobalRotation;

        /// <summary>The up direction of the tranform in world space.</summary>
        public Vector3 Up => Vector3.UnitY * GlobalRotation;

        /// <summary>The down direction of the tranform in world space.</summary>
        public Vector3 Down => (-Vector3.UnitY) * GlobalRotation;

        /// <summary>The left direction of the tranform in world space.</summary>
        public Vector3 Left => (-Vector3.UnitX) * GlobalRotation;

        /// <summary>The right direction of the tranform in world space.</summary>
        public Vector3 Right => Vector3.UnitX * GlobalRotation;

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

        /// <summary>The transform matrix.</summary>
        public Matrix4x4 Matrix => Matrix4x4.CreateScale(GlobalScale)
                                 * Matrix4x4.CreateTranslation(
                                       GlobalPosition.X * (RendererManager.MVPSettings.InvertX ? -1 : 1),
                                       GlobalPosition.Y * (RendererManager.MVPSettings.InvertY ? -1 : 1),
                                       GlobalPosition.Z * (RendererManager.MVPSettings.InvertZ ? -1 : 1))
                                 * Matrix4x4.CreateFromQuaternion(RendererManager.MVPSettings.InvertRotation ? GlobalRotation.Inverse : GlobalRotation);

        /// <summary>The transform of the parent object.</summary>
        private Transform? ParentTransform => GameObject.Parent?.Transform;

        /// <summary>The global position of the parent object.</summary>
        private Vector3 ParentPosition => ParentTransform?.GlobalPosition ?? Vector3.Zero;

        /// <summary>The global rotation of the parent object.</summary>
        private Quaternion ParentRotation => ParentTransform?.GlobalRotation ?? Quaternion.Identity;

        /// <summary>The global scale of the parent object.</summary>
        private Vector3 ParentScale => ParentTransform?.GlobalScale ?? Vector3.One;


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
        ** Private Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>Constructs an instance.</summary>
        private Transform() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

﻿using NovaEngine.Maths;

namespace NovaEngine.Core
{
    /// <summary>Represents the transform for an <see cref="Core.GameObject"/>.</summary>
    public class Transform
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The game object the transform belongs to.</summary>
        public GameObject GameObject { get; }

        /// <summary>The position of the object relative to the object's parent.</summary>
        public Vector3 LocalPosition { get; set; }

        /// <summary>The rotation of the object relative to the object's parent, in degrees.</summary>
        public Quaternion LocalRotation { get; set; }

        /// <summary>The scale of the object relative to the object's parent.</summary>
        public Vector3 LocalScale { get; set; } = Vector3.One;

        /// <summary>The world position of the object.</summary>
        public Vector3 GlobalPosition
        {
            get => ParentPosition + LocalPosition;
            set => LocalPosition = value - ParentPosition;
        }

        /// <summary>The world rotation of the object, in degrees.</summary>
        public Quaternion GlobalRotation
        {
            get => ParentRotation + LocalRotation;
            set => LocalRotation = value - ParentRotation;
        }

        /// <summary>The world scale of the object.</summary>
        public Vector3 GlobalScale
        {
            get => ParentScale * LocalScale;
            set => LocalScale = value - ParentScale;
        }

        /// <summary>The transform matrix.</summary>
        public Matrix4x4 Matrix => Matrix4x4.CreateScale(GlobalScale)
                                 * Matrix4x4.CreateFromQuaternion(GlobalRotation)
                                 * Matrix4x4.CreateTranslation(GlobalPosition);

        /// <summary>The <see cref="Transform"/> of the parent <see cref="Core.GameObject"/>.</summary>
        private Transform? ParentTransform => GameObject?.Parent?.Transform;

        /// <summary>The global position of the parent object.</summary>
        private Vector3 ParentPosition => ParentTransform?.GlobalPosition ?? Vector3.Zero;

        /// <summary>The global rotation of the parent object, in degrees.</summary>
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
    }
}
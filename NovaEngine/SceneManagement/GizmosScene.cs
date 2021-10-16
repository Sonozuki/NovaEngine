using NovaEngine.Core;
using NovaEngine.DataStructures;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using System;

namespace NovaEngine.SceneManagement
{
    /// <summary>Represents a scene specifically for storing gizmos.</summary>
    internal class GizmosScene : Scene
    {
        /*********
        ** Fields
        *********/
        //// <summary>The pool of cube game objects.</summary>
        private ObjectPool<GameObject> CubeGameObjects = new(() => GameObject.Cube, gameObject => gameObject.IsEnabled = false);


        /*********
        ** Public Methods
        *********/
        /// <summary>Adds a cube gizmo to the scene.</summary>
        /// <param name="position">The position of the cube.</param>
        /// <param name="scale">The scale of the cube.</param>
        /// <param name="colour">The colour of the cube.</param>
        public void AddCube(Vector3 position, Vector3 scale, Colour colour)
        {
            var gameObject = CubeGameObjects.GetObject();
            gameObject.IsEnabled = true;

            gameObject.Transform.LocalPosition = position;
            gameObject.Transform.LocalScale = scale;
            gameObject.Components.MeshRenderer!.Material.Tint = colour;

            RootGameObjects.Add(gameObject);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            CubeGameObjects.Dispose();
        }


        /*********
        ** Internal Methods
        *********/
        /// <inheritdoc/>
        internal override void Start() { }

        /// <inheritdoc/>
        internal override void Update()
        {
            while (RootGameObjects.Count > 0)
            {
                var gameObject = RootGameObjects[0];
                RootGameObjects.RemoveAt(0);

                CubeGameObjects.ReturnObject(gameObject);
            }
        }
    }
}

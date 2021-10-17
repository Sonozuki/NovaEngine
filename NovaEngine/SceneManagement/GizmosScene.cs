using NovaEngine.Core;
using NovaEngine.DataStructures;
using NovaEngine.Graphics;
using NovaEngine.Maths;

namespace NovaEngine.SceneManagement
{
    /// <summary>Represents a scene specifically for storing gizmos.</summary>
    internal class GizmosScene : Scene
    {
        /*********
        ** Fields
        *********/
        /// <summary>The pool of cube game objects.</summary>
        private readonly ObjectPool<GameObject> CubeGameObjects = new(() => GameObject.Cube, gameObject => gameObject.IsEnabled = false);

        /// <summary>The pool of sphere game objects.</summary>
        private readonly ObjectPool<GameObject> SphereGameObjects = new(() => GameObject.Sphere, gameObject => gameObject.IsEnabled = false);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>The game object that contains all the cubes as children.</summary>
        private GameObject CubesParent;

        /// <summary>The game object that contains all the spheres as children.</summary>
        private GameObject SpheresParent;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


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

            CubesParent.Children.Add(gameObject);
        }

        /// <summary>Adds a sphere gizmo to the scene.</summary>
        /// <param name="position">The position of the sphere.</param>
        /// <param name="radius">The radius of the sphere.</param>
        /// <param name="colour">The colour of the sphere.</param>
        public void AddSphere(Vector3 position, float radius, Colour colour)
        {
            var gameObject = SphereGameObjects.GetObject();
            gameObject.IsEnabled = true;

            gameObject.Transform.LocalPosition = position;
            gameObject.Transform.LocalScale = new(radius);
            gameObject.Components.MeshRenderer!.Material.Tint = colour;

            SpheresParent.Children.Add(gameObject);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            CubeGameObjects.Dispose();
            SphereGameObjects.Dispose();
        }


        /*********
        ** Internal Methods
        *********/
        /// <inheritdoc/>
        internal override void Start()
        {
            CubesParent = new GameObject("Cubes");
            SpheresParent = new GameObject("Spheres");

            RootGameObjects.Add(CubesParent);
            RootGameObjects.Add(SpheresParent);
        }

        /// <inheritdoc/>
        internal override void Update()
        {
            // cubes
            while (CubesParent.Children.Count > 0)
            {
                var cube = CubesParent.Children[0];
                CubesParent.Children.RemoveAt(0);
                CubeGameObjects.ReturnObject(cube);
            }

            // spheres
            while (SpheresParent.Children.Count > 0)
            {
                var sphere = SpheresParent.Children[0];
                SpheresParent.Children.RemoveAt(0);
                SphereGameObjects.ReturnObject(sphere);
            }
        }
    }
}

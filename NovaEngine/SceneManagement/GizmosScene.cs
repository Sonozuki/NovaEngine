namespace NovaEngine.SceneManagement;

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

    /// <summary>The pool of line game objects.</summary>
    private readonly ObjectPool<GameObject> LineGameObjects = new(() => GameObject.Line, gameObject => gameObject.IsEnabled = false);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>The game object that contains all the cubes as children.</summary>
    private GameObject CubesParent;

    /// <summary>The game object that contains all the spheres as children.</summary>
    private GameObject SpheresParent;

    /// <summary>The game object that contains all the lines as children.</summary>
    private GameObject LinesParent;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    public GizmosScene()
        : base("Gizmos", true)
    {
        CubesParent = new GameObject("Cubes");
        SpheresParent = new GameObject("Spheres");
        LinesParent = new GameObject("Lines");
    }

    /// <summary>Adds a cube gizmo to the scene.</summary>
    /// <param name="position">The position of the cube.</param>
    /// <param name="rotation">The rotation of the cube.</param>
    /// <param name="scale">The scale of the cube.</param>
    /// <param name="colour">The colour of the cube.</param>
    public void AddCube(Vector3<float> position, Quaternion<float> rotation, Vector3<float> scale, Colour colour)
    {
        var gameObject = CubeGameObjects.GetObject();
        gameObject.IsEnabled = true;

        gameObject.Transform.LocalPosition = position;
        gameObject.Transform.LocalRotation = rotation;
        gameObject.Transform.LocalScale = scale;
        gameObject.Components.Get<MeshRenderer>()!.Material.Tint = colour;

        CubesParent.Children.Add(gameObject);
    }

    /// <summary>Adds a sphere gizmo to the scene.</summary>
    /// <param name="position">The position of the sphere.</param>
    /// <param name="radius">The radius of the sphere.</param>
    /// <param name="colour">The colour of the sphere.</param>
    public void AddSphere(Vector3<float> position, float radius, Colour colour)
    {
        var gameObject = SphereGameObjects.GetObject();
        gameObject.IsEnabled = true;

        gameObject.Transform.LocalPosition = position;
        gameObject.Transform.LocalScale = new(radius);
        gameObject.Components.Get<MeshRenderer>()!.Material.Tint = colour;

        SpheresParent.Children.Add(gameObject);
    }

    /// <summary>Adds a line gizmo to the scene.</summary>
    /// <param name="point1">The first point of the line.</param>
    /// <param name="point2">The second point of the line.</param>
    /// <param name="colour">The colour of the line.</param>
    public void AddLine(Vector3<float> point1, Vector3<float> point2, Colour colour)
    {
        var gameObject = LineGameObjects.GetObject();
        gameObject.IsEnabled = true;

        var meshRenderer = gameObject.Components.Get<MeshRenderer>();
        meshRenderer!.Mesh!.VertexData[0].Position = point1;
        meshRenderer!.Mesh!.VertexData[1].Position = point2;
        meshRenderer!.UpdateMesh();

        meshRenderer!.Material.Tint = colour;

        LinesParent.Children.Add(gameObject);
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        base.Dispose();
        CubeGameObjects.Dispose();
        SphereGameObjects.Dispose();
        LineGameObjects.Dispose();
    }


    /*********
    ** Internal Methods
    *********/
    /// <inheritdoc/>
    internal override void Start()
    {
        RootGameObjects.Add(CubesParent);
        RootGameObjects.Add(SpheresParent);
        RootGameObjects.Add(LinesParent);
    }

    /// <inheritdoc/>
    internal override void Update()
    {
        while (CubesParent.Children.Count > 0)
        {
            var cube = CubesParent.Children[0];
            CubesParent.Children.RemoveAt(0);
            CubeGameObjects.ReturnObject(cube);
        }

        while (SpheresParent.Children.Count > 0)
        {
            var sphere = SpheresParent.Children[0];
            SpheresParent.Children.RemoveAt(0);
            SphereGameObjects.ReturnObject(sphere);
        }

        while (LinesParent.Children.Count > 0)
        {
            var line = LinesParent.Children[0];
            LinesParent.Children.RemoveAt(0);
            LineGameObjects.ReturnObject(line);
        }
    }
}

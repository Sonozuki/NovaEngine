﻿namespace NovaEngine.External.Rendering;

/// <summary>Represents a renderer game object.</summary>
public abstract class RendererGameObjectBase
{
    /*********
    ** Accessors
    *********/
    /// <summary>The underlying game object.</summary>
    public GameObject BaseGameObject { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="baseGameObject">The underlying game object.</param>
    public RendererGameObjectBase(GameObject baseGameObject)
    {
        BaseGameObject = baseGameObject;
    }

    /// <summary>Invoked whenever the mesh of a game object gets updated.</summary>
    /// <param name="mesh">The updated mesh.</param>
    public abstract void UpdateMesh(Mesh mesh);

    /// <summary>Invoked every tick to update the game object for a specified camera, to update a UBO.</summary>
    /// <param name="camera">The camera to update the game object UBO with.</param>
    public abstract void UpdateUBO(Camera camera);

    /// <inheritdoc/>
    public abstract void Dispose();
}

namespace NovaEngine.Core;

/// <summary>Represents a mesh material.</summary>
public class Material
{
    /*********
    ** Properties
    *********/
    /// <summary>The tint of the material.</summary>
    public Colour Tint { get; set; }

    /// <summary>The roughness of the material.</summary>
    public float Roughness { get; set; }

    /// <summary>The metallicness of the material.</summary>
    public float Metallicness { get; set; }

    /// <summary>The default material.</summary>
    [NonSerialisable]
    public static Material Default => new(Colour.Grey, .5f, .5f);


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public Material() { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="tint">The tint of the material.</param>
    /// <param name="roughness">The roughness of the material.</param>
    /// <param name="metallicness">The metallicness of the material.</param>
    public Material(Colour tint, float roughness, float metallicness)
    {
        Tint = tint;
        Roughness = roughness;
        Metallicness = metallicness;
    }
}

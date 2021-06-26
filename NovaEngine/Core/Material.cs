using NovaEngine.Graphics;

namespace NovaEngine.Core
{
    /// <summary>Represents a mesh material.</summary>
    public class Material
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The tint of the material.</summary>
        public Colour Tint { get; set; }

        /// <summary>The default material.</summary>
        public static Material Default { get; } = new(Colour.Gray);


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        public Material() { }

        /// <summary>Constructs an instance.</summary>
        /// <param name="tint">The tint of the material.</param>
        public Material(Colour tint)
        {
            Tint = tint;
        }
    }
}

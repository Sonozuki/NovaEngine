namespace NovaEngine.Maths
{
    /// <summary>Represents a width and height.</summary>
    public struct Size
    {
        /*********
        ** Fields
        *********/
        /// <summary>The width of the size.</summary>
        public int Width;

        /// <summary>The height of the size.</summary>
        public int Height;


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="width">The width of the size.</param>
        /// <param name="height">The height of the size.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}

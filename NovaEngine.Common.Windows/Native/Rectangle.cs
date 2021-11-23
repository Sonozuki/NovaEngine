using NovaEngine.Maths;

namespace NovaEngine.Common.Windows.Native
{
    /// <summary>Contains a rectangle defined by its upper-left and lower-right corners.</summary>
    public struct Rectangle
    {
        /*********
        ** Fields
        *********/
        /// <summary>The x-coordinate of the upper-left corner of the rectangle.</summary>
        public int Left;

        /// <summary>The y-coordinate of the upper-left corner of the rectangle.</summary>
        public int Top;

        /// <summary>The x-coordinate of the lower-right corner of the rectangle.</summary>
        public int Right;

        /// <summary>The y-coordinate of the lower-right corner of the rectangle.</summary>
        public int Bottom;


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="topLeft">The coordinates of the upper-left corner of the rectangle.</param>
        /// <param name="bottomRight">The coordinates of the lower-right corner of the rectangle.</param>
        public Rectangle(Vector2I topLeft, Vector2I bottomRight)
        {
            Left = topLeft.X;
            Top = topLeft.Y;
            Right = bottomRight.X;
            Bottom = bottomRight.Y;
        }


        /*********
        ** Operators
        *********/
        /// <summary>Checks two rectangles for equality.</summary>
        /// <param name="rectangle1">The first rectangle.</param>
        /// <param name="rectangle2">The second rectangle.</param>
        /// <returns><see langword="true"/>, if the rectangles are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Rectangle rectangle1, Rectangle rectangle2) => rectangle1.Left == rectangle2.Left && rectangle1.Top == rectangle2.Top && rectangle1.Right == rectangle2.Right && rectangle1.Bottom == rectangle2.Bottom;

        /// <summary>Checks two rectangles for inequality.</summary>
        /// <param name="rectangle1">The first rectangle.</param>
        /// <param name="rectangle2">The second rectangle.</param>
        /// <returns><see langword="true"/>, if the rectangles are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Rectangle rectangle1, Rectangle rectangle2) => !(rectangle1 == rectangle2);

    }
}

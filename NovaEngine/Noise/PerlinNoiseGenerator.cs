using NovaEngine.Maths;

namespace NovaEngine.Noise
{
    /// <summary>Handles generating Perlin noise.</summary>
    public static class PerlinNoiseGenerator
    {
        /*********
        ** Fields
        *********/
        /// <summary>The short permutation set, this will be duplicated twice into <see cref="Permutation"/></summary>
        private static readonly int[] ShortPermutation = new int[256] {
            151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30, 69, 142, 8, 99, 37, 240, 21, 10, 23, 190, 6, 148, 247, 120,
            234, 75, 0, 26, 197, 62, 94, 252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 88, 237, 149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175, 74, 165, 71,
            134, 139, 48, 27, 166, 77, 146, 158, 231, 83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54, 65, 25, 63,
            161, 1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 135, 130, 116, 188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 226,
            250, 124, 123, 5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 206, 59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 223, 183, 170, 213, 119, 248, 152,
            2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43, 172, 9, 129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218, 246, 97,
            228, 251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145, 235, 249, 14, 239, 107, 49, 192, 214, 31, 181, 199, 106, 157, 184, 84,
            204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205, 93, 222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180
        };

        /// <summary>The full permutation set, this is <see cref="ShortPermutation"/> duplicated twice.</summary>
        private static readonly int[] Permutation = new int[512];


        /*********
        ** Public Methods
        *********/
        /// <summary>Initialises the class.</summary>
        static PerlinNoiseGenerator()
        {
            for (int i = 0; i < 256; i++)
                Permutation[i] = Permutation[i + 256] = ShortPermutation[i];
        }

        /// <summary>Evaluates a point in space.</summary>
        /// <param name="x">The X position to evaluate.</param>
        /// <param name="y">The Y position to evaluate.</param>
        /// <param name="z">The Z position to evaluate.</param>
        /// <returns>The value of the specified point.</returns>
        public static float Evaluate(float x, float y, float z)
        {
            // calculate the unit cube that contains the point being evaluated
            var xi = (int)x & 255;
            var yi = (int)y & 255;
            var zi = (int)z & 255;

            var xf = x - (int)x;
            var yf = y - (int)y;
            var zf = z - (int)z;

            var u = Fade(xf);
            var v = Fade(yf);
            var w = Fade(zf);

            // calculate hashes. this is done by taking xi (between 0 and 255 because of the mask) and get the corresponding permutation
            // then add yi to it, then get the corresponding permutation of that and add zi to it

            // then, we get another value by adding 1 and getting the corresponding permutation of that, and add zi
            // this is repeated with xi+1 to get another set

            // another set will be calculated later on by using aa, ab, ba, and bb and with +1 to create a set of 8 values, representing the corners of the unit cube
            var a = Permutation[xi] + yi;
            var aa = Permutation[a] + zi;
            var ab = Permutation[a + 1] + zi;

            var b = Permutation[xi + 1] + yi;
            var ba = Permutation[b] + zi;
            var bb = Permutation[b + 1] + zi;

            // using the above set, calculate a new set and use that to get our final gradient values, then interpolate between all 8 values to get the point being evaluated
            var x1 = MathsHelper.Lerp(
                Gradient(Permutation[aa], xf, yf, zf),
                Gradient(Permutation[ba], xf - 1, yf, zf),
                u);
            var x2 = MathsHelper.Lerp(
                Gradient(Permutation[ab], xf, yf - 1, zf),
                Gradient(Permutation[bb], xf - 1, yf - 1, zf),
                u);
            var y1 = MathsHelper.Lerp(x1, x2, v);

            x1 = MathsHelper.Lerp(
                Gradient(Permutation[aa], xf, yf, zf - 1),
                Gradient(Permutation[ba], xf - 1, yf, zf - 1),
                u);
            x2 = MathsHelper.Lerp(
                Gradient(Permutation[ab], xf, yf - 1, zf - 1),
                Gradient(Permutation[bb], xf - 1, yf - 1, zf - 1),
                u);
            var y2 = MathsHelper.Lerp(x1, x2, v);

            return (MathsHelper.Lerp(y1, y2, w) + 1) / 2; // rebind to be 0 to 1 (instead of -1 to 1)
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Eases coordinate values toward integral values, this ends up smoothing the final output.</summary>
        /// <param name="t">The value to fade.</param>
        /// <returns>The value, faded.</returns>
        private static float Fade(float t) => t * t * t * (t * (t * 6 - 15) + 10);

        /// <summary>Calculates a dot product between (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) and a pseudo random gradient vector.</summary>
        /// <param name="hash">The psuedo random gradient vector..</param>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        /// <returns>The calculates dot product.</returns>
        private static float Gradient(int hash, float x, float y, float z)
        {
            var h = hash & 0b1111;

            var u = h < 0b1000 ? x : y;
            var v = h < 0b0100 ? y : h == 0b1100 || h == 0b1110 ? x : z;

            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v); // use the last 2 bits to decide the sign of u and v
        }
    }
}

namespace NovaEngine.Noise;

/// <summary>Handles generating Perlin noise.</summary>
public class PerlinNoiseGenerator
{
    /*********
    ** Fields
    *********/
    /// <summary>The permutation table.</summary>
    private readonly int[] Permutation = new int[512];

    /// <summary>The gradients used for calculating 2D noise.</summary>
    private readonly static Vector2I[] Gradients2D = new Vector2I[] { new(0, 1), new(1, 1), new(1, 0), new(1, -1), new(0, -1), new(-1, -1), new(-1, 0), new(-1, 1) };


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public PerlinNoiseGenerator()
        : this(new Random()) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="seed">The seed to use when generating the noise.</param>
    public PerlinNoiseGenerator(int seed)
        : this(new Random(seed)) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="random">The rng to use when generating the noise.</param>
    public PerlinNoiseGenerator(Random random)
    {
        var shortPermutation = Enumerable.Range(0, 256).ToArray();
        for (int i = shortPermutation.Length - 1; i >= 0; i--) // shuffle using Fisher-Yates shuffle algorithm. see: https://blog.codinghorror.com/the-danger-of-naivete/
            Swap(ref shortPermutation[i], ref shortPermutation[random.Next(i + 1)]);

        for (int i = 0; i < 256; i++)
            Permutation[i] = Permutation[i + 256] = shortPermutation[i];

        static void Swap(ref int a, ref int b) => (a, b) = (b, a);
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Evaluates a point in space.</summary>
    /// <param name="x">The X position to evaluate.</param>
    /// <param name="y">The Y position to evaluate.</param>
    /// <returns>The value of the specified point.</returns>
    public float Evaluate(float x, float y)
    {
        // calculate the unit square that contains the point being evaluated
        var xi = (int)x & 255;
        var yi = (int)y & 255;

        var xf = x - (int)x;
        var yf = y - (int)y;

        var u = Fade(xf);
        var v = Fade(yf);

        // calculate the hashes
        var pyia = Permutation[yi];
        var pyib = Permutation[yi + 1];

        var aa = Permutation[xi +     pyia];
        var ba = Permutation[xi + 1 + pyia];
        var ab = Permutation[xi +     pyib];
        var bb = Permutation[xi + 1 + pyib];

        // interpolate between the 4 points being evaluated
        var x1 = MathsHelper<float>.Lerp(
            Gradient(aa, xf, yf),
            Gradient(ba, xf - 1, yf),
            u);
        var x2 = MathsHelper<float>.Lerp(
            Gradient(ab, xf, yf - 1),
            Gradient(bb, xf - 1, yf - 1),
            u);

        return (MathsHelper<float>.Lerp(x1, x2, v) + 1) / 2;
    }

    /// <summary>Evaluates a point in space.</summary>
    /// <param name="x">The X position to evaluate.</param>
    /// <param name="y">The Y position to evaluate.</param>
    /// <param name="z">The Z position to evaluate.</param>
    /// <returns>The value of the specified point.</returns>
    public float Evaluate(float x, float y, float z)
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

        // calculate the hashes
        var a = Permutation[xi] + yi;
        var aa = Permutation[a] + zi;
        var ab = Permutation[a + 1] + zi;

        var b = Permutation[xi + 1] + yi;
        var ba = Permutation[b] + zi;
        var bb = Permutation[b + 1] + zi;

        // interpolate between the 8 points being evaluated
        var x1 = MathsHelper<float>.Lerp(
            Gradient(Permutation[aa], xf, yf, zf),
            Gradient(Permutation[ba], xf - 1, yf, zf),
            u);
        var x2 = MathsHelper<float>.Lerp(
            Gradient(Permutation[ab], xf, yf - 1, zf),
            Gradient(Permutation[bb], xf - 1, yf - 1, zf),
            u);
        var y1 = MathsHelper<float>.Lerp(x1, x2, v);

        x1 = MathsHelper<float>.Lerp(
            Gradient(Permutation[aa + 1], xf, yf, zf - 1),
            Gradient(Permutation[ba + 1], xf - 1, yf, zf - 1),
            u);
        x2 = MathsHelper<float>.Lerp(
            Gradient(Permutation[ab + 1], xf, yf - 1, zf - 1),
            Gradient(Permutation[bb + 1], xf - 1, yf - 1, zf - 1),
            u);
        var y2 = MathsHelper<float>.Lerp(x1, x2, v);

        return (MathsHelper<float>.Lerp(y1, y2, w) + 1) / 2;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Eases coordinate values toward integral values, this ends up smoothing the final output.</summary>
    /// <param name="t">The value to fade.</param>
    /// <returns>The value, faded.</returns>
    private static float Fade(float t) => t * t * t * (t * (t * 6 - 15) + 10);

    /// <summary>Calculates a dot product between (<paramref name="x"/>, <paramref name="y"/>) and a pseudo random gradient vector.</summary>
    /// <param name="hash">The psuedo random gradient vector.</param>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    /// <returns>The calculates dot product.</returns>
    private static float Gradient(int hash, float x, float y)
    {
        var gradient = Gradients2D[hash & 7];
        return gradient.X * x + gradient.Y * y;
    }

    /// <summary>Calculates a dot product between (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) and a pseudo random gradient vector.</summary>
    /// <param name="hash">The psuedo random gradient vector.</param>
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

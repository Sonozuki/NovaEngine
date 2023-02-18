namespace NovaEngine.ContentPipeline.Readers;

/// <summary>Defines how a texture should be read from a nova file.</summary>
public class TextureReader : IContentReader
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public string Type => "texture2d";

    /// <inheritdoc/>
    public Type[] OutputTypes => new[] { typeof(Texture2D) };


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public unsafe object Read(FileStream novaFileStream, Type outputType)
    {
        using var binaryReader = new BinaryReader(novaFileStream);

        var width = (uint)binaryReader.ReadInt32();
        var height = (uint)binaryReader.ReadInt32();

        var pixels = binaryReader.ReadBytes((int)(width * height * 4));

        var pixelsArray = new Colour[height, width];
        fixed (byte* pixelsPointer = pixels)
        fixed (Colour* pixelsArrayPointer = pixelsArray)
            Buffer.MemoryCopy(pixelsPointer, pixelsArrayPointer, pixels.Length, pixels.Length);

        var texture = new Texture2D(width, height);
        texture.SetPixels(pixelsArray);
        return texture;
    }
}

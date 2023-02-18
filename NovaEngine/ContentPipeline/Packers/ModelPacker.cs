using NovaEngine.ContentPipeline.Models.Model;

namespace NovaEngine.ContentPipeline.Packers;

/// <summary>Defines how an 3d model file should be rewritten for a nova file.</summary>
public class ModelPacker : IContentPacker
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public string Type => "3dmodel";

    /// <inheritdoc/>
    public string[] Extensions => new[] { "obj" };


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public Stream Write(FileStream fileStream)
    {
        var stream = new MemoryStream();
        Serialiser.Serialise(stream, ParseObjFile(fileStream));
        return stream;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Parses an .obj file to a <see cref="MeshContent"/>.</summary>
    /// <param name="fileStream">The .obj file to parse.</param>
    private static ModelContent ParseObjFile(FileStream fileStream)
    {
        // TODO: smooth shading

        var modelContent = new ModelContent();
        var meshContent = new MeshContent();

        var vertexPositions = new List<Vector3<float>>();
        var vertexTextureCoordinates = new List<Vector2<float>>();
        var vertexNormals = new List<Vector3<float>>();

        using var reader = new StreamReader(fileStream);
        while (!reader.EndOfStream)
            ParseLine(reader.ReadLine()!, modelContent, ref meshContent, vertexPositions, vertexTextureCoordinates, vertexNormals);

        // ensure the last object gets added
        modelContent.Meshes.Add(meshContent);

        return modelContent;
    }

    /// <summary>Parses a line from an .obj file.</summary>
    /// <param name="line">The line to parse.</param>
    /// <param name="modelContent">The model content to store complete meshes on.</param>
    /// <param name="meshContent">The mesh currently being constructed.</param>
    /// <param name="vertexPositions">All the vertex positions parsed from the file.</param>
    /// <param name="vertexTextureCoordinates">All the vertex texture coordinates parsed from the file.</param>
    /// <param name="vertexNormals">All the vertex normals parsed from the file.</param>
    /// <exception cref="ContentException">Thrown if the lines failed to parse.</exception>
    private static void ParseLine(string line, ModelContent modelContent, ref MeshContent meshContent, List<Vector3<float>> vertexPositions, List<Vector2<float>> vertexTextureCoordinates, List<Vector3<float>> vertexNormals)
    {
        var tokens = line.Split(' ');
        if (tokens.Length == 0 || tokens[0].StartsWith("#", true, G11n.Culture))
            return;

        switch (tokens[0])
        {
            case "o": // object
                {
                    // ensure there is at least one face in the current mesh
                    if (meshContent.Vertices.Count == 0)
                    {
                        // reset the name to be the new mesh
                        meshContent.Name = tokens[1];
                        return;
                    }

                    modelContent.Meshes.Add(meshContent);
                    meshContent = new(tokens[1]);
                    return;
                }
            case "v": // vertex position
                {
                    var x = float.Parse(tokens[1], G11n.Culture);
                    var y = float.Parse(tokens[2], G11n.Culture);
                    var z = float.Parse(tokens[3], G11n.Culture);

                    vertexPositions.Add(new(x, y, z));
                    return;
                }
            case "vt": // texture coordinates
                {
                    var u = float.Parse(tokens[1], G11n.Culture);
                    var v = 1 - ((tokens.Length > 2) // '1 -' because .obj V coordinate is 0 at the bottom but nova has 0 at the top
                        ? float.Parse(tokens[2], G11n.Culture)
                        : 0);
                    // TODO: does obj support 3d texture coordinates?

                    vertexTextureCoordinates.Add(new(u, v));
                    return;
                }
            case "vn": // normal
                {
                    var x = float.Parse(tokens[1], G11n.Culture);
                    var y = float.Parse(tokens[2], G11n.Culture);
                    var z = float.Parse(tokens[3], G11n.Culture);

                    vertexNormals.Add(new(x, y, z));
                    return;
                }
            case "f": // face
                {
                    // ensure the face is a triangle
                    if (tokens.Length != 4) // TODO: triangulate faces
                        throw new ContentException($"Model line: \"{line}\" invalid. Faces must be triangle");

                    // split each token on the '/' character (used to separate the positions, texture coordinates, and normals)
                    var vertexDataGroups = new List<List<string>>();
                    for (var i = 0; i < 3; i++)
                        vertexDataGroups.Add(tokens[i + 1].Split('/').ToList()); // +1 to accomodate for the first 'f' token

                    // create the face
                    for (var i = 0; i < 3; i++)
                    {
                        var vertexData = vertexDataGroups[i];
                        if (vertexData.Count == 0)
                            throw new ContentException($"Model line: \"{line}\" invalid. It contains a blank vertex data token");

                        var vertex = new Vertex
                        {
                            Position = vertexPositions[int.Parse(vertexData[0], G11n.Culture) - 1]
                        };

                        // add vertex texture coordinates
                        if (vertexData.Count > 1 && !string.IsNullOrEmpty(vertexData[1])) // ensure texture coordinates aren't empty, this can be the case if only the normals are specified
                        {
                            var textureCoordinatesIndex = int.Parse(vertexData[1], G11n.Culture);
                            if (textureCoordinatesIndex < 0) // if the token is negative, convert the number to a positive
                                textureCoordinatesIndex += vertexTextureCoordinates.Count + 1;

                            vertex.TextureCoordinates = vertexTextureCoordinates[textureCoordinatesIndex - 1];
                        }

                        // add vertex normal
                        if (vertexData.Count > 2)
                        {
                            var textureNormalIndex = int.Parse(vertexData[2], G11n.Culture);
                            if (textureNormalIndex < 0) // if the token is negative, convert the number to a positive
                                textureNormalIndex += vertexNormals.Count + 1;

                            vertex.Normal = vertexNormals[textureNormalIndex - 1];
                        }

                        var index = meshContent.Vertices.IndexOf(vertex);
                        if (index != -1)
                            meshContent.Indices.Add((uint)index);
                        else
                        {
                            meshContent.Vertices.Add(vertex);
                            meshContent.Indices.Add((uint)meshContent.Vertices.Count - 1);
                        }
                    }

                    return;
                }
        }
    }
}

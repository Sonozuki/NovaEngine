using NovaEngine.Content.Models;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using NovaEngine.Serialisation;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NovaEngine.Content.Packers
{
    /// <summary>Defines how an 3d model file should be rewritten for a nova file.</summary>
    public class ModelPacker : IContentPacker
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public string Type => "3dmodel";

        /// <inheritdoc/>
        public List<string> Extensions => new() { ".obj" };


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public Stream Write(string file)
        {
            var stream = new MemoryStream();
            Serialiser.Serialise(stream, ParseObjFile(file));
            return stream;
        }

        /*********
        ** Private Methods
        *********/
        /// <summary>Parses an .obj file to a <see cref="MeshContent"/>.</summary>
        /// <param name="file">The .obj file to parse.</param>
        private static ModelContent ParseObjFile(string file)
        {
            // TODO: smooth shading

            // parse file
            var modelContent = new ModelContent();
            var meshContent = new MeshContent();

            var vertexPositions = new List<Vector3>();
            var vertexTextureCoordinates = new List<Vector2>();
            var vertexNormals = new List<Vector3>();

            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine()!;
                    var tokens = line.Split(' ');

                    // ignore lines that are blank or comments
                    if (tokens.Length == 0 || tokens[0].StartsWith("#"))
                        continue;

                    // parse lines
                    switch (tokens[0])
                    {
                        case "o": // object
                            {
                                // ensure there is at least one face in the current mesh
                                if (meshContent.Vertices.Count == 0)
                                {
                                    // reset the name to be the new mesh
                                    meshContent.Name = tokens[1];
                                    continue;
                                }

                                modelContent.Meshes.Add(meshContent);
                                meshContent = new(tokens[1]);
                                continue;
                            }
                        case "v": // vertex position
                            {
                                var x = float.Parse(tokens[1]);
                                var y = float.Parse(tokens[2]);
                                var z = float.Parse(tokens[3]);

                                vertexPositions.Add(new(x, y, z));
                                continue;
                            }
                        case "vt": // texture coordinates
                            {
                                var u = float.Parse(tokens[1]);
                                var v = 1 - ((tokens.Length > 2) // '1 -' because .obj V coordinate is 0 at the bottom but nova has 0 at the top
                                    ? float.Parse(tokens[2])
                                    : 0);
                                // TODO: does obj support 3d texture coordinates?

                                vertexTextureCoordinates.Add(new(u, v));
                                continue;
                            }
                        case "vn": // normal
                            {
                                var x = float.Parse(tokens[1]);
                                var y = float.Parse(tokens[2]);
                                var z = float.Parse(tokens[3]);

                                vertexNormals.Add(new(x, y, z));
                                continue;
                            }
                        case "f": // face
                            {
                                // ensure the face is a triangle
                                if (tokens.Length != 4) // TODO: triangulate faces
                                    throw new ContentException($"Model line: \"{line}\" invalid. Faces must be triangle");

                                // split each token on the '/' character (used to separate the positions, texture coordinates, and normals)
                                var vertexDataGroups = new List<List<string>>();
                                for (int i = 0; i < 3; i++)
                                    vertexDataGroups.Add(tokens[i + 1].Split('/').ToList()); // +1 to accomodate for the first 'f' token

                                // create the face
                                for (int i = 0; i < 3; i++)
                                {
                                    var vertexData = vertexDataGroups[i];

                                    // ensure there is at least 1 token
                                    if (vertexData.Count == 0)
                                        throw new ContentException($"Model line: \"{line}\" invalid. It contains a blank vertex data token");

                                    var vertex = new Vertex();
                                    vertex.Position = vertexPositions[int.Parse(vertexData[0]) - 1];

                                    // add vertex texture coordinates
                                    if (vertexData.Count > 1 && !string.IsNullOrEmpty(vertexData[1])) // ensure texture coordinates aren't empty, this can be the case if only the normals are specified
                                    {
                                        var textureCoordinatesIndex = int.Parse(vertexData[1]);

                                        // if the token is negative, convert the number to a positive
                                        if (textureCoordinatesIndex < 0)
                                            textureCoordinatesIndex += vertexTextureCoordinates.Count + 1;

                                        vertex.TextureCoordinates = vertexTextureCoordinates[textureCoordinatesIndex - 1];
                                    }

                                    // add vertex normal
                                    if (vertexData.Count > 2)
                                    {
                                        var textureNormalIndex = int.Parse(vertexData[2]);

                                        // if the token is negative, convert the number to a positive
                                        if (textureNormalIndex < 0)
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

                                continue;
                            }
                    }
                }

                // ensure the last object gets added
                modelContent.Meshes.Add(meshContent);
            }

            return modelContent;
        }
    }
}

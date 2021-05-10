using NovaEngine.Graphics;
using System;
using System.Collections.Generic;

namespace NovaEngine.Content.Models
{
    /// <summary>Contains the positions, texture coordinates, and normals vertex data as well as the faces that make up a mesh.</summary>
    public class MeshContent
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The unique identifier for the mesh.</summary>
        public Guid Guid { get; set; }

        /// <summary>The name of the mesh.</summary>
        public string Name { get; set; }
        
        /// <summary>The vertices of the mesh.</summary>
        public List<Vertex> Vertices { get; } = new();

        /// <summary>The indices of the mesh.</summary>
        public List<uint> Indices { get; } = new();


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance./summary>
        /// <param name="name">The name of the mesh.</param>
        public MeshContent(string name = "")
            : this(Guid.NewGuid(), name) { }

        /// <summary>Constructs an instance.</summary>
        /// <param name="guid">The unique identifier for the mesh.</param>
        /// <param name="name">The name of the mesh.</param>
        public MeshContent(Guid guid, string name)
        {
            Guid = guid;
            Name = name;
        }
    }
}

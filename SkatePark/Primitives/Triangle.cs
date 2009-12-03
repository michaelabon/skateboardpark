using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkatePark.Primitives;
using System.Diagnostics;

namespace SkatePark.Primitives
{
    /// <summary>
    /// Represents a triangle. Contains three vertices, texel coordinates, texel Material, and normal vectors (not necessarily normalized)
    /// </summary>
    public class Triangle
    {
        private int vertex1, vertex2, vertex3;
        private int texel1, texel2, texel3;
        private int normal1, normal2, normal3;

        /// <summary>
        /// The Material that will be the texture for this face
        /// </summary>
        public Material material { get; set; }

        /// <summary>
        /// Creates a Triangle as specified. Vertices must be CCW
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="texel1"></param>
        /// <param name="normal1"></param>
        /// <param name="vertex2"></param>
        /// <param name="texel2"></param>
        /// <param name="normal2"></param>
        /// <param name="vertex3"></param>
        /// <param name="texel3"></param>
        /// <param name="normal3"></param>
        /// <param name="material"></param> The texture for this Triangle
        public Triangle(int vertex1, int texel1, int normal1, int vertex2, int texel2, int normal2, int vertex3, int texel3, int normal3, Material material)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
            this.vertex3 = vertex3;
            this.texel1 = texel1;
            this.texel2 = texel2;
            this.texel3 = texel3;
            this.normal1 = normal1;
            this.normal2 = normal2;
            this.normal3 = normal3;
            Debug.Assert(normal1 == normal2);
            Debug.Assert(normal1 == normal3);
            this.material = material;
        }

        /// <summary>
        /// Creates a Triangle as specified
        /// </summary>
        /// <param name="vertex1">A string in the format "vertex/texture/normal". These index into the vertex, texture, and normal array</param> 
        /// <param name="vertex2">A string in the format "vertex/texture/normal". These index into the vertex, texture, and normal array</param>
        /// <param name="vertex3">A string in the format "vertex/texture/normal". These index into the vertex, texture, and normal array</param>
        /// <param name="material">A Material that will be the texture for this Triangle</param>
        public Triangle(string vertex1, string vertex2, string vertex3, Material material)
        {
            // vertex/texture/normal
            string[] firstVertex = vertex1.Split('/');
            string[] secondVertex = vertex2.Split('/');
            string[] thirdVertex = vertex3.Split('/');

            this.vertex1 = Convert.ToInt32(firstVertex[0]);
            this.texel1 = Convert.ToInt32(firstVertex[1]);
            this.normal1 = Convert.ToInt32(firstVertex[2]);
            this.vertex2 = Convert.ToInt32(secondVertex[0]);
            this.texel2 = Convert.ToInt32(secondVertex[1]);
            this.normal2 = Convert.ToInt32(secondVertex[2]);
            this.vertex3 = Convert.ToInt32(thirdVertex[0]);
            this.texel3 = Convert.ToInt32(thirdVertex[1]);
            this.normal3 = Convert.ToInt32(thirdVertex[2]);
            Debug.Assert(normal1 == normal2);
            Debug.Assert(normal1 == normal3);
            this.material = material;
        }

        /// <summary>
        /// Get the vertex vectors for this Triangle
        /// Note that the indices are read from and stored as 1-based indices but that OpenGL needs them as 0-based indices. This corrects that.
        /// </summary>
        /// <param name="vertexArray">The vertex array that holds coordinates for this Triangle</param> 
        /// <param name="vertexVector1">The vertex vector of the first coordinate.</param> 
        /// <param name="vertexVector2">The vertex vector of the second coordinate.</param> 
        /// <param name="vertexVector3">The vertex vector of the third coordinate.</param> 
        public void getVertices(List<Vector3f> vertexArray, out Vector3f vertexVector1, out Vector3f vertexVector2, out Vector3f vertexVector3)
        {
            vertexVector1 = vertexArray[vertex1 - 1];
            vertexVector2 = vertexArray[vertex2 - 1];
            vertexVector3 = vertexArray[vertex3 - 1];
        }

        /// <summary>
        /// Get the normal vectors for this Triangle.
        /// Note that the indices are read from and stored as 1-based indices but that OpenGL needs them as 0-based indices. This corrects that.
        /// </summary>
        /// <param name="normalArray">The normal array that holds coordinates for this Triangle</param> 
        /// <param name="normalVector1">The normal vector of the first coordinate.</param> 
        /// <param name="normalVector2">The normal vector of the second coordinate.</param> 
        /// <param name="normalVector3">The normal vector of the third coordinate.</param> 
        public void getNormals(List<Vector3f> normalArray, out Vector3f normalVector1, out Vector3f normalVector2, out Vector3f normalVector3)
        {
            normalVector1 = normalArray[normal1-1];
            normalVector2 = normalArray[normal2-1];
            normalVector3 = normalArray[normal3-1];
        }

        /// <summary>
        /// Get the texel vectors for this Triangle
        /// Note that the indices are read from and stored as 1-based indices but that OpenGL needs them as 0-based indices. This corrects that.
        /// </summary>
        /// <param name="texelArray">The texel array that holds coordinates for this Triangle</param> 
        /// <param name="texelVector1">The texel vector of the first coordinate.</param> 
        /// <param name="texelVector2">The texel vector of the second coordinate.</param> 
        /// <param name="texelVector3">The texel vector of the third coordinate.</param> 
        public void getTexels(List<Vector2f> texelArray, out Vector2f texelVector1, out Vector2f texelVector2, out Vector2f texelVector3)
        {
            texelVector1 = texelArray[texel1-1];
            texelVector2 = texelArray[texel2-1];
            texelVector3 = texelArray[texel3-1];
        }
    }
}

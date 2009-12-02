using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkatePark.Primitives;
using SkatePark.Drawables;
using Tao.OpenGl;

namespace SkatePark
{
    public abstract class ICubelet : IDrawable
    {
        protected string myType;

        public int PosX
        {
            get;
            set;
        }

        public int PosY
        {
            get;
            set;
        }
        public int Orientation { get; set; }

        public void Draw()
        {
            CubletRenderingData data = CubletWarehouse.cubletDictionary[myType];
            Vector3f normalVector1, normalVector2, normalVector3, vertex1, vertex2, vertex3;
            Vector2f texel1, texel2, texel3;

            foreach (Triangle triangle in data.triangleArray)
            {
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, triangle.material.GL_ID);
                Gl.glBegin(Gl.GL_TRIANGLES);
                triangle.getNormals(data.normalArray, out normalVector1, out normalVector2, out normalVector3);
                triangle.getTexels(data.texelArray, out texel1, out texel2, out texel3);
                triangle.getVertices(data.vertexArray, out vertex1, out vertex2, out vertex3);
                Gl.glNormal3f(normalVector1.X, normalVector1.Y, normalVector1.Z);
                Gl.glTexCoord2f(texel1.X, texel1.Y);
                Gl.glVertex3f(vertex1.X, vertex1.Y, vertex1.Z);
                Gl.glTexCoord2f(texel2.X, texel2.Y);
                Gl.glVertex3f(vertex3.X, vertex3.Y, vertex3.Z);
                Gl.glTexCoord2f(texel3.X, texel3.Y);
                Gl.glVertex3f(vertex3.X, vertex3.Y, vertex3.Z);
                Gl.glEnd();
            }
        }
    }
}

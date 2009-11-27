using System;
using Tao.OpenGl;

namespace SkatePark.Drawables
{
    public class GameBoard : IDrawable
    {
        private int maxBlockUnits;
        private int blockSize;

        public GameBoard(int maxBlockUnits, int blockSize)
        {
            this.maxBlockUnits = maxBlockUnits;
            this.blockSize = blockSize;
        }

        public bool ShowGrid { get; set; }

        public void Draw()
        {
            
            Gl.glBegin(Gl.GL_QUADS);
            {
                Gl.glColor3f(1, 0, 0);
                Gl.glVertex3f(0, 0, 0);
                Gl.glColor3f(0, 1, 0);
                Gl.glVertex3f(maxBlockUnits * blockSize, 0, 0);
                Gl.glColor3f(0, 0, 1);
                Gl.glVertex3f(maxBlockUnits * blockSize, 0 , -maxBlockUnits * blockSize);
                Gl.glColor3f(1, 0, 1);
                Gl.glVertex3f(0,0, -maxBlockUnits * blockSize);
            }
            Gl.glEnd();
        }
    }
}

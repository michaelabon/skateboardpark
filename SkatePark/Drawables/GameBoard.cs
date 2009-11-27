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
            Gl.glColor3f(1, 0, 0);
            Gl.glBegin(Gl.GL_QUADS);
            {
                Gl.glVertex3f(0, 0, 10);
                Gl.glVertex3f(maxBlockUnits * blockSize, 0, 10);
                Gl.glVertex3f(maxBlockUnits * blockSize, maxBlockUnits * blockSize, 10);
                Gl.glVertex3f(0, maxBlockUnits * blockSize, 10);
            }
            Gl.glEnd();
        }
    }
}

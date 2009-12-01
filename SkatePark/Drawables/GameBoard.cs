using System;
using Tao.OpenGl;

namespace SkatePark.Drawables
{
    public class GameBoard : IDrawable
    {
        public int BlockPixelSize { get; private set; }
        public int NumBlocks { get; private set; }

        public GameBoard(int blockPixelSize, int numBlocks)
        {
            this.BlockPixelSize = blockPixelSize;
            this.NumBlocks = numBlocks;
        }

        public bool ShowGrid { get; set; }

        public void Draw()
        {
            Gl.glBegin(Gl.GL_QUADS);
            {
                Gl.glColor3f(1, 0, 0);
                Gl.glVertex3f(0, 0, 0);
                Gl.glColor3f(0, 1, 0);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, 0, 0);
                Gl.glColor3f(0, 0, 1);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, 0 , -BlockPixelSize * NumBlocks);
                Gl.glColor3f(1, 0, 1);
                Gl.glVertex3f(0,0, -BlockPixelSize * NumBlocks);
            }
            Gl.glEnd();
        }
    }
}

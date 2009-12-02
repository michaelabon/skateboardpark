using System;
using Tao.OpenGl;

namespace SkatePark.Drawables
{
    public class GameBoard : ICubelet
    {
        public int BlockPixelSize { get; private set; }
        public int NumBlocks { get; private set; }

        public GameBoard(int blockPixelSize, int numBlocks)
        {
            this.BlockPixelSize = blockPixelSize;
            this.NumBlocks = numBlocks;
        }

        public bool ShowGrid { get; set; }

        public override void Draw()
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

        /// <summary>
        /// Draws the grids of the game board.
        /// For now, we colour each grid by its own random color.
        /// </summary>
        /// <param name="isSelectionMode">Specifies whether or not we're drawing in selection mode</param>
        public void DrawGrids(bool isSelectionMode)
        {
            int count = 0;
            if (!isSelectionMode)
            {
                Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_LINE);
                Gl.glPolygonMode(Gl.GL_BACK, Gl.GL_LINE);
            }

            for (int i = 0; i < NumBlocks * BlockPixelSize; i += BlockPixelSize)
            {
                for (int j = 0; j < NumBlocks * BlockPixelSize; j += BlockPixelSize)
                {
                    if( isSelectionMode )
                        Gl.glPushName(count);

                    Gl.glBegin(Gl.GL_QUADS);

                    Gl.glVertex3f(j, 2, -i);
                    Gl.glVertex3f(j + BlockPixelSize, 2, -i);
                    Gl.glVertex3f(j + BlockPixelSize, 2, -i - BlockPixelSize);
                    Gl.glVertex3f(j, 2, -i - BlockPixelSize);

                    Gl.glEnd();

                    if( isSelectionMode )
                        Gl.glPopName();
                    count++;
                }
            }

            if (!isSelectionMode)
            {
                Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_FILL);
                Gl.glPolygonMode(Gl.GL_BACK, Gl.GL_FILL);
            }
        }
    }
}

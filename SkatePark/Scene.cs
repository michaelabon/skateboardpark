using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using SkatePark.Drawables;

namespace SkatePark
{
    class Scene
    {
        List<IDrawable> drawables;
        GameBoard gameBoard;


        private float cameraX;
        private float cameraY;
        private float cameraZ;

        private float pitch;
        private float heading;

        public Scene()
        {
            drawables = new List<IDrawable>();
            gameBoard = new GameBoard(50, 10);
            drawables.Add(gameBoard);
        }

        internal void InitGL()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_CULL_FACE);
        }

        internal void SetView(int height, int width)
        {
            // Set viewport to window dimensions
            Gl.glViewport(0, 0, width, height);

            // Reset projection matrix stack
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            // Prevent a divide by zero
            if (height == 0)
            {
                height = 1;
            }

            // Establish clipping volume (left, right, bottom,
            // top, near, far)
            Glu.gluPerspective(45, width / height, 1, 1000);

            // reser modelview matrix stack
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        internal void RenderScene()
        {
            ClearScene();
            Gl.glPushMatrix();


            foreach (IDrawable drawableObject in drawables)
            {
                drawableObject.Draw();
            }
            Gl.glPopMatrix();
            Gl.glFlush();

            int error = Gl.glGetError();
            if (error != 0)
            {
                throw new ApplicationException("An error has occurred: " + error.ToString());
            }
        }

        private void ClearScene()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }
    }
}

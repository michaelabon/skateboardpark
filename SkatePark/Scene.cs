using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using SkatePark.Drawables;
using System.Windows.Forms;
using System.Drawing;

namespace SkatePark
{
    class Scene
    {
        List<IDrawable> drawables;
        GameBoard gameBoard;

        private int height;
        private int width;

        private float cameraX;
        private float cameraY;
        private float cameraZ;

        private float pitch;
        private float heading;

        /// <summary>
        /// Used to know if the user has the mouse down or not.
        /// </summary>
        private bool MouseIsUp { get; set; }
        private Point FirstMouseCoords { get; set; }
        /// <summary>
        /// Determines what to do when user drags mouse.
        /// </summary>
        private bool CameraMoveMode { get; set; }
        private MouseButtons PressedMouseButton { get; set; }


        public Scene()
        {
            drawables = new List<IDrawable>();
            gameBoard = new GameBoard(50, 10);
            drawables.Add(gameBoard);

            MouseIsUp = true;
            CameraMoveMode = false;

            heading = 0;
            pitch = 0;

            cameraX = 50;
            cameraY = 30;
            cameraZ = 500;
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
            this.height = height;
            this.width = width;
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
            // Test values
            
            // Camera location.
            //Glu.gluLookAt(cameraX, cameraY, cameraZ, 250, 0, -250, 0, 1, 0);
            //Gl.glTranslatef(0, 0, -50);
            

            Gl.glTranslatef(-cameraX, -cameraY, -cameraZ);
            // Camera orientation.
            Gl.glRotatef(pitch, 1, 0, 0);
            Gl.glRotatef(heading, 0.0f, 1.0f + cameraX, 0.0f);
            
            
            
            

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

        private void UpdateCameraLocationFromDrag(Point dCoords)
        {
            if (PressedMouseButton == MouseButtons.Middle)
            {
                heading += dCoords.X;
                pitch += dCoords.Y;
            }
            else if (PressedMouseButton == MouseButtons.Left)
            {
                cameraX += dCoords.X;
                cameraZ += dCoords.Y;
            }
        }

        public void onMouseDown(MouseEventArgs e)
        {
            // User has mouse on.
            MouseIsUp = false;

            // Save the first coordinates.
            FirstMouseCoords = e.Location;

            CameraMoveMode = true;
            PressedMouseButton = e.Button;
        }

        public void onMouseRelease(MouseEventArgs e)
        {
            // User doesn't have mouse down anymore.
            MouseIsUp = true;
            CameraMoveMode = false;
        }

        public void onMouseMove(MouseEventArgs e)
        {
            /**
             * Note: This is a camera movement. NOT an object movement.
             */
            // Did the user have the mouse down?
            if (!MouseIsUp && CameraMoveMode)
            {
                // We have a drag!
                Point newCoords = e.Location;

                // Compute how much the mouse moved.
                Point dCoords = new Point(FirstMouseCoords.X - e.X, FirstMouseCoords.Y - e.Y);

                // Since this is camera, we're not interested about net movement (ie from mouse down)
                FirstMouseCoords = e.Location;

                UpdateCameraLocationFromDrag(dCoords);

            }
        }
    }
}

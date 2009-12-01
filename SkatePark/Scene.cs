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
    public partial class Scene
    {
        List<IDrawable> drawables;
        GameBoard gameBoard;

        private int height;
        private int width;
        private float fovy;
        private float zFar;
        private float zNear;

        private float translateX;
        private float translateY;

        float r;


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
            pitch = 30;

            fovy = 45;
            zNear = 1;
            zFar = 10000;

            r = 500;

            translateX = 0;
            translateY = 0;
        }

        internal void InitGL()
        {
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
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
            Glu.gluPerspective(fovy, width / height, zNear, zFar);

            // reser modelview matrix stack
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        internal void RenderScene()
        {
            ClearScene();
            Gl.glPushMatrix();

            float theta = (float)((90 - pitch) * Math.PI / 180);
            float azimuth = (float)(heading * Math.PI / 180);

            float cameraZ = -(float)(r * Math.Sin(theta) * Math.Cos(azimuth));
            float cameraX = (float)(r * Math.Sin(theta) * Math.Sin(azimuth));
            float cameraY = (float)(r * Math.Cos(theta));

            Glu.gluLookAt(cameraX, cameraY, cameraZ, 0, 0, 0, 0, 1, 0);

            Gl.glTranslatef(translateX, 0, translateY);

            foreach (IDrawable drawableObject in drawables)
            {
                drawableObject.Draw();
            }

            /* Render camera lines */

            Gl.glColor3f(0, 0, 0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(cameraX, cameraY - 5, cameraZ);
            Gl.glVertex3f(0, 0, 0);
            Gl.glEnd();

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
            Console.WriteLine("H: " + heading + " P: " + pitch);
            if (PressedMouseButton == MouseButtons.Middle)
            {

                float dHeading = (float)dCoords.X / 10.0f;
                heading += dHeading;
                float dPitch = (float)dCoords.Y / 10.0f;
                pitch -= dPitch;
                
            }
            else if (PressedMouseButton == MouseButtons.Left)
            {
                // Movement is (0,0) -> (X,Y)
                // (X,Y) needs to be rotated by heading.
                float cos = (float)Math.Cos(heading * Math.PI / 180);
                float sin = (float)Math.Sin(heading * Math.PI / 180);
                float x = dCoords.X * cos - sin * dCoords.Y;
                float y = dCoords.X * sin + cos * dCoords.Y;
                translateX += x;
                translateY += y;
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

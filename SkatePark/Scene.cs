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

        private float cameraX;
        private float cameraY;
        private float cameraZ;

        private float originX, originY, originZ;

        private float upX;
        private float upY;
        private float upZ;


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

            originX = 0;
            originY = 0;
            originZ = 0;

            fovy = 45;
            zNear = 1;
            zFar = 10000;

            upX = 0;
            upY = 1;
            upZ = 0;
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

            Glu.gluLookAt(cameraX, cameraY, cameraZ, originX, originY, originZ, upX, upY, upZ);

            foreach (IDrawable drawableObject in drawables)
            {
                drawableObject.Draw();
            }

            /* Render camera lines */

            Gl.glColor3f(0, 0, 0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(cameraX, cameraY - 5, cameraZ);
            Gl.glVertex3f(originX, originY, originZ);
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
            if (PressedMouseButton == MouseButtons.Middle)
            {

                float dHeading = (float)dCoords.X / 10.0f;
                heading += dHeading;

                
                float cos = (float)Math.Cos(dHeading * Math.PI / 180);
                float sin = (float)Math.Sin(dHeading * Math.PI / 180);

                // Take X,Y to (0,0)

                float x = cameraX - originX;
                float y = cameraZ - originZ;

                cameraX = x * cos - sin * y;
                cameraZ = x * sin + cos * y;

                // Take back to whatever
                cameraX += originX;
                cameraZ += originZ;

                {

                    // Do some rotation with pitch.
                    float dPitch = (float)dCoords.Y / 10.0f;
                    pitch += dPitch;

                    float cosP = (float)Math.Cos(dPitch * Math.PI / 180);
                    float sinP = (float)Math.Sin(dPitch * Math.PI / 180);

                    // Translate origin
                    float xP = originY - cameraY;
                    float yP = originZ - cameraZ;

                    originY = xP * cosP - sinP * yP;
                    originZ = xP * sinP + cosP * yP;

                    //// Translate back
                    originY += cameraY;
                    originZ += cameraZ;

                }
                
            }
            else if (PressedMouseButton == MouseButtons.Left)
            {
                // Rotate the movement vector by the heading
                float cos = (float)Math.Cos(heading * Math.PI / 180);
                float sin = (float)Math.Sin(heading * Math.PI / 180);
                float x, y;

                x = dCoords.X * cos - sin * dCoords.Y;
                y = dCoords.X * sin + cos * dCoords.Y;


                cameraX += x;
                cameraZ += y;

                originX += x;
                originZ += y;
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

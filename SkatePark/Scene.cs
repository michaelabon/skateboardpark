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
        List<ICubelet> drawables;
        GameBoard gameBoard;

        private int height;
        private int width;
        private float fovy;
        private float zFar;
        private float zNear;

        private float cameraX, cameraY, cameraZ;
        private float translateX;
        private float translateY;
        public bool StopRender { get; set; }

        private float[] LightAmbient = { 0.5f, 0.5f, 0.5f, 1 };
        private float[] LightDiffuse = { 1, 1, 1, 1 };
        private float[] LightPosition;

        float r;

        private float pitch;
        private float heading;

        public Scene()
        {
            drawables = new List<ICubelet>();
            gameBoard = new GameBoard(50, 10);
            LightPosition = new float[] {0.5f * gameBoard.BlockPixelSize * gameBoard.NumBlocks, 0.5f * gameBoard.BlockPixelSize * gameBoard.NumBlocks, 0.5f * gameBoard.BlockPixelSize * gameBoard.NumBlocks, 1};
            drawables.Add(gameBoard);

            MouseIsUp = true;
            CurrentDragMode = DragMode.None;

            heading = 0;
            pitch = 30;

            fovy = 45;
            zNear = 1;
            zFar = 10000;

            r = 500;

            translateX = 0;
            translateY = 0;
            StopRender = false;


            SelectedCommand = ToolPanelCommand.BlockDrag;
            SelectedDragMode = DragMode.Move;
            SelectedBlockAdd = "cube";

            InitializeGridArray();

            ICubelet test = new ICubelet("cube");
            test.PosX = 0;
            test.PosY = 0;
            gridArray[0] = test;
            drawables.Add(test);
        }

        internal void InitGL()
        {
            
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            Gl.glClearDepth(1.0f);
            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glDepthFunc(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LEQUAL);
            Gl.glEnable(Gl.GL_CULL_FACE);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_DECAL);
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);					// Set Line Antialiasing

            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, this.LightAmbient);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, this.LightDiffuse);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, this.LightPosition);
            Gl.glEnable(Gl.GL_LIGHT1);
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

        private void CalculateModelView()
        {
            float theta = (float)((90 - pitch) * Math.PI / 180);
            float azimuth = (float)(heading * Math.PI / 180);

            cameraZ = -(float)(r * Math.Sin(theta) * Math.Cos(azimuth));
            cameraX = (float)(r * Math.Sin(theta) * Math.Sin(azimuth));
            cameraY = (float)(r * Math.Cos(theta));

            Glu.gluLookAt(cameraX, cameraY, cameraZ, 0, 0, 0, 0, 1, 0);

            Gl.glTranslatef(translateX, 0, translateY);
        }

        internal void RenderScene()
        {
            if(!StopRender)
            ClearScene();
            Gl.glPushMatrix();

            CalculateModelView();

            foreach (ICubelet drawableObject in drawables)
            {
                Gl.glPushMatrix();
                Gl.glTranslatef(drawableObject.PosX * gameBoard.BlockPixelSize, 0, -drawableObject.PosY * gameBoard.BlockPixelSize);

                // Create scale.
                float scaleFactor = gameBoard.BlockPixelSize / 10.0f;

                if(drawableObject != gameBoard)
                    Gl.glScalef(scaleFactor, scaleFactor * 1.5f, scaleFactor);

                drawableObject.Draw();
                Gl.glPopMatrix();
            }
            
            // Render grids for debug
            gameBoard.DrawGrids(false);

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
                Console.WriteLine("An error has occurred: " + error.ToString());
            }

           // StopRender = true;
        }

        private void ClearScene()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }

        private void UpdateCameraLocationFromDrag(Point dCoords)
        {
            //Console.WriteLine("H: " + heading + " P: " + pitch);
            if (PressedMouseButton == MouseButtons.Middle)
            {
                if (Control.ModifierKeys == Keys.Shift)
                {
                    dCoords = PanCamera(dCoords);
                }
                else
                {
                    dCoords = RotateCamera(dCoords);
                }
            }
        }

        private Point PanCamera(Point dCoords)
        {
            // Movement is (0,0) -> (X,Y)
            // (X,Y) needs to be rotated by heading.
            float cos = (float)Math.Cos(heading * Math.PI / 180);
            float sin = (float)Math.Sin(heading * Math.PI / 180);
            float x = dCoords.X * cos - sin * dCoords.Y;
            float y = dCoords.X * sin + cos * dCoords.Y;
            translateX += x;
            translateY += y;
            return dCoords;
        }

        private Point RotateCamera(Point dCoords)
        {
            float dHeading = -(float)dCoords.X / 10.0f;
            heading += dHeading;
            float dPitch = (float)dCoords.Y / 10.0f;
            pitch -= dPitch;
            return dCoords;
        }

        /// <summary>
        /// Finds out whether the user clicked anything on the screen.
        /// Returns true if the user clicked on a grid, false otherwise.
        /// </summary>
        private int IntersectMouse(bool drawCubelets)
        {

            // Draw all fake grids.
            // Each grid is maxBlockUnits*maxBlockUnit
            int[] buffer = new int[512];
            int[] viewport = new int[4];

            Gl.glSelectBuffer(512, buffer);
            Gl.glGetIntegerv(Gl.GL_VIEWPORT, viewport);

            Gl.glRenderMode(Gl.GL_SELECT);
            Gl.glInitNames();
            
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluPickMatrix(FirstMouseCoords.X, viewport[3] - FirstMouseCoords.Y - 40, 1, 1, viewport);
            Glu.gluPerspective(fovy, width / height, zNear, zFar);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glPushMatrix();
            CalculateModelView();
            ClearScene();
            gameBoard.DrawGrids(true);

            int hits =0;
            // restore
            SetView(height,width);

            Gl.glPopMatrix();
            Gl.glFlush();

            hits = Gl.glRenderMode(Gl.GL_RENDER);
            

            if (hits > 0)
            {
                return ProcessHits(hits, buffer);
            }
            else
                return -1;
        }

        private int ProcessHits(int hits, int[] buffer)
        {
            if (hits > 2)
            {
                // To cut down complexity, only allow user to click one grid at a time.
                return -1;
            }

            int numHits = buffer[0];
            if (numHits != 1)
            {
                // Same as above. Cutting down complexity.
                return -1;
            }

            int objectName = buffer[3];
            return objectName;
        }
    }
}

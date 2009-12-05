using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using SkatePark.Drawables;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

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

        private float[] LightAmbient = { 0.2f, 0.5f, 0.5f, 1 };
        private float[] LightDiffuse = { 0.2f, 0.2f, 0.2f, 1 };
        private float[] LightPosition;

        private float[] light1_ambient = { 0.2f, 0.2f, 0.2f, 1 };
        private float[] light1_diffuse = { 1.0f, 1.0f, 1.0f, 1 };
        private float[] light1_specular = { 1.0f, 1.0f, 1.0f, 1 };
        private float[] light1_position = { -2.0f, 20.0f, 1.0f, 1 };
        private float[] spot_direction = { -1.0f, -1.0f, 0 };

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


            SelectedCommand = ToolPanelCommand.None;
            SelectedDragMode = DragMode.None;
            SelectedBlockAdd = "cube";

            InitializeGridArray();
        }

        internal void InitGL()
        {
            
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            Gl.glClearDepth(1.0f);
            Gl.glShadeModel(Gl.GL_SMOOTH);
            //Gl.glDepthFunc(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LEQUAL);
            Gl.glEnable(Gl.GL_CULL_FACE);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_DECAL);
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);					// Set Line Antialiasing

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, this.LightAmbient);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, this.LightDiffuse);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, this.LightPosition);
            Gl.glEnable(Gl.GL_LIGHT0);

            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, light1_ambient);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, light1_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPECULAR, light1_specular);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, light1_position);
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_CONSTANT_ATTENUATION, 1.5f);
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_LINEAR_ATTENUATION, 0.5f);
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_QUADRATIC_ATTENUATION, 0.2f);
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_CUTOFF, 45.0f);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPOT_DIRECTION, spot_direction);
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_EXPONENT, 2.0f);
            Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glEnable(Gl.GL_LIGHTING);
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

        private void RenderCubelets(bool isSelection)
        {
            int i = 0;
            foreach (ICubelet drawableObject in drawables)
            {
                if (isSelection)
                {
                    if (drawableObject == gameBoard)
                        continue;

                    Gl.glPushName(gameBoard.NumBlocks * gameBoard.NumBlocks + i);
                }
                Gl.glPushMatrix();
                Gl.glTranslatef(drawableObject.PosX * gameBoard.BlockPixelSize, 0, -drawableObject.PosY * gameBoard.BlockPixelSize);

                // Create scale.
                float scaleFactor = gameBoard.BlockPixelSize / 10.0f;

                if (drawableObject != gameBoard)
                {
                    // Rotate
                    Gl.glTranslatef(gameBoard.BlockPixelSize / 2.0f, 0, -gameBoard.BlockPixelSize / 2.0f);
                    
                    Gl.glRotatef(drawableObject.Orientation, 0, 1, 0);
                    Gl.glTranslatef(-gameBoard.BlockPixelSize / 2.0f, 0, gameBoard.BlockPixelSize / 2.0f);
                    


                    Gl.glScalef(scaleFactor, scaleFactor * 1.5f, scaleFactor);
                }

                drawableObject.Draw();
                Gl.glPopMatrix();

                if (isSelection)
                {
                    Gl.glPopName();
                    i++;
                }
            }
        }

        internal void RenderScene()
        {
            if (!StopRender)
            { ClearScene(); }
            Gl.glPushMatrix();

            CalculateModelView();

            // Render everything
            RenderCubelets(false);
            
            
            // Render grids for debug
            Gl.glColor3f(1, 1, 1);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            gameBoard.DrawGrids(false);
            Gl.glEnable(Gl.GL_TEXTURE_2D);

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

            if (drawCubelets)
                RenderCubelets(true);

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
            int minimumZ = Int32.MaxValue, minimumZIndex = Int32.MaxValue;
            bool found = false;
            for (int i = 0; i < hits;)
            {
                if (buffer[i] > 0)
                {
                    // We have names.
                    if (buffer[i + 1] < minimumZ)
                    {
                        minimumZ = buffer[i + 1];
                        minimumZIndex = i;
                    }

                    i += buffer[i];

                    found = true;
                }
                i += 3;
            }

            // We have minimum index. Get the first hit.
            if (found)
            {
                return buffer[minimumZIndex + 3];
            }
            else
                return -1; // No name
        }

        public void SaveScene()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = dialog.FileName;

                WriteSceneToBuffer(new FileStream(filepath, FileMode.Create));
            }
        }

        public void LoadScene()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = dialog.FileName;

                LoadSceneFromBuffer(new FileStream(filepath, FileMode.Open, FileAccess.Read));
            }
        }

        private void WriteSceneToBuffer(Stream buff)
        {
            BinaryWriter write = new BinaryWriter(buff);

            foreach (ICubelet cube in drawables)
            {
                if (cube == gameBoard)
                    continue;
                // Write name
                write.Write(cube.MyType);

                write.Write((byte)0);

                // Write PosX.
                write.Write(cube.PosX);
                write.Write((byte)0);

                // Write PosY
                write.Write(cube.PosY);
                write.Write((byte)0);

                // Write orientation
                write.Write(cube.Orientation);
                write.Write((byte)0);
            }

            write.Close();
        }

        private void LoadSceneFromBuffer(Stream buff)
        {

            BinaryReader read = new BinaryReader(buff);

            // Clear the current models.
            if (MessageBox.Show("Warning: Loading a new scene will delete all changes made to the current scene. Are you sure?", "Are you Sure?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                buff.Close();
                return;
            }

            drawables.Clear();
            drawables.Add(gameBoard);
            InitializeGridArray();

            while (read.PeekChar() > 0)
            {
                string type = read.ReadString();
                ICubelet cube = new ICubelet(type);

                read.ReadByte();

                cube.PosX = read.ReadInt32();
                read.ReadByte();

                cube.PosY = read.ReadInt32();

                read.ReadByte();

                cube.Orientation = read.ReadInt32();

                read.ReadByte();


                // Add to drawables
                drawables.Add(cube);
                // Add to array
                gridArray[cube.PosX + gameBoard.NumBlocks * cube.PosY] = cube;
            }

            buff.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkatePark
{
    public partial class Scene
    {

        public float[][] getPixelSpaceToWorldSpaceMatrix()
        {
            /*
             * (1/a | 0 | 0 | 0
                0 | 1/b | 0 | 0
                0 | 0 | 0 | -1
                0 | 0 | 1/d | c/d)
             */

            float f = 1 / (float)Math.Tan(fovy/2);
            float aspect = width/height;
            
            float a = f / aspect;

            float b = f;
            float c = (zFar + zNear) / (zNear-zFar);
            float d = (2 * zFar * zNear) / (zNear - zFar);

            //float[][] ret = { { 1 / a, 0, 0, 0 }, { 0, 1 / b, 0, 0 }, { 0, 0, 0, -1 }, { 0, 0, 1 / d, c / d } };

            return null;
        }

        public float[] getWorldSpaceCoords(float x, float y)
        {
            return null;
        }

        public void onMouseWheel(MouseEventArgs e)
        {
            // Find the camera-origin direction vector.
            float dirX = cameraX - originX;
            float dirY = cameraY - originY;
            float dirZ = cameraZ - originZ;

            // Normalize it
            float mag = (float)Math.Sqrt(Math.Pow(dirX, 2) + Math.Pow(dirY, 2) + Math.Pow(dirZ, 2));
            dirX /= mag;
            dirY /= mag;
            dirZ /= mag;

            // Find the current parameter
            float s = (cameraX - originX) / dirX;
            
            s += (e.Delta > 0 ? -20 : 20);

            // Get the new vector.

            float newX = originX + s * dirX;
            float newY = originY + s * dirY;
            float newZ = originZ + s * dirZ;

            cameraX = newX;
            cameraY = newY;
            cameraZ = newZ;
        }

    }
}

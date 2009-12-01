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
            
            r += (e.Delta > 0 ? -20 : 20);


        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkatePark.Primitives
{
    /// <summary>
    /// Represents 3 floats.
    /// </summary>
    class Vector3f
    {
        /// <summary>
        /// Defaults all three component magnitudes to zero.
        /// </summary>
        public Vector3f()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        /// <summary>
        /// Creates a new Vector3f with component magnitudes as described
        /// </summary>
        /// <param name="x">the magnitude in the first direction</param> 
        /// <param name="y">the magnitude in the second direction</param> 
        /// <param name="z">the magnitude in the third direction</param> 
        public Vector3f(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Creates a new Vector3f with component magnitudes as described. Parameters will be converted to floats for you.
        /// </summary>
        /// <param name="x">the magnitude in the first direction</param> 
        /// <param name="y">the magnitude in the second direction</param> 
        /// <param name="z">the magnitude in the third direction</param> 
        public Vector3f(string x, string y, string z)
        {
            this.X = Convert.ToSingle(x);
            this.Y = Convert.ToSingle(y);
            this.Z = Convert.ToSingle(z);
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float this[int pos]
        {
            get
            {
                switch (pos)
                {
                    case 0: return this.X;
                    case 1: return this.Y;
                    case 2: return this.Z;
                    default: throw new IndexOutOfRangeException("Only 0, 1, and 2 are valid indexes");
                }
            }
            set
            {
                switch (pos)
                {
                    case 0: this.X = value; break;
                    case 1: this.Y = value; break;
                    case 2: this.Z = value; break;
                    default: throw new IndexOutOfRangeException("Only 0, 1, and 2 are valid indexes");
                }
            }
        }

    }
}

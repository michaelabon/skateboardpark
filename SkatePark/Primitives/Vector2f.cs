using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkatePark.Primitives
{
    /// <summary>
    /// Represents 2 floats
    /// </summary>
    class Vector2f
    {
        /// <summary>
        /// Defaults both component magnitudes to zero.
        /// </summary>
        public Vector2f()
        {
            this.X = 0;
            this.Y = 0;
        }

        /// <summary>
        /// Creates a new Vector2f with component magnitudes as described
        /// </summary>
        /// <param name="x">the magnitude in the first direction</param>
        /// <param name="y">the magnitude in the second direction</param>
        public Vector2f(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Creates a new Vector2f with component magnitudes as described. Parameters will be converted to floats for you.
        /// </summary>
        /// <param name="x">the magnitude in the first direction</param>
        /// <param name="y">the magnitude in the second direction</param>
        public Vector2f(string x, string y)
        {
            this.X = Convert.ToSingle(x);
            this.Y = Convert.ToSingle(y);
        }

        float X { get; set; }
        float Y { get; set; }

        public float this[int pos]
        {
            get
            {
                switch (pos)
                {
                    case 0: return this.X;
                    case 1: return this.Y;
                    default: throw new IndexOutOfRangeException("Only 0 and 1 are valid indexes");
                }
            }
            set
            {
                switch (pos)
                {
                    case 0: this.X = value; break;
                    case 1: this.Y = value; break;
                }
            }
        }
    }
}

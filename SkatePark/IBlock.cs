using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkatePark
{
    public abstract class IBlock
    {

        public int PosX
        {
            get;
            set;
        }

        public int PosY
        {
            get;
            set;
        }
        public int Orientation { get; set; }

        public abstract void Draw();


    }
}

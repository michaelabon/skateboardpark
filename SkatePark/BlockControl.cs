using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkatePark
{
    public partial class Scene
    {
        private void OnBlockSelected(int blockNum)
        {
            Console.WriteLine("Block number: " + blockNum + " selected.");
        }
    }
}

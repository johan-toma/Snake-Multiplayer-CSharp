using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame6
{
    class Utility
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public enum Direction { Right, Left, Up, Down };

        public Utility()
        {
            Width = 10;
            Height = 10;
        }
    }
}

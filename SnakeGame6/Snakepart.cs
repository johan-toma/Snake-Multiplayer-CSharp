using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame6
{
    class Snakepart
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Snakepart()
        {
            X = 0;
            Y = 0;
        }

        public Snakepart(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Snakepart(int xy)
        {
            X = xy;
            Y = xy;
        }
    }
}

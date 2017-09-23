using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_Your_Way
{
    [Serializable]
    public class Obstacle
    {
        public int x, y, width, height;

        public Obstacle(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}

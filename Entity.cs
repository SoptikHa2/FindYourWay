using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_Your_Way
{
    [Serializable]
    public class Entity
    {
        public float x, y;
        public float nextX, nextY;
        public int[] data;
        [NonSerialized]
        public bool alive = true;
        [NonSerialized]
        public int fitness = 0;

        public Entity(float x, float y, int dataLength = 1000, float nextX = 0, float nextY = 0)
        {
            this.x = x;
            this.y = y;
            this.data = GenRandomData(dataLength);
            this.nextX = nextX;
            this.nextY = nextY;
        }

        private Entity(float x, float y, int[] data, float nextX = 0, float nextY = 0)
        {
            this.x = x;
            this.y = y;
            this.data = data;
            this.nextX = nextX;
            this.nextY = nextY;
        }

        private int[] GenRandomData(int length, int from = -1, int to = 1)
        {
            int[] d = new int[length];
            for (int i = 0; i < length; i++)
            {
                d[i] = Game.currentGame.rnd.Next(from, to + 1);
            }
            return d;
        }

        private Entity Breed(Entity partner, int mutationFrom = -1, int mutationTo = 1)
        {
            int[] newData = new int[data.Length];
            int line = Game.currentGame.rnd.Next(1, data.Length);
            int i;
            for (i = 0; i < line; i++)
            {
                if (Game.currentGame.rnd.Next(101) <= Game.currentGame.mutationChance)
                    newData[i] = Game.currentGame.rnd.Next(mutationFrom, mutationTo + 1);
                else
                    newData[i] = data[i];
            }
            for (; i < data.Length; i++)
            {
                if (Game.currentGame.rnd.Next(101) <= Game.currentGame.mutationChance)
                    newData[i] = Game.currentGame.rnd.Next(mutationFrom, mutationTo + 1);
                else
                    newData[i] = partner.data[i];
            }

            return new Entity(x, y, newData);
        }

        public void Move(int frame)
        {
            if (!alive)
                return;

            int c = data[frame];

            float change = 0.3f;
            float smallChange = 0.2f;
            float max = 1f;

            if (c == 1)
            {
                if (nextY < 0f)
                {
                    nextY += change;
                    if (nextX < max)
                        nextX += change;
                }
                else
                {
                    if (nextY < max)
                        nextY += change;
                    if (nextX > 0f)
                        nextX -= change;
                }
            }
            else if (c == -1)
            {
                if (nextY > 0)
                {
                    nextY -= change;
                    if (nextX < max)
                        nextX += change;
                }
                else
                {
                    if (nextY > -max)
                        nextY -= change;
                    if (nextX > 0f)
                        nextX -= change;
                }
            }
            else
            {
                if (nextX < max)
                    nextX += smallChange;
                if (nextY < 0f)
                    nextY += smallChange;
                else if (nextY > 0f)
                    nextY -= smallChange;
            }



            x += nextX * Game.currentGame.multiplier;
            y += nextY * Game.currentGame.multiplier;


            if (x < 0 || y < 0 || y > 600)
            {
                alive = false;
                return;
            }
            else if (x > 500)
            {
                // WON
                alive = false;
                return;
            }
            else
            {
                foreach (Obstacle o in Game.currentGame.obstacles)
                {
                    if(x >= o.x && x <= o.x + o.width &&
                            y >= o.y && y <= o.y + o.height)
                    {
                        alive = false;
                        return;
                    }
                }
            }
        }
    }
}

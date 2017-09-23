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
        public int[] dataX, dataY;
        [NonSerialized]
        private bool alive = true;

        public Entity(float x, float y, int dataLength = 1000, float nextX = 0, float nextY = 0)
        {
            this.x = x;
            this.y = y;
            this.dataX = GenRandomData(dataLength);
            this.dataY = GenRandomData(dataLength);
            this.nextX = nextX;
            this.nextY = nextY;
        }

        private Entity(float x, float y, int[] dataX, int[] dataY, float nextX = 0, float nextY = 0)
        {
            this.x = x;
            this.y = y;
            this.dataX = dataX;
            this.dataY = dataY;
            this.nextX = nextX;
            this.nextY = nextY;
        }

        private int[] GenRandomData(int length, int from = -10, int to = 10)
        {
            int[] d = new int[length];
            for (int i = 0; i < length; i++)
            {
                d[i] = Game.currentGame.rnd.Next(from, to + 1);
            }
            return d;
        }

        private Entity Breed(Entity partner, int mutationFrom = -10, int mutationTo = 10)
        {
            int[] newDataX = new int[dataX.Length];
            int line = Game.currentGame.rnd.Next(1, dataX.Length);
            int i;
            for (i = 0; i < line; i++)
            {
                if (Game.currentGame.rnd.Next(101) <= Game.currentGame.mutationChance)
                    newDataX[i] = Game.currentGame.rnd.Next(mutationFrom, mutationTo + 1);
                else
                    newDataX[i] = dataX[i];
            }
            for (; i < dataX.Length; i++)
            {
                if (Game.currentGame.rnd.Next(101) <= Game.currentGame.mutationChance)
                    newDataX[i] = Game.currentGame.rnd.Next(mutationFrom, mutationTo + 1);
                else
                    newDataX[i] = partner.dataX[i];
            }

            int[] newDataY = new int[dataX.Length];
            line = Game.currentGame.rnd.Next(1, dataX.Length);
            i = 0;
            for (i = 0; i < line; i++)
            {
                if (Game.currentGame.rnd.Next(101) <= Game.currentGame.mutationChance)
                    newDataX[i] = Game.currentGame.rnd.Next(mutationFrom, mutationTo + 1);
                else
                    newDataX[i] = dataX[i];
            }
            for (; i < dataX.Length; i++)
            {
                if (Game.currentGame.rnd.Next(101) <= Game.currentGame.mutationChance)
                    newDataX[i] = Game.currentGame.rnd.Next(mutationFrom, mutationTo + 1);
                else
                    newDataX[i] = partner.dataX[i];
            }

            return new Entity(x, y, newDataX, newDataY);
        }

        public void Move(int frame)
        {
            if (!alive)
                return;

            int cx = dataX[frame];
            int cy = dataY[frame];

            nextX += cx;
            if (nextX > 1.2f)
                nextX = 1.2f;
            else if (nextX < -1.2f)
                nextX = -1.2f;
            nextY += cy;
            if (nextY > 1.2f)
                nextY = 1.2f;
            else if (nextY < -1.2f)
                nextY = -1.2f;

            // TODO: Collisions
            // TODO: If |addition| > 10, split them and add part of it and test collision repeatedly
            x += nextX * Game.currentGame.multiplier;
            y += nextY * Game.currentGame.multiplier;
        }
    }
}

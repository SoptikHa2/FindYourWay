using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_Your_Way
{
    [Serializable]
    public class Game
    {
        private static Pen obstacleBrush = new Pen(new SolidBrush(Color.Black));
        private static SolidBrush entityBrush = new SolidBrush(Color.Red);

        public static Game currentGame { get; private set; }

        public Random rnd { get; private set; }
        public Obstacle[] obstacles;
        public Entity[] entities;
        public int numberOfEntities { get; private set; }
        public int mutationChance { get; private set; }
        public float multiplier = 1;
        private int genomLength;

        public Game(Obstacle[] innerObstacles, int genomLength = 1000, int numberOfEntities = 1000, int mutationChance = 2)
        {
            rnd = new Random();
            this.obstacles = innerObstacles;
            this.numberOfEntities = numberOfEntities;
            this.mutationChance = mutationChance;
            this.genomLength = genomLength;
            SetGame();
            Generate();
        }

                                                        // Have to use useSeed, constructors should be different (without it, how would you tell, what constructor am I using from code [new Game(new Obstacle[0], 2, 2, 2)] ?
        public Game(Obstacle[] innerObstacles, int seed, bool useSeed = true, int genomLength = 1000, int numberOfEntities = 1000, int mutationChance = 2)
        {
            if (useSeed)
                rnd = new Random(seed);
            else
                rnd = new Random(seed);
            this.obstacles = innerObstacles;
            this.numberOfEntities = numberOfEntities;
            this.mutationChance = mutationChance;
            this.genomLength = genomLength;
            SetGame();
            Generate();
        }

        public void SetGame()
        {
            currentGame = this;
        }

        private void Generate(int x = 0, int y = 250)
        {
            entities = new Entity[numberOfEntities];
            for (int i = 0; i < numberOfEntities; i++)
            {
                entities[i] = new Entity(x, y, genomLength, 0.7f, 0f);
            }
        }

        public void Draw(Graphics g)
        {
            foreach (Obstacle o in obstacles)
            {
                g.DrawRectangle(obstacleBrush, o.x, o.y, o.width, o.height);
            }
            foreach (Entity e in entities)
            {
                g.FillRectangle(entityBrush, e.x - 3, e.y - 3, 3, 3);
            }
        }

        public void Move(int frame)
        {
            foreach (Entity e in entities)
            {
                e.Move(frame);
            }
        }
    }
}

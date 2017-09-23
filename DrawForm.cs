﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Find_Your_Way
{
    public partial class DrawForm : Form
    {
        const int maxTicks = 2000;
        const int movesPerFrame = 5;
        const int numberOfEntities = 500;

        private Graphics graphics;
        private Timer timer;
        private int ticks = 0;
        private ulong generation = 1;
        private SolidBrush textBrush = new SolidBrush(Color.Black);
        private static readonly object locker = new object();
        private bool isEditMode = false;
        public DrawForm()
        {
            InitializeComponent();
            Game game = new Game(new Obstacle[] { new Obstacle(50, 220, 10, 60), new Obstacle(100, 180, 10, 60), new Obstacle(100, 290, 10, 60) }, maxTicks, numberOfEntities);
            graphics = CreateGraphics();
            timer = new Timer();
            timer.Interval = 15;
            timer.Tick += Timer_Tick;
            //timer.Start();
            nextgen.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            graphics.Clear(BackColor);
            graphics.DrawString($"Ticks: {ticks} / {maxTicks}     Generation: {generation}", new Font("Arial", 11), textBrush, new PointF(0, 0));
            Game.currentGame.Draw(graphics);
            for (int i = 0; i < movesPerFrame; i++)
                Game.currentGame.Move(ticks);
            ticks += 1 * movesPerFrame;
            if (ticks >= maxTicks)
            {
                timer.Stop();
                ticks = 0;
                if (!checkbox_autoNextGen.Checked)
                {
                    nextgen.Enabled = true;
                    setObstacles.Enabled = true;
                }
                else
                    StartNextGen();
            }
            else if (Game.currentGame.entities.Where(x => x.alive).Count() == 0)
            {
                timer.Stop();
                ticks = 0;
                if (!checkbox_autoNextGen.Checked)
                {
                    nextgen.Enabled = true;
                    setObstacles.Enabled = true;
                }
                else
                    StartNextGen();
            }
        }

        private void DrawForm_Paint(object sender, PaintEventArgs e)
        {
            Game.currentGame.Draw(graphics);
        }

        private void DrawForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void nextgen_Click(object sender, EventArgs e)
        {
            StartNextGen();
        }

        private void checkbox_autoNextGen_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox_autoNextGen.Checked)
            {
                nextgen.Enabled = false;
                if (!timer.Enabled)
                {
                    StartNextGen();
                }
            }
        }

        private void StartNextGen()
        {
            lock (locker)
            {
                if (!timer.Enabled)
                {
                    nextgen.Enabled = false;
                    setObstacles.Enabled = false;

                    List<Entity> ent = Game.currentGame.entities.ToList();

                    // Comp fitness
                    for (int i = 0; i < ent.Count; i++)
                        ent[i].CalcFitnessAndReset();
                    // Order
                    ent = ent.OrderBy(x => x.fitness).ToList();
                    // Remove last half
                    ent.RemoveRange(0, numberOfEntities / 2);

                    for (int i = 0; i < ent.Count; i++)
                        if (ent[i].fitness < 1)
                            ent[i].fitness = 1;
                    // Breed
                    List<Entity> newOnes = new List<Entity>();
                    while(newOnes.Count() < ent.Count)
                    {
                        Entity parent1 = null;
                        Entity parent2 = null;

                        int sum = ent.Select(x => x.fitness).Sum();
                        int random = Game.currentGame.rnd.Next(0, sum);

                        for(int i = 0; i < ent.Count(); i++)
                        {
                            if(random < ent[i].fitness)
                            {
                                parent1 = ent[i];
                                break;
                            }
                            random -= ent[i].fitness;
                        }
                        for (int i = 0; i < ent.Count(); i++)
                        {
                            if (random < ent[i].fitness)
                            {
                                parent2 = ent[i];
                                break;
                            }
                            random -= ent[i].fitness;
                        }

                        newOnes.Add(parent1.Breed(parent2));
                    }
                    ent.AddRange(newOnes);
                    Game.currentGame.entities = ent.ToArray();

                    generation++;
                    timer.Start();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Stop();
            checkbox_autoNextGen.Checked = false;
            isEditMode = !isEditMode;
            checkbox_autoNextGen.Enabled = !isEditMode;
            nextgen.Enabled = !isEditMode;
        }

        int obstacleX = -1;
        int obstacleY = -1;
        private void DrawForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (isEditMode)
            {
                int x = e.X;
                int y = e.Y;
                if (x > 400 || y > 560)
                    return;

                // If clicked inside obstacle, remove it
                bool clickedInside = false;
                for(int i = 0; i < Game.currentGame.obstacles.Length; i++)
                {
                    Obstacle o = Game.currentGame.obstacles[i];
                    if(x >= o.x && x <= o.x + o.width &&
                        y >= o.y && y <= o.y + o.height)
                    {
                        clickedInside = true;
                        List<Obstacle> newList = Game.currentGame.obstacles.ToList();
                        newList.RemoveAt(i);
                        Game.currentGame.obstacles = newList.ToArray();
                        obstacleX = -1;
                        obstacleY = -1;
                        graphics.Clear(BackColor);
                        Game.currentGame.Draw(graphics);
                        break;
                    }
                }
                if (!clickedInside)
                {
                    if(obstacleX == -1)
                    {
                        obstacleX = x;
                        obstacleY = y;
                    }
                    else
                    {
                        int obsX = Math.Min(obstacleX, x);
                        int obsY = Math.Min(obstacleY, y);
                        int width = Math.Abs(obstacleX - x);
                        int height = Math.Abs(obstacleY - y);
                        List<Obstacle> newList = Game.currentGame.obstacles.ToList();
                        newList.Add(new Obstacle(obsX, obsY, width, height));
                        Game.currentGame.obstacles = newList.ToArray();
                        obstacleX = -1;
                        obstacleY = -1;
                        graphics.Clear(BackColor);
                        Game.currentGame.Draw(graphics);
                    }
                }
            }
        }
    }
}

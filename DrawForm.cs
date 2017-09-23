using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Find_Your_Way
{
    public partial class DrawForm : Form
    {
        const int maxTicks = 2000;
        int movesPerFrame = 5;
        const int numberOfEntities = 500;

        private Graphics graphics;
        private Timer timer;
        private int ticks = 0;
        private ulong generation = 1;
        private SolidBrush textBrush = new SolidBrush(Color.Black);
        private SolidBrush buildBrush = new SolidBrush(Color.Blue);
        private static readonly object locker = new object();
        private bool isEditMode = false;
        private bool forceQuit = true;

        public DrawForm(bool load = false)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Game game = null;
            if (!load)
                game = new Game(new Obstacle[] { new Obstacle(50, 220, 10, 60), new Obstacle(100, 180, 10, 60), new Obstacle(100, 290, 10, 60) }, maxTicks, numberOfEntities, 5);
            else
            {
                try
                {
                    using (OpenFileDialog dialog = new OpenFileDialog())
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            string name = dialog.FileName;
                            string text = File.ReadAllText(name);
                            try
                            {
                                Game.currentGame = Serializer.DeserializeObject(text) as Game;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Failed to load save. File is corrupted.", "Failed to load");
                                throw ex;
                            }
                        }
                        else
                            throw new Exception("User canceled file load");
                    }
                }
                catch
                {
                    Form1.thisForm.Show();
                    forceQuit = false;
                    Close();
                    return;
                }
            }
            graphics = CreateGraphics();
            timer = new Timer();
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            nextgen.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            graphics.Clear(BackColor);
            graphics.DrawString($"Ticks: {ticks} / {maxTicks}     Generation: {generation}", new Font("Arial", 11), textBrush, new PointF(0, 0));
            Game.currentGame.Draw(graphics);
            for (int i = 0; i < movesPerFrame; i++)
            {
                Game.currentGame.Move(ticks);
                ticks++;
                if (ticks >= maxTicks)
                {
                    timer.Stop();
                    ticks = 0;
                    if (!checkbox_autoNextGen.Checked)
                    {
                        nextgen.Enabled = true;
                        setObstacles.Enabled = true;
                        reset_entities.Enabled = true;
                        save.Enabled = true;
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
                        reset_entities.Enabled = true;
                        save.Enabled = true;
                    }
                    else
                        StartNextGen();
                }
            }
        }

        private void DrawForm_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Game.currentGame.Draw(graphics);
            }
            catch { }
        }

        private void DrawForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (forceQuit)
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
                    save.Enabled = false;
                    reset_entities.Enabled = false;

                    // Change moves:draw ration
                    string text = movesPerDraw.Text;
                    int mpd = 5;
                    if (int.TryParse(text, out mpd))
                    {
                        if (mpd >= 1)
                            if (mpd > 300)
                            {
                                movesPerDraw.Text = "300";
                                movesPerFrame = 300;
                            }
                            else
                                movesPerFrame = mpd;
                        else
                        {
                            movesPerDraw.Text = "1";
                            movesPerFrame = 1;
                        }
                    }
                    else
                        movesPerDraw.Text = movesPerFrame.ToString();

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
                    while (newOnes.Count() < ent.Count)
                    {
                        Entity parent1 = null;
                        Entity parent2 = null;

                        int sum = ent.Select(x => x.fitness).Sum();
                        int random = Game.currentGame.rnd.Next(0, sum);

                        for (int i = 0; i < ent.Count(); i++)
                        {
                            if (random < ent[i].fitness)
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

        /// <summary>
        /// Edit obstacles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (x > 500 || y > 560)
                    return;

                // If clicked inside obstacle, remove it
                bool clickedInside = false;
                for (int i = 0; i < Game.currentGame.obstacles.Length; i++)
                {
                    Obstacle o = Game.currentGame.obstacles[i];
                    if (x >= o.x && x <= o.x + o.width &&
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
                    if (obstacleX == -1)
                    {
                        obstacleX = x;
                        obstacleY = y;
                        graphics.FillRectangle(buildBrush, x - 3, y - 3, 6, 6);
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

        private void reset_entities_Click(object sender, EventArgs e)
        {
            if (generation > 1)
            {
                if (MessageBox.Show("Are you sure? That'll erase everything that your entities learned and "
                     + "death of " + (numberOfEntities / 2) * generation + " entities you killed will become unnecessary", "Kill ALL entities?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    generation = 1;
                    Game.currentGame.GenerateEntities();
                }
            }

        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                string save = Serializer.SerializeObject(Game.currentGame);

                SaveFileDialog savefile = new SaveFileDialog();
                // set a default file name
                savefile.FileName = $"save-{DateTime.Now.ToShortDateString()}-{DateTime.Now.ToShortTimeString().Replace(':', '.')}.fyw";
                // set filters - this can be done in properties as well
                savefile.Filter = "Find Your Way file (*.fyw)|*.fyw|All files (*.*)|*.*";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(savefile.FileName))
                        sw.WriteLine(save);
                }
            }
            catch
            {
                MessageBox.Show("Couldn't save file, something bad happened", "Error");
            }
        }

        private void delay_TextChanged(object sender, EventArgs e)
        {
            int c = 0;
            if (int.TryParse(delay.Text, out c))
            {
                if (c < 15)
                {
                }
                else if (c > 100)
                {
                }
                else
                    timer.Interval = c;
            }
            else
                delay.Text = timer.Interval.ToString();
        }
    }
}

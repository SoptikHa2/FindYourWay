using System;
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
        const int maxTicks = 1000;
        const int movesPerFrame = 3;

        private Graphics graphics;
        private Timer timer;
        private int ticks = 0;
        private SolidBrush textBrush = new SolidBrush(Color.Black);
        public DrawForm()
        {
            InitializeComponent();
            Game game = new Game(new Obstacle[] { new Obstacle(100, 200, 10, 200) }, maxTicks, 500);
            graphics = CreateGraphics();
            timer = new Timer();
            timer.Interval = 15;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            graphics.DrawString("Ticks: " + ticks + " / " + maxTicks, new Font("Arial", 11), textBrush, new PointF(0, 0));
            Game.currentGame.Draw(graphics);
            for (int i = 0; i < movesPerFrame; i++)
                Game.currentGame.Move(ticks);
            ticks += 1 * movesPerFrame;
            if (ticks >= maxTicks)
            {
                timer.Stop();
                ticks = 0;
            }else if(Game.currentGame.entities.Where(x => x.alive).Count() == 0)
            {
                timer.Stop();
                ticks = 0;
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
    }
}

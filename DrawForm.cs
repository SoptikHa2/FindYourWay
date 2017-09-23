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
        private Graphics graphics;
        private Timer timer;
        private int ticks = 0;
        private SolidBrush textBrush = new SolidBrush(Color.Black);
        public DrawForm()
        {
            InitializeComponent();
            Game game = new Game(new Obstacle[] { new Obstacle(100, 200, 10, 200) });
            graphics = CreateGraphics();
            timer = new Timer();
            timer.Interval = 15;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            graphics.DrawString("Ticks: " + ticks + " / 1000", new Font("Arial", 11), textBrush, new PointF(0, 0));
            Game.currentGame.Move(ticks);
            Game.currentGame.Draw(graphics);
            ticks++;
            if(ticks >= 1000)
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

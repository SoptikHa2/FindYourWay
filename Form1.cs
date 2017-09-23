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
    public partial class Form1 : Form
    {
        public static Form1 thisForm;

        public Form1()
        {
            InitializeComponent();
            thisForm = this;
        }

        /// <summary>
        /// Start new simulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            new DrawForm().Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                new DrawForm(true).Show();
                Hide();
            }
            catch { }
        }
    }
}

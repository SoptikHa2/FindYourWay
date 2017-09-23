namespace Find_Your_Way
{
    partial class DrawForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.nextgen = new System.Windows.Forms.Button();
            this.checkbox_autoNextGen = new System.Windows.Forms.CheckBox();
            this.setObstacles = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.movesPerDraw = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.reset_entities = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.delay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(500, 2);
            this.label1.MinimumSize = new System.Drawing.Size(0, 550);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 550);
            this.label1.TabIndex = 0;
            // 
            // nextgen
            // 
            this.nextgen.Location = new System.Drawing.Point(509, 13);
            this.nextgen.Name = "nextgen";
            this.nextgen.Size = new System.Drawing.Size(126, 23);
            this.nextgen.TabIndex = 1;
            this.nextgen.Text = "Next Generation";
            this.nextgen.UseVisualStyleBackColor = true;
            this.nextgen.Click += new System.EventHandler(this.nextgen_Click);
            // 
            // checkbox_autoNextGen
            // 
            this.checkbox_autoNextGen.AutoSize = true;
            this.checkbox_autoNextGen.Location = new System.Drawing.Point(640, 17);
            this.checkbox_autoNextGen.Name = "checkbox_autoNextGen";
            this.checkbox_autoNextGen.Size = new System.Drawing.Size(132, 17);
            this.checkbox_autoNextGen.TabIndex = 2;
            this.checkbox_autoNextGen.Text = "Automatically next gen";
            this.checkbox_autoNextGen.UseVisualStyleBackColor = true;
            this.checkbox_autoNextGen.CheckedChanged += new System.EventHandler(this.checkbox_autoNextGen_CheckedChanged);
            // 
            // setObstacles
            // 
            this.setObstacles.Location = new System.Drawing.Point(509, 43);
            this.setObstacles.Name = "setObstacles";
            this.setObstacles.Size = new System.Drawing.Size(126, 23);
            this.setObstacles.TabIndex = 3;
            this.setObstacles.Text = "Set Obstacles";
            this.setObstacles.UseVisualStyleBackColor = true;
            this.setObstacles.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(509, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Moves : Draw ratio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(640, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "1 :";
            // 
            // movesPerDraw
            // 
            this.movesPerDraw.Location = new System.Drawing.Point(665, 70);
            this.movesPerDraw.Name = "movesPerDraw";
            this.movesPerDraw.Size = new System.Drawing.Size(40, 20);
            this.movesPerDraw.TabIndex = 6;
            this.movesPerDraw.Text = "5";
            // 
            // save
            // 
            this.save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.save.Location = new System.Drawing.Point(512, 518);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(260, 31);
            this.save.TabIndex = 7;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // reset_entities
            // 
            this.reset_entities.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.reset_entities.Location = new System.Drawing.Point(512, 481);
            this.reset_entities.Name = "reset_entities";
            this.reset_entities.Size = new System.Drawing.Size(260, 31);
            this.reset_entities.TabIndex = 8;
            this.reset_entities.Text = "Reset Entities";
            this.reset_entities.UseVisualStyleBackColor = true;
            this.reset_entities.Click += new System.EventHandler(this.reset_entities_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(509, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Delay between frames";
            // 
            // delay
            // 
            this.delay.Location = new System.Drawing.Point(619, 96);
            this.delay.Name = "delay";
            this.delay.Size = new System.Drawing.Size(40, 20);
            this.delay.TabIndex = 10;
            this.delay.Text = "30";
            this.delay.TextChanged += new System.EventHandler(this.delay_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(665, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "15 - 100";
            // 
            // DrawForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.delay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.reset_entities);
            this.Controls.Add(this.save);
            this.Controls.Add(this.movesPerDraw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.setObstacles);
            this.Controls.Add(this.checkbox_autoNextGen);
            this.Controls.Add(this.nextgen);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DrawForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Your Way";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DrawForm_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawForm_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button nextgen;
        private System.Windows.Forms.CheckBox checkbox_autoNextGen;
        private System.Windows.Forms.Button setObstacles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox movesPerDraw;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button reset_entities;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox delay;
        private System.Windows.Forms.Label label5;
    }
}
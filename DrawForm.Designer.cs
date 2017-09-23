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
            // DrawForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button nextgen;
        private System.Windows.Forms.CheckBox checkbox_autoNextGen;
    }
}
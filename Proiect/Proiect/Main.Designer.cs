namespace Proiect
{
    partial class Main
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
            this.Canvas = new System.Windows.Forms.Panel();
            this.menuBox = new System.Windows.Forms.GroupBox();
            this.triangulateBttn = new System.Windows.Forms.Button();
            this.deleteBttn = new System.Windows.Forms.Button();
            this.menuBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Canvas.Location = new System.Drawing.Point(139, 13);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(552, 331);
            this.Canvas.TabIndex = 0;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            // 
            // menuBox
            // 
            this.menuBox.Controls.Add(this.deleteBttn);
            this.menuBox.Controls.Add(this.triangulateBttn);
            this.menuBox.Location = new System.Drawing.Point(13, 13);
            this.menuBox.Name = "menuBox";
            this.menuBox.Size = new System.Drawing.Size(120, 331);
            this.menuBox.TabIndex = 1;
            this.menuBox.TabStop = false;
            this.menuBox.Text = "Meniu";
            // 
            // triangulateBttn
            // 
            this.triangulateBttn.Location = new System.Drawing.Point(6, 19);
            this.triangulateBttn.Name = "triangulateBttn";
            this.triangulateBttn.Size = new System.Drawing.Size(108, 23);
            this.triangulateBttn.TabIndex = 0;
            this.triangulateBttn.Text = "Imparte";
            this.triangulateBttn.UseVisualStyleBackColor = true;
            this.triangulateBttn.Click += new System.EventHandler(this.TriangulateBttn_Click);
            // 
            // deleteBttn
            // 
            this.deleteBttn.Location = new System.Drawing.Point(6, 48);
            this.deleteBttn.Name = "deleteBttn";
            this.deleteBttn.Size = new System.Drawing.Size(108, 23);
            this.deleteBttn.TabIndex = 1;
            this.deleteBttn.Text = "Sterge";
            this.deleteBttn.UseVisualStyleBackColor = true;
            this.deleteBttn.Click += new System.EventHandler(this.deleteBttn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 356);
            this.Controls.Add(this.menuBox);
            this.Controls.Add(this.Canvas);
            this.Name = "Main";
            this.Text = "Proiect";
            this.menuBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.GroupBox menuBox;
        private System.Windows.Forms.Button triangulateBttn;
        private System.Windows.Forms.Button deleteBttn;
    }
}


namespace WindowsFormsUI
{
    partial class GraphicsEditor
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
            this.pcbWorkSpace = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbWorkSpace)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbWorkSpace
            // 
            this.pcbWorkSpace.BackColor = System.Drawing.Color.White;
            this.pcbWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.pcbWorkSpace.Name = "pcbWorkSpace";
            this.pcbWorkSpace.Size = new System.Drawing.Size(100, 50);
            this.pcbWorkSpace.TabIndex = 0;
            this.pcbWorkSpace.TabStop = false;
            this.pcbWorkSpace.Click += new System.EventHandler(this.pcbWorkSpace_Click);
            this.pcbWorkSpace.Paint += new System.Windows.Forms.PaintEventHandler(this.pcbWorkSpace_Paint);
            this.pcbWorkSpace.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pcbWorkSpace_MouseMove);
            // 
            // GraphicsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pcbWorkSpace);
            this.Name = "GraphicsEditor";
            this.Text = "GraphicsEditor";
            this.Load += new System.EventHandler(this.GraphicsEditor_Load);
            this.Shown += new System.EventHandler(this.GraphicsEditor_Shown);
            this.ResizeEnd += new System.EventHandler(this.GraphicsEditor_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.GraphicsEditor_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.GraphicsEditor_SizeChanged);
            this.Click += new System.EventHandler(this.GraphicsEditor_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraphicsEditor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pcbWorkSpace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbWorkSpace;
    }
}
namespace WindowsFormsUI
{
    partial class GraphicsVisualizer
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
            this.pcbGraphic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbGraphic)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbGraphic
            // 
            this.pcbGraphic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcbGraphic.Location = new System.Drawing.Point(0, 0);
            this.pcbGraphic.Name = "pcbGraphic";
            this.pcbGraphic.Size = new System.Drawing.Size(800, 450);
            this.pcbGraphic.TabIndex = 0;
            this.pcbGraphic.TabStop = false;
            // 
            // GraphicsVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pcbGraphic);
            this.Name = "GraphicsVisualizer";
            this.Text = "Wizualizator";
            this.Load += new System.EventHandler(this.GraphicsVisualizer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbGraphic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbGraphic;
    }
}
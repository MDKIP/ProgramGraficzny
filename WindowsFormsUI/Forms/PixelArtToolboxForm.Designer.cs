namespace WindowsFormsUI
{
    partial class PixelArtToolboxForm
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
            this.btnAddColor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddColor
            // 
            this.btnAddColor.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddColor.Location = new System.Drawing.Point(12, 12);
            this.btnAddColor.Name = "btnAddColor";
            this.btnAddColor.Size = new System.Drawing.Size(241, 23);
            this.btnAddColor.TabIndex = 0;
            this.btnAddColor.Text = "Dodaj Kolor";
            this.btnAddColor.UseVisualStyleBackColor = false;
            this.btnAddColor.Click += new System.EventHandler(this.btnAddColor_Click);
            // 
            // PixelArtToolboxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 450);
            this.Controls.Add(this.btnAddColor);
            this.Name = "PixelArtToolboxForm";
            this.Text = "PixelArtToolboxForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddColor;
    }
}
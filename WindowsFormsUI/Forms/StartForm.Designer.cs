namespace WindowsFormsUI
{
    partial class StartForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.btnCreateGraphics = new System.Windows.Forms.Button();
            this.btnLoadGraphic = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblName.Location = new System.Drawing.Point(13, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(359, 52);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Program Graficzny";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCreateGraphics
            // 
            this.btnCreateGraphics.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCreateGraphics.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCreateGraphics.Location = new System.Drawing.Point(46, 100);
            this.btnCreateGraphics.Name = "btnCreateGraphics";
            this.btnCreateGraphics.Size = new System.Drawing.Size(300, 35);
            this.btnCreateGraphics.TabIndex = 1;
            this.btnCreateGraphics.Text = "Stwórz Grafikę";
            this.btnCreateGraphics.UseVisualStyleBackColor = true;
            this.btnCreateGraphics.Click += new System.EventHandler(this.btnCreateGraphics_Click);
            // 
            // btnLoadGraphic
            // 
            this.btnLoadGraphic.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnLoadGraphic.Location = new System.Drawing.Point(46, 141);
            this.btnLoadGraphic.Name = "btnLoadGraphic";
            this.btnLoadGraphic.Size = new System.Drawing.Size(300, 35);
            this.btnLoadGraphic.TabIndex = 2;
            this.btnLoadGraphic.Text = "Wczytaj Grafikę";
            this.btnLoadGraphic.UseVisualStyleBackColor = true;
            this.btnLoadGraphic.Click += new System.EventHandler(this.btnLoadGraphic_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSettings.Location = new System.Drawing.Point(46, 182);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(300, 35);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Ustawienia";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnLoadGraphic);
            this.Controls.Add(this.btnCreateGraphics);
            this.Controls.Add(this.lblName);
            this.MaximumSize = new System.Drawing.Size(400, 500);
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "StartForm";
            this.Text = "Program Name (Program Version)";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnCreateGraphics;
        private System.Windows.Forms.Button btnLoadGraphic;
        private System.Windows.Forms.Button btnSettings;
    }
}
namespace WindowsFormsUI
{
    partial class SettingsForm
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
            this.lblVisualizer = new System.Windows.Forms.Label();
            this.lblColorOfVisualizerBackground = new System.Windows.Forms.Label();
            this.btnSetColorOfVisualizerBackground = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.lblPixelArt = new System.Windows.Forms.Label();
            this.lblRealPixelsPerEditorPixels = new System.Windows.Forms.Label();
            this.nudRealPixelsPerEditorPixels = new System.Windows.Forms.NumericUpDown();
            this.lblEditor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbThemes = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudRealPixelsPerEditorPixels)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVisualizer
            // 
            this.lblVisualizer.AutoSize = true;
            this.lblVisualizer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVisualizer.Location = new System.Drawing.Point(12, 210);
            this.lblVisualizer.Name = "lblVisualizer";
            this.lblVisualizer.Size = new System.Drawing.Size(122, 24);
            this.lblVisualizer.TabIndex = 0;
            this.lblVisualizer.Text = "Wizualizator";
            // 
            // lblColorOfVisualizerBackground
            // 
            this.lblColorOfVisualizerBackground.AutoSize = true;
            this.lblColorOfVisualizerBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblColorOfVisualizerBackground.Location = new System.Drawing.Point(13, 238);
            this.lblColorOfVisualizerBackground.Name = "lblColorOfVisualizerBackground";
            this.lblColorOfVisualizerBackground.Size = new System.Drawing.Size(63, 18);
            this.lblColorOfVisualizerBackground.TabIndex = 1;
            this.lblColorOfVisualizerBackground.Text = "Kolor tła";
            // 
            // btnSetColorOfVisualizerBackground
            // 
            this.btnSetColorOfVisualizerBackground.Location = new System.Drawing.Point(159, 238);
            this.btnSetColorOfVisualizerBackground.Name = "btnSetColorOfVisualizerBackground";
            this.btnSetColorOfVisualizerBackground.Size = new System.Drawing.Size(140, 18);
            this.btnSetColorOfVisualizerBackground.TabIndex = 2;
            this.btnSetColorOfVisualizerBackground.UseVisualStyleBackColor = true;
            this.btnSetColorOfVisualizerBackground.Click += new System.EventHandler(this.btnSetColorOfVisualizerBackground_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSaveChanges.Location = new System.Drawing.Point(159, 394);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(140, 33);
            this.btnSaveChanges.TabIndex = 3;
            this.btnSaveChanges.Text = "Zapisz zmiany";
            this.btnSaveChanges.UseVisualStyleBackColor = false;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // lblPixelArt
            // 
            this.lblPixelArt.AutoSize = true;
            this.lblPixelArt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPixelArt.Location = new System.Drawing.Point(12, 116);
            this.lblPixelArt.Name = "lblPixelArt";
            this.lblPixelArt.Size = new System.Drawing.Size(97, 24);
            this.lblPixelArt.TabIndex = 4;
            this.lblPixelArt.Text = "Piksel Art";
            // 
            // lblRealPixelsPerEditorPixels
            // 
            this.lblRealPixelsPerEditorPixels.AutoSize = true;
            this.lblRealPixelsPerEditorPixels.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblRealPixelsPerEditorPixels.Location = new System.Drawing.Point(13, 151);
            this.lblRealPixelsPerEditorPixels.Name = "lblRealPixelsPerEditorPixels";
            this.lblRealPixelsPerEditorPixels.Size = new System.Drawing.Size(129, 18);
            this.lblRealPixelsPerEditorPixels.TabIndex = 5;
            this.lblRealPixelsPerEditorPixels.Text = "Domyślny RPPEP";
            // 
            // nudRealPixelsPerEditorPixels
            // 
            this.nudRealPixelsPerEditorPixels.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudRealPixelsPerEditorPixels.Location = new System.Drawing.Point(159, 149);
            this.nudRealPixelsPerEditorPixels.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudRealPixelsPerEditorPixels.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRealPixelsPerEditorPixels.Name = "nudRealPixelsPerEditorPixels";
            this.nudRealPixelsPerEditorPixels.Size = new System.Drawing.Size(140, 20);
            this.nudRealPixelsPerEditorPixels.TabIndex = 6;
            this.nudRealPixelsPerEditorPixels.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblEditor
            // 
            this.lblEditor.AutoSize = true;
            this.lblEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblEditor.Location = new System.Drawing.Point(12, 9);
            this.lblEditor.Name = "lblEditor";
            this.lblEditor.Size = new System.Drawing.Size(70, 24);
            this.lblEditor.TabIndex = 7;
            this.lblEditor.Text = "Edytor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(13, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Styl";
            // 
            // cmbThemes
            // 
            this.cmbThemes.FormattingEnabled = true;
            this.cmbThemes.Location = new System.Drawing.Point(159, 42);
            this.cmbThemes.Name = "cmbThemes";
            this.cmbThemes.Size = new System.Drawing.Size(140, 21);
            this.cmbThemes.TabIndex = 9;
            this.cmbThemes.SelectedIndexChanged += new System.EventHandler(this.cmbThemes_SelectedIndexChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 439);
            this.Controls.Add(this.cmbThemes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblEditor);
            this.Controls.Add(this.nudRealPixelsPerEditorPixels);
            this.Controls.Add(this.lblRealPixelsPerEditorPixels);
            this.Controls.Add(this.lblPixelArt);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnSetColorOfVisualizerBackground);
            this.Controls.Add(this.lblColorOfVisualizerBackground);
            this.Controls.Add(this.lblVisualizer);
            this.Name = "SettingsForm";
            this.Text = "Ustawienia ";
            ((System.ComponentModel.ISupportInitialize)(this.nudRealPixelsPerEditorPixels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVisualizer;
        private System.Windows.Forms.Label lblColorOfVisualizerBackground;
        private System.Windows.Forms.Button btnSetColorOfVisualizerBackground;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Label lblPixelArt;
        private System.Windows.Forms.Label lblRealPixelsPerEditorPixels;
        private System.Windows.Forms.NumericUpDown nudRealPixelsPerEditorPixels;
        private System.Windows.Forms.Label lblEditor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbThemes;
    }
}
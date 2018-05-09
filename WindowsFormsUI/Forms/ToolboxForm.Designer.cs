namespace WindowsFormsUI
{
    partial class ToolboxForm
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
            this.cmbTools = new System.Windows.Forms.ComboBox();
            this.lblPenSize = new System.Windows.Forms.Label();
            this.nudPenSize = new System.Windows.Forms.NumericUpDown();
            this.btnSetColorForPen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPenSize)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbTools
            // 
            this.cmbTools.FormattingEnabled = true;
            this.cmbTools.Items.AddRange(new object[] {
            "Linia"});
            this.cmbTools.Location = new System.Drawing.Point(13, 13);
            this.cmbTools.Name = "cmbTools";
            this.cmbTools.Size = new System.Drawing.Size(159, 21);
            this.cmbTools.TabIndex = 0;
            this.cmbTools.Text = "Linia";
            this.cmbTools.SelectedIndexChanged += new System.EventHandler(this.cmbTools_SelectedIndexChanged);
            // 
            // lblPenSize
            // 
            this.lblPenSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPenSize.Location = new System.Drawing.Point(13, 41);
            this.lblPenSize.Name = "lblPenSize";
            this.lblPenSize.Size = new System.Drawing.Size(59, 20);
            this.lblPenSize.TabIndex = 1;
            this.lblPenSize.Text = "Rozmiar";
            // 
            // nudPenSize
            // 
            this.nudPenSize.Location = new System.Drawing.Point(78, 41);
            this.nudPenSize.Name = "nudPenSize";
            this.nudPenSize.Size = new System.Drawing.Size(94, 20);
            this.nudPenSize.TabIndex = 2;
            this.nudPenSize.ValueChanged += new System.EventHandler(this.nudPenSize_ValueChanged);
            // 
            // btnSetColorForPen
            // 
            this.btnSetColorForPen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSetColorForPen.Location = new System.Drawing.Point(13, 67);
            this.btnSetColorForPen.Name = "btnSetColorForPen";
            this.btnSetColorForPen.Size = new System.Drawing.Size(159, 23);
            this.btnSetColorForPen.TabIndex = 3;
            this.btnSetColorForPen.Text = "Ustaw Kolor";
            this.btnSetColorForPen.UseVisualStyleBackColor = true;
            this.btnSetColorForPen.Click += new System.EventHandler(this.btnSetColorForPen_Click);
            // 
            // ToolboxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 461);
            this.Controls.Add(this.btnSetColorForPen);
            this.Controls.Add(this.nudPenSize);
            this.Controls.Add(this.lblPenSize);
            this.Controls.Add(this.cmbTools);
            this.MaximumSize = new System.Drawing.Size(400, 1000);
            this.MinimumSize = new System.Drawing.Size(200, 300);
            this.Name = "ToolboxForm";
            this.Text = "ToolboxForm";
            this.Load += new System.EventHandler(this.ToolboxForm_Load);
            this.SizeChanged += new System.EventHandler(this.ToolboxForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.nudPenSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTools;
        private System.Windows.Forms.Label lblPenSize;
        private System.Windows.Forms.NumericUpDown nudPenSize;
        private System.Windows.Forms.Button btnSetColorForPen;
    }
}
namespace WindowsFormsUI
{
    partial class NewGraphicForm
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
            this.lblGraphicType = new System.Windows.Forms.Label();
            this.cmbGraphicType = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.lblPathToImage = new System.Windows.Forms.Label();
            this.lblWidthOfSelectedImage = new System.Windows.Forms.Label();
            this.lblHeightOfSelectedImage = new System.Windows.Forms.Label();
            this.cmbSizeOfPixelArt = new System.Windows.Forms.ComboBox();
            this.lblSizeOfPixelArt = new System.Windows.Forms.Label();
            this.lblSizePerPixel = new System.Windows.Forms.Label();
            this.nudSizePerPixel = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudSizePerPixel)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGraphicType
            // 
            this.lblGraphicType.AutoSize = true;
            this.lblGraphicType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblGraphicType.Location = new System.Drawing.Point(13, 13);
            this.lblGraphicType.Name = "lblGraphicType";
            this.lblGraphicType.Size = new System.Drawing.Size(73, 16);
            this.lblGraphicType.TabIndex = 0;
            this.lblGraphicType.Text = "Typ Grafiki";
            // 
            // cmbGraphicType
            // 
            this.cmbGraphicType.FormattingEnabled = true;
            this.cmbGraphicType.Items.AddRange(new object[] {
            "Pusta",
            "Zdjęcie",
            "Piksel Art"});
            this.cmbGraphicType.Location = new System.Drawing.Point(92, 8);
            this.cmbGraphicType.Name = "cmbGraphicType";
            this.cmbGraphicType.Size = new System.Drawing.Size(191, 21);
            this.cmbGraphicType.TabIndex = 1;
            this.cmbGraphicType.Text = "Pusta";
            this.cmbGraphicType.SelectedIndexChanged += new System.EventHandler(this.cmbGraphicType_SelectedIndexChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCreate.Location = new System.Drawing.Point(13, 223);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(271, 35);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Stwórz";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWidth.Location = new System.Drawing.Point(14, 55);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(72, 16);
            this.lblWidth.TabIndex = 3;
            this.lblWidth.Text = "Szerokość";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblHeight.Location = new System.Drawing.Point(14, 81);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(72, 16);
            this.lblHeight.TabIndex = 4;
            this.lblHeight.Text = "Wysokość";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(92, 51);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(191, 20);
            this.txtWidth.TabIndex = 5;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(92, 77);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(191, 20);
            this.txtHeight.TabIndex = 6;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSelectImage.Location = new System.Drawing.Point(12, 48);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(272, 23);
            this.btnSelectImage.TabIndex = 7;
            this.btnSelectImage.Text = "Wybierz zdjęcie";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // lblPathToImage
            // 
            this.lblPathToImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPathToImage.Location = new System.Drawing.Point(13, 77);
            this.lblPathToImage.Name = "lblPathToImage";
            this.lblPathToImage.Size = new System.Drawing.Size(270, 98);
            this.lblPathToImage.TabIndex = 8;
            this.lblPathToImage.Text = "Ścieżka";
            // 
            // lblWidthOfSelectedImage
            // 
            this.lblWidthOfSelectedImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWidthOfSelectedImage.Location = new System.Drawing.Point(12, 179);
            this.lblWidthOfSelectedImage.Name = "lblWidthOfSelectedImage";
            this.lblWidthOfSelectedImage.Size = new System.Drawing.Size(127, 23);
            this.lblWidthOfSelectedImage.TabIndex = 9;
            this.lblWidthOfSelectedImage.Text = "Szerokość:";
            // 
            // lblHeightOfSelectedImage
            // 
            this.lblHeightOfSelectedImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblHeightOfSelectedImage.Location = new System.Drawing.Point(145, 179);
            this.lblHeightOfSelectedImage.Name = "lblHeightOfSelectedImage";
            this.lblHeightOfSelectedImage.Size = new System.Drawing.Size(138, 23);
            this.lblHeightOfSelectedImage.TabIndex = 10;
            this.lblHeightOfSelectedImage.Text = "Wysokość:";
            // 
            // cmbSizeOfPixelArt
            // 
            this.cmbSizeOfPixelArt.FormattingEnabled = true;
            this.cmbSizeOfPixelArt.Items.AddRange(new object[] {
            "16",
            "32",
            "64"});
            this.cmbSizeOfPixelArt.Location = new System.Drawing.Point(92, 48);
            this.cmbSizeOfPixelArt.Name = "cmbSizeOfPixelArt";
            this.cmbSizeOfPixelArt.Size = new System.Drawing.Size(191, 21);
            this.cmbSizeOfPixelArt.TabIndex = 11;
            this.cmbSizeOfPixelArt.Text = "32";
            this.cmbSizeOfPixelArt.SelectedIndexChanged += new System.EventHandler(this.cmbSizeOfPixelArt_SelectedIndexChanged);
            // 
            // lblSizeOfPixelArt
            // 
            this.lblSizeOfPixelArt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSizeOfPixelArt.Location = new System.Drawing.Point(16, 48);
            this.lblSizeOfPixelArt.Name = "lblSizeOfPixelArt";
            this.lblSizeOfPixelArt.Size = new System.Drawing.Size(70, 21);
            this.lblSizeOfPixelArt.TabIndex = 12;
            this.lblSizeOfPixelArt.Text = "Rozmiar";
            // 
            // lblSizePerPixel
            // 
            this.lblSizePerPixel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSizePerPixel.Location = new System.Drawing.Point(16, 78);
            this.lblSizePerPixel.Name = "lblSizePerPixel";
            this.lblSizePerPixel.Size = new System.Drawing.Size(115, 19);
            this.lblSizePerPixel.TabIndex = 13;
            this.lblSizePerPixel.Text = "RPPEP";
            // 
            // nudSizePerPixel
            // 
            this.nudSizePerPixel.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudSizePerPixel.Location = new System.Drawing.Point(137, 78);
            this.nudSizePerPixel.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudSizePerPixel.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudSizePerPixel.Name = "nudSizePerPixel";
            this.nudSizePerPixel.Size = new System.Drawing.Size(146, 20);
            this.nudSizePerPixel.TabIndex = 14;
            this.nudSizePerPixel.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // NewGraphicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 270);
            this.Controls.Add(this.nudSizePerPixel);
            this.Controls.Add(this.lblSizePerPixel);
            this.Controls.Add(this.lblSizeOfPixelArt);
            this.Controls.Add(this.cmbSizeOfPixelArt);
            this.Controls.Add(this.lblHeightOfSelectedImage);
            this.Controls.Add(this.lblWidthOfSelectedImage);
            this.Controls.Add(this.lblPathToImage);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cmbGraphicType);
            this.Controls.Add(this.lblGraphicType);
            this.MaximumSize = new System.Drawing.Size(312, 309);
            this.MinimumSize = new System.Drawing.Size(312, 309);
            this.Name = "NewGraphicForm";
            this.Text = "NewGraphicForm";
            this.Load += new System.EventHandler(this.NewGraphicForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSizePerPixel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGraphicType;
        private System.Windows.Forms.ComboBox cmbGraphicType;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Label lblPathToImage;
        private System.Windows.Forms.Label lblWidthOfSelectedImage;
        private System.Windows.Forms.Label lblHeightOfSelectedImage;
        private System.Windows.Forms.ComboBox cmbSizeOfPixelArt;
        private System.Windows.Forms.Label lblSizeOfPixelArt;
        private System.Windows.Forms.Label lblSizePerPixel;
        private System.Windows.Forms.NumericUpDown nudSizePerPixel;
    }
}
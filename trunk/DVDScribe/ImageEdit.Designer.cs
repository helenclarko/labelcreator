namespace DVDScribe
{
    partial class ImageEdit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.pbxImage = new System.Windows.Forms.PictureBox();
            this.bottomRight = new System.Windows.Forms.PictureBox();
            this.bottomLeft = new System.Windows.Forms.PictureBox();
            this.topRight = new System.Windows.Forms.PictureBox();
            this.topLeft = new System.Windows.Forms.PictureBox();
            this.pnlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEdit.Controls.Add(this.btnLoadFile);
            this.pnlEdit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlEdit.Location = new System.Drawing.Point(16, 16);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Padding = new System.Windows.Forms.Padding(1);
            this.pnlEdit.Size = new System.Drawing.Size(27, 25);
            this.pnlEdit.TabIndex = 1;
            this.pnlEdit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageEdit_MouseMove);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLoadFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadFile.FlatAppearance.BorderSize = 0;
            this.btnLoadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFile.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLoadFile.Image = global::DVDScribe.Properties.Resources.img_add_image;
            this.btnLoadFile.Location = new System.Drawing.Point(1, 1);
            this.btnLoadFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(23, 21);
            this.btnLoadFile.TabIndex = 1;
            this.btnLoadFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLoadFile.UseVisualStyleBackColor = false;
            this.btnLoadFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // pbxImage
            // 
            this.pbxImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxImage.Location = new System.Drawing.Point(1, 1);
            this.pbxImage.Margin = new System.Windows.Forms.Padding(0);
            this.pbxImage.Name = "pbxImage";
            this.pbxImage.Size = new System.Drawing.Size(185, 141);
            this.pbxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImage.TabIndex = 0;
            this.pbxImage.TabStop = false;
            this.pbxImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageEdit_MouseMove);
            // 
            // bottomRight
            // 
            this.bottomRight.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bottomRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomRight.Location = new System.Drawing.Point(178, 134);
            this.bottomRight.Name = "bottomRight";
            this.bottomRight.Size = new System.Drawing.Size(8, 8);
            this.bottomRight.TabIndex = 2;
            this.bottomRight.TabStop = false;
            // 
            // bottomLeft
            // 
            this.bottomLeft.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bottomLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomLeft.Location = new System.Drawing.Point(0, 134);
            this.bottomLeft.Name = "bottomLeft";
            this.bottomLeft.Size = new System.Drawing.Size(8, 8);
            this.bottomLeft.TabIndex = 3;
            this.bottomLeft.TabStop = false;
            // 
            // topRight
            // 
            this.topRight.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.topRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topRight.Location = new System.Drawing.Point(179, 0);
            this.topRight.Name = "topRight";
            this.topRight.Size = new System.Drawing.Size(8, 8);
            this.topRight.TabIndex = 4;
            this.topRight.TabStop = false;
            // 
            // topLeft
            // 
            this.topLeft.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.topLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topLeft.Location = new System.Drawing.Point(0, 0);
            this.topLeft.Name = "topLeft";
            this.topLeft.Size = new System.Drawing.Size(8, 8);
            this.topLeft.TabIndex = 5;
            this.topLeft.TabStop = false;
            this.topLeft.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // ImageEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.topLeft);
            this.Controls.Add(this.topRight);
            this.Controls.Add(this.bottomLeft);
            this.Controls.Add(this.bottomRight);
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.pbxImage);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ImageEdit";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(187, 143);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ImageEdit_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageEdit_MouseMove);
            this.Resize += new System.EventHandler(this.ImageEdit_Resize);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageEdit_MouseUp);
            this.pnlEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topLeft)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Button btnLoadFile;
        public System.Windows.Forms.OpenFileDialog dlgOpenFile;
        public System.Windows.Forms.PictureBox pbxImage;
        private System.Windows.Forms.PictureBox bottomRight;
        private System.Windows.Forms.PictureBox bottomLeft;
        private System.Windows.Forms.PictureBox topRight;
        private System.Windows.Forms.PictureBox topLeft;

    }
}

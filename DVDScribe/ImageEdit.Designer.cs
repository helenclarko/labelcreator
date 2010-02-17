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
            this.pnlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEdit.Controls.Add(this.btnLoadFile);
            this.pnlEdit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlEdit.Location = new System.Drawing.Point(4, 4);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Padding = new System.Windows.Forms.Padding(1);
            this.pnlEdit.Size = new System.Drawing.Size(23, 20);
            this.pnlEdit.TabIndex = 1;
            this.pnlEdit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageEdit_MouseMove);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLoadFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLoadFile.FlatAppearance.BorderSize = 0;
            this.btnLoadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFile.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLoadFile.Image = global::DVDScribe.Properties.Resources.img_add_image;
            this.btnLoadFile.Location = new System.Drawing.Point(1, -2);
            this.btnLoadFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(19, 19);
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
            // ImageEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Button btnLoadFile;
        public System.Windows.Forms.OpenFileDialog dlgOpenFile;
        public System.Windows.Forms.PictureBox pbxImage;

    }
}

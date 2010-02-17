namespace DVDScribe
{
    partial class frmSelectBackground
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectBackground));
            this.ilFileList = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.lv = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilFileList
            // 
            this.ilFileList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilFileList.ImageStream")));
            this.ilFileList.TransparentColor = System.Drawing.Color.Transparent;
            this.ilFileList.Images.SetKeyName(0, "computer.png");
            this.ilFileList.Images.SetKeyName(1, "hdd_unmount.png");
            this.ilFileList.Images.SetKeyName(2, "usbpendrive_unmount.png");
            this.ilFileList.Images.SetKeyName(3, "cdwriter_mount.png");
            this.ilFileList.Images.SetKeyName(4, "folder_orange.png");
            this.ilFileList.Images.SetKeyName(5, "folder_open.png");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(3, 16);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(7, 426);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // lv
            // 
            this.lv.Location = new System.Drawing.Point(340, 81);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(389, 426);
            this.lv.TabIndex = 7;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            this.lv.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lv);
            this.groupBox1.Controls.Add(this.splitter1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(642, 445);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select image";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // frmSelectBackground
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 445);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSelectBackground";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select image to LightScribe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectBackground_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList ilFileList;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}